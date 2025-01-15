using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class jack_controller : MonoBehaviour
{
    //---------------------------------------------
    //インスペクター参照不可
    //---------------------------------------------
    private GameObject player;//プレイヤー
    private GameObject enemy;// jack
    private Animator anim;//アニメーション
    private float animCnt;//カウント用
    //アニメーション終了値
    private float FindMax;
    private float JackMax;
    private float BackMax;
    //---------------------------------------------
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;
    //インスペクター参照可
    //---------------------------------------------
    [SerializeField] private State state;
    //---------------------------------------------
    //Stateの作成
    //---------------------------------------------
    private enum State
    {
        Non, Idle, Find, Jack, Back
    };
    private void Start()
    {
        //初期化
        {
            player = GameObject.Find("Player");
            enemy = this.gameObject;
            state = State.Idle;
            animCnt = 0;
            this.anim = enemy.GetComponent<Animator>();
            ChangeStateAnim(State.Idle);
            //アニメーションの終了値
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
        //rayを生成する用のベクトル、他では使わない
        Vector3 enemypos = new Vector3(
            enemy.transform.position.x, enemy.transform.position.y + 0.5f, enemy.transform.position.z - 1.2f);
        return enemypos;
    }
    private bool PlayerFind()
    {
        //Rayの生成
        Ray ray = new Ray(GetPosition(), enemy.transform.forward);
        //Debug.DrawRay(ray.origin, ray.direction * 900, Color.blue, 5.0f);
        RaycastHit hit;

        //プレイヤーとrayが接触したか判断
        if (Physics.Raycast(ray, out hit))
        {
            //プレイヤーかタグで判断
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
        //引数をもとにアニメーションを変更
        this.state = state;
        int Anim = (int)this.state;
        anim.SetInteger("Jack_controller", Anim);
    }
    private bool AnimCnt(float cntMax)
    {
        //カウントさせる
        animCnt += 1.0f * Time.deltaTime;

        //引数をもとに指定の時間がたつとカウント終了
        if(animCnt >= cntMax)
        {
            animCnt = 0;
            return true;
        }

        return false;
    }
    //---------------------------------------------
    //参照可能関数
    //---------------------------------------------
    public int GetState()
    {
        //stateを参照させる、enumをプライベートにしているからintにキャストしている
        return (int)this.state;
    }
}
