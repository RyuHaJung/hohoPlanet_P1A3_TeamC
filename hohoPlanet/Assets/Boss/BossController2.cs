using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController2 : MonoBehaviour
{
    // ü��
    public int hp = 10;
    // ���� �Ÿ�
    public float reactionDistance = 7.0f;

    public GameObject bulletPrefab;     //�Ѿ�
    public float shootSpeed = 5.0f;     //�Ѿ� �ӵ�

    //���������� ����
    bool inAttack = false;

    //���� �߰� �Ѱ�
    public float barrageSpeed = 5.0f; //ź�� �ӵ�
    public int numberOfProjectiles = 5; //�߻��� �Ѿ� ����

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (hp > 0)
        {
            //Player ���� ������Ʈ ��������
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                //�÷��̾���� �Ÿ� Ȯ��
                Vector3 plpos = player.transform.position;
                float dist = Vector2.Distance(transform.position, plpos);
                if (dist <= reactionDistance && inAttack == false)
                {
                    //���� �� & ���� ���� �ƴϸ� ���� �ִϸ��̼�
                    inAttack = true;
                    // �ִϸ��̼� ����
                    GetComponent<Animator>().Play("BossAttack");
                }
                else if (dist > reactionDistance && inAttack)
                {
                    inAttack = false;
                    // �ִϸ��̼� ����
                    GetComponent<Animator>().Play("BossIdle");
                }
            }
            else
            {
                inAttack = false;
                // �ִϸ��̼� ����
                GetComponent<Animator>().Play("BossIdle");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            //������
            hp--;
            if (hp <= 0)
            {
                //�����
                //�浹 ���� ��Ȱ��
                GetComponent<CircleCollider2D>().enabled = false;
                // �ִϸ��̼� ����
                GetComponent<Animator>().Play("BossDead");
                //1�� �ڿ� ����
                Destroy(gameObject, 1);

                //SE��� (���� ���)
                //SoundManager.soundManager.SEPlay(SEType.BossKilled);
            }
        }
    }
    //����
    void Attack()
    {
        //�߻� ��ġ ������Ʈ ��������
        Transform tr = transform.Find("gate");
        GameObject gate = tr.gameObject;

        //�÷��̾� ã��
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                float angle = i * (360 / numberOfProjectiles); //ź���� ������ ����

                //Prefab���� �Ѿ� ������Ʈ �����(���� �������� ȸ��)
                Quaternion r = Quaternion.Euler(0, 0, angle);
                GameObject bullet = Instantiate(bulletPrefab, gate.transform.position, r);

                //ź�� �ӵ��� ���� ����
                float rad = angle * Mathf.Deg2Rad;
                float x = Mathf.Cos(rad);
                float y = Mathf.Sin(rad);
                Vector3 v = new Vector3(x, y) * barrageSpeed;

                //�߻�
                Rigidbody2D rbody = bullet.GetComponent<Rigidbody2D>();
                rbody.AddForce(v, ForceMode2D.Impulse);
            }
        }
    }
}