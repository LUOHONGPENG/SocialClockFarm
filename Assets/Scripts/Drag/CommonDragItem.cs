using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonDragItem : MonoBehaviour
{

    public Collider2D colDrag;
    protected float dragInitStartPosX;
    protected float dragInitStartPosY;
    protected float dragStartPosX;
    protected float dragStartPosY;

    public bool isDragging = false;
    protected bool canDrag = false;
    public bool isInitDrag = false;

    //Initialize the Drag Component
    public void InitDrag()
    {
        dragInitStartPosX = this.transform.position.x;
        dragInitStartPosY = this.transform.position.y;
        isInitDrag = true;
        canDrag = true;
    }

    public void TimeGoDrag()
    {
        if (isInitDrag)
        {
            CheckWhetherDrag();
            CheckDrag();
        }
    }


    //
    public void CheckDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = PublicTool.GetMousePosition2D();
            this.gameObject.transform.position = new Vector3(mousePos.x - dragStartPosX, mousePos.y - dragStartPosY, 0);
        }
    }

    public void CheckWhetherDrag()
    {
        if (Input.GetMouseButtonDown(0) && canDrag)
        {
            Vector3 mousePosNew = Input.mousePosition;
            mousePosNew.z = 10;
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(mousePosNew);
            RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider == colDrag)
                {
                    Vector3 mousePos = PublicTool.GetMousePosition2D();
                    dragStartPosX = mousePos.x - this.transform.position.x;
                    dragStartPosY = mousePos.y - this.transform.position.y;
                    isDragging = true;
                    return;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                isDragging = false;
                DealDragDown();
            }
        }
    }

    public void DealDragDown()
    {

    }
}
