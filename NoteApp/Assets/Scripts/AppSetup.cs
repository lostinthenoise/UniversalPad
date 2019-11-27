using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

// TODO: possibly replace buttons with glowing animated buttons for user interface...much like a spaceship HUD.

public class AppSetup : MonoBehaviour
{
    //This is Main Camera in the Scene
    Camera m_MainCamera;
    // public GameObject[] planets; // leave this for now

    public float sunPos;
    public Light sunLight;
    public float cameraStartPos = 0.00F;
    public float sunLightPos = 0.00F;
   
    // private GameObject[] instatiateObjects;

    void Start()
    {
        //This gets the Main Camera from the Scene
        m_MainCamera = Camera.main;
        //This enables Main Camera
        m_MainCamera.enabled = true;

    }

    // Update is called once per frame
    // Rotate the camera and parented canvas object slowly

    void Update()
    {
       
        cameraStartPos = cameraStartPos + 0.005F;
        transform.eulerAngles = new Vector3(0, cameraStartPos, 0);

        sunLightPos = sunLightPos + 0.005F;
        transform.eulerAngles = new Vector3(0, sunLightPos, 0);

    }

    
}
