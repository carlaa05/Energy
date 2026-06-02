using System.Collections.Generic;
using UnityEngine;

public class EnergyTrail : MonoBehaviour
{
    [Header("Line Renderer")]
    public LineRenderer lineRenderer;

    [Header("Trail Settings")]
    public int maxPoints = 5;
    public float minDistance = 0.15f;

    [Header("Collider Settings")]
    public GameObject trailColliderPrefab;

    private List<Vector3> points = new List<Vector3>();
    private List<GameObject> trailColliders = new List<GameObject>();

    private Vector3 lastPoint;
    private int initialMaxPoints;

    public bool canDraw = false;

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        initialMaxPoints = maxPoints;

        lineRenderer.positionCount = 0;
        lastPoint = transform.position;
    }

    void Update()
    {
        if (!canDraw)
            return;

        float distance = Vector3.Distance(transform.position, lastPoint);

        if (distance >= minDistance)
        {
            int steps = Mathf.FloorToInt(distance / minDistance);

            for (int i = 1; i <= steps; i++)
            {
                Vector3 point = Vector3.Lerp(lastPoint, transform.position, (float)i / steps);
                AddPoint(point);
            }

            lastPoint = transform.position;
        }
    }

    void AddPoint(Vector3 point)
    {
        points.Add(point);

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

        if (points.Count > maxPoints)
        {
            points.RemoveAt(0);

            if (trailColliders.Count > 0)
            {
                Destroy(trailColliders[0]);
                trailColliders.RemoveAt(0);
            }
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    public void AddTrailPoints(int amount)
    {
        maxPoints += amount;
    }

    public void ResetTrail()
    {
        canDraw = false;
        points.Clear();

        foreach (GameObject col in trailColliders)
        {
            if (col != null)
            {
                Destroy(col);
            }
        }

        trailColliders.Clear();

        lineRenderer.positionCount = 0;
        maxPoints = 5;
        lastPoint = transform.position;
    }

    public void StartDrawing()
    {
        lastPoint = transform.position;
        canDraw = true;
    }
}