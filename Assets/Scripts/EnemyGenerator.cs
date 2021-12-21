using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public int generatorNo;

    [SerializeField]
    private EnemyController enemyPrefab;

    public Transform[] enemyTrans;

    [SerializeField]
    private GameManager gameManager;

    //イベントで発生する敵の数
    public int maxEnemyCount;

    private int enemyCount;

    //すべての敵を出現し終えたらtrue
    public bool isFinish;

    /// <summary>
    /// イベント地点についたら発動
    /// </summary>
    public IEnumerator GenerateEnemy()
    {
        while (enemyCount < maxEnemyCount)
        {
            isFinish = false;

            // ランダムで敵の発生地点を決める
            int randomTran = Random.Range(0, enemyTrans.Length);
            int randomX = Random.Range(-3, 3);
            int randomZ = Random.Range(-3, 3);

            Quaternion rot = Quaternion.Euler(0, 180, 0);
            EnemyController enemy = Instantiate(enemyPrefab, new Vector3(enemyTrans[randomTran].position.x + randomX, enemyTrans[randomTran].position.y, enemyTrans[randomTran].position.z + randomZ),rot);
            enemyCount++;

            // gameManagerの敵リストに追加
            gameManager.enemiesList.Add(enemy);

            yield return new WaitForSeconds(2.0f);

            //出現した敵の数がmaxEnemyCountを越えたら敵の生成終了
            if (enemyCount >= maxEnemyCount) isFinish = true;

        }
        yield return null;
    }
}
