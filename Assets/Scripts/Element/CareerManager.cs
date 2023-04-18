using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SlotType
{
    School,
    Job0,
    Job1,
    Job2,
    Marriage
}

public class CareerManager : MonoBehaviour
{
    [Header("Slot")]
    public Transform tfSlot;
    public GameObject pfSlot;
    [Header("ConditionUI")]
    public Text txAge;
    public GameObject objConditionEdu;
    public Text txEdu;

    //Data
    private SlotType slotType;
    private int ageMin;
    private int ageMax;
    private float limitEdu;


    private List<SlotManager> listSlot = new List<SlotManager>();

    public void Init(SlotType slotType)
    {
        this.slotType = slotType;
        switch (slotType)
        {
            case SlotType.School:
                this.ageMin = GameGlobal.ageMin_School;
                this.ageMax = GameGlobal.ageMax_School;
                break;
            case SlotType.Job0:
                this.limitEdu = GameGlobal.arrayEduLevel[0];
                this.ageMin = GameGlobal.ageMin_Job;
                this.ageMax = GameGlobal.ageMax_Job;
                break;
            case SlotType.Job1:
                this.limitEdu = GameGlobal.arrayEduLevel[1];
                this.ageMin = GameGlobal.ageMin_Job;
                this.ageMax = GameGlobal.ageMax_Job;
                break;
            case SlotType.Job2:
                this.limitEdu = GameGlobal.arrayEduLevel[2];
                this.ageMin = GameGlobal.ageMin_Job;
                this.ageMax = GameGlobal.ageMax_Job;
                break;
        }

        InitSlot();
        InitUI();
    }

    public void InitUI()
    {
        if (limitEdu > 0)
        {
            objConditionEdu.SetActive(true);
            txEdu.text = string.Format(">{0}%", limitEdu);
        }
        else
        {
            objConditionEdu.SetActive(false);
        }

        txAge.text = string.Format("{0}-{1}", ageMin, ageMax);
    }


    #region SlotControl
    public void InitSlot()
    {
        int slotNum = 0;
        switch (slotType)
        {
            case SlotType.School:
                slotNum = 2;
                break;
            case SlotType.Job0:
                slotNum = 3;
                break;
            case SlotType.Job1:
                slotNum = 2;
                break;
            case SlotType.Job2:
                slotNum = 1;
                break;
        }

        for(int i = 0; i < slotNum; i++)
        {
            CreateSlot(slotType);
        }
        RefreshSlotPos();
    }

    /// <summary>
    /// Create Single Slot
    /// </summary>
    public void CreateSlot(SlotType slotType)
    {
        GameObject objSlot = GameObject.Instantiate(pfSlot, tfSlot);
        SlotManager itemSlot = objSlot.GetComponent<SlotManager>();
        itemSlot.Init(slotType,ageMin,ageMax,limitEdu);
        listSlot.Add(itemSlot);
    }

    /// <summary>
    /// 
    /// </summary>
    public void RefreshSlotPos()
    {
        int totalNum = listSlot.Count;
        for (int i = 0; i < listSlot.Count; i++)
        {
            Vector2 targetPos = PublicTool.CalculatePosDelta(totalNum, i, 1f);
            listSlot[i].SetLocalPos(targetPos);
        }
    }
    #endregion
}
