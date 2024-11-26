using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float moveSpeed = 5f; // 移動速度
    [SerializeField] private float jumpForce = 5f; // ジャンプ力
    [SerializeField] private float rotationSpeed = 10f;  // 回転速度
    [SerializeField] private Transform GroundCheck; // 接地判定に使う空のゲームオブジェクト
    [SerializeField] private LayerMask GroundLayer; // 接地判定に使うレイヤー
    [SerializeField] private float GroundCheckRadius = 0.2f; // 接地判定の半径

    [Header("プレイヤーの状態")]
    [SerializeField] private Vector3 moveDirection; // プレイヤーの移動方向
    [SerializeField] private bool isGrounded; // 接地中
    [SerializeField] private bool canHide; // 「隠れる」可能
    [SerializeField] private bool isHiding; // 「隠れる」中

    // スクリプトの処理用
    private Rigidbody playerRigidbody; // プレイヤーのRigidbody
    private Vector2 moveInput; // コントローラーの入力方向
    private bool jumpInProgress; // ジャンプ処理中

    // 変数のプロパティ
    public float MoveSpeed
    {
        get => moveSpeed; // moveSpeedを参照する

        private set
        { 
            moveSpeed = value; // moveSpeedを変更する
        }
    }
    public float JumpForce
    {
        get => jumpForce; // jumpForceを参照する

        private set
        {
            jumpForce = value; // jumpForceを変更する
        }
    }
    public bool IsHiding
    {
        get => isHiding; // isHidingを参照する

        private set
        {
            isHiding = value; // isHidingを変更する

            // 状態遷移時の処理
            HandleHidingStateChanged();
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();

        GroundCheck = GameObject.Find("GroundCheck").transform; // GroundCheckを取得

        GroundLayer = LayerMask.GetMask("Ground"); // Groundレイヤーを取得
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //----------------------------------------------
        // 隠れている間も行われる処理
        //----------------------------------------------

        // 接地判定
        CheckGrounded();

        if (IsHiding)
        {
            return;
        }

        //----------------------------------------------
        // 隠れている間は行われない処理
        //----------------------------------------------

        // 移動処理
        HandleMovement();

        // ジャンプ処理
        if (isGrounded && jumpInProgress) // 接地中かつジャンプ入力が成功している場合
        {
            JumpPlayer();
        }
    }

    //----------------------------------------------
    // 各種処理
    //----------------------------------------------

    // 移動処理
    private void HandleMovement()
    {
        playerRigidbody.velocity = new Vector3(moveDirection.x * moveSpeed, playerRigidbody.velocity.y, moveDirection.z * moveSpeed); // 移動速度を現在速度に加える

        // 0.1f以上の入力がある場合回転処理を行う
        if (moveDirection.magnitude > 0.1f)
        {
            RotatePlayer(moveDirection);
        }
    }

    // 回転処理
    private void RotatePlayer(Vector3 direction)
    {
        // スティックの入力方向を向く
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // スムーズに回転させる
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    // ジャンプ処理
    private void JumpPlayer()
    {
        // ジャンプ力をY方向速度に入れる
        playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpForce, playerRigidbody.velocity.z);

        // ジャンプ処理中を解除
        jumpInProgress = false;

        Debug.Log("Jump");
    }

    // 「隠れる」状態が変化したときの処理
    private void HandleHidingStateChanged()
    {
        // TODO: 未実装
        // テスト用にデバッグログ表示
        Debug.Log(isHiding ? "isHiding: false -> true" : "isHiding: true -> false");
    }

    //----------------------------------------------
    // 各種判定
    //----------------------------------------------

    // 接地判定
    private void CheckGrounded()
    {
        // 足元のゲームオブジェクトがGroundLayerに接しているか判定する
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundCheckRadius, GroundLayer);
    }

    //「隠れる」可能オブジェクトの範囲内判定
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hideable"))
        {
            canHide = true;
        }
    }
    //「隠れる」可能オブジェクトの範囲外判定
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hideable"))
        {
            canHide = false;
        }
    }

    //----------------------------------------------
    // 入力イベント
    //----------------------------------------------

    // 入力イベント：移動
    public void OnMove(InputAction.CallbackContext context)
    {
        // 入力された移動ベクトルを取得
        moveInput = context.ReadValue<Vector2>();

        // 移動ベクトルを3D方向に変換
        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
    }

    // 入力イベント：ジャンプ
    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Input:Jump");

        if (IsHiding || !isGrounded)
        {
            // ジャンプができる条件を満たしてないので処理を行わない
            return;
        }

        if (context.started)
        {
            // ジャンプ処理中にする
            jumpInProgress = true;
        }
    }

    // 入力イベント：隠れる
    public void OnHide(InputAction.CallbackContext context)
    {
        Debug.Log("Input:Hide");

        if (context.started && IsHiding)
        {
            // 隠れるを解除
            IsHiding = false;
        }
        else if (context.started && canHide)
        {
            // 隠れる
            IsHiding = true;

            // 移動を停止
            playerRigidbody.velocity = Vector3.zero;
        }
    }
}
