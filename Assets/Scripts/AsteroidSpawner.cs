using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;

    public float spawnInterval = 2f;
    public float minSpeed = 3f;
    public float maxSpeed = 7f;

    public float minSize = 0.6f;
    public float maxSize = 2f;

    public float minX = -35f;
    public float maxX = 35f;
    public float minZ = -35f;
    public float maxZ = 35f;
    public float yPosition = 0.5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnAsteroid();
            timer = 0f;
        }
    }

    void SpawnAsteroid()
    {
        bool spawnFromX = Random.value > 0.5f;

        Vector3 position;

        if (spawnFromX)
        {
            float x = Random.value > 0.5f ? minX : maxX;
            position = new Vector3(x, yPosition, Random.Range(minZ, maxZ));
        }
        else
        {
            float z = Random.value > 0.5f ? minZ : maxZ;
            position = new Vector3(Random.Range(minX, maxX), yPosition, z);
        }

        Vector3 target = new Vector3(
            Random.Range(minX, maxX),
            yPosition,
            Random.Range(minZ, maxZ)
        );

        Vector3 direction = target - position;

        GameObject asteroid = Instantiate(asteroidPrefab, position, Quaternion.identity);

        float size = Random.Range(minSize, maxSize);
        asteroid.transform.localScale = Vector3.one * size;

        AsteroidMover mover = asteroid.GetComponent<AsteroidMover>();
        if (mover != null)
        {
            mover.direction = direction;
            mover.speed = Random.Range(minSpeed, maxSpeed);
        }
    }
}