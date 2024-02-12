using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Attacks))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Attacks attacks;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        attacks = GetComponent<Attacks>();
    }

    private void Update()
    {
        float horizontalDirection = Input.GetAxis(GlobalStringVars.HORIZONTAL_AXIS);
        bool isJumpButtonPressed = Input.GetButtonDown(GlobalStringVars.JUMP);

        if (Input.GetButtonDown(GlobalStringVars.FIRE_1))
            attacks.Attack();

        playerMovement.Move(horizontalDirection, isJumpButtonPressed);
    }
}
