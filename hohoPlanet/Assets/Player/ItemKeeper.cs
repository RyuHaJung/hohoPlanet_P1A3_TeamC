using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKeeper : MonoBehaviour
{
    public static int hasKeys = 0;
    public static int hasArrows = int.MaxValue; //���Ѵ�� ����
    // Start is called before the first frame update
    void Start()
    {
        hasKeys = PlayerPrefs.GetInt("Keys");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //������ �����ϱ�
    public static void SaveItem()
    {
        PlayerPrefs.SetInt("Keys", hasKeys);
    }
}
