using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleRotator : MonoBehaviour
{
    public float speed;
    public Vector3 Dirction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Dirction * speed * Time.deltaTime);
    }
}
