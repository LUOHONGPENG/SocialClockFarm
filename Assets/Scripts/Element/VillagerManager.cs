using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class VillagerManager : MonoBehaviour
{
    public Transform tfPos;
    public SpriteRenderer srVillager;
    public CommonDragItem dragManager;

    private bool isInit = false;

    public void Init(VillagerData villagerData)
    {
        if (dragManager != null)
        {
            dragManager.InitDrag();
        }

        this.villagerData = villagerData;
    }

    public void SetLocalPos(Vector2 pos)
    {
        tfPos.localPosition = pos;
    }
}
