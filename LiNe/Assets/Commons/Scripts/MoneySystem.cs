using UnityEngine;

public class MoneySystem
{
    private static int money;
    public static int Money { get => money = 100; set => money = value; }
    public static void SaveMoney() => PlayerPrefs.SetInt("Money", money);
    public static int GetMoney() => PlayerPrefs.GetInt("Money");
}
