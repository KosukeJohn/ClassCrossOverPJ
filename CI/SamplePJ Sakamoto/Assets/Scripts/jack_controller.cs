using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class jack_controller : MonoBehaviour
{
    //---------------------------------------------
    //�C���X�y�N�^�[�Q�ƕs��
    //---------------------------------------------
    private GameObject player;//�v���C���[
    private GameObject enemy;// jack
    private Animator anim;//�A�j���[�V����
    private float animCnt;//�J�E���g�p
    //�A�j���[�V�����I���l
    private float FindMax;
    private float JackMax;
    private float BackMax;
    //---------------------------------------------
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;
    //�C���X�y�N�^�[�Q�Ɖ�
    //---------------------------------------------
    [SerializeField] private State state;
    //---------------------------------------------
    //State�̍쐬
    //---------------------------------------------
    private enum State
    {
        Non, Idle, Find, Jack, Back
    };
    private void Start()
    {
        //������
        {
            player = GameObject.Find("Player");
            enemy = this.gameObject;
            state = State.Idle;
            animCnt = 0;
            this.anim = enemy.GetComponent<Animator>();
            ChangeStateAnim(State.Idle);
            //�A�j���[�V�����̏I���l
            FindMax = 2.20f;
            JackMax = 1.0f;
            BackMax = 1.167f;
        }
    }
    private void FixedUpdate()
    {
        if(state == State.Idle)
        {
            if (PlayerFind())
            {
                Vector3 pos = new(transform.position.x, transform.position.y, transform.position.z - 4);
                player.GetComponent<PlayerFirstPos>().SetFirstPos(pos);
                ChangeStateAnim(State.Find);
                source.clip = clip; ;
                source.Play();
            }
        }
        
        if(state == State.Find)
        {
            if(AnimCnt(FindMax))
            {
                ChangeStateAnim(State.Jack);
            }
        }
        
        if(state == State.Jack)
        {
            if (AnimCnt(JackMax))
            {
                ChangeStateAnim(State.Back);
            }
        }

        if (state == State.Back)
        {
            if (AnimCnt(BackMax))
            {
                ChangeStateAnim(State.Idle);
            }
        }
    }
    private Vector3 GetPosition()
    {
        //ray�𐶐�����p�̃x�N�g���A���ł͎g��Ȃ�
        Vector3 enemypos = new Vector3(
            enemy.transform.position.x, enemy.transform.position.y + 0.5f, enemy.transform.position.z - 1.2f);
        return enemypos;
    }
    private bool PlayerFind()
    {
        //Ray�̐���
        Ray ray = new Ray(GetPosition(), enemy.transform.forward);
        //Debug.DrawRay(ray.origin, ray.direction * 900, Color.blue, 5.0f);
        RaycastHit hit;

        //�v���C���[��ray���ڐG���������f
        if (Physics.Raycast(ray, out hit))
        {
            //�v���C���[���^�O�Ŕ��f
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("hit");
                state = State.Find;
                return true;
            }
        }

        return false;

    }
    private void ChangeStateAnim(State state)
    {
        //���������ƂɃA�j���[�V������ύX
        this.state = state;
        int Anim = (int)this.state;
        anim.SetInteger("Jack_controller", Anim);
    }
    private bool AnimCnt(float cntMax)
    {
        //�J�E���g������
        animCnt += 1.0f * Time.deltaTime;

        //���������ƂɎw��̎��Ԃ����ƃJ�E���g�I��
        if(animCnt >= cntMax)
        {
            animCnt = 0;
            return true;
        }

        return false;
    }
    //---------------------------------------------
    //�Q�Ɖ\�֐�
    //---------------------------------------------
    public int GetState()
    {
        //state���Q�Ƃ�����Aenum���v���C�x�[�g�ɂ��Ă��邩��int�ɃL���X�g���Ă���
        return (int)this.state;
    }
}
