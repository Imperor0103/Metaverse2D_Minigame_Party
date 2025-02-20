using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public enum eGameMode
{
    None,           // �⺻���
    FlappyPlane,    // FlappyPlane ���
    TheStack,       // TheStack ���
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

    // ���� ������ ���� �Ŵ���
    DataManager dataManager;

    #region THESTACK MEMBER
    // TheStack ����
    eUIState currentState = eUIState.Home;
    [SerializeField] public HomeUI homeUI = null;
    [SerializeField] public GameUI gameUI = null;
    [SerializeField] public ScoreUI scoreUI = null;
    [SerializeField] public TheStack theStack = null;
    #endregion

    #region FLAPPYPLANE MEMBER
    // FlappyPlane ����
    public TextMeshProUGUI flappyTitleText;
    public TextMeshProUGUI flappyDescriptionText;
    public TextMeshProUGUI flappyGameoverText;
    public TextMeshProUGUI flappyScoreText;
    #endregion

    // ��Ÿ���� ����
    public RecordUI recordUI = null;
    public FishUI fishUI = null;  // fish�� ������ Ʈ���� �ߵ��� �޼��� ����
    public HamburgerUI hamburgerUI = null; // �ܹ��ſ� ������ Ʈ���� �ߵ��� �޼��� ����

    // Ʈ���� ����
    public bool isFishTriggered = false;
    public bool isHamburgerTriggered = false;

    private void Awake()
    {
        instance = this;

        // ���� ���� �ҷ��ͼ� �̴ϰ��Ӻ��� �����Ѵ�
        // �ȵȴ� �Ұ�����
        Scene curScene = SceneManager.GetActiveScene();
        OnSceneLoaded(curScene, LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene curScene, LoadSceneMode mode)
    {
        // ���� �ٲ�� ���� ���� �ִ� ������ �������Ƿ�, ���� �ҷ��;��Ѵ�
        if (curScene.name == "TheStack")
        {
            // �ػ󵵸� 1080, 1920���� �ٲ۴�
            Screen.SetResolution(1080, 1920, false);

            // ���� �ٲ�� ���� ������ �������Ƿ� ���� ã��
            theStack = FindObjectOfType<TheStack>();
            homeUI = FindObjectOfType<HomeUI>(true);
            gameUI = FindObjectOfType<GameUI>(true);
            scoreUI = FindObjectOfType<ScoreUI>(true);

            // homeUI�� canvas�� ������ �����Ƿ� InChildren 
            homeUI = GetComponentInChildren<HomeUI>(true);  // true: ��Ȱ��ȭ ������Ʈ�� �����ؼ� ã�´�
            homeUI?.Init(this); // homeUI�� null�� �ƴϸ� Init(this)
            gameUI = GetComponentInChildren<GameUI>(true);
            gameUI?.Init(this);
            scoreUI = GetComponentInChildren<ScoreUI>(true);
            scoreUI?.Init(this);
            ChangeState(eUIState.Home);
        }
        // �̴ϰ��� �߰�
        else if (curScene.name == "FlappyPlane")
        {
            // �ػ󵵸� 1920, 1080���� �ٲ۴�
            Screen.SetResolution(1920, 1080, false);

            // FlappyPlane UI ��� �ٽ� ã��
            // �� �� �ڷ����� ���Ƽ� �̸����� �˻��ߴ�
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
            // ��Ÿ����
            recordUI = FindObjectOfType<RecordUI>(true);
            recordUI = GetComponentInChildren<RecordUI>(true);  // true: ��Ȱ��ȭ ������Ʈ�� �����ؼ� ã�´�
            recordUI?.Init(this); // homeUI�� null�� �ƴϸ� Init(this)

            if (recordUI != null)
            {
                recordUI.UpdateHighScore(); // �˾Ƽ� �����Ѵ�
            }
            //titleText = transform.Find("TitleText")?.GetComponent<TextMeshProUGUI>();
            //descriptionText = transform.Find("DesText")?.GetComponent<TextMeshProUGUI>();
            //gameoverText = transform.Find("GameoverText")?.GetComponent<TextMeshProUGUI>();
            //flappyScoreText = transform.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();

            fishUI = FindObjectOfType<FishUI>(true);
            hamburgerUI = FindObjectOfType<HamburgerUI>(true);
            // fish�� �ܹ��ŵ� ���� ����(�̹� ������)
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
        // #: ��ó����
        // ��ó���⸦ �̿��Ͽ� �÷������� �ٸ��� �����ϰ� ���� �� �ִ�
        // ����Ƽ ������ ���¶�� �����͸� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }

    public void UpdateScore()
    {
        gameUI.SetUI(theStack.Score, theStack.Combo, theStack.MaxCombo);
    }
    // score�� UI�� ǥ��
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
