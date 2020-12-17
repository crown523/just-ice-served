using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathUIManager : MonoBehaviour
{
    public Text endScore;
    public GameObject panelOpt1;
    public GameObject panelOpt2;
    private int selectedOpt = 1;
    private bool canInteract = true;

    // used to skip tutorial cutscene
    // set true when playtesting
    public static bool replayWasClicked = true;

    // Start is called before the first frame update
    void Start()
    {
        panelOpt1.GetComponent<Button>().Select();
        endScore.GetComponent<Text>().text = "You got got. You managed to nab " + ScoreScript.score + " criminals.";
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
        SceneManager.LoadScene("EndlessMode");
    }

    public void OnClickMainMenu()
    {
        // Returns to main menu
        replayWasClicked = false;
        SceneManager.LoadScene("MainMenu");
    }
}
