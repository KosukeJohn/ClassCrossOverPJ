using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy2 : MonoBehaviour
{
    protected enum StateType
    {
        Non,
        Born,
        BornMove,
        BornAttack,
        Idle,
        Find,
        Chase,
        Attack,
        Death
    }
    protected enum TriggerType
    {
        EnterNext,
        EnterIdle,
        EnterFind,
        EnterChase,
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
    protected TriggerType nextTriggerType;

    protected float SpeedY;
    protected bool attackFlag;
    private float _timeCnt;
    private bool onChase;
    private float chasePos;

    private float firstPosY;
    private bool hitFlag;
    private int LightCnt = 0;
    private bool setFlashLight;

    [Header("�X�e�[�^�X")]
    [SerializeField] protected float SpeedX;

    [Header("�e�펞�Ԑݒ�")]
    [SerializeField] private float ChaseCntMax;// �ǐՎ���
    [SerializeField] private float toAttackCntMax;// �_�Ŏ���
    [SerializeField] private float AttackCntMax;// �U������

    [Header("�e��ʒu���")]
    [SerializeField] private float changeCntSpeedPos;// �ǐՎ��Ԃ��ς��ꏊ
    [SerializeField] private float destroyEnemyPos;// �}���I�l�b�g�̔j��ꏊ

    [Header("���ʉ�")]
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip atk;

    private void Start()
    {
        stateMachine = new StateMachine<StateType, TriggerType>(StateType.Born);
        NextStateType();
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

        // �J�ڏ����̓o�^

        stateMachine.AddTransition(StateType.Born, StateType.BornMove, TriggerType.EnterNext);
        stateMachine.AddTransition(StateType.BornMove, StateType.BornAttack, TriggerType.EnterNext);
        stateMachine.AddTransition(StateType.BornAttack, StateType.Idle, TriggerType.EnterIdle);
        stateMachine.AddTransition(StateType.Born, StateType.Idle, TriggerType.EnterIdle);
        stateMachine.AddTransition(StateType.Idle,StateType.Death,TriggerType.EnterDeath);
        stateMachine.AddTransition(StateType.Idle, StateType.Find, TriggerType.EnterFind);
        stateMachine.AddTransition(StateType.Find, StateType.Chase, TriggerType.EnterChase);
        stateMachine.AddTransition(StateType.Find, StateType.Idle, TriggerType.EnterIdle);
        stateMachine.AddTransition(StateType.Chase, StateType.Attack, TriggerType.EnterAttack);
        stateMachine.AddTransition(StateType.Attack, StateType.Idle, TriggerType.EnterIdle);

        // Action�̓o�^

        stateMachine.SetupState(StateType.Born, () => EnterBorn(), () => ExitBorn(), deltatime => UpdateBorn());
        stateMachine.SetupState(StateType.BornMove, () => EnterBornMove(), () => ExitBornMove(), deltatime => UpdateBornMove());
        stateMachine.SetupState(StateType.BornAttack, () => EnterBornAttack(), () => ExitBornAttack(), deltatime => UpdateBornAttack());
        stateMachine.SetupState(StateType.Idle, () => EnterIdle(), () => ExitIdle(), deltatime => UpdateIdle());
        stateMachine.SetupState(StateType.Find, () => EnterFind(), () => ExitFind(), deltatime => UpdateFind());
        stateMachine.SetupState(StateType.Chase, () => EnterChase(), () => ExitChase(), deltatime => UpdateChase());
        stateMachine.SetupState(StateType.Attack, () => EnterAttack(), () => ExitAttack(), deltatime => UpdateAttack());
        stateMachine.SetupState(StateType.Death, () => EnterDeath(), () => ExitDeath(), deltatime => UpdateDeath());

        // �ϐ��̏�����
        {
            firstPosY = 9.3f;
            source.clip = atk;
            SpeedX /= -100;
            _timeCnt = 0;
        }
    }

    private void Update()
    {
        // �X�e�[�g�}�V�[���̍X�V
        stateMachine.Update(Time.deltaTime);

        // �����蔻��̎���
        HitCheck.GetComponent<PlayerHitCheck>().SetPlayerHitCheck(hitFlag);
        if (hitFlag) { hitFlag = false; }

        // ���C�g
        if (setFlashLight) { LightFlashing(8); }
    }

    // �X�e�[�^�X���Ƃ̃��\�b�h

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
            stateMachine.ExecuteTrigger(nextTriggerType);
        }
    }
    private void EnterBornMove() { anim.Play("Idle", 0, 0); }
    private void ExitBornMove() { }
    private void UpdateBornMove() 
    {
        if (transform.position.x >= 131.410004f)
        {
            stateMachine.ExecuteTrigger(TriggerType.EnterNext);
            return;
        }

        Vector3 pos = new Vector3(131.410004f, this.transform.position.y, this.transform.position.z);
        float moveSpeed = 3.0f;
        this.transform.position =
            Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);
    }
    private void EnterBornAttack() 
    {
        anim.Play("Find+", 0, 0);
        ChangeLightColor(ColorType.Red);
        _timeCnt = 0;
    }
    private void ExitBornAttack() { }
    private void UpdateBornAttack() 
    {
        _timeCnt += Time.deltaTime;

        if (_timeCnt >= 2f) 
        {
            stateMachine.ExecuteTrigger(TriggerType.EnterIdle);
            return;
        }
    }
    private void EnterIdle() 
    { 
        anim.Play("Idle", 0, 0);
        ChangeLightColor(ColorType.Blue);
        _timeCnt = 0;

        {
            SpeedY = 0;
            this.transform.position =
                new Vector3(this.transform.position.x, firstPosY, this.transform.position.z);
        }

        if (transform.position.x >= changeCntSpeedPos)
        {
            ChaseCntMax = 2.0f;
        }
    }
    private void ExitIdle() { Debug.Log("ExitIdle"); }
    protected void UpdateIdle()
    {
        _timeCnt += Time.deltaTime;

        SpeedY -= 9.8f * Time.deltaTime;
        if (SpeedY < 0) { SpeedY = 0; }

        if (this.transform.position.x >= destroyEnemyPos)
        {
            stateMachine.ExecuteTrigger(TriggerType.EnterDeath);
            return;
        }

        // ���C�g�̓_�ŏ���
        if (_timeCnt >= ChaseCntMax) 
        {
            stateMachine.ExecuteTrigger(TriggerType.EnterFind);
        }

        // �}���I�l�b�g�̈ړ�����
        this.transform.Translate(SpeedX, SpeedY, 0);
    }
    private void EnterFind() 
    {
        // ���C�g��F�ɓ_�ł�����
        ChangeLightColor(ColorType.Blue, true);

        // �^�C�}�[���[���ɂ���
        _timeCnt = 0;

        // �A�j���[�V�����𓮂���
        anim.Play("Idle", 0, 0);

        onChase = false;
    }
    private void ExitFind() { }
    private void UpdateFind() 
    {
        _timeCnt += Time.deltaTime;

        // ��莞�Ԃőҋ@�ɖ߂�
        if (_timeCnt >= AttackCntMax)
        {
            // Invoke("AtkSoundPlay", 1.0f);
            stateMachine.ExecuteTrigger(TriggerType.EnterIdle);
            return;
        }

        // �R���g���[���[���擾���ē|��Ă�����ǐՂɈڍs

        GameObject player = GameObject.Find("Player");
        Vector2 vector = player.GetComponent<PlayerController>().GetMoveInput();

        if (vector.x != 0) 
        {
            chasePos = player.transform.position.x;
            stateMachine.ExecuteTrigger(TriggerType.EnterChase);
            return;
        }
        if (vector.y != 0) 
        {
            chasePos = player.transform.position.x;
            stateMachine.ExecuteTrigger(TriggerType.EnterChase);            
            return;
        }
    }
    private void EnterChase() 
    {
        ChangeLightColor(ColorType.Red);
    }
    private void ExitChase() { }
    private void UpdateChase() 
    {
        bool onAttack = Mathf.Approximately(this.transform.position.x, chasePos);
        if (onAttack)
        {
            stateMachine.ExecuteTrigger(TriggerType.EnterAttack);
            return;
        }

        Vector3 pos = new Vector3(chasePos, this.transform.position.y, this.transform.position.z);
        float moveSpeed = 8.0f;
        this.transform.position =
            Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);
    }
    private void EnterAttack() 
    {
        // ���C�g��Ԃɂ���
        ChangeLightColor(ColorType.Red);

        // �A�j���[�V�����̍Đ�
        anim.Play("Find+", 0, 0);

        // �^�C�}�[���O�ɂ���
        _timeCnt = 0;
    }
    private void ExitAttack() { Debug.Log("ExitAttack");}
    protected virtual void UpdateAttack() 
    {
        _timeCnt += Time.deltaTime;
        // �A�j���[�V�������I�������Idle�ɂ���

        if (_timeCnt >= 2.0f)
        {
            stateMachine.ExecuteTrigger(TriggerType.EnterIdle);
        }
    }
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

    // ���\�b�h

    protected abstract void NextStateType();
    protected virtual void OnTriggerStay(Collider other)
    {
        if (attackFlag)
        {
            if (other.tag == "Player")
            {
                hitFlag = true;
            }
        }
    }
    private void ChangeLightColor(ColorType color, bool setFlashing = false)
    {
        if (color == ColorType.Red)
        {
            for (int i = 0; i < redLight.Length; i++) { redLight[i].enabled = true; }
            for (int i = 0; i < blueLight.Length; i++) { blueLight[i].enabled = false; }
        }
        if (color == ColorType.Blue)
        {
            for (int i = 0; i < redLight.Length; i++) { redLight[i].enabled = false; }
            for (int i = 0; i < blueLight.Length; i++) { blueLight[i].enabled = true; }
        }

        if (setFlashing) { setFlashLight = true; }
        else { setFlashLight = false; }
    }
    private void AtkSoundPlay()
    {
        source.Play();
    }
    private void LightFlashing(int change,ColorType color = ColorType.Blue)
    {
        LightCnt++;

        if (color == ColorType.Blue) 
        {
            bool onlight = blueLight[0].enabled;

            if (LightCnt % (change + 1) == 0)
            {
                if (onlight) { onlight = false; }
                else { onlight = true; }
            }

            for (int i = 0; i < blueLight.Length; i++) { blueLight[i].enabled = onlight; }
        }

        if (color == ColorType.Red) 
        {
            bool onlight = redLight[0].enabled;

            if (LightCnt % (change + 1) == 0)
            {
                if (onlight) { onlight = false; }
                else { onlight = true; }
            }

            for (int i = 0; i < redLight.Length; i++) { redLight[i].enabled = onlight; }
        }
    }
}
