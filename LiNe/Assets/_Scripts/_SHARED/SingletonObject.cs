using UnityEngine;

public class SingletonObject : MonoBehaviour
{
    private void Awake()
    {
        GameObject oldInstance = GameObject.Find(gameObject.name + " Instance");
        if (oldInstance != null)
            Destroy(oldInstance);
        DontDestroyOnLoad(gameObject);
        gameObject.name += " Instance";
    }
}

