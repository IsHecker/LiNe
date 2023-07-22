using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    [SerializeField] private TrailRenderer myTrail;
    [SerializeField] private Transform player;
    private EdgeCollider2D myCollider;
    private List<Vector2> _points;
    private void Start() => myCollider = GetComponent<EdgeCollider2D>();
    private void FixedUpdate() => SetCollisionPoints();
    private void SetCollisionPoints()
    {
        _points = new List<Vector2>();
        for (int position = 0; position < myTrail.positionCount; position++)
            _points.Add(myTrail.GetPosition(position));
        if (_points.Count < 3) return;
        RemovePoints();
        myCollider.SetPoints(_points);
    }
    private void RemovePoints()
    {
        _points.RemoveAt(_points.Count - 1);
        _points.RemoveAt(_points.Count - 1);
        _points.RemoveAt(0);
    } //Removing unnecessary Points that causes Problems
}
