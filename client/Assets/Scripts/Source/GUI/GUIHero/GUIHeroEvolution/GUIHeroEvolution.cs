//  GUIHeroEvolution.cs
//  Author: Cheng Xia
//  2013-12-30

using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using Game.Resource;
using Game.Gfx;
using Game.Base;

/// <summary>
/// 进化主界面
/// </summary>
class GUIHeroEvolution : GUIBase
{
    /// <summary>
    /// RES开头为地址节点  HERO开头为3D界面 
    /// </summary>
    private const string RES_MAIN = "GUI_HeroEvolution";    //英雄进化界面地址//
    private const string RES_BOTTOM = "Bottom"; //界面底部
    private const string RES_BEFORE = "Before"; //进化前节点地址//
    private const string RES_AFTER = "After";   //进化后节点地址//
    private const string RES_EVOLUTION = "Evolution";
    private const string RES_CANEVOLUTION = "CanEvolution";
    private const string RES_HERO_JINHUA = "GUI_HERO_JINHUA";
    private const string HERO_EVOLUTION = "HERO_JINHUA"; //3D面板//
    private const string HERO_BACKGROUND = "BACK_Base";  //3D模型主面板背景
    private const string HERO_BACKGROUND_BACK = "BACK_Back";

    private const string HERO_AFTER = "After";  //3D进化后面板地址//
    private const string HERO_BEFORE = "Before";    //3D进化前面板地址//
    private const string HERO_BEFOREPOS = "BeforePos";  //3D进化前地址//
    private const string HERO_ENDPOS = "EndPos";    //3D进化后地址//

    private const string EVOLUTION_TEAM = "Item_";  //进化素材面板地址//
    private const string TEAM_HIDENHEAD = "HidenHead";  //隐藏地址//
    private const string TEAM_ITEMBORDER = "ItemBorder";    //英雄框地址//
    private const string TEAM_ITEMMONSTER = "ItemMonster";  //英雄头像显示地址//
    private const string TEAM_ITEMFRAME = "ItemFrame";  //框背景地址//
    private const string TEAM_NUM = "Num";  //英雄数量地址//
    private const string TEAM_NONE = "None";

    private const string ARROW = "Arrow";
    
    //属性面板地址//
    private const string LAB_ATTACK = "LabAttack";  //攻击地址//
    private const string LAB_COST = "LabCost";  //cost地址//
    private const string LAB_DEFENSE = "LabDefense";    //防御地址//
    private const string LAB_HP = "LabHp";  //血量地址//
    private const string LAB_LVMAX = "LabLvMax";    //最大等级地址//
    private const string LAB_LVMIN = "LabLvMin";    //最低等级地址//
    private const string LAB_REVERT = "LabRevert";  //回复力地址//
    private const string LAB_DETAIL = "LabDetail";  //详细地址//
    private const string LAB_GOLD = "LabGold";      //需要的金币//
    private const string LAB_TITLE = "Title/Label";

    private const string BTN_EVOLUTION = "BtnEvolution";    //进化按钮
    private const string BTN_CLOSE = "Title/BtnBack"; //关闭按钮地址

    /// <summary>
    /// 3D部分
    /// </summary>
    private GameObject m_cHeroPlane;
    private GameObject m_cHeroAfter;
    private GameObject m_cHeroBefore;
    private GameObject m_cHeroBeforePos;
    private GameObject m_cHeroEndPos;
    private GameObject m_cHeroJinHua;

    /// <summary>
    /// UI部分主节点
    /// </summary>
    private GameObject m_cBottom;
    private GameObject m_cBefore;
    private GameObject m_cAfter;
    private GameObject m_cEvolution;
    private GameObject m_cCanEvolution;

    private UILabel m_labTitle; //标题文本//

    /// <summary>
    /// 文本
    /// </summary>
    private UILabel m_labBeforeAttack;  
    private UILabel m_labBeforeCost;
    private UILabel m_labBeforeDefense;
    private UILabel m_labBeforeHp;
    private UILabel m_labBeforeLvMax;
    private UILabel m_labBeforeLvMin;
    private UILabel m_labBeforeRevert;

    private UILabel m_labAfterAttack;
    private UILabel m_labAfterCost;
    private UILabel m_labAftereDefense;
    private UILabel m_labAfterHp;
    private UILabel m_labAfterLvMax;
    private UILabel m_labAfterLvMin;
    private UILabel m_labAfterRevert;

    private UILabel m_labDetail;
    private UILabel m_labGold;

    private GameObject m_cBack;
    private GameObject m_cBase;

    private GameObject m_cBtnBack;  //返回按钮
    private GameObject m_cBtnEvolution; //进化按钮//

    private List<GameObject> m_lstItems;    //进化素材显示列表//

    public int m_selectID; //选择进化的英雄ID
    public int m_selectTableID; //旧英雄tableID
    private GfxObject m_gfxSelf;    //英雄实例
    private GfxObject m_gfxNext;    //下一等级英雄实例//

    public List<int> m_lstEvolutionItems;  //进化材料的ID//
    public List<Hero> m_lstHeros;   //进化英雄//

    private bool isEvolution;
    private string m_strEvolutionPrompt;

    private UISprite m_cSprArrowRight;          //向右滑动特效
    private TDAnimation m_cEffectRight;        //特效类

    enum LOAD_STATE
    {
        START = 0,  //开始
        LOAD = 1,   //加载过程
        END = 2,  //加载结束
        OUT = 3     //不再加载
    }

    private LOAD_STATE m_eState;    //状态

    public GUIHeroEvolution(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_EVOLUTIONHERO, UILAYER.GUI_PANEL)
    {
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {

        this.m_eLoadingState = LOADING_STATE.START;
        GUI_FUNCTION.AYSNCLOADING_SHOW();

        Hero selectHero = Role.role.GetHeroProperty().GetHero(m_selectID);
        HeroTable htNext = HeroTableManager.GetInstance().GetHeroTable(selectHero.m_iEvolutionID);
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_HERO_JINHUA);
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_MODEL_PATH + selectHero.m_strModel);
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_MODEL_PATH + htNext.Modle);
        //ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_TEX_PATH, "");

    }

    /// <summary>
    /// 展示界面初始
    /// </summary>
    private void Init()
    {
        //Debug.Log("pid_"+Role.role.GetBaseProperty().m_iPlayerId);
        isEvolution = true;
        string prompt = "目前可以进化";
        //进化前数值
        Hero selectHero = Role.role.GetHeroProperty().GetHero(m_selectID);
        m_labBeforeLvMin.text = selectHero.m_iLevel.ToString();
        m_labBeforeLvMax.text = "/" + selectHero.m_iMaxLevel.ToString();
        m_labBeforeHp.text = selectHero.GetMaxHP().ToString();
        m_labBeforeAttack.text = selectHero.GetAttack().ToString();
        m_labBeforeDefense.text = selectHero.GetDefence().ToString();
        m_labBeforeRevert.text = selectHero.GetRecover().ToString();
        m_labBeforeCost.text = selectHero.m_iCost.ToString();


        float spentGold;
        if (GAME_DEFINE.m_vecEvolutionHeroID.Contains(selectHero.m_iTableID))
        {
            spentGold = GAME_DEFINE.m_iEvolutionSpent[selectHero.m_iStarLevel - 1];
        }
        else
        {
            spentGold = GAME_DEFINE.m_iOtherEvolutionSpent[selectHero.m_iStarLevel - 1];
        }

        m_labGold.text = spentGold.ToString();
        if (spentGold > Role.role.GetBaseProperty().m_iGold)
        {
            m_strEvolutionPrompt = "金币不足！";
            prompt = "[ff0000]金币不足";
            isEvolution = false;
            m_labGold.color = Color.red;
        }
        else
            m_labGold.color = Color.white;

        m_cCanEvolution.SetActive(true);

        if (m_gfxSelf != null)
        {
            m_gfxSelf.Destory();
        }

        GameObject objSelf = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(selectHero.m_strModel)) as GameObject;
        objSelf.transform.parent = m_cHeroBeforePos.transform;
        objSelf.transform.localPosition = new Vector3(0, 0, 0);
        objSelf.transform.localScale = Vector3.one;
        objSelf.name = selectHero.m_strModel;
        m_gfxSelf = new GfxObject(objSelf);
        //Debug.Log("table_" + selectHero.m_iTableID);
        //Debug.Log("spid_" + selectHero.m_iSpIndex);
        HeroTable htNext = HeroTableManager.GetInstance().GetHeroTable(selectHero.m_iEvolutionID);
        m_labAfterCost.text = htNext.Cost.ToString();
        if (m_gfxNext != null)
        {
            m_gfxNext.Destory();
        }

		HeroBook heroBook = new HeroBook();
        //进化后数值  如果我曾经拥有过该进化后的英雄，则显示该进化后英雄1级的基础属性
		if ( heroBook.HadHero(selectHero.m_iEvolutionID))
        {
            if (htNext != null)
            {
                List<int> prop = HeroGrowTableManager.GetInstance().GetHeroByType(htNext.ID, GrowType.Balance);
                m_labAfterHp.text = prop[0].ToString();   //默认1级平衡型基础数据
                m_labAfterAttack.text = prop[1].ToString();
                m_labAftereDefense.text = prop[2].ToString();
                m_labAfterRevert.text = prop[3].ToString();

                m_labAfterCost.text = htNext.Cost.ToString();
                m_labAfterLvMax.text = "/" + htNext.MaxLevel.ToString();
                m_labAfterLvMin.text = "1";
            }
        }

        GameObject objNext = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(htNext.Modle)) as GameObject;
        objNext.transform.parent = m_cHeroEndPos.transform;
        objNext.transform.localPosition = new Vector3(0, 0, 0);
        objNext.transform.localScale = Vector3.one;
        objNext.name = htNext.Modle;


        //获得过的英雄 ， 模型全部展示
		if (!heroBook.HadHero(selectHero.m_iEvolutionID))
        {
            foreach (Transform child in objNext.transform)
            {
                if (child.renderer != null && child.renderer.material != null)
                {
                    if (child.renderer.material.HasProperty("_Color"))
                    {
                        child.renderer.material.SetColor("_Color", Color.black);
                    }
                }
            }

        }

        m_gfxNext = new GfxObject(objNext);

        //所有英雄列表
        List<Hero> allheros = Role.role.GetHeroProperty().GetAllHero();
        //去除 有装备的，被强化的，在队伍中的，锁定的。
		List<Hero> partyHeros = allheros.FindAll(new Predicate<Hero>((item) => { return (item.m_iEquipID == -1) && !(CheckExistInTeams(HeroTeam.ToArray(), item.m_iID)) && (!item.m_bLock); }));
        this.m_lstHeros = partyHeros;

        int[] heroNums = new int[selectHero.m_vecEvolution.Length];  //用于下标显示的英雄数据
        for (int i = 0; i < selectHero.m_vecEvolution.Length; i++)
        {
            if (selectHero.m_vecEvolution[i] != 0)
            {
                List<Hero> all = m_lstHeros.FindAll((q) => { return q.m_iTableID == selectHero.m_vecEvolution[i]; });
                if (all == null)
                {
                    heroNums[i] = 0;
                }
                else
                {
                    heroNums[i] = all.Count;
                }
            }
            else
            {
                heroNums[i] = 0;
            }
        }

        bool canEvo = true;  //素材是否足够
        for (int i = 0; i < selectHero.m_vecEvolution.Length; i++)
        {
            if (selectHero.m_vecEvolution[i] != 0)
            {
                m_lstItems[i].SetActive(true);

                GameObject hidenHead = GUI_FINDATION.GET_GAME_OBJECT(m_lstItems[i], TEAM_HIDENHEAD);
                UISprite itemBorder = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_lstItems[i], TEAM_ITEMBORDER);
                UISprite itemFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_lstItems[i], TEAM_ITEMFRAME);
                UISprite itemMonster = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_lstItems[i], TEAM_ITEMMONSTER);
                UILabel num = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_lstItems[i], TEAM_NUM);
                GameObject none = GUI_FINDATION.GET_GAME_OBJECT(m_lstItems[i], TEAM_NONE);

                hidenHead.SetActive(true);
                HeroTable table = HeroTableManager.GetInstance().GetHeroTable(selectHero.m_vecEvolution[i]);
                GUI_FUNCTION.SET_AVATORS(itemMonster, table.AvatorMRes);
                GUI_FUNCTION.SET_HeroBorderAndBack(itemBorder, itemFrame, (Nature)table.Property);

                // 进化素材 显示     121 |  8 |  8 |  8 | 3  如果8只有2  那么 前两个 8 显示 高亮可用  最后一个 8 变灰
                int deleIndex = this.m_lstHeros.FindIndex(q => { return q.m_iTableID == selectHero.m_vecEvolution[i]; });
                if (deleIndex >= 0)
                {
                    m_lstEvolutionItems.Add(m_lstHeros[deleIndex].m_iID);
                    this.m_lstHeros.RemoveAt(deleIndex);
                    hidenHead.SetActive(false);
                }
                else
                {
                    canEvo = false;
                    hidenHead.SetActive(true);
                }

                num.text = heroNums[i].ToString() + "持有";

				if (heroBook.HadHero(selectHero.m_vecEvolution[i]))
                {
                    none.SetActive(false);
                }
                else
                {
                    none.SetActive(false);
                    //none.SetActive(true);
                }
            }
            else
            {
                m_lstItems[i].SetActive(false);
            }
        }

        if (canEvo)
        {
            m_cCanEvolution.SetActive(true);
        }
        else
        {
            m_strEvolutionPrompt = "素材不足！";
            m_cCanEvolution.SetActive(false);
            prompt = "[ff0000]素材不足";
            isEvolution = false;
        }

        if (selectHero.m_iLevel != selectHero.m_iMaxLevel)
        {
            m_strEvolutionPrompt = "等级不够！";
            prompt = "[ff0000]等级不够";
            m_cCanEvolution.SetActive(false);
            isEvolution = false;
            m_labBeforeLvMin.color = Color.red;
        }
        else
            m_labBeforeLvMin.color = Color.white;
                
        m_labDetail.text = prompt;
    }

    public override void Hiden()
    {
        if (m_gfxSelf != null)
        {
            m_gfxSelf.Destory();
        }
        if (m_gfxNext != null)
        {
            m_gfxNext.Destory();
        }
        base.Hiden();
        CameraManager.GetInstance().HidenUIHeroEvolutionCamera();
        m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
        //SetLocalPos(Vector3.one * 0xFFFF);
        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        if (m_cHeroJinHua!=null)
        {
            GameObject.DestroyImmediate(m_cHeroJinHua);
        }
        m_cHeroJinHua = null;

        m_cHeroPlane = null;
        m_cHeroAfter = null;
        m_cHeroBefore = null;
        m_cHeroBeforePos = null;
        m_cHeroEndPos = null;
        m_cBottom = null;
        m_cBefore = null;
        m_cAfter = null;
        m_cEvolution = null;
        m_cCanEvolution = null;
        m_labTitle = null; //标题文本//
        m_labBeforeAttack = null;
        m_labBeforeCost = null;
        m_labBeforeDefense = null;
        m_labBeforeHp = null;
        m_labBeforeLvMax = null;
        m_labBeforeLvMin = null;
        m_labBeforeRevert = null;
        m_labAfterAttack = null;
        m_labAfterCost = null;
        m_labAftereDefense = null;
        m_labAfterHp = null;
        m_labAfterLvMax = null;
        m_labAfterLvMin = null;
        m_labAfterRevert = null;
        m_labDetail = null;
        m_labGold = null;
        m_cBack = null;
        m_cBase = null;
        m_cBtnBack = null;  //返回按钮
        m_cBtnEvolution = null; //进化按钮//
        if (null != m_lstItems) m_lstItems.Clear();    //进化素材显示列表//

        m_cSprArrowRight = null;          //向右滑动特效
        m_cEffectRight = null;        //特效类


        base.Hiden();
        base.Destory();
    }

    //返回//
    private void OnBack(GUI_INPUT_INFO info, object[] arg)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_EVOLUTION).Show();
        }
    }

    //点击进化按钮事件//
    private void OnEvolution(GUI_INPUT_INFO info, object[] arg)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {

            if (isEvolution)
            {
                m_cBase.SetActive(false);
                m_cBack.SetActive(true);

                SendAgent.SendHeroEvolution(Role.role.GetBaseProperty().m_iPlayerId, m_selectID);

                //if (SessionManager.GetInstance().Refresh())
                //{
                //    SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_EVOLUTIONRESULT).Show);
                //}
                //else
                //{
                //    this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_EVOLUTIONRESULT).Show();
                //}
            }
            else
            {
                GUI_FUNCTION.MESSAGEM(null, m_strEvolutionPrompt);
            }
        }
    }

    protected override void InitGUI()
    {

        base.Show();

        GUI_FUNCTION.AYSNCLOADING_HIDEN();
        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_ENHANCE_HERO));
        CameraManager.GetInstance().ShowUIHeroEvolutionCamera();
        if (m_cGUIObject == null)
        {
            m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            m_cGUIObject.transform.localScale = Vector3.one;

            m_cBottom = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, RES_BOTTOM);
            m_cBefore = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, RES_BEFORE);
            m_cAfter = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, RES_AFTER);
            m_cCanEvolution = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, RES_CANEVOLUTION);
            m_cEvolution = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, RES_EVOLUTION);
            m_labTitle = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cGUIObject, LAB_TITLE);
            m_labTitle.text = "英雄进化";

            m_labBeforeAttack = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cBefore, LAB_ATTACK);
            m_labBeforeCost = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cBefore, LAB_COST);
            m_labBeforeDefense = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cBefore, LAB_DEFENSE);
            m_labBeforeHp = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cBefore, LAB_HP);
            m_labBeforeLvMax = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cBefore, LAB_LVMAX);
            m_labBeforeLvMin = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cBefore, LAB_LVMIN);
            m_labBeforeRevert = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cBefore, LAB_REVERT);

            m_labAfterAttack = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cAfter, LAB_ATTACK);
            m_labAfterCost = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cAfter, LAB_COST);
            m_labAftereDefense = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cAfter, LAB_DEFENSE);
            m_labAfterHp = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cAfter, LAB_HP);
            m_labAfterLvMax = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cAfter, LAB_LVMAX);
            m_labAfterLvMin = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cAfter, LAB_LVMIN);
            m_labAfterRevert = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cAfter, LAB_REVERT);

            m_labDetail = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cBottom, LAB_DETAIL);
            m_labGold = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cBottom, LAB_GOLD);

            m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, BTN_CLOSE);
            GUIComponentEvent ceClose = m_cBtnBack.AddComponent<GUIComponentEvent>();
            ceClose.AddIntputDelegate(OnBack);

            m_cBottom = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, RES_BOTTOM);
            m_cBtnEvolution = GUI_FINDATION.GET_GAME_OBJECT(m_cBottom, BTN_EVOLUTION);
            GUIComponentEvent ceEvolution = m_cBtnEvolution.AddComponent<GUIComponentEvent>();
            ceEvolution.AddIntputDelegate(OnEvolution);

            m_lstItems = new List<GameObject>();


            for (int i = 1; i <= 5; i++)
            {
                GameObject item = GUI_FINDATION.GET_GAME_OBJECT(m_cEvolution, EVOLUTION_TEAM + i);
                m_lstItems.Add(item);
            }

            m_cHeroPlane = GUI_FINDATION.FIND_GAME_OBJECT(HERO_EVOLUTION);
            m_cHeroJinHua = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_HERO_JINHUA)) as GameObject;
            m_cHeroJinHua.transform.parent = m_cHeroPlane.transform;
            m_cHeroJinHua.transform.localPosition = Vector3.zero;
            m_cHeroJinHua.transform.localScale = Vector3.one;
            m_cHeroAfter = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroJinHua, HERO_AFTER);
            m_cHeroBefore = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroJinHua, HERO_BEFORE);
            m_cHeroBeforePos = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroBefore, HERO_BEFOREPOS);
            m_cHeroEndPos = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroBefore, HERO_ENDPOS);

            m_cSprArrowRight = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, ARROW);

            m_cBack = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroJinHua, HERO_BACKGROUND_BACK);
            m_cBase = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroJinHua, HERO_BACKGROUND);
        }

        m_cBase.SetActive(true);
        m_cBack.SetActive(false);

        this.m_cEffectRight = new TDAnimation(m_cSprArrowRight.atlas, m_cSprArrowRight);
        this.m_cEffectRight.Play("Arrow", Game.Base.TDAnimationMode.Loop, 0.5F);

        m_cHeroAfter.SetActive(false);
        m_cHeroBefore.SetActive(true);
        m_lstEvolutionItems = new List<int>();
        Init();

        m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Hiden();
        this.m_cGUIMgr.SetCurGUIID(this.ID);
        SetLocalPos(Vector3.zero);

    }

    //update 刷新
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
            if (this.m_cEffectRight != null)
            {
                this.m_cEffectRight.Update();
            }
        }
        return true;
    }

    /// <summary>
    /// 判断所有队伍列表中存在该英雄ID与否
    /// </summary>
    /// <param name="heroTeam"></param>
    /// <param name="heroId"></param>
    /// <returns></returns>
    private bool CheckExistInTeams(HeroTeam[] heroTeam, int heroId)
    {
        return heroTeam.Any<HeroTeam>(new Func<HeroTeam, bool>((item) =>
        {
            return item.m_vecTeam.Contains(heroId);
        }));
    }
}
