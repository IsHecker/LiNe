using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private float minimumVertexDistance = 0.1f;
    [SerializeField] private Vector3 velocity;

    private LineRenderer line;
    private List<Vector3> points = new List<Vector3>();
    private Queue<float> spawnTimes = new Queue<float>();

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;
        points.Add(transform.position);
        line.positionCount = 1;
        line.SetPosition(0, transform.position);
    }

    private void AddPoint(Vector3 position)
    {
        points.Insert(0, position);
        spawnTimes.Enqueue(Time.time);
    }

    private void RemovePoint()
    {
        spawnTimes.Dequeue();
        points.RemoveAt(points.Count - 1);
    }

    private void Update()
    {
        while (spawnTimes.Count > 0 && spawnTimes.Peek() + lifetime < Time.time)
        {
            RemovePoint();
        }

        Vector3 diff = -velocity * Time.deltaTime;
        for (int i = 0; i < points.Count; i++)
        {
            points[i] += diff;
        }

        if (Vector3.Distance(transform.position, points[0]) > minimumVertexDistance)
        {
            AddPoint(transform.position);
        }

        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
    }
}
