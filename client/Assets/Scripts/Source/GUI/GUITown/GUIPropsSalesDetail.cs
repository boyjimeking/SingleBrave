using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Resource;
using UnityEngine;

//道具售卖-详细
//Author:Sunyi
//2013-12-5

public class GUIPropsSalesDetail
{
    public const string RES_MAIN = "GUI_PropsSalesDetail";//物品售卖详细资源地址
    private const string SLIDE = "CenterItem/Slider";//进度条地址
    private const string LABEL_SELECTCOUNT = "CenterItem/Lab_Count";//最大所有数标签地址
    private const string LABEL_MAXCOUNT = "CenterItem/Lab_MaxCount";//最大所有数标签地址
    private const string LB_MAXCOUNTTIP = "CenterItem/Text_MaxCount";
    private const string LB_ItemPrice = "CenterItem/Lab_Price";
    private const string LABEL_PRICE = "BottomItem/Lab_Count";//总价格标签地址
    private const string LB_ITEMDESC = "TopItem/Lab_Desc";  //物品描述
    private const string LB_ITEMNAME = "TopItem/Lab_Name";  //物品名称
    private const string SP_ITEMBORDER = "TopItem/Spr_IconFrame";   //物品边框
    private const string SP_ITEMPATH = "TopItem/Spri_Icon";  //物品图标
    private const string BTN_SELL = "BottomItem/Btn_Sale";

    public GameObject m_cMain;
    public UISlider m_cSlide;//进度条
    public UILabel m_cLbSelectCount;
    public UILabel m_cLbMaxCount; //最大所有数标签
    public UILabel m_cLbMaxCountTip; //最大所有数标签
    public UILabel m_cLbSellPrice;//总价格
    public UILabel m_cLbItemPrice; //单价
    public UILabel m_cLbDesc;
    public UILabel m_cLbName;
    public UISprite m_cSpItemBorder;
    public UISprite m_cSpItemPath;
    public GameObject m_cBtnSell;

    public Item m_cProp;
    public Action m_cSlideCallBack;
    public int m_iMaxItemNum;
    public int m_iSelectItemNum = 0;

    public GUIPropsSalesDetail(GameObject parent, Item itemm)
    {
        m_cProp = itemm;
        int showNum = Role.role.GetItemProperty().GetItemCountByTableId(itemm.m_iTableID);
        Item tmpp = Role.role.GetItemProperty().GetBattleItemByTableID(m_cProp.m_iTableID);
        if (tmpp != null)
        {
            showNum -= tmpp.m_iNum;
        }        
        m_iMaxItemNum = showNum;

        this.m_cMain = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_MAIN)) as GameObject;
        this.m_cMain.transform.parent = parent.transform;
        this.m_cMain.transform.localScale = Vector3.one;
        this.m_cMain.transform.localPosition = Vector3.zero;

        this.m_cLbSelectCount = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cMain, LABEL_SELECTCOUNT);

        this.m_cLbMaxCount = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cMain, LABEL_MAXCOUNT);

        this.m_cLbMaxCountTip = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cMain, LB_MAXCOUNTTIP);

        this.m_cLbSellPrice = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cMain, LABEL_PRICE);

        this.m_cLbItemPrice = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cMain, LB_ItemPrice);

        this.m_cLbDesc = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cMain, LB_ITEMDESC);

        this.m_cLbName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cMain, LB_ITEMNAME);

        this.m_cSpItemBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cMain, SP_ITEMBORDER);

        this.m_cSpItemPath = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cMain, SP_ITEMPATH);

        this.m_cBtnSell = GUI_FINDATION.GET_GAME_OBJECT(this.m_cMain, BTN_SELL);
        this.m_cBtnSell.AddComponent<GUIComponentEvent>().AddIntputDelegate(ItemSell_OnEvent);

        this.m_cSlide = GUI_FINDATION.GET_OBJ_COMPONENT<UISlider>(this.m_cMain, SLIDE);

        if (m_cProp != null)
        {
            ItemTable table = ItemTableManager.GetInstance().GetItem(m_cProp.m_iTableID);

            this.m_cLbDesc.text = table.SellWarning;
            this.m_cLbName.text = m_cProp.m_strName;

            GUI_FUNCTION.SET_ITEMM(this.m_cSpItemPath, m_cProp.m_strSprName);
            GUI_FUNCTION.SET_ITEM_BORDER(this.m_cSpItemBorder, m_cProp.m_eType);

            if (itemm.m_eType == ITEM_TYPE.EQUIP)
                this.m_cLbMaxCountTip.text = "1个";  
            else
                this.m_cLbMaxCountTip.text = m_iMaxItemNum.ToString() + "个";
            this.m_cLbMaxCount.text = m_iMaxItemNum.ToString();
            this.m_cLbItemPrice.text = ItemTableManager.GetInstance().GetItem(m_cProp.m_iTableID).Price.ToString();

            EventDelegate.Add(this.m_cSlide.onChange, delegate()
            {
                if (m_cSlide.value == 0 || itemm.m_eType == ITEM_TYPE.EQUIP)
                {
                    m_iSelectItemNum = 1;

                }
                else
                {
                    m_iSelectItemNum = (int)Math.Ceiling(m_cSlide.value * m_iMaxItemNum);
                }

                this.m_cLbSelectCount.text = m_iSelectItemNum.ToString(); ;
                this.m_cLbSellPrice.text = (m_iSelectItemNum * ItemTableManager.GetInstance().GetItem(m_cProp.m_iTableID).Price).ToString();

            });
            if (m_iMaxItemNum == 1 || itemm.m_eType == ITEM_TYPE.EQUIP)
                this.m_cSlide.value = 1;
            else
                this.m_cSlide.value = 0;
        }
    }

    /// <summary>
    /// 物品出售
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    public void ItemSell_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (m_cProp != null)
            {
                GUI_FUNCTION.MESSAGEL_(MessageResult, "确认出售？");

            }
        }
    }

    /// <summary>
    /// 弹出框结果
    /// </summary>
    /// <param name="ok"></param>
    private void MessageResult(bool ok)
    {
        if (ok)
        {
            //发送出售数据
            SendAgent.SendItemSellReq(
                Role.role.GetBaseProperty().m_iPlayerId,
                m_cProp.m_iID,
                m_iSelectItemNum);
        }
        else
        {
        }
    }

    public void Destroy()
    {
        m_cMain = null;
        m_cSlide = null;//进度条
        m_cLbSelectCount = null;
        m_cLbMaxCount = null; //最大所有数标签
        m_cLbMaxCountTip = null; //最大所有数标签
        m_cLbSellPrice = null;//总价格
        m_cLbItemPrice = null; //单价
        m_cLbDesc = null;
        m_cLbName = null;
        m_cSpItemBorder = null;
        m_cSpItemPath = null;
        m_cBtnSell = null;
    }
}