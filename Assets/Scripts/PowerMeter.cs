using UnityEngine;

public class PowerMeter : MonoBehaviour
{
    [Header("現在の老人の機嫌メーター　※確認用")]
    [SerializeField] private float checkCurrentPowerMeter;
    public float currentPowerMeter { get; private set; }

    [Header("現在の機嫌メーター上昇値(b<c<b+1)")]
    [Header("※確認用")]
    [SerializeField] private float _currentPowerMeterUpValue;

    [Header("機嫌メーターの上限値　※編集用")]
    [SerializeField] private float _editPowerMeterMax;
    public float powerMeterMax { get; private set; }


    [Header("指数関数の累乗の値(y=x^a+b:0<x<1/a=現在のﾒｰﾀｰ)")]
    [Header("(これによって上昇値の減少具合が変わる)　※編集用")]
    [SerializeField] private float _baseOfExponentValue;

    [Header("指数関数に足す数(y=x^a+bのbの値)　※編集用")]
    [SerializeField] private float _exponentAddValue;

    [Header("機嫌メーターの毎秒の減少値　※編集用")]
    [SerializeField] private float _powerMeterDownValue;

    [Header("")]
    [SerializeField] HandController handController;

    //機嫌メーターの上昇値の計算
    float _powerMeterAddValue;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPowerMeter = 0;
        powerMeterMax = _editPowerMeterMax;
    }

    // Update is called once per frame
    void Update()
    {
        //機嫌メーターの上昇値の計算式
        _powerMeterAddValue = Mathf.Pow(_baseOfExponentValue, currentPowerMeter) + _exponentAddValue;

        //肩を叩けたときに実行
        if (handController.SuccessfulHit)
        {
            UpPowerValue();
        }

        //機嫌メーターが0より大きければ毎秒一定値減少させる
        if (currentPowerMeter > 0)
        {
            DownPowerMeter();
        }
        else
        {
            currentPowerMeter = 0;
        }

        _currentPowerMeterUpValue = _powerMeterAddValue;
        checkCurrentPowerMeter = currentPowerMeter;
    }

    //機嫌メーター増加処理
    void UpPowerValue()
    {
        currentPowerMeter += _powerMeterAddValue;
    }

    //機嫌メーター減少処理
    void DownPowerMeter()
    {
        currentPowerMeter -= _powerMeterDownValue * Time.deltaTime;
    }
}
