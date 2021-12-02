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
    private RayController rayController;

    void Start()
    {
        //ゲームの状態（準備中）
        currentGameState = GameState.Wait;
        Debug.Log("準備中");

        //RailMoveControllerの設定
        railMoveController.SetUpRailMoveController(this, railPathData);

        //プレイヤーの設定
        playerController.SetUpPlayer();

        //RayControllerの設定
        rayController.SetUpRayController(playerController);

        //ゲームの状態（プレイ中）
        currentGameState = GameState.Play;
        Debug.Log("ゲーム開始");

        //移動開始
        railMoveController.Move();

        //プレイ中は移動と停止の操作可能
        StartCoroutine(railMoveController.ChangeMove());
    }

    private void Update()
    {
        if (playerController.BulletCount > 0 && Input.GetMouseButton(0))
        {
            //発車時間の計測
            StartCoroutine(rayController.ShootTimer());
        }
    }

    /// <summary>
    /// 次のRailPathDataがあるか確認
    /// </summary>
    public void CheckNextRailPathData()
    {
        //TODO 次のRailPathDataがあれば，そちらに進む

        //次のRailPathDataがなければゲーム終了
        currentGameState = GameState.GameOver;

        //tweenをKill
        railMoveController.KillTween();

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
            Debug.Log("イベント発生");

            enemyGenerator.GenerateEnemy();

            railMoveController.Stop();
            
        }
    }
}
