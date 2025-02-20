using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// ���ӿ���, �����, ����
public class GameManager : MonoBehaviour
{
    // singleton ��ü�� ����� ���� ���
    static GameManager gameManager; // 1.�ڽ��� �����ϴ� static ����
    public static GameManager Instance { get { return gameManager; } }  // 2.�� ������ �ܺη� ������ �� �ִ� ������Ƽ 1��

    private int currentScore = 0;

    // Ʈ���� ����
    public Fish fish;
    public Hamburger hamburger;
    public DataManager dataManager; // ��������

    // UI�Ŵ���
    public FlappyUIManager flappyUiManager;
    public FlappyUIManager FlappyUIManagerInstance { get { return flappyUiManager; } }    // UIManager �� �ܺη� ������ �� �ִ� ������Ƽ


    private void Awake()
    {
        gameManager = this; // 3.������ ��ü�� �������ִ� �۾�

        fish = FindObjectOfType<Fish>();
        hamburger = FindObjectOfType<Hamburger>();

        // UIManager�� �̱��� ��ü�� �ƴϹǷ� FindObjectsOfType���� ã�´�
        flappyUiManager = FindObjectOfType<FlappyUIManager>();

    }
    private void Start()
    {
        dataManager = DataManager.Instance;

        flappyUiManager.UpdateScore(0);   // �����Ҷ��� 0��
    }

    // ���� ����
    public void GameOver()
    {
        Debug.Log("Game Over");
        /// ���ӿ��� �Ǿ����� �ð��� ��Ȱ��ȭ �� �ʿ� ����
        /// �̹� ������ �������� �����߱� ����
    }
    // ������� ����. Lobby�� ���ư���
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        /// �� ó�� LoadScene�Ҷ��� FlappyPlanePlayer���� �ð��� �����Ѵ�
        /// Ŭ���� �Ǿ��ٸ� �ð��� Ȱ��ȭ�ϰ� �����ؾ��Ѵ�
        Time.timeScale = 1.0f;
    }
    public void StopGame()
    {
        // �� �ٲٱ� ���� UI ��Ȱ��ȭ
        flappyUiManager.restartText.gameObject.SetActive(false);
        flappyUiManager.scoreText.gameObject.SetActive(false);

        // ���� ����, ����
        int score = int.Parse(flappyUiManager.scoreText.text);  // �̹��� ���� ����
        dataManager.thisFlappyScore = score;

        int max = dataManager.bestFlappyScore;
        max = max > score ? max : score;
        dataManager.bestFlappyScore = max;

        /// PlayerPrefbs�� ����(�Ŵ����� �����ص� �Ѿ�� �Ŵ����� ���λ����ع�����)
        // ���� �� �ٲ�� ���� �����ؾ��Ѵ�
        dataManager.SaveDataToPlayerPrefs();        

        SceneManager.LoadScene("Lobby");    // �κ�� ���ư���
    }
    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log("Score: " + currentScore);
        flappyUiManager.UpdateScore(currentScore);    // ���� �߰�
    }
}
