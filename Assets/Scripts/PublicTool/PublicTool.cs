using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicTool : MonoBehaviour
{
    public static void ClearChildItem(UnityEngine.Transform tf)
    {
        foreach (UnityEngine.Transform item in tf)
        {
            UnityEngine.Object.Destroy(item.gameObject);
        }
    }
    public static Vector2 GetMousePosition2D()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(mousePosition.x, mousePosition.y);
    }

    public static Vector2 CalculatePosDelta(int totalNum,int index,float span)
    {
        float posx = 0;
        float posy = 0;
        if (totalNum <= 10)
        {
            //if totalNum = 8
            int totalrow = totalNum / 5 + 1;//2
            int rowIndex = index / 5;
            int columnIndex = index % 5;
            int currentRowColumn = 0;
            if(index >= (totalrow - 1) * 5)
            {
                currentRowColumn = totalNum % 5;
            }
            else
            {
                currentRowColumn = 5;
            }
            posx = span * -0.5f * (currentRowColumn - 1) + columnIndex * span;
            posy = span * 0.5f * (totalrow - 1) + rowIndex * span;
            return new Vector2(posx, posy);
        }
        return Vector2.zero;
    }
}
