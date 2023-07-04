using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void slowdowntime()
    {
        Time.timeScale = 0;
    }
    public void backtime()
    {
        Time.timeScale = 1;
    }
}
