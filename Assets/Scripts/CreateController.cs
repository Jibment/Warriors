using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreateController : MonoBehaviour
{
    [SerializeField] GameObject zombie;
    private Vector3[] respownPoint = new Vector3[3];

    private float elapsedTime;
    public float appearNextTime;

    private int maxZombie = 20;
    // Start is called before the first frame update
    void Start()
    {
        appearNextTime = 0.5f;
        elapsedTime = 0f;
        respownPoint[0] = new Vector3(-7, 0, 30);
        respownPoint[1] = new Vector3(-22, 0, 12);
        respownPoint[2] = new Vector3(19, 0, 8);
        for (int i = 0; i < 10; i++)
        {
            StartCoroutine(CreateZombie());
        }
    }

    private IEnumerator CreateZombie()
    {
            int random = Random.Range(0, 3);
            GameObject _character = Instantiate(zombie, respownPoint[random], Quaternion.identity, this.transform); //GameSystem�I�u�W�F�N�g�̎q�Ƃ���Instantiate

            yield return new WaitForSeconds(0.5f);
            _character.GetComponent<NavMeshAgent>().enabled = true; //0.5�b�҂���AI���N��(�ŏ�����true���ƃo�O��)
    }

    // Update is called once per frame
    void Update()
    {       
        elapsedTime += Time.deltaTime;
        //��莞�Ԍo�߂��o����������łȂ��ꍇ
        if (elapsedTime > appearNextTime && this.gameObject.transform.childCount < maxZombie)
        {
            elapsedTime = 0f;
            StartCoroutine(CreateZombie());
        }
    }
}
