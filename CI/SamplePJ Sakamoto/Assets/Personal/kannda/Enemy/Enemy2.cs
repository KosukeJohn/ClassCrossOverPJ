using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy2 : MonoBehaviour
{
    protected enum StateType
    {
        Non,
        Born,
        Idle,
        Attack,
        Death
    }
    protected enum TriggerType
    {
        EnterIdle,
        EnterAttack,
        EnterDeath
    }
    protected enum ColorType
    {
        Non,
        Red,
        Blue
    }

    protected StateMachine<StateType, TriggerType> stateMachine;
    private GameObject HitCheck;
    protected GameObject player;
    protected Animator anim;
    protected GameObject RedLightParent;
    protected GameObject BlueLightParent;
    protected Light[] redLight;
    protected Light[] blueLight;
    protected Collider hitColl;
    private Animation animat;

    protected float SpeedY;
    protected bool attackFlag;
    private float firstPosY;
    private bool hitFlag;
    private bool hitStop;
    private float animTimeCnt;


    [Header("ステータス")]
    [SerializeField] protected float SpeedX;

    [Header("効果音")]
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip atk;

    private void Start()
    {
        stateMachine = new StateMachine<StateType, TriggerType>(StateType.Born);
        HitCheck = GameObject.Find("HitCheck");
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        hitColl = GetComponent<Collider>();

        RedLightParent = transform.GetChild(0).gameObject;
        redLight = new Light[RedLightParent.transform.childCount];
        for (int i = 0; i < redLight.Length; i++)
        {
            redLight[i] = RedLightParent.transform.GetChild(i).GetComponent<Light>();
            redLight[i].enabled = false;
        }

        BlueLightParent = transform.GetChild(1).gameObject;
        blueLight = new Light[BlueLightParent.transform.childCount];
        for (int i = 0; i < blueLight.Length; i++)
        {
            blueLight[i] = BlueLightParent.transform.GetChild(i).GetComponent<Light>();
            blueLight[i].enabled = false;
        }

        // 遷移条件の登録

        stateMachine.AddTransition(StateType.Born, StateType.Idle, TriggerType.EnterIdle);
        stateMachine.AddTransition(StateType.Idle,StateType.Death,TriggerType.EnterDeath);
        stateMachine.AddTransition(StateType.Idle, StateType.Attack, TriggerType.EnterAttack);
        stateMachine.AddTransition(StateType.Attack, StateType.Death, TriggerType.EnterDeath);
        stateMachine.AddTransition(StateType.Attack, StateType.Idle, TriggerType.EnterIdle);

        // Actionの登録

        stateMachine.SetupState(StateType.Born, () => EnterBorn(), () => ExitBorn(), deltatime => UpdateBorn());
        stateMachine.SetupState(StateType.Idle, () => EnterIdle(), () => ExitIdle(), deltatime => UpdateIdle());
        stateMachine.SetupState(StateType.Attack, () => EnterAttack(), () => ExitAttack(), deltatime => UpdateAttack());
        stateMachine.SetupState(StateType.Death, () => EnterDeath(), () => ExitDeath(), deltatime => UpdateDeath());

        // 変数の初期化
        {
            firstPosY = 9.3f;
            source.clip = atk;
            SpeedX /= -100;
        }
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

    private void EnterBorn() { anim.Play("Idle", 0, 0);}
    private void ExitBorn() { Debug.Log("ExitBorn"); }
    private void UpdateBorn()
    {
        SpeedY = 1.5f;
        this.transform.Translate(0, SpeedY, 0);

        if (this.transform.position.y >= firstPosY)
        {
            this.transform.position =
                new Vector3(this.transform.position.x, firstPosY, this.transform.position.z);
            SpeedY = 0;
            stateMachine.ExecuteTrigger(TriggerType.EnterIdle);
        }
    }
    private void EnterIdle() { anim.Play("Idle", 0, 0); ChangeLightColor(false); }
    private void ExitIdle() { Debug.Log("ExitIdle"); }
    protected abstract void UpdateIdle();
    private void EnterAttack() { hitStop = true; }
    private void ExitAttack() { Debug.Log("ExitAttack");}
    protected abstract void UpdateAttack();
    private void EnterDeath() { anim.Play("Idle", 0, 0); }
    private void ExitDeath() { Debug.Log("ExitDeath"); }
    private void UpdateDeath() 
    {
        if (!GetComponent<Rigidbody>())
        {
            this.gameObject.AddComponent<Rigidbody>();
        }

        if (this.transform.position.y <= -20)
        {
            Destroy(gameObject);
        }
    }

    // メソッド

    protected virtual void OnTriggerStay(Collider other)
    {
        if (attackFlag)
        {
            if (other.tag == "Player")
            {
                // ヒットストップ
                if (hitStop) 
                {
                    HitStopManager.instance.StartHitStop(0.3f);
                    hitStop = false;
                }
               
                hitFlag = true;
            }
        }
    }
    protected void ChangeLightColor(bool onLight)
    {
        if (onLight)
        {
            for (int i = 0; i < redLight.Length; i++) { redLight[i].enabled = true; }
            for (int i = 0; i < blueLight.Length; i++) { blueLight[i].enabled = false; }
        }
        else
        {
            for (int i = 0; i < redLight.Length; i++) { redLight[i].enabled = false; }
            for (int i = 0; i < blueLight.Length; i++) { blueLight[i].enabled = true; }
        }
    }
    private void AtkSoundPlay()
    {
        source.Play();
    }
}
