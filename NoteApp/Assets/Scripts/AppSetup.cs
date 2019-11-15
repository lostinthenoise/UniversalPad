using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

// TODO: possibly replace buttons with glowing animated buttons for user interface...much like a spaceship HUD.

public class AppSetup : MonoBehaviour
{
    //This is Main Camera in the Scene
    Camera m_MainCamera;
    public float cameraStartPos = 0.00F;
    
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

    }
}
