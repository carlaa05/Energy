using UnityEngine;

public class StartZone : MonoBehaviour
{
    public GameObject requiredPlayer;
    public bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == requiredPlayer || other.transform.IsChildOf(requiredPlayer.transform))
        {
            activated = true;
            Debug.Log(requiredPlayer.name + " activated " + gameObject.name);
        }
    }
}