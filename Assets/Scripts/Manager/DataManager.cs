using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : Singleton<DataManager>
{
    // PlayerPrefs를 사용할 때 필요한 key값
    private const string HighestFlappyScoreKey = "HighestFlappyScore";
    private const string HighestStackScoreKey = "HighestStackScoreKey";
    private const string HighestStackComboKey = "HighestStackComboKey";

    // 모든 점수들은 여기에 저장한다

    // Flappy의 게임점수, 최고점수
    public int thisFlappyScore; // 이번게임
    public int bestFlappyScore; // 최고점

    // TheStack의 게임점수,게임콤보, 최고점수, 최고콤보
    public int thisStackScore;  // 이번점수
    public int thisStackCombo;  // 이번콤보
    public int bestStackScore;  // 최고점수
    public int bestStackCombo;  // 최고콤보

    private void Awake()
    {
        /// 만약 기존에 DataManager가 존재하면 새로 생긴 것을 삭제
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        /// 기존 매니저가 유지되도록 설정
        DontDestroyOnLoad(gameObject);

        // 점수 불러온다
        LoadDataFromPlayerPrefs();
    }

    // Start is called before the first frame update
    void Start()
    {
    }
    public void SaveDataToPlayerPrefs()
    {
        // 게임이 끝나고 나서 저장한다
        PlayerPrefs.SetInt(HighestFlappyScoreKey, bestFlappyScore);
        PlayerPrefs.SetInt(HighestStackScoreKey, bestStackScore);
        PlayerPrefs.SetInt(HighestStackComboKey, bestStackCombo);
    }
    public void LoadDataFromPlayerPrefs()
    {
        // 데이터매니저를 생성할때 여기서 데이터를 가져온다
        bestFlappyScore = PlayerPrefs.GetInt(HighestFlappyScoreKey, 0);
        bestStackScore = PlayerPrefs.GetInt(HighestStackScoreKey, 0);
        bestStackCombo = PlayerPrefs.GetInt(HighestStackComboKey, 0);
    }

}
