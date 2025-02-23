using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 :MonoBehaviour
{
    private enum StateType
    {
        Non,
        Idle,
        Chase,
        Attack,
        Death
    }

    private enum TriggerType
    {
        EnterChase,
        EnterAttack,
        EnterDeath
    }

    private StateMachine<StateType, TriggerType> stateMachine;
    private GameObject HitCheck;
    private GameObject player;
    private Animator anim;
    private Collider hitColl;

    private bool onMove;
    private bool hitFlag;
    private float timeCnt;

    [Header("位置角度")]
    [SerializeField] private float maxHight;
    [SerializeField] private float rotateY;

    [Header("速度")]
    [SerializeField] private float speedX;
    [SerializeField] private float speedY;

    [Header("設定")]
    [SerializeField] private string AttackAnimName;
    [SerializeField] private TriggerType nextTriger;

    private void Start() 
    {
        stateMachine = new StateMachine<StateType, TriggerType>(StateType.Idle);
        player = GameObject.Find("Player");
        HitCheck = GameObject.Find("HitCheck");
        hitColl = GetComponent<Collider>();
        hitColl.enabled = false;
        anim = GetComponent<Animator>();
        transform.position =
            new(transform.position.x,-20,transform.position.z);
        onMove = false;

        // 遷移条件の登録

        stateMachine.AddTransition(StateType.Idle,StateType.Attack,TriggerType.EnterAttack);
        stateMachine.AddTransition(StateType.Attack, StateType.Chase, TriggerType.EnterChase);
        stateMachine.AddTransition(StateType.Attack, StateType.Death, TriggerType.EnterDeath);
        stateMachine.AddTransition(StateType.Chase, StateType.Death, TriggerType.EnterDeath);

        // Actionの登録

        stateMachine.SetupState(StateType.Idle, () => EnterIdle(), () => ExitIdle(), deltatime => UpdateIdle());
        stateMachine.SetupState(StateType.Chase, () => EnterChase(), () => ExitChase(), deltatime => UpdateChase());
        stateMachine.SetupState(StateType.Attack, () => EnterAttack(), () => ExitAttack(), deltatime => UpdateAttack());
        stateMachine.SetupState(StateType.Death, () => EnterDeath(), () => ExitDeath(), deltatime => UpdateDeath());
    }

    private void Update()
    {
        // ステートマシーンの更新
        stateMachine.Update(Time.deltaTime);

        // 当たり判定の実装
        HitCheck.GetComponent<PlayerHitCheck>().SetPlayerHitCheck(hitFlag);
        if (hitFlag) { hitFlag = false; }
    }

    // ステータスごとのメソッド

    private void EnterIdle() 
    {
        anim.Play("Idle", 0, 0);
    }
    private void ExitIdle() { }
    private void UpdateIdle() 
    {
        if (!onMove) { return; }

        if (this.transform.position.y >= maxHight)
        {
            transform.position =
                new(this.transform.position.x, maxHight, this.transform.position.z);
            stateMachine.ExecuteTrigger(TriggerType.EnterAttack);
            return;
        }

        transform.Translate(0, speedY * Time.deltaTime, 0);
    }
    private void EnterAttack() 
    {
        anim.Play(AttackAnimName, 0, 0);
        timeCnt = 0;
    }
    private void ExitAttack() { }
    private void UpdateAttack() 
    {
        timeCnt += Time.deltaTime;

        float flamCnt = 0 ;
        if (AttackAnimName == "Ryote") { flamCnt = 1 / 3; }
        else { flamCnt = 1 / 2; }

        if (timeCnt >= flamCnt)
        {
            if (transform.parent.GetComponent<fallFlag>())
            {
                transform.parent.GetComponent<fallFlag>().SetFallFlag(true);
            }
        }

        if (timeCnt >= 2.0f)
        {
            stateMachine.ExecuteTrigger(nextTriger);
        }
    }
    private void EnterChase() 
    {
        anim.Play("Chase", 0, 0);
        hitColl.enabled = true;
    }
    private void ExitChase() { }
    private void UpdateChase() 
    {
        Vector3 playerPos = new(player.transform.position.x, player.transform.position.z, 0);
        Vector3 enemyPos = new(this.transform.position.x, this.transform.position.z, 0);
        Vector3 vector = playerPos - enemyPos;

        float rotate = transform.forward.x * vector.y - transform.forward.z * vector.x;
        bool go = false;
        if (-0.05f < rotate && rotate < 0.05f) { go = true; }

        if (go)
        {
            Vector3 pos =
                new(playerPos.x, this.transform.position.y, playerPos.y);

            // 移動処理
            transform.position =
               Vector3.MoveTowards(transform.position, pos, speedX * Time.deltaTime);
            return;
        }

        if (rotate > 0) { transform.Rotate(0f, -5.0f * Time.deltaTime, 0f); }
        else { transform.Rotate(0f, 5.0f * Time.deltaTime, 0f); }
    }
    private void EnterDeath() { anim.Play("Idle", 0, 0); }
    private void ExitDeath() { }
    private void UpdateDeath() 
    {
        if (!GetComponent<Rigidbody>())
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        }

        if (transform.position.y <= -20)
        {
            Destroy(gameObject);
        }
    }

    // メソッド

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            hitFlag = true;
        }
    }

    // 参照可能関数

    public void SetOnMove(bool OnMove) { onMove = OnMove; }
}
