using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int hp = 3;
    public GameObject itemPrefab; // 아이템 프리팹
    Rigidbody2D rbody;      // Rigidbody 2D

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy collided with Player");
            // 정적으로 선언된 플레이어의 HP 변수에 접근합니다.
            //PlayerController.hp -= 1;
        }

        if (collision.gameObject.CompareTag("Arrow"))
        {
            // 데미지
            hp--;
            if (hp <= 0)
            {
                // 사망!
                // =====================
                // 사망 연출
                // =====================

                // 충돌 판정 비활성
                GetComponent<CapsuleCollider2D>().enabled = false;

                // 아이템 생성
                Instantiate(itemPrefab, transform.position, Quaternion.identity);

                // 0.5초 후에 제거
                Destroy(gameObject, 0.05f);

            }
        }
    }
}
