using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    private Image HpBarCurrent;
    private float currentHealth;
    private ParameterController parameter;
    private EnemyController enemy;

    // Start is called before the first frame update
    void Start()
    {
        parameter = GameObject.Find("ParameterSystem").GetComponent<ParameterController>();
        enemy = GetComponent<EnemyController>();
        HpBarCurrent = this.transform.Find("HP").transform.Find("enemyHPBarCurrent").gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //体力を参照してHPバーを表示する．
        currentHealth = Mathf.Clamp(enemy.hp, 0, parameter.enemyHP);
        HpBarCurrent.fillAmount = currentHealth / parameter.enemyHP;
    }
}
