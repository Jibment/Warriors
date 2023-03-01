using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterController : MonoBehaviour
{
    // Start is called before the first frame update
    //プレイヤーのパラメータ
    public int playerHP;
    public int playerAttackPower;
    public float playerSpeed = 8.0f;
    //敵のパラメータ
    public int enemyHP;
    public int enemyAttackPower;
    public float enemyAttackInterval;

    //目標撃破数
    public int maxKOcount = 50;

    public void SetParameterEasy()
    {
        playerHP = 100;
        playerAttackPower = 30;
        enemyHP = 20;
        enemyAttackPower = 20;
        enemyAttackInterval = 0f;
    }

    public void SetParameterNormal()
    {
        playerHP = 80;
        playerAttackPower = 30;
        enemyHP = 50;
        enemyAttackPower = 20;
        enemyAttackInterval = 1f;
    }

    public void SetParameterHard()
    {
        playerHP = 50;
        playerAttackPower = 30;
        enemyHP = 100;
        enemyAttackPower = 20;
        enemyAttackInterval = 2f;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
