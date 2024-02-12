using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    public static Attacks instance;

    public bool canReceiveInput;
    public bool inputReceived;
    public Animator anim;
    
    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }
    public void Attack()
    {
        if (canReceiveInput)
        {
            inputReceived = true;
            canReceiveInput = false;
        }
        else
            return;     
    }

    public void InputManager()
    {
        if (!canReceiveInput)
            canReceiveInput = true;
        else
            canReceiveInput = false;
    }
}
