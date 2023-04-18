using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public RetireUIManager retireManager;

    public void Init()
    {
        retireManager.Init();
    }

    public void ShowRetire(HumanManager human)
    {
        retireManager.ShowPopup(human);
        GameManager.Instance.isTimeStop = true;
    }
}
