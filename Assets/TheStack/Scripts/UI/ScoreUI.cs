using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreUI : BaseUI
{
    TextMeshProUGUI scoreText;
    TextMeshProUGUI comboText;
    TextMeshProUGUI bestScoreText;
    TextMeshProUGUI bestComboText;

    Button startButton;
    Button exitButton;

    DataManager dataManager;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        comboText = transform.Find("ComboText").GetComponent<TextMeshProUGUI>();
        bestScoreText = transform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
        bestComboText = transform.Find("BestComboText").GetComponent<TextMeshProUGUI>();
        startButton = transform.Find("StartButton").GetComponent<Button>();
        exitButton = transform.Find("ExitButton").GetComponent<Button>();
        dataManager = DataManager.Instance;

        // OnCLick �̺�Ʈ ����
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }
    protected override eUIState GetUIState()
    {
        return eUIState.Score;
    }

    public void SetUI(int score, int combo, int bestScore, int bestCombo)
    {
        scoreText.text = score.ToString();
        comboText.text = combo.ToString();
        bestScoreText.text = bestScore.ToString();
        bestComboText.text = bestCombo.ToString();

        // data�Ŵ����� �Ѱ��ֱ�
        dataManager.thisStackCombo = score;
        dataManager.thisStackCombo = combo;
        dataManager.bestStackScore = bestScore;
        dataManager.bestStackCombo = bestCombo;
        /// PlayerPrefbs�� ����(�Ŵ����� �����ص� �Ѿ�� �Ŵ����� ���λ����ع�����)
        dataManager.SaveDataToPlayerPrefs();
    }

    // ��ư���� �̺�Ʈ �޼���
    void OnClickStartButton()
    {
        uiManager.OnClickStart();
    }
    void OnClickExitButton()
    {
        SceneManager.LoadScene("Lobby");

        //uiManager.OnClickExit();
    }

}
