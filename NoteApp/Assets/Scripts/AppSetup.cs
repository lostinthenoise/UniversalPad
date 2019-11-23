using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

// TODO: possibly replace buttons with glowing animated buttons for user interface...much like a spaceship HUD.

public class AppSetup : MonoBehaviour
{
    //This is Main Camera in the Scene
    Camera m_MainCamera;
    public GameObject[] planets;
    public Light sunLight;
    public float cameraStartPos = 0.00F;
    public float sunLightPos = 0.00F;
    int indexingArray;
    Vector3 camPos;
    Vector3 camRot;
    int spawnValue = 1199;
    int count;
    int i;

    private GameObject[] instatiateObjects;

    void Start()
    {
        //This gets the Main Camera from the Scene
        m_MainCamera = Camera.main;
        //This enables Main Camera
        m_MainCamera.enabled = true;
        camPos = m_MainCamera.transform.position;

        instatiateObjects = new GameObject[planets.Length];
        Debug.Log("Array length: " + planets.Length);
        indexingArray = 0;
        Debug.Log("indexingArray Array: " + indexingArray);
        i = 0;
        count = 0;
    }


    // Update is called once per frame
    // Rotate the camera and parented canvas object slowly

    void Update()
    {
        count++;
        camRot = m_MainCamera.transform.position;
        cameraStartPos = cameraStartPos + 0.1F;
        transform.eulerAngles = new Vector3(0, cameraStartPos, 0);
        camRot = transform.eulerAngles;

        // sunLight.transform.eulerAngles = new Vector3(0, sunLightPos, 0);
        sunLightPos = sunLightPos + 0.1F;
        sunLight.transform.eulerAngles = new Vector3(0, sunLightPos, 0);
        Debug.Log("Count: " + count);

        if (count == spawnValue)
        {
            SpawnPlanets();
        }
        
    }

    void SpawnPlanets()
    {
        
        
        Debug.Log("Value of i: " + i);
        
        instatiateObjects[i] = Instantiate(planets[i]) as GameObject;
        instatiateObjects[i].transform.eulerAngles = new Vector3(31458, 462, 77123);
        Debug.Log("Planets: " + planets[i]);
        i++;

    }
}
