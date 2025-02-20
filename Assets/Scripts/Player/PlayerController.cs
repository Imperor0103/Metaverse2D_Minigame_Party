using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    public MainGameManager mainGameManager;

    private Camera camera;
    private Animator animator;

    // 방향전환을 위해 선언
    private Vector2 lastMovementDirection = Vector2.zero;
    private bool isChangingDirection = false;


    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
        animator = GetComponentInChildren<Animator>();
    }

    protected override void HandleAction()
    {
        // npc와 상호작용 중에는 움직일 수 없다
        // float로 선언했으니... false 대신 0f를 대입해서 못움직이게하자
        float horizontal = MainGameManager.Instance.isAction ? 0f : Input.GetAxisRaw("Horizontal");
        float vertical = MainGameManager.Instance.isAction ? 0f : Input.GetAxisRaw("Vertical");

        // isometric 처리
        Vector2 isometricMovementDirection = new Vector2(
            (horizontal + vertical),    // x축 변환 (좌표계 회전 적용)
            (vertical - horizontal) / 2 // y축 변환 (높이는 1/2로 반영)
        ).normalized;
        movementDirection = isometricMovementDirection;

        // Idle 또는 Move 방향 설정
        SetDirection(movementDirection);
    }

    // Idle / Move 방향 설정 함수
    void SetDirection(Vector2 movement)
    {
        int direction = 0;
        string animationState = "IdleDirection"; /// 기본적으로 Idle 상태를 사용

        // Bow, Sword 또한 여기서 추가한다
        if (movement != Vector2.zero)
        {
            // 이동 중일 때도 방향을 먼저 설정
            if (movement.x >= 0 && movement.y < 0) // 우하
                direction = 1;
            else if (movement.x >= 0 && movement.y >= 0) // 우상
                direction = 2;
            else if (movement.x < 0 && movement.y >= 0) // 좌상
                direction = 3;
            else // 좌하
                direction = 0;

            // 방향 먼저 바꾸고
            animator.SetInteger(animationState, direction);

            // "IsMove" 상태는 일정 시간 후에 설정
            if (!isChangingDirection)
            {
                StartCoroutine(ChangeMoveStateAfterDelay());
            }

            //// 움직임이 있을 때 Move 방향 설정
            //animator.SetBool("IsMove", true);  /// 움직일 때는 Move 상태 사용
            //Debug.Log($"movement.x: {movement.x}, movement.y: {movement.y} ");
            // 현재 이동 방향 저장
            //lastMovementDirection = movement;
        }
        else
        {
            // 움직임이 없을 때 방향에 따라 Idle 방향 설정
            animator.SetBool("IsMove", false); /// 멈추면 "IsMove"를 false로 설정

            ///// lookDirection 은 마우스 위치와 비교하여 판단한다
            ///// movementDirection 을 써야 실제 움직이는 방향으로 판단
            //// 정지 상태일 때 마지막 이동 방향 사용
            //if (lastMovementDirection.x >= 0 && lastMovementDirection.y < 0) // 우하
            //    direction = 1;
            //else if (lastMovementDirection.x >= 0 && lastMovementDirection.y >= 0) // 우상
            //    direction = 2;
            //else if (lastMovementDirection.x < 0 && lastMovementDirection.y >= 0) // 좌상
            //    direction = 3;
            //else // 좌하
            //    direction = 0;            // Direction이 0인 경우 Idle로 처리
        }
        animator.SetInteger(animationState, direction);
    }

    IEnumerator ChangeMoveStateAfterDelay()
    {
        isChangingDirection = true;

        // 방향 변경 후 잠시 대기
        yield return new WaitForSeconds(0.001f); // 예: 0.2초 후에 "IsMove" 설정

        // 이동 상태 설정
        animator.SetBool("IsMove", true);

        isChangingDirection = false;
    }
}
