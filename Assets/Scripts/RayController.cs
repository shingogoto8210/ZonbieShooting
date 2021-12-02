using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
    private bool isShooting;

    private GameObject target;

    [SerializeField]
    private int[] layerMasks;

    //Debug用
    [SerializeField]
    private string[] layerMasksStr;

    private PlayerController playerController;

    public void SetUpRayController(PlayerController playerController)
    {
        this.playerController = playerController;

        //Layerの情報を文字列に変換し、Raycastメソッドで利用しやすい情報を変数として作成しておく
        layerMasksStr = new string[layerMasks.Length];
        for (int i = 0; i < layerMasks.Length; i++)
        {
            layerMasksStr[i] = LayerMask.LayerToName(layerMasks[i]);
        }
    }

    /// <summary>
    /// 発射間隔
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShootTimer()
    {
        if (!isShooting)
        {
            isShooting = true;

            Shoot();

            yield return new WaitForSeconds(playerController.shootInterval);

            isShooting = false;
        }
    }

    /// <summary>
    /// 発射
    /// </summary>
    public void Shoot()
    {
        //カメラの位置からクリックした地点にRayを投射
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction, Color.red, 3.0f);
        
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, playerController.shootRange, LayerMask.GetMask(layerMasksStr)))
        {

            target = hit.collider.gameObject;

            Debug.Log(target.name);

            if(target.TryGetComponent(out EnemyController enemyController))
            {
                enemyController.CalculateEnemyHp(playerController.bulletPower);
            }

        }
        playerController.CalcBulletCount(-1);
    }
}
