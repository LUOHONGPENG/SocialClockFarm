using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetireManager : MonoBehaviour
{
    public Text txAge;
    public Transform tfSlot;
    public GameObject pfSlot;

    public void Init()
    {
        //UI
        txAge.text = string.Format("{0}+", GameGlobal.ageMin_Retire);
        //Slot
        GameObject objSlot = GameObject.Instantiate(pfSlot, tfSlot);
        SlotManager itemSlot = objSlot.GetComponent<SlotManager>();
        itemSlot.Init(SlotType.Retire, GameGlobal.ageMin_Retire, 1000);

    }

}
