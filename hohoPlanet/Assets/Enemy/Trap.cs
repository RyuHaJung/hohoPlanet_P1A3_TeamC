using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int hp = 3;
    public GameObject itemPrefab; // ������ ������
    public Sprite newSprite; // ������ ���ο� ��������Ʈ �̹���

    public int arrangeId = 0;   //��ġ �ĺ��� ���

    private SpriteRenderer spriteRenderer; // ��������Ʈ ������

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // ��������Ʈ ������ ��������
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with Trap");
            // ���� ü���� 5�� ����
            hp = 5;
            //PlayerController.hp -= 1;

            // �̹��� ����
            if (spriteRenderer != null && newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }
        }

        if (collision.gameObject.CompareTag("Arrow"))
        {
            // ������
            hp--;
            if (hp <= 0)
            {
                // ���!
                // =====================
                // ��� ����
                // =====================

                // �浹 ���� ��Ȱ��
                GetComponent<CapsuleCollider2D>().enabled = false;
                // ������ ����
                Instantiate(itemPrefab, transform.position, Quaternion.identity);

                // 0.5�� �Ŀ� ����
                Destroy(gameObject, 0.05f);

                //��ġ Id ����
                SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);

            }
        }
    }
}

