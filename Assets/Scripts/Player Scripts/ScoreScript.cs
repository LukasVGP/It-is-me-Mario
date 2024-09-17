using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private Text coinTextScore;
    private AudioSource audioManager;
    private int scoreCount;

    void Awake()
    {
        audioManager = GetComponent<AudioSource>();
    }

    void Start()
    {
        coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();
        scoreCount = PlayerPrefs.GetInt("Score", 0);
        UpdateScoreText();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.COIN_TAG)
        {
            target.gameObject.SetActive(false);
            scoreCount++;
            UpdateScoreText();
            PlayerPrefs.SetInt("Score", scoreCount);
            PlayerPrefs.Save();
            audioManager.Play();
        }
    }

    void UpdateScoreText()
    {
        coinTextScore.text = "x" + scoreCount;
    }
}
