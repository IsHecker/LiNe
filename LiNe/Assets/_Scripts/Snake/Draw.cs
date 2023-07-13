using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Draw : MonoBehaviour
{
    [SerializeField] private Transform PlayerHead;
    private List<Vector2> points = new List<Vector2>();
    private EdgeCollider2D edge;
    private TrailRenderer playerTrail;
    private float trailTime;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        points.Add(PlayerHead.transform.position);
        edge = GetComponent<EdgeCollider2D>();
        playerTrail = PlayerHead.gameObject.GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        trailTime = playerTrail.time;
        if (timer <= trailTime && points.Count > 1) timer += Time.deltaTime;
        if (Vector2.Distance(points.Last(), PlayerHead.position) > 0.15f) DrawCollision(PlayerHead.position);
    }
    void DrawCollision(Vector2 position)
    {
        if (timer > trailTime) points.RemoveAt(0);
        if (points.Count > 1) edge.points = points.ToArray<Vector2>();
        points.Add(position);
    }
}
