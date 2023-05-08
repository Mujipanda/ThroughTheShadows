using UnityEngine;

public class LiftingPlatform : MonoBehaviour
{
    public Vector3 moveDirection;
    public float moveSpeed = 2f;
    public float moveDistance = 5f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float moveFactor = (Mathf.Sin(Time.time * moveSpeed) + 1) / 2;
        transform.position = Vector3.Lerp(startPosition - moveDirection * moveDistance / 2, startPosition + moveDirection * moveDistance / 2, moveFactor);
    }
}