using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float Speed, TimeToRevert, TimeToAttack;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Transform allertColliderTransform;
    [SerializeField] private LayerMask allertMask;
    [SerializeField] private Vector2 allertRange;

    public Transform attackTrigger;

    private Rigidbody2D rb;
    private bool allertR, allertL;
    private float currentState, currentTimeToRevert, lastState;
    private Health health;
    private Quaternion attR;
    private Quaternion attL;

    private const float IDLE_STATE = 0;
    private const float WALK_STATE = 1;
    private const float REVERT_STATE = 2;
    private const float ATTACK_STATE = 3;

    void Start()
    {
        currentState = WALK_STATE;
        currentTimeToRevert = 0;
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        attR = Quaternion.Euler(0, 180, 0);
        attL = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        if (health.isAlive)
        {
            if (currentTimeToRevert >= TimeToRevert)
            {
                currentTimeToRevert = 0;
                currentState = REVERT_STATE;
            }
            switch (currentState)
            {
                case IDLE_STATE:
                    currentTimeToRevert += Time.deltaTime;
                    break;
                case WALK_STATE:
                    rb.velocity = Vector2.left * Speed;
                    break;
                case REVERT_STATE:
                    sp.flipX = !sp.flipX;
                    Speed *= -1;
                    currentState = WALK_STATE;
                    break;
                case ATTACK_STATE:
                    rb.velocity = Vector2.left * 0;
                    anim.Play("Attack");
                    break;
            }
        }



        anim.SetFloat("Velocity", rb.velocity.magnitude);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyStopper"))

            currentState = IDLE_STATE;
        Debug.Log(collision.CompareTag("EnemyStopper"));
    }

    private void FixedUpdate()
    {
        Vector2 overlapBoxPosition = allertColliderTransform.position;
        allertL = Physics2D.OverlapBox(overlapBoxPosition - allertRange, allertRange, 0, allertMask); 
        allertR = Physics2D.OverlapBox(overlapBoxPosition + allertRange, allertRange, 0, allertMask);

        if ((allertL & Speed > 0) || (allertR & Speed < 0))
        {
            if (currentState != ATTACK_STATE)
            {
                lastState = currentState;
                currentState = ATTACK_STATE;               
            }
        }
        else if (currentState == ATTACK_STATE) currentState = lastState;
        if ((allertL & Speed < 0) || (allertR & Speed > 0))
        {
            currentState = REVERT_STATE;
        }

        if (sp.flipX)
            attackTrigger.rotation = attR;
        else attackTrigger.rotation = attL;
    }
}


