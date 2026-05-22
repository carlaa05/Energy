using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float speedX = 0.001f;
    public float speedY = 0.0003f;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        Vector2 offset = rend.material.mainTextureOffset;

        offset.x += speedX * Time.deltaTime;
        offset.y += speedY * Time.deltaTime;

        rend.material.mainTextureOffset = offset;
    }
}