using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public string playerName = "Player";
    public AudioClip deathSound;
    public float volume = 0.5f;

    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        // Obstacles
        if (other.CompareTag("Obstacle"))
        {
            if (deathSound != null)
            {
                audioSource.PlayOneShot(deathSound, volume);
            }

            gameObject.SetActive(false);
            return;
        }

        // Trails
        if (!other.CompareTag("Trail"))
            return;

        TrailOwner trail = other.GetComponent<TrailOwner>();

        if (trail == null || trail.owner == null)
            return;

        if (trail.owner != gameObject)
        {
            if (deathSound != null)
            {
                audioSource.PlayOneShot(deathSound, volume);
            }

            gameObject.SetActive(false);
        }
    }
}