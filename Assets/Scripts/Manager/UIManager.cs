using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public InterfaceUIManager interfaceUIManager;
    public RetireUIManager retireUIManager;
    public RecipeUIManager recipeUIManager;
    public EndUIManager endUIManager;

    public void Init()
    {
        interfaceUIManager.Init();
        retireUIManager.Init();
        recipeUIManager.Init();
        endUIManager.Init();
    }

    public void ShowRetire(HumanBasic human)
    {
        retireUIManager.ShowPopup(human);
        GameManager.Instance.isUIPageOn = true;
    }
    public void ShowRecipe()
    {
        recipeUIManager.ShowPopup();
        GameManager.Instance.isUIPageOn = true;
    }

    public void ShowEndUI()
    {
        endUIManager.ShowPopup();
        GameManager.Instance.isUIPageOn = true;
    }
}
