using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyPrefab;

    public Transform[] enemyTrans;

    /// <summary>
    /// ìGÇÃê∂ê¨
    /// </summary>
    public void GenerateEnemy()
    {
        for (int i = 0; i < enemyTrans.Length; i++)
        {
             Instantiate(enemyPrefab, enemyTrans[i]).SetUpEnemy();
        }
    }
}
