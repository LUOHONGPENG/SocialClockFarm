using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public InterfaceUIManager interfaceManager;
    public RetireUIManager retireManager;
    public RecipeUIManager recipeManager;

    public void Init()
    {
        interfaceManager.Init();
        retireManager.Init();
        recipeManager.Init();
    }

    public void ShowRecipe()
    {
        recipeManager.ShowPopup();
        GameManager.Instance.isUIPageOn = true;
    }

    public void ShowRetire(HumanBasic human)
    {
        retireManager.ShowPopup(human);
        GameManager.Instance.isUIPageOn = true;
    }
}
