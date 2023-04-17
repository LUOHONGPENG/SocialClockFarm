using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CareerType
{
    School,
    Company
}


public class CareerManager : MonoBehaviour
{
    public CareerType careerType;
    public Transform tfSlot;
    public GameObject pfSlot;
    public List<SlotManager> listSlot;

    public void Init(CareerType careerType)
    {
        this.careerType = careerType;
        InitSlot();
    }

    #region SlotControl
    public void InitSlot()
    {
        for(int i = 0; i < 2; i++)
        {
            CreateSlot();
        }
        RefreshSlotPos();
    }

    public void CreateSlot()
    {
        GameObject objSlot = GameObject.Instantiate(pfSlot, tfSlot);
        SlotManager itemSlot = objSlot.GetComponent<SlotManager>();
        //itemSlot.
        listSlot.Add(itemSlot);
    }

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
