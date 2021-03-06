﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    Enemy enemy = null;

    // Use this for initialization
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void UpdateHealthBar()
    {
        Image healthBar = GetComponent<Image>();
        healthBar.fillAmount = enemy.healthAsPercentage;
    }


}
