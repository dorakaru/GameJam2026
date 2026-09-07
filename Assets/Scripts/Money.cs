using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    [Header("プレイヤーの現在のお年玉の金額　※確認用")]
    [SerializeField] private int _checkPlayerMoney;
    //シーンをまたぐ変数
    public static int playerMoney;

    [Header("肩たたき一回につき増えるお年玉の金額　※編集用")]
    [SerializeField] private int _upMoneyValue;

    [Header("老人怒り時の肩たたき一回につき減るお年玉の金額　※編集用")]
    [SerializeField] private int _downMoneyValue;

    [Header("")]
    [SerializeField] TextMeshProUGUI moneyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMoney = 0;
        _checkPlayerMoney = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _checkPlayerMoney = playerMoney;
        moneyText.text = playerMoney.ToString();
    }

    //お年玉増加処理
    public void UpMoneyValue()
    {
        playerMoney += _upMoneyValue;
    }

    //お年玉減少処理
    public void DownMoneyValue()
    {
        playerMoney -= _downMoneyValue;
    }
}
