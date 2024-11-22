using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //private Enemy mEnemy;
    private GameObject enemy;
    private float Life;
    private float Attack;
    private float Cnt;

    //public Enemy(float life)
    //{
    //    this.Life = life;
    //}
    private void FixedUpdate()
    {
        EnemyUpDate();
    }

    protected virtual void EnemyUpDate()
    {
        Debug.Log("Enemy オーバーロード失敗");
    }
    protected GameObject GetObject()
    {
        return this.enemy;
    }
    protected void SetObject(GameObject obj)
    {
        this.enemy = obj;
    }
    protected Vector3 GetPosition()
    {
        return this.enemy.transform.position;
    }
    protected void SetPosition(Vector3 pos)
    {
        this.enemy.transform.position = pos;
    }
    protected float GetLife()
    {
        return this.Life;
    }  
    protected void SetLife(float life)
    {
        this.Life = life;
    }
    protected float GetAttack()
    {
        return this.Attack;
    }
    protected void SetAttack(float attack)
    {
        this.Attack = attack;
    }
    protected float GetCnt()
    {
        return this.Cnt;
    }
    protected void SetCnt(float cnt)
    {
        this.Cnt = cnt;
    }
    protected void ResetCnt()
    {
        this.Cnt = 0;
    }
    protected void EnemyDestroy(GameObject obj)
    {
        Destroy(obj);
    }
}
