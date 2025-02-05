using System.Collections;
using System.Collections.Generic;
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
        Death
    }

    protected enum TriggerType 
    {
        EnterIdle,
        EnterChase,
        EnterBack,
        EnterAttack,
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

    [Header("追いかけるスピード")]
    [SerializeField] protected float ChaseSpeed = 4.0f;//追いかけるスピード
    [Header("効果音")]
    [SerializeField] AudioSource source;//オーディオソース
    [SerializeField] AudioClip attack;

    private void Start()
    {

        stateMachine = new StateMachine<StateType, TriggerType>(StateType.Born);
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

        // 変数の初期化

        {
            bornTimeCnt = 0;
            bornTimeCntMax = 2.0f;
            chacePosMax = 67.67566f;
            prePos = transform.position;
            deathTimeCnt = 0;
            deathTimeCntMax = 5.0f;
        }
    }

    private void Update()
    {
        // ステートマシーンの更新
        stateMachine.Update(Time.deltaTime);
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
    public void SetPlayerPosition(Vector3 PlayerPos) 
    {
        playerPos = PlayerPos;
    }
    public void SetMoveDirection(int dir) { moveDirection = dir; }
}
