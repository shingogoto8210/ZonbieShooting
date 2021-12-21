using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public AudioSource weapon;

    public AudioClip reloadSE, triggerSE, fireSE;

    //連射制限のための変数
    public  bool canShoot;

    public static Weapon instance;

    //銃口
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

    //gameManagerとplayerControllerを取得
    public void SetUpWeapon(GameManager gameManager,PlayerController playerController)
    {
        this.gameManager = gameManager;
        this.playerController = playerController;
    }

    /// <summary>
    /// 撃てる状態にする
    /// </summary>
    public void CanShoot()
    {
        canShoot = true;
    }

    /// <summary>
    /// 発射SE
    /// </summary>
    public void FireSE()
    {
        weapon.clip = fireSE;
        weapon.Play();
    }

    /// <summary>
    /// リロードSE
    /// </summary>
    public void ReloadSE()
    {
        weapon.clip = reloadSE;
        weapon.Play();
    }

    /// <summary>
    /// トリガー音SE
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
    /// 撃つ
    /// </summary>
    public void Shooting()
    {
        //Debug.Log("Shooting");

        //Rayが当たったオブジェクトの情報を入れる
        RaycastHit hitInfo;

        //Rayを飛ばしてオブジェクトに当たったら
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
                    Debug.Log("head射撃");

                }
                else if (partsTypeDetail.partsType == PartsType.body)
                {
                    EnemyController enemyController = partsTypeDetail.transform.gameObject.GetComponent<EnemyController>();
                    StartCoroutine(enemyController.DestroyEnemy(gameManager,enemyController.point));
                    Debug.Log("body射撃");

                }
            }
        }
    }

    //Reloadのアニメーションが終わったか確認
    public void FinishReloading()
    {
        playerController.isReloading = false;
    }

}
