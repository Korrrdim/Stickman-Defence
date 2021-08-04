using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    [Header("Color change params")]

    [SerializeField] private AnimationCurve curveColor;
    [SerializeField] private Color colorForDead, colorForBlink;
    [SerializeField] private float timeColor = 2.0f;

    [Header("Rotation params")]

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform ballTransform;
    [SerializeField] private AnimationCurve curveRotation;
    [SerializeField] private float timeRotation = 2.0f;

    private PlayerMove playerMoveScript;
    private Renderer playerRenderer;
    private Animator playerAnimation;

    private void Start()
    {
        playerMoveScript = GetComponent<PlayerMove>();
        playerRenderer = transform.GetChild(1).GetComponent<Renderer>();
        playerAnimation = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.TryGetComponent(out EnemyBrain enemy))
        {
            if (playerMoveScript.enabled)
            {
                StartCoroutine(Blink());
                playerAnimation.SetBool("Run", false);
                playerAnimation.SetBool("Fall", true);
                playerMoveScript.enabled = false;
                Destroy(ball);
            }
        }
        if (other.gameObject.transform.TryGetComponent(out RotatePlace rotate))
        {
            StartCoroutine(Rotate(rotate.transform));
        }

    }

    private IEnumerator Blink()
    {
        for (float t = 0; t < 1; t += Time.deltaTime / timeColor)
        {
            playerRenderer.material.color = Color.Lerp(colorForBlink, colorForDead, curveColor.Evaluate(t));
            yield return null;
        }
        playerRenderer.material.color = colorForDead;
    }

    private IEnumerator Rotate(Transform rotation)
    {
        float tempSpeed = PlayerMove.Speed;
        PlayerMove.Speed = 0.0f;
        playerAnimation.SetTrigger("Rotate");
        yield return new WaitForSeconds(0.5f);
        transform.position = new Vector3(rotation.position.x, transform.position.y, rotation.position.z);
        for (float t = 0; t < 1; t += Time.deltaTime / timeRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation.rotation, curveRotation.Evaluate(t));
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, rotation.rotation, curveRotation.Evaluate(t));
            ballTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, rotation.rotation, curveRotation.Evaluate(t));
            yield return null;
        }
        PlayerMove.Speed = tempSpeed;
        transform.rotation = rotation.rotation;
        Destroy(rotation.gameObject);
    }

}
