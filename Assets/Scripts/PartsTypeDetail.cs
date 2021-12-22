using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsTypeDetail : MonoBehaviour
{
    public PartsType partsType;

    public void HitCheckParts(GameManager gameManager)
    {
        if (this.partsType == PartsType.head)
        {
            CapsuleCollider enemyCol = this.GetComponent<CapsuleCollider>();
            enemyCol.enabled = false;
            EnemyController enemy = this.transform.root.gameObject.GetComponent<EnemyController>();
            StartCoroutine(enemy.DestroyEnemy(gameManager, enemy.point * 5));
            Debug.Log("headŽËŒ‚");

        }
        else if (this.partsType == PartsType.body)
        {
            EnemyController enemyController = this.transform.gameObject.GetComponent<EnemyController>();
            StartCoroutine(enemyController.DestroyEnemy(gameManager, enemyController.point));
            Debug.Log("bodyŽËŒ‚");
        }
    }
}
