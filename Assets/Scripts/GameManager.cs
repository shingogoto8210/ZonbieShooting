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
        //�Q�[���̏�ԁi�������j
        currentGameState = GameState.Wait;
        Debug.Log("������");

        //RailMoveController�̐ݒ�
        railMoveController.SetUpRailMoveController(this, railPathData,playerController);

        playerController.SetUpPlayerController();

        weapon.SetUpWeapon(this,playerController);

        //�Q�[���̏�ԁi�v���C���j
        currentGameState = GameState.Move;
        Debug.Log("�Q�[���J�n");

        //�ړ��J�n
        railMoveController.Move();


    }

    /// <summary>
    /// ����RailPathData�����邩�m�F
    /// </summary>
    public void CheckNextRailPathData()
    {

        //����RailPathData���Ȃ���΃Q�[���I��
        currentGameState = GameState.GameOver;

        //tween��Kill
        railMoveController.KillTween();

        //�v���C���[�̓������~�߂�
        playerController.MoveAnimation(false);

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

            currentGameState = GameState.Event;

            Debug.Log("�C�x���g����");

            enemyGenerator.GenerateEnemy();

            railMoveController.Stop();

        }
    }

    /// <summary>
    /// ���ׂĂ̓G���|�ꂽ���m�F
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
