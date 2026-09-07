using UnityEngine;

public class PeopleMood : MonoBehaviour
{
    [Header("老人のお花状態突入割合(%表記)　※編集用")]
    public float flowerPercentage;

    [Header("老人の困り顔突入割合(%表記)　※編集用")]
    public float troubledFacePercentage;

    [Header("老人の怒り突入割合(%表記)　※編集用")]
    public float angerPercentage;

    [Header("")]
    [SerializeField] HandController handController;
    [SerializeField] PowerMeter powerMeter;
    [SerializeField] Money money;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(powerMeter.currentPowerMeter / powerMeter.powerMeterMax * 100);
        if (powerMeter.currentPowerMeter /powerMeter.powerMeterMax * 100 >= angerPercentage)
        { //機嫌メーターが老人の怒り突入割合を越えた場合に処理
            //肩を叩けたときに実行
            if (handController.SuccessfulHit)
            {
                money.DownMoneyValue();
            }
        }
        else if (powerMeter.currentPowerMeter / powerMeter.powerMeterMax * 100 >= troubledFacePercentage)
        { //機嫌メーターが老人の困り顔突入割合を越えた場合に処理
            //肩を叩けたときに実行
            if (handController.SuccessfulHit)
            {
                money.UpMoneyValue();
            }
        }
        else if (powerMeter.currentPowerMeter / powerMeter.powerMeterMax * 100 >= flowerPercentage)
        { //機嫌メーターが老人のお花状態突入割合を越えた場合に処理
            //肩を叩けたときに実行
            if (handController.SuccessfulHit)
            {
                money.UpMoneyValue();
            }
        }
        else
        { //機嫌メーターが上記以外の場合に処理
            //肩を叩けたときに実行
            if (handController.SuccessfulHit)
            {
                money.UpMoneyValue();
            }
        }
    }
}
