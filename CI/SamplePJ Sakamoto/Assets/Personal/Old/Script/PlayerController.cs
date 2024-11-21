using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 5f; // 移動速度
    public float jumpForce = 5f; // ジャンプ力
    public float rotationSpeed = 10f;  // 回転速度
    [SerializeField] private Transform groundCheck; // 接地判定に使う空のゲームオブジェクト
    [SerializeField] private LayerMask groundLayer; // 接地判定に使うレイヤー
    [SerializeField] private float groundCheckRadius = 0.2f; // 接地判定の半径

    private Rigidbody rb; // プレイヤーのRigidbody
    private Vector2 moveInput; // プレイヤーの移動方向
    private Vector3 moveDirection; // キャラクターの移動方向
    private bool isJumping; // ジャンプ中
    private bool isGrounded; // 接地中
 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 接地判定
        CheckGrounded();
    }

    private void FixedUpdate()
    {
        // 移動処理
        MovePlayer();

        // 回転処理
        if (moveDirection.magnitude > 0.1f) // 入力がある場合
        {
            RotatePlayer();
        }

        // ジャンプ処理
        if (isGrounded && isJumping) //接地中かつジャンプ入力が成功している場合
        {
            JumpPlayer();
        }
    }

    // 移動処理
    private void MovePlayer()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y); // 入力に基づいて移動方向を設定
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
    }

    // 回転処理
    private void RotatePlayer()
    { 
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up); // スティックの入力方向を向く
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime); // スムーズに回転
    }

    //ジャンプ処理
    private void JumpPlayer()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); // ジャンプ力をY方向に加える
        isJumping = false; // ジャンプ中を解除
        Debug.Log("Jump");
    }

    //接地判定
    private void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer); //足元のゲームオブジェクトがgroundLayerに接しているか判定する
    }


    // 入力イベント：移動
    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); // 入力された移動ベクトルを取得
        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y); // 移動ベクトルを3D方向に変換
        Debug.Log("Move Input: " + moveInput + " Direction:" + moveDirection);
    }

    // 入力イベント：ジャンプ
    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded) // ボタンが押された瞬間に接地中であればジャンプ入力成功
        {
            isJumping = true; // ジャンプ中にする
            Debug.Log("Input:Jump");
        }
    }
}
