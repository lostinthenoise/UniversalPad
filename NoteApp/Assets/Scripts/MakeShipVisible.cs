using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using PathCreation;

public class MakeShipVisible : MonoBehaviour
{
	public GameObject spaceShip;
    public Vector3 lastPos;
	// public PathCreator pathCreator;

    public Animator animator;

	private float randNum;
	private float timer;

	private bool canCount = true;
	private bool doOnce = false;

    // Start is called before the first frame update

    void Start()
    {
        animator = GetComponent<Animator>();
        //spaceShip.transform.position = pathCreator.path.GetPoint(0);
        //lastPos = spaceShip.transform.position;
        //randNum = Random.Range(10, 20);
        //timer = randNum;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (timer >= 0.0f && canCount)
        {
            timer -= 1 * Time.deltaTime;
            // enabled = true;
            Debug.Log("In if statement  Timer: " + timer);
        }
        else if (timer <= 0.0f && !doOnce)
        {
            enabled = false;
            spaceShip.SetActive(true);
            canCount = false;
            animator.SetTrigger("flyShipTrigger");
            animator.Play("flySpaceship");
            animator.speed = 1.0f;
            Debug.Log("In else if statement");
            ObjectMoved();
            if (ObjectMoved())
            {
                enabled = true;
                Debug.Log("We're moving...");
                Debug.Log("Spaceship X position: " + spaceShip.transform.position.x);
                Debug.Log("ObjectMoved(): " + ObjectMoved());
            }
            else if (!ObjectMoved())
            {
                // canCount = false;
                Debug.Log("We've stopped.");
                // spaceShip.transform.position = pathCreator.path.GetPoint(0);
                //lastPos = spaceShip.transform.position;
                spaceShip.SetActive(false);
                animator.ResetTrigger("flyShipTrigger");
                animator.speed = 0;
                StartCoroutine(SetTimer());
            }
            lastPos = spaceShip.transform.position;
        }
        
    }


    IEnumerator SetTimer()
    {
        
        //spaceShip.transform.position = pathCreator.path.GetPoint(0);
        //lastPos = spaceShip.transform.position;
        Debug.Log("Last Position: " + lastPos);
        
        timer = 0.0f;
        randNum = Random.Range(10.0f, 20.0f);
        timer = randNum;
        canCount = true;
        doOnce = false;
        Debug.Log("Waiting for restart...");
        yield return new WaitForSeconds(1);

    }

    bool ObjectMoved()
    {
        Vector3 displacement = spaceShip.transform.position - lastPos;

        return displacement.magnitude > 0.001;
    }

   
}
