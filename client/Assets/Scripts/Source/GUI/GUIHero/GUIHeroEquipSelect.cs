//Micro.Sanvey
//2013.11.28
//sanvey.china@gmail.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;
using Game.Base;

/// <summary>
/// 装备英雄选择界面
/// </summary>
public class GUIHeroEquipSelect : GUIHeroSelectBase
{
    private const string TITLE = "英雄选择";

    public Dictionary<int, int> m_lstOldEquip = new Dictionary<int, int>();

    //private const string BTN_NULL = "GUI_BtnNull";
    //private const string SP_NULL = "SP_Null";
    //private UISprite m_cBtnNull;
    //public bool m_bShowNull;  //是否显示移除

    public GUIHeroEquipSelect(GUIManager mgr)
        : base(mgr, GUI_DEFINE.GUIID_HEROEQUIPSELECT, UILAYER.GUI_PANEL)
    {
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        this.m_eLoadingState = LOADING_STATE.START;
        GUI_FUNCTION.AYSNCLOADING_SHOW();
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + BTN_NULL);
        if (this.m_cGUIObject == null)
        {
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_HEROITEM);
        }
    }



    /// <summary>
    /// 展示
    /// </summary>
    protected override void InitGUI()
    {
        //为置空按钮预留空间
        this.m_iShowOffsetX = m_bShowNull ? 1 : 0;
        //设置要显示的英雄列表
        this.m_lstHero = Role.role.GetHeroProperty().GetAllHero();

        if (this.m_cGUIObject == null)
        {
            base.InitGUI();

            this.m_cUITittle.text = TITLE;

            GUIComponentEvent ce = this.m_cCancelBtn.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnCancel);
        }
        else
        {
            base.InitGUI();
        }


        if (m_bShowNull)
        {
            GUIComponentEvent btnCe = m_cBtnNull.gameObject.AddComponent<GUIComponentEvent>();
            btnCe.AddIntputDelegate(OnBtnNull);
        }

        //设置点击英雄接口
        foreach (HeroShowItem item in this.m_lstHeroShow)
        {
            GUIComponentEvent tmp = item.m_cItem.GetComponent<GUIComponentEvent>();
            if (tmp == null)
                tmp = item.m_cItem.AddComponent<GUIComponentEvent>();
            tmp.AddIntputDelegate(OnHero, item.m_cHero.m_iID);
        }

        //设置整体GUI点击GUIID
        this.m_cGUIMgr.SetCurGUIID(this.ID);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_SELECT_EQUIP_HERO));
    }

    public override void Hiden()
    {
        //m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HEROEQUIPMENT).HidenImmediately();
        Destory();
        base.Hiden();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        //资源加载等待
        switch (this.m_eLoadingState)
        {
            case LOADING_STATE.START:
                this.m_eLoadingState++;
                return false;
            case LOADING_STATE.LOADING:
                if (ResourceMgr.GetProgress() >= 1f && ResourceMgr.IsComplete())
                {
                    this.m_eLoadingState++;
                }
                return false;
            case LOADING_STATE.END:
                InitGUI();
                this.m_eLoadingState++;
                break;
        }

        if (this.IsShow())
        {
            if (m_bHasDrag)  //如果是拖动
            {
                return true;
            }
            if (this.m_bHasPress)  //如果是按下,开始计时
            {
                float dis = GAME_TIME.TIME_FIXED() - m_fDis;
                if (dis >= PRESS_TIME)
                {
                    m_bBeLongPress = true;
                    m_bHasPress = false;

             
                    int heroid = this.m_iSelectHeroIndex;
                    GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;

                    herodetail.m_cLocalposition = this.m_cClipPanel.transform.localPosition;
                    herodetail.m_cClipRange = this.m_cClipPanel.clipRange;

                    this.Hiden();

                    herodetail.Show(this.ShowNotChange, Role.role.GetHeroProperty().GetHero(heroid));
             
                }
            }
        }
        return base.Update();
    }

    /// <summary>
    /// 进入英雄图鉴时回调方法
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void ShowNotChange()
    {
        GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;
        this.m_cPanelClipRange = herodetail.m_cClipRange;
        this.m_cPanelLocalposition = herodetail.m_cLocalposition;

        Show();
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
            GUI_FUNCTION.LOCKPANEL_AUTO_HIDEN();

            this.m_cPanelClipRange = Vector4.zero;
            this.m_cPanelLocalposition = Vector3.zero;

            this.Hiden();

            GUIHeroEquipment tmp = m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HEROEQUIPMENT) as GUIHeroEquipment;
            tmp.m_lstOldEquips = this.m_lstOldEquip;
            tmp.Show();

        }
    }

    /// <summary>
    /// 点击英雄
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnHero(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS)
        {
            if (!this.m_bHasPress)
            {
                this.m_bHasPress = true;
                //开始长按钮计时
                this.m_fDis = GAME_TIME.TIME_FIXED();
                this.m_iSelectHeroIndex = (int)args[0];
            }
            else
            {
                //弹起
                this.m_bHasPress = false;
                this.m_bHasDrag = false;
            }
        }
        else if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.DRAG)
        {
            this.m_bHasDrag = true;
        }
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (m_bBeLongPress)
            {
                return;
            }


            this.Hiden();

            GUIHeroEquipment tmp = m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HEROEQUIPMENT) as GUIHeroEquipment;
            tmp.m_lstOldEquips = this.m_lstOldEquip;
            int heroid = (int)args[0];
            tmp.SetSelectHeroId(heroid);

            tmp.Show();
        }
    }

    /// <summary>
    /// 移除
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnBtnNull(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.Hiden();

            GUIHeroEquipment tmp = m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HEROEQUIPMENT) as GUIHeroEquipment;
            tmp.SetSelectHeroId(-1);
            tmp.m_lstOldEquips = this.m_lstOldEquip;
            tmp.Show();
        }
    }

}