using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    public MainGameManager mainGameManager;

    private Camera camera;
    private Animator animator;

    // ������ȯ�� ���� ����
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
        // npc�� ��ȣ�ۿ� �߿��� ������ �� ����
        // float�� ����������... false ��� 0f�� �����ؼ� �������̰�����
        float horizontal = MainGameManager.Instance.isAction ? 0f : Input.GetAxisRaw("Horizontal");
        float vertical = MainGameManager.Instance.isAction ? 0f : Input.GetAxisRaw("Vertical");

        // isometric ó��
        Vector2 isometricMovementDirection = new Vector2(
            (horizontal + vertical),    // x�� ��ȯ (��ǥ�� ȸ�� ����)
            (vertical - horizontal) / 2 // y�� ��ȯ (���̴� 1/2�� �ݿ�)
        ).normalized;
        movementDirection = isometricMovementDirection;

        // Idle �Ǵ� Move ���� ����
        SetDirection(movementDirection);
    }

    // Idle / Move ���� ���� �Լ�
    void SetDirection(Vector2 movement)
    {
        int direction = 0;
        string animationState = "IdleDirection"; /// �⺻������ Idle ���¸� ���

        // Bow, Sword ���� ���⼭ �߰��Ѵ�
        if (movement != Vector2.zero)
        {
            // �̵� ���� ���� ������ ���� ����
            if (movement.x >= 0 && movement.y < 0) // ����
                direction = 1;
            else if (movement.x >= 0 && movement.y >= 0) // ���
                direction = 2;
            else if (movement.x < 0 && movement.y >= 0) // �»�
                direction = 3;
            else // ����
                direction = 0;

            // ���� ���� �ٲٰ�
            animator.SetInteger(animationState, direction);

            // "IsMove" ���´� ���� �ð� �Ŀ� ����
            if (!isChangingDirection)
            {
                StartCoroutine(ChangeMoveStateAfterDelay());
            }

            //// �������� ���� �� Move ���� ����
            //animator.SetBool("IsMove", true);  /// ������ ���� Move ���� ���
            //Debug.Log($"movement.x: {movement.x}, movement.y: {movement.y} ");
            // ���� �̵� ���� ����
            //lastMovementDirection = movement;
        }
        else
        {
            // �������� ���� �� ���⿡ ���� Idle ���� ����
            animator.SetBool("IsMove", false); /// ���߸� "IsMove"�� false�� ����

            ///// lookDirection �� ���콺 ��ġ�� ���Ͽ� �Ǵ��Ѵ�
            ///// movementDirection �� ��� ���� �����̴� �������� �Ǵ�
            //// ���� ������ �� ������ �̵� ���� ���
            //if (lastMovementDirection.x >= 0 && lastMovementDirection.y < 0) // ����
            //    direction = 1;
            //else if (lastMovementDirection.x >= 0 && lastMovementDirection.y >= 0) // ���
            //    direction = 2;
            //else if (lastMovementDirection.x < 0 && lastMovementDirection.y >= 0) // �»�
            //    direction = 3;
            //else // ����
            //    direction = 0;            // Direction�� 0�� ��� Idle�� ó��
        }
        animator.SetInteger(animationState, direction);
    }

    IEnumerator ChangeMoveStateAfterDelay()
    {
        isChangingDirection = true;

        // ���� ���� �� ��� ���
        yield return new WaitForSeconds(0.001f); // ��: 0.2�� �Ŀ� "IsMove" ����

        // �̵� ���� ����
        animator.SetBool("IsMove", true);

        isChangingDirection = false;
    }
}
