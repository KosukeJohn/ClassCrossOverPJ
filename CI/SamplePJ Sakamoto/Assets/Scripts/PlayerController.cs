
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float moveSpeed = 5f; // 移動速度
    [SerializeField] private float rotationSpeed = 4f;  // 回転速度

    [Header("ジャンプ設定")]
    [SerializeField] private float jumpForce = 7f; // 初期ジャンプ力
    [SerializeField] private float maxJumpTime = 0.5f; // ジャンプ力を加算する最大時間
    [SerializeField] private float jumpBoostForce = 15f; // ボタンを押し続けによって追加できるジャンプ力
    [SerializeField] private Transform GroundCheck; // 接地判定に使う空のゲームオブジェクト
    [SerializeField] private LayerMask GroundLayer; // 接地判定に使うレイヤー
    [SerializeField] private float GroundCheckRadius = 0.3f; // 接地判定の半径
    [SerializeField] private float coyoteTime = 0.04f; // コヨーテタイム（地面から離れたときにジャンプ可能な時間を補正）

    [Header("プレイヤーの状態")]
    [SerializeField] private Vector3 moveDirection; // プレイヤーの移動方向
    [SerializeField] private bool isGrounded; // 接地中
    [SerializeField] private float groundTimer; // コヨーテタイムをカウントするタイマー
    [SerializeField] private bool isJumping; // ジャンプ処理中
    [SerializeField] private bool jumpHeld; // ジャンプボタン押下中
    [SerializeField] private float jumpTimer; // ジャンプボタン押下時間
    [SerializeField] private bool canHide; // 「隠れる」可能
    [SerializeField] private bool isHiding; // 「隠れる」中

    // 変数の追加はここに記述してください
    [Header("追加変数")]
    [SerializeField] private bool hasKeyItem=false; // クリアに必要なアイテムを持っているか
    [SerializeField] private Vector3 respawnPosition = new Vector3(0,0,0); // respawnする場所を格納
    [SerializeField] private bool inSafeArea = false; // 時間内に目標の場所に入っているか
    [SerializeField] private float timeRemaining = 2f; // 目標の場所に入るまでの時間
    [SerializeField] AudioSource source;
    [SerializeField]AudioClip clip;
    public GameObject bossPrefab; // ボスのプレハブ
    private float time;
    bool flag;
    bool goal;
    GameObject Fade;
    float alfa;
    Image end;
    bool playedSE = false;
    float fadespeed;
    private bool isMoveCheck = false;

    //----------------------------------------------
    // スクリプトの処理用
    //----------------------------------------------

    private Rigidbody playerRigidbody; // プレイヤーのRigidbody
    private Vector2 moveInput; // コントローラーの入力方向
    private Animator playerAnimator; // プレイヤーのAnimator

    // 変数の追加はここに記述してください
    private GameObject hitcheck;
    
    //----------------------------------------------
    // 変数のプロパティ
    //----------------------------------------------

    public float MoveSpeed
    {
        // moveSpeedの参照
        get => moveSpeed;

        // moveSpeedの変更
        private set
        { 
            moveSpeed = value; 
        }
    }
    public float JumpForce
    {
        // jumpForceの参照
        get => jumpForce;

        // jumpForceの変更
        private set
        {
            jumpForce = value; 
        }
    }
    public bool IsHiding
    {
        // isHidingの参照
        get => isHiding;

        // isHidingの変更
        private set
        {
            isHiding = value; 

            // 状態遷移時の処理
            HandleHidingStateChanged();
        }
    }

    //----------------------------------------------
    // イベント関数
    //----------------------------------------------

    // オブジェクトがアクティブになった後、最初のフレームで一度だけ呼ばれる
    private void Start()
    {
        // PlayerのRigidBodyを取得
        playerRigidbody = GetComponent<Rigidbody>();

        // PlayerのAnimatorを取得
        playerAnimator = GetComponent<Animator>();

        // GroundCheckを取得
        GroundCheck = GameObject.Find("GroundCheck").transform;

        // Groundレイヤーを取得
        GroundLayer = LayerMask.GetMask("Ground");

        // 処理の追加はここに記述してください
        //1/7に追加したもの
        hitcheck = GameObject.Find("HitCheck");
        this.transform.position = GetComponent<PlayerFirstPos>().GetFirstPos();
        Fade = GameObject.Find("EndFade");
        end = Fade.GetComponent<Image>();
        //タイマー初期化
        time = 0.0f;
        source.clip = clip;
        flag = false;
        goal= false;
        fadespeed = 0.005f;
        alfa = end.color.a;
    }

    // 一定間隔で呼ばれる更新処理
    private void FixedUpdate()
    {
        //----------------------------------------------
        // プレイヤーが隠れている間も行われる処理
        //----------------------------------------------

        // 接地判定
        isGrounded = CheckGrounded();

        // コヨーテタイムの処理
        if (isGrounded)
        {
            groundTimer = coyoteTime;
        }
        else
        {
            groundTimer -= Time.fixedDeltaTime;
        }

        // 処理の追加はここに記述してください
        // pass

        //----------------------------------------------
        // プレイヤーが隠れている間は行われない処理
        //----------------------------------------------

        // プレイヤーが隠れている間の処理をここまでにする
        if (IsHiding)
        {
            return;
        }

        // 移動処理
        HandleMovement();

        // ジャンプ処理
        // ボタンを押し続けている間の処理
        if (jumpHeld && isJumping) 
        {
            AddJumpForce();
        }

        // 処理の追加はここに記述してください
        //足音管理
        if (moveDirection.x == 0 && moveDirection.z == 0)
        {
            source.Stop();
            flag = true;
        }
        else
        {
            if (flag)
            {
                Debug.Log("play");
                source.Play();
                flag = false;
            }
        }
        if (!isGrounded)
        {
            source.Stop();
            flag = true;
        }

        if (goal == true)
        {
            
            Debug.Log("End");
            alfa += fadespeed;
            end.color = new Color(255, 255, 255, alfa);

            // 音再生
            if (!playedSE)
            {
                playedSE = true;
                GameObject GoalSE = GameObject.Find("GoalSE");
                GoalSE.GetComponent<GoalSEPlayer>().PlaySound();
            }

            time += Time.deltaTime;
            if(time>4.0f)
            {
                // エンディングシーンをロード
                SceneManager.LoadScene("EndingScene Movie");//1/7名称の変更
            }
        }

        if (goal)
        {
            //ゴールした時移動制限用
            moveDirection = new Vector3(0, 0f, 0);
            return;
        }
    }

    // オブジェクトが別の物理コライダーと接触した際に一度だけ呼ばれる
    private void OnTriggerEnter(Collider other)
    {
        // 隠れる可能オブジェクトと接触した場合は「隠れる」可能状態にする
        if (checkHideable(other))
        {
            canHide = true;
        }
    }

    // オブジェクトが他のコライダーとの接触を終了した際に一度だけ呼ばれる
    private void OnTriggerExit(Collider other)
    {
        // 隠れる可能オブジェクトと接触を終了した場合は「隠れる」可能状態を終了する
        if (checkHideable(other))
        {
            canHide = false;
        }
    }

    // オブジェクトが別の物理コライダーと接触した際に一度だけ呼ばれる
    private void OnCollisionEnter(Collision collision)
    {
        //処理の追加はここに記述してください
        // 衝突したオブジェクトの名前をチェック
        if (collision.gameObject.name == "keyItem")
        {
            Debug.Log("keyItem と衝突しました！");
            Destroy(collision.gameObject); // 衝突したオブジェクトを削除
            Instantiate(bossPrefab, new Vector3(162, 20, 5), Quaternion.Euler(0, 270, 0)); // ボスを生成
            hasKeyItem = true;
        }

        if (collision.gameObject.name == "StageCheckPoint") 
        {
            respawnPosition=collision.transform.position;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "Gameover Area") 
        {
            //1/7に変更、作ってもらったのにすみません
            //transform.position = respawnPosition;
            hitcheck.GetComponent<PlayerHitCheck>().SetPlayerHitCheck(true);
        }

        if (collision.gameObject.name == "Stage2StartPoint")
        {
            inSafeArea = false;
            Destroy(collision.gameObject);
            StartCoroutine(GoToSafeArea(timeRemaining,inSafeArea,respawnPosition));

        }

        if (collision.gameObject.name == "Stage2GoalPoint")
        {
            inSafeArea = true;
            Destroy(collision.gameObject);
            

        }

        if (collision.gameObject.name == "EndingScene" && !goal)
        {
            goal = true;
        }
    }

    // メソッドの追加はここに記述してください
    IEnumerator GoToSafeArea(float timeRemaining, bool inSafeArea, Vector3 respawnPosition)
    {
        // 指定した時間だけ待機
        yield return new WaitForSeconds(timeRemaining);

        // セーフエリア外ならリスポーン位置に移動
        if (!inSafeArea)
        {
            // ゲームオーバー処理
            transform.position = respawnPosition;
            Debug.Log("ゲームオーバー: リスポーン位置に移動");
        }
        else 
        {
            Debug.Log("クリア");
        }
    }

    private void DebugLog(string code)
    {
        Debug.Log(code);
    }

    //----------------------------------------------
    // 各種処理
    //----------------------------------------------

    // 移動処理
    private void HandleMovement()
    {
        // 移動速度を現在速度に加える
        playerRigidbody.velocity = new Vector3(moveDirection.x * moveSpeed, playerRigidbody.velocity.y, moveDirection.z * moveSpeed); 

        // 0.1f以上の入力がある場合アニメーションと回転処理を行う
        if (moveDirection.magnitude > 0.1f)
        {
            // 回転
            RotatePlayer(moveDirection);

            // 移動のアニメーション
            playerAnimator.SetBool("isRunning", true);
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);
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
        // 初期ジャンプ力をY方向に加える
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // ジャンプのアニメーション
        playerAnimator.SetTrigger("Jump");

        Debug.Log("Jump");
    }

    // ジャンプ力追加処理
    private void AddJumpForce()
    {
        // ジャンプ時間を加算
        jumpTimer += Time.deltaTime;

        // ジャンプ時間が最大ジャンプ時間に達していない場合ジャンプ力を加算
        if (jumpTimer < maxJumpTime)
        {
            float jumpMultiplier = 1f - (jumpTimer / maxJumpTime); // リニア減衰
            playerRigidbody.AddForce(Vector3.up * jumpBoostForce * jumpMultiplier, ForceMode.Acceleration);
        }
        // ジャンプ時間が最大ジャンプ時間に達した場合ジャンプを終了
        else
        {
            isJumping = false;
        }
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
    private bool CheckGrounded()
    {
        // 接地判定用オブジェクトがGroundLayerに接しているかで判定する
        return (Physics.CheckSphere(GroundCheck.position, GroundCheckRadius, GroundLayer));
    }

    //「隠れる」可能オブジェクトか判定
    private bool checkHideable(Collider other)
    {
        // オブジェクトが「Hideable」タグを持っているかで判定する
        return (other.CompareTag("Hideable"));
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

        // ボタンが押下されたとき
        // 接地判定はコヨーテタイムを考慮して行う
        if (context.started && groundTimer > 0f)
        {
            isJumping = true;
            jumpHeld = true;
            jumpTimer = 0f;

            // Y軸の速度を初期化
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);

            JumpPlayer();
        }
        // ボタン離されたとき
        else if (context.canceled)
        {
            isJumping = false;
            jumpHeld = false;
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
