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
        // �浹ü�� �����÷��̾�(Player)���� Ȯ��
        if (collision.CompareTag("Player"))
        {
            // Fish UI�� �ְ� ���� ����
            UIManager.Instance.fishUI.UpdateHighScore();

            // Fish UIâ �����ش�
            UIManager.Instance.fishUI.gameObject.SetActive(true);
            UIManager.Instance.fishUI.enterMini_1.gameObject.SetActive(true);

            UIManager.Instance.isFishTriggered = true;  // update���� Ȯ�� �� ���� �ٲ۴�
        }
    }
}
