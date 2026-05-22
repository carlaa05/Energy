using UnityEngine;

public class AsteroidMover : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 5f;

    void Start()
    {
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}