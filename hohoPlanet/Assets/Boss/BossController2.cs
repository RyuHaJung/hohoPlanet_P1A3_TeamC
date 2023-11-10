using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController2 : MonoBehaviour
{
    public int hp = 10;
    public float reactionDistance = 7.0f;

    public GameObject bulletPrefab;     // �Ѿ�
    public float shootSpeed = 5.0f;     // �Ѿ� �ӵ�

    bool inAttack = false;

    // ���� �߰��� �κ�
    public float barrageSpeed = 5.0f; // ź�� �ӵ�
    public int numberOfProjectilesPattern1 = 8; // 1��° ������ �߻��� �Ѿ� ����
    public int numberOfProjectilesPattern2 = 10; // 2��° ������ �߻��� �Ѿ� ����

    private bool isPattern1 = true;

    void Update()
    {
        if (hp > 0)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Vector3 plpos = player.transform.position;
                float dist = Vector2.Distance(transform.position, plpos);

                if (dist <= reactionDistance && !inAttack)
                {
                    inAttack = true;
                    GetComponent<Animator>().Play("BossAttack");

                    // ���� ����
                    StartCoroutine(SwitchAttackPattern());
                }
                else if (dist > reactionDistance && inAttack)
                {
                    inAttack = false;
                    GetComponent<Animator>().Play("BossIdle");
                    // ���� ���
                    StopAllCoroutines();
                }
            }
            else
            {
                inAttack = false;
                GetComponent<Animator>().Play("BossIdle");
                // ���� ���
                StopAllCoroutines();
            }
        }
    }

    IEnumerator SwitchAttackPattern()
    {
        while (true)
        {
            // 1��° ���� ����
            Attack();
            yield return new WaitForSeconds(3f); // 3�� �������� ���� ����

            // 2��° ���� ����
            Attack();
            yield return new WaitForSeconds(3f); // 3�� �������� ���� ����
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            hp--;
            if (hp <= 0)
            {
                // ���!
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<Animator>().Play("BossDead");
                Destroy(gameObject, 1);
            }
        }
    }

    // 1��° ����
    void AttackPattern1()
    {
        Transform tr = transform.Find("gate");
        GameObject gate = tr.gameObject;

        for (int i = 0; i < numberOfProjectilesPattern1; i++)
        {
            float angle = i * (360f / numberOfProjectilesPattern1);

            Quaternion r = Quaternion.Euler(0, 0, angle);
            GameObject bullet = Instantiate(bulletPrefab, gate.transform.position, r);

            float rad = angle * Mathf.Deg2Rad;
            float x = Mathf.Cos(rad);
            float y = Mathf.Sin(rad);
            Vector3 v = new Vector3(x, y) * barrageSpeed;

            Rigidbody2D rbody = bullet.GetComponent<Rigidbody2D>();
            rbody.AddForce(v, ForceMode2D.Impulse);
        }
    }

    // 2��° ����
    void AttackPattern2()
    {
        Transform tr = transform.Find("gate");
        GameObject gate = tr.gameObject;

        for (int i = 0; i < numberOfProjectilesPattern2; i++)
        {
            float angle = i * (360f / numberOfProjectilesPattern2);

            Quaternion r = Quaternion.Euler(0, 0, angle);
            GameObject bullet = Instantiate(bulletPrefab, gate.transform.position, r);

            float rad = angle * Mathf.Deg2Rad;
            float x = Mathf.Cos(rad);
            float y = Mathf.Sin(rad);
            Vector3 v = new Vector3(x, y) * barrageSpeed;

            Rigidbody2D rbody = bullet.GetComponent<Rigidbody2D>();
            rbody.AddForce(v, ForceMode2D.Impulse);
        }
    }

    // ����
    void Attack()
    {
        // ���Ͽ� ���� �ٸ� ���� ����
        if (isPattern1)
        {
            // ù ��° ���� ����
            AttackPattern1();
        }
        else
        {
            // �� ��° ���� ����
            AttackPattern2();
        }

        // ���� ����
        isPattern1 = !isPattern1;
    }
}