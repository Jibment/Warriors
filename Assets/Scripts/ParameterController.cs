using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterController : MonoBehaviour
{
    // Start is called before the first frame update
    //�v���C���[�̃p�����[�^
    public int playerHP;
    public int playerAttackPower;
    public float playerSpeed = 8.0f;
    //�G�̃p�����[�^
    public int enemyHP;
    public int enemyAttackPower;
    public float enemyAttackInterval;

    //�ڕW���j��
    public int maxKOcount = 50;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
