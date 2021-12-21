using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState currentGameState;
    
    [SerializeField]
    private RailMoveController railMoveController;

    [SerializeField]
    private RailPathData railPathData;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private UIManager uiManager;

    //イベント地点ごとにGeneratorを用意
    public EnemyGenerator[] enemyGenerators;
    
    public List<EnemyController> enemiesList;

    private int eventNo = 0;

    void Start()
    {
        //ゲームの状態（準備中）
        currentGameState = GameState.Wait;
        Debug.Log("準備中");

        //初期設定
        railMoveController.SetUpRailMoveController(this, railPathData,playerController);
        playerController.SetUpPlayerController(uiManager);
        weapon.SetUpWeapon(this,playerController);

        //ゲームの状態（プレイ中）
        currentGameState = GameState.Move;
        Debug.Log("ゲーム開始");

        //移動開始
        railMoveController.Move();

    }

    /// <summary>
    /// その場所でイベントがあるか確認
    /// </summary>
    public void CheckEvent(int index)
    {
        Debug.Log(index + "番目の目的地");

        if (index >= 1 && railPathData.railPathDatas[index - 1].isEvent)
        {

            currentGameState = GameState.Event;

            railMoveController.Stop();

            Debug.Log("イベント発生");

            StartCoroutine(enemyGenerators[eventNo].GenerateEnemy());
        }
    }

    /// <summary>
    /// すべての敵が倒れたか確認
    /// </summary>
    public IEnumerator CheckFinishEvent()
    {
        //EnemyGeneratorによって生成された敵が全て倒れたか確認する
        if (enemiesList.Count <= 0 && enemyGenerators[eventNo].isFinish == true)
        {
            currentGameState = GameState.Move;
            yield return new WaitForSeconds(3.0f);
            railMoveController.Resume();
            eventNo++;
            Debug.Log("イベント終了");
        }
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void GameOver()
    {

        //ゲーム終了
        currentGameState = GameState.GameOver;

        //tweenをKill
        railMoveController.KillTween();

        //プレイヤーの動きを止める
        //playerController.MoveAnimation(false);

        Debug.Log("ゲーム終了");

    }
}
