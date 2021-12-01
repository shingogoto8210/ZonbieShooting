using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RailMoveController : MonoBehaviour
{
    public Tween tween;

    [SerializeField]
    private RailPathData railPathData;

    private Vector3[] paths;

    [SerializeField, Header("�ړ�����")]
    private float moveTime;

    private bool isStop;

    private GameManager gameManager;

    /// <summary>
    /// RailMoveController�̐ݒ�
    /// </summary>
    /// <param name="gameManager"></param>
    public void SetUpRailMoveController(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    /// <summary>
    /// �ړ�
    /// </summary>
    public void Move()
    {
        //railPathData����Transform�^�̔z����擾���CVector�R�^�ɒ����ĕϐ��ɑ��
        paths = railPathData.railPathDatas.Select(x => x.position).ToArray();

        //Path�f�[�^�����ɐi��ł����C�Ō�̂Ƃ���ő������邩�m�F�B�Ȃ���ΏI���B
        tween = transform.DOPath(paths, moveTime).SetEase(Ease.Linear).OnComplete(() => gameManager.CheckNextRailPathData());
        Debug.Log("�ړ��J�n");
    }

    /// <summary>
    /// ��~
    /// </summary>
    public void Stop()
    {
        tween.Pause();
        Debug.Log("��~");
    }

    /// <summary>
    /// �ړ��ĊJ
    /// </summary>
    public void Resume()
    {
        tween.Play();
        Debug.Log("�ړ��ĊJ");
    }
    
    //�ړ��ƒ�~��ύX����
    public IEnumerator ChangeMove()
    {
        while (gameManager.currentGameState == GameState.Play)
        {
            //�ړ����ɃX�y�[�X�������Ǝ~�܂�
            if (isStop == false && Input.GetKeyDown(KeyCode.Space))
            {
                Stop();
                isStop = true;
            }
            //��~���ɃX�y�[�X�������ƈړ��ĊJ
            else if (isStop && Input.GetKeyDown(KeyCode.Space))
            {
                Resume();
                isStop = false;
            }
            yield return null;
        }
    }
}
