using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    //�]���r�I�u�W�F�N�g�̓ǂݍ��ނ���
    private Animator animator;
    private Rigidbody rb;
    private MeshRenderer mesh;
    private NavMeshAgent agent;
    public Collider attackCollider;

    private int hp;
    bool isDeath;    
    public GameObject target; //�v���C���[�I�u�W�F�N�g��ǂݍ���

   
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
        //�p�����[�^���
        //�G�̍U�����o�������_���Ŏw��
        enemyAttackInterval = Random.Range(5.0f, 10.0f - parameter.enemyAttackInterval); 
        intervalTime = Random.Range(0.0f + parameter.enemyAttackInterval, 5.0f);       
        hp = parameter.enemyHP;

        _hpBarcurrent = this.transform.Find("HP").transform.Find("enemyHPBarCurrent").gameObject.GetComponent<Image>();
        currentHealth = parameter.enemyHP;

        attackCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other) //�Փˎ��̏���
    {
        DamageController damager = other.GetComponent<DamageController>();
        if(damager != null)
        {
            //animator.SetTrigger("Gethit"); //�Փ˂�����̂����胂�[�V����(�A�j���[�V�����Ȃ�)
            Damage();
        }
    }

    //�_���[�W����
    void Damage() 
    {
        if (hp < 0) return;
        hp -= parameter.playerAttackPower;
        if(hp <= 0)
        {
            agent.updatePosition = false; //�|�ꂽ���ɓ������~�߂�
            animator.SetTrigger("Gethit");
            _ui.KOcount += 1;
            Destroy(gameObject, 3f);     //�|�ꂽ���ɏ���
            ItemDrop();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            //navMeshAgent�̑���
            agent.destination = target.transform.position;
        }

        //�ړ��̏���
        float dis = Vector3.Distance(this.transform.position, target.transform.position); //�L�����N�^�[�Ƃ̋���
        if(dis < 1.5) //�������͈͓��ɂȂ�����
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
        _hpBarcurrent.fillAmount = currentHealth / parameter.enemyHP; //���݂�hp��MAXHP

    }

    //�ړ����邩��~���邩�̏���
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
        // 1�͉񕜃A�C�e��
        // 2�͍U���A�C�e�� enum�Ő����H
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
