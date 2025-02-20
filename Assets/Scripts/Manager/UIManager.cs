using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public enum eGameMode
{
    None,           // 기본모드
    FlappyPlane,    // FlappyPlane 모드
    TheStack,       // TheStack 모드
}

public enum eUIState
{
    Home,
    Game,
    Score,
    Record,
    Fish,
    Hamburger
}

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    // 점수 갱신을 위한 매니저
    DataManager dataManager;

    #region THESTACK MEMBER
    // TheStack 관련
    eUIState currentState = eUIState.Home;
    [SerializeField] public HomeUI homeUI = null;
    [SerializeField] public GameUI gameUI = null;
    [SerializeField] public ScoreUI scoreUI = null;
    [SerializeField] public TheStack theStack = null;
    #endregion

    #region FLAPPYPLANE MEMBER
    // FlappyPlane 관련
    public TextMeshProUGUI flappyTitleText;
    public TextMeshProUGUI flappyDescriptionText;
    public TextMeshProUGUI flappyGameoverText;
    public TextMeshProUGUI flappyScoreText;
    #endregion

    // 메타버스 관련
    public RecordUI recordUI = null;
    public FishUI fishUI = null;  // fish에 닿으면 트리거 발동해 메세지 띄운다
    public HamburgerUI hamburgerUI = null; // 햄버거에 닿으면 트리거 발동해 메세지 띄운다

    // 트리거 관련
    public bool isFishTriggered = false;
    public bool isHamburgerTriggered = false;

    private void Awake()
    {
        instance = this;

        // 현재 씬을 불러와서 미니게임별로 구분한다
        // 안된다 불가능해
        Scene curScene = SceneManager.GetActiveScene();
        OnSceneLoaded(curScene, LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene curScene, LoadSceneMode mode)
    {
        // 씬이 바뀌면 기존 씬에 있던 참조가 끊어지므로, 새로 불러와야한다
        if (curScene.name == "TheStack")
        {
            // 해상도를 1080, 1920으로 바꾼다
            Screen.SetResolution(1080, 1920, false);

            // 씬이 바뀌면 기존 참조가 끊어지므로 새로 찾기
            theStack = FindObjectOfType<TheStack>();
            homeUI = FindObjectOfType<HomeUI>(true);
            gameUI = FindObjectOfType<GameUI>(true);
            scoreUI = FindObjectOfType<ScoreUI>(true);

            // homeUI는 canvas의 하위에 있으므로 InChildren 
            homeUI = GetComponentInChildren<HomeUI>(true);  // true: 비활성화 오브젝트도 포함해서 찾는다
            homeUI?.Init(this); // homeUI가 null이 아니면 Init(this)
            gameUI = GetComponentInChildren<GameUI>(true);
            gameUI?.Init(this);
            scoreUI = GetComponentInChildren<ScoreUI>(true);
            scoreUI?.Init(this);
            ChangeState(eUIState.Home);
        }
        // 미니게임 추가
        else if (curScene.name == "FlappyPlane")
        {
            // 해상도를 1920, 1080으로 바꾼다
            Screen.SetResolution(1920, 1080, false);

            // FlappyPlane UI 요소 다시 찾기
            // 둘 다 자료형이 같아서 이름으로 검색했다
            flappyTitleText = GameObject.Find("TitleText")?.GetComponent<TextMeshProUGUI>();
            flappyDescriptionText = GameObject.Find("DesText")?.GetComponent<TextMeshProUGUI>();
            flappyGameoverText = GameObject.Find("GameoverText")?.GetComponent<TextMeshProUGUI>();
            flappyScoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
            if (flappyTitleText == null)
                Debug.LogError("title text  is null");
            if (flappyDescriptionText == null)
                Debug.LogError("description text  is null");
            if (flappyGameoverText == null)
                Debug.LogError("gameover text is null");
            if (flappyScoreText == null)
                Debug.LogError("score text  is null");
            flappyGameoverText.gameObject.SetActive(false);
            flappyScoreText.gameObject.SetActive(true);
        }
        else if (curScene.name == "Lobby")
        {
            // 메타버스
            recordUI = FindObjectOfType<RecordUI>(true);
            recordUI = GetComponentInChildren<RecordUI>(true);  // true: 비활성화 오브젝트도 포함해서 찾는다
            recordUI?.Init(this); // homeUI가 null이 아니면 Init(this)

            if (recordUI != null)
            {
                recordUI.UpdateHighScore(); // 알아서 갱신한다
            }
            //titleText = transform.Find("TitleText")?.GetComponent<TextMeshProUGUI>();
            //descriptionText = transform.Find("DesText")?.GetComponent<TextMeshProUGUI>();
            //gameoverText = transform.Find("GameoverText")?.GetComponent<TextMeshProUGUI>();
            //flappyScoreText = transform.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();

            fishUI = FindObjectOfType<FishUI>(true);
            hamburgerUI = FindObjectOfType<HamburgerUI>(true);
            // fish와 햄버거도 점수 갱신(이미 끝났다)
        }
    }
    #region THESTACK
    public void ChangeState(eUIState state)
    {
        currentState = state;
        homeUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        scoreUI?.SetActive(currentState);
    }

    public void OnClickStart()
    {
        theStack.Restart();
        ChangeState(eUIState.Game);
    }

    public void OnClickExit()
    {
        // #: 전처리기
        // 전처리기를 이용하여 플랫폼마다 다르게 동작하게 만들 수 있다
        // 유니티 에디터 상태라면 에디터를 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    public void UpdateScore()
    {
        gameUI.SetUI(theStack.Score, theStack.Combo, theStack.MaxCombo);
    }
    // score를 UI에 표시
    public void SetScoreUI()
    {
        scoreUI.SetUI(theStack.Score, theStack.MaxCombo, theStack.BestScore, theStack.BestCombo);

        ChangeState(eUIState.Score);
    }
    #endregion

    #region FLAPPYPLANE
    public void FlappyGameOver()
    {
        flappyGameoverText.gameObject.SetActive(true);

    }
    public void UpdateScore(int score)
    {
        if (flappyScoreText != null)
        {
            flappyScoreText.text = score.ToString();
        }
    }
    #endregion
}
