using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float destination;
    [SerializeField] private float degree;

    [SerializeField] private bool loop;

    [SerializeField] private Vector3 direction;
    [SerializeField] private Vector3 rotationDirection;
    private void Start()
    {
        direction.Normalize();
        rotationDirection.Normalize();

        if (loop)
        {
            transform.LeanMoveLocal(direction * destination, 1 / speed).setLoopPingPong();
            transform.LeanRotateAroundLocal(rotationDirection, degree, 1 / speed);
        }
    }
    private void Update()
    {
        if (loop) return;
        if (direction.magnitude != 0)
            transform.Translate(speed * Time.deltaTime * direction, Space.Self);
        if (rotationDirection.magnitude != 0)
            transform.Rotate(speed * Time.deltaTime * rotationDirection, Space.Self);
    }
}
