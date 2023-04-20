using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUIManager : MonoBehaviour
{
    public Button btnClose;

    public GameObject objPopup;

    public void Init()
    {
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(HidePopup);
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
