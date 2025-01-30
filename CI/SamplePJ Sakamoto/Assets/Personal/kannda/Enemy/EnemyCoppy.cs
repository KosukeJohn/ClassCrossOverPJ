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
        Death
    }

    private enum TriggerType 
    {
        EnterIdle,
        EnterChase,
        EnterBack,
        EnterDeath
    }

    private StateMachine<StateType,TriggerType> stateMachine;
    private Animator anim;
    private Vector3 playerPos;
    private bool checkHideFlag;
    private bool playerFindFlag;
    private bool goPlayerFlag;
    private int moveDirection;

    [SerializeField] private float ChaseSpeed = 4.0f;//追いかけるスピード

    private void Start()
    {

        stateMachine = new StateMachine<StateType, TriggerType>(StateType.Idle);
        anim = GetComponent<Animator>();

        // 遷移条件の登録

        stateMachine.AddTransition(StateType.Born, StateType.Idle, TriggerType.EnterIdle);
        stateMachine.AddTransition(StateType.Idle, StateType.Chase, TriggerType.EnterChase);
        stateMachine.AddTransition(StateType.Idle, StateType.Back, TriggerType.EnterBack);
        stateMachine.AddTransition(StateType.Idle, StateType.Death, TriggerType.EnterDeath);
        stateMachine.AddTransition(StateType.Chase, StateType.Idle, TriggerType.EnterIdle);
        stateMachine.AddTransition(StateType.Chase, StateType.Back, TriggerType.EnterBack);
        stateMachine.AddTransition(StateType.Chase, StateType.Death, TriggerType.EnterDeath);
        stateMachine.AddTransition(StateType.Back, StateType.Idle, TriggerType.EnterIdle);
        stateMachine.AddTransition(StateType.Back, StateType.Chase, TriggerType.EnterChase);
        stateMachine.AddTransition(StateType.Back, StateType.Death, TriggerType.EnterDeath);

        // Actionの登録

        stateMachine.SetupState(StateType.Born, () => EnterBorn(), () => ExitBorn(), deltatime => UpdateBorn());
        stateMachine.SetupState(StateType.Idle, () => EnterIdle(), () => ExitIdle(), deltatime => UpdateIdle());
        stateMachine.SetupState(StateType.Chase, () => EnterChase(), () => ExitChase(), deltatime => UpdateChase());
        stateMachine.SetupState(StateType.Back, () => EnterBack(), () => ExitBack(), deltatime => UpdateBack());
        stateMachine.SetupState(StateType.Death, () => EnterDeath(), () => ExitDeath(), deltatime => UpdateDeath());

    }

    private void Update()
    {
        // ステートマシーンの更新
        stateMachine.Update(Time.deltaTime);
    }

    //メソッド

    private void EnterIdle() { anim.Play("Idle", 0, 0); }
    private void UpdateIdle() 
    {
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
    private void EnterChase() { anim.Play("Run", 0, 0); }
    private void UpdateChase() 
    {
        if (!playerFindFlag) { ChangeStateMachine(TriggerType.EnterIdle); return; }

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
    private void EnterBack() { anim.Play("Run", 0, 0); }
    protected abstract void UpdateBack();
    private void ExitBack() { DebugLog("ExitBack"); }
    private void EnterDeath() { }
    private void UpdateDeath() { }
    private void ExitDeath() { }
    private void EnterBorn() { }
    private void UpdateBorn() { }
    private void ExitBorn() { }

    private void DebugLog(string code) { Debug.Log(code); }
    private void ChangeStateMachine(TriggerType trigger) 
    {
        if (stateMachine.GetState() == StateType.Non) { return; }

        stateMachine.ExecuteTrigger(trigger);
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
