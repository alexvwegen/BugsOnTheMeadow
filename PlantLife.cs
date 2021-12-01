using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLife : MonoBehaviour
{

    public Transform[] Flower;
    public float InitSize;
    public float MaxSize;
    public float GrowSpeed;
    public int Viva;
    public Transform BloomPoint;

    private bool hasBloomed;
    public Vector3 root;
    void Awake()
    {
        gameObject.transform.localScale = Vector3.one * Random.Range(InitSize, MaxSize);
        hasBloomed = false;
        Viva = 30;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("MeadowManager").GetComponent<MeadowManager>().Plants++;
        root = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.localScale.x < MaxSize)
        {
            hasBloomed = false;
            gameObject.transform.localScale += Vector3.one * 0.01f * GrowSpeed;
            gameObject.transform.position = root + new Vector3(0f, 0f + transform.localScale.y / 2, 0f);
        }   

        if (gameObject.transform.localScale.x >= MaxSize && !hasBloomed)
        {
            hasBloomed = true;
            Instantiate(Flower[Random.Range(0, Flower.Length)], BloomPoint.position, Quaternion.identity, gameObject.transform);
        }
    }
}
