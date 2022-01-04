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

    //�C�x���g�n�_���Ƃ�Generator��p��
    public EnemyGenerator[] enemyGenerators;
    
    public List<EnemyController> enemiesList;

    private int eventNo = 0;

    IEnumerator Start()
    {
        //�Q�[���̏�ԁi�������j
        currentGameState = GameState.Wait;
        Debug.Log("������");

        //�����ݒ�
        railMoveController.SetUpRailMoveController(this, railPathData,playerController);
        playerController.SetUpPlayerController(uiManager);
        weapon.SetUpWeapon(this,playerController);

        //���o
        yield return StartCoroutine(uiManager.GameStartLogo());

        //�Q�[���̏�ԁi�v���C���j
        Debug.Log("�Q�[���J�n");
        currentGameState = GameState.Move;

        //�ړ��J�n
        railMoveController.Move();

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

            railMoveController.Stop();

            Debug.Log("�C�x���g����");

            StartCoroutine(enemyGenerators[eventNo].GenerateEnemy());
        }
    }

    /// <summary>
    /// ���ׂĂ̓G���|�ꂽ���m�F
    /// </summary>
    public IEnumerator CheckFinishEvent()
    {
        //EnemyGenerator�ɂ���Đ������ꂽ�G���S�ē|�ꂽ���m�F����
        if (enemiesList.Count <= 0 && enemyGenerators[eventNo].isFinish == true)
        {
            currentGameState = GameState.Move;
            yield return new WaitForSeconds(3.0f);
            railMoveController.Resume();
            eventNo++;
            Debug.Log("�C�x���g�I��");
        }
    }

    /// <summary>
    /// �Q�[���I��
    /// </summary>
    public void GameOver()
    {

        //�Q�[���I��
        currentGameState = GameState.GameOver;

        uiManager.GameOverLogo();

        //tween��Kill
        railMoveController.KillTween();

        //�v���C���[�̓������~�߂�
        //playerController.MoveAnimation(false);

        Debug.Log("�Q�[���I��");

    }

    
}
