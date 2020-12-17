using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryGameController : MonoBehaviour
{

    //Movement for story mode

    private float scale;
    public GameObject player;

    // ui refs

    private Text notifText;

    // Start is called before the first frame update
    void Start()
    {
        CancelInvoke(); // clear any leftover invokes
        notifText = GameObject.Find("notifText").GetComponent<Text>();
        StartCoroutine(GameStartCutscene());
    }

    // Update is called once per frame
    void Update()
    {
        player.transform.Translate(scale * Vector3.right * Time.deltaTime);
    }

    // instructions "cutscene" at start of game
    IEnumerator GameStartCutscene()
    {

        StartCoroutine(CreateNotif("Welcome to story mode.", 3));
        /*
        yield return new WaitForSeconds(3);
        StartCoroutine(CreateNotif("Press space to move to an adjacent lane. You'll start off moving down, and change directions when in the top or bottom lane.", 4));
        yield return new WaitForSeconds(5);
        //Instantiate(criminal, new Vector3(player.transform.position.x + 12, 0, 0), Quaternion.identity);
        StartCoroutine(CreateNotif("This is a criminal. Press 'z' to throw a snowball and hit criminals to knock them out.", 3));
        yield return new WaitForSeconds(3);
        //Instantiate(cop, new Vector3(player.transform.position.x + 14, 0, 0), Quaternion.identity);
        StartCoroutine(CreateNotif("This is a cop. Cops will change lanes and occasionally shoot out tasers.", 3));
        yield return new WaitForSeconds(3);
        StartCoroutine(CreateNotif("Cops will block snowballs, and running into a cop or their taser ends the game.", 3));
        yield return new WaitForSeconds(3);
        StartCoroutine(CreateNotif("Try and apprehend all criminals whle avoiding the cops.", 3));
        */

        // start the game proper

        yield return new WaitForSeconds(3);

        scale = 2.5f;
    }

    IEnumerator CreateNotif(string msg, int time)
    {
        notifText.text = msg;
        yield return new WaitForSeconds(time);
        notifText.text = "";
    }
}
