using UnityEngine;

public class MainMenuSetup : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform player;

    [SerializeField] private GameObject Line_Title;
    [SerializeField] private GameObject PTS_Title;


    private Transform Head;
    private Animator animator;

    const byte Head_Speed = 2;
    const byte Initate_Point = 0;

    private CameraController cameraController;


    private bool isMenuPrepared = false;
    private bool isFirstTimeOpenedGame = true;
    private bool prepare; //if the screen is pressed and the menu showed up.

    private void Start()
    {
        cameraController = CameraController.Instance;
        animator = GetComponent<Animator>();
        Head = GameObject.Find("Head").transform;
        OpenPreparedMenu();
    }

    private void OpenPreparedMenu()
    {
        isFirstTimeOpenedGame = ScenesData.isFirstTimeOpenedGame;
        ShowTitles();
    }

    private void PrepareMainMenu()
    {
        if (isMenuPrepared || (!Input.GetMouseButtonDown(0) && isFirstTimeOpenedGame)) return;

        //if (!Input.GetMouseButtonDown(0) || !(Head.position.y >= Initate_Point + 2)) return;

        MoveTitle();
        ScenesData.isFirstTimeOpenedGame = false;
        prepare = true;
        animator.SetBool("menu", true);
    }

    private void CameraFollow()
    {
        if (Head.position.y >= 0)
        {
            cameraController.SetYFollow(true);
        }
    }

    private void ShowTitles()
    {
        if(isMenuPrepared) return;

        if (Head.position.y < 0 && !prepare) return;

        Line_Title.SetActive(true);

        if (Head.position.y > 2 && !prepare) 
        { 
            PTS_Title.SetActive(true); 
            return; 
        }

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
        float cameraSize = cameraController.Camera.orthographicSize;
        LeanTween.value(cameraSize, cameraSize + target, 0.7f).setEaseOutQuart().setOnUpdate((value) => { cameraController.Camera.orthographicSize = value; });
        LeanTween.moveLocalX(cameraController.CameraTransform.gameObject, player.position.x - 1.5f, Resize_Time).setEaseOutQuart();
        isMenuPrepared = true;
    }

    private void Update()
    {
        CameraFollow();
        PrepareMainMenu();
        ShowTitles();
        ResizeCameraAnimation();
    }
}
