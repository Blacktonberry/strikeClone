using UnityEngine;
using System.Collections;
/// <summary>
/// Holds on to the pools for bullets, missles and eventualy explosens
/// </summary>
public class PoolHandeler : MonoBehaviour {
    [SerializeField]
    private int bulletPoolSize = 10;
    [SerializeField]
    private BaseBullet BulletPrefab;
    /// <summary>
    /// List containg all normal bullets 
    /// </summary>
    private System.Collections.Generic.List<BaseBullet> bulletPool;



    [SerializeField]
    private int rocketPoolSize = 4;
    [SerializeField]
    private BaseBullet rocketPrefab;
    /// <summary>
    /// List containg all normal bullets 
    /// </summary>
    private System.Collections.Generic.List<BaseBullet> rocketPool;


    [SerializeField]
    private int misslePoolSize = 4;
    [SerializeField]
    private BaseBullet misslePrefab;
    /// <summary>
    /// List containg all normal bullets 
    /// </summary>
    private System.Collections.Generic.List<BaseBullet> misslePool;

    [SerializeField]
    private int explosionPoolSize = 15;
    [SerializeField]
    private ParticlePool explosionPrefab;
    /// <summary>
    /// List containg all normal bullets 
    /// </summary>
    private System.Collections.Generic.List<ParticlePool> explosionPool;





    public void GenetatePool()
    {
        bulletPool = new System.Collections.Generic.List<BaseBullet>();
        BaseBullet BullHolder;
        for (int i = 0; i < bulletPoolSize; ++i)
        {
            BullHolder = Instantiate(BulletPrefab) as BaseBullet;
            BullHolder.Init();
            bulletPool.Add(BullHolder);
        }
        rocketPool = new System.Collections.Generic.List<BaseBullet>();
        for (int i = 0; i < rocketPoolSize; ++i)
        {
            BullHolder = Instantiate(rocketPrefab) as BaseBullet;
            BullHolder.Init();
            rocketPool.Add(BullHolder);
        }
        misslePool = new System.Collections.Generic.List<BaseBullet>();
        for (int i = 0; i < misslePoolSize; ++i)
        {
            BullHolder = Instantiate(misslePrefab) as BaseBullet;
            BullHolder.Init();
            misslePool.Add(BullHolder);
        }
        explosionPool = new System.Collections.Generic.List<ParticlePool>();
        ParticlePool SysHolder;
        for (int i = 0; i < explosionPoolSize; ++i)
        {
            SysHolder = Instantiate(explosionPrefab) as ParticlePool;
            SysHolder.init();
            explosionPool.Add(SysHolder);
        }
    }

    public bool FireBullet(EnitySide newSide, Vector3 initalPosition, Quaternion initalDirection)
    {
        for (int i = 0; i < bulletPoolSize; ++i)
        {
            if (!bulletPool[i].isAlive)
            {
                bulletPool[i].SpawnBullet(newSide, initalPosition, initalDirection);
                return true;
            }
        }
        Debug.Log("Firing More bullets than we Have");
        return false;
    }

    public bool FireRocket(EnitySide newSide, Vector3 initalPosition, Quaternion initalDirection)
    {
        for (int i = 0; i < rocketPoolSize; ++i)
        {
            if (!rocketPool[i].isAlive)
            {
                rocketPool[i].SpawnBullet(newSide, initalPosition, initalDirection);
                return true;
            }
        }
        Debug.Log("Firing More rockets than we Have");
        return false;
    }

    public bool FireMissle(EnitySide newSide, Vector3 initalPosition, Quaternion initalDirection)
    {
        for (int i = 0; i < misslePoolSize; ++i)
        {
            if (!misslePool[i].isAlive)
            {
                misslePool[i].SpawnBullet(newSide, initalPosition, initalDirection);
                return true;
            }
        }
        Debug.Log("Firing More Missles than we Have");
        return false;
    }

    public bool SpaweExplosion(Transform location)
    {
        for (int i = 0; i < explosionPoolSize; ++i)
        {
            if (!explosionPool[i].isPlaying)
            {
                explosionPool[i].Spawn(location);
                return true;
            }
        }
        Debug.Log("More Explosions then we have");
        return false;
    }

}
