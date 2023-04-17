using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class VillagerManager : MonoBehaviour
{
    public SpriteRenderer srVillager;

    public CommonDragItem dragManager;

    private bool isInit = false;

    public void Init()
    {
        if (dragManager != null)
        {
            dragManager.InitDrag();
        }
    }
}
