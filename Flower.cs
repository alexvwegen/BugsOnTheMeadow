using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public int PollenCount;
    public int Viva;
    public string[] names;
    public string FlowerName;

    // Start is called before the first frame update
    void Start()
    {
        PollenCount = 10;
        Viva = 30;
        int n = Random.Range(0, names.Length);
        FlowerName = names[n];
    }

    // Update is called once per frame
    void Update()
    {
        if (PollenCount <= 0)
        {
            gameObject.transform.parent.localScale = Vector3.one * 0.1f;
            gameObject.transform.parent.GetComponent<PlantLife>().Viva += 10;
            Destroy(gameObject);
        }
    }
}
