using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    [Header("Color change params")]

    [SerializeField] private AnimationCurve curve;
    [SerializeField] private Color colorForDead, colorForBlink;
    [SerializeField] private float time = 2.0f;

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
    }

    private IEnumerator Blink()
    {
        for (float t = 0; t < 1; t += Time.deltaTime / time)
        {
            playerRenderer.material.color = Color.Lerp(colorForBlink, colorForDead, curve.Evaluate(t));
            yield return null;
        }
        playerRenderer.material.color = colorForDead;
    }
}
