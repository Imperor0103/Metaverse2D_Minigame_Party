using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// 게임오버, 재시작, 점수
public class GameManager : MonoBehaviour
{
    // singleton 객체를 만들기 위한 요소
    static GameManager gameManager; // 1.자신을 참조하는 static 변수
    public static GameManager Instance { get { return gameManager; } }  // 2.그 변수를 외부로 가져갈 수 있는 프로퍼티 1개

    private int currentScore = 0;

    // 트리거 관련
    public Fish fish;
    public Hamburger hamburger;
    public DataManager dataManager; // 점수전달

    // UI매니저
    public FlappyUIManager flappyUiManager;
    public FlappyUIManager FlappyUIManagerInstance { get { return flappyUiManager; } }    // UIManager 를 외부로 가져갈 수 있는 프로퍼티


    private void Awake()
    {
        gameManager = this; // 3.최초의 객체를 생성해주는 작업

        fish = FindObjectOfType<Fish>();
        hamburger = FindObjectOfType<Hamburger>();

        // UIManager는 싱글턴 객체가 아니므로 FindObjectsOfType으로 찾는다
        flappyUiManager = FindObjectOfType<FlappyUIManager>();

    }
    private void Start()
    {
        dataManager = DataManager.Instance;

        flappyUiManager.UpdateScore(0);   // 시작할때는 0점
    }

    // 게임 오버
    public void GameOver()
    {
        Debug.Log("Game Over");
        /// 게임오버 되었을때 시간을 비활성화 할 필요 없다
        /// 이미 비행기는 떨어져서 정지했기 때문
    }
    // 재시작은 없다. Lobby로 돌아간다
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        /// 맨 처음 LoadScene할때는 FlappyPlanePlayer에서 시간을 정지한다
        /// 클릭이 되었다면 시간을 활성화하고 진행해야한다
        Time.timeScale = 1.0f;
    }
    public void StopGame()
    {
        // 씬 바꾸기 전에 UI 비활성화
        flappyUiManager.restartText.gameObject.SetActive(false);
        flappyUiManager.scoreText.gameObject.SetActive(false);

        // 점수 전달, 갱신
        int score = int.Parse(flappyUiManager.scoreText.text);  // 이번에 받은 점수
        dataManager.thisFlappyScore = score;

        int max = dataManager.bestFlappyScore;
        max = max > score ? max : score;
        dataManager.bestFlappyScore = max;

        /// PlayerPrefbs에 저장(매니저에 저장해도 넘어가면 매니저를 새로생성해버린다)
        // 따라서 씬 바뀌기 전에 저장해야한다
        dataManager.SaveDataToPlayerPrefs();        

        SceneManager.LoadScene("Lobby");    // 로비로 돌아간다
    }
    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log("Score: " + currentScore);
        flappyUiManager.UpdateScore(currentScore);    // 점수 추가
    }
}
