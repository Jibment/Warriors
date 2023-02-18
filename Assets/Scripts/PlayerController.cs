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
        //�p�����[�^���
        hp = parameter.playerHP;
        Speed = parameter.playerSpeed;
        isDeath = false;
    }

    private void OnTriggerEnter(Collider other) //�Փˎ��̏���
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
            isDeath = true; //���S���Ă���t���O���N��
            animator.SetTrigger("Death");
            rb.velocity = Vector3.zero; //rigidbody�̒�~
            animator.SetTrigger("Death");
            StartCoroutine(GameOver());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDeath) //����ł��Ȃ����
        {
            //�ړ�����
            x = Input.GetAxisRaw("Horizontal");
            z = Input.GetAxisRaw("Vertical");

            //�v���C���[�̈ړ��֌W
            Vector3 target_dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            rb.velocity = new Vector3(x, 0, z) * Speed;       //�������x
            animator.SetFloat("Walk", rb.velocity.magnitude);

            if (target_dir.magnitude > 0.01f)
            {
                Quaternion rotation = Quaternion.LookRotation(target_dir);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * smooth);
            }

            //�U������
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
        //3�b���GameOver�V�[���Ɉړ�����
        yield return  new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");
    }

}
