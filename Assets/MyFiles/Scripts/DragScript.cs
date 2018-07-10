using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragScript : MonoBehaviour ,IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler{
    public  bool isFalse = false;
    public  bool isRot = false;
    public  bool isWin = false;

    private int moveInt = 0;
    private Vector2 initPos;

    public void OnDrag(PointerEventData eventData)
    {

      Vector2 result;
      RectTransformUtility.ScreenPointToLocalPointInRectangle(
      this.transform.parent as RectTransform,
      eventData.position,
      eventData.pressEventCamera,
      out result);
      this.transform.GetComponent<RectTransform>().anchoredPosition = result;
    }       

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (moveInt == 0)
        {
            initPos = this.transform.GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (this.gameObject.name != "wBtn")
        {
            if ((this.transform.GetComponent<RectTransform>().anchoredPosition - initPos).magnitude >= 20)
            {
                isFalse = true;
                //Debug.Log("有错误移动操作！");
            }
            else
            {
                if (isFalse)
                {
                    isFalse = false;
                    //Debug.Log("错误移动操作被消除！");
                    isWinTrue();
                }
            }
        }
        else
        {
            Vector2 tempPos = new Vector2();
            switch(GameAuto.topicInt)
            {
                case 0://第一关
                    tempPos = new Vector3(-763, 42, 0);
                    break;
                case 1:
                    tempPos = new Vector3(-7.9f, 0.8f, 0);
                    break;
                case 2:
                    tempPos = new Vector3(180, 0, 0);
                    break;
                case 3:
                    tempPos = new Vector3(460, 80, 0);
                    break;
                case 4:
                    tempPos = new Vector3(283, 76, 0);
                    break;
            }
            float dis = (this.transform.GetComponent<RectTransform>().anchoredPosition - tempPos).magnitude;
            //Debug.Log("dis ======  " + dis);
            if (dis >= 6)
            {
                isWin = false;
                //Debug.Log("选择正确，未移到正确位置！");
            }
            else
            {
                isWin = true;
                //Debug.Log("胜利!");
                isWinTrue();
            }
        }
        moveInt++;
    }

    public void OnDrop(PointerEventData eventData)
    {

    }


    public void SetIsFalsrRot(bool bl)
    {
        isRot = bl;
    }

    bool isWinTrue()
    {
        GameAuto.isWin = true;
        //Debug.Log("TopicInt === " + GameAuto.topicInt);
        foreach (Transform ts in GameAuto.tempParent.transform)
        {
            if (ts.gameObject.name == "preMatch(Clone)")
            {
                isWinChild(ts.gameObject);
            }
        }

        if (GameAuto.isWin)
        {
            //Debug.Log("真正的胜利！");   
            int glevel = GameAuto.topicInt + 1;
            GameAuto.instance.EnabledWin("恭喜你，顺利完成，第[" + glevel + "]关！");
        }
        return GameAuto.isWin;
    }

    void isWinChild(GameObject ob)
    {
        foreach (Transform child in ob.transform)
        {
            if (child.gameObject.activeSelf)
            {
                
                if (child.gameObject.name == "wBtn")
                {
                    if (!child.GetComponent<DragScript>().isWin)
                    {
                        GameAuto.isWin = false;
                        return;
                    }
                    bool isCRot = false;
                    switch (GameAuto.topicInt)
                    {
                        case 0:
                            isCRot = true;//第一关 主旗子 要转
                            break;
                        case 1:
                            isCRot = false;//第二关 主旗子 不要转
                            break;
                        case 2:
                            isCRot = true;//第三关 主旗子 要转
                            break;
                        case 3:
                            isCRot = false;//第四关 主旗子 不要转
                            break;
                        case 4:
                            isCRot = true;//第五关 主旗子 要转
                            break;
                    }
                    if (child.GetComponent<DragScript>().isRot != isCRot)
                    {
                        GameAuto.isWin = false;
                        return;
                    }
                } 
                else
                {
                    if (child.GetComponent<DragScript>().isFalse)
                    {
                        GameAuto.isWin = false;
                        return;
                    }
                    bool isCRot = false;

                    if (child.GetComponent<DragScript>().isRot != isCRot)
                    {
                        GameAuto.isWin = false;
                        return;
                    }
                }
            }
        }
      }

}
