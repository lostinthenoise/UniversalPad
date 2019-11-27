
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BtnHelp : MonoBehaviour
{
    public Button btnHelp;
    public TMP_InputField tmpDaInputField;
    public string txtToPutBack;

    // FIXED THE HELP BUTTON DO NOT MODIFY
    public void ShowHelp()
    {
        string help = "Hello, hope you're enjoying UniversalPad.\n" +
            "You can use Esc key to clear controls that become visible if no longer want to use them.\n" +
            "When clicking the Save button, enter your file name and then click Save again.\n" +
            "PWReset is the password reset button, incase you want to change it.\n" +
            "KeyReset will delete your private keys. IMPORTANT: YOU CANNOT DECRYPT THOSE FILES " +
            "ENCRYPTED WITH THOSE PREVIOUS KEYS IF YOU RESET YOUR KEYS. You were warned.\n" +
            "610ry 5ku115 does not keep your keys, gather your personal data, contacts or " +
            "anything else that exists on your device. It's your information, not ours.\n" +
            "Now try out the Esc key.\n" +
            "The ESC key will clear the text field as well for a 'Quick Clear' of any information.";

        if(tmpDaInputField.text == "")
        {
            Debug.Log("");
            tmpDaInputField.text = help;
        }
        else if(tmpDaInputField.text == help && txtToPutBack.Length == 0)
        {
            tmpDaInputField.text = "";
        }
        else if (tmpDaInputField.text != help && tmpDaInputField.text != "")
        {
            txtToPutBack = tmpDaInputField.text;
            tmpDaInputField.text = help;
        }
        else if (tmpDaInputField.text == help && txtToPutBack.Length > 0)
        {
            tmpDaInputField.text = txtToPutBack;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           
           tmpDaInputField.text = "";
            txtToPutBack = "";

        }
    }

}
