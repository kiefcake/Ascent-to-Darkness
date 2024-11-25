using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent ai;
    public List<Transform> destinations;
    public Animator aiAnim;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, idleTime, detectionDistance, catchDistance, searchDistance, minChaseTime, maxChaseTime, minSearchTime, maxSearchTime, jumpscareTime;
    public bool walking, chasing, searching;
    public Transform player;
    Transform currentDest;
    Vector3 dest;
    public Vector3 rayCastOffset;
    public string deathScene;
    public float aiDistance;
    public GameObject hideText, stopHideText;
    private bool canMove = false; //control enemy movement

    void Start()
    {
        walking = true;
        currentDest = destinations[Random.Range(0, destinations.Count)];
    }

    void Update()
    {
        if (!canMove) return; //exit if enemy cannot move

        //calculate direction and distance to the player
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        aiDistance = Vector3.Distance(player.position, this.transform.position);

        //check for player detection
        if (Physics.Raycast(transform.position + rayCastOffset, direction, out hit, detectionDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                //player detected, initiate search routine
                walking = false;
                StopCoroutine("stayIdle");
                StopCoroutine("searchRoutine");
                StartCoroutine("searchRoutine");
                searching = true;
            }
        }

        //handle searching behavior
        if (searching == true)
        {
            //adjust AI properties for searching
            ai.speed = 0;
            aiAnim.ResetTrigger("walk");
            aiAnim.ResetTrigger("idle");
            aiAnim.ResetTrigger("sprint");
            aiAnim.SetTrigger("search");

            //check if player is within search distance
            if (aiDistance <= searchDistance)
            {
                //player found, initiate chase routine
                StopCoroutine("stayIdle");
                StopCoroutine("searchRoutine");
                StopCoroutine("chaseRoutine");
                StartCoroutine("chaseRoutine");
                chasing = true;
                searching = false;
            }
        }

        //handle chasing behavior
        if (chasing == true)
        {
            dest = player.position;
            ai.destination = dest;
            ai.speed = chaseSpeed;
            aiAnim.ResetTrigger("walk");
            aiAnim.ResetTrigger("idle");
            aiAnim.ResetTrigger("search");
            aiAnim.SetTrigger("sprint");

            //check if player is within catch distance
            if (aiDistance <= catchDistance)
            {
                //player caught, initiate jumpscare and death routine
                player.gameObject.SetActive(false);
                aiAnim.ResetTrigger("walk");
                aiAnim.ResetTrigger("idle");
                aiAnim.ResetTrigger("search");
                hideText.SetActive(false);
                stopHideText.SetActive(false);
                aiAnim.ResetTrigger("sprint");
                aiAnim.SetTrigger("jumpscare");
                StartCoroutine(deathRoutine());
                chasing = false;
            }
        }

        //handle walking behavior
        if (walking == true)
        {
            dest = currentDest.position;
            ai.destination = dest;
            ai.speed = walkSpeed;
            aiAnim.ResetTrigger("sprint");
            aiAnim.ResetTrigger("idle");
            aiAnim.ResetTrigger("search");
            aiAnim.SetTrigger("walk");

            //checks if thge AI reached its destination
            if (ai.remainingDistance <= ai.stoppingDistance)
            {
                //idle after reaching destination
                aiAnim.ResetTrigger("sprint");
                aiAnim.ResetTrigger("walk");
                aiAnim.ResetTrigger("search");
                aiAnim.SetTrigger("idle");
                ai.speed = 0;
                StopCoroutine("stayIdle");
                StartCoroutine("stayIdle");
                walking = false;
            }
        }
    }

    //function to stop chasing
    public void stopChase()
    {
        walking = true;
        chasing = false;
        StopCoroutine("chaseRoutine");
        currentDest = destinations[Random.Range(0, destinations.Count)];
    }

    //idle
    IEnumerator stayIdle()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        walking = true;
        currentDest = destinations[Random.Range(0, destinations.Count)];
    }

    //searching
    IEnumerator searchRoutine()
    {
        yield return new WaitForSeconds(Random.Range(minSearchTime, maxSearchTime));
        searching = false;
        walking = true;
        currentDest = destinations[Random.Range(0, destinations.Count)];
    }

    //chasing
    IEnumerator chaseRoutine()
    {
        yield return new WaitForSeconds(Random.Range(minChaseTime, maxChaseTime));
        stopChase();
    }

    //death/jumpscare routine
    IEnumerator deathRoutine()
    {
        yield return new WaitForSeconds(jumpscareTime);
        SceneManager.LoadScene(deathScene);
    }

    //update enemy stats based on the number of orbs collected
    public void UpdateEnemyStats(int numberOfOrbs)
    {
        //calculate the fraction of orbs collected
        float orbFraction = (float)numberOfOrbs / 20f;

        //update enemy parameters maybe increase more
        chaseSpeed = Mathf.Lerp(chaseSpeed, chaseSpeed + 5f, orbFraction);
        detectionDistance = Mathf.Lerp(detectionDistance, detectionDistance + 10f, orbFraction);
        minChaseTime = Mathf.Lerp(minChaseTime, minChaseTime + 5f, orbFraction);
        maxChaseTime = Mathf.Lerp(maxChaseTime, maxChaseTime + 10f, orbFraction);
    }

    //start the enemy movement
    public void StartMoving()
    {
        canMove = true;
    }
}
