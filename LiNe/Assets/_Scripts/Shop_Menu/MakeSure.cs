using UnityEngine;

public class MakeSure : MonoBehaviour
{
    private Animator animator;

    private static System.Action actionEvent;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Approve()
    {
        actionEvent?.Invoke();
        Close();
    }

    public void Cancel() => Close();

    private void Close()
    {
        animator.Play("Make_Sure_Reversed");
        actionEvent = null;
        Helpers.Invoke(() => gameObject.SetActive(false), 0.45f);
    }

    public static void OnApproveEvent(System.Action e) => actionEvent = e;
}
