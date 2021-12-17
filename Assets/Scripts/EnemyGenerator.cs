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
    /// 10体倒すまで敵が生成される
    /// </summary>
    public IEnumerator GenerateEnemy()
    {
        while (enemyCount < maxEnemyCount)
        {
            isFinish = false;

            //  ランダムで敵の発生地点を決める
            int randomTran = Random.Range(0, enemyTrans.Length);
            EnemyController enemy = Instantiate(enemyPrefab, enemyTrans[randomTran]);
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
