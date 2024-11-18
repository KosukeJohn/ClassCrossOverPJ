using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_cont : MonoBehaviour
{
    public GameObject   Player;
    public Animator     playerAnim;
    public float    speed;
    public float    AngleSpeed;

    private Rigidbody   rb;
    private Collider    Coll;
    private Transform   trans;
    
    void Start()
    {
        this.rb = Player.GetComponent<Rigidbody>();
        this.Coll = Player.GetComponent<Collider>();
        this.trans = Player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Player.GetComponent<Material_Change>().ChangeValue();
        }
        
    }
    void FixedUpdate()
    {
        //プレイヤーの動き
        {
            //コントローラーを取得
            float z = Input.GetAxis("Horizontal");

            if (0.01 > z && z > -0.01)
            {
                z = 0;
            }
            if (z > 0)
            {
                rb.velocity = transform.forward * speed * Time.deltaTime;
            }
            if (z < 0)
            {
                rb.velocity = -transform.forward * (speed - 80.0f) * Time.deltaTime;
            }

            //アニメーション
            Player_Anim(z);
        }
       
        
    }
    //アニメーション
    private void Player_Anim(float z)
    {
        if (z > 0)
        {
            playerAnim.SetTrigger("Run");
            playerAnim.ResetTrigger("Idle");
            playerAnim.ResetTrigger("walkBackward");
        }
        if (z == 0)
        {
            playerAnim.ResetTrigger("Run");
            playerAnim.ResetTrigger("walkBackward");
            playerAnim.SetTrigger("Idle");
        }
        if (z < 0)
        {
            playerAnim.SetTrigger("walkBackward");
            playerAnim.ResetTrigger("Idle");
            playerAnim.ResetTrigger("Run");
        }
    }

}
