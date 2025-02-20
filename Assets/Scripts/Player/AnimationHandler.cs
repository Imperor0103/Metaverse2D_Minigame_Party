using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // 문자열 비교보다는 숫자비교가 좋기 때문에 String을 Hash로 변환한다
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
        animator.SetBool(IsMoving, obj.magnitude > .5f);    // 벡터 obj의 크기가 0.5 이상이면 IsMoving
    }

    public void LookUp()
    {
        animator.SetBool(IsUp, true);
    }
}
