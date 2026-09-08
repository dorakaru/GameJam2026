using UnityEngine;

public class PeopleMood : MonoBehaviour
{
    [Header("老人のお花状態突入割合(%表記)　※編集用")]
    [SerializeField] private float _flowerPercentage;
    [SerializeField] GameObject flowerEffect;

    [Header("老人の困り顔突入割合(%表記)　※編集用")]
    [SerializeField] private float _troubledFacePercentage;
    [SerializeField] GameObject troubledFaceEffect;

    [Header("老人の怒り突入割合(%表記)　※編集用")]
    [SerializeField] private float _angerPercentage;
    [SerializeField] GameObject angerEffect;

    [Header("エフェクトの傾く間隔の時間　※編集用")]
    [SerializeField] private float _effectRoteSpan;

    [Header("エフェクトの傾き具合　※編集用")]
    [SerializeField] private float _effectRoteValue;

    [Header("")]
    [SerializeField] HandController handController;
    [SerializeField] PowerMeter powerMeter;
    [SerializeField] Money money;

    float effectTimer;
    bool effectMoved;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideEffect();
        effectTimer = 0;
        effectMoved = false;
    }

    private void OnDisable()
    {
        HideEffect();
        effectTimer = 0;
        effectMoved = false;
    }

    // Update is called once per frame
    void Update()
    {
        HideEffect();
        effectTimer += Time.deltaTime;

        if (powerMeter.currentPowerMeter /powerMeter.powerMeterMax * 100 >= _angerPercentage)
        { //機嫌メーターが老人の怒り突入割合を越えた場合に処理

            //エフェクト表示
            DisplayEffect(angerEffect);

            //肩を叩けたときに実行
            if (handController.SuccessfulHit)
            {
                money.DownMoneyValue();
            }
        }
        else if (powerMeter.currentPowerMeter / powerMeter.powerMeterMax * 100 >= _troubledFacePercentage)
        { //機嫌メーターが老人の困り顔突入割合を越えた場合に処理

            //エフェクト表示
            DisplayEffect(troubledFaceEffect);

            //肩を叩けたときに実行
            if (handController.SuccessfulHit)
            {
                money.TroubledFaceUpMoneyValue();
            }
        }
        else if (powerMeter.currentPowerMeter / powerMeter.powerMeterMax * 100 >= _flowerPercentage)
        { //機嫌メーターが老人のお花状態突入割合を越えた場合に処理

            //エフェクト表示
            DisplayEffect(flowerEffect);

            //肩を叩けたときに実行
            if (handController.SuccessfulHit)
            {
                money.FlowerMoodUpMoneyValue();
            }
        }
        else
        { //機嫌メーターが上記以外(エフェクトがない状態)の場合に処理
            //肩を叩けたときに実行
            if (handController.SuccessfulHit)
            {
                money.UpMoneyValue();
            }
        }
    }

    //エフェクトを非表示にする処理
    void HideEffect()
    {
        flowerEffect.GetComponent<SpriteRenderer>().enabled = false;
        troubledFaceEffect.GetComponent<SpriteRenderer>().enabled = false;
        angerEffect.GetComponent<SpriteRenderer>().enabled = false;
    }

    //エフェクトを表示して傾かせる処理
    void DisplayEffect(GameObject effect)
    {
        effect.GetComponent<SpriteRenderer>().enabled = true;

        if (effectTimer >= _effectRoteSpan)
        {
            if (!effectMoved)
            {
                Transform effectTransform = effect.GetComponent<Transform>();
                effectTransform.localEulerAngles = new Vector3(0, 0, _effectRoteValue);
                effectMoved = true;
            }
            else
            {
                Transform effectTransform = effect.GetComponent<Transform>();
                effectTransform.localEulerAngles = new Vector3(0, 0, 0);
                effectMoved = false;
            }

            effectTimer = 0;
        }
    }
}
