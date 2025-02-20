using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    // [SerializeField]를 통해 private도 인스펙터에 공개할 수 있다
    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    /// 좌상, 좌하 방향을 보는 스프라이트는 따로 저장해두어야한다
    [SerializeField] private Sprite defaultSprite; // 기본 스프라이트 (좌하)
    [SerializeField] private Sprite upSprite;      // 위쪽을 볼 때 사용할 스프라이트 (좌상)

    protected Vector2 movementDirection = Vector2.zero; // 이동방향
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero; // 바라보는 방향
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction(); // 입력처리, 이동에 필요한 데이터 처리
                        //Rotate(lookDirection);  // 회전은 오직 "Bow" 공격할때만


        // 메인 플레이어와 업데이트를 같이 쓰기 때문에.. 트리거 체크를 여기서 한다
        // 물고기, 햄버거 중에 어느것인지도 확인해야한다
        if (UIManager.Instance.isFishTriggered && Input.anyKeyDown)
        {
            UIManager.Instance.isFishTriggered = false; // 해제해주고 씬 교체
            SceneManager.LoadScene("FlappyPlane");
            /// 씬에 들어오기 전에 아래를 실행한다
            Debug.Log("BaseController");
            UIManager.Instance.fishUI.gameObject.SetActive(false); // 열려 있는 UI는 닫고 넘어가야한다
        }
        else if (UIManager.Instance.isHamburgerTriggered && Input.anyKeyDown)
        {
            UIManager.Instance.isHamburgerTriggered = false; // 해제해주고 씬 교체
            SceneManager.LoadScene("TheStack");
            /// 씬에 들어오기 전에 아래를 실행한다
            Debug.Log("BaseController");
            UIManager.Instance.hamburgerUI.gameObject.SetActive(false);// 열려 있는 UI는 닫고 넘어가야한다
        }
    }

    protected virtual void FixedUpdate()
    {
        Movment(movementDirection); // 이동
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleAction()
    {

    }

    protected virtual void Movment(Vector2 direction)
    {
        direction = direction * 5;
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }

        _rigidbody.velocity = direction;
    }

    private void Rotate(Vector2 direction)
    {
        /// 공격상황에서만 마우스가 회전할 수 있게 해야한다

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLookingUp = (rotZ > 0) && (rotZ < 180);  // 위쪽(1,2사분면)이면 true
        //Debug.Log($"rotZ: {rotZ}, isUp: {isLookingUp}");

        bool isLookingRight = Mathf.Abs(rotZ) < 90f;  // 1,3사분면이면 true (오른쪽)

        // 위쪽을 바라보면 upSprite 사용, 아래쪽이면 기본 스프라이트 사용
        characterRenderer.sprite = isLookingUp ? upSprite : defaultSprite;
        characterRenderer.flipX = isLookingRight;   // 좌우 반전

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
        // 벡터의 뺼셈이다 A-B: B가 A를 바라보는 벡터
    }
}
