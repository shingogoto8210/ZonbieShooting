using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public AudioSource weapon;

    public  bool canShoot;

    public AudioClip reloadSE, triggerSE, fireSE;

    public static Weapon instance;

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
        Debug.DrawRay(shotDirection.position, shotDirection.forward * 300, Color.green);
    }

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
        Debug.Log("Shooting");
        RaycastHit hitInfo;

        if (Physics.Raycast(shotDirection.transform.position, shotDirection.transform.forward, out hitInfo, 300))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out EnemyController enemy))
            {
                enemy.DestroyEnemy(gameManager);
                Debug.Log("�ˌ�");
            }
            else if(hitInfo.collider.gameObject != null)
            {
                Debug.Log(hitInfo.collider.gameObject.name);
            }
            else
            {
                Debug.Log("�擾�Ȃ�");
            }
        }
    }

}
