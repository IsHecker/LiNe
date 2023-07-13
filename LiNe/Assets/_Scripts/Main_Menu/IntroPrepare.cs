using UnityEngine;
using UnityEngine.Rendering;

public class IntroPrepare : MonoBehaviour
{
    private Transform Head;
    private CameraControl cameraTweak;
    private Animator anim;
    const byte Head_Speed = 2;
    const byte Initate_Point = 0;
    Vector3 cameraOffset = Vector3.zero;


    [SerializeField] private Transform mainCamera;
    [SerializeField] private GameObject Line_Title;
    [SerializeField] private GameObject PTS_Title;
    [SerializeField] private GameObject SelectionMode_UI;

    private bool isMenuPrepared = false;
    private bool firstTimeOpened = false;
    private void Awake()
    {
        Head = GameObject.Find("Head").transform;
        cameraTweak = Camera.main.GetComponent<CameraControl>();
        anim = GetComponent<Animator>();
        OpenPreparedMenu();
    }
    private void OpenPreparedMenu()
    {
        firstTimeOpened = ScenesData.firstTimeOpened;
        SelectionMode_UI.SetActive(firstTimeOpened);
        showTitles();
    }
    private void PrepareMainMenu()
    {
        if (isMenuPrepared) return;
        //if (!Input.GetMouseButtonDown(0) || !(Head.position.y >= Initate_Point + 2)) return;
        if (!Input.GetMouseButtonDown(0) && !firstTimeOpened) return;

        ScenesData.firstTimeOpened = true;
        prepare = true;
        anim.SetBool("Menu", true);
    }

    private void CameraFollow()
    {
        if (Head.transform.position.y >= 0)
            mainCamera.position = new Vector3(0, Head.position.y, -100);
    }

    private void HeadMoveAnimation() => Head.Translate(Vector2.up * Head_Speed * Time.deltaTime);

    private bool prepare; //if the screen is pressed and the menu showed up.
    private void showTitles()
    {
        if(isMenuPrepared) return;

        if (Head.transform.position.y < 0 && !prepare) return;
        Line_Title.SetActive(true);
        if (Head.transform.position.y > 2 && !prepare) { PTS_Title.SetActive(true); return; }
        PTS_Title.SetActive(false);
    }

    private void MoveTitle() => Line_Title.transform.position = new Vector3(cameraTweak.XSlider.localPosition.x, mainCamera.position.y, -10);

    const byte Resize_Time = 1;
    private void ResizeCameraAnimation()
    {
        if (!prepare || isMenuPrepared) return; 
        float targetSize = 5;
        float cameraSize = cameraTweak.MainCamera.orthographicSize;
        LeanTween.value(cameraSize, targetSize, 0.7f).setEaseOutQuart().setOnUpdate((value) => { cameraTweak.MainCamera.orthographicSize = value; });
        LeanTween.moveLocalX(cameraTweak.XSlider.gameObject, 0f, Resize_Time).setEaseOutQuart();
        isMenuPrepared = true;
        //cameraTweak.Size = (5, 2);
    }

    private void Update()
    {
        HeadMoveAnimation();
        CameraFollow();
        PrepareMainMenu();
        showTitles();
        ResizeCameraAnimation();
    }
    private void LateUpdate()
    {
        MoveTitle();
    }

}
