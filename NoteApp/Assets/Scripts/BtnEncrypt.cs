using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnEncrypt : MonoBehaviour
{

    public TMP_InputField tmpInputField;
    public GameObject txtOutput;
    public GameObject saveAnim;
    public Button btnDecrypt;
    public Button btnEncrypt;

    string fEncrypted = "Text Encrypted";
    string nToEnc = "Nothing to Encrypt";

    string filePathSCD = "starclusterposdata.txt";
    string filePathSPD = "starposdata.txt";

    private void Start()
    {
        // need to decrypt the file for usage.

        // read in key files
        
    }

    #region Encrypt Text

    public void EncryptText()
    {
        string getDK = File.ReadAllText(filePathSPD);
        string getDIV = File.ReadAllText(filePathSCD);

        // Decrypt the getKey and getIV files
        // store in global variable and then CLEAR as soon as used

        Debug.Log("Star Pos: " + getDK);
        Debug.Log("Star cluster data: " + getDIV);

        if (tmpInputField.text == "")
        {
            NothingToEncrypt();
        }
        else
        {
            // encryption function goes here
            string encdStr = EncryptFunc(tmpInputField.text);
            // Debug.Log("Returning Core Enc Func String: " + encdStr);
            tmpInputField.text = encdStr;
            btnEncrypt.interactable = false;
            btnDecrypt.interactable = true;
            txtOutput.GetComponent<Text>().text = fEncrypted;
            StartCoroutine(FadeOut());

        }

    }
    #endregion

    #region Core Encryption Function
    private string EncryptFunc(string textToEnc)
    {
        // read in the key file
        string getKey = File.ReadAllText(filePathSPD).TrimEnd('\n');
        string getIV = File.ReadAllText(filePathSCD).TrimEnd('\n');

        //string getKey = starPosData.ToString().TrimEnd('\n');
        //string getIV = starClusterData.ToString().TrimEnd('\n');
        getKey = getKey.Substring(0, 32);
        getIV = getIV.Substring(0, 16);

        // string gKey = getKey.TrimEnd('\n');

        //Debug.Log("getKey length: " + gKey.Length);
        //Debug.Log("getIV length: " + getIV.Length);



        byte[] plainTextBytes = new UTF8Encoding().GetBytes(textToEnc);
        AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
        aes.BlockSize = 128;
        aes.KeySize = 256;
        aes.Key = ASCIIEncoding.ASCII.GetBytes(getKey.TrimEnd('\n'));

        aes.IV = ASCIIEncoding.ASCII.GetBytes(getIV.TrimEnd('\n'));

        aes.Padding = PaddingMode.PKCS7;
        aes.Mode = CipherMode.CBC;
        ICryptoTransform crypto = aes.CreateEncryptor(aes.Key, aes.IV);
        byte[] encrypted = crypto.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);

        // Debug.Log("In Core Encyrption function now: " + textToEnc);

        return Convert.ToBase64String(encrypted);
    }
    #endregion

    #region Nothing To Encrypt 
    public void NothingToEncrypt()
    {
        txtOutput.GetComponent<Text>().text = nToEnc;
        StartCoroutine(FadeOut());
    }
    #endregion

    #region Fadeout Animation
    IEnumerator FadeOut()
    {
        saveAnim.GetComponent<Animator>().Play("outputText");
        yield return new WaitForSeconds(2);
        saveAnim.GetComponent<Animator>().Play("New State");

    }
    #endregion
}
