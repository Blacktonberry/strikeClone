using UnityEngine;
using System.Collections;
using System;

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance;
    [SerializeField]
    private PoolHandeler poolHandeler;

    public PlayerControler PlayerCon;

    void Awake()
    {
        if (Instance != null)
        {
            Instance.CleanUp();
            DestroyImmediate(Instance.gameObject);
        }

        Instance = this;
        poolHandeler.GenetatePool();
    }

    public bool FireBullet(EnitySide newSide, Vector3 initalPosition, Quaternion initalDirection)
    {
        return poolHandeler.FireBullet(newSide, initalPosition, initalDirection);
    }

    public bool FireRocket(EnitySide newSide, Vector3 initalPosition, Quaternion initalDirection)
    {
        return poolHandeler.FireRocket(newSide, initalPosition, initalDirection);
    }

    public bool FireMissle(EnitySide newSide, Vector3 initalPosition, Quaternion initalDirection)
    {
        return poolHandeler.FireMissle(newSide, initalPosition, initalDirection);
    }

    public bool SpawnExplosion(Transform location)
    {
        return poolHandeler.SpaweExplosion(location);
    }


    /// <summary>
    /// Destroy All GameObjects created for the level
    /// </summary>
    public void CleanUp()
    {
        
    }
}
