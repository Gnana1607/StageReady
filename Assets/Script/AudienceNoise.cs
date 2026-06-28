using UnityEngine;

public class AudienceNoise : MonoBehaviour
{
    [SerializeField] private AudioSource whisperSource;
    [SerializeField] private float minTime=8f;
    [SerializeField] private float maxTime=15f;
    private float timer;
    private float nextPlayTime;
    private void Start()
    {
        nextPlayTime=Random.Range(minTime,maxTime);
    }
    private void Update()
    {
        timer+=Time.deltaTime;
        if (timer >= nextPlayTime)
        {
            if (!whisperSource.isPlaying)
            {
                whisperSource.Play();
            }
            timer=0f;
            nextPlayTime=Random.Range(minTime,maxTime);
        }
    }
}
