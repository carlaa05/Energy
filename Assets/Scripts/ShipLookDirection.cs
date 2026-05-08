using UnityEngine;

public class ShipLookDirection : MonoBehaviour
{
    public Transform playerRoot;
    public float rotationSpeed = 10f;

    private Vector3 lastPosition;

    void Start()
    {
        if (playerRoot == null)
        {
            playerRoot = transform.parent;
        }

        lastPosition = playerRoot.position;
    }

    void Update()
    {
        Vector3 movement = playerRoot.position - lastPosition;
        movement.y = 0;

        if (movement.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        lastPosition = playerRoot.position;
    }
}