
using UnityEngine;
using UnityEngine.UI;

public class SetInterface : MonoBehaviour
{
    public Button btnDecrypt;
    public Button btnSavePW;
    public Button btnResetKeys;
    public Text txtFileInfo;
    public Text txtErrorOutput;

    public InputField newPWFile;

    // DO NOT DELETE THIS FILE IT FOR STATE OF THE DECRYPT BUTTON
    // Start is called before the first frame update
    void Start()
    {
        btnDecrypt.interactable = false;
        btnSavePW.interactable = true;
        newPWFile.interactable = true;

    }

    // this clears part of the interface
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Called from SetupInterface.");
            txtFileInfo.text = "";
            txtErrorOutput.text = "";
        }
       
    }

}
