using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라의 자식으로 카메라와 일정 거리를 두고 따라다니며
// 충돌한 장애물을 맵의 맨 끝으로 보내서 재활용
public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5;  // 배경은 5개의 background sprite로 만들었다
    public int obstacleCount = 0;   // 장애물 개수
    public Vector3 obstacleLastPosition = Vector3.zero;


    // 장애물을 모두 찾아서, 화면에 배치까지 한다
    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        obstacleLastPosition = obstacles[0].transform.position; // 찾은 것들 중 첫번째
        obstacleCount = obstacles.Length;

        // 모든 장애물을 배치
        for (int i = 0; i < obstacleCount; i++)
        {
            // obstacles[i]를 베치하고, 그 좌표(obstacleLastPosition)를 가져와서 다음 장애물을 배치한다
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    // trigger: 충돌에 대한 통보만 해준다
    // trigger충돌에서 Collider는 충돌에 대한 정보는 줄 수 없고, 부딪힌 충돌체에 대한 정보만 줄 수 있다
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered:" + collision.name);
        // 충돌체가 배경(background)인지 확인
        if (collision.CompareTag("Background"))
        {
            // 충돌체가 width를 가지고 있다
            /// Collider2D는 모든 collider들의 부모 클래스라서, 그 클래스에 접근을 하는 것이 아니기 때문에 BoxCollider의 size를 가져올 수 없다
            /// BoxCollider2D로 형변환한다
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position; // 충돌체의 위치를 가져와서

            pos.x += widthOfBgObject * numBgCount;  // 배경 5개만큼 뒤로 보낸다
            collision.transform.position = pos;
            return;
        }

        // 충돌체가 장애물인지 확인
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        // 충돌체가 Obstacle을 가지고 있다면 
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);    // 충돌한 물체는 위치를 바꾼다
        }
    }
}
