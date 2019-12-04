
using UnityEngine;
//using PathCreation;
using System.Collections;

public class Follower : MonoBehaviour
{
    // x 3810 y 1196.004  z 9233.375
    public GameObject spaceShip;

    //public PathCreator pathCreator;
    //public EndOfPathInstruction end;

    public float speed = 0.04f;
    float distanceTravelled = 0.0f;
    

    //private void Start()
    //{
    //    spaceShip.transform.position = pathCreator.path.GetPoint(0);
    //}

    //private void Update()
    //{
    //    distanceTravelled += speed * Time.deltaTime;
    //    // Debug.Log("Distance Travalled: " + distanceTravelled);
    //    spaceShip.transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);

    //}

   
}


