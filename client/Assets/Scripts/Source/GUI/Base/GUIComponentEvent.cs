

using UnityEngine;
using Game.Base;

//  GUIComponentEvent.cs
//  Author:Lu Zexi
//  2012-10-31


/// <summary>
/// GUI组件响应事件类
/// </summary>
public class GUIComponentEvent : MonoBehaviour
{
    public delegate void OnInputFunc(GUI_INPUT_INFO info, params object[] arg);  //输入事件委托定义
    private OnInputFunc m_delInput = null;  //输入委托
    private object[] m_vecArg;          //响应参数
    private float m_fLastTime;          //最近一次输入时间
    private float m_fClickTime = 0.1f;  //点击间隔时间

    private Vector2 m_vecDownPos;   //按下位置


    /// <summary>
    /// 设置点击间隔时间
    /// </summary>
    /// <param name="time"></param>
    public void SetClickTime(float time)
    {
        this.m_fClickTime = time;
    }

    /// <summary>
    /// 设置可用性
    /// </summary>
    public void SetEnable( bool enable )
    {
        this.enabled = enable;
    }

    /// <summary>
    /// 设置输入事件
    /// </summary>
    /// <param name="input"></param>
    public void AddIntputDelegate(OnInputFunc input, params object[] arg)
    {
        this.m_delInput += input;
        this.m_vecArg = arg;
    }

    /// <summary>
    /// 移除输入事件
    /// </summary>
    public void RemoveInputDelegate()
    {
        m_delInput = null;
    }

    /// <summary>
    /// OnHover (isOver) is sent when the mouse hovers over a collider or moves away.
    /// </summary>
    /// <param name="isOver"></param>
    protected void OnHover(bool isOver)
    {
        if (!enabled) return;
        GUI_INPUT_INFO info = new GUI_INPUT_INFO();
        info.m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.HOVER;
        info.m_bDone = isOver;
        SendEvent(info);
    }

    /// <summary>
    /// OnPress (isDown) is sent when a mouse button gets pressed on the collider.
    /// </summary>
    /// <param name="isDown"></param>
    protected void OnPress(bool isDown)
    {
        if (!enabled) return;

        if(isDown) this.m_vecDownPos = UICamera.currentTouch.pos;

        if (GuideManager.GetInstance().GetState() != GuideManager.GUIDE_STATE.NONE && (GuideManager.GetInstance().GetState() != GuideManager.GUIDE_STATE.BUTTON_PRESS && GuideManager.GetInstance().GetState() != GuideManager.GUIDE_STATE.BUTTON_LONG_DOWN && GuideManager.GetInstance().GetState() != GuideManager.GUIDE_STATE.SLIP ))
        {
            return;
        }

        if (!isDown && GuideManager.GetInstance().GetState() == GuideManager.GUIDE_STATE.BUTTON_PRESS)
        {
            GuideManager.GetInstance().ShowNext();
        }

        if (!isDown && GuideManager.GetInstance().GetState() == GuideManager.GUIDE_STATE.SLIP)
        {
            Vector2 dis = UICamera.currentTouch.pos - this.m_vecDownPos;

            if (GuideManager.GetInstance().OFFSET_X < 0)
            {
                if (dis.x > GuideManager.GetInstance().OFFSET_X)
                {
                    return;
                }
            }
            else 
            {
                if (dis.x < GuideManager.GetInstance().OFFSET_X)
                {
                    return;
                }
            }

            if (GuideManager.GetInstance().OFFSET_Y < 0)
            {
                if (dis.y > GuideManager.GetInstance().OFFSET_Y)
                {
                    return;
                }
            }
            else
            {
                if (dis.y < GuideManager.GetInstance().OFFSET_Y)
                {
                    return;
                }
            }
            GuideManager.GetInstance().ShowNext();
        }

        GUI_INPUT_INFO info = new GUI_INPUT_INFO();
        info.m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS;
        info.m_bDone = isDown;
        SendEvent(info);
    }

    /// <summary>
    /// OnSelect (selected) is sent when a mouse button is released on the same object as it was pressed on.
    /// </summary>
    /// <param name="selected"></param>
    protected void OnSelect(bool isSelected)
    {
        if (!enabled) return;
        GUI_INPUT_INFO info = new GUI_INPUT_INFO();
        info.m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.SELECT;
        info.m_bDone = isSelected;
        SendEvent(info);
    }

    /// <summary>
    /// OnClick () is sent with the same conditions as OnSelect, with the added check to see if the mouse has not moved much. UICamera.currentTouchID tells you which button was clicked.
    /// </summary>
    protected void OnClick()
    {
        if (!enabled) return;
        if (GAME_TIME.TIME_APP() - this.m_fLastTime < this.m_fClickTime)
            return;
        this.m_fLastTime = GAME_TIME.TIME_APP();

        if (GuideManager.GetInstance().GetState() != GuideManager.GUIDE_STATE.NONE && GuideManager.GetInstance().GetState() != GuideManager.GUIDE_STATE.BUTTON_CLICK)
        {
            return;
        }

        if (GuideManager.GetInstance().GetState() == GuideManager.GUIDE_STATE.BUTTON_CLICK)
        {
            GuideManager.GetInstance().ShowNext();
        }

        GUI_INPUT_INFO info = new GUI_INPUT_INFO();
        info.m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK;
        SendEvent(info);
    }


    /// <summary>
    /// OnDoubleClick () is sent when the click happens twice within a fourth of a second. UICamera.currentTouchID tells you which button was clicked.
    /// </summary>
    protected void OnDoubleClick()
    {
        if (!enabled) return;
        GUI_INPUT_INFO info = new GUI_INPUT_INFO();
        info.m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.DOUBLE_CLICK;
        SendEvent(info);
    }

    /// <summary>
    /// OnDrag (delta) is sent when a mouse or touch gets pressed on a collider and starts dragging it.
    /// </summary>
    /// <param name="delta"></param>
    protected void OnDrag(Vector2 delta)
    {
        if (!enabled) return;
        GUI_INPUT_INFO info = new GUI_INPUT_INFO();
        info.m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.DRAG;
        //当分辨率有改变时，拖动参数会错误
        //info.m_vecDelta = delta * GameGlobal.PixelSizeAdjustment;
        info.m_vecDelta = delta;
        SendEvent(info);
    }

    /// <summary>
    /// OnDrop (gameObject) is sent when the mouse or touch get released on a different collider than the one that was being dragged.
    /// </summary>
    /// <param name="gameobject"></param>
    protected void OnDrop(GameObject tarObj)
    {
        if (!enabled) return;
        GUI_INPUT_INFO info = new GUI_INPUT_INFO();
        info.m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.DROP;
        info.m_cTarget = tarObj;
        SendEvent(info);
    }

    /// <summary>
    ///  OnInput (text) is sent when typing (after selecting a collider by clicking on it).
    /// </summary>
    /// <param name="text"></param>
    protected void OnInput(string text)
    {
        if (!enabled) return;
        GUI_INPUT_INFO info = new GUI_INPUT_INFO();
        info.m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.INPUT;
        info.m_strText = text;
        SendEvent(info);
    }

    /// <summary>
    /// OnTooltip (show) is sent when the mouse hovers over a collider for some time without moving.
    /// </summary>
    /// <param name="show"></param>
    protected void OnTooltip(bool show)
    {
        if (!enabled) return;
        GUI_INPUT_INFO info = new GUI_INPUT_INFO();
        info.m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.TOOLTIP;
        info.m_bDone = show;
        SendEvent(info);
    }

    /// <summary>
    /// // - OnScroll (float delta) is sent out when the mouse scroll wheel is moved.
    /// </summary>
    /// <param name="delta"></param>
    protected void OnScroll(float delta)
    {
        if (!enabled) return;
        GUI_INPUT_INFO info = new GUI_INPUT_INFO();
        info.m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.SCROLL;
        info.m_fDelta = delta;
        SendEvent(info);
    }

    /// <summary>
    /// OnKey (KeyCode key) is sent when keyboard or controller input is used.
    /// </summary>
    /// <param name="key"></param>
    protected void OnKey(KeyCode key)
    {
        if (!enabled) return;
        GUI_INPUT_INFO info = new GUI_INPUT_INFO();
        info.m_eType = GUI_INPUT_INFO.GUI_INPUT_TYPE.KEY;
        info.m_eKey = key;
        SendEvent(info);
    }

    /// <summary>
    /// 发送事件
    /// </summary>
    public void SendEvent(GUI_INPUT_INFO info)
    {
        if (this.m_delInput != null)
        {
            this.m_delInput(info, this.m_vecArg);
        }
    }

}

