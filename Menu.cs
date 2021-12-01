using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject MenuObject;
    public GameObject CreditObject;
    public GameObject HowToObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Meadow");
    }

    public void Credits()
    {
        MenuObject.GetComponent<Animation>().Play("MenuAnimation");
        CreditObject.GetComponent<Animation>().Play("CreditAnimations");
    }

    public void BackToMenu()
    {
        CreditObject.GetComponent<Animation>().Play("CreditAnimationsReverse");
        MenuObject.GetComponent<Animation>().Play("MenuAnimationsReverse");
    }

    public void HowTo()
    {
        MenuObject.GetComponent<Animation>().Play("MenuAnimation");
        HowToObject.GetComponent<Animation>().Play("CreditAnimations");
    }

    public void BackToMenu2()
    {
        HowToObject.GetComponent<Animation>().Play("CreditAnimationsReverse");
        MenuObject.GetComponent<Animation>().Play("MenuAnimationsReverse");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
