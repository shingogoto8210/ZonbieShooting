using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RailMoveController : MonoBehaviour
{
    private Tween tween;

    //�ړI�n��z��ŊǗ����Ă���N���X
    private RailPathData railPathData;

    //RailPathData�̏���Vector3�^�ɂ��ĊǗ����邽�߂̕ϐ�
    private Vector3[] paths;

    [SerializeField, Header("�ړ�����")]
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
        tween = transform.DOPath(paths, moveTime).SetEase(Ease.Linear).OnWaypointChange((index) => gameManager.CheckEvent(index)).OnComplete(() => gameManager.GameOver());
        
        //player.MoveAnimation(true);

        player.PlayerWalkFootStep();
        Debug.Log("�ړ��J�n");
    }

    /// <summary>
    /// �ړ���~
    /// </summary>
    public void Stop()
    {
        tween.Pause();
        isStop = true;
        //player.MoveAnimation(false);
        player.StopFootStep();
        Debug.Log("��~");
    }

    /// <summary>
    /// �ړ��ĊJ
    /// </summary>
    public void Resume()
    {
        tween.Play();
        isStop = false;
        //player.MoveAnimation(true);
        player.PlayerWalkFootStep();
        Debug.Log("�ړ��ĊJ");
    }

    /// <summary>
    /// tween��j������
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
