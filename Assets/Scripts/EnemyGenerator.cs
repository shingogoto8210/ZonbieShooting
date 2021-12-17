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

    //�C�x���g�Ŕ�������G�̐�
    public int maxEnemyCount;

    private int enemyCount;

    //���ׂĂ̓G���o�����I������true
    public bool isFinish;

    /// <summary>
    /// �C�x���g�n�_�ɂ����甭��
    /// 10�̓|���܂œG�����������
    /// </summary>
    public IEnumerator GenerateEnemy()
    {
        while (enemyCount < maxEnemyCount)
        {
            isFinish = false;

            //  �����_���œG�̔����n�_�����߂�
            int randomTran = Random.Range(0, enemyTrans.Length);
            EnemyController enemy = Instantiate(enemyPrefab, enemyTrans[randomTran]);
            enemyCount++;

            // gameManager�̓G���X�g�ɒǉ�
            gameManager.enemiesList.Add(enemy);

            yield return new WaitForSeconds(2.0f);

            //�o�������G�̐���maxEnemyCount���z������G�̐����I��
            if (enemyCount >= maxEnemyCount) isFinish = true;

        }
        yield return null;
    }
}
