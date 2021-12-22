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
        //Ray���΂��ăI�u�W�F�N�g�ɓ���������
        if (Physics.Raycast(shotDirection.transform.position, shotDirection.transform.forward, out RaycastHit hitInfo, 300))
        {
            if(hitInfo.collider.gameObject.TryGetComponent(out PartsTypeDetail partsTypeDetail))
            {
                partsTypeDetail.HitCheckParts(gameManager);
            }
        }
    }

    //Reload�̃A�j���[�V�������I��������m�F
    public void FinishReloading()
    {
        playerController.isReloading = false;
    }

}
