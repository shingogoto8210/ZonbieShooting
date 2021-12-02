using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    
    [SerializeField]
    private int enemyHp;

    [SerializeField]
    private int attackPower;

    private NavMeshAgent agent;

    [SerializeField]
    private PlayerController target;


    private void Update()
    {
        if(target == null)
        {
            return;
        }
        else
        {
            agent.destination = target.transform.position;
        }
    }

    /// <summary>
    /// 敵の設定
    /// </summary>
    public void SetUpEnemy()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// 敵のHP計算
    /// </summary>
    /// <param name="amount"></param>
    public void CalculateEnemyHp(int amount)
    {
        enemyHp -= amount;
        Debug.Log(enemyHp);

        if(enemyHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// プレイヤー発見
    /// </summary>
    /// <param name="player"></param>
    public void DiscoverPlayer(PlayerController player)
    {
        target = player;
    }

    /// <summary>
    /// プレイヤー見失う
    /// </summary>
    public void MissPlayer()
    {
        target = null;
    }

}
