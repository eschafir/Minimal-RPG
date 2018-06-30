using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float projectileSpeed;
    float projectileDamage;

    public void SetProjectileDamage(float dmg)
    {
        projectileDamage = dmg;
    }

    void OnTriggerEnter(Collider other)
    {
        Component damageableObject = other.gameObject.GetComponent(typeof(IDamageable));

        // Checks if damageableObject hitted implements IDamageable interface
        if (damageableObject) {
            (damageableObject as IDamageable).TakeDamage(projectileDamage);
        }
    }

}
