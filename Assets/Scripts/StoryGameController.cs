using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryGameController : MonoBehaviour
{

    //Movement for story mode
    public float scrollSpeed;
    public float snowballSpeed;
    public GameObject player;

    // ui refs
    private Text notifText;

    //Objects to activate/change at certain points in the mode
    public GameObject tutorialEnemy;
    public GameObject tutorialCop;
    public GameObject hardcodedInstances;
    public GameObject boss;
    public GameObject background;
    private bool bossFight;

    // Start is called before the first frame update
    void Start()
    {
        CancelInvoke(); // clear any leftover invokes

        scrollSpeed = 0f;
        background.GetComponent<BackgroundScroll>().speed = 0f;
        bossFight = false;

        //set the check for having beat the boss to false
        StoryScorebar.bossBeat = false;
        StoryScorebar.enemiesBeat = 0;

        

        notifText = GameObject.Find("notifText").GetComponent<Text>();

        hardcodedInstances.SetActive(false);

        StartCoroutine(GameStartCutscene());
    }

    // Update is called once per frame
    void Update()
    {
        player.transform.Translate(scrollSpeed * Vector3.right * Time.deltaTime);
        Camera.main.transform.Translate(scrollSpeed * Vector3.right * Time.deltaTime);
        background.transform.Translate(scrollSpeed * Vector3.right * Time.deltaTime);

        if(player.GetComponent<PlayerController>().finished && !bossFight)
        {
            bossFight = true;
            if (StoryScorebar.enemiesBeat >= StoryScorebar.totalEnemies)
            {
                Destroy(GameObject.Find("FinishLine"));
                
                scrollSpeed = 0f;
                background.GetComponent<BackgroundScroll>().speed = 0f;
                Instantiate(boss, new Vector3(player.transform.position.x + 12, 0, 0), Quaternion.identity);
            }
            else
            {
                SceneManager.LoadScene("StoryEndScreen");
            }
            
        }
        
    }

    // instructions "cutscene" at start of game
    IEnumerator GameStartCutscene()
    {
        
        StartCoroutine(CreateNotif("Welcome to story mode.", 3));
        yield return new WaitForSeconds(3);

        StartCoroutine(CreateNotif("After a thug knocked over your little sibling's snowman, you've taken it upon yourself to hunt down every criminal in the vicinity of your neighborhood.", 3));
        yield return new WaitForSeconds(3);

        StartCoroutine(CreateNotif("Press space to move to an adjacent lane. You'll start off moving down, and change directions when in the top or bottom lane.", 5));
        yield return new WaitForSeconds(5);


        StartCoroutine(CreateNotif("This is a criminal. Press 'z' to throw a snowball and hit criminals to knock them out.", 3));
        Instantiate(tutorialEnemy, new Vector3(player.transform.position.x + 8, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(3);

        //a hacky way to destroy the specific instantiated example gameobjects
        Destroy(GameObject.Find("Enemy(Clone)"));
        Instantiate(tutorialCop, new Vector3(player.transform.position.x + 11, 0, 0), Quaternion.identity);
        StartCoroutine(CreateNotif("This is a cop. Cops will change lanes and occasionally shoot out tasers.", 3));
        yield return new WaitForSeconds(3);


        StartCoroutine(CreateNotif("Cops will block snowballs, and running into a cop or their taser ends the game.", 3));
        yield return new WaitForSeconds(3);

        Destroy(GameObject.Find("Cop(Clone)"));
        StartCoroutine(CreateNotif("Try and apprehend all criminals whle avoiding the cops. " +
                                    "\nAvenging your sibling's fallen snowman is in your hands", 3));
        
        yield return new WaitForSeconds(3);

        // start the game proper
        
        scrollSpeed = 2.5f;
        background.GetComponent<BackgroundScroll>().speed = 0.5f;
        hardcodedInstances.SetActive(true);

        //resets score in case you killed that example guy
        StoryScorebar.enemiesBeat = 0;
        //sets the number of criminals to get now that they're all active
        StoryScorebar.totalEnemies = GameObject.FindObjectsOfType(typeof(EnemyAI)).Length;

        //used for testing the end screens
        //ScoreScript.score = GameObject.FindObjectsOfType(typeof(EnemyAI)).Length;
    }

    IEnumerator CreateNotif(string msg, int time)
    {
        notifText.text = msg;
        yield return new WaitForSeconds(time);
        notifText.text = "";
    }
}
