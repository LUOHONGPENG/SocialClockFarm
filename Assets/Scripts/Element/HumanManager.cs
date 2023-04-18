using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class HumanManager : MonoBehaviour
{
    [Header("Basic")]
    public SpriteRenderer srHuman;
    public CommonDragItem dragManager;

    [Header("UI")]
    public Text txAge;
    public Image imgFillEdu;
    public Text codeEdu;
    public Image imgFillFortune;
    public Text codeFortune;


    private bool isInit = false;
    private SlotManager currentSlot;

    #region Basic
    public void Init(HumanData humanData)
    {
        if (dragManager != null)
        {
            dragManager.InitDrag(srHuman);
            dragManager.dragDealAction = delegate ()
            {
                DragDeal();
            };
        }
        this.humanData = humanData;
        RefreshUI();
    }

    public void TimeGo()
    {
        TimeGoCheckDrag();
        TimeGoData();
    }

    #endregion

    #region DragControl

    private void DragDeal()
    {
        //CheckWhetherSlot
        Vector3 screenPos = PublicTool.GetMousePosition2D();
        RaycastHit2D[] hits = Physics2D.RaycastAll(screenPos, Vector2.zero);
        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Slot")
                {
                    GameObject objSlot = hit.collider.gameObject;
                    SlotManager itemSlot = objSlot.GetComponent<SlotManager>();
                    if (itemSlot != null && currentSlot != itemSlot)
                    {
                        if (itemSlot.CheckHumanValid(humanData))
                        {
                            //Bind the current Slot
                            currentSlot = itemSlot;
                            itemSlot.isFilled = true;
                        }
                        else
                        {
                            //Back to the current Slot
                            dragManager.MoveBackStartPoint();
                        }
                        return;
                    }
                }
            }
        }

        //Back to the Hole
        if (currentSlot != null)
        {
            currentSlot.isFilled = false;
            currentSlot = null;
        }
        dragManager.MoveBackInitialPoint();
    }

    private void TimeGoCheckDrag()
    {
        if (dragManager != null)
        {
            dragManager.TimeGoDrag();

        }
    }
    #endregion

    #region UIControl

    public void RefreshUI()
    {
        txAge.text = humanData.Age.ToString();
        imgFillEdu.fillAmount = vCurrentEdu / 100f;
        imgFillFortune.fillAmount = vCurrentFortune / 100f;
        codeEdu.text = string.Format("{0}%", Mathf.RoundToInt(vCurrentEdu));
        codeFortune.text = string.Format("{0}%", Mathf.RoundToInt(vCurrentFortune));

    }

    #endregion 
}
