using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HealthManager : MonoBehaviour
{
    public float health;
    MoveCharacter charcontrol;
    [SerializeField] private Dictionary<string, float> weaponDamages = new Dictionary<string, float>
    {
        {"Pitchforkgreen", 10f},
        {"redsword", 13f}
    };
    [SerializeField] string[] friendlyWeapons;
    private float dmg;

    void Start()
    {
        charcontrol = GetComponent<MoveCharacter>();
        foreach(var key in friendlyWeapons)
        {
            weaponDamages.Remove(key);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 )
        {
            charcontrol.Die();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject hitObject = collision.collider.gameObject;

        if (weaponDamages.ContainsKey(hitObject.tag))
        {
            dmg = weaponDamages[hitObject.tag];
            TakeDamage(dmg);
            return;
        }

        if (hitObject.transform.parent != null && weaponDamages.ContainsKey(hitObject.transform.parent.tag))
        {
            dmg = weaponDamages[hitObject.transform.parent.tag];
            TakeDamage(dmg);
        }
        Debug.Log(hitObject.tag);
        Debug.Log(hitObject.name);
    }
}
