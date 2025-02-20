using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어를 따라다니는 카메라
public class FollowCamera : MonoBehaviour
{
    public Transform target;    // 카메라가 추적할 대상(player)
    float offsetX;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null) return;

        offsetX = transform.position.x - target.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        Vector3 pos = transform.position;
        pos.x = target.position.x + offsetX;
        transform.position = pos;
    }
}
