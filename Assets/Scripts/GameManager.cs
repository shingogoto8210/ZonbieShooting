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
        //�Q�[���̏�ԁi�������j
        currentGameState = GameState.Wait;
        Debug.Log("������");

        //railMoveController�̐ݒ�
        railMoveController.SetUpRailMoveController(this);

        //�Q�[���̏�ԁi�v���C���j
        currentGameState = GameState.Play;
        Debug.Log("�Q�[���J�n");

        //�ړ��J�n
        //railMoveController.Move();

        //�v���C���͈ړ��ƒ�~�̑���\
        StartCoroutine(railMoveController.ChangeMove());
    }

    /// <summary>
    /// ����RailPathData�����邩�m�F
    /// </summary>
    public void CheckNextRailPathData()
    {
        //TODO ����RailPathData������΁C������ɐi��

        //����RailPathData���Ȃ���΃Q�[���I��
        currentGameState = GameState.GameOver;
        
        //tween��Kill
        railMoveController.tween.Kill();

        Debug.Log("�Q�[���I��");

    }
}
