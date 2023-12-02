using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleAnim : MonoBehaviour
{
    public int _rotSpeed = -60;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, _rotSpeed * Time.deltaTime);
    }

    void resetAnim()
    {
        transform.rotation = Quaternion.identity;
    }
}
