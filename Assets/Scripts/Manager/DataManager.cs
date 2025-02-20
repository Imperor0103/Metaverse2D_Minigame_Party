using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : Singleton<DataManager>
{
    // PlayerPrefs�� ����� �� �ʿ��� key��
    private const string HighestFlappyScoreKey = "HighestFlappyScore";
    private const string HighestStackScoreKey = "HighestStackScoreKey";
    private const string HighestStackComboKey = "HighestStackComboKey";

    // ��� �������� ���⿡ �����Ѵ�

    // Flappy�� ��������, �ְ�����
    public int thisFlappyScore; // �̹�����
    public int bestFlappyScore; // �ְ���

    // TheStack�� ��������,�����޺�, �ְ�����, �ְ��޺�
    public int thisStackScore;  // �̹�����
    public int thisStackCombo;  // �̹��޺�
    public int bestStackScore;  // �ְ�����
    public int bestStackCombo;  // �ְ��޺�

    private void Awake()
    {
        /// ���� ������ DataManager�� �����ϸ� ���� ���� ���� ����
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        /// ���� �Ŵ����� �����ǵ��� ����
        DontDestroyOnLoad(gameObject);

        // ���� �ҷ��´�
        LoadDataFromPlayerPrefs();
    }

    // Start is called before the first frame update
    void Start()
    {
    }
    public void SaveDataToPlayerPrefs()
    {
        // ������ ������ ���� �����Ѵ�
        PlayerPrefs.SetInt(HighestFlappyScoreKey, bestFlappyScore);
        PlayerPrefs.SetInt(HighestStackScoreKey, bestStackScore);
        PlayerPrefs.SetInt(HighestStackComboKey, bestStackCombo);
    }
    public void LoadDataFromPlayerPrefs()
    {
        // �����͸Ŵ����� �����Ҷ� ���⼭ �����͸� �����´�
        bestFlappyScore = PlayerPrefs.GetInt(HighestFlappyScoreKey, 0);
        bestStackScore = PlayerPrefs.GetInt(HighestStackScoreKey, 0);
        bestStackCombo = PlayerPrefs.GetInt(HighestStackComboKey, 0);
    }

}
