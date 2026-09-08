using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] AudioClip shoulderHitSound;
    [SerializeField] AudioClip BGM;

    [SerializeField] HandController handController;

    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(BGM);
    }

    // Update is called once per frame
    void Update()
    {
        //å®Çí@ÇØÇΩÇ∆Ç´Ç…é¿çs
        if (handController.SuccessfulHit)
        {
            audioSource.PlayOneShot(shoulderHitSound);
        }
    }
}
