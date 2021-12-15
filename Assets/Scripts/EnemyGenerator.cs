using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyPrefab;

    public Transform[] enemyTrans;

    [SerializeField]
    private GameManager gameManager;

    /// <summary>
    /// �G�𐶐����ăG�l�~�[���X�g�ɉ�����
    /// </summary>
    public void GenerateEnemy()
    {
        for (int i = 0; i < enemyTrans.Length; i++)
        {
            EnemyController enemy = Instantiate(enemyPrefab, enemyTrans[i]);
            //enemy.SetUpEnemy();
            gameManager.enemiesList.Add(enemy);
        }
    }
}
