using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField,Header("���݂�HP")]
    private int hp;

    [SerializeField,Header("�ő�HP")]
    private int maxHp;

    //HP�̃v���p�e�B
    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            hp = Mathf.Clamp(hp, 0, maxHp);
        }
    }

    [SerializeField,Header("���݂̒e��")]
    private int bulletCount;

    [SerializeField,Header("�ő�e��")]
    private int maxBulletCount;

    //�e�̃v���p�e�B
    public int BulletCount
    {
        set
        {
            bulletCount = value;
            bulletCount = Mathf.Clamp(bulletCount, 0, maxBulletCount);
        }
        get
        {
            return bulletCount;
        }
    }

    [Header("�U���͈�")]
    public float shootRange;

    [Header("�U����")]
    public int bulletPower;

    [Header("�e�̔��ˊԊu")]
    public int shootInterval;

    /// <summary>
    /// PlayerController�̐ݒ�
    /// </summary>
    public void SetUpPlayer()
    {
        if(maxHp == 0)
        {
            maxHp = 10;
        }
        if(maxBulletCount == 0)
        {
            maxBulletCount = 10;
        }
        Hp = maxHp;
        BulletCount = maxBulletCount;
    }

    /// <summary>
    /// �e���̌v�Z
    /// </summary>
    /// <param name="amount"></param>
    public void CalcBulletCount(int amount)
    {
        BulletCount += amount;
    }
}
