using UnityEngine.SceneManagement;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LogIn : MonoBehaviour
{
    public CanvasGroup canvasLogin;
    public InputField sPWD1;
    public Text lblLoginError;

    string filePath = "pw.txt";
    string contentsOfFile;

    // compare filePath with inputfield text
    public void CheckPW()
    {

        contentsOfFile = File.ReadAllText(@filePath);
        Debug.Log("In Start Function Contents of File: " + contentsOfFile);

        Debug.Log("contents of file: " + contentsOfFile);
        string contentStr = contentsOfFile.TrimEnd('\n');
        string hashInputField = sPWD1.text;
        string hashedStr = ComputeSha256Hash(hashInputField.TrimEnd('\n'));

        Debug.Log("Button is working");
        Debug.Log("Input field: " + sPWD1.text);
        
        Debug.Log("Contents: " + contentStr);
        Debug.Log("hashedStr: " + hashedStr);
        

        if(contentStr == hashedStr)
        {
            // show app scene
            Debug.Log("Password matches.");
            SceneManager.LoadScene("HackApp");
        }
        else
        {
            Debug.Log(" Wrong password");
            lblLoginError.text = "Wrong password, please try again.";
        }

    }

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

   
}
