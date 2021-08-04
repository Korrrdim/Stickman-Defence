using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    public static float Speed;
    [SerializeField] private Transform cameraTransform;

    private Rigidbody playerRigidbody;
    private Animator playerAnimation;
    private Vector3 movement;
    private Vector3 newCameraPos;

    private void Start()
    {
        Speed = speed;
        playerAnimation = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimation.SetBool("Run", true);
    }

    private void FixedUpdate()
    {
        forwardMove();
    }

    private void forwardMove()
    {
        movement = new Vector3 (0.0f, 0.0f, Speed);
        movement = transform.TransformDirection(movement);
        playerRigidbody.velocity = movement;

        newCameraPos = transform.position;
        cameraTransform.position = newCameraPos;
    }
}
