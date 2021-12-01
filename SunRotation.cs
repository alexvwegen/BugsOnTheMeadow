using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour
{
    public float speed;
    public float DayTime;
    public int Days;
    // Start is called before the first frame update
    void Start()
    {
        DayTime = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0f, speed, 0f, Space.World);
        DayTime += speed;

        if (DayTime >= 360)
        {
            Days++;
            GameObject.Find("MeadowManager").GetComponent<MeadowManager>().Days = Days;
            DayTime = 0;
        }
    }
}
