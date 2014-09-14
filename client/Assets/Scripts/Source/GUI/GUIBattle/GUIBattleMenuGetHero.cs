using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Resource;
using UnityEngine;

//战斗菜单-获得英雄
//Author sunyi
//2014-3-26
public class GUIBattleMenuGetHero : GUIBase
{
    private const string RES_MAIN = "GUI_BattleMenuGetHero";//主资源地址
    private const string RES_HEROITEM = "GUI_BattleMenuGetHeroItem";//英雄资源地址

    private const string HEROITEM_PARENT = "GetHero/ItemAll";//获取英雄item父对象地址
    private const string HEROITEM_UITABLE = "GetHero/ItemAll/UITable";//获取英雄item父对象列表地址
    private const string HEROITEMNONE_PARENT = "GetHero/None";//获取英雄为0时显示的面板地址

    private const string ITEM_BG = "ItemBg";//item中英雄属性背景地址
    private const string ITEM_FRAME = "ItemFrame";//item中英雄属性框地址
    private const string ITEM_MONSTER = "ItemMonster";//item中英雄头像地址
    private const string ITEM_LV = "LabelBottom";//item中等级标签地址


    private UnityEngine.Object m_cGetHeroItem;//获取的英雄列表item
    private GameObject m_cGetHeroNone;//没有获取任何英雄时显示的面板
    private GameObject m_cHeroItemParent;//获取英雄item父对象

    private List<int> m_lstSoul;//获得的英雄表id

    private bool m_bIsFirstGetHero;//是否第一次展示

    public GUIBattleMenuGetHero(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_BATTLE_MENU_GETHERO, GUILAYER.GUI_PANEL3)
    { 
        //
    }

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
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
			ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_HEROITEM);
        }
        else
        {
            InitGUI();
        }
    }

    /// <summary>
    /// 创建GUI
    /// </summary>
    protected override void InitGUI()
    {
        base.Show();

        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        if (this.m_cGUIObject == null)
        {
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cHeroItemParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, HEROITEM_PARENT);
            this.m_cGetHeroNone = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, HEROITEMNONE_PARENT);
            this.m_cGetHeroItem = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_HEROITEM);
        }

        ShowGetHero();

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        ResourceMgr.UnloadResource(RES_MAIN);
        ResourceMgr.UnloadResource(RES_HEROITEM);
        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cHeroItemParent = null;
        this.m_cGetHeroNone = null;
        this.m_cGetHeroItem = null;

        base.Destory();
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

        return base.Update();
    }

    /// <summary>
    /// 展示获得英雄面板
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void ShowGetHero()
    {
        UITable table = GUI_FINDATION.GET_OBJ_COMPONENT<UITable>(this.m_cGUIObject, HEROITEM_PARENT);
        if (this.m_lstSoul != null)
        {
            if (this.m_lstSoul.Count > 0)
            {
                this.m_cGetHeroNone.SetActive(false);
                this.m_cHeroItemParent.SetActive(true);
                if (this.m_bIsFirstGetHero)
                {
                    foreach (Transform tsf in this.m_cHeroItemParent.transform)
                    {
                        GameObject.Destroy(tsf.gameObject);
                    }
                    for (int i = 0; i < this.m_lstSoul.Count; i++)
                    {
                        GameObject getHeroItem = GameObject.Instantiate(this.m_cGetHeroItem) as GameObject;
                        getHeroItem.transform.parent = this.m_cHeroItemParent.transform;
                        getHeroItem.transform.localScale = Vector3.one;
                        this.m_bIsFirstGetHero = false;
                        table.repositionNow = true;
                    }
                }
            }
            else
            {
                this.m_cGetHeroNone.SetActive(true);
                this.m_cHeroItemParent.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 获得英雄
    /// </summary>
    /// <param name="lst"></param>
    public void GetHero(List<int> lst)
    {
        this.m_lstSoul = lst;
    }

    public void SetIsFirstGetHero(bool isFirstGetHero)
    {
        this.m_bIsFirstGetHero = isFirstGetHero;
    }
}

