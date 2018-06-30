using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {

    [SerializeField] int enemyLayer = 10;
    [SerializeField] float attackRadious = 5f;
    [SerializeField] float damagePerHit = 10f;
    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float minTimeBetweenAttacks = 0.5f;

    GameObject currentTarget = null;
    CameraRaycaster cameraRaycaster;

    float currentHealthPoints;
    float lastHitTime = 0;

    void Start()
    {
        currentHealthPoints = maxHealthPoints;
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
    }

    public float healthAsPercentage {
        get {
            return currentHealthPoints / maxHealthPoints;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
    }

    void OnMouseClick(RaycastHit raycastHit, int layerHit)
    {
        if (layerHit == enemyLayer) {
            GameObject enemy = raycastHit.collider.gameObject;
            float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);

            if (distanceToEnemy > attackRadious) {
                return;
            }

            currentTarget = enemy;

            if (Time.time - lastHitTime > minTimeBetweenAttacks) {
                // Checks if damageableObject hitted implements IDamageable interface
                Component isdamageableObject = enemy.GetComponent(typeof(IDamageable));
                if (isdamageableObject) {
                    (isdamageableObject as IDamageable).TakeDamage(damagePerHit);
                    lastHitTime = Time.time;
                }
            }
        }
    }
}
