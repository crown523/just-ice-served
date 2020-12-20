using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Snowball>() != null)
        {
            StartCoroutine(Die());
        }

    }

    IEnumerator Die()
    {
        anim.Play("enemy-hit");
        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }
}
