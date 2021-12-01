using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RailMoveController : MonoBehaviour
{
    public Tween tween;

    [SerializeField]
    private RailPathData railPathData;

    private Vector3[] paths;

    [SerializeField, Header("移動時間")]
    private float moveTime;

    private bool isStop;

    private GameManager gameManager;

    /// <summary>
    /// RailMoveControllerの設定
    /// </summary>
    /// <param name="gameManager"></param>
    public void SetUpRailMoveController(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    /// <summary>
    /// 移動
    /// </summary>
    public void Move()
    {
        //railPathDataからTransform型の配列を取得し，Vector３型に直して変数に代入
        paths = railPathData.railPathDatas.Select(x => x.position).ToArray();

        //Pathデータを順に進んでいき，最後のところで続きあるか確認。なければ終了。
        tween = transform.DOPath(paths, moveTime).SetEase(Ease.Linear).OnComplete(() => gameManager.CheckNextRailPathData());
        Debug.Log("移動開始");
    }

    /// <summary>
    /// 停止
    /// </summary>
    public void Stop()
    {
        tween.Pause();
        Debug.Log("停止");
    }

    /// <summary>
    /// 移動再開
    /// </summary>
    public void Resume()
    {
        tween.Play();
        Debug.Log("移動再開");
    }
    
    //移動と停止を変更する
    public IEnumerator ChangeMove()
    {
        while (gameManager.currentGameState == GameState.Play)
        {
            //移動中にスペースを押すと止まる
            if (isStop == false && Input.GetKeyDown(KeyCode.Space))
            {
                Stop();
                isStop = true;
            }
            //停止中にスペースを押すと移動再開
            else if (isStop && Input.GetKeyDown(KeyCode.Space))
            {
                Resume();
                isStop = false;
            }
            yield return null;
        }
    }
}
