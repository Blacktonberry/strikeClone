using UnityEngine;
using System.Collections;

public class PlayerControler : BaseEnity {
    [SerializeField]
    private Rigidbody rd;
    [SerializeField]
    private float turnSpeed = 2;
    [SerializeField]
    private float speed= 1;

    [SerializeField]
    private float hoverDistance = 10;


    [SerializeField]
    private Transform mechineGunMount;
    [SerializeField]
    private float mechineGunCoolDownMax = 0.3f;
    private float mechineGunCoolDown = 0;
    private int mechineGunAmmo = 1500;
    public int CurrentGumAmmo { get { return mechineGunAmmo; }set { mechineGunAmmo = value; } }

    [SerializeField]
    private Transform rocketMount1;
    [SerializeField]
    private Transform rocketMount2;
    [SerializeField]
    private float rocketCoolDownMax = 0.6f;
    private float rocketCoolDown = 0;
    private int rocketAmmo = 320;
    private bool rocketSide = false;
    public int CurrentRocketAmmo { get { return rocketAmmo; } set { rocketAmmo = value; } }
    

    [SerializeField]
    private Transform missleMount1;
    [SerializeField]
    private Transform missleMount2;
    [SerializeField]
    private float missleCoolDownMax = 1f;
    private float missleCoolDown = 0;
    private int missleAmmo = 80;
    private bool missleSide = false;
    public int CurrentMissleAmmo { get { return missleAmmo; } set { missleAmmo = value; } }



    
    public override void Destroy()
    {
        base.Destroy();
        Debug.Log("<color=blue><b>PLAYER HAS DIED</b></color>");
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


        if (mechineGunCoolDown <= 0){
            if (Input.GetAxis("Fire1") != 0){
                if (mechineGunAmmo > 0){
                    LevelManager.Instance.FireBullet(EnitySide.player, mechineGunMount.position, GetTargetRotation(mechineGunMount));
                    mechineGunCoolDown = mechineGunCoolDownMax;
                    mechineGunAmmo--;
                }else{
                    Debug.Log("out of Ammo");
                }
            }
        }
        else
        {
            mechineGunCoolDown -= Time.deltaTime;
        }

        if (rocketCoolDown <= 0)
        {
            if (Input.GetAxis("Fire2") != 0)
            {
                if (rocketAmmo > 0)
                {
                    if (rocketSide)
                    {
                        LevelManager.Instance.FireRocket(EnitySide.player, rocketMount1.position, GetTargetRotation(rocketMount1));
                    }
                    else
                    {
                        LevelManager.Instance.FireRocket(EnitySide.player, rocketMount2.position,GetTargetRotation(rocketMount2));
                    }
                    rocketSide = !rocketSide;
                    rocketCoolDown = rocketCoolDownMax;
                    rocketAmmo--;
                }
                else {
                    Debug.Log("out of Ammo");
                }
            }
        }
        else
        {
            rocketCoolDown -= Time.deltaTime;
        }

        if (missleCoolDown <= 0)
        {
            if (Input.GetAxis("Fire3") != 0)
            {
                if (missleAmmo > 0)
                {
                    if (missleSide)
                    {
                        LevelManager.Instance.FireMissle(EnitySide.player, missleMount1.position, GetTargetRotation(missleMount1));
                    }
                    else
                    {
                        LevelManager.Instance.FireMissle(EnitySide.player, missleMount2.position, GetTargetRotation(missleMount2));
                    }
                    missleSide = !missleSide;
                    missleCoolDown = missleCoolDownMax;

                    missleAmmo--;
                }
                else {
                    Debug.Log("out of Ammo");
                }
            }
        }
        else
        {
            missleCoolDown -= Time.deltaTime;
        }
    }

    private Quaternion GetTargetRotation(Transform launchPos)
    {
        if (target == Vector3.down) {
           return launchPos.rotation;
        }
        else
        {
            return Quaternion.LookRotation(target-launchPos.position, launchPos.up);
        }
    }

    //Update Physics
    void FixedUpdate() {
        ///these controls are simple to get to project moving plus get the more arcadey movment i wanted
        //Obtain Input For moving fowrad negtive becasue of the modle if your flying backwords fix this bit!
        float ForwardForceFactor = -Input.GetAxis("Vertical");
   
        RaycastHit hit = new RaycastHit();
        Ray downRay = new Ray(transform.position, -Vector3.up);
        Vector3 newVel = transform.forward* ForwardForceFactor;
        if (Physics.Raycast(downRay, out hit, 100, 1 << 8))
        {
            float HightDistance = hoverDistance- hit.distance;
            newVel.y = HightDistance/speed;
        }
        rd.velocity = newVel * speed;
               
        float TurnForceFactor = Input.GetAxis("Horizontal");
        rd.angularVelocity = new Vector3(0, TurnForceFactor * turnSpeed, 0);

        ///TODO: tilt foward and backward when 
    }

    void LateUpdate()
    {
        target = LevelManager.Instance.PlayerGetTarget(mechineGunMount, angle, targetDistance);

    }
    [SerializeField]
    private float angle = 20;
    [SerializeField]
    private float targetDistance = 100;
    /// <summary>
    /// the Current target for auto aim set to Vector3.down if there is none
    /// </summary>
    [SerializeField]
    private Vector3 target= Vector3.down;
    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(mechineGunMount.position, mechineGunMount.forward*10);
            Gizmos.DrawRay(mechineGunMount.position, (Quaternion.AngleAxis(angle, mechineGunMount.up) * mechineGunMount.forward) * targetDistance);
            Gizmos.DrawRay(mechineGunMount.position, (Quaternion.AngleAxis(-angle, mechineGunMount.up) * mechineGunMount.forward) * targetDistance);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(mechineGunMount.position, (Quaternion.AngleAxis(angle, mechineGunMount.right) * mechineGunMount.forward) * targetDistance);
            Gizmos.DrawRay(mechineGunMount.position, (Quaternion.AngleAxis(-angle, mechineGunMount.right) * mechineGunMount.forward) * targetDistance);
            if (target != Vector3.down)
            {
                Gizmos.DrawLine(target, mechineGunMount.position);
            }
        }
    }

}
