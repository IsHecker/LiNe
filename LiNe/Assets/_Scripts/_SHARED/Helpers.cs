using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helpers
{
    private static Camera _camera;
    public static Camera Camera { get { if (!_camera) { _camera = Camera.main; } return _camera; } }


    private readonly static Dictionary<float, WaitForSeconds> waitDictionary = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds WaitFor(float seconds) => waitDictionary.TryGetValue(seconds, out var wait) ? wait : waitDictionary[seconds] = new WaitForSeconds(seconds);


    public static bool IsOverUI(Touch touch)
    {
        if (touch.phase != TouchPhase.Began)
            return false;

        return !EventSystem.current.IsPointerOverGameObject(touch.fingerId);
    }
    
    public static GameObject GetPressedUI() => EventSystem.current.currentSelectedGameObject;
    
    public static Vector3 CanvasPositionToWorldPosition(RectTransform canvas)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, canvas.position, Camera, out var result);
        return result;
    }
    
    public static void TransitionUI(GameObject currentUI, GameObject targetUI, Animator animator, string boolName, bool state, float duration)
    {
        targetUI.SetActive(true);
        animator.SetBool(boolName, state);
        Invoke(() => currentUI.SetActive(false), duration);
    }
    
    public static void TransitionToUI(this GameObject currentUI, GameObject targetUI, Animator animator, string boolName, bool state, float duration)
    {
        //Just Extension function of TransitionUI()
        TransitionUI(currentUI, targetUI, animator, boolName, state, duration);
    }

    public static T Find<T>(this T[] value, System.Func<T, bool> predicate)
    {
        foreach (var item in value)
            if (predicate(item)) return item;

        return default(T);
    }

   
    public delegate bool Condition();
    public static void Update(System.Action Method, Condition condition)
    {
        UpdateProcess();
        async void UpdateProcess()
        {
            while (condition())
            {
                await Task.Delay(20);
                Method?.Invoke();
            }
        }

    }

    public static void Update<T1>(System.Action<T1> Method, Condition condition)
    {
        UpdateProcess();
        async void UpdateProcess()
        {
            while (condition())
            {
                await Task.Delay(20);
                Method?.Invoke(default(T1));
            }
        }

    }

    public static void Update<T1, T2>(System.Action<T1, T2> Method, Condition condition)
    {
        UpdateProcess();
        async void UpdateProcess()
        {
            while (condition())
            {
                await Task.Delay(20);
                Method?.Invoke(default(T1), default(T2));
            }
        }

    }

    public static void Invoke(System.Action Method, float time)
    {
        time += Time.time;
        bool done = false;
        Update(() => { if (Time.time >= time) { Method?.Invoke(); done = true; } }, () => !done);
    }

    public static void InvokeMethod<T>(System.Action<T> Method, T value, float time)
    {
        time += Time.time;
        bool done = false;
        Update(() => { if (Time.time >= time + time) { Method?.Invoke(value); done = true; } }, () => !done);
    }

    public static void DeleteChildren(this Transform transform)
    {
        foreach(Transform child in transform) UnityEngine.Object.Destroy(child.gameObject);
    }

    private static readonly RandomNumber randomNumber = new RandomNumber();
    public static int GenerateRandomNumber(int minRange, int maxRange)
    {
        return randomNumber.GetRandomNumber(minRange, maxRange);
    }
}

public class RandomNumber
{
    private int remainingNumbers;
    private int lastNumber;
    private readonly System.Random random = new System.Random();

    public int GetRandomNumber(int min, int max)
    {
        this.remainingNumbers = max - min + 1;

        if (remainingNumbers == 0)
        {
            remainingNumbers = max - min + 1;
            lastNumber = -1; // Reset the last number to avoid immediate repetition.
        }

        int selectedNumber;

        do
        {
            selectedNumber = random.Next(min, max + 1);
        }
        while (selectedNumber == lastNumber);

        lastNumber = selectedNumber;
        remainingNumbers--;

        return selectedNumber;
    }

}
