using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fish : MonoBehaviour, ITrigger
{
    public void Toggle()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered:" + collision.name);
        // 충돌체가 메인플레이어(Player)인지 확인
        if (collision.CompareTag("Player"))
        {
            // Fish UI의 최고 점수 갱신
            UIManager.Instance.fishUI.UpdateHighScore();

            // Fish UI창 보여준다
            UIManager.Instance.fishUI.gameObject.SetActive(true);
            UIManager.Instance.fishUI.enterMini_1.gameObject.SetActive(true);

            UIManager.Instance.isFishTriggered = true;  // update에서 확인 후 씬을 바꾼다
        }
    }
}
