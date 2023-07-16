using UnityEngine;

public class SceneToMainMenu : MonoBehaviour
{
    public Fading transition;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(ToMenu), 6.7f);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) ToMenu();
    }
    void ToMenu() => transition.FadeTo("Main Menu");
}
