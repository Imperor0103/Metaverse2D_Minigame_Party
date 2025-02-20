using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// �÷��̾ ���󰡸�, ���� �Ѿ �� ����
public class CustomCamera : MonoBehaviour
{
    public Transform target;    // ī�޶� ������ ���(player)

    public float threshold = 10.0f;  //���� ����

    private Vector2 offset; // isometric ���� ������ ����

    public Tilemap tilemap;     // ī�޶��� Bounds�� �����ϱ� ���� Ÿ�ϸ��� ũ�Ⱑ �ʿ��ϴ�


    // Start is called before the first frame update
    void Start()
    {
        //tilemap = GameObject.Find("Plain").GetComponentIn;

        // ������ ������ ���̹Ƿ�, ���� �����Ѵ�
        target = GameObject.Find("MainPlayer")?.transform;

        if (target == null) return;

        // �ʱ� �������� �̼Ҹ�Ʈ�� ��ǥ �������� ����
        offset.x = threshold;
        offset.y = threshold / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        // �̼Ҹ�Ʈ�� ��ǥ ��ȯ
        // ���� �׷��ʿ��ֳ�? �÷��̾��� ��ǥ���� ���Ǿ� �����µ�
        // �÷��̾�κ��� ������ �Ÿ��� x�� offset.x���� �۰ų� ���ƾ��ϰ�
        // 
        float isometricX = target.position.x - target.position.y;
        float isometricY = (target.position.x + target.position.y) * 0.5f;


        Vector2 currentPos = transform.position;


        // �̵� ����



        Vector3 pos = transform.position;
        // ��ǥ ��ġ ���
        //pos.x = target.position.x + offsetX;
        transform.position = pos;
    }
}
