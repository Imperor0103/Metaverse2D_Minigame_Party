using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// top, bottom�� ������ �ִ� 1���� ��ֹ��� �ٴ� ��ũ��Ʈ
public class Obstacle : MonoBehaviour
{
    // ��ֹ��� �����̵�
    public float highPosY = 1f;
    public float lowPosY = -1f;
    // top�� bottom ������ �Ÿ�
    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    public Transform topObject;
    public Transform bottomObject;

    // ������Ʈ ������ ��(width)
    public float widthPadding = 4f;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public Vector3 SetRandomPlace(Vector3 lastPos, int obsCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2;

        // top�� bottom�� �հ� holeSize��ŭ ����߷����´�
        topObject.localPosition = new Vector3(0, halfHoleSize); // top�� holeSize�� �ݸ�ŭ ���� �ø���
        bottomObject.localPosition = new Vector3(0, -halfHoleSize); // bottom�� holeSize�� �ݸ�ŭ �Ʒ��� ������

        // local: �θ�(obstacles) �������� top�� bottom�� ��ġ�Ѵ�
        Vector3 placePosition = lastPos + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);

        // ���� ��ǥ�� obstacle�� �̵�
        transform.position = placePosition;

        return placePosition;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Collider�� �浹�� ���� player��� ���� �߰�
        FlappyPlanePlayer player = collision.GetComponent<FlappyPlanePlayer>();
        if (player != null)
        {
            gameManager.AddScore(1);
        }
    }
}
