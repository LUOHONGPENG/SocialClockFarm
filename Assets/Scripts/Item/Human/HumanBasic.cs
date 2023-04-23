using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class HumanBasic : MonoBehaviour
{
    [Header("Basic")]
    public SpriteRenderer srHuman;
    public Transform tfHuman;
    public CommonDragItem dragManager;

    private bool isInit = false;
    private SlotBasic currentSlot;


    #region Init
    public void Init(HumanModel humanModel)
    {
        if (dragManager != null)
        {
            dragManager.InitDrag(srHuman);
            dragManager.dragDealAction = delegate ()
            {
                DragDeal();
            };
        }
        this.humanModel = humanModel;
        RefreshUI();
    }
    #endregion

    #region DragControl

    private void DragDeal()
    {
        //Condition
        SlotBasic validSlot = null;
        bool isHitSlot = false;
        //CheckWhetherSlot
        Vector3 screenPos = PublicTool.GetMousePosition2D();
        RaycastHit2D[] hits = Physics2D.RaycastAll(screenPos, Vector2.zero);

        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Slot")
                {
                    GameObject objSlot = hit.collider.transform.parent.gameObject;
                    SlotBasic itemSlot = objSlot.GetComponent<SlotBasic>();
                    if (itemSlot != null && currentSlot != itemSlot)
                    {
                        isHitSlot = true;
                        if (itemSlot.CheckHumanValid(humanModel))
                        {
                            validSlot = itemSlot;
                            break;
                        }
                    }
                }
            }
        }

        //DealSlot
        if(validSlot != null)
        {
            //Unbind the current Slot
            if (currentSlot != null)
            {
                currentSlot.UnBindHuman(this);
                currentSlot = null;
            }
            switch (validSlot.slotType)
            {
                case SlotType.Marriage:
                    this.humanModel.RecordMarried();
                    GameManager.Instance.levelManager.ReachMarriage();
                    dragManager.MoveBackInitialPoint();
                    GameManager.Instance.soundManager.PlaySound(SoundType.Marriage);
                    break;
                case SlotType.Retire:
                    GameManager.Instance.levelManager.Retire(this);
                    break;
                case SlotType.Study:
                    GameManager.Instance.soundManager.PlaySound(SoundType.Study);
                    currentSlot = validSlot;
                    currentSlot.BindHuman(this);
                    break;
                case SlotType.Job:
                    GameManager.Instance.soundManager.PlaySound(SoundType.Job);
                    currentSlot = validSlot;
                    currentSlot.BindHuman(this);
                    break;
            }
        }
        else
        {
            if (isHitSlot && currentSlot !=null)
            {
                //Back to the current Slot
                dragManager.MoveBackStartPoint();
            }
            else
            {
                //Back to the Hole
                if (currentSlot != null)
                {
                    currentSlot.UnBindHuman(this);
                    currentSlot = null;
                }
                dragManager.MoveBackInitialPoint();
            }
        }
    }

    public void SetHumanSlot()
    {
        if (currentSlot != null)
        {
            currentSlot.ResetHumanPos(this);
        }
    }
    #endregion


}
