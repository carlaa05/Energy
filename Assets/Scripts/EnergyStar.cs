using UnityEngine;

public class EnergyStar : MonoBehaviour
{
    public int value = 5;

    public float blinkSpeed = 3f;
    public float blinkIntensity = 2f;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (rend != null)
        {
            float emission = 1f + Mathf.Sin(Time.time * blinkSpeed) * blinkIntensity;
            rend.material.SetColor("_EmissionColor", Color.cyan * emission);
        }
    }

    public void SetSizeAndValue(float size, int newValue)
    {
        transform.localScale = Vector3.one * size;
        value = newValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnergyTrail trail = other.GetComponent<EnergyTrail>();

        if (trail != null)
        {
            trail.AddTrailPoints(value);
            Destroy(gameObject);
        }
    }
}