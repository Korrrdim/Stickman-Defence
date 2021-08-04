using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDrag : MonoBehaviour
{
    private Rigidbody ballRigidbody;
    private float ballDefaultPosY;
    private Vector3 movement;

    private void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
        ballDefaultPosY = transform.position.y;
    }

    private void FixedUpdate()
    {
        forwardMove();
    }

    void OnMouseDrag()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        Vector3 lastPosition = new Vector3(objPosition.x, ballDefaultPosY, objPosition.z);
        transform.position = lastPosition;
    }

    private void forwardMove()
    {
        movement = new Vector3(0.0f, 0.0f, PlayerMove.Speed);
        movement = transform.TransformDirection(movement);
        ballRigidbody.velocity = movement;
    }
}
