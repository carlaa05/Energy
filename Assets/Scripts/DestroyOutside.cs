using UnityEngine;

public class DestroyOutside : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("border"))
        {
            Destroy(gameObject);
        }
    }
}
