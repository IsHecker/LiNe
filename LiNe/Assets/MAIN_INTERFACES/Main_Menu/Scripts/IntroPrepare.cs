using UnityEngine;
using UnityEngine.Rendering;

public class IntroPrepare : MonoBehaviour
{
    private Transform Head;
    private CameraControl cameraTweak;
    private Animator anim;
    const byte Head_Speed = 2;
    const byte Initate_Point = 0;
    Vector3 cameraOffset = new Vector3(1.5f, 0, -100);

    [SerializeField] private Transform mainCamera;
    [SerializeField] private GameObject Line_Title;
    [SerializeField] private GameObject PTS_Title;
    [SerializeField] private GameObject SelectionMode_UI;

    private bool isMenuPrepared = false;
    private bool firstTimeOpened = false;
    private void Start()
    {
        Head = GameObject.Find("Head").transform;
        cameraTweak = Camera.main.GetComponent<CameraControl>();
        anim = GetComponent<Animator>();
        SharePostProcessing();
        OpenPreparedMenu();
    }

    private static Volume volume;
    private void SharePostProcessing()
    {
        if (volume != null) return;
        volume = GameObject.Find("Post Processing").GetComponent<Volume>();
        DontDestroyOnLoad(volume.gameObject);
    }
    private void OpenPreparedMenu()
    {
        firstTimeOpened = ScenesData.firstTimeOpened;
        SelectionMode_UI.SetActive(firstTimeOpened);
        if (!firstTimeOpened) showTitles();
    }
    private void PrepareMainMenu()
    {
        if (isMenuPrepared) return;
        if (cameraTweak.XOffset.x < 0.01f) isMenuPrepared = true;
        //if (!Input.GetMouseButtonDown(0) || !(Head.position.y >= Initate_Point + 2)) return;
        if (!Input.GetMouseButtonDown(0) && !firstTimeOpened) return;

        ScenesData.firstTimeOpened = true;
        prepare = true;
        anim.SetBool("Menu", true);
    }

    private void CameraFollow() => mainCamera.position = new Vector3(0, Head.position.y, -100);

    private void HeadMoveAnimation()
    {
        Head.Translate(Vector2.up * Head_Speed * Time.deltaTime);
        if (Head.transform.position.y >= 0) CameraFollow();
    }

    private bool prepare; //if the screen is pressed and the menu showed up.
    private void showTitles()
    {
        if (Head.transform.position.y < 0) return;
        Line_Title.SetActive(true);

        if (Head.transform.position.y > 2 && !prepare) { PTS_Title.SetActive(true); return; }

        PTS_Title.SetActive(false);
    }


    private void MoveTitle() 
    {
        Line_Title.transform.position = new Vector3(cameraTweak.XOffset.x, mainCamera.position.y, -10);
    }

    const byte Resize_Speed = 5;
    private void ResizeCameraAnimation()
    {
        if (!prepare || isMenuPrepared) return;
        cameraTweak.Size = (5, 2);
        cameraOffset.x = Mathf.Lerp(cameraOffset.x, 0f, Resize_Speed * Time.deltaTime);
        cameraTweak.XOffset = cameraOffset;
    }

    private void Update()
    {
        HeadMoveAnimation();
        showTitles();
        MoveTitle();
        PrepareMainMenu();
        ResizeCameraAnimation();
    }

}
