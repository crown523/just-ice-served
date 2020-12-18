using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        CancelInvoke(); // clear any leftover invokes
        //set the check for having beat the boss to false
        ScoreScript.bossBeat = false;
        
        //was used for testing the end screens
        //ScoreScript.score = GameObject.FindObjectsOfType(typeof(EnemyAI)).Length;

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
        ScoreScript.score = 0;
        scrollSpeed = 2.5f;
        background.GetComponent<BackgroundScroll>().speed = 0.5f;
        hardcodedInstances.SetActive(true);

    }

    IEnumerator CreateNotif(string msg, int time)
    {
        notifText.text = msg;
        yield return new WaitForSeconds(time);
        notifText.text = "";
    }
}
