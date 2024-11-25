using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassEnemy_Patrol : ClassEnemy_State
{
    //public EnemyMove move;
    [SerializeField]
    public int NextPoint = 0;//次の移動のseed値
    protected override void EnemyUpdate()
    {
        //base.EnemyUpdate();
        //探索("Patrol")の時のみ更新処理
        if (enemyState == (int)EnemyState.Patrol)
        {
            Debug.Log("Patrol");

            //指定地点に移動したら待機(Idel)に遷移
            if (Enemy_Move(SetNextPosition(NextPoint % 8)))
            {
                //次の移動地点の更新
                NextPoint++;

                //待機
                Debug.Log("Patrol->Idel");
                SetState(EnemyState.Idel);
                return;
            }
        }
    }
    private Vector3 SetNextPosition(int point)
    {
        //seed値をもとに移動先を指定
        //適当に決めました
        float Length = 3;
        float posY = Enemy.transform.position.y;

        Vector3 pos = new(Length, posY, Length);
        switch (point)
        {
            case 0:
                pos = new Vector3(1 * Length, posY, 1 * Length);
                break;
            case 1:
                pos = new Vector3(0 * Length, posY, 1 * Length);
                break;
            case 2:
                pos = new Vector3(-1 * Length, posY, 1 * Length);
                break;
            case 3:
                pos = new Vector3(-1 * Length, posY, 0 * Length);
                break;
            case 4:
                pos = new Vector3(-1 * Length, posY, -1 * Length);
                break;
            case 5:
                pos = new Vector3(0 * Length, posY, -1 * Length);
                break;
            case 6:
                pos = new Vector3(1 * Length, posY, -1 * Length);
                break;
            case 7:
                pos = new Vector3(1 * Length, posY, 0 * Length);
                break;
            default:
                break;
        }
        return pos;
    }
    private bool Enemy_Move(Vector3 pos)
    {
        //Enemyが指定された地点に移動したか調べて移動していなければ移動
        //指定地点についたか判断
        if (Enemy_GetPosition().x >= pos.x - 0.01 && Enemy_GetPosition().x <= pos.x + 0.01)
            if (Enemy_GetPosition().z >= pos.z - 0.01 && Enemy_GetPosition().z <= pos.z + 0.01)
            {
                //浮動小数点数対策
                Enemy.transform.position = pos;

                //ついたらtrue
                Debug.Log("Pos = true");
                return true;
            }

        //移動
        {
            float move = 3.0f;
            Enemy.transform.position =
                Vector3.MoveTowards(Enemy.transform.position, pos, move * Time.deltaTime);
        }

        return false;
    }

    //private void Start()
    //{
    //    move = new EnemyMoveIdle();
    //}
}
