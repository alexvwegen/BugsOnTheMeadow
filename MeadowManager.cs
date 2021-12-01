using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class MeadowManager : MonoBehaviour
{
    public GameObject Bee;
    public int Bugs;
    public int Plants;
    public int Days;

    public Text PollenCount;
    public Text BugCount;
    public Text PlantCount;
    public Text DayCount;
    public Text GameOver;
    public Text Result;

    public List<GameObject> LivingPlants;
    private InsectStats BeeStats;
    // Start is called before the first frame update
    void Start()
    {
        BeeStats = Bee.GetComponent<InsectStats>();
        foreach (GameObject plant in GameObject.FindGameObjectsWithTag("Plant"))
        {
            LivingPlants.Add(plant);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (LivingPlants.Count <= 0)
        {
            Days = GameObject.Find("Sun").GetComponent<SunRotation>().Days;
            GameOver.gameObject.SetActive(true);
            Result.text = "survived for " + Days.ToString() + " days";
            StartCoroutine(GameOverFX());
        }
    }
    void FixedUpdate()
    {
        PollenCount.text = BeeStats.CollectedPollen.ToString();
        BugCount.text = Bugs.ToString();
        PlantCount.text = LivingPlants.Count.ToString();
        DayCount.text = Days.ToString();
    }

    private IEnumerator GameOverFX()
    {
        GameObject.Find("Bee").SetActive(false);
        GameObject[] bugs = GameObject.FindGameObjectsWithTag("Bugs");
        foreach (GameObject bug in bugs)
        {
            Destroy(bug);
        }
        GameObject.Find("InGameUI").SetActive(false);
        Vignette vig;
        GameObject.Find("PostPro").GetComponent<Volume>().profile.TryGet<Vignette>(out vig);
        for (int i = 0; i <= 13; i++)
        {
            vig.intensity.value += 0.05f;
            yield return new WaitForSeconds(0.13f);
        }
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Menu");
    }
}

