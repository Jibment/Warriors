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

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
