using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform cameraTransform;

    private Rigidbody playerRigidbody;
    private Animator playerAnimation;
    private Vector3 movement;
    private Vector3 newCameraPos;
    private float cameraDefaultPosZ;

    private void Start()
    {
        cameraDefaultPosZ = cameraTransform.position.z;
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
        movement = new Vector3(0.0f, 0.0f, speed);
        playerRigidbody.velocity = movement;
        newCameraPos = new Vector3(cameraTransform.position.x, cameraTransform.position.y, cameraDefaultPosZ + transform.position.z);
        cameraTransform.position = newCameraPos;
    }
}
