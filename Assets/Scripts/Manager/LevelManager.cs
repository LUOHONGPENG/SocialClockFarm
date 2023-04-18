using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Human")]
    public Transform tfHuman;
    [HideInInspector]
    public List<HumanManager> listHuman = new List<HumanManager>();
    public List<HumanData> listHumanData = new List<HumanData>();
    public GameObject pfHuman;

    [Header("Career")]
    public CareerManager careerSchool;
    public CareerManager careerJob0;
    public CareerManager careerJob1;
    public CareerManager careerJob2;

    [Header("Marriage")]
    public List<MarriageManager> listMarriage = new List<MarriageManager>();
    public Transform tfMarriage;
    public GameObject pfMarriage;
    private int countID_marriage;

    [Header("Retirement")]
    public RetireManager retireManager;


    private bool isInit = false;

    public void Init()
    {
        listHumanData.Clear();
        listHuman.Clear();

        InitHuman();
        InitCareer();
        InitMarriage();
        retireManager.Init();

        isInit = true;
    }

    #region HumanControl
    public void InitHuman()
    {
        for(int i = 0; i < 3; i++)
        {
            CreateNewHuman();
        }
    }

    public void CreateNewHuman()
    {
        HumanData humanData = new HumanData(listHumanData.Count, 50f);
        listHumanData.Add(humanData);
        CreateNewHumanPrefab(humanData);
    }

    public void CreateNewHumanPrefab(HumanData humanData)
    {
        GameObject objHuman = GameObject.Instantiate(pfHuman, tfHuman);
        HumanManager itemHuman = objHuman.GetComponent<HumanManager>();
        itemHuman.Init(humanData);
        listHuman.Add(itemHuman);
        RefreshHumanPos();
    }

    public void DeleteHuman()
    {

    }

    public void RefreshHumanPos()
    {
        int totalNum = listHuman.Count;
        for(int i = 0; i < listHuman.Count; i++)
        {
            Vector2 targetPos = PublicTool.CalculatePosDelta(totalNum, i, 2f);
            listHuman[i].transform.localPosition = targetPos;
            listHuman[i].SetHumanSlot();
        }
    }
    #endregion

    #region CareerControl
    public void InitCareer()
    {
        careerSchool.Init(SlotType.School);
        careerJob0.Init(SlotType.Job0);
        careerJob1.Init(SlotType.Job1);
        careerJob2.Init(SlotType.Job2);
    }
    #endregion

    #region MarriageControl

    public void InitMarriage()
    {
        listMarriage.Clear();
        for(int i = 0; i < 2; i++)
        {
            CreateMarriage();
        }
    }

    public void CreateMarriage()
    {
        GameObject objMarriage = GameObject.Instantiate(pfMarriage, tfMarriage);
        MarriageManager itemMarriage = objMarriage.GetComponent<MarriageManager>();
        itemMarriage.Init(countID_marriage);
        listMarriage.Add(itemMarriage);
        RefreshMarriagePos();
    }

    public void RefreshMarriagePos()
    {
        int totalNum = listMarriage.Count;
        for (int i = 0; i < listMarriage.Count; i++)
        {
            Vector2 targetPos = PublicTool.CalculatePosDelta(totalNum, i, 2f);
            listMarriage[i].transform.localPosition = targetPos;
        }
    }
    public void ReachMarriage(int ID)
    {
        MarriageManager targetMarriage = null;
        foreach(var marriage in listMarriage)
        {
            if (marriage.marriageID == ID)
            {
                targetMarriage = marriage;
            }
        }

        if (targetMarriage != null)
        {
            Destroy(targetMarriage.gameObject);
            listMarriage.Remove(targetMarriage);
            CreateMarriage();
            CreateNewHuman();
        }

    }


    #endregion

    #region RetireControl

    public void Retire(HumanManager human)
    {

    }

    #endregion

    #region TimeControl

    public void TimeGo()
    {
        if (!isInit||GameManager.Instance.isTimeStop)
        {
            return;
        }

        for(int i = 0; i < listHuman.Count; i++)
        {
            listHuman[i].TimeGo();
        }
    }


    #endregion

}
