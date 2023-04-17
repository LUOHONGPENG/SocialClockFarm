using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class VillagerManager : MonoBehaviour
{
    public Transform tfPos;
    public SpriteRenderer srVillager;
    public CommonDragItem dragManager;

    private bool isInit = false;

    public void Init(VillagerData villagerData)
    {
        if (dragManager != null)
        {
            dragManager.InitDrag();
            dragManager.dragDealAction = delegate ()
            {
                Vector3 screenPos = PublicTool.GetMousePosition2D();
                RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero);

                if (hit.collider != null)
                {
                    if (hit.collider.tag == "Slot")
                    {
                        GameObject objSlot = hit.collider.gameObject;
                        SlotManager itemSlot = objSlot.GetComponent<SlotManager>();
                        if (itemSlot != null)
                        {

                            return;
                        }
                    }
                }


            };
        }

        this.villagerData = villagerData;
    }

    public void SetLocalPos(Vector2 pos)
    {
        tfPos.localPosition = pos;
    }

    public void TimeGo()
    {
        TimeGoCheckDrag();
        TimeGoVillager();
    }


    private void TimeGoCheckDrag()
    {
        if (dragManager != null)
        {
            dragManager.TimeGoDrag();
            if (dragManager.isDragging)
            {
                srVillager.maskInteraction = SpriteMaskInteraction.None;
            }
            else
            {
                srVillager.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }
        }
    }
}
