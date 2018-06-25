using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour {

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float attackRadius = 5f;

    float health;
    EnemyHealthBar healthBar;
    AICharacterControl aiCharacterControl;
    GameObject player;

    void Start()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        health = maxHealthPoints;
        aiCharacterControl = GetComponent<AICharacterControl>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= attackRadius) {
            aiCharacterControl.SetTarget(player.transform);
        } else {
            aiCharacterControl.SetTarget(transform);
        }
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
