using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// top, bottom을 가지고 있는 1쌍의 장애물에 붙는 스크립트
public class Obstacle : MonoBehaviour
{
    // 장애물의 상하이동
    public float highPosY = 1f;
    public float lowPosY = -1f;
    // top과 bottom 사이의 거리
    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    public Transform topObject;
    public Transform bottomObject;

    // 오브젝트 사이의 폭(width)
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

        // top과 bottom을 합계 holeSize만큼 떨어뜨려놓는다
        topObject.localPosition = new Vector3(0, halfHoleSize); // top은 holeSize의 반만큼 위로 올린다
        bottomObject.localPosition = new Vector3(0, -halfHoleSize); // bottom은 holeSize의 반만큼 아래로 내린다

        // local: 부모(obstacles) 기준으로 top과 bottom을 배치한다
        Vector3 placePosition = lastPos + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);

        // 월드 좌표로 obstacle을 이동
        transform.position = placePosition;

        return placePosition;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Collider에 충돌한 것이 player라면 점수 추가
        FlappyPlanePlayer player = collision.GetComponent<FlappyPlanePlayer>();
        if (player != null)
        {
            gameManager.AddScore(1);
        }
    }
}
