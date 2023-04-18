using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetireUIManager : MonoBehaviour
{
    public GameObject objPopup;
    public Button btnClose;

    public RectTransform rtComment;
    public GameObject pfComment;

    private HumanManager storedHuman;

    public void Init()
    {
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(HidePopup);
    }

    public void ShowPopup(HumanManager human)
    {
        this.storedHuman = human;
        HumanData humanData = human.humanData;

        objPopup.SetActive(true);
    }

    public void HidePopup()
    {
        if (storedHuman != null)
        {
            GameManager.Instance.levelManager.DeleteHuman(storedHuman);
        }

        objPopup.SetActive(false);
        GameManager.Instance.isTimeStop = false;

    }
}
