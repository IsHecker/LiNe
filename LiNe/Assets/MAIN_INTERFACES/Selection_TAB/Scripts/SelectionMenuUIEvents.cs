using System.Collections;
using UnityEngine;

public class SelectionMenuUIEvents : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Fading fader;
    private CameraControl cameraTweak;
    private void Awake() => cameraTweak = Camera.main.GetComponent<CameraControl>();
    public void ToSettings() => animator.SetBool("Settings", true);
    public void ToShop() { ToShopAnimation(); animator.SetBool("Shop", true); }
    public void ToSelectModeMenu() => fader.Canvastranstion("SelectMode_UI");

    const int Speed = 4;
    private void ToShopAnimation() //Simple Animation for preparing shop
    {
        Vector3 offset = Vector3.zero;
        StartCoroutine(CameraAnimation());
        IEnumerator CameraAnimation()
        {
            while (true)
            {
                if (offset.z < -89.8f) yield break;

                offset.x = Mathf.LerpUnclamped(offset.x, 2.8f, Speed * Time.deltaTime);
                offset.y = Mathf.LerpUnclamped(offset.y, -1, Speed * Time.deltaTime);
                offset.z = Mathf.LerpUnclamped(offset.z, -90, Speed * Time.deltaTime);

                cameraTweak.Size = (6, 3);
                cameraTweak.XOffset = offset;
                cameraTweak.YOffset = offset;
                cameraTweak.Angle = offset;

                yield return new WaitForSeconds(0.001f);
            }
        }
    }
}
