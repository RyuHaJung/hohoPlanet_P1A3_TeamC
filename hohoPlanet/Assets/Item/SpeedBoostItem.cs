using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostItem : MonoBehaviour
{
    public float speedBoostAmount = 2.0f; // 증가시킬 속도량

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어 컨트롤러 스크립트 가져오기
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                // 플레이어의 속도를 증가시키기
                playerController.IncreaseSpeed(speedBoostAmount);
                // 아이템을 획득했으므로 해당 아이템 제거
                Destroy(gameObject);
            }
        }
    }
}
