using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    // int�� id
    Dictionary<int, string[]> talkData;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "�ȳ�?", 
            "�� ���� ó�� �Ա���?", 
            "�̴ϰ��ӵ��� ����־���?",    // ���⼭ �亯�� 2���� �Ѵ�
        "����־��ٴ� �����̾�",
        "�׷��� ��������� �ʾұ���...",
        "�׷��� �� ������ �°ž�?"});


    }

    // �� ���徿 �����´�
    public string GetTalk(int id, int talkIndex)    // talkIndex: string[]�� ���° string�� ������ �ǰ�
    {
        return talkData[id][talkIndex];
    }


}
