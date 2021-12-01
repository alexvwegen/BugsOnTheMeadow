using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent player;
    public float flyingHeight;
    public InsectStats stats;
    public Slider PollenBar;
    public Text PlantName;
    public AudioSource Buzz;
    public AudioSource Drink;

    private GameObject FlowerInReach;
    private Flower Flower;
    private bool isCollecting;
    // Start is called before the first frame update
    void Start()
    {
        stats.CollectedPollen = 0;
        isCollecting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitpoint;

            if (Physics.Raycast(ray, out hitpoint))
            {
                player.SetDestination(hitpoint.point);
            }
        }

        if (Input.GetMouseButtonDown(1) && stats.CollectedPollen >= 30)
        {
            RaycastHit hitpoint;

            if (Physics.Raycast(transform.position, new Vector3(0f, -1f, 0f), out hitpoint, Mathf.Infinity, layerMask: 1 << 10))
            {
                hitpoint.transform.gameObject.GetComponent<GridManager>().SeedTargettedPlant(hitpoint.point);
            }
            stats.CollectedPollen -= 30;
        }


        if (Input.GetKeyDown(KeyCode.Space) && Flower)
        {
            StartCoroutine(CollectPollen());   
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopAllCoroutines();
            Drink.Stop();
            PollenBar.gameObject.SetActive(false);
            isCollecting = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flower"))
        {
            FlowerInReach = other.gameObject;
            Flower = FlowerInReach.GetComponent<Flower>();
            //PollenBar.value = Flower.PollenCount;
            PlantName.text = Flower.FlowerName;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Flower") && Flower)
        {
            ResetTarget();
        }
    }

    private IEnumerator CollectPollen()
    {
        while (Input.GetKey(KeyCode.Space))
        {
            if (!Drink.isPlaying)
            {
                Drink.Play();
            }

            if (!PollenBar.IsActive())
            {
                PollenBar.gameObject.SetActive(true);
            }

            if (Flower.PollenCount <= 0)
            {
                ResetTarget();
            }

            stats.CollectedPollen++;
            Flower.PollenCount--;
            PollenBar.value = Flower.PollenCount;
            yield return new WaitForSeconds(0.13f);
        };
        yield break;
    }

    public void ResetTarget()
    {
        StopAllCoroutines();
        Drink.Stop();
        PollenBar.gameObject.SetActive(false);
        isCollecting = false;
        FlowerInReach = null;
        Flower = null;
    }
}
