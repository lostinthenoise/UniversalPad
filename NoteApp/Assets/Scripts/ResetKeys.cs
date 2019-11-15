using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetKeys : MonoBehaviour
{
    public CanvasGroup canvas5;
    
    public Button delDaKeys;
    public Button cancelDeleOperation;

    string daKeyPath = "starposdata.txt";
    string daIVPath  = "starclusterposdata.txt";

    public void ShowWarningPanel()
    {
        canvas5.alpha = 1f;
        canvas5.interactable = true;
        canvas5.blocksRaycasts = true;
    }


    public void DelDaFile()
    {
        if (File.Exists(daKeyPath) && File.Exists(daIVPath))
        {
            File.Delete(daKeyPath);
            File.Delete(daIVPath);

            SceneManager.LoadScene("LogInScene");
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            canvas5.alpha = 0f;
            canvas5.interactable = false;
            canvas5.blocksRaycasts = false;
        }
    }

    public void CancelDelOperation()
    {
        canvas5.alpha = 0f;
        canvas5.interactable = false;
        canvas5.blocksRaycasts = false;
    }
}
