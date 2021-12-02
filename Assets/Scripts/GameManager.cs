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
        //�Q�[���̏�ԁi�������j
        currentGameState = GameState.Wait;
        Debug.Log("������");

        //RailMoveController�̐ݒ�
        railMoveController.SetUpRailMoveController(this, railPathData);

        //�v���C���[�̐ݒ�
        playerController.SetUpPlayer();

        //RayController�̐ݒ�
        rayController.SetUpRayController(playerController);

        //�Q�[���̏�ԁi�v���C���j
        currentGameState = GameState.Play;
        Debug.Log("�Q�[���J�n");

        //�ړ��J�n
        railMoveController.Move();

        //�v���C���͈ړ��ƒ�~�̑���\
        StartCoroutine(railMoveController.ChangeMove());
    }

    private void Update()
    {
        if (playerController.BulletCount > 0 && Input.GetMouseButton(0))
        {
            //���Ԏ��Ԃ̌v��
            StartCoroutine(rayController.ShootTimer());
        }
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
        railMoveController.KillTween();

        Debug.Log("�Q�[���I��");

    }

    /// <summary>
    /// ���̏ꏊ�ŃC�x���g�����邩�m�F
    /// </summary>
    public void CheckEvent(int index)
    {
        Debug.Log(index + "�Ԗڂ̖ړI�n");


        if (index >= 1 && railPathData.railPathDatas[index - 1].isEvent)
        {
            Debug.Log("�C�x���g����");

            enemyGenerator.GenerateEnemy();

            railMoveController.Stop();
            
        }
    }
}
