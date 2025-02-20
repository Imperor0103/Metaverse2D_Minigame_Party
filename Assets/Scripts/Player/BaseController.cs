using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    // [SerializeField]�� ���� private�� �ν����Ϳ� ������ �� �ִ�
    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    /// �»�, ���� ������ ���� ��������Ʈ�� ���� �����صξ���Ѵ�
    [SerializeField] private Sprite defaultSprite; // �⺻ ��������Ʈ (����)
    [SerializeField] private Sprite upSprite;      // ������ �� �� ����� ��������Ʈ (�»�)

    protected Vector2 movementDirection = Vector2.zero; // �̵�����
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero; // �ٶ󺸴� ����
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
        HandleAction(); // �Է�ó��, �̵��� �ʿ��� ������ ó��
                        //Rotate(lookDirection);  // ȸ���� ���� "Bow" �����Ҷ���


        // ���� �÷��̾�� ������Ʈ�� ���� ���� ������.. Ʈ���� üũ�� ���⼭ �Ѵ�
        // �����, �ܹ��� �߿� ����������� Ȯ���ؾ��Ѵ�
        if (UIManager.Instance.isFishTriggered && Input.anyKeyDown)
        {
            UIManager.Instance.isFishTriggered = false; // �������ְ� �� ��ü
            SceneManager.LoadScene("FlappyPlane");
            /// ���� ������ ���� �Ʒ��� �����Ѵ�
            Debug.Log("BaseController");
            UIManager.Instance.fishUI.gameObject.SetActive(false); // ���� �ִ� UI�� �ݰ� �Ѿ���Ѵ�
        }
        else if (UIManager.Instance.isHamburgerTriggered && Input.anyKeyDown)
        {
            UIManager.Instance.isHamburgerTriggered = false; // �������ְ� �� ��ü
            SceneManager.LoadScene("TheStack");
            /// ���� ������ ���� �Ʒ��� �����Ѵ�
            Debug.Log("BaseController");
            UIManager.Instance.hamburgerUI.gameObject.SetActive(false);// ���� �ִ� UI�� �ݰ� �Ѿ���Ѵ�
        }
    }

    protected virtual void FixedUpdate()
    {
        Movment(movementDirection); // �̵�
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
        /// ���ݻ�Ȳ������ ���콺�� ȸ���� �� �ְ� �ؾ��Ѵ�

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLookingUp = (rotZ > 0) && (rotZ < 180);  // ����(1,2��и�)�̸� true
        //Debug.Log($"rotZ: {rotZ}, isUp: {isLookingUp}");

        bool isLookingRight = Mathf.Abs(rotZ) < 90f;  // 1,3��и��̸� true (������)

        // ������ �ٶ󺸸� upSprite ���, �Ʒ����̸� �⺻ ��������Ʈ ���
        characterRenderer.sprite = isLookingUp ? upSprite : defaultSprite;
        characterRenderer.flipX = isLookingRight;   // �¿� ����

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
        // ������ �E���̴� A-B: B�� A�� �ٶ󺸴� ����
    }
}
