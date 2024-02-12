using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private GameObject glow;
    [SerializeField] private float healKD;

    private bool heal = true;
    private float timeToRestHeal;

    private void Update()
    {
        if (timeToRestHeal >= 0)
        {
            timeToRestHeal -= Time.deltaTime;
            heal = false;
        }
        else 
        {
            heal = true;
            glow.SetActive(true);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damageable") & heal == true & collision.IsTouchingLayers(3))
        {
            collision.gameObject.GetComponent<Health>().TakeHeal();
            glow.SetActive(false);
            timeToRestHeal = healKD;
        }
    }
}

