
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using TMPro;
using System;

// to put components in the foreground, change their sort order on the Inspector
public class BtnLoadFile : MonoBehaviour
{
   
    public TMP_InputField tmpInputField;
    public GameObject txtOutput;
    public GameObject saveAnim;
    public Dropdown loadFileDropDown;
    public Button btnDecrypt;
    DateTime creation;
    public Text txtFileInfo;

    public CanvasGroup canvas2;
    List<string> textToSelect = new List<string>() { };
    public string daStrPath;

    // fixed...Dropdown now refreshes with simple for loop in the Apply()

    public void btnLoadFile()
    {
        canvas2.interactable = true;
        loadFileDropDown.interactable = true;
        textToSelect.Clear(); // fixed list overloading by Clearing it.

        loadFileDropDown.enabled = true;
        loadFileDropDown.ClearOptions();
        // show the Dropdown component
        canvas2.alpha = 1f;
        canvas2.interactable = true;
        canvas2.blocksRaycasts = true;
        // loadFileDropDown.ClearOptions();
        // ClearCanvas();
        string daPath = Directory.GetCurrentDirectory();
        int arrayLength;

        arrayLength = Directory.GetFiles(daPath).Length;
        int aCount = 0; 
        // Debug.Log("Array length: " + arrayLength);
        string fileName;
        
        for (int i = 0; i < Directory.GetFiles(daPath).Length; i++)
        {
            fileName = Directory.GetFiles(daPath)[i];
            textToSelect.Add(fileName);
            // Debug.Log("For loopy: " + fileName);
            aCount++;
            if (aCount == arrayLength)
            {
                fileName = "";
                daPath = "";
                // Debug.Log("Filename: " + fileName + " dapath: " + daPath);
                break;
            }
            // Debug.Log("aCount: " + aCount);
        }

        loadFileDropDown.AddOptions(textToSelect);
    }

   
    void Start()
    {
        ClearCanvas();
    }

    /// <summary>
    /// For some reason I am not able to get the indexing correct on the List
    /// TODO: Correct List indexing so the proper file can be shown in the InputField
    /// </summary>
    public void Dropdown_IndexChanged(int index)
    {
        // index of select file
        index = loadFileDropDown.value;
        // read the file
        tmpInputField.text = File.ReadAllText(textToSelect[index]);
        daStrPath = textToSelect[index];

        creation = File.GetCreationTime(textToSelect[index]);
        txtFileInfo.text = creation.ToString();

        btnDecrypt.interactable = true;

        ClearCanvas();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            loadFileDropDown.ClearOptions();
            loadFileDropDown.interactable = false;
            loadFileDropDown.enabled = false;
            canvas2.alpha = 0f;
            canvas2.interactable = false;
            canvas2.blocksRaycasts = false;
            canvas2.interactable = false;
            
        }
    }

    #region
    void ClearCanvas()
    {
        loadFileDropDown.ClearOptions();
        loadFileDropDown.enabled = false;
        loadFileDropDown.interactable = false;
        canvas2.alpha = 0f;
        canvas2.interactable = false;
        canvas2.blocksRaycasts = false;
        canvas2.interactable = false;
    }
    #endregion

}
