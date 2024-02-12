using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Animator animator;
    public float maxHealth;
    public float currentHealth;
    public bool isAlive;
    public Counter counter;

    private void Awake()
    {
        currentHealth = maxHealth;
        isAlive = true;
        counter = GameObject.Find("Popap").GetComponent<Counter>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        ChecksiAlive();
    }

    public void TakeHeal()
    {
        currentHealth = maxHealth;
    }

    private void ChecksiAlive()
    {
        if (currentHealth > 0)
        {
            isAlive = true;
            Hit();
        }
        else
        {
            isAlive = false;
            Death();
        }
          

    }

    private void Death()
    {
        animator.SetTrigger("Death");
    }

    private void Hit()
    {
        animator.SetTrigger("Hit");
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
        counter.GetPointsforKill();
    }
}
