//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.EventSystems;
///* SUBSCRIBING TO MY YOUTUBE CHANNEL: 'VIN CODES' WILL HELP WITH MORE VIDEOS AND CODE SHARING IN THE FUTURE :) THANK YOU */

//public class MoveToNextLevel : MonoBehaviour
//{
//    public GameObject[] firework;
//    WaveControl headscript;
//    Fading scenfader;
//    public string lvlofmode, unlockedbtn;
//    public bool nextlvl;
//    public static int nextSceneLoad;
//    public AudioSource uiclick;
//    // Start is called before the first frame update
//    void Start()
//    {
//        headscript = FindObjectOfType<WaveControl>();
//        if (headscript.wave && headscript.levels)
//        {
//            lvlofmode = "wavelevel";
//            unlockedbtn = "waveunlockedbtn";
//            nextlvl = true;
//        }
//        else if (headscript.Gravity && headscript.levels)
//        {
//            lvlofmode = "gravitylevel";
//            unlockedbtn = "gravityunlockedbtn";
//            nextlvl = true;
//        }
//        else if (headscript.run && headscript.levels)
//        {
//            lvlofmode = "runlevel";
//            unlockedbtn= "rununlockedbtn";
//            nextlvl = true;
//        }

//        nextSceneLoad = PlayerPrefs.GetInt(lvlofmode);

//        if (uimanager.Instance.snake)
//        {
//            lvlofmode = "snakelevel";
//            unlockedbtn = "snakeunlockedbtn";
//            nextlvl = true;
//        }
//        //if (nextlvl)
//        //{
//        //    int.TryParse(transform.GetChild(0).transform.name, out nextSceneLoad);
//        //    PlayerPrefs.SetInt(lvlofmode, nextSceneLoad);
//        //}
//    }
//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.C))
//            PlayerPrefs.DeleteAll();
//    }

//    public void OnTriggerEnter2D(Collider2D other)
//    {
//        if(other.gameObject.tag == "Player")
//        {
//            headscript.Finish();
//            firework[0].SetActive(true);
//            firework[1].SetActive(true);
//            //Move to next level
//            Finish();
//        }
//    }
//    public void Finish()
//    {
//        nextSceneLoad += 1;
//        PlayerPrefs.SetInt(lvlofmode, nextSceneLoad);
//        //Move to next level
//        if (nextSceneLoad > PlayerPrefs.GetInt(unlockedbtn))
//        {
//            PlayerPrefs.SetInt(unlockedbtn, nextSceneLoad);
//        }
//    }
//    public void selectscene()
//    {
//        uiclick.Play();
//        scenfader = FindObjectOfType<Fading>();
//        var go = EventSystem.current.currentSelectedGameObject;
//        int.TryParse(go.transform.GetChild(0).transform.name, out nextSceneLoad);
//        scenfader.FadeTo(go.name);
//        PlayerPrefs.SetInt(lvlofmode, nextSceneLoad);
//    }

//    public void nextscene()
//    {
//        scenfader = FindObjectOfType<Fading>();
//        scenfader.FadeTo(lvlofmode + nextSceneLoad);
//    }
//}
