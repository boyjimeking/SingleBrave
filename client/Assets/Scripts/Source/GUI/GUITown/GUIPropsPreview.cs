using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;

//战斗道具选择
//sanvey
//2013-12-5

/// <summary>
/// 战斗道具选择
/// </summary>
public class GUIPropsPreview : GUIItemSelectBase
{
    private GUIPropsPreviewDetail m_cItemDetail;

    public GUIPropsPreview(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_PROPSPREVIEW, GUILAYER.GUI_PANEL)
    { }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        this.m_eLoadingState = LOADING_STATE.NONE;

        if (this.m_cGUIObject == null)
        {
            this.m_eLoadingState = LOADING_STATE.START;
            GUI_FUNCTION.AYSNCLOADING_SHOW();
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_PROPSITEM);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, GUIPropsPreviewDetail.RES_MAIN);
        }
        else
        {
            InitGUI();
        }
    }

    public override bool Update()
    {
        //资源加载等待
        switch (this.m_eLoadingState)
        {
            case LOADING_STATE.START:
                this.m_eLoadingState++;
                return false;
            case LOADING_STATE.LOADING:
                if (ResourcesManager.GetInstance().GetProgress() >= 1f && ResourcesManager.GetInstance().IsComplete())
                {
                    this.m_eLoadingState++;
                }
                return false;
            case LOADING_STATE.END:
                InitGUI();
                this.m_eLoadingState++;
                break;
        }
        return base.Update();
    }

    protected override void InitGUI()
    {

        this.m_lstItems = Role.role.GetItemProperty().GetAllItem();
        this.m_lstItems.ForEach(q => { q.m_iDummyNum = q.m_iNum; });

        if (this.m_cGUIObject == null)
        {
            //设置要显示的英雄列表
            this.m_iShowOffsetX = 0;

            //生成画面
            base.InitGUI();
            //设置页面头
            this.m_cUITittle.text = "道具一览";

            //设置返回输入接口
            GUIComponentEvent ce = this.m_cBtnBack.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnCancel);

            this.m_cBack.gameObject.AddComponent<GUIComponentEvent>().AddIntputDelegate(BackClick);
        }
        else
        {
            base.InitGUI();
        }
        //设置背景遮罩
        this.m_cBack.enabled = true;
        this.m_cBack.gameObject.SetActive(false);

        //设置点击英雄接口
        for (int i = 0; i < this.m_lstItemShow.Count; i++)
        {
            ItemShowItem item = m_lstItemShow[i];
            GUIComponentEvent tmp = item.m_cRes.GetComponent<GUIComponentEvent>();
            if (tmp == null)
                tmp = item.m_cRes.AddComponent<GUIComponentEvent>();
            tmp.AddIntputDelegate(ItemSelect_OnEvent, i);
        }

        if (m_cItemDetail != null)
        {
            GameObject.DestroyImmediate(m_cItemDetail.m_cMain);
            m_cItemDetail = null;
        }

        //设置整体GUI点击GUIID
        GUITown town = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TOWN) as GUITown;
        town.SetTownChildId(this.ID);
        town.SetTownBlack(false);
        GUIBackFrameTop backtop = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
        backtop.Show();
        //this.m_cGUIMgr.SetCurGUIID(this.ID);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_SCANE_ITEM));

    }

    /// <summary>
    /// 取消按钮
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnCancel(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (m_cItemDetail != null)
            {
                this.m_cBack.gameObject.SetActive(false);
                GameObject.DestroyImmediate(m_cItemDetail.m_cMain);
                m_cItemDetail = null;
                return;
            }

            this.Hiden();
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSWAREHOUSE).Show();
        }
    }

    /// <summary>
    /// 道具英雄
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void ItemSelect_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            int itemIndex = (int)args[0];
            Item tmp = m_lstItems[itemIndex];

            this.m_cBack.gameObject.SetActive(true);

            if (m_cItemDetail != null)
            {
                GameObject.DestroyImmediate(m_cItemDetail.m_cMain);
                m_cItemDetail = null;
            }

            m_cItemDetail = new GUIPropsPreviewDetail(this.m_cMainPanel, tmp);
        }
    }

    public override void Hiden()
    {
        base.Hiden();
        ResourcesManager.GetInstance().UnloadUnusedResources();
        Destory();
    }

    public override void Destory()
    {
        if (m_cItemDetail != null)
        {
            m_cItemDetail.Destroy();
        }
        m_cItemDetail = null;

        base.Destory();
    }

    /// <summary>
    /// 背景遮罩点击
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void BackClick(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_cBack.gameObject.SetActive(false);

            if (m_cItemDetail != null)
            {
                GameObject.DestroyImmediate(m_cItemDetail.m_cMain);
                m_cItemDetail = null;
            }
        }
    }

}