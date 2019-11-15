using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnDecrypt : MonoBehaviour
{
    public TMP_InputField tmpInputField;

    public GameObject txtOutput;
    public GameObject saveAnim;

    public Text txtErrorOutput;
    public Button btnDecrypt;
    public Button btnEncrypt;

    string decrypted = "Text Decrypted";
    string nToDec = "Nothing to Decrypt";

    string filePathSCD = "starclusterposdata.txt";
    string filePathSPD = "starposdata.txt";

    private void Start()
    {
        //Debug.Log("Star Pos: " + starPosData.text);
        //Debug.Log("Star cluster data: " + starClusterData.text);
    }

    #region Drecypt the Text
    public void DecryptText()
    {
        string getKey = File.ReadAllText(filePathSPD).TrimEnd('\n');
        string getIV = File.ReadAllText(filePathSCD).TrimEnd('\n');

        getKey = getKey.Substring(0, 32);
        getIV = getIV.Substring(0, 16);

        //string gKey = getKey.TrimEnd('\n');

        //Debug.Log("getKey length: " + gKey.Length);
        //Debug.Log("getIV length: " + getIV.Length);

        if (tmpInputField.text == "")
        {
            NothingToDecrypt();
        }
        else
        {
            string encrypted = tmpInputField.text;
            string trimEncrypted = encrypted.TrimEnd('\n');
            if (encrypted.Contains(" "))
            {
                txtOutput.GetComponent<Text>().text = decrypted;
                
                StartCoroutine(FadeOut());
            }
            else
            { 

                try
                {
                    byte[] encryptedBytes = Convert.FromBase64String(trimEncrypted);
                    AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                    aes.BlockSize = 128;
                    aes.KeySize = 256;
                    aes.Key = ASCIIEncoding.ASCII.GetBytes(getKey.TrimEnd('\n'));

                    aes.IV = ASCIIEncoding.ASCII.GetBytes(getIV.TrimEnd('\n'));

                    aes.Padding = PaddingMode.PKCS7;
                    aes.Mode = CipherMode.CBC;
                    ICryptoTransform crypto = aes.CreateDecryptor(aes.Key, aes.IV);
                    byte[] secret = crypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                    crypto.Dispose();
                    tmpInputField.text = ASCIIEncoding.ASCII.GetString(secret);

                    
                    txtOutput.GetComponent<Text>().text = decrypted;
                    StartCoroutine(FadeOut());
                }
                catch (Exception ex)
                {
                    // txtOutput.GetComponent<Text>().text = decrypted;
                    txtErrorOutput.text = "Could not decrypt file. You may have encrypted with another key system.";
                    Debug.Log(ex);
                    // StartCoroutine(FadeOut());
                }
                
            }

        }
        btnDecrypt.interactable = false;
        btnEncrypt.interactable = true;

    }
    #endregion

    public void NothingToDecrypt()
    {
        txtOutput.GetComponent<Text>().text = nToDec;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        saveAnim.GetComponent<Animator>().Play("outputText");
        yield return new WaitForSeconds(2);
        saveAnim.GetComponent<Animator>().Play("New State");

    }
}
