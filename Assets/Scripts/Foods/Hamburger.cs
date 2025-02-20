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
        // �浹ü�� �����÷��̾�(Player)���� Ȯ��
        if (collision.CompareTag("Player"))
        {
            // �ܹ��� UIâ �����ش�
            UIManager.Instance.hamburgerUI.gameObject.SetActive(true);
            UIManager.Instance.isHamburgerTriggered = true;  // update���� Ȯ�� �� ���� �ٲ۴�
        }
    }
}
