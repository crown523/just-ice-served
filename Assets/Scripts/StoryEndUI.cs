using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryEndUI : MonoBehaviour
{
    public Text titleText;
    public Text endScore;
    public GameObject panelOpt1;
    public GameObject panelOpt2;
    private int selectedOpt = 1;
    private bool canInteract = true;

    // used to skip tutorial cutscene
    // set true when playtesting
    public static bool replayWasClicked = false;
    public static bool diedToCop = false;

    // Start is called before the first frame update
    void Start()
    {
        panelOpt1.GetComponent<Button>().Select();
        
        //Check possible endings of Story Mode
        //Not all enemies apprehended
        if(StoryScorebar.enemiesBeat < StoryScorebar.totalEnemies)
        {
            if (diedToCop)
            {
                endScore.GetComponent<Text>().text = "The (cough corrupt cough) cops put an end to your mission. Make sure to avoid them next time.";
            }
            else
            {
                endScore.GetComponent<Text>().text = "You let some criminals walk free. Make sure all of them face justice for their crimes against snowmanity.";
            }
            
        }
        else
        {
            if(!StoryScorebar.bossBeat)
            {
                endScore.GetComponent<Text>().text = "The don walks free. Snowmen everywhere tremble in fear. Better luck next time.";
            }
            else
            {
                titleText.GetComponent<Text>().text = "You Win!";
                endScore.GetComponent<Text>().text = "You've avenged the demise of your sibling's snowman. The streets are clear of filth, and the snowmen of your neighborhood can breathe easy again. You are a hero.";
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // use buttons to navigate
        if (Input.GetKeyDown("space") && canInteract)
        {
            print("spacebar");
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

    IEnumerator changeSelection()
    {
        print("selection changed");
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
        replayWasClicked = true;
        SceneManager.LoadScene("StoryMode");
    }

    public void OnClickMainMenu()
    {
        // Returns to main menu
        replayWasClicked = false;
        SceneManager.LoadScene("MainMenu");
    }
}
