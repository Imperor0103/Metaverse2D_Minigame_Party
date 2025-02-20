using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// 이건 싱글턴 아니다
public class FlappyUIManager : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    public TextMeshProUGUI restartText;
    public TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        if (titleText == null)
            titleText = GameObject.Find("TitleText")?.GetComponent<TextMeshProUGUI>();
        if (descriptionText == null)
            descriptionText = GameObject.Find("DesText")?.GetComponent<TextMeshProUGUI>();
        if (restartText == null)
            restartText = GameObject.Find("GameoverText")?.GetComponent<TextMeshProUGUI>();
        if (scoreText == null)
            scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();

        Time.timeScale = 0.0f;
        // 키 입력 기다린다
        StartCoroutine(WaitForKeyPress());
    }
    IEnumerator WaitForKeyPress()
    {
        yield return new WaitUntil(() => Input.anyKeyDown);

        // 키가 눌리면 UI 변경
        titleText.gameObject.SetActive(false);
        descriptionText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);

        Time.timeScale = 1.0f;
    }

    public void SetRestart()
    {
        restartText.gameObject.SetActive(true);
    }
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
