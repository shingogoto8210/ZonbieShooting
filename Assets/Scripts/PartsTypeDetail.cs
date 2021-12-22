using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsTypeDetail : MonoBehaviour
{
    public PartsType partsType;

    public void HitCheckParts(GameManager gameManager)
    {
        
        EnemyController enemy = this.transform.root.gameObject.GetComponent<EnemyController>();
        int damage = enemy.point;
        if (this.partsType == PartsType.head)
        {
            CapsuleCollider enemyCol = this.GetComponent<CapsuleCollider>();
            enemyCol.enabled = false;
            damage = enemy.point * 5;
        }
        //else if (this.partsType == PartsType.body)
        //{
        //    EnemyController enemyController = this.transform.gameObject.GetComponent<EnemyController>();
        //    StartCoroutine(enemyController.DestroyEnemy(gameManager, enemyController.point));
        //    Debug.Log("bodyŽËŒ‚");
        //}
        StartCoroutine(enemy.DestroyEnemy(gameManager, damage));
        Debug.Log(partsType.ToString() + "ŽËŒ‚");
    }
}
