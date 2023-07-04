using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bestscoretext : MonoBehaviour
{
    public TMP_Text bestscore;
    public string savescorekey;
    // Start is called before the first frame update
    void Start()
    {
        bestscore.text = PlayerPrefs.GetFloat(savescorekey).ToString("0");
    }
}
