using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ī�޶��� �ڽ����� ī�޶�� ���� �Ÿ��� �ΰ� ����ٴϸ�
// �浹�� ��ֹ��� ���� �� ������ ������ ��Ȱ��
public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5;  // ����� 5���� background sprite�� �������
    public int obstacleCount = 0;   // ��ֹ� ����
    public Vector3 obstacleLastPosition = Vector3.zero;


    // ��ֹ��� ��� ã�Ƽ�, ȭ�鿡 ��ġ���� �Ѵ�
    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        obstacleLastPosition = obstacles[0].transform.position; // ã�� �͵� �� ù��°
        obstacleCount = obstacles.Length;

        // ��� ��ֹ��� ��ġ
        for (int i = 0; i < obstacleCount; i++)
        {
            // obstacles[i]�� ��ġ�ϰ�, �� ��ǥ(obstacleLastPosition)�� �����ͼ� ���� ��ֹ��� ��ġ�Ѵ�
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    // trigger: �浹�� ���� �뺸�� ���ش�
    // trigger�浹���� Collider�� �浹�� ���� ������ �� �� ����, �ε��� �浹ü�� ���� ������ �� �� �ִ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered:" + collision.name);
        // �浹ü�� ���(background)���� Ȯ��
        if (collision.CompareTag("Background"))
        {
            // �浹ü�� width�� ������ �ִ�
            /// Collider2D�� ��� collider���� �θ� Ŭ������, �� Ŭ������ ������ �ϴ� ���� �ƴϱ� ������ BoxCollider�� size�� ������ �� ����
            /// BoxCollider2D�� ����ȯ�Ѵ�
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position; // �浹ü�� ��ġ�� �����ͼ�

            pos.x += widthOfBgObject * numBgCount;  // ��� 5����ŭ �ڷ� ������
            collision.transform.position = pos;
            return;
        }

        // �浹ü�� ��ֹ����� Ȯ��
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        // �浹ü�� Obstacle�� ������ �ִٸ� 
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);    // �浹�� ��ü�� ��ġ�� �ٲ۴�
        }
    }
}
