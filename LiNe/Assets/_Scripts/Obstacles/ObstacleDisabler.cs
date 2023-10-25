using UnityEngine;

public class ObstacleDisabler : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
            collision.gameObject.SetActive(false);
    }
}
