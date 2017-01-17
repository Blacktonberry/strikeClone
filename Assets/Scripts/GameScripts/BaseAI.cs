using UnityEngine;
using System.Collections;

public class BaseAI : BaseEnity {
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Transform turretTurnPoint;

    private Vector3 targetPoint;

    [SerializeField]
    private float retargetTime = 4;
    private float retargetCountDown = -1;

    [SerializeField]
    private float sightRange = 100;

    [SerializeField]
    private float gunRange = 50;

    /// <summary>
    /// If target is in sight range skip update and fire
    /// </summary>
    private bool targetAcquired = false;
    // Update is called once per frame
    void Update () {
        retargetCountDown -= Time.deltaTime;
        if (targetAcquired)
        {
            targetPoint = LevelManager.Instance.AiSeesPlayer();
            turretTurnPoint.LookAt(targetPoint);
            AttemptToFire();
        }
        else if (retargetCountDown <0)
        {
            retargetCountDown = retargetTime;//resets to account for player compleating objective
            if (LevelManager.Instance.CanAiGetPlayer(ref targetPoint)) {
                if (Vector3.Distance(transform.position, targetPoint) < sightRange)
                {
                    targetAcquired = true;
                    AttemptToFire();
                }
            }
            else
            {

            }

        }
	}



    [SerializeField]
    private float gunCoolDownMax = 0.3f;
    private float gunCoolDown = 0;

    private void AttemptToFire()
    {
        if (gunCoolDown <= 0){
            if (Vector3.Distance(transform.position, targetPoint) < gunRange) {
                LevelManager.Instance.FireBullet(EnitySide.Enemey, firePoint.position, firePoint.rotation);
                gunCoolDown = gunCoolDownMax;
            }
        }else{
            gunCoolDown -= Time.deltaTime;
        }



    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawLine(turretTurnPoint.position, targetPoint);
        }
    }
}
