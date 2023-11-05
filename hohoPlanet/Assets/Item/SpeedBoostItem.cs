using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostItem : MonoBehaviour
{
    public float speedBoostAmount = 2.0f; // ������ų �ӵ���

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ ��������
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                // �÷��̾��� �ӵ��� ������Ű��
                playerController.IncreaseSpeed(speedBoostAmount);
                // �������� ȹ�������Ƿ� �ش� ������ ����
                Destroy(gameObject);
            }
        }
    }
}
