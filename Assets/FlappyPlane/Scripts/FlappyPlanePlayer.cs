using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

public class FlappyPlanePlayer : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float flapForce = 6f;    // ������
    public float fowrardSpeed = 3f; // �����̵�
    public bool isDead = false;
    float deathCooldown = 0f;   // �浹�ϰ� ���� �ð� �Ŀ� �״´�

    bool isFlap = false;    // ������ �پ����� �ȶپ�����

    public bool godMode = false;    // �׽�Ʈ ���

    GameManager gameManager;

    // UI�Ŵ���
    public FlappyUIManager flappyUiManager;
    public FlappyUIManager FlappyUIManager { get { return flappyUiManager; } }    // UIManager �� �ܺη� ������ �� �ִ� ������Ƽ


    private void Awake()
    {
        Debug.Log("FlappyPlanePlayer.Awake");
        // UIManager�� �̱��� ��ü�� �ƴϹǷ� FindObjectsOfType���� ã�´�
        flappyUiManager = FindObjectOfType<FlappyUIManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("FlappyPlanePlayer.Start");

        gameManager = GameManager.Instance; // Awake���� ȣ���ϸ� ���� �� ������ Start���� ȣ��

        animator = GetComponentInChildren<Animator>();    // �ڽ��� Model�� ������ �ִ�
        _rigidbody = GetComponent<Rigidbody2D>();
        if (animator == null)
        {
            Debug.LogError("Not Founded animator");
        }
        if (_rigidbody == null)
        {
            Debug.LogError("Not Founded Rigidbody");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("FlappyPlanePlayer.Update");

        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                // ���⼭ UI ����
                gameManager.FlappyUIManagerInstance.titleText.gameObject.SetActive(true);
                // ����â ���� �Ͻ�����
                gameManager.FlappyUIManagerInstance.descriptionText.gameObject.SetActive(true);
                gameManager.FlappyUIManagerInstance.restartText.gameObject.SetActive(true);


                // ���� �����
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gameManager.RestartGame();

                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameManager.StopGame();
                    // Fish UI�� ��� ����ش�
                    // data�Ŵ������� 10�� �̻��̸� ���� ���
                    //UIManager.Instance.fishUI.successText.text = DataManager.Instance.thisFlappyScore >= 10 ? "Success" : "Fail";
                    //UIManager.Instance.fishUI.enterMini_1.gameObject.SetActive(false);
                    //UIManager.Instance.fishUI.exitMini_1.gameObject.SetActive(true);    // ���â �����ش�
                }
            }
            else
            {
                // deathCooldown�� �����ִٸ� �� ������ ������ �ð���ŭ ����
                deathCooldown -= Time.deltaTime;    // Time.deltaTime: ���� �������� Update�� ���� �������� Update ������ �ð� ����
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;
            }
        }
    }
    // ���� ó��
    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _rigidbody.velocity; // rigidbody�� ���������� �ް� �ִ� ��

        velocity.x = fowrardSpeed;

        if (isFlap)
        {
            velocity.y += flapForce;
            // Vector3�� ����ü�� ���� velocity�� ����Ǿ��� ��, _rigidbody.velocity�� ���� �ٲ�� ���� �ƴϴ�
            isFlap = false;
        }
        _rigidbody.velocity = velocity;
        float angle = Mathf.Clamp((_rigidbody.velocity.y) * 10f, -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return;        // �׽�Ʈ ���
        if (isDead) return;

        isDead = true;
        deathCooldown = 1f; // 1�� �� ����� ����
        animator.SetInteger("IsDie", 1);

        gameManager.GameOver();
    }
}
