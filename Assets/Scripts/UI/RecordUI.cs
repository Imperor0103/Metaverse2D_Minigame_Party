using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordUI : BaseUI
{
    // 두 미니게임의 최고기록을 같이 보여주기 위한 변수
    public TextMeshProUGUI flappyPlaneBestScoreText;
    public TextMeshProUGUI theStackBestScoreText;
    public TextMeshProUGUI theStackBestComboText;


    public Button closeButton;

    private void Start()
    {
        Init(UIManager.Instance);
    }

    protected override eUIState GetUIState()
    {
        return eUIState.Home;
    }
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // transform.Find는 자식인 경우에 한해 비활성화된 오브젝트까지 연결해준다
        flappyPlaneBestScoreText = transform.Find("FPBestScoreText").GetComponent<TextMeshProUGUI>();
        theStackBestScoreText = transform.Find("TSBestScoreText").GetComponent<TextMeshProUGUI>();
        theStackBestComboText = transform.Find("TSBestComboText").GetComponent<TextMeshProUGUI>();


        // Find: GetChild와는 달리 path를 가져온다
        closeButton = transform.Find("CloseButton").GetComponent<Button>();

        /// 에디터의 OnClick을 이용하지 않고 코드를 이용하는 방법
        closeButton.onClick.AddListener(OnClickExitButton);
    }
    // 시작하면 UIManager가 호출해서 flappy와 theStack의 점수 갱신
    public void UpdateHighScore()
    {
        // data매니저에 있는걸 가져와서 그냥 갱신한다
        flappyPlaneBestScoreText.text = DataManager.Instance.bestFlappyScore.ToString();
        theStackBestScoreText.text = DataManager.Instance.bestStackScore.ToString();
        theStackBestComboText.text = DataManager.Instance.bestStackCombo.ToString();
    }
    // 버튼들의 이벤트 메서드
    void OnClickStartButton()
    {
        uiManager.OnClickStart();
    }

    // 아래의 내용은 게임종료니까 다른곳에서 한다
    void OnClickExitButton()
    {
        uiManager.OnClickExit();
    }
}
