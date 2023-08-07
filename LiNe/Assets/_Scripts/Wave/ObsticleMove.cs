using UnityEngine;

public class ObsticleMove : MonoBehaviour
{
    public float Speed;
    public float target;
    [SerializeField] private Vector3 direction;
    private void Start() => transform.LeanMoveLocal(direction, 1 / Speed).setLoopPingPong();
}
