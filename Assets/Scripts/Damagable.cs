using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {

    [SerializeField]
    private HPBarController hpBarController;
    [SerializeField]
    private UnitStatistic unitStatistic;
    [SerializeField]
    private Animator animator;

    private int maxHealth;
    private float actualHealth;

    private void Start()
    {
        maxHealth = unitStatistic.maxHpPoints;
        actualHealth = maxHealth;
        if (hpBarController != null)
            hpBarController.MaxHealthPoint = maxHealth;
    }

    public void DealDamage(float damage)
    {
        actualHealth -= damage;
        if (hpBarController != null)
            hpBarController.ActualHealthPoint = actualHealth;

        if (animator != null)
            animator.SetTrigger("GetHurt");

        if (actualHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.CompareTag("Player"))
            if (Input.GetKeyDown(KeyCode.A))
                DealDamage(1);
    }
}
