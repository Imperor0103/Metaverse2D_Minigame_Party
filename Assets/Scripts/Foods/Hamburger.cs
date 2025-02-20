using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamburger : MonoBehaviour, ITrigger
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
            // 햄버거 UI창 보여준다
            UIManager.Instance.hamburgerUI.gameObject.SetActive(true);
            UIManager.Instance.isHamburgerTriggered = true;  // update에서 확인 후 씬을 바꾼다
        }
    }
}
