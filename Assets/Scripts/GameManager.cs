using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private RailMoveController railMoveController;

    public GameState currentGameState;

    [SerializeField]
    private RailPathData railPathData;

    [SerializeField]
    private EnemyGenerator enemyGenerator;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private Weapon weapon;

    public List<EnemyController> enemiesList;

    void Start()
    {
        //ゲームの状態（準備中）
        currentGameState = GameState.Wait;
        Debug.Log("準備中");

        //RailMoveControllerの設定
        railMoveController.SetUpRailMoveController(this, railPathData,playerController);

        playerController.SetUpPlayerController();

        weapon.SetUpWeapon(this,playerController);

        //ゲームの状態（プレイ中）
        currentGameState = GameState.Move;
        Debug.Log("ゲーム開始");

        //移動開始
        railMoveController.Move();


    }

    /// <summary>
    /// 次のRailPathDataがあるか確認
    /// </summary>
    public void CheckNextRailPathData()
    {

        //次のRailPathDataがなければゲーム終了
        currentGameState = GameState.GameOver;

        //tweenをKill
        railMoveController.KillTween();

        //プレイヤーの動きを止める
        playerController.MoveAnimation(false);

        Debug.Log("ゲーム終了");

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

            Debug.Log("イベント発生");

            enemyGenerator.GenerateEnemy();

            railMoveController.Stop();

        }
    }

    /// <summary>
    /// すべての敵が倒れたか確認
    /// </summary>
    public void CheckFinishEvent()
    {
        if (enemiesList.Count <= 0)
        {
            currentGameState = GameState.Move;
            railMoveController.Resume();
        }
    }
}
