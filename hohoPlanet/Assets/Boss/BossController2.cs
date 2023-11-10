using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController2 : MonoBehaviour
{
    public int hp = 10;
    public float reactionDistance = 7.0f;

    public GameObject bulletPrefab;     // 총알
    public float shootSpeed = 5.0f;     // 총알 속도

    bool inAttack = false;

    // 내가 추가한 부분
    public float barrageSpeed = 5.0f; // 탄막 속도
    public int numberOfProjectilesPattern1 = 8; // 1번째 패턴의 발사할 총알 개수
    public int numberOfProjectilesPattern2 = 10; // 2번째 패턴의 발사할 총알 개수

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

                    // 패턴 변경
                    StartCoroutine(SwitchAttackPattern());
                }
                else if (dist > reactionDistance && inAttack)
                {
                    inAttack = false;
                    GetComponent<Animator>().Play("BossIdle");
                    // 패턴 취소
                    StopAllCoroutines();
                }
            }
            else
            {
                inAttack = false;
                GetComponent<Animator>().Play("BossIdle");
                // 패턴 취소
                StopAllCoroutines();
            }
        }
    }

    IEnumerator SwitchAttackPattern()
    {
        while (true)
        {
            // 1번째 패턴 실행
            Attack();
            yield return new WaitForSeconds(3f); // 3초 간격으로 패턴 변경

            // 2번째 패턴 실행
            Attack();
            yield return new WaitForSeconds(3f); // 3초 간격으로 패턴 변경
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            hp--;
            if (hp <= 0)
            {
                // 사망!
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<Animator>().Play("BossDead");
                Destroy(gameObject, 1);
            }
        }
    }

    // 1번째 패턴
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

    // 2번째 패턴
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

    // 공격
    void Attack()
    {
        // 패턴에 따라 다른 공격 실행
        if (isPattern1)
        {
            // 첫 번째 패턴 실행
            AttackPattern1();
        }
        else
        {
            // 두 번째 패턴 실행
            AttackPattern2();
        }

        // 패턴 변경
        isPattern1 = !isPattern1;
    }
}