using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordUI : BaseUI
{
    // �� �̴ϰ����� �ְ����� ���� �����ֱ� ���� ����
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

        // transform.Find�� �ڽ��� ��쿡 ���� ��Ȱ��ȭ�� ������Ʈ���� �������ش�
        flappyPlaneBestScoreText = transform.Find("FPBestScoreText").GetComponent<TextMeshProUGUI>();
        theStackBestScoreText = transform.Find("TSBestScoreText").GetComponent<TextMeshProUGUI>();
        theStackBestComboText = transform.Find("TSBestComboText").GetComponent<TextMeshProUGUI>();


        // Find: GetChild�ʹ� �޸� path�� �����´�
        closeButton = transform.Find("CloseButton").GetComponent<Button>();

        /// �������� OnClick�� �̿����� �ʰ� �ڵ带 �̿��ϴ� ���
        closeButton.onClick.AddListener(OnClickExitButton);
    }
    // �����ϸ� UIManager�� ȣ���ؼ� flappy�� theStack�� ���� ����
    public void UpdateHighScore()
    {
        // data�Ŵ����� �ִ°� �����ͼ� �׳� �����Ѵ�
        flappyPlaneBestScoreText.text = DataManager.Instance.bestFlappyScore.ToString();
        theStackBestScoreText.text = DataManager.Instance.bestStackScore.ToString();
        theStackBestComboText.text = DataManager.Instance.bestStackCombo.ToString();
    }
    // ��ư���� �̺�Ʈ �޼���
    void OnClickStartButton()
    {
        uiManager.OnClickStart();
    }

    // �Ʒ��� ������ ��������ϱ� �ٸ������� �Ѵ�
    void OnClickExitButton()
    {
        uiManager.OnClickExit();
    }
}
