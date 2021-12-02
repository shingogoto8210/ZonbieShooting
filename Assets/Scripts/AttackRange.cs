using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemy;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("�N��");
        if(other.gameObject.TryGetComponent(out PlayerController player))
        {
            enemy.DiscoverPlayer(player);
            Debug.Log("�v���C���[����");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerController player))
        {
            enemy.MissPlayer();
            Debug.Log("�v���C���[������");
        }
    }
}
