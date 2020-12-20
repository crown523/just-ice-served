using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject opt1;
    public GameObject opt2;
    public GameObject opt3;

    private bool canInteract = true;
    private int selectedOpt = 1;

    // Start is called before the first frame update
    void Start()
    {
        opt1.GetComponent<Button>().Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && canInteract) {
            canInteract = false;
            StartCoroutine(changeSelection());
            canInteract = true;
        }
        if (Input.GetKeyDown("z")) {
            switch(selectedOpt)
            {
                case 1:
                    OnClickStory();
                    break;
                case 2:
                    OnClickEndless();
                    break;
                case 3:
                    Application.Quit();
                    break;
            }
        }
    }

    IEnumerator changeSelection() {
        switch (selectedOpt)
        {
            case 1:
                selectedOpt = 2;
                opt2.GetComponent<Button>().Select();
                break;
            case 2:
                selectedOpt = 3;
                opt3.GetComponent<Button>().Select();
                break;
            case 3:
                selectedOpt = 1;
                opt1.GetComponent<Button>().Select();
                break;
        }
        yield return new WaitForSeconds(1);
    }

    public void OnClickEndless()
    {
        SceneManager.LoadScene("EndlessMode");
    }
    
    public void OnClickStory()
    {
        SceneManager.LoadScene("StoryMode");
    }
}
