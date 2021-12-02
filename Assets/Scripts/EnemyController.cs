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
    /// �G�̐ݒ�
    /// </summary>
    public void SetUpEnemy()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// �G��HP�v�Z
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
    /// �v���C���[����
    /// </summary>
    /// <param name="player"></param>
    public void DiscoverPlayer(PlayerController player)
    {
        target = player;
    }

    /// <summary>
    /// �v���C���[������
    /// </summary>
    public void MissPlayer()
    {
        target = null;
    }

}
