using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class CommonDragItem : MonoBehaviour
{
    public SpriteRenderer srTarget;
    public Collider2D colDrag;
    protected float dragInitStartPosX;
    protected float dragInitStartPosY;
    protected float dragCurrentStartPosX;
    protected float dragCurrentStartPosY;
    protected float dragStartPosX;
    protected float dragStartPosY;

    public bool isDragging = false;
    protected bool canDrag = false;
    public bool isInitDrag = false;

    public UnityAction dragDealAction;

    //Initialize the Drag Component
    public void InitDrag(SpriteRenderer srTarget)
    {
        this.srTarget = srTarget;

        dragInitStartPosX = this.transform.localPosition.x;
        dragInitStartPosY = this.transform.localPosition.y;
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
            /*            Vector3 mousePosNew = Input.mousePosition;
                        mousePosNew.z = 10;
                        Vector3 screenPos = Camera.main.ScreenToWorldPoint(mousePosNew);*/
            Vector3 screenPos = PublicTool.GetMousePosition2D();
            RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero);

            dragCurrentStartPosX = this.transform.localPosition.x;
            dragCurrentStartPosY = this.transform.localPosition.y;

            if (hit.collider != null)
            {
                if (hit.collider == colDrag)
                {
                    Vector3 mousePos = PublicTool.GetMousePosition2D();
                    dragStartPosX = mousePos.x - this.transform.position.x;
                    dragStartPosY = mousePos.y - this.transform.position.y;
                    isDragging = true;
                    srTarget.maskInteraction = SpriteMaskInteraction.None;
                    return;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                isDragging = false;
                srTarget.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                dragDealAction.Invoke();
            }
        }
    }

    public void MoveBackInitialPoint()
    {
        this.transform.DOLocalMove(new Vector2(dragInitStartPosX, dragInitStartPosY), 0.5f);
        StartCoroutine(IE_MoveBackDeal());
    }

    public void MoveBackStartPoint()
    {
        this.transform.DOLocalMove(new Vector2(dragCurrentStartPosX, dragCurrentStartPosY), 0.5f);
        StartCoroutine(IE_MoveBackDeal());
    }

    public IEnumerator IE_MoveBackDeal()
    {
        srTarget.maskInteraction = SpriteMaskInteraction.None;
        canDrag = false;
        yield return new WaitForSeconds(0.5f);
        srTarget.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        canDrag = true;
    }

}
