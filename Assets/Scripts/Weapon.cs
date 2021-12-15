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

    public void SetUpWeapon(GameManager gameManager)
    {
        this.gameManager = gameManager;
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
        RaycastHit hitInfo;

        if (Physics.Raycast(shotDirection.transform.position, shotDirection.transform.forward, out hitInfo, 300))
        {
            if (hitInfo.collider.gameObject.GetComponent<EnemyController>())
            {
                EnemyController enemy = hitInfo.collider.gameObject.GetComponent<EnemyController>();
                enemy.DestroyEnemy(gameManager);
            }
        }
    }
}
