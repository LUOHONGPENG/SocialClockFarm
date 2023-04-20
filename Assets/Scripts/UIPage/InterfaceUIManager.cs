using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceUIManager : MonoBehaviour
{
    public Button btnRecipe;
    public Button btnSpeed;
    public Text txSpeed;

    public void Init()
    {
        btnRecipe.onClick.RemoveAllListeners();
        btnRecipe.onClick.AddListener(delegate ()
        {
            GameManager.Instance.uiManager.ShowRecipe();
        });

        btnSpeed.onClick.RemoveAllListeners();
        btnSpeed.onClick.AddListener(delegate ()
        {
            GameManager.Instance.isDoubleSpeed =!GameManager.Instance.isDoubleSpeed;
            RefreshSpeedBtn();
        });

        RefreshSpeedBtn();
    }

    public void RefreshSpeedBtn()
    {
        if (GameManager.Instance.isDoubleSpeed)
        {
            txSpeed.text = string.Format("X{0}", 2);
        }
        else
        {
            txSpeed.text = string.Format("X{0}", 1);
        }
    }
}
