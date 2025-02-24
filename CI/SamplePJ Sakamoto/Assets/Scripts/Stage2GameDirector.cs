using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2GameDirector : MonoBehaviour
{   
    private GameObject enemy;
    private GameObject house;
    private GameObject player;
    private Collider coll;
    private bool EnemyInstanceFlag;
    private bool HouseInstanceFlag;
    private float HouseInstancePos = 75f;

    [Header("各種位置情報")]
    [SerializeField] private float EnemyInstancePos;

    [Header("プレファブ")]
    [SerializeField] private GameObject prefab_NewGame;
    [SerializeField] private GameObject prefab_Continue;
    [SerializeField] private GameObject House;

    private void Start()
    {
        player = GameObject.Find("Player");
        coll = GetComponent<Collider>();
        EnemyInstanceFlag = true;
        HouseInstanceFlag = true;
    }
    private void Update()
    {
        if (HouseInstanceFlag)
        {
            if (player.transform.position.x <= HouseInstancePos)
            {
                HouseInstanceFlag = false;
                house = Instantiate(House);
                house.transform.position =
                    new Vector3(131.410004f, 15.4899998f, 4.28405762f);
            }
        }

        PlayerMoveLock();

        if (EnemyInstanceFlag)
        {
            if (player.transform.position.x >= EnemyInstancePos)
            {
                EnemyInstanceFlag = false;

                if (House) { enemy = Instantiate(prefab_NewGame); }
                else { enemy = Instantiate(prefab_Continue); }

                enemy.transform.position =
                    new Vector3(EnemyInstancePos, -8.92000008f, 8.88000011f);
            }
        }

        if (enemy)
        {
            if (player.transform.position.x >= 236.3461f)
            {
                Destroy(enemy);
            }
        }
    }

    private void PlayerMoveLock()
    {
        if (house)
        {
            coll.enabled = true;
        }
        else 
        {
            if (player.transform.position.x <= EnemyInstancePos) { return; }

            GameObject camera = GameObject.Find("Virtual Camera");
            Transform _transform = GameObject.Find("CameraTranse").GetComponentInChildren<Transform>();
            camera.GetComponent<CinemachineVirtualCamera>().Follow = _transform;
            coll.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            GameObject camera = GameObject.Find("Virtual Camera");
            if(house)
            camera.GetComponent<CinemachineVirtualCamera>().Follow = house.transform;
        }
    }
}
