using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType
{
    Career,
    Marriage
}

public class SlotManager : MonoBehaviour
{
    public SlotType slotType;
    public Transform tfPos;

    public void SetLocalPos(Vector2 pos)
    {
        tfPos.localPosition = pos;
    }
}
