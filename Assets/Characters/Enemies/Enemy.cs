using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable {

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float attackRadius = 5f;
    [SerializeField] float chaseRadius = 10f;
    [SerializeField] float damagePerShot = 9f;
    [SerializeField] float secondsBetweenShots = 0.5f;
    [SerializeField] GameObject projectileToUse;
    [SerializeField] GameObject projectileSocket;

    bool isAttacking = false;
    float currentHealth;
    EnemyHealthBar healthBar;
    AICharacterControl aiCharacterControl;
    GameObject player;


    void Start()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        currentHealth = maxHealthPoints;
        aiCharacterControl = GetComponent<AICharacterControl>();
        player = GameObject.FindGameObjectWithTag("Player");

    }
    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0f, maxHealthPoints);
        healthBar.UpdateHealthBar();
    }

    public float healthAsPercentage {
        get {
            return currentHealth / maxHealthPoints;
        }
    }



    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= attackRadius && !isAttacking) {
            isAttacking = true;
            InvokeRepeating("SpawnProjectile", 0f, secondsBetweenShots);
        }

        if (distanceToPlayer > attackRadius) {
            isAttacking = false;
            CancelInvoke();
        }

        if (distanceToPlayer <= chaseRadius) {
            aiCharacterControl.SetTarget(player.transform);
        } else {
            aiCharacterControl.SetTarget(transform);
        }
    }

    void SpawnProjectile()
    {
        GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);

        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
        projectileComponent.SetProjectileDamage(damagePerShot);

        Vector3 unitVectorToPlayer = (player.transform.position - projectileSocket.transform.position).normalized;
        float projectileSpeed = projectileComponent.projectileSpeed;
        newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileSpeed;

    }

    void OnDrawGizmos()
    {
        // Draw attack sphere
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);


        // Draw chase sphere
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }

}
