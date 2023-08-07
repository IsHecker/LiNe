using UnityEngine;

public class IntroPrepare : MonoBehaviour
{
    private Transform Head;
    private CameraControl cameraTweak;
    private Animator animator;
    const byte Head_Speed = 2;
    const byte Initate_Point = 0;


    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform player;

    [SerializeField] private GameObject Line_Title;
    [SerializeField] private GameObject PTS_Title;

    private bool isMenuPrepared = false;
    private bool firstTimeOpened = false;
    private bool prepare; //if the screen is pressed and the menu showed up.

    private void Awake()
    {
        Head = GameObject.Find("Head").transform;
        cameraTweak = Helpers.Camera.GetComponent<CameraControl>();
        animator = GetComponent<Animator>();
        OpenPreparedMenu();
    }
    private void OpenPreparedMenu()
    {
        firstTimeOpened = ScenesData.firstTimeOpened;
        ShowTitles();
    }
    private void PrepareMainMenu()
    {
        if (isMenuPrepared) return;
        //if (!Input.GetMouseButtonDown(0) || !(Head.position.y >= Initate_Point + 2)) return;
        if (!Input.GetMouseButtonDown(0) && !firstTimeOpened) return;
        MoveTitle();
        ScenesData.firstTimeOpened = true;
        prepare = true;
        animator.SetBool("menu", true);
    }

    private Vector3 cameraOffset;
    private void CameraFollow()
    {
        cameraOffset.Set(0, Head.position.y, -100);
        if (Head.transform.position.y >= 0)
            mainCamera.position = cameraOffset;
    }

    private void HeadMoveAnimation() => Head.Translate(Head_Speed * Time.deltaTime * Vector2.up);

    private void ShowTitles()
    {
        if(isMenuPrepared) return;

        if (Head.transform.position.y < 0 && !prepare) return;
        Line_Title.SetActive(true);
        if (Head.transform.position.y > 2 && !prepare) { PTS_Title.SetActive(true); return; }
        PTS_Title.SetActive(false);
    }

    private void MoveTitle()
    {
        LeanTween.value(Line_Title.transform.position.x, 0, 1).setEaseOutQuart().
            setOnUpdate(value => Line_Title.transform.position = 
            new Vector3(value, Line_Title.transform.position.y, -10));
    }

    const byte Resize_Time = 1;
    private void ResizeCameraAnimation()
    {
        if (!prepare || isMenuPrepared) return; 
        float target = 1;
        float cameraSize = cameraTweak.MainCamera.orthographicSize;
        LeanTween.value(cameraSize, cameraSize + target, 0.7f).setEaseOutQuart().setOnUpdate((value) => { cameraTweak.MainCamera.orthographicSize = value; });
        LeanTween.moveLocalX(cameraTweak.XSlider.gameObject, player.position.x - 1.5f, Resize_Time).setEaseOutQuart();
        isMenuPrepared = true;
    }

    private void Update()
    {
        //HeadMoveAnimation();
        CameraFollow();
        PrepareMainMenu();
        ShowTitles();
        ResizeCameraAnimation();
    }
}
