
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ClearPWD : MonoBehaviour
{
    string pwFilePath = "pw.txt";
    public InputField IPFEnterPW;
    public Button btnSavePW;
    public Button btnResetKeys;
    public CanvasGroup canvas4;
    public Text txtErrorOutput;
    public GameObject saveAnim;
    public Text txtOutput;
    string pwReset = "Password Reset";

    public Vector3 pos;
    public Vector3 oringinalStartPos;
    public Vector3 transformedPos;

    // Start is called before the first frame update
    void Start()
    {
        pos = btnResetKeys.transform.localPosition;
        oringinalStartPos = pos;
        IPFEnterPW.text = "";
        transformedPos.y = pos.y - 80;

    }

    public void SetDaComponentsVisibile()
    {
        Debug.Log("WTF");
        pos = btnResetKeys.transform.localPosition;
        IPFEnterPW.text = "";
        transformedPos.y = pos.y - 80;

        canvas4.alpha = 1f;
        canvas4.interactable = true;
        canvas4.blocksRaycasts = true;

        pos = btnResetKeys.transform.localPosition;
        pos.y = transformedPos.y;
        btnResetKeys.transform.localPosition = pos;

    }

    public void EnterInNewPWFile()
    {
        if (File.Exists(pwFilePath))
        {
            File.Delete(pwFilePath);
        }

        Debug.Log(IPFEnterPW.text);
        if (IPFEnterPW.text == "")
        {
            Debug.Log("Cannot leave fields blank");
            txtErrorOutput.text = "In ClearPWD class. Cannot leave Password field blanks. Press Escape key.";
        }
        else
        {
            File.Delete(pwFilePath);
            string content = IPFEnterPW.text;
            string trimContent = content.TrimEnd('\n');

            // implement sha256 hashing
            string hashedPW = ComputeSha256Hash(trimContent);

            StreamWriter writer = new StreamWriter(@pwFilePath, false);
            writer.WriteLine(hashedPW);
            writer.Close();
            writer.Dispose();
            IPFEnterPW.text = "";

            canvas4.alpha = 0f;
            canvas4.interactable = false;
            canvas4.blocksRaycasts = false;

            btnResetKeys.transform.localPosition = oringinalStartPos;

            txtErrorOutput.text = "";
            txtOutput.text = pwReset;
            StartCoroutine(FadeOut());
            Debug.Log("Hashed string: " + hashedPW);

        }

    }

    IEnumerator FadeOut()
    {
        saveAnim.GetComponent<Animator>().Play("outputText");
        yield return new WaitForSeconds(2);
        saveAnim.GetComponent<Animator>().Play("New State");

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canvas4.interactable == true)
        {
            canvas4.alpha = 0f;
            canvas4.interactable = false;
            canvas4.blocksRaycasts = false;

            btnResetKeys.transform.localPosition = oringinalStartPos;

        }

        if (Input.GetKeyDown(KeyCode.Return) && IPFEnterPW.text != "")
        {
            canvas4.alpha = 0f;
            canvas4.interactable = false;
            canvas4.blocksRaycasts = false;

            EnterInNewPWFile();
            // DOES NOT NEED TO TRANSFORM RESSET BUTTON
            // ALREADAY DONE IN ENTERINNEWPWFILE()

        }
        if (Input.GetKeyDown(KeyCode.Return) && IPFEnterPW.text == "" && canvas4.interactable == true)
        {
            Debug.Log("We're in here now");
            canvas4.alpha = 0f;
            canvas4.interactable = false;
            canvas4.blocksRaycasts = false;

            btnResetKeys.transform.localPosition = oringinalStartPos;

        }

        //if (Input.GetKeyDown(KeyCode.Return) && canvas4.interactable == true)
        //{
        //    canvas4.alpha = 0f;
        //    canvas4.interactable = false;
        //    canvas4.blocksRaycasts = false;
        //    //btnResetKeys.transform.localPosition = originalPos;

        //    pos = btnResetKeys.transform.localPosition;
        //    originalPos = pos;
        //    IPFEnterPW.text = "";
        //    transformedPos.y = pos.y - 80;

        //    txtErrorOutput.text = "Cannot leave Password field blanks. Press Escape key.";

        //}
        //if (Input.GetKeyDown(KeyCode.Escape) && canvas4.interactable == true)
        //{
        //    btnResetKeys.transform.localPosition = transformedPos;
        //}
    }
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
