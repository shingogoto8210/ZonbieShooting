using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RailMoveController : MonoBehaviour
{
    private Tween tween;

    private RailPathData railPathData;

    private Vector3[] paths;

    [SerializeField, Header("�ړ�����")]
    private float moveTime;

    private GameManager gameManager;

    private PlayerController player;

    public AudioSource playerFootStep;

    public AudioClip walkFootStepSE;

    private bool isStop;


    private void Update()
    {
        StopAndMove();

        //Debug.Log(isStop);

    }
    /// <summary>
    /// RailMoveController�̐ݒ�
    /// </summary>
    /// <param name="gameManager"></param>
    public void SetUpRailMoveController(GameManager gameManager, RailPathData railPathData, PlayerController player)
    {
        this.gameManager = gameManager;
        this.railPathData = railPathData;
        this.player = player;
    }

    /// <summary>
    /// �ړ�
    /// </summary>
    public void Move()
    {
        //railPathData����Transform�^�̔z����擾���CVector�R�^�ɒ����ĕϐ��ɑ��
        paths = railPathData.railPathDatas.Select(x => x.transform.position).ToArray();
        isStop = false;
        //Path�f�[�^�����ɐi��ł����C�Ō�̂Ƃ���ő������邩�m�F�B�Ȃ���ΏI���B
        tween = transform.DOPath(paths, moveTime).SetEase(Ease.Linear).OnWaypointChange((index) => gameManager.CheckEvent(index)).OnComplete(() => gameManager.CheckNextRailPathData());
        player.MoveAnimation(true);
        PlayerWalkFootStep();
        Debug.Log("�ړ��J�n");
    }

    /// <summary>
    /// �ړ���~
    /// </summary>
    public void Stop()
    {
        tween.Pause();
        isStop = true;

        player.MoveAnimation(false);
        StopFootStep();
        Debug.Log("��~");
    }

    /// <summary>
    /// �ړ��ĊJ
    /// </summary>
    public void Resume()
    {
        tween.Play();
        isStop = false;
        player.MoveAnimation(true);
        PlayerWalkFootStep();
        Debug.Log("�ړ��ĊJ");
    }

    /// <summary>
    /// tween��j������
    /// </summary>
    public void KillTween()
    {
        StopFootStep();
        tween.Kill();
    }

    /// <summary>
    /// ������炷
    /// </summary>
    public void PlayerWalkFootStep()
    {
        playerFootStep.loop = true;

        playerFootStep.pitch = 1f;

        playerFootStep.clip = walkFootStepSE;

        playerFootStep.Play();

    }

    /// <summary>
    /// �������~�߂�
    /// </summary>
    public void StopFootStep()
    {
        playerFootStep.Stop();

        playerFootStep.loop = false;
    }

    private void StopAndMove()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isStop == false)
        {
            Stop();

        }
        else if (Input.GetKeyDown(KeyCode.Space) && isStop == true)
        {
            Resume();

        }


    }

}
