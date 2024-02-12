using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidenTrigger : MonoBehaviour
{
    public GameObject hidenTile;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            hidenTile.SetActive(true);
    }
}
