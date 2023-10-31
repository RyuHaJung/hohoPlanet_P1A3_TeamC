using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKeeper : MonoBehaviour
{
    public static int hasKeys = 0;
    public static int hasArrows = int.MaxValue; //무한대로 설정
    // Start is called before the first frame update
    void Start()
    {
        hasKeys = PlayerPrefs.GetInt("Keys");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //아이템 저장하기
    public static void SaveItem()
    {
        PlayerPrefs.SetInt("Keys", hasKeys);
    }
}
