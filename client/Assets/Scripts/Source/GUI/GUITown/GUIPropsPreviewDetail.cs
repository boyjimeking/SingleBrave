using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;

//道具一览-详细
//Author:Sunyi
//2013-12-5

public class GUIPropsPreviewDetail
{
    public const string RES_MAIN = "GUI_PropsPreviewDetail";//物品预览详细资源地址

    private const string CONTENT = "AlertView/Content";//消息面板地址
    private const string LAB_DESC = "Lab_Desc";  //用途
    private const string LAB_DETAIL = "Lab_Detail"; //详细介绍
    private const string LAB_NAME = "Lab_Name";  //全称
    private const string SP_ITEM = "Spr_Icon";  //物品图标
    private const string SP_ITEM_BORDER = "Spr_IconFrame"; //物品边框

    public GameObject m_cMain;
    private GameObject m_cContent;  //消息面板
    public UILabel m_cLbDesc;
    public UILabel m_cLbDetail;
    public UILabel m_cLbName;
    public UISprite m_cSpItem;
    public UISprite m_cSpItemBorder;
    public Item m_cProp;

    public GUIPropsPreviewDetail(GameObject parent, Item item)
    {
        this.m_cProp = item;

        this.m_cMain = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_MAIN)) as GameObject;
        this.m_cMain.transform.parent = parent.transform;
        this.m_cMain.transform.localScale = Vector3.one;
        this.m_cMain.transform.localPosition = Vector3.zero;

        this.m_cContent = GUI_FINDATION.GET_GAME_OBJECT(m_cMain, CONTENT);

        this.m_cLbDesc = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cContent, LAB_DESC);

        this.m_cLbDetail = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cContent, LAB_DETAIL);

        this.m_cLbName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cContent, LAB_NAME);

        this.m_cSpItem = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cContent, SP_ITEM);

        this.m_cSpItemBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cContent, SP_ITEM_BORDER);

        this.m_cLbDesc.text = item.m_strDesc;
        this.m_cLbDetail.text = item.m_strDetail;
        this.m_cLbName.text = item.m_strName;
        GUI_FUNCTION.SET_ITEMM(this.m_cSpItem, item.m_strSprName);
        GUI_FUNCTION.SET_ITEM_BORDER(this.m_cSpItemBorder, item.m_eType);

    }

    public void Destroy()
    {
        m_cMain = null;
        m_cContent = null;  //消息面板
        m_cLbDesc = null;
        m_cLbDetail = null;
        m_cLbName = null;
        m_cSpItem = null;
        m_cSpItemBorder = null;
        m_cProp = null;
    }
}