using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] float maxHealthPoints = 100f;
    float health = 100f;
    private EnemyHealthBar healthBar;

    void Start()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        print(healthBar);
    }

    public float healthAsPercentage {
        get {
            return health / maxHealthPoints;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.UpdateHealthBar();
    }

}
