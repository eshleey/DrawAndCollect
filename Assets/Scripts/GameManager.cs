using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("TECHNICAL OBJECTS")]
    [SerializeField] ThrowBall ThrowBall;
    [SerializeField] DrawLine DrawLine;

    [Header("GENERAL OBJECTS")]
    [SerializeField] private ParticleSystem BucketEnter;
    [SerializeField] private ParticleSystem BestScorePass;
    [SerializeField] private AudioSource[] Sounds;

    [Header("UI OBJECTS")]
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private TextMeshProUGUI[] ScoreTexts;

    int IncomingBallCount;

    void Start()
    {
        IncomingBallCount = 0;

        if (PlayerPrefs.HasKey("BestScore"))
        {
            ScoreTexts[0].text = PlayerPrefs.GetInt("BestScore").ToString();
            ScoreTexts[1].text = PlayerPrefs.GetInt("BestScore").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("BestScore", 0);
            ScoreTexts[0].text = "0";
            ScoreTexts[1].text = "0";
        }
    }
    public void Continue(Vector2 Pos)
    {
        BucketEnter.transform.position = Pos;
        BucketEnter.gameObject.SetActive(true);
        BucketEnter.Play();

        IncomingBallCount++;
        Sounds[0].Play();
        ThrowBall.Continue();
        DrawLine.Continue();
    }

    public void GameOver()
    {
        Sounds[1].Play();
        Panels[1].SetActive(true);
        Panels[2].SetActive(false);

        ScoreTexts[1].text = PlayerPrefs.GetInt("BestScore").ToString();
        ScoreTexts[2].text = IncomingBallCount.ToString();

        if (IncomingBallCount > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", IncomingBallCount);
            BestScorePass.gameObject.SetActive(true);
            BestScorePass.Play();
        }

        ThrowBall.StopThrowBall();
        DrawLine.StopDrawLine();
    }

    public void StartGame()
    {
        Panels[0].SetActive(false);
        ThrowBall.StartGame();
        DrawLine.StartDrawLine();
        Panels[2].SetActive(true);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
