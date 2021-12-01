using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private RailMoveController railMoveController;

    public GameState currentGameState;

    void Start()
    {
        //ゲームの状態（準備中）
        currentGameState = GameState.Wait;
        Debug.Log("準備中");

        //railMoveControllerの設定
        railMoveController.SetUpRailMoveController(this);

        //ゲームの状態（プレイ中）
        currentGameState = GameState.Play;
        Debug.Log("ゲーム開始");

        //移動開始
        //railMoveController.Move();

        //プレイ中は移動と停止の操作可能
        StartCoroutine(railMoveController.ChangeMove());
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
        railMoveController.tween.Kill();

        Debug.Log("ゲーム終了");

    }
}
