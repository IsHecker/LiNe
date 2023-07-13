using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimcontroller : MonoBehaviour
{
    public GameObject[] buttons;
    public GameObject[] offsets;
    public List<GameObject> tempgb = new List<GameObject>();
    private void OnEnable()
    {
        for (int i = 0; i < buttons.Length; i++)
        {

            GameObject go = Instantiate(buttons[i], Vector3.zero, Quaternion.identity);
            go.SetActive(true);
            go.transform.SetParent(offsets[i].transform);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            tempgb.Add(go);
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < tempgb.Count; i++)
        {
            Destroy(tempgb[i]);
        }
        tempgb.Clear();
    }
}
