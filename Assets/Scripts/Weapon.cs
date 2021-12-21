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
            //�I�u�W�F�N�g��EnemyController�������Ă�����
            if (hitInfo.collider.gameObject.TryGetComponent(out EnemyController enemy))
            {
                StartCoroutine(enemy.DestroyEnemy(gameManager));
                //Debug.Log("�ˌ�");
            }
            //else if(hitInfo.collider.gameObject != null)
            //{
            //    Debug.Log(hitInfo.collider.gameObject.name);
            //}
            //else
            //{
            //    Debug.Log("�擾�Ȃ�");
            //}
        }
    }

    //Reload�̃A�j���[�V�������I��������m�F
    public void FinishReloading()
    {
        playerController.isReloading = false;
    }

}
