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
    public Text notifText;
    public GameObject box;
    public AudioSource stageMusicPlayer;
    public AudioSource bossMusicPlayer;

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

        

        hardcodedInstances.SetActive(false);

        if (!StoryEndUI.replayWasClicked) 
        {
            StartCoroutine(GameStartCutscene());
        }
        else
        {
            scrollSpeed = 2.5f;
            background.GetComponent<BackgroundScroll>().speed = 0.25f;
            hardcodedInstances.SetActive(true);
            stageMusicPlayer.Play(0);

            //resets score in case you killed that example guy
            StoryScorebar.enemiesBeat = 0;
            //sets the number of criminals to get now that they're all active
            StoryScorebar.totalEnemies = GameObject.FindObjectsOfType(typeof(EnemyAI)).Length;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        player.transform.Translate(scrollSpeed * Vector3.right * Time.deltaTime);
        Camera.main.transform.Translate(scrollSpeed * Vector3.right * Time.deltaTime);
        background.transform.Translate(scrollSpeed * Vector3.right * Time.deltaTime);

        if(player.GetComponent<PlayerController>().finished && !bossFight)
        {
            
            if (StoryScorebar.enemiesBeat >= StoryScorebar.totalEnemies)
            {
                bossFight = true;
                stageMusicPlayer.Stop();
                Destroy(GameObject.Find("FinishLine"));
                
                scrollSpeed = 0f;
                background.GetComponent<BackgroundScroll>().speed = 0f;
                StartCoroutine(BossCutscene());
                
            }
            else
            {
                StoryEndUI.diedToCop = false;
                SceneManager.LoadScene("StoryEndScreen");
            }
            
        }
        
    }

    // instructions "cutscene" at start of game
    IEnumerator GameStartCutscene()
    {
        box.SetActive(true);

        StartCoroutine(CreateNotif("Your story begins here.", 3));
        yield return new WaitForSeconds(3);

        StartCoroutine(CreateNotif("After an organized crime syndicate obliterated your little sibling's snowman, you've taken it upon yourself to hunt down every criminal in your neighborhood.", 5));
        yield return new WaitForSeconds(5);

        StartCoroutine(CreateNotif("Press space to move to an adjacent lane. You'll start off moving down, and change directions when in the top or bottom lane.", 5));
        yield return new WaitForSeconds(5);


        StartCoroutine(CreateNotif("This is snowman-terrorizing mafioso. Press 'z' to throw a snowball and hit mafiosos to knock them out.", 4));
        Instantiate(tutorialEnemy, new Vector3(player.transform.position.x + 8, -2.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(3);
        //a hacky way to destroy the specific instantiated example gameobjects
        Destroy(GameObject.Find("Enemy(Clone)"));
        yield return new WaitForSeconds(1);
        
        Instantiate(tutorialCop, new Vector3(player.transform.position.x + 11, -2.5f, 0), Quaternion.identity);
        StartCoroutine(CreateNotif("This is a (cough corrupt cough) cop. Cops will change lanes and occasionally shoot out tasers.", 3));
        yield return new WaitForSeconds(3);


        StartCoroutine(CreateNotif("Cops will block snowballs, and running into a cop or their taser ends the game.", 3));
        yield return new WaitForSeconds(3);

        Destroy(GameObject.Find("Cop(Clone)"));
        StartCoroutine(CreateNotif("Clear the streets of all mafiosos while avoiding law enforcement. " +
                                    "\nThe fate of all snowmanity hangs in the balance. Good luck.", 5));
        
        yield return new WaitForSeconds(5);

        // start the game proper
        
        box.SetActive(false);

        scrollSpeed = 2.5f;
        background.GetComponent<BackgroundScroll>().speed = 0.25f;
        hardcodedInstances.SetActive(true);
        stageMusicPlayer.Play(0);

        //resets score in case you killed that example guy
        StoryScorebar.enemiesBeat = 0;
        //sets the number of criminals to get now that they're all active
        StoryScorebar.totalEnemies = GameObject.FindObjectsOfType(typeof(EnemyAI)).Length;

        //used for testing the end screens
        //StoryScorebar.enemiesBeat = 24;
    }

    IEnumerator BossCutscene()
    {
        
        Destroy(hardcodedInstances);
        player.GetComponent<PlayerController>().controlsActive = false;
        Instantiate(boss, new Vector3(player.transform.position.x + 24, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(3);

        box.SetActive(true);
        StartCoroutine(CreateNotif("So you're the little punk running down the street and knocking out all my boys with snowballs.", 3));
        yield return new WaitForSeconds(3);

        StartCoroutine(CreateNotif("Let's see how you like a taste of your own medicine!", 2));
        yield return new WaitForSeconds(2);

        player.GetComponent<PlayerController>().controlsActive = true;
        bossMusicPlayer.Play(0);
        box.SetActive(false);

    }

    IEnumerator CreateNotif(string msg, int time)
    {
        notifText.text = msg;
        yield return new WaitForSeconds(time);
        notifText.text = "";
    }
}
