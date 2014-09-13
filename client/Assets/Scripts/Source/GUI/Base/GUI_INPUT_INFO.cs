using UnityEngine;


//  GUI_INPUT_INFO.cs
//  Author: Lu Zexi
//  2013-11-13


/// <summary>
/// GUI事件结构体
/// </summary>
public struct GUI_INPUT_INFO
{
    /// <summary>
    /// 输入事件类型
    /// </summary>
    public enum GUI_INPUT_TYPE
    {
        NONE = 0,       //NONE
        HOVER,          /// - OnHover (isOver) is sent when the mouse hovers over a collider or moves away.
        PRESS,          /// - OnPress (isDown) is sent when a mouse button gets pressed on the collider.
        SELECT,         /// - OnSelect (selected) is sent when a mouse button is released on the same object as it was pressed on.
        CLICK,          /// - OnClick () is sent with the same conditions as OnSelect, with the added check to see if the mouse has not moved much. UICamera.currentTouchID tells you which button was clicked.
        DOUBLE_CLICK,   /// - OnDoubleClick () is sent when the click happens twice within a fourth of a second. UICamera.currentTouchID tells you which button was clicked.
        DRAG,           /// - OnDrag (delta) is sent when a mouse or touch gets pressed on a collider and starts dragging it.
        DROP,           /// - OnDrop (gameObject) is sent when the mouse or touch get released on a different collider than the one that was being dragged.
        INPUT,          /// - OnInput (text) is sent when typing (after selecting a collider by clicking on it).
        TOOLTIP,        /// - OnTooltip (show) is sent when the mouse hovers over a collider for some time without moving.
        SCROLL,         /// - OnScroll (float delta) is sent out when the mouse scroll wheel is moved.
        KEY,            /// - OnKey (KeyCode key) is sent when keyboard or controller input is used.
        MAX,            //MAX
    }

    public GUI_INPUT_TYPE m_eType;  //输入事件类型
    public float m_fDelta;          //数值细节
    public Vector2 m_vecDelta;      //坐标细节
    public bool m_bDone;            //是否执行
    public GameObject m_cTarget;    //目标物体
    public string m_strText;        //字符串内容
    public KeyCode m_eKey;          //按键
}
