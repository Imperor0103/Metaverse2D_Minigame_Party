using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public GameObject talkPanel;    // 대화할 때만 창을 띄운다
    public Text talkText;
    public GameObject scanObject;   // NPC를 포함한 오브젝트
    public bool isAction;   // 상태 저장용 변수

    // 상호작용하는 scanObj
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
