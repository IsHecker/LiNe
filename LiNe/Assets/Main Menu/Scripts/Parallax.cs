using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{
    float length, startposition;
    public GameObject cam;
    public float parallaxeffect;
    public bool y = true;
    // Start is called before the first frame update
    void Start()
    {
        if (y)
        {
            startposition = transform.position.y;
            length = GetComponent<SpriteRenderer>().bounds.size.y;
        }
        else
        {
            startposition = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float distance = 0;
        if (y)
        {
            distance = (cam.transform.position.y * parallaxeffect);
            transform.position = new Vector3(transform.position.x, startposition + distance, transform.position.z);
        }
        else
        {
            distance = (cam.transform.position.x * parallaxeffect);
            transform.position = new Vector3(startposition + distance, transform.position.y, transform.position.z);
        }
        //if (temp > startposition + (length - 2)) startposition += length;
        //else if (temp < startposition - length) startposition -= length;
    }
}
