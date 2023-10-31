using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int hp = 3;
    public GameObject itemPrefab; // 아이템 프리팹
    public Sprite newSprite; // 변경할 새로운 스프라이트 이미지

    public int arrangeId = 0;   //배치 식별에 사용

    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // 스프라이트 렌더러 가져오기
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with Trap");
            // 적의 체력을 5로 증가
            hp = 5;
            //PlayerController.hp -= 1;

            // 이미지 변경
            if (spriteRenderer != null && newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }
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

                //배치 Id 저장
                SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);

            }
        }
    }
}

