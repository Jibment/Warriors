using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float x;
    private float z;
    public float Speed;
    public int hp;
    float smooth = 10f;
    private Vector3 latestPos;

    private bool isDeath;

    private Rigidbody rb;
    private Animator animator;
    public Collider attackCollider;

    private ParameterController parameter;



    // Start is called before the first frame update
    void Start()
    {
        attackCollider.enabled = false;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        parameter = GameObject.Find("ParameterSystem").GetComponent<ParameterController>();
        //パラメータ代入
        hp = parameter.playerHP;
        Speed = parameter.playerSpeed;
        isDeath = false;
    }

    private void OnTriggerEnter(Collider other) //衝突時の処理
    {
        if (!isDeath)
        {
            DamageController damager = other.GetComponent<DamageController>();
            if (damager != null)
            {
                animator.SetTrigger("Gethit");
                Damage();
            }
        }
    }

    void Damage()
    {
        hp -= parameter.enemyAttackPower;
        if (hp <= 0)
        {
            isDeath = true; //死亡しているフラグを起動
            animator.SetTrigger("Death");
            rb.velocity = Vector3.zero; //rigidbodyの停止
            animator.SetTrigger("Death");
            StartCoroutine(GameOver());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDeath) //死んでいなければ
        {
            //移動入力
            x = Input.GetAxisRaw("Horizontal");
            z = Input.GetAxisRaw("Vertical");

            //プレイヤーの移動関係
            Vector3 target_dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            rb.velocity = new Vector3(x, 0, z) * Speed;       //歩く速度
            animator.SetFloat("Walk", rb.velocity.magnitude);

            if (target_dir.magnitude > 0.01f)
            {
                Quaternion rotation = Quaternion.LookRotation(target_dir);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * smooth);
            }

            //攻撃入力
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Attack");
            }
        }
    }

    public void OffColliderAttack()
    {
        attackCollider.enabled = false;
    }

    public void OnColliderAttack()
    {
        attackCollider.enabled = true;
    }

    private IEnumerator GameOver()
    {
        //3秒後にGameOverシーンに移動する
        yield return  new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");
    }

}
