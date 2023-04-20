using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndUIManager : MonoBehaviour
{
    public Button btnRestart;

    public GameObject objPopup;

    public void Init()
    {
        btnRestart.onClick.RemoveAllListeners();
        btnRestart.onClick.AddListener(delegate() {        
            HidePopup();
            SceneManager.LoadScene("Main");
        });
    }

    public void ShowPopup()
    {
        objPopup.SetActive(true);
    }
    public void HidePopup()
    {
        objPopup.SetActive(false);
        GameManager.Instance.isUIPageOn = false;
    }
}
