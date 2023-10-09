using UnityEngine;
public class MoneySystem
{
    public static int Money { get; set; } = GetMoney();
    public static void SaveMoney() => PlayerPrefs.SetInt("Money", Money);
    //public static int GetMoney() => PlayerPrefs.GetInt("Money");
    public static int GetMoney() => 500;
}