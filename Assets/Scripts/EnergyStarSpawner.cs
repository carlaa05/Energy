using UnityEngine;

public class EnergyStarSpawner : MonoBehaviour
{
    public GameObject energyStarPrefab;

    public int maxStars = 40;
    public float spawnInterval = 0.3f;

    public float minSize = 0.2f;
    public float maxSize = 2.2f;

    public float trailMultiplier = 4f;

    public float minX = -35f;
    public float maxX = 35f;
    public float minZ = -35f;
    public float maxZ = 35f;
    public float yPosition = 0.5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && CountStars() < maxStars)
        {
            SpawnStar();
            timer = 0f;
        }
    }

    void SpawnStar()
    {
        Vector3 pos = new Vector3(
            Random.Range(minX, maxX),
            yPosition,
            Random.Range(minZ, maxZ)
        );

        GameObject star = Instantiate(energyStarPrefab, pos, Quaternion.identity);

        float size = Random.Range(minSize, maxSize);

        EnergyStar energyStar = star.GetComponent<EnergyStar>();

        if (energyStar != null)
        {
            int value = Mathf.RoundToInt(size * trailMultiplier);
            energyStar.SetSizeAndValue(size, value);
        }
    }

    int CountStars()
    {
        return GameObject.FindGameObjectsWithTag("Energy").Length;
    }
}