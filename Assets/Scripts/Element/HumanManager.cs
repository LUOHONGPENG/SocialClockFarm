using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class HumanManager : MonoBehaviour
{
    [Header("Basic")]
    public SpriteRenderer srHuman;
    public Transform tfHuman;
    public CommonDragItem dragManager;

    [Header("UI")]
    public Text txAge;
    public Image imgFillEdu;
    public Text codeEdu;
    public Image imgFillFortune;
    public Text codeFortune;

    private bool isInit = false;
    private SlotManager currentSlot;

    private bool isDelaySchoolRed = false;


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
        TimeGoCheckRed();
    }

    public void TimeGoCheckRed()
    {
        if(isInSchool && humanData.Age > GameGlobal.ageMax_School)
        {
            isDelaySchoolRed = true;
        }
        else
        {
            isDelaySchoolRed = false;
        }

        if (isDelaySchoolRed)
        {
            srHuman.color = Color.red;
        }
        else
        {
            srHuman.color = Color.white;
        }
    }
    #endregion

    #region DragControl

    private void DragDeal()
    {
        //Condition
        SlotManager validSlot = null;
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
                    GameObject objSlot = hit.collider.gameObject;
                    SlotManager itemSlot = objSlot.GetComponent<SlotManager>();
                    if (itemSlot != null && currentSlot != itemSlot)
                    {
                        isHitSlot = true;
                        if (itemSlot.CheckHumanValid(humanData))
                        {
                            validSlot = itemSlot;
                            Debug.Log("ValidSlot");
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
                currentSlot.isFilled = false;
                currentSlot = null;
            }
            switch (validSlot.slotType)
            {
                case SlotType.Marriage:
                    this.humanData.RecordMarried();
                    GameManager.Instance.levelManager.ReachMarriage(validSlot.slotID);
                    dragManager.MoveBackInitialPoint();
                    break;
                case SlotType.Retire:
                    GameManager.Instance.levelManager.Retire(this);
                    break;
                default:
                    currentSlot = validSlot;
                    validSlot.isFilled = true;
                    SetHumanSlot();
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
                    currentSlot.isFilled = false;
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
            tfHuman.position = currentSlot.transform.position;
        }
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
