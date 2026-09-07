using UnityEngine;

public class HandPower : MonoBehaviour
{
    [Header("現在の老人の機嫌メーター　※確認用")]
    public float currentPowerMeter;

    [Header("現在の機嫌メーター上昇値(b<c<b+1)")]
    [Header("※確認用")]
    public float currentPowerMeterUpValue;

    [Header("機嫌メーターの上限値　※編集用")]
    public float powerMeterMax;

    [Header("指数関数の累乗の値(y=x^a+b:0<x<1/a=現在のﾒｰﾀｰ)")]
    [Header("(これによって上昇値の減少具合が変わる)　※編集用")]
    public float baseOfExponentValue;

    [Header("指数関数に足す数(y=x^a+bのbの値)　※編集用")]
    public float exponentAddValue;

    [Header("機嫌メーターの毎秒の減少値　※編集用")]
    public float powerMeterDownValue;

    //機嫌メーターの上昇値の計算
    float _powerMeterAddValue;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPowerMeter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _powerMeterAddValue = Mathf.Pow(baseOfExponentValue, currentPowerMeter) + exponentAddValue;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpPowerValue();
        }

        if (currentPowerMeter > 0)
        {
            DownPowerMeter();
        }
        else
        {
            currentPowerMeter = 0;
        }

        currentPowerMeterUpValue = _powerMeterAddValue;
    }

    void UpPowerValue()
    {
        currentPowerMeter += _powerMeterAddValue;
    }

    void DownPowerMeter()
    {
        currentPowerMeter -= powerMeterDownValue * Time.deltaTime;
    }
}
