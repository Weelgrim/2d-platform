//using UnityEngine;

//public class OpponentController : MonoBehaviour
//{
//    public float speed = 5f; // opponent speed
//    public float detectionRange = 5f; // detection range to detect player
//    public float attackRange = 1f; // attack range to hit player
//    public float attackDelay = 1f; // time between attacks
//    private float attackTimer; // timer for tracking attack delay
//    private GameObject player; // reference to the player object
//    private bool facingRight = true; // facing direction of the opponent

//    private void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("Player"); // find the player object using tag
//    }

//    private void Update()
//    {
//        // calculate the distance between the opponent and the player
//        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

//        // if the player is within the detection range
//        if (distanceToPlayer <= detectionRange)
//        {
//            // calculate the direction to the player
//            Vector2 direction = player.transform.position - transform.position;
//            direction.Normalize();

//            // flip opponent to face player direction
//            if (direction.x > 0 && !facingRight)
//            {
//                Flip();
//            }
//            else if (direction.x < 0 && facingRight)
//            {
//                Flip();
//            }

//            // move towards the player
//            transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;

//            // if the opponent is within attack range and attack delay is over
//            if (distanceToPlayer <= attackRange && attackTimer <= 0)
//            {
//                // attack the player
//                Debug.Log("Opponent attacks the player!");

//                // reset attack timer
//                attackTimer = attackDelay;
//            }
//        }

//        // decrement attack timer
//        if (attackTimer > 0)
//        {
//            attackTimer -= Time.deltaTime;
//        }
//    }

//    // flip the opponent's sprite to face the other direction
//    private void Flip()
//    {
//        facingRight = !facingRight;
//        transform.Rotate(0f, 180f, 0f);
//    }

//    // check if the player is behind the opponent
//    private void OnTriggerStay2D(Collider2D collision)
//    {
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            // get the direction from the opponent to the player
//            Vector2 direction = collision.transform.position - transform.position;

//            // flip opponent to face player direction
//            if (direction.x > 0 && !facingRight)
//            {
//                Flip();
//            }
//            else if (direction.x < 0 && facingRight)
//            {
//                Flip();
//            }
//        }
//    }
//}
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    public float speed = 5f; // opponent speed
    public float detectionRange = 5f; // detection range to detect player
    public Vector2 patrolAreaSize = new Vector2(5, 5); // size of the patrol area
    private GameObject player; // reference to the player object
    private Collider2D patrolArea; // reference to the patrol area collider
    private Vector2 patrolAreaCenter; // center of the patrol area
    private bool patrolling = true; // whether the opponent is currently patrolling
    private bool facingRight = true; // facing direction of the opponent

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // find the player object using tag
        patrolArea = GetComponent<Collider2D>(); // get the collider for the patrol area
        patrolAreaCenter = patrolArea.bounds.center; // get the center of the patrol area
    }

    private void Update()
    {
        // if the opponent is patrolling
        if (patrolling)
        {
            // calculate the target position within the patrol area
            Vector2 targetPosition = new Vector2(
                Random.Range(patrolArea.bounds.min.x, patrolArea.bounds.max.x),
                Random.Range(patrolArea.bounds.min.y, patrolArea.bounds.max.y)
            );

            // move towards the target position
            Vector2 direction = targetPosition - (Vector2)transform.position;
            direction.Normalize();
            transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;

            // flip opponent to face movement direction
            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                Flip();
            }
        }
        else // if the opponent is chasing the player
        {
            // calculate the direction to the player
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            // restrict movement to within the patrol area
            Vector2 nextPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;
            if (patrolArea.bounds.Contains(nextPosition))
            {
                transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
            }
            else
            {
                // if the opponent is outside of the patrol area, move back towards the center
                direction = patrolAreaCenter - (Vector2)transform.position;
                direction.Normalize();
                transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
            }

            // flip opponent to face movement direction
            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    // flip the opponent's sprite to face the other direction
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    // check if the player has entered the patrol area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // stop patrolling and start chasing the player
            patrolling = false;
        }
    }

    // check if the player has left the patrol area
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            patrolling = true;
        }
    }
}


