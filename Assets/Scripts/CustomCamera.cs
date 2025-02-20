using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// 플레이어를 따라가며, (-20,0), (0,-10), (20,0), (0,10)을 정점으로 하는 마름모 내부에서만 플레이어 따라간다
public class CustomCamera : MonoBehaviour
{
    public Transform target;    // 카메라가 추적할 대상(player)
    public float followDistance = 2.0f;  //오차 범위
    public float followSpeed = 5.0f;

    public Vector2 center = Vector2.zero; // 마름모 중심
    public float width = 20f; // 가로 거리 (좌우 범위)
    public float height = 10f; // 세로 거리 (상하 범위)

    // Start is called before the first frame update
    void Start()
    {
        // 참조가 끊어질 것이므로, 직접 연결한다
        target = GameObject.Find("MainPlayer")?.transform;

        if (target == null) return;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPos = target.position; // 플레이어 위치

        Vector3 cameraPos = transform.position; // 현재 카메라 위치 

        // 카메라가 플레이어를 따라가되, 최대 followDistance이내에서만 이동

        float distance = Vector3.Distance(cameraPos, targetPos);
        if (distance > followDistance)
        {
            // 거리가 followDistance를 초과하면 천천히 따라감
            cameraPos = Vector2.Lerp(cameraPos, targetPos, followSpeed * Time.deltaTime);
        }
        // 마름모 내부인지 체크하는 함수 호출(clamp: 최소, 최대 사이로 값을 제한한다)
        Vector3 clampedPos = ClampToDiamond(cameraPos, center, width, height);
        // Z 좌표를 -10으로 유지
        clampedPos.z = -10f;

        transform.position = clampedPos;
    }

    Vector2 ClampToDiamond(Vector2 position, Vector2 center, float width, float height)
    {
        float dx = Mathf.Abs(position.x - center.x); // X 거리 변화량
        float dy = Mathf.Abs(position.y - center.y); // Y 거리

        // 마름모 내부라면 그대로 반환
        if (dx / width + dy / height <= 1)  /// x절편, y절편 지나는 직선의 방정식으로 둘러싸인 내부
            return new Vector3(position.x, position.y, -10f); // Z값은 -10f로 고정

        // 만약 마름모 외부라면
        // 마름모 경계에 위치를 맞춤
        float clampedX = Mathf.Clamp(position.x, center.x - width, center.x + width);
        float clampedY = Mathf.Clamp(position.y, center.y - height, center.y + height);

        // 클램프한 위치가 마름모 내부에 있는지 다시 확인
        float newDx = Mathf.Abs(clampedX - center.x);
        float newDy = Mathf.Abs(clampedY - center.y);
        if (newDx / width + newDy / height > 1)
        {
            // 경계를 벗어난다면 가장 가까운 마름모 경계로 이동
            float scaleFactor = 1f / (newDx / width + newDy / height);
            clampedX = center.x + (clampedX - center.x) * scaleFactor;
            clampedY = center.y + (clampedY - center.y) * scaleFactor;
        }

        // Z값은 -10f로 고정
        return new Vector3(clampedX, clampedY, -10f);
    }
}