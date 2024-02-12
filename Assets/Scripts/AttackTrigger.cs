using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{   
    public GameObject sword;
    public void SwordAttack()
    {
        sword.SetActive(true);
    } 
    public void EndAttack()
    {
        sword.SetActive(false);
    }
}
