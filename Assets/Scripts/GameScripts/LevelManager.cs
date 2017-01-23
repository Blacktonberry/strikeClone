using UnityEngine;
using System.Collections;
using System;

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance;
    [SerializeField]
    private PoolHandeler poolHandeler;
    [SerializeField]
    private bool radarActive = true;
    
    [SerializeField]
    private PlayerControler playerCon;

    [SerializeField]
    private BaseEnity[] hostileEnitys;
    //private System.Collections.Generic.List<BaseEnity> hostileEnitys;
    [SerializeField]
    private BaseEnity[] staticEnitys;
    //private System.Collections.Generic.List<BaseEnity> staticEnitys;


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

    void Update()
    {

    }

    public bool CanAiGetPlayer(ref Vector3 playerLocation)
    {
        playerLocation = playerCon.transform.position;
        if (radarActive) {  return true; }
        else { return false; }
    }

    public Vector3 AiSeesPlayer()
    {
        return playerCon.transform.position;
    }

    public Vector3 PlayerGetTarget(Transform playerMainGun, float angle,float distance)
    {
        //for (int i = 0; i < hostileEnitys.Count; ++i)
        Vector3 playerPos = playerMainGun.position;
        Vector3 returnValue = Vector3.down;
        float currentMaxDistance = distance;
        float dist = 0;
        for (int i = 0; i < hostileEnitys.Length; ++i)
        {
            if (!hostileEnitys[i].gameObject.activeSelf) { continue; }
            dist = Vector3.Distance(hostileEnitys[i].transform.position, playerPos);
            if (dist <= currentMaxDistance)
            {
                if (Vector3.Angle(hostileEnitys[i].transform.position - playerPos, playerMainGun.forward) < angle)
                {
                    currentMaxDistance = dist;
                    returnValue = hostileEnitys[i].transform.position;
                }

            }
        }
        //no hostile get building
        if (returnValue == Vector3.down)
        {
            for (int i = 0; i < staticEnitys.Length; ++i)
            {
                if (!hostileEnitys[i].gameObject.activeSelf) { continue; }
                dist = Vector3.Distance(staticEnitys[i].transform.position, playerPos);
                if (dist <= currentMaxDistance)
                {
                    if (Vector3.Angle(staticEnitys[i].transform.position - playerPos, playerMainGun.forward) < angle)
                    {
                        currentMaxDistance = dist;
                        returnValue = staticEnitys[i].transform.position;
                    }

                }
            }
        }
        return returnValue;
    }

    #region PoolHandeler

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
    #endregion



    /// <summary>
    /// Destroy All GameObjects created for the level
    /// </summary>
    public void CleanUp()
    {
        
    }
}
