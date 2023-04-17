using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class VillagerManager : MonoBehaviour
{
    public Transform tfPos;
    public SpriteRenderer srVillager;
    public CommonDragItem dragManager;

    private bool isInit = false;
    private SlotManager currentSlot;

    #region Basic
    public void Init(VillagerData villagerData)
    {
        if (dragManager != null)
        {
            dragManager.InitDrag(srVillager);
            dragManager.dragDealAction = delegate ()
            {
                DragDeal();
            };
        }
        this.villagerData = villagerData;
    }

    public void TimeGo()
    {
        TimeGoCheckDrag();
        TimeGoVillager();
    }

    public void SetLocalPos(Vector2 pos)
    {
        tfPos.localPosition = pos;
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
                        if (itemSlot.CheckVillagerValid(villagerData))
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
}
