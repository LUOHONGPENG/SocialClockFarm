using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceUIManager : MonoBehaviour
{
    public Button btnRecipe;

    public void Init()
    {
        btnRecipe.onClick.RemoveAllListeners();
        btnRecipe.onClick.AddListener(delegate ()
        {
            GameManager.Instance.uiManager.ShowRecipe();
        });
    }
}
