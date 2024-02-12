using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SildePlatform : MonoBehaviour
{
    private float startPosition;
    private float endPosition;
    public SliderJoint2D slider;
    public SpriteRenderer sprite;
    private float speed;
    private JointMotor2D motor2D;

    private void Start()
    {
        slider = GetComponent<SliderJoint2D>();
        startPosition = transform.position.x;
        endPosition = slider.connectedAnchor.x;
        speed = slider.motor.motorSpeed;
        motor2D = slider.motor;

    }

    private void Update()
    {
        if (transform.position.x >= endPosition)
        {
            sprite.flipY = true;
            speed = 1;
        }
        if (transform.position.x <= startPosition)
        {
            sprite.flipY = false;
            speed = -1;
        }
        motor2D.motorSpeed = speed;
        slider.motor = motor2D;
    }
}
