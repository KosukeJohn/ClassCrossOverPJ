using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {//Ç‡ÇµÉGÉäÉAì‡Ç…ì¸Ç¡ÇΩÇÁí«ê’Ç∑ÇÈ
       if (player.GetComponent<Player_cont>().isArea == true)
        {
            agent.destination = player.transform.position;
            
        }
        
    }
}