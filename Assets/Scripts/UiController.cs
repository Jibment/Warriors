using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public int KOcount = 0;
    public Text KOtext; //KO表示テキスト
    private PlayerController player;
    private ParameterController parameter;
    [SerializeField] private Image _hpBarcurrent;

    private float currentHealth;

    public int maxKOCount;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("RPGHeroHP").GetComponent<PlayerController>();
        parameter = GameObject.Find("ParameterSystem").GetComponent<ParameterController>();
        maxKOCount = parameter.maxKOcount;

        currentHealth = player.hp;
        
    }

    // Update is called once per frame
    void Update()
    {
        //KO表示のテキスト変更
        KOtext.text = "K.O.Count :" + KOcount;

        currentHealth = Mathf.Clamp(player.hp, 0, parameter.playerHP);
        _hpBarcurrent.fillAmount = currentHealth / parameter.playerHP; //現在のhp÷MAXHP


        //KOが一定以上になったらクリア画面を表示
        if (KOcount > maxKOCount)
        {
            SceneManager.LoadScene("Clear");
        }
    }
}
