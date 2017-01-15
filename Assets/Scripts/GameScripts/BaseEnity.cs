using UnityEngine;
using System.Collections;
using System;

public enum EnitySide
{
    player = 0,
    Enemey = 1,
    Buulding = 2,
    Neutral = 3
}


public class BaseEnity : MonoBehaviour {
    /// <summary>
    /// Holder THe max Hp usesfull for prfabs and maby a floating helth bar
    /// </summary>
    [SerializeField]
    private float maxHP = 10;
    /// <summary>
    /// The Enitys CurrentHP
    /// </summary>
    private float enityHp = 10;

    public float CurrentHP{get { return enityHp; }}

    /// <summary>
    /// Used for frendly fire
    /// </summary>
    [SerializeField]
    private EnitySide side = EnitySide.Neutral;


    public EnitySide Faction { get { return side; } }





	// Use this for initialization
	void Awake() {
        enityHp = maxHP;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void Damage(float bulletDamage)
    {
        enityHp -= bulletDamage;
        if (enityHp <= 0)
        {
            Destroy();
        }
    }

    public virtual void Destroy()
    {
        gameObject.SetActive(false);
    }

}
