using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int enemyHp;

    public void AttackEnemy(int amount)
    {
        enemyHp -= amount;
        Debug.Log(enemyHp);

        if(enemyHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
