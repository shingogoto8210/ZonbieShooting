using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RailMoveController : MonoBehaviour
{
    private Tween tween;

    //目的地を配列で管理しているクラス
    private RailPathData railPathData;

    //RailPathDataの情報をVector3型にして管理するための変数
    private Vector3[] paths;

    [SerializeField, Header("移動時間")]
    private float moveTime;

    private GameManager gameManager;

    private PlayerController player;

    public bool isStop;


    private void Update()
    {
        ///StopAndMove();
        //Debug.Log(isStop);

    }

    /// <summary>
    /// RailMoveControllerの設定
    /// </summary>
    /// <param name="gameManager"></param>
    public void SetUpRailMoveController(GameManager gameManager, RailPathData railPathData, PlayerController player)
    {
        this.gameManager = gameManager;
        this.railPathData = railPathData;
        this.player = player;
    }

    /// <summary>
    /// 移動
    /// </summary>
    public void Move()
    {
        //railPathDataからTransform型の配列を取得し，Vector３型に直して変数に代入
        paths = railPathData.railPathDatas.Select(x => x.transform.position).ToArray();

        isStop = false;

        //Pathデータを順に進んでいき，最後のところで続きあるか確認。なければ終了。
        tween = transform.DOPath(paths, moveTime).SetEase(Ease.Linear).OnWaypointChange((index) => gameManager.CheckEvent(index)).OnComplete(() => gameManager.GameOver());
        
        //player.MoveAnimation(true);

        player.PlayerWalkFootStep();
        Debug.Log("移動開始");
    }

    /// <summary>
    /// 移動停止
    /// </summary>
    public void Stop()
    {
        tween.Pause();
        isStop = true;
        //player.MoveAnimation(false);
        player.StopFootStep();
        Debug.Log("停止");
    }

    /// <summary>
    /// 移動再開
    /// </summary>
    public void Resume()
    {
        tween.Play();
        isStop = false;
        //player.MoveAnimation(true);
        player.PlayerWalkFootStep();
        Debug.Log("移動再開");
    }

    /// <summary>
    /// tweenを破棄する
    /// </summary>
    public void KillTween()
    {
        player.StopFootStep();
        tween.Kill();
    }

    

    //private void StopAndMove()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && isStop == false)
    //    {
    //        Stop();
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Space) && isStop == true)
    //    {
    //        Resume();
    //    }
    //}

}
