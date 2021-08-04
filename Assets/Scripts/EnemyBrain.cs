using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [Header("Color change params")]

    [SerializeField] private AnimationCurve curve;
    [SerializeField] private Color colorForDead, colorForBlink;
    [SerializeField] private float time = 2.0f;

    [Header("Look&Move at/to target")]

    [SerializeField] private float distance;
    [SerializeField] private float speed;
    [SerializeField] private Transform target;

    private Renderer enemyRenderer;
    private Animator enemyAnimator;
    private BoxCollider enemyCollider;

    private void Start()
    {
        enemyCollider = GetComponent<BoxCollider>();
        enemyRenderer = transform.GetChild(1).GetComponent<Renderer>();
        enemyAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (enemyAnimator.enabled)
        {
            transform.LookAt(target);
            MoveToTarget();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.TryGetComponent(out BallDrag ball) | other.gameObject.transform.TryGetComponent(out PlayerTrigger player))
        {
            if (enemyAnimator.enabled)
            {
                StartCoroutine(Blink());
                enemyCollider.enabled = false;
                enemyAnimator.enabled = false;
                StartCoroutine(Dead());
            }
        }
    }

    private void MoveToTarget()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < distance)
        {
            enemyAnimator.SetBool("Run", true);
            transform.position += transform.forward * speed;
            transform.localPosition = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
    }

    private IEnumerator Blink()
    {
        for (float t = 0; t < 1; t += Time.deltaTime / time)
        {
            enemyRenderer.material.color = Color.Lerp(colorForBlink, colorForDead, curve.Evaluate(t));
            yield return null;
        }
        enemyRenderer.material.color = colorForDead;
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}