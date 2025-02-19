using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // ���ڿ� �񱳺��ٴ� ���ں񱳰� ���� ������ String�� Hash�� ��ȯ�Ѵ�
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int IsUp = Animator.StringToHash("IsUp");
    private static readonly int IsBow = Animator.StringToHash("IsBow");
    private static readonly int IsSword = Animator.StringToHash("IsSword");

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);    // ���� obj�� ũ�Ⱑ 0.5 �̻��̸� IsMoving
    }

    public void LookUp()
    {
        animator.SetBool(IsUp, true);
    }
}
