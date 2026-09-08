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


    [Header("上昇値の指数関数の累乗の底の値(y=a^x+b:0<a<1/x=現在のﾒｰﾀｰ)")]
    [Header("(これによって上昇値の減少具合が変わる)　※編集用")]
    [SerializeField] private float _baseOfExponentUpValue;

    [Header("指数関数に足す数(y=a^x+bのbの値)　※編集用")]
    [SerializeField] private float _upExponentAddValue;

    [Header("減少値の指数関数の累乗の底の値(y=a^x+b:1<a/x=現在のﾒｰﾀｰ)")]
    [Header("(これによって減少値の増加具合が変わる)　※編集用")]
    [SerializeField] private float _baseOfExponentDownValue;

    [Header("指数関数にかける数(y=a^x*bのbの値)　※編集用")]
    [SerializeField] private float _downExponentMultiValue;

    [Header("現在の機嫌メーター減少値　※確認用")]
    [SerializeField] private float _currentPowerMeterDownValue;

    [Header("")]
    [SerializeField] HandController handController;

    //機嫌メーターの上昇値の計算
    float _powerMeterUpValue;
    //機嫌メーターの減少値の計算
    float _powerMeterDownValue;


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
        _powerMeterUpValue = Mathf.Pow(_baseOfExponentUpValue, currentPowerMeter) + _upExponentAddValue;
        //機嫌メーターの減少値の計算式
        _powerMeterDownValue = Mathf.Pow(_baseOfExponentDownValue, currentPowerMeter) * _downExponentMultiValue;

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

        _currentPowerMeterUpValue = _powerMeterUpValue;
        _currentPowerMeterDownValue = _powerMeterDownValue;
        checkCurrentPowerMeter = currentPowerMeter;
    }

    //機嫌メーター増加処理
    void UpPowerValue()
    {
        currentPowerMeter += _powerMeterUpValue;
    }

    //機嫌メーター減少処理
    void DownPowerMeter()
    {
        currentPowerMeter -= _powerMeterDownValue * Time.deltaTime;
    }
}
