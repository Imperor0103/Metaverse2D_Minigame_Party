using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    // int는 id
    Dictionary<int, string[]> talkData;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕?", 
            "이 곳에 처음 왔구나?", 
            "미니게임들은 재미있었어?",    // 여기서 답변을 2개로 한다
        "재미있었다니 다행이야",
        "그렇게 재미있지는 않았구나...",
        "그러면 뭐 때문에 온거야?"});


    }

    // 한 문장씩 가져온다
    public string GetTalk(int id, int talkIndex)    // talkIndex: string[]의 몇번째 string을 가져올 건가
    {
        return talkData[id][talkIndex];
    }


}
