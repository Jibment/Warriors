using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    //ゾンビオブジェクトの読み込むもの
    private Animator animator;
    private Rigidbody rb;
    private MeshRenderer mesh;
    private NavMeshAgent agent;
    public Collider attackCollider;

    private int hp;
    bool isDeath;    
    public GameObject target; //プレイヤーオブジェクトを読み込む

   
    float enemyAttackInterval;
    float intervalTime;

    private UiController _ui;
    private ParameterController parameter;

    private Image _hpBarcurrent;
    private float currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("RPGHeroHP");
        _ui = GameObject.Find("GameSystem").GetComponent<UiController>();
        parameter = GameObject.Find("ParameterSystem").GetComponent<ParameterController>();
        //パラメータ代入
        //敵の攻撃感覚をランダムで指定
        enemyAttackInterval = Random.Range(5.0f, 10.0f - parameter.enemyAttackInterval); 
        intervalTime = Random.Range(0.0f + parameter.enemyAttackInterval, 5.0f);       
        hp = parameter.enemyHP;

        _hpBarcurrent = this.transform.Find("HP").transform.Find("enemyHPBarCurrent").gameObject.GetComponent<Image>();
        currentHealth = parameter.enemyHP;

        attackCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other) //衝突時の処理
    {
        DamageController damager = other.GetComponent<DamageController>();
        if(damager != null)
        {
            //animator.SetTrigger("Gethit"); //衝突したらのけぞりモーション(アニメーションなし)
            Damage();
        }
    }

    //ダメージ処理
    void Damage() 
    {
        if (hp < 0) return;
        hp -= parameter.playerAttackPower;
        if(hp <= 0)
        {
            agent.updatePosition = false; //倒れた時に動きを止める
            animator.SetTrigger("Gethit");
            _ui.KOcount += 1;
            Destroy(gameObject, 3f);     //倒れた時に消滅
            ItemDrop();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            //navMeshAgentの操作
            agent.destination = target.transform.position;
        }

        //移動の処理
        float dis = Vector3.Distance(this.transform.position, target.transform.position); //キャラクターとの距離
        if(dis < 1.5) //距離が範囲内になったら
        {
            intervalTime += Time.deltaTime;
            if(intervalTime >= enemyAttackInterval)
            {
                intervalTime = 0.0f;
                enemyAttackInterval = Random.Range(5.0f - parameter.enemyAttackInterval, 10.0f - parameter.enemyAttackInterval);
                animator.SetTrigger("Attack");
            }
            checkMove(dis);
        }
        else
        {
            checkMove(dis);
        }

        currentHealth = Mathf.Clamp(hp, 0, parameter.enemyHP);
        _hpBarcurrent.fillAmount = currentHealth / parameter.enemyHP; //現在のhp÷MAXHP

    }

    //移動するか停止するかの処理
    private void checkMove(float dis)
    {
        if (agent == null) return;
        if(hp <= 0)
        {
            agent.updatePosition = false;
            agent.updateRotation = false;
        }
        else if(dis < 1.5)
        {
            animator.SetTrigger("inRange");
            agent.updatePosition = false;
        }
        else
        {
            animator.SetTrigger("outRange");
            agent.updatePosition = true;
        }
    }

    private void ItemDrop()
    {
        int random = Random.Range(1, 10);
        // 1は回復アイテム
        // 2は攻撃アイテム enumで整理？
        if(random == 1)
        {
            //DropHeal
            GameObject obj = (GameObject)Resources.Load("Item_Heal");
            GameObject instance = (GameObject)Instantiate(obj, this.transform.position, Quaternion.identity);
        }else if(random == 2)
        {
            //DropAttack
            GameObject obj = (GameObject)Resources.Load("Item_Attack");
            GameObject instance = (GameObject)Instantiate(obj, this.transform.position, Quaternion.identity);
        }
        else
        {
            return;
        }
        return;
    }

    public void OffColliderAttack()
    {
        attackCollider.enabled = false;
    }

    public void OnColliderAttack()
    {
        attackCollider.enabled = true;
    }
}
