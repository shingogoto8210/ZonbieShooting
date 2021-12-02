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

    private bool isStop;

    private GameManager gameManager;

    /// <summary>
    /// RailMoveController�̐ݒ�
    /// </summary>
    /// <param name="gameManager"></param>
    public void SetUpRailMoveController(GameManager gameManager,RailPathData railPathData)
    {
        this.gameManager = gameManager;
        this.railPathData = railPathData;
    }

    /// <summary>
    /// �ړ�
    /// </summary>
    public void Move()
    {
        //railPathData����Transform�^�̔z����擾���CVector�R�^�ɒ����ĕϐ��ɑ��
        paths = railPathData.railPathDatas.Select(x => x.transform.position).ToArray();

        //Path�f�[�^�����ɐi��ł����C�Ō�̂Ƃ���ő������邩�m�F�B�Ȃ���ΏI���B
        tween = transform.DOPath(paths, moveTime).SetEase(Ease.Linear).OnWaypointChange((index) => gameManager.CheckEvent(index)).OnComplete(() => gameManager.CheckNextRailPathData());
        Debug.Log("�ړ��J�n");
    }

    /// <summary>
    /// �ړ���~
    /// </summary>
    public void Stop()
    {
        tween.Pause();
        isStop = true;
        Debug.Log("��~");
    }

    /// <summary>
    /// �ړ��ĊJ
    /// </summary>
    public void Resume()
    {
        tween.Play();
        isStop = false;
        Debug.Log("�ړ��ĊJ");
    }

    /// <summary>
    /// tween��j������
    /// </summary>
    public void KillTween()
    {
        tween.Kill();
    }

    /// <summary>
    /// �ړ��ƒ�~��ύX����
    /// </summary>
    /// <returns></returns>
    public IEnumerator ChangeMove()
    {
        while (gameManager.currentGameState == GameState.Play)
        {
            //�ړ����ɃX�y�[�X�������Ǝ~�܂�
            if (isStop == false && Input.GetKeyDown(KeyCode.Space))
            {
                Stop();
            }

            //��~���ɃX�y�[�X�������ƈړ��ĊJ
            else if (isStop && Input.GetKeyDown(KeyCode.Space))
            {
                Resume();
            }

            yield return null;
        }
    }
}
