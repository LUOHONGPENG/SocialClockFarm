using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarriageManager : MonoBehaviour
{
    [Header("Slot")]
    public Transform tfSlot;
    public GameObject pfSlot;
    [Header("ConditionUI")]
    public Text txAge;
    public Text txEdu;
    public Text txFortune;

    //Data
    private int ageMin;
    private int ageMax;
    private float limitEdu;
    private float limitFortune;

    private bool isInit = false;

    public void Init()
    {
        PublicTool.ClearChildItem(tfSlot);

        ageMin = GameGlobal.ageMin_Marriage;
        ageMax = GameGlobal.ageMax_Marriage;

        limitEdu = Random.Range(0, 50);
        limitFortune = Random.Range(0, 30);

        InitSlot();
        InitUI();

        isInit = true;
    }

    public void InitUI()
    {
        txAge.text = string.Format("{0}-{1}", ageMin, ageMax);
        txEdu.text = string.Format(">{0}%", Mathf.CeilToInt(limitEdu));
        txFortune.text = string.Format(">{0}%", Mathf.CeilToInt(limitFortune));
    }

    public void InitSlot()
    {
        GameObject objSlot = GameObject.Instantiate(pfSlot, tfSlot);
        SlotManager itemSlot = objSlot.GetComponent<SlotManager>();
        itemSlot.Init(SlotType.Marriage, ageMin, ageMax, limitEdu,limitFortune);
    }


}
