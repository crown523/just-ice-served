using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    public static int score = 7;
    Text scoreCounter;
    private GameObject endScore;
    private GameObject panelOpt1;
    private GameObject panelOpt2;
    private int selectedOpt = 1;
    private bool canInteract = true;

    // Start is called before the first frame update
    void Start()
    {
        scoreCounter = GetComponent<Text>();
        endScore = GameObject.Find("EndScoreText");
        panelOpt1 = GameObject.Find("ReplayButton");
        panelOpt2 = GameObject.Find("MainMenuButton");

        if(panelOpt1 != null)
        {
            panelOpt1.GetComponent<Button>().Select();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreCounter.text = "Criminals served: " + score;

        if (endScore != null)
        {
            endScore.GetComponent<Text>().text = "You got got. You managed to nab " + score + " criminals.";
            // if game over screen is up
            // use buttons to navigate
            if (Input.GetKeyDown("space") && canInteract)
            {
                canInteract = false;
                StartCoroutine(changeSelection());
                canInteract = true;
            }
            if (Input.GetKeyDown("z"))
            {
                switch (selectedOpt)
                {
                    case 1:
                        OnClickReplay();
                        break;
                    case 2:
                        OnClickMainMenu();
                        break;
                }
            }
        }
    }

    IEnumerator changeSelection()
    {
        switch (selectedOpt)
        {
            case 1:
                selectedOpt = 2;
                panelOpt2.GetComponent<Button>().Select();
                break;
            case 2:
                selectedOpt = 1;
                panelOpt1.GetComponent<Button>().Select();
                break;
        }
        yield return new WaitForSeconds(1);
    }

    public void OnClickReplay()
    {
        // restart Endless Mode
        SceneManager.LoadScene("EndlessMode");
    }

    public void OnClickMainMenu()
    {
        // nothing yet
        SceneManager.LoadScene("MainMenu");
    }

}
