using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PollenCounter : MonoBehaviour
{
    public GameObject Bee;
    public Text text;

    private InsectStats beeparams;
    // Start is called before the first frame update
    void Start()
    {
        beeparams = Bee.GetComponent<InsectStats>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = beeparams.CollectedPollen.ToString();
    }
}
