using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Security.Cryptography;

// SETPASSWORD SOURCE

public class SetPassword : MonoBehaviour
{

    public InputField sPW1;
    public InputField sPW2;
    public Button setpwButton;
    public CanvasGroup canvasPWD;
    public CanvasGroup canvasLogin;
    public Text exOutPut;

    string filePath = "pw.txt"; // rename file for testing only


    // 1. check for password file
    // 2. if pw not found show Canvas to SetPassword, else show Canvas 2 to LogIn
    // 3. Check LogIn pw if successful, open Application scene, else give 3 tries and then quit.

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Checking for pw file");
    
        if (File.Exists(@filePath))
        {
            canvasPWD.alpha = 0f;
            canvasPWD.interactable = false;
            canvasPWD.blocksRaycasts = false;

            // call show LogIn canvas2
            canvasLogin.alpha = 1f;
            canvasLogin.interactable = true;
            canvasLogin.blocksRaycasts = true;
        }
        else
        {
            Debug.Log("PW file not found");
        }
       
    }

    #region Make the Password File
    public void MakePW()
    {
        Debug.Log(sPW1.text);
        if(sPW1.text == "" || sPW2.text == "")
        {
            Debug.Log("Cannot leave fields blank");
            exOutPut.text = "Cannot leave fields blanks.";

        }
        else
        {
            if (sPW1.text == sPW2.text)
            {
                string content = sPW1.text;
                string trimContent = content.TrimEnd('\n');

                // implement sha256 hashing
                string hashedPW = ComputeSha256Hash(trimContent);

                StreamWriter writer = new StreamWriter(@filePath, true);
                writer.WriteLine(hashedPW);
                writer.Close();

                Debug.Log("Hashed string: " + hashedPW);

                canvasPWD.alpha = 0f;
                canvasPWD.interactable = false;
                canvasPWD.blocksRaycasts = false;
                ShowLogIn();
            }
            else
            {
                // passwords don't match
                Debug.Log("Passwords don't match.");
                exOutPut.text = "Passwords don't match.";
            }
            
        }

    }
    #endregion

    #region Show Login Canvas
    void ShowLogIn()
    {
        canvasLogin.alpha = 1f;
        canvasLogin.interactable = true;
        canvasLogin.blocksRaycasts = true;

        exOutPut.text = "";
    }
    #endregion

    #region Compute 256 Hash
    static string ComputeSha256Hash(string rawData)
    {
        // Create a SHA256   
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
    #endregion

}
