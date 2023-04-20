using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SlotType
{ 
    Study,
    Job,
    Marriage,
    Retire
}

public class SlotCondition
{
    public int ageMin;
    public int ageMax;
    public float eduMin;
    public float careerMin;

    public SlotCondition(int ageMin,int ageMax,int eduMin,int careerMin)
    {
        this.ageMin = ageMin;
        this.ageMax = ageMax;
        this.eduMin = eduMin;
        this.careerMin = careerMin;
    }

    public bool CheckSlotCondition(HumanModel humanModel)
    {
        if (humanModel.Age < ageMin)
        {
            return false;
        }
        if (humanModel.Age > ageMax)
        {
            return false;
        }
        if (humanModel.vEdu < eduMin)
        {
            return false;
        }
        if (humanModel.vCareer < careerMin)
        {
            return false;
        }
        return true;
    }
}

public class SlotBasic : MonoBehaviour
{
    #region Define
    [Header("BasicInfo")]
    public SlotType slotType;
    public int slotID;

    [Header("ViewInfo")]
    public Text txVolume;
    public SpriteRenderer srSlot;
    public List<Sprite> listSpSlot = new List<Sprite>();
    private PolygonCollider2D colSlot;
    public SlotConditionUI conditionUI;
    public SlotConditionUI conditionUIExtra;


    [Header("FillInfo")]
    private int maxVolume;
    private int curVolume;
    public bool isFilled
    {
        get
        {
            if (curVolume < maxVolume)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    private List<HumanBasic> listPointHuman = new List<HumanBasic>() { null, null, null };
    private List<Vector2> listPointPos = new List<Vector2>() {new Vector2(-1,0), new Vector2(0, 1), new Vector2(1, 0)};

    [Header("DataInfo")]
    private List<SlotCondition> listCondition = new List<SlotCondition>();

    #endregion

    public void Init(SlotType slotType,int ID,int volume,int ageMin,int ageMax,int eduMin = 0, int careerMin = 0)
    {
        //SlotBasicInfo
        this.slotType = slotType;
        this.slotID = ID;
        this.maxVolume = volume;
        this.curVolume = 0;
        //SlotCondition
        listCondition.Clear();
        if(slotType== SlotType.Marriage)
        {
            listCondition.Add(new SlotCondition(ageMin + Random.Range(-2, 2), ageMax + Random.Range(-5, 12), eduMin, careerMin));
            listCondition.Add(new SlotCondition(ageMin + Random.Range(-2, 2), ageMax + Random.Range(-5, 12), eduMin, careerMin));
        }
        else
        {
            listCondition.Add(new SlotCondition(ageMin, ageMax, eduMin, careerMin));
        }

        InitView();
    }

    public void InitView()
    {
        srSlot.sprite = listSpSlot[(int)slotType];
        colSlot = srSlot.gameObject.AddComponent<PolygonCollider2D>();

        conditionUIExtra.gameObject.SetActive(false);
        if (slotType == SlotType.Study)
        {
            conditionUI.InitUI(new SlotCondition(GameGlobal.ageMin_School, GameGlobal.ageMax_School, 0,0));
        }
        else if(slotType == SlotType.Marriage)
        {
            conditionUI.InitUI(listCondition[0]);
            conditionUIExtra.gameObject.SetActive(true);
            conditionUIExtra.InitUI(listCondition[1]);
        }
        else
        {
            conditionUI.InitUI(listCondition[0]);
        }
        RefreshVolumeView();
    }

    #region CheckValid

    //CheckWhetherAHumanValid
    public bool CheckHumanValid(HumanModel humanModel)
    {
        if (isFilled)
        {
            return false;
        }

        if (slotType == SlotType.Marriage)
        {
            if (humanModel.isMarried)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < listCondition.Count; i++)
                {
                    return listCondition[i].CheckSlotCondition(humanModel);
                }
            }
        }
        else
        {
            return listCondition[0].CheckSlotCondition(humanModel);
        }
        return true;
    }
    #endregion

    #region Bind
    public void BindHuman(HumanBasic humanBasic)
    {
        for(int i = 0; i < listPointHuman.Count; i++)
        {
            if (listPointHuman[i] == null)
            {
                curVolume++;
                listPointHuman[i] = humanBasic;
                ResetHumanPos(humanBasic);
                RefreshVolumeView();
                return;
            }
        }
    }
    
    public void ResetHumanPos(HumanBasic humanBasic)
    {
        for (int i = 0; i < listPointHuman.Count; i++)
        {
            if (listPointHuman[i] == humanBasic)
            {
                listPointHuman[i].srHuman.transform.position 
                    = new Vector2(this.transform.position.x + listPointPos[i].x, this.transform.position.y + listPointPos[i].y);
            }
        }
    }

    public void UnBindHuman(HumanBasic humanBasic)
    {
        for (int i = 0; i < listPointHuman.Count; i++)
        {
            if (listPointHuman[i] == humanBasic)
            {
                curVolume--;
                listPointHuman[i] = null;
                RefreshVolumeView();
            }
        }
    }
    #endregion

    #region VolumeView

    public void RefreshVolumeView()
    {
        if(slotType == SlotType.Study|| slotType == SlotType.Job)
        {
            txVolume.gameObject.SetActive(true);
            txVolume.text = string.Format("{0}/{1}", curVolume, maxVolume);
        }
        else
        {
            txVolume.gameObject.SetActive(false);
        }
    }

    #endregion
}
