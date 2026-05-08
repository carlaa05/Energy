using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float speedX = 0.01f;
    public float speedY = 0.0f;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        rend.material.mainTextureOffset = new Vector2(
            Time.time * speedX,
            Time.time * speedY
        );
    }
}