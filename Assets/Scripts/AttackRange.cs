using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemy;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("侵入");
        if(other.gameObject.TryGetComponent(out PlayerController player))
        {
            enemy.DiscoverPlayer(player);
            Debug.Log("プレイヤー発見");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerController player))
        {
            enemy.MissPlayer();
            Debug.Log("プレイヤー見失う");
        }
    }
}
