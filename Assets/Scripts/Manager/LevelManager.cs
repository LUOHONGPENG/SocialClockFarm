using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Human")]
    public Transform tfHuman;
    [HideInInspector]
    public List<HumanBasic> listHuman = new List<HumanBasic>();
    public List<HumanModel> listHumanModel = new List<HumanModel>();
    public GameObject pfHuman;


    [Header("Slot")]
    public List<SlotBasic> listSlot = new List<SlotBasic>();
    public Transform tfSlot;
    public GameObject pfSlot;

    private bool isInit = false;

    public void Init()
    {
        InitHuman();
        InitSlot();

        isInit = true;
    }

    #region HumanControl
    public void InitHuman()
    {
        listHumanModel.Clear();
        listHuman.Clear();
        for (int i = 0; i < 4; i++)
        {
            CreateHuman();
        }
    }

    public void CreateHuman()
    {
        //Create a Model
        HumanModel humanModel = new HumanModel(listHumanModel.Count, 50f);
        listHumanModel.Add(humanModel);
        //Create a Prefab
        GameObject objHuman = GameObject.Instantiate(pfHuman, tfHuman);
        HumanBasic itemHuman = objHuman.GetComponent<HumanBasic>();
        itemHuman.Init(humanModel);
        listHuman.Add(itemHuman);
        RefreshHumanPos();
    }

    /// <summary>
    /// Delete a Human and bring it to dead
    /// </summary>
    /// <param name="human"></param>
    public void DeleteHuman(HumanBasic human)
    {
        if (human != null)
        {
            Destroy(human.gameObject);
            listHuman.Remove(human);
            RefreshHumanPos();
        }
    }

    public void RefreshHumanPos()
    {
        int totalNum = listHuman.Count;
        for(int i = 0; i < listHuman.Count; i++)
        {
            Vector2 targetPos = PublicTool.CalculatePosDelta(totalNum, i, 2f,8);
            listHuman[i].transform.localPosition = targetPos;
            listHuman[i].SetHumanSlot();
        }
    }
    #endregion

    #region SlotControl

    public void InitSlot()
    {
        listSlot.Clear();
        CreateSlot(SlotType.Study, 1001, 2, new Vector2(-6f, -0.5f), 4, 60);
        CreateSlot(SlotType.Job, 2001, 3, new Vector2(-2f, 0), 16, 60);
        CreateSlot(SlotType.Job, 2002, 2, new Vector2(1f, 0), 23, 45, 30);
        CreateSlot(SlotType.Job, 2003, 1, new Vector2(-2f, -2.5f), 25, 60, 50);
        CreateSlot(SlotType.Marriage, 3001, 1, new Vector2(6f, 2f), 18, 35, 20,30);
        CreateSlot(SlotType.Retire, 4001, 1, new Vector2(6f, -2.5f), 60, 1000);

    }

    public void CreateSlot(SlotType slotType,int ID, int volume, Vector2 pos, int ageMin, int ageMax, int eduMin = 0, int careerMin = 0)
    {
        GameObject objSlot = GameObject.Instantiate(pfSlot, pos, Quaternion.Euler(Vector2.zero), tfSlot);
        SlotBasic itemSlot = objSlot.GetComponent<SlotBasic>();
        itemSlot.Init(slotType, ID, volume, ageMin, ageMax, eduMin, careerMin);
        listSlot.Add(itemSlot);
    }

    #endregion

    #region RetireControl

    public void Retire(HumanBasic human)
    {
        GameManager.Instance.uiManager.ShowRetire(human);
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
