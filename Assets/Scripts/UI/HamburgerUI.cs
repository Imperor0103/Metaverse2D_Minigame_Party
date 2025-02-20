using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HamburgerUI : BaseUI
{
    public Transform enterMini_2;   // ������ ������ �� ��Ȱ��ȭ
    public Transform exitMini_2;    // ������ ���� �� Ȱ��ȭ

    public TextMeshProUGUI successText; // �������� ���

    public TextMeshProUGUI thisStackScoreText;
    public TextMeshProUGUI thisStackComboText;
    public TextMeshProUGUI bestStackScoreText;
    public TextMeshProUGUI bestStackComboText;

    private void Start()
    {
        Init(UIManager.Instance);
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        enterMini_2 = transform.Find("EnterMini_2");
        exitMini_2 = transform.Find("ExitMini_2");


        bestStackScoreText = transform.Find("EnterMini_2/BSScoreText").GetComponent<TextMeshProUGUI>();
        bestStackComboText = transform.Find("EnterMini_2/BSComboText").GetComponent<TextMeshProUGUI>();
        thisStackScoreText = transform.Find("ExitMini_2/TSScoreText").GetComponent<TextMeshProUGUI>();
        thisStackComboText = transform.Find("ExitMini_2/TSComboText").GetComponent<TextMeshProUGUI>();

    }
    public void UpdateHighScore()
    {
        // data�Ŵ����� �ִ°� �����ͼ� �׳� �����Ѵ�
        thisStackScoreText.text = DataManager.Instance.thisStackScore.ToString();
        thisStackComboText.text = DataManager.Instance.thisStackCombo.ToString();
        bestStackScoreText.text = DataManager.Instance.bestStackScore.ToString();
        bestStackComboText.text = DataManager.Instance.bestStackCombo.ToString();
    }



    protected override eUIState GetUIState()
    {
        return eUIState.Hamburger;

    }
}
