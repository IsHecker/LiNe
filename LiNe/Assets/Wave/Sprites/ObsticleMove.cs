using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleMove : MonoBehaviour
{
    public float Speed,MinDistance,MaxDistance;
    public Vector2 Dirction;
    public bool specificdir = true;
    public bool startmove = false;
    public static bool move = false;
    // Start is called before the first frame update
    void Start()
    {
        if (startmove)
            move = startmove;
    }

    // Update is called once per frame
    void Update()
    {
        if (!move)
            return;


        transform.Translate(Dirction * Speed * Time.deltaTime, Space.Self);


        if (!specificdir)
            return;

        if (transform.localPosition.x > MaxDistance)
            Speed *= -1;
        if (transform.localPosition.x < MinDistance)
            Speed *= -1;
        if (transform.localPosition.y < MinDistance && Dirction.y != 0)
            Speed *= -1;
        if (transform.localPosition.y > MaxDistance && Dirction.y != 0)
            Speed *= -1;
    }
}
