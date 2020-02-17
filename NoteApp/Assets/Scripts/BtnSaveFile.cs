
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;

public class BtnSaveFile : MonoBehaviour
{
  
    public GameObject txtOutput;
    public GameObject saveAnim;
  
    public TMP_InputField tmpInputField;
    public TMP_InputField ipfSaveFileAs;
    public string time, date;
    string fSaved = "File Saved";
    string nToSave = "Nothing to Save";
    string enterFileName = "Enter File Name";
    
    public BtnLoadFile script;

    public CanvasGroup canvas2;
    public CanvasGroup canvas3;

    public void Apply()
    {
        
            canvas3.alpha = 1f;
            canvas3.interactable = true;
            canvas3.blocksRaycasts = true;
            string filePath = ipfSaveFileAs.text;
            SaveText(filePath);
      
        
    }

    public void SaveText(string filePath)
    {

        if(tmpInputField.text == "")
        {

            NothingToSave();

            ClearDropDown();

            canvas3.alpha = 0f;
            canvas3.interactable = false;
            canvas3.blocksRaycasts = false;

        }
        else
        {
            ClearDropDown();

            // Debug.Log("File Path: " + filePath);
 
            if(ipfSaveFileAs.text != "")
            {
                string content = tmpInputField.text;
                string trimContent = content.TrimEnd('\n');
                StreamWriter writer = new StreamWriter(filePath, false);
                writer.WriteLine(time.TrimEnd('\n') + trimContent);
                writer.Close();
                writer.Dispose();

                ShowSuccess();

                
            }
            else
            {
                txtOutput.GetComponent<Text>().text = enterFileName;
                StartCoroutine(FadeOut());
            }  
        }

    }


    public void ShowSuccess()
    {
        canvas3.alpha = 0f;
        canvas3.interactable = false;
        canvas3.blocksRaycasts = false;
        ipfSaveFileAs.text = "";

        txtOutput.GetComponent<Text>().text = fSaved;
        StartCoroutine(FadeOut());
    }

    public void NothingToSave()
    {
        txtOutput.GetComponent<Text>().text = nToSave;
        StartCoroutine(FadeOut());
    }

    // used Animator to build simple fade animation 
    IEnumerator FadeOut()
    {
        saveAnim.GetComponent<Animator>().Play("outputText");
        yield return new WaitForSeconds(2);
        saveAnim.GetComponent<Animator>().Play("New State"); 
    }

    #region Clear the DropDown
    public void ClearDropDown()
    {
        canvas2.alpha = 0f;
        canvas2.interactable = false;
        canvas2.blocksRaycasts = false;
    }
    #endregion

    #region Clear Save Textfield
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas3.alpha = 0f;
            canvas3.interactable = false;
            canvas3.blocksRaycasts = false;
            ipfSaveFileAs.text = "";
        } 
    }
    #endregion
}
