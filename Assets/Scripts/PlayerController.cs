using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField,Header("Œ»İ‚ÌHP")]
    private int hp;

    [SerializeField,Header("Å‘åHP")]
    private int maxHp;

    //HP‚ÌƒvƒƒpƒeƒB
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

    [SerializeField,Header("Œ»İ‚Ì’e”")]
    private int bulletCount;

    [SerializeField,Header("Å‘å’e”")]
    private int maxBulletCount;

    //’e‚ÌƒvƒƒpƒeƒB
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

    [Header("UŒ‚”ÍˆÍ")]
    public float shootRange;

    [Header("UŒ‚—Í")]
    public int bulletPower;

    [Header("’e‚Ì”­ËŠÔŠu")]
    public int shootInterval;

    /// <summary>
    /// PlayerController‚Ìİ’è
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
    /// ’e”‚ÌŒvZ
    /// </summary>
    /// <param name="amount"></param>
    public void CalcBulletCount(int amount)
    {
        BulletCount += amount;
    }
}
