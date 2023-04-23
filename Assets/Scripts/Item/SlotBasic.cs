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

public enum ErrorType
{
    Full,
    isMarried,
    TooOld,
    TooYound,
    MoreEdu,
    MoreCareer
}

public class SlotCondition
{
    public SlotType slotType;
    public int ageMin;
    public int ageMax;
    public float eduMin;
    public float careerMin;

    public SlotCondition(SlotType slotType,int ageMin,int ageMax,int eduMin,int careerMin)
    {
        this.slotType = slotType;
        this.ageMin = ageMin;
        this.ageMax = ageMax;
        this.eduMin = eduMin;
        this.careerMin = careerMin;
    }

    public bool CheckSlotCondition(HumanModel humanModel)
    {
        if (humanModel.Age < ageMin)
        {
            GameManager.Instance.levelManager.TipEffect(slotType, ErrorType.TooYound);
            return false;
        }
        if (humanModel.Age > ageMax)
        {
            GameManager.Instance.levelManager.TipEffect(slotType, ErrorType.TooOld);
            return false;
        }
        if (humanModel.vEdu < eduMin)
        {
            GameManager.Instance.levelManager.TipEffect(slotType, ErrorType.MoreEdu);
            return false;
        }
        if (humanModel.vCareer < careerMin)
        {
            GameManager.Instance.levelManager.TipEffect(slotType, ErrorType.MoreCareer);
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
    private List<Vector2> listPointPos = new List<Vector2>();

    [Header("ConditionInfo")]
    public GameObject pfConditionNormal;
    public GameObject pfConditionMarry;
    public Transform tfCondition;
    private List<SlotCondition> listCondition = new List<SlotCondition>();

    #endregion

    public virtual void Init(SlotType slotType,int ID,int volume,int ageMin,int ageMax,int eduMin = 0, int careerMin = 0)
    {
        //SlotBasicInfo
        this.slotType = slotType;
        this.slotID = ID;
        this.maxVolume = volume;
        this.curVolume = 0;
        //SlotCondition
        listCondition.Clear();
        if (slotType == SlotType.Marriage)
        {
            for(int i = 0; i < 2; i++)
            {
                listCondition.Add(new SlotCondition(slotType,ageMin + Random.Range(-2, 2), ageMax + Random.Range(-5, 12), Random.Range(1, eduMin), Random.Range(1, careerMin)));
            }
        }
        else
        {
            listCondition.Add(new SlotCondition(slotType, ageMin, ageMax, eduMin, careerMin));
        }

        if (slotType== SlotType.Study)
        {
            listPointPos.Add(new Vector2(-0.86f, 0.549f));
            listPointPos.Add(new Vector2(0, 1f));
            listPointPos.Add(new Vector2(1, 0));
        }
        else
        {
            listPointPos.Add(new Vector2(-0.6f, 0.5f));
            listPointPos.Add(new Vector2(0, 1f));
            listPointPos.Add(new Vector2(0.6f, 0.5f));
        }

        InitView();
    }

    public virtual void InitView()
    {
        srSlot.sprite = listSpSlot[(int)slotType];
        colSlot = srSlot.gameObject.AddComponent<PolygonCollider2D>();

        PublicTool.ClearChildItem(tfCondition);

        if (slotType == SlotType.Study)
        {
            GameObject objCondition = GameObject.Instantiate(pfConditionNormal, tfCondition);
            SlotConditionUI itemCondition = objCondition.GetComponent<SlotConditionUI>();
            itemCondition.InitUI(new SlotCondition(slotType, GameGlobal.ageMin_School, GameGlobal.ageMax_School, 0,0));
        }
        else if(slotType == SlotType.Job|| slotType == SlotType.Retire)
        {
            GameObject objCondition = GameObject.Instantiate(pfConditionNormal, tfCondition);
            SlotConditionUI itemCondition = objCondition.GetComponent<SlotConditionUI>();
            itemCondition.InitUI(listCondition[0]);
        }
        else//Marriage
        {
            for(int i = 0; i < listCondition.Count; i++)
            {
                GameObject objCondition = GameObject.Instantiate(pfConditionMarry, tfCondition);
                SlotConditionUI itemCondition = objCondition.GetComponent<SlotConditionUI>();
                itemCondition.InitUI(listCondition[i]);
            }
        }
        RefreshVolumeView();
    }

    #region CheckValid

    //CheckWhetherAHumanValid
    public virtual bool CheckHumanValid(HumanModel humanModel)
    {
        if (isFilled)
        {
            GameManager.Instance.levelManager.TipEffect(slotType, ErrorType.Full);
            return false;
        }

        if (slotType == SlotType.Marriage)
        {
            if (humanModel.isMarried)
            {
                GameManager.Instance.levelManager.TipEffect(slotType, ErrorType.isMarried);
                return false;
            }
            else
            {
                bool isMatch = false;
                for (int i = 0; i < listCondition.Count; i++)
                {
                    isMatch =  listCondition[i].CheckSlotCondition(humanModel);
                    if (isMatch)
                    {
                        GameManager.Instance.levelManager.MarryReadyId = i;
                        return true;
                    }
                }
                return false;
            }
        }
        else
        {
            return listCondition[0].CheckSlotCondition(humanModel);
        }
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

    #region MarryNewCondition

    public void MarryRenewCondition(int ID)
    {
        if(ID < listCondition.Count)
        {
            listCondition[ID] = (new SlotCondition(slotType, 18 + Random.Range(-2, 2), 35 + Random.Range(-5, 12), Random.Range(0, 20), Random.Range(0, 30)));

            PublicTool.ClearChildItem(tfCondition);

            for (int i = 0; i < listCondition.Count; i++)
            {
                GameObject objCondition = GameObject.Instantiate(pfConditionMarry, tfCondition);
                SlotConditionUI itemCondition = objCondition.GetComponent<SlotConditionUI>();
                itemCondition.InitUI(listCondition[i]);
            }
        }
    }
    #endregion
}
