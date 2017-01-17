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
                    LevelManager.Instance.FireBullet(EnitySide.player, mechineGunMount.position, mechineGunMount.rotation);
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
                        LevelManager.Instance.FireRocket(EnitySide.player, rocketMount1.position, rocketMount1.rotation);
                    }
                    else
                    {
                        LevelManager.Instance.FireRocket(EnitySide.player, rocketMount2.position, rocketMount2.rotation);
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
                        LevelManager.Instance.FireMissle(EnitySide.player, missleMount1.position, missleMount1.rotation);
                    }
                    else
                    {
                        LevelManager.Instance.FireMissle(EnitySide.player, missleMount2.position, missleMount2.rotation);
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


}
