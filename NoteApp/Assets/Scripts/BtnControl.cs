using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnControl : MonoBehaviour
{

    public TMP_InputField tmpInputField;
    public GameObject txtOutput;
    public GameObject saveAnim;
    public CanvasGroup canvas2;
    public Text txtErroroutput;
    public Text txtFileInput;

    string nToClear = "Nothing to Clear";
    string cleared = "Cleared";

    public void ClearText()
    {
        ClearDropDown();

    }

    IEnumerator FadeOut()
    {
        saveAnim.GetComponent<Animator>().Play("outputText");
        yield return new WaitForSeconds(2);
        saveAnim.GetComponent<Animator>().Play("New State");

    }

    public void ClearDropDown()
    {
        if (tmpInputField.text == "")
        {
            txtOutput.GetComponent<Text>().text = nToClear;
            StartCoroutine(FadeOut());
        }
        else
        {
            // clear the text field
            tmpInputField.text = "";
            txtFileInput.text = "";
            txtOutput.GetComponent<Text>().text = cleared;
            StartCoroutine(FadeOut());
        }

        if (txtErroroutput.text.Length > 0)
        {
            txtErroroutput.text = "";
        }

        canvas2.alpha = 0f;
        canvas2.interactable = false;
        canvas2.blocksRaycasts = false;
    }
}
