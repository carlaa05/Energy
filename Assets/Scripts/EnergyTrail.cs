using System.Collections.Generic;
using UnityEngine;

public class EnergyTrail : MonoBehaviour
{
    [Header("Line Renderer")]
    public LineRenderer lineRenderer;

    [Header("Trail Settings")]
    public int maxPoints = 80;
    public float minDistance = 0.15f;

    [Header("Collider Settings")]
    public GameObject trailColliderPrefab;

    private List<Vector3> points = new List<Vector3>();
    private List<GameObject> trailColliders = new List<GameObject>();

    private Vector3 lastPoint;

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        lineRenderer.positionCount = 0;
        lastPoint = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, lastPoint) >= minDistance)
        {
            AddPoint(lastPoint);
            lastPoint = transform.position;
        }
    }

    void AddPoint(Vector3 point)
    {
        points.Add(point);

        // Crear collider
        if (trailColliderPrefab != null)
        {
            GameObject col = Instantiate(trailColliderPrefab, point, Quaternion.identity);

            TrailOwner ownerScript = col.GetComponent<TrailOwner>();

            if (ownerScript == null)
            {
                ownerScript = col.AddComponent<TrailOwner>();
            }

            ownerScript.owner = gameObject;

            trailColliders.Add(col);
        }

        // Limitar tamaño del trail
        if (points.Count > maxPoints)
        {
            points.RemoveAt(0);

            if (trailColliders.Count > 0)
            {
                Destroy(trailColliders[0]);
                trailColliders.RemoveAt(0);
            }
        }

        // Actualizar línea
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}