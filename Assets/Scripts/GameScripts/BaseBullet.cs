using UnityEngine;
using System.Collections;

public class BaseBullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rd;
    [SerializeField]
    private float bulletDamage = 10;
    [SerializeField]
    private float bulletSpeed = 30;

    private EnitySide BulletSide = EnitySide.Neutral;
    private bool alive = false;
    public bool isAlive{get { return alive; }}

    void OnCollisionEnter(Collision collision)
    {
        
        ///if colided with ground or water
        if (collision.collider.gameObject.layer == 8)
        {
            KillBullet();
            return;
        }
        else if (collision.collider.gameObject.layer == 9)
        {
            BaseEnity target = collision.collider.GetComponent<BaseEnity>();
            if (target != null)
            {
                if (target.Faction != BulletSide)
                {
                    target.Damage(bulletDamage);
                }
                KillBullet();
            }
            else
            {
                target = collision.collider.transform.parent.GetComponent<BaseEnity>();
                if (target != null)
                {
                    if (target.Faction != BulletSide)
                    {
                        target.Damage(bulletDamage);
                    }
                    KillBullet();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        ///if colided with ground or water
        if (other.gameObject.layer == 8)
        {
            KillBullet();
            return;
        }
        else if (other.gameObject.layer == 9)
        {
            BaseEnity target = other.GetComponent<BaseEnity>();
            if (target!= null)
            {
                if(target.Faction!= BulletSide)
                {
                    target.Damage(bulletDamage);
                }
                KillBullet();
            }
        }
    }

    public virtual void Init()
    {
        Reset();
    }

    public virtual void SpawnBullet(EnitySide newSide,Vector3 initalPosition, Quaternion initalDirection)
    {
        gameObject.SetActive(true);
        BulletSide = newSide;
        transform.position= initalPosition;
        transform.rotation = initalDirection;
        rd.WakeUp();
        rd.velocity = bulletSpeed * transform.forward;
        alive = true;
    }
    /// <summary>
    /// Kill bullet and return it to the pool
    /// </summary>
    public virtual void KillBullet()
    {
        ///spawn death animation
        LevelManager.Instance.SpawnExplosion(transform);
        Reset();
    }

    protected virtual void Reset()
    {
        gameObject.SetActive(false);
        rd.Sleep();
        transform.position = new Vector3();
        transform.rotation = new Quaternion();
        alive = false;
    }

    void Update()
    {
        if (transform.position.y < -100)
        {
            Reset();
        }
        else if (transform.position.y > 500)
        {
            Reset();
        }
    }

}
