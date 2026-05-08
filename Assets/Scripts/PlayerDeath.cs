using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public string playerName = "Player";
    public AudioClip deathSound;
    public float volume;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")){
            if (deathSound != null)
            {
                AudioSource.PlayClipAtPoint(deathSound, transform.position, volume);
            }
            gameObject.SetActive(false);
            return;
        }
        if (!other.CompareTag("Trail"))
            return;

        TrailOwner trail = other.GetComponent<TrailOwner>();

        if (trail == null || trail.owner == null)
            return;

        if (trail.owner != gameObject)
        {
            if (deathSound != null)
            {
                AudioSource.PlayClipAtPoint(deathSound, transform.position, volume);
            }

            gameObject.SetActive(false);
        }
    }
}