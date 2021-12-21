using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public AudioSource weapon;

    public AudioClip reloadSE, triggerSE, fireSE;

    //�A�ː����̂��߂̕ϐ�
    public  bool canShoot;

    public static Weapon instance;

    //�e��
    public Transform shotDirection;

    private GameManager gameManager;

    private PlayerController playerController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //Debug.DrawRay(shotDirection.position, shotDirection.forward * 300, Color.green);
    }

    //gameManager��playerController���擾
    public void SetUpWeapon(GameManager gameManager,PlayerController playerController)
    {
        this.gameManager = gameManager;
        this.playerController = playerController;
    }

    /// <summary>
    /// ���Ă��Ԃɂ���
    /// </summary>
    public void CanShoot()
    {
        canShoot = true;
    }

    /// <summary>
    /// ����SE
    /// </summary>
    public void FireSE()
    {
        weapon.clip = fireSE;
        weapon.Play();
    }

    /// <summary>
    /// �����[�hSE
    /// </summary>
    public void ReloadSE()
    {
        weapon.clip = reloadSE;
        weapon.Play();
    }

    /// <summary>
    /// �g���K�[��SE
    /// </summary>
    public void TriggerSE()
    {
        if (!weapon.isPlaying)
        {
            weapon.clip = triggerSE;
            weapon.Play();
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Shooting()
    {
        //Debug.Log("Shooting");

        //Ray�����������I�u�W�F�N�g�̏�������
        RaycastHit hitInfo;

        //Ray���΂��ăI�u�W�F�N�g�ɓ���������
        if (Physics.Raycast(shotDirection.transform.position, shotDirection.transform.forward, out hitInfo, 300))
        {
            if(hitInfo.collider.gameObject.TryGetComponent(out PartsTypeDetail partsTypeDetail))
            {

                if(partsTypeDetail.partsType == PartsType.head)
                {
                    CapsuleCollider enemyCol = partsTypeDetail.GetComponent<CapsuleCollider>();
                    enemyCol.enabled = false;
                    EnemyController enemy = partsTypeDetail.transform.root.gameObject.GetComponent<EnemyController>();
                    StartCoroutine(enemy.DestroyEnemy(gameManager,enemy.point * 5));
                    Debug.Log("head�ˌ�");

                }
                else if (partsTypeDetail.partsType == PartsType.body)
                {
                    EnemyController enemyController = partsTypeDetail.transform.gameObject.GetComponent<EnemyController>();
                    StartCoroutine(enemyController.DestroyEnemy(gameManager,enemyController.point));
                    Debug.Log("body�ˌ�");

                }
            }
        }
    }

    //Reload�̃A�j���[�V�������I��������m�F
    public void FinishReloading()
    {
        playerController.isReloading = false;
    }

}
