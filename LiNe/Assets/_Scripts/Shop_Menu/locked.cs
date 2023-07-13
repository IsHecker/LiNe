using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class locked : MonoBehaviour
{
    public TMP_Text tasktxt;
    public string keyname;
    public int targetscore;
    public string modename;
    // Start is called before the first frame update
    void Start()
    {
        tasktxt.text = "reach score " + targetscore + " in " + modename + " mode";
        if (PlayerPrefs.GetFloat(keyname) >= targetscore)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
