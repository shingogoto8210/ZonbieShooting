using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
    private bool isShooting;

    private GameObject target;

    [SerializeField]
    private int[] layerMasks;

    //Debug�p
    [SerializeField]
    private string[] layerMasksStr;

    private PlayerController playerController;

    public void SetUpRayController(PlayerController playerController)
    {
        this.playerController = playerController;

        //Layer�̏��𕶎���ɕϊ����ARaycast���\�b�h�ŗ��p���₷������ϐ��Ƃ��č쐬���Ă���
        layerMasksStr = new string[layerMasks.Length];
        for (int i = 0; i < layerMasks.Length; i++)
        {
            layerMasksStr[i] = LayerMask.LayerToName(layerMasks[i]);
        }
    }

    /// <summary>
    /// ���ˊԊu
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
    /// ����
    /// </summary>
    public void Shoot()
    {
        //�J�����̈ʒu����N���b�N�����n�_��Ray�𓊎�
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
