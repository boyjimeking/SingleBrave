//  GUIHeroUpgradeSelect.cs
//  Author: Cheng Xia
//  2013-12-23

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;
using Game.Base;

/// <summary>
/// 英雄升级素材选择
/// </summary>
public class GUIHeroUpgradeSelect : GUIHeroSelectBase
{
    //----------------------------------------------资源地址--------------------------------------------------

    private const string RES_HEROSELECT = "GUI_HeroUpgradeSelect";  //英雄升级选择主界面地址
    private const string BTN_OK = "BtnOk";                   //选择确定按钮
    private const string BTN_REPEAT = "BtnRepeat";      //重新选择按钮
    private const string LAB_EXP = "ExpValue";              //经验显示地址
    private const string LAB_GOLD = "SpendValue";      //花费显示地址

    //----------------------------------------------游戏对象--------------------------------------------------

    private GameObject m_cUpgradeHeroSelect;
    private GameObject m_cBtnOk;                                 //确定按钮
    private GameObject m_cBtnRepeat;                           //重新选择按钮
    private UILabel m_labExp;                                          //经验文本
    private UILabel m_labGold;                                        //金币文本

    //----------------------------------------------data--------------------------------------------------

    //英雄图标选中状态维护数据
    private List<HeroShowItem> m_lstSelectHero;                      //选择的
    private int m_iSelectMax = 5;                                                //最大选择数//

    //该界面需要其他节目传值的数据
    private int m_iBeUpgradeHeroID = 0;                                      //被选中需要强化的英雄
    private List<int> m_lstSelectedHeros = new List<int>();         //被选中英雄的ID

    public GUIHeroUpgradeSelect(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_UPGRADEHEROSELECT, GUILAYER.GUI_PANEL)
    {
        //
    }

    //展示
    public override void Show()
    {
        this.m_eLoadingState = LOADING_STATE.START;
        GUI_FUNCTION.AYSNCLOADING_SHOW();
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + BTN_NULL);
        if (this.m_cGUIObject == null)
        {
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_HEROITEM);
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_HEROSELECT);
        }
    }

    protected override void InitGUI()
    {

        //优化每次进入都读取最新所有英雄，否则，如果界面没有被销毁，进入界面选择英雄是老数据
        //所有英雄列表
        List<Hero> allheros = Role.role.GetHeroProperty().GetAllHero();
        //去除 有装备的，被强化的，在队伍中的，锁定的。
        List<Hero> partyHeros = allheros.FindAll(new Predicate<Hero>((item) => { return (item.m_iEquipID == -1) && item.m_iID != m_iBeUpgradeHeroID && !(CheckExistInTeams(Role.role.GetTeamProperty().GetAllTeam(), item.m_iID)) && (!item.m_bLock); }));
        this.m_lstHero = partyHeros;

        //设置主资源中panel位置大小调整
        this.m_fClipParentY = 18f;
        this.m_fClipCenterY = -28f;
        this.m_fClipSizeY = 440f;

        if (this.m_cGUIObject == null)
        {
            //生成画面
            base.InitGUI();

            this.m_cUITittle.text = "素材选择";

            GUIComponentEvent ce = this.m_cCancelBtn.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnCancel);

            m_cUpgradeHeroSelect = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_HEROSELECT)) as GameObject;

            m_cBtnOk = GUI_FINDATION.GET_GAME_OBJECT(m_cUpgradeHeroSelect, BTN_OK);
            ce = m_cBtnOk.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnSure);

            m_cBtnRepeat = GUI_FINDATION.GET_GAME_OBJECT(m_cUpgradeHeroSelect, BTN_REPEAT);
            ce = m_cBtnRepeat.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnRepeat);

            m_labExp = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cUpgradeHeroSelect, LAB_EXP);
            m_labGold = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cUpgradeHeroSelect, LAB_GOLD);

            m_cUpgradeHeroSelect.transform.parent = this.m_cTopRightParent.transform;
            m_cUpgradeHeroSelect.transform.localPosition = new Vector3(0, 35f, 0);
            m_cUpgradeHeroSelect.transform.localScale = Vector3.one;
        }
        else
        {
            base.InitGUI();
        }

        m_lstSelectHero = new List<HeroShowItem>();
        //设置点击英雄接口
        foreach (HeroShowItem item in this.m_lstHeroShow)
        {
            GUIComponentEvent tmp = item.m_cItem.GetComponent<GUIComponentEvent>();
            if (tmp == null)
                tmp = item.m_cItem.AddComponent<GUIComponentEvent>();
            tmp.AddIntputDelegate(OnHero, item.m_cHero.m_iID);

            if (m_lstSelectedHeros.Contains(item.m_cHero.m_iID))
            {
                m_lstSelectHero.Add(item);
            }
        }

        UpdateSelectState();

        this.m_cGUIMgr.SetCurGUIID(this.ID);

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_SELECT_SOURCE));

        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_HERO_UP4);
    }

    //隐藏
    public override void Hiden()
    {
        ResourceMgr.UnloadUnusedResources();

        //SetLocalPos(Vector3.one * 0xFFFF);
        base.Hiden();
        CameraManager.GetInstance().HidenUIModelCamera();
        Destory();
    }

    public override void Destory()
    {
        m_cUpgradeHeroSelect = null;
        m_cBtnOk = null;                                 //确定按钮
        m_cBtnRepeat = null;                           //重新选择按钮
        m_labExp = null;                                          //经验文本
        m_labGold = null;                                        //金币文本

        base.Destory();
    }

    //逻辑更新
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
                //新手引导禁用长按功能
                if (Role.role.GetBaseProperty().m_iModelID <= 0)
                {
                    float dis = GAME_TIME.TIME_FIXED() - m_fDis;
                    if (dis >= PRESS_TIME)
                    {
                        m_bBeLongPress = true;

            
                        int heroid = this.m_iSelectHeroIndex;
                        GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;

                        herodetail.m_cLocalposition = this.m_cClipPanel.transform.localPosition;
                        herodetail.m_cClipRange = this.m_cClipPanel.clipRange;

                        this.Hiden();

                        herodetail.Show(this.ShowNotChange, Role.role.GetHeroProperty().GetHero(heroid));
                    
                    }
                } 
            }
        }
        return base.Update();
    }

    /// <summary>
    ///查看英雄信息回调
    /// </summary>
    private void ShowNotChange()
    {
        GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;
        this.m_cPanelClipRange = herodetail.m_cClipRange;
        this.m_cPanelLocalposition = herodetail.m_cLocalposition;

        this.Show();
    }

    //返回
    private void OnCancel(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_cPanelClipRange = Vector4.zero;
            this.m_cPanelLocalposition = Vector3.zero;
            Hiden();

            GUIHeroUpgrade tmp = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UPGRADEHERO) as GUIHeroUpgrade;
            tmp.SetUpgradeData(this.m_iBeUpgradeHeroID, this.m_lstSelectedHeros);
            tmp.Show();
        }
    }

    //英雄选中
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

            int heroID = (int)args[0];  //选中英雄ID
            HeroShowItem selectItem = this.m_lstHeroShow.Find(new Predicate<HeroShowItem>((item) =>
            {
                return item.m_cHero.m_iID == heroID;
            }));
            //已经选择的再次点击，变为非选中
            bool exist = m_lstSelectHero.Exists(new Predicate<HeroShowItem>((item) =>
            {
                return item.m_cHero.m_iID == selectItem.m_cHero.m_iID;
            }));
            if (exist)  //存在则移除选中
            {
                m_lstSelectHero.RemoveAll(new Predicate<HeroShowItem>((item) =>
                {
                    return item.m_cHero.m_iID == selectItem.m_cHero.m_iID;
                }));
                selectItem.m_cSelectBar.alpha = 0f;
                selectItem.m_cSelectLb.enabled = false;
            }
            else
            {
                //大于一次性最大出售数量，剩余项目变灰，不可点击
                if (m_lstSelectHero.Count >= m_iSelectMax)
                {
                    //刷新选中图标
                    UpdateSelectState();
                    return;
                }
                m_lstSelectHero.Add(selectItem);
            }

            //刷新选中图标
            UpdateSelectState();
        }
    }

    // 确定
    private void OnSure(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIHeroUpgrade tmp = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UPGRADEHERO) as GUIHeroUpgrade;
            tmp.SetUpgradeData(this.m_iBeUpgradeHeroID, this.m_lstSelectedHeros);
            tmp.Show();
        }
    }

    // 全部取消
    private void OnRepeat(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //将已有状态去除
            m_lstSelectHero.ForEach((item =>
            {
                item.m_cSelectBar.alpha = 0f;
                item.m_cSelectLb.enabled = false;
            }));
            m_lstSelectHero.Clear();
            m_lstSelectedHeros.Clear();
            //刷新选中图标
            UpdateSelectState();
        }
    }

    // 刷新选中图标
    private void UpdateSelectState()
    {
        //大于一次性最大出售数量，剩余项目变灰，不可点击
        for (int i = 0; i < m_lstHeroShow.Count; i++)
        {
            m_lstHeroShow[i].m_cItemCover.enabled = (m_lstSelectHero.Count >= m_iSelectMax);
        }

        m_lstSelectedHeros = new List<int>();  //清空选中英雄ID列表，进行重新计算
        for (int i = 0; i < m_lstSelectHero.Count; i++)
        {
            m_lstSelectedHeros.Add(m_lstSelectHero[i].m_cHero.m_iID);

            m_lstSelectHero[i].m_cSelectLb.text = (i + 1).ToString();

            m_lstSelectHero[i].m_cSelectBar.alpha = 1f;
            m_lstSelectHero[i].m_cSelectBar.gameObject.SetActive(false);
            m_lstSelectHero[i].m_cSelectBar.gameObject.SetActive(true);
            m_lstSelectHero[i].m_cSelectLb.enabled = true;
            m_lstSelectHero[i].m_cItemCover.enabled = false;

        }

        //刷新显示
        int heroGold = GetAllSelectedGolds();
        int heroExp = GetAllSelectedExps();

        m_labExp.text = heroExp.ToString();
        if (Role.role.GetBaseProperty().m_iGold < heroGold)
        {
            m_labGold.text = "[FF0000]金币不够";
        }
        else
        {
            m_labGold.text = heroGold.ToString();
        }
    }

    //获取得到的经验值
    private int GetAllSelectedExps()
    {
        int exp = 0;

        Hero selfHero = Role.role.GetHeroProperty().GetHero(m_iBeUpgradeHeroID);
        foreach (int heroID in m_lstSelectedHeros)
        {
            Hero selectHero = Role.role.GetHeroProperty().GetHero(heroID);

            //被吃掉英雄常规计算
            int allexp = HeroEXPTableManager.GetInstance().GetMinExp(selectHero.m_iExpType, selectHero.m_iLevel);
            int tmpExp = selectHero.m_iCombineExp + (int)((allexp + selectHero.m_iCurrenExp) * 0.4f);
            //属性相同，经验在加成
            if (selfHero.m_eNature == selectHero.m_eNature)
            {
                tmpExp= (int)(tmpExp * 1.5f);
            }

            exp += tmpExp;  //累加
        }

        return exp;
    }

    //获取消费的金币
    private int GetAllSelectedGolds()
    {
        int gold = 0;

        Hero selfHero = Role.role.GetHeroProperty().GetHero(m_iBeUpgradeHeroID);

        foreach (int heroID in m_lstSelectedHeros)
        {
            Hero selectHero = Role.role.GetHeroProperty().GetHero(heroID);

            if ((selfHero.m_iLevel == selfHero.m_iMaxLevel) && (selfHero.m_iLevel == 40))
            {
                gold += 1000 + selectHero.m_iLevel * 25;
            }
            else if ((selfHero.m_iLevel == selfHero.m_iMaxLevel) && (selfHero.m_iLevel == 60))
            {
                gold += 1500 + selectHero.m_iLevel * 25;
            }
            else if ((selfHero.m_iLevel == selfHero.m_iMaxLevel) && (selfHero.m_iLevel == 80))
            {
                gold += 2000 + selectHero.m_iLevel * 25;
            }
            else
            {
                gold += selfHero.m_iLevel * 100 + selectHero.m_iLevel * 100;
            }
        }

        return gold;
    }

    /// <summary>
    /// 设置本界面需要数据
    /// </summary>
    /// <param name="selfId"></param>
    /// <param name="selectIds"></param>
    public void SetShowData(int selfId, List<int> selectIds)
    {
        m_iBeUpgradeHeroID = selfId;      
        m_lstSelectedHeros = selectIds;
    }
}