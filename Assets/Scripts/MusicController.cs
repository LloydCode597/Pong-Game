using UnityEngine;

public class MusicController : MonoBehaviour
{
    public ScoreManager scoreManager;
    public float normalVolume = 0.35f;
    public float gameOverVolume = 0.1f;

    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.volume = normalVolume;
    }

    void Update()
    {
        float target = scoreManager.GameOver ? gameOverVolume : normalVolume;
        source.volume = Mathf.MoveTowards(source.volume, target, Time.deltaTime * 0.5f);
    }
}
