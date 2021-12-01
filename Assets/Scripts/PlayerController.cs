using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField,Header("ťÝĚHP")]
    private int hp;

    [SerializeField,Header("ĹĺHP")]
    private int maxHp;

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

    [SerializeField,Header("ťÝĚe")]
    private int bulletCount;

    [SerializeField,Header("Ĺĺe")]
    private int maxBulletCount;

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

    [Header("UÍÍ")]
    public float shootRange;

    public int bulletPower;

    public int shootInterval;

   

    // Start is called before the first frame update
    void Start()
    {
        SetUpPlayer();
    }

    private void SetUpPlayer()
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


    public void CalcBulletCount(int amount)
    {
        BulletCount += amount;
    }
}
