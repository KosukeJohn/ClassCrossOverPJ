using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[��UI�̍��W���擾
        Vector3 Plpos=Player.transform.position; 
        Vector3 UIPos=UI.transform.position;
        //�v���C���[��UI�̋������擾
        float dis=Vector3.Distance(Plpos, UIPos);
        //�v���C���[��UI�̋��������ȉ��Ȃ�UI���\��
        if(dis<10.5f)
        {
            UI.SetActive(true);
        }
        //�v���C���[��UI�̋���������Ȃ�UI��\��
        if (dis>20.0f)
        {
            UI.SetActive(false);
        }
    }
}
