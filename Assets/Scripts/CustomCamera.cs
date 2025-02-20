using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// 플레이어를 따라가며, 벽을 넘어갈 수 없다
public class CustomCamera : MonoBehaviour
{
    public Transform target;    // 카메라가 추적할 대상(player)

    public float threshold = 10.0f;  //오차 범위

    private Vector2 offset; // isometric 기준 오프셋 저장

    public Tilemap tilemap;     // 카메라의 Bounds를 설정하기 위해 타일맵의 크기가 필요하다


    // Start is called before the first frame update
    void Start()
    {
        //tilemap = GameObject.Find("Plain").GetComponentIn;

        // 참조가 끊어질 것이므로, 직접 연결한다
        target = GameObject.Find("MainPlayer")?.transform;

        if (target == null) return;

        // 초기 오프셋을 이소메트릭 좌표 기준으로 저장
        offset.x = threshold;
        offset.y = threshold / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        // 이소메트릭 좌표 변환
        // 굳이 그럴필요있나? 플레이어의 좌표에서 계산되어 나오는데
        // 플레이어로부터 떨어진 거리가 x는 offset.x보다 작거나 같아야하고
        // 
        float isometricX = target.position.x - target.position.y;
        float isometricY = (target.position.x + target.position.y) * 0.5f;


        Vector2 currentPos = transform.position;


        // 이동 제한



        Vector3 pos = transform.position;
        // 목표 위치 계산
        //pos.x = target.position.x + offsetX;
        transform.position = pos;
    }
}
