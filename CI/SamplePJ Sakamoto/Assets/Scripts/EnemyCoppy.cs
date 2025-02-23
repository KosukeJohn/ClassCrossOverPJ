using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public abstract class EnemyCoppy : MonoBehaviour
{
    private enum StateType
    {
        Non,
        Born,
        Idle,
        Chase,
        Back,
        Attack,
        Jump,
        Death
    }

    protected enum TriggerType 
    {
        EnterIdle,
        EnterChase,
        EnterBack,
        EnterAttack,
        EnterJump,
        EnterDeath
    }

    protected enum ColorType 
    { 
        Non,
        Red,
        Blue
    }

    private StateMachine<StateType,TriggerType> stateMachine;
    protected Animator anim;
    protected Light redLight;
    protected Light blueLight;
    protected Vector3 playerPos;
    protected bool checkHideFlag;
    protected bool playerFindFlag;
    protected bool goPlayerFlag;
    protected int moveDirection;

    private float bornTimeCnt;
    private float bornTimeCntMax;
    private float chacePosMax;
    private Vector3 prePos;
    private float deathTimeCnt;
    private float deathTimeCntMax;
    private bool isJump;
    private float enterJumpCnt;
    private int jumpCnt;
    private bool enemyHight;
    private float jumpLenge;
    private float preLenge;

    [Header("追いかける")]
    [SerializeField] protected float ChaseSpeed = 4.0f;//追いかけるスピード
    [Header("飛び越える")]
    [SerializeField] protected float MaxJumpCnt = 1.0f;
    [SerializeField] protected float JumpUpSpeed = 5.0f;
    [SerializeField] protected float JumpFowrdSpeed = 5.0f;
    [SerializeField] protected float JumpDownSpeed = 5.0f;
    [Header("効果音")]
    [SerializeField] AudioSource source;//オーディオソース
    [SerializeField] AudioClip attack;

    private void Start()
    {
        stateMachine = new StateMachine<StateType, TriggerType>(StateType.Idle);
        anim = GetComponent<Animator>();
        redLight = transform.GetChild(0).GetComponent<Light>();
        blueLight = transform.GetChild(1).GetComponent<Light>();
        redLight.enabled = false;
        blueLight.enabled = false;

        // 遷移条件の登録

        stateMachine.AddTransition(StateType.Born, StateType.Idle, TriggerType.EnterIdle);
        stateMachine.AddTransition(StateType.Idle, StateType.Chase, TriggerType.EnterChase);
        stateMachine.AddTransition(StateType.Idle, StateType.Back, TriggerType.EnterBack);
        stateMachine.AddTransition(StateType.Idle, StateType.Attack, TriggerType.EnterAttack);
        stateMachine.AddTransition(StateType.Chase, StateType.Idle, TriggerType.EnterIdle);
        stateMachine.AddTransition(StateType.Chase, StateType.Back, TriggerType.EnterBack);
        stateMachine.AddTransition(StateType.Chase, StateType.Attack, TriggerType.EnterAttack);
        stateMachine.AddTransition(StateType.Chase, StateType.Jump, TriggerType.EnterJump);
        stateMachine.AddTransition(StateType.Jump, StateType.Chase, TriggerType.EnterChase);
        stateMachine.AddTransition(StateType.Back, StateType.Idle, TriggerType.EnterIdle);
        stateMachine.AddTransition(StateType.Back, StateType.Chase, TriggerType.EnterChase);
        stateMachine.AddTransition(StateType.Back, StateType.Attack, TriggerType.EnterAttack);
        stateMachine.AddTransition(StateType.Attack, StateType.Death, TriggerType.EnterDeath);
        

        // Actionの登録

        stateMachine.SetupState(StateType.Born, () => EnterBorn(), () => ExitBorn(), deltatime => UpdateBorn());
        stateMachine.SetupState(StateType.Idle, () => EnterIdle(), () => ExitIdle(), deltatime => UpdateIdle());
        stateMachine.SetupState(StateType.Chase, () => EnterChase(), () => ExitChase(), deltatime => UpdateChase());
        stateMachine.SetupState(StateType.Back, () => EnterBack(), () => ExitBack(), deltatime => UpdateBack());
        stateMachine.SetupState(StateType.Death, () => EnterDeath(), () => ExitDeath(), deltatime => UpdateDeath());
        stateMachine.SetupState(StateType.Attack, () => EnterAttack(), () => ExitAttack(), deltatime => UpdateAttack());
        stateMachine.SetupState(StateType.Jump, () => EnterJump(), () => ExitJump(), deltatime => UpdateJump());

        // 変数の初期化

        {
            bornTimeCnt = 0;
            bornTimeCntMax = 2.0f;
            chacePosMax = 67.67566f;
            prePos = transform.position;
            deathTimeCnt = 0;
            deathTimeCntMax = 5.0f;
            enterJumpCnt = 0;
        }
    }

    private void Update()
    {
        // ステートマシーンの更新
        stateMachine.Update(Time.deltaTime);

        if (isJump) { isJump = false; }
    }

    // ステータスごとのメソッド

    private void EnterIdle() { anim.Play("Idle", 0, 0); ChangeLight(ColorType.Red); }
    private void UpdateIdle() 
    {
        bool isDeath = ChangeStateDeath();
        if (isDeath) { return; }

        if (playerFindFlag) 
        {
            if (checkHideFlag) 
            {
                ChangeStateMachine(TriggerType.EnterBack);
            }
            else 
            {
                ChangeStateMachine(TriggerType.EnterChase);
            }
            return;
        }
    }
    private void ExitIdle() { DebugLog("ExitIdle"); }
    private void EnterChase() { anim.Play("Run", 0, 0); ChangeLight(ColorType.Red); }
    private void UpdateChase() 
    {
        if (!playerFindFlag) { ChangeStateMachine(TriggerType.EnterIdle); return; }
        if (checkHideFlag) { ChangeStateMachine(TriggerType.EnterBack); return; }
        if (isJump) 
        {
            enterJumpCnt += Time.deltaTime;
            if (enterJumpCnt >= MaxJumpCnt)
            {
                enterJumpCnt = 0;
                ChangeStateMachine(TriggerType.EnterJump);
                return;
            }
        }
        bool isDeath = ChangeStateDeath();
        if (isDeath) { return; }

        if (goPlayerFlag)
        {
            transform.position =
               Vector3.MoveTowards(transform.position, playerPos, ChaseSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate
                (0f, 3.0f * moveDirection, 0f);
        }
    }
    private void ExitChase() { DebugLog("ExitChase"); }
    protected abstract void EnterBack();
    protected abstract void UpdateBack();
    private void ExitBack() { DebugLog("ExitBack"); }
    private void EnterDeath() { anim.Play("Run", 0, 0); ChangeLight(ColorType.Red); }
    private void UpdateDeath() 
    {
        deathTimeCnt += Time.deltaTime;

        if (deathTimeCnt > deathTimeCntMax)
        {
            Destroy(gameObject);
        }

        transform.position =
               Vector3.MoveTowards(transform.position, prePos, ChaseSpeed * Time.deltaTime);
        transform.Rotate
               (0f, 20.0f, 0f);
    }
    private void ExitDeath() { DebugLog("ExitDeath"); }
    private void EnterBorn() { anim.Play("Find", 0, 0); ChangeLight(ColorType.Non); }
    private void UpdateBorn() 
    {
        bornTimeCnt += Time.deltaTime;

        if (bornTimeCnt > bornTimeCntMax)
        {
            bornTimeCnt = 0;
            ChangeStateMachine(TriggerType.EnterIdle);
        }
    }
    private void ExitBorn() { DebugLog("ExitBorn"); }
    private void EnterAttack() { anim.Play("Find", 0, 0);ChangeLight(ColorType.Red); AttackSound(); }
    private void UpdateAttack() 
    {
        bornTimeCnt += Time.deltaTime;

        if (bornTimeCnt > bornTimeCntMax)
        {
            bornTimeCnt = 0;
            ChangeStateMachine(TriggerType.EnterDeath);
        }
    }
    private void ExitAttack() { DebugLog("ExitAttack"); }
    private void EnterJump() { anim.Play("Run", 0, 0); jumpCnt = 0; jumpLenge = CheckFallPosition(); }
    private void UpdateJump() 
    {
        // 前に進む処理
        float moveSpeed = JumpFowrdSpeed * Time.deltaTime;
        transform.position += this.transform.forward * moveSpeed;
        preLenge += moveSpeed;

        // 上下の処理
        if (preLenge <= jumpLenge / 2)
        {
            float upSpeed = JumpUpSpeed * Time.deltaTime;
            transform.position += this.transform.up * upSpeed;
        }
        else
        {
            float fallSpeed = JumpDownSpeed * Time.deltaTime;
            transform.position += -1 * this.transform.up * fallSpeed;

            if (transform.position.y<=0.1f)
            {
                transform.position =
                    new(this.transform.position.x, 0, this.transform.position.z);
                ChangeStateMachine(TriggerType.EnterChase);
            }
        }
    }
    private void ExitJump() { DebugLog("ExitJump"); preLenge = 0; }

    // メソッド

    private void DebugLog(string code) { Debug.Log(code); }
    protected void ChangeStateMachine(TriggerType trigger) 
    {
        if (stateMachine.GetState() == StateType.Non) { return; }

        stateMachine.ExecuteTrigger(trigger);
    }
    protected bool ChangeStateDeath() 
    {
        if (transform.position.x >= chacePosMax) 
        {
            ChangeStateMachine(TriggerType.EnterAttack);
            return true;
        }

        return false;
    }
    protected void ChangeLight(ColorType colorType) 
    {
        switch (colorType)
        {
            case ColorType.Non:
                {
                    redLight.enabled = false;
                    blueLight.enabled = false;
                    break;
                }
            case ColorType.Red: 
                {
                    redLight.enabled = true;
                    blueLight.enabled = false;
                    break;
                }
            case ColorType.Blue:
                {
                    redLight.enabled = false;
                    blueLight.enabled = true;
                    break;
                }
        }
    }
    private void AttackSound() 
    {
        source.clip = attack;
        source.Play();
    }
    private float CheckFallPosition()
    {
        Vector3 prePos =
            new(this.transform.position.x, this.transform.position.y + 5, this.transform.position.z);
        Vector3 ForwardPos = this.transform.forward;

        for (int i = 2; i <= 4; i++)
        {
            if (Physics.Raycast(prePos + ForwardPos * 1.0f * i, this.transform.up * -1, out RaycastHit hit))
            {
                if (hit.collider.tag == "Ground")
                {
                    return 1.0f * i;
                }
            }
        }
        
        return 5.0f;
    }

    //参照可能関数

    public void SetPlayerFind(bool findFlag) 
    {
         playerFindFlag = findFlag;
    }
    public void SetPlayerState(bool hideFlag) 
    {
        checkHideFlag = hideFlag;
    }
    public void SetGoPlayer(bool goPlayer) 
    {
        goPlayerFlag = goPlayer;
    }
    public void SetIsJump(bool IsJump) { this.isJump = IsJump; }
    public void SetPlayerPosition(Vector3 PlayerPos) 
    {
        playerPos = PlayerPos;
    }
    public void SetMoveDirection(int dir) { moveDirection = dir; }
}
