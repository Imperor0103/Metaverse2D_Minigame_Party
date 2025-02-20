using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public GameObject talkPanel;    // ��ȭ�� ���� â�� ����
    public Text talkText;
    public GameObject scanObject;   // NPC�� ������ ������Ʈ
    public bool isAction;   // ���� ����� ����

    // ��ȣ�ۿ��ϴ� scanObj
    public void Action(GameObject scanObj)
    {
        if (isAction)
        {
            isAction = false;
        }
        else
        {
            isAction = true;
            scanObject = scanObj;
            talkText.text = scanObject.name;
        }
        talkPanel.SetActive(isAction);

    }



}
