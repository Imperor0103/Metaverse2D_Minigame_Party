using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishUI : BaseUI
{
    public Transform enterMini_1;   // ������ ������ �� ��Ȱ��ȭ
    public Transform exitMini_1;    // ������ ���� �� Ȱ��ȭ
    public TextMeshProUGUI successText; // �������� ���
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
    // Lobby ���� �ö� ȣ���ؾ��Ѵ�
    public void UpdateHighScore()
    {
        // data�Ŵ����� �ִ°� �����ͼ� �׳� �����Ѵ�
        thisFlappyScoreText.text = DataManager.Instance.thisFlappyScore.ToString();
        bestFlappyScoreText.text = DataManager.Instance.bestFlappyScore.ToString();
    }



    protected override eUIState GetUIState()
    {
        return eUIState.Fish;
    }

}
