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

    [Header("老人がお花状態の時に増えるお年玉の金額　※編集用")]
    [SerializeField] private int _flowerUpMoneyValue;

    [Header("老人が困り顔の時に増えるお年玉の金額　※編集用")]
    [SerializeField] private int _troubledFaceUpMoneyValue;

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

    //お花状態お年玉増加処理
    public void FlowerMoodUpMoneyValue()
    {
        playerMoney += _flowerUpMoneyValue;
    }

    public void TroubledFaceUpMoneyValue()
    {
        playerMoney += _troubledFaceUpMoneyValue;
    }

    //お年玉減少処理
    public void DownMoneyValue()
    {
        if (playerMoney > 0)
        {
            playerMoney -= _downMoneyValue;
        }
        
        if (playerMoney < 0)
        {
            playerMoney = 0;
        }
    }
}
