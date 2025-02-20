using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishUI : BaseUI
{
    public Transform enterMini_1;   // 게임이 시작할 때 비활성화
    public Transform exitMini_1;    // 게임이 끝날 때 활성화
    public TextMeshProUGUI successText; // 성공여부 출력
    public TextMeshProUGUI thisFlappyScoreText;
    public TextMeshProUGUI bestFlappyScoreText;

    private void Start()
    {
        Init(UIManager.Instance);
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        enterMini_1 = transform.Find("EnterMini_1");
        exitMini_1 = transform.Find("ExitMini_1");

        bestFlappyScoreText = transform.Find("EnterMini_1/BFScoreText").GetComponent<TextMeshProUGUI>();
        thisFlappyScoreText = transform.Find("ExitMini_1/TFScoreText").GetComponent<TextMeshProUGUI>();
    }
    // Lobby 씬에 올때 호출해야한다
    public void UpdateHighScore()
    {
        // data매니저에 있는걸 가져와서 그냥 갱신한다
        thisFlappyScoreText.text = DataManager.Instance.thisFlappyScore.ToString();
        bestFlappyScoreText.text = DataManager.Instance.bestFlappyScore.ToString();
    }



    protected override eUIState GetUIState()
    {
        return eUIState.Fish;
    }

}
