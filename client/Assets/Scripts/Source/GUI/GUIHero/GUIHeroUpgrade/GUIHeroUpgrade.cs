//  GUIHeroUpgrade.cs
//  Author: Sanvey
//  2013-12-20

using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using Game.Resource;
using Game.Gfx;
using Game.Base;

/// <summary>
/// 英雄升级面板
/// </summary>
class GUIHeroUpgrade : GUIBase
{
    //----------------------------------------------资源地址--------------------------------------------------

    private const string RES_MAIN = "GUI_HeroUpgrade"; //主资源

    private const string TEAM_POS = "Team/Pos_";                       //队伍位置
    private const string HERO_ATTRIBUTE = "Attribute";                //属性面板
    private const string SELF_POS = "Team/Pos_Self/Attribute";    //自己属性面板
    private const string POS_SELF = "Team/Pos_Self";                   //自己位置节点

    private const string LAB_EXP = "Bottom/ExpValue";                            //经验地址
    private const string LAB_SPEND = "Bottom/SpendValue";                  //花费地址
    private const string LAB_LV = "Bottom/LvValue";                               //等级地址
    private const string LAB_FROM = "Bottom/LvFrom";                          //等级地址
    private const string LAB_TO = "Bottom/LvTo";                                   //等级地址
    private const string SP_ARR = "Bottom/Arr";                                      //箭头
    private const string SLIDER_UP = "Bottom/Up/UpBar";                      //升级经验条地址
    private const string SLIDER_AFTERUP = "Bottom/Up/UpAfterBar";     //升级经验后的地址
    private const string BTN_COMPOUND = "Bottom/BtnCompound";   //人物强化按钮地址
    private const string BTN_CLOSE = "Title/BtnBack";                            //关闭按钮地址
    private const string BTN_ATTRIBUTESHOW = "BtnAttributeShow";    //属性显示按钮地址
    private const string BTN_CHANAGE = "BtnChanage";                        //人物改变按钮

    private const string HERO_MAIN = "HERO_LEVEL_UP";    //3D模型主面板地址
    private const string HERO_BACKGROUND = "BACK_Base";  //3D模型主面板背景
    private const string HERO_BACKGROUND_BACK = "BACK_Back";
    private const string RES_HERO_UPGRADE = "GUI_HERO_UPGRADE";  //3d资源
    private const string HERO_RESULT = "Result";                  //3D模型结果地址
    private const string HERO_Panel = "Team";                       //3D模型队伍面板地址
    private const string HERO_POS = "Pos_";                         //3D模型的位置地址
    private const string HERO_SELF = "UpHeroPos";              //3D模型自己的位置地址

    //属性面板
    private class HeroAttributes
    {
        private GameObject m_attributes;
        private const string OBJ_Attributes = "Attribute";
        private UILabel m_labLv;
        private const string LAB_LV = "LvValue";
        private UILabel m_labHp;
        private const string LAB_HP = "HpValue";
        private UILabel m_labCost;
        private const string LAB_COST = "CostValue";
        private UILabel m_labAttack;
        private const string LAB_ATTACK = "AttackValue";
        private UILabel m_labDefense;
        private const string LAB_DEFENSE = "DefenseValue";
        private UILabel m_labRever;
        private const string LAB_REVER = "RevertValue";
        private UISprite m_spProperty;
        private const string LAB_Property = "Property";

        //属性设定//
        public void SetAttributes(GameObject obj, Hero hero)
        {
            m_labLv = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(obj, LAB_LV);
            m_labLv.text = hero.m_iLevel.ToString();
            m_labHp = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(obj, LAB_HP);
            m_labHp.text = hero.GetMaxHP().ToString();
            m_labCost = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(obj, LAB_COST);
            m_labCost.text = hero.m_iCost.ToString();
            m_labAttack = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(obj, LAB_ATTACK);
            m_labAttack.text = hero.GetAttack().ToString();
            m_labDefense = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(obj, LAB_DEFENSE);
            m_labDefense.text = hero.GetDefence().ToString();
            m_labRever = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(obj, LAB_REVER);
            m_labRever.text = hero.GetRecover().ToString();
            m_spProperty = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(obj, LAB_Property);
            GUI_FUNCTION.SET_NATURES(m_spProperty, hero.m_eNature);
        }

        //属性面板展现//
        public void ShowAttributes(GameObject obj, bool isShow)
        {
            m_attributes = GUI_FINDATION.GET_GAME_OBJECT(obj, OBJ_Attributes);
            if (isShow)
            {
                m_attributes.SetActive(true);
            }
            else
            {
                m_attributes.SetActive(false);
            }
        }
    }

    //----------------------------------------------游戏对象--------------------------------------------------

    private GameObject m_cBtnBack;  //返回按钮//
    private GameObject m_cBtnAttributeShow; //属性展示按钮//
    private GameObject m_cBtnChanage;   //换英雄按钮//
    private GameObject m_cBtnCompound;  //强化按钮//

    private UILabel m_labExp;   //合成获得经验//
    private UILabel m_labGold;  //合成所需金币//
    private UILabel m_labLv;    //离一下等级所需经验//
    private UISprite m_sliderUp;    //经验条//
    private UISprite m_sliderAfterUp;   //升级后经验条//
    private UILabel m_cLbLvFrom;  //等级提升原始
    private UILabel m_cLbLvTo;  //等级提升最终
    private UISprite m_cSpArr;  //等级提升箭头

    private GameObject m_cHeroMain;     //3D模型主面板/
    private GameObject m_cHeroResult;   //3D模型结果面板//
    private GameObject m_cHeroPanel;    //3D模型队伍主面板//
    private GameObject m_cHeroSelf;     //3D模型自己位置//
    private GameObject m_cTeamPos;  //3D模型其他人属性//
    private GameObject m_cSelfPos;  //3D自己属性位置//
    private GameObject m_cBack;
    private GameObject m_cBase;
    private GameObject m_cHeroUpgrade; //3d资源

    private GfxObject m_gfxSelf;      //自己实例//
    public List<GfxObject> m_gfxObjHeros;    //升级经验英雄实例//
    private List<GameObject> m_cAttribteList;    //所有属性//
    private List<GameObject> m_cHeroPosList;    //所有英雄点//

    //----------------------------------------------data--------------------------------------------------

    public int m_iSelfID;   //强化英雄ID//
    public List<int> m_lstSelectID=new List<int>(); //选择的ID//

    public bool m_bIsClear; //是否清除//

    private bool m_bIsAttributeShow; //英雄属性显示标志

    private PressState m_pressState; //按下时候状态//
    public Hero m_iSelectHero;   //点击的英雄//
    private float m_fPressTime;    //点击时间//
    private float m_fReleaseTime;   //释放时间//

    enum LOAD_STATE
    {
        START = 0,  //开始
        LOAD = 1,   //加载过程
        END = 2,  //加载结束
        OUT = 3     //不再加载
    }

    private LOAD_STATE m_eState;    //状态

    public GUIHeroUpgrade(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_UPGRADEHERO, UILAYER.GUI_PANEL)
    {
    }

    public override void Show()
    {
        this.m_eLoadingState = LOADING_STATE.START;
        GUI_FUNCTION.AYSNCLOADING_SHOW();

        Hero self = Role.role.GetHeroProperty().GetHero(m_iSelfID);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_MODEL_PATH + self.m_strModel);
		ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_HERO_UPGRADE);
        

        if (m_lstSelectID != null)
        {
            for (int i = 0; i < m_lstSelectID.Count; i++)
            {
                Hero hero = Role.role.GetHeroProperty().GetHero(m_lstSelectID[i]);
				ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_MODEL_PATH + hero.m_strModel);
            }
        }


        if (this.m_cGUIObject == null)
        {
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
        }
    }

    public override void Hiden()
    {
        ResourceMgr.UnloadUnusedResources();

        foreach (GfxObject gfx in m_gfxObjHeros)
        {
            gfx.Destory();
        }

        if (m_gfxSelf != null)
        {
            m_gfxSelf.Destory();
        }

        base.Hiden();
        CameraManager.GetInstance().HidenUIHeroUpgradeCamera();
        m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
        //SetLocalPos(Vector3.one * 0xFFFF);
        Destory();
    }

    public override void Destory()
    {
        m_cBase = null;
        m_cBack = null;

        if (m_cHeroUpgrade != null)
        {
            GameObject.DestroyImmediate(m_cHeroUpgrade);
        }
        m_cHeroUpgrade = null;

        m_cBtnBack = null;  //返回按钮//
        m_cBtnAttributeShow = null; //属性展示按钮//
        m_cBtnChanage = null;   //换英雄按钮//
        m_cBtnCompound = null;  //强化按钮//
        m_labExp = null;   //合成获得经验//
        m_labGold = null;  //合成所需金币//
        m_labLv = null;    //离一下等级所需经验//
        m_sliderUp = null;    //经验条//
        m_sliderAfterUp = null;   //升级后经验条//
        m_cLbLvFrom = null;  //等级提升原始
        m_cLbLvTo = null;  //等级提升最终
        m_cSpArr = null;  //等级提升箭头
        m_cHeroMain = null;     //3D模型主面板/
        m_cHeroResult = null;   //3D模型结果面板//
        m_cHeroPanel = null;    //3D模型队伍主面板//
        m_cHeroSelf = null;     //3D模型自己位置//
        m_cTeamPos = null;  //3D模型其他人属性//
        m_cSelfPos = null;  //3D自己属性位置//
        m_cBack = null;
        m_cBase = null;
        m_gfxSelf = null;      //自己实例//

        if (null != m_gfxObjHeros) m_gfxObjHeros.Clear();    //升级经验英雄实例//
        if (null != m_cAttribteList) m_cAttribteList.Clear();    //所有属性//
        if (null != m_cHeroPosList) m_cHeroPosList.Clear();    //所有英雄点//

        base.Hiden();
        base.Destory();
    }

    //刷新//
    private void Reflash()
    {
        //被强化英雄
        Hero self = Role.role.GetHeroProperty().GetHero(m_iSelfID);
        //属性显示框
        HeroAttributes haSelf = new HeroAttributes();
        haSelf.SetAttributes(m_cSelfPos, self);
        //被英雄模型
        if (m_gfxSelf != null)
        {
            m_gfxSelf.Destory();
        }
        GameObject objSelf = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(self.m_strModel)) as GameObject;
        objSelf.transform.parent = m_cHeroSelf.transform;
        objSelf.transform.localPosition = Vector3.zero;
        objSelf.transform.localScale = Vector3.one;
        objSelf.name = self.m_strModel;
        m_gfxSelf = new GfxObject(objSelf);
        //选中被吃掉的英雄素材模型
        foreach (GfxObject gfxObj in m_gfxObjHeros)
        {
            gfxObj.Destory();
        }
        m_gfxObjHeros.Clear();


        if (m_lstSelectID!=null&&m_lstSelectID.Count > 0)
        {
            for (int i = 0; i < m_lstSelectID.Count; i++)
            {
                Hero hero = Role.role.GetHeroProperty().GetHero(m_lstSelectID[i]);

                HeroAttributes ha = new HeroAttributes();
                ha.SetAttributes(m_cAttribteList[i], hero);

                GameObject objHero = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(hero.m_strModel)) as GameObject;
                objHero.transform.parent = m_cHeroPosList[i].transform;
                objHero.transform.localPosition = Vector3.zero;
                objHero.transform.localScale = Vector3.one;
                objHero.name = hero.m_strModel;
                m_gfxObjHeros.Add(new GfxObject(objHero));
            }
        }
        //刷新属性面板显示
        if (m_bIsAttributeShow)
        {
            ShowAttribute();
        }
        //金币经验计算
        int curLv = self.m_iLevel;  //当前等级
        int CurMaxExp = HeroEXPTableManager.GetInstance().GetMaxExp(self.m_iExpType, self.m_iLevel);
        int CurMinExp = HeroEXPTableManager.GetInstance().GetMinExp(self.m_iExpType, self.m_iLevel);
        int CurDistanceMax = CurMaxExp - CurMinExp;  //经验差
        int CurDistance=CurDistanceMax-self.m_iCurrenExp;  //我距离升级的差值
        float pCurExp = (float)self.m_iCurrenExp / (float)CurDistanceMax; //当前经验和差值比例
        float pNextExp = (float)(self.m_iCurrenExp + GetAllSelectedExps()) / (float)CurDistanceMax;  //强化后和差值比例
        if (pNextExp>1)
            pNextExp = 1;

        int NextLevel = HeroEXPTableManager.GetInstance().GetLv(self.m_iExpType, CurMinExp + self.m_iCurrenExp + GetAllSelectedExps());
        if (NextLevel > self.m_iMaxLevel)
        {
            NextLevel = self.m_iMaxLevel;
        }

        if (NextLevel == self.m_iLevel)
        {
            this.m_cLbLvTo.enabled = false;
            this.m_cSpArr.enabled = false;
        }
        else
        {
            this.m_cLbLvTo.enabled = true;
            this.m_cSpArr.enabled = true;
            this.m_cLbLvTo.text = NextLevel.ToString();
        }

        //当前等级显示
        this.m_cLbLvFrom.text = curLv.ToString();
        //距离下一级升级需要经验
        if (self.m_iLevel == self.m_iMaxLevel)
        {
            m_labLv.text = "---";
        }
        else
        {
            m_labLv.text = CurDistance.ToString();
        }
        //当前经验条
        if (self.m_iLevel == self.m_iMaxLevel)
        { pCurExp = 1; }
        m_sliderUp.fillAmount = pCurExp;
        //强化后经验条
        m_sliderAfterUp.fillAmount = pNextExp;
        //获得经验显示
        m_labExp.text = GetAllSelectedExps().ToString();
        //消耗金币显示
        if (Role.role.GetBaseProperty().m_iGold < GetAllSelectedGolds())
            m_labGold.text = "[FF0000]金币不够";
        else
            m_labGold.text = GetAllSelectedGolds().ToString();


    }

    //刷新属性面板//
    private void ShowAttribute()
    {

        for (int i = 0; i < m_cAttribteList.Count; i++)
        {
            if (i < m_lstSelectID.Count)
            {
                m_cAttribteList[i].SetActive(m_bIsAttributeShow);
            }
            else
            {
                m_cAttribteList[i].SetActive(false);
            }
        }
    }

    //返回//
    private void OnBack(GUI_INPUT_INFO info, object[] arg)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();
            m_lstSelectID.Clear();
            if (false)
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UPGRADE).Show);
            }
            else
            {
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UPGRADE).Show();
            }
        }
    }

    //点击自己英雄//
    private void OnSelf(GUI_INPUT_INFO info, object[] args)
    {

        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS)
        {
            m_iSelectHero = Role.role.GetHeroProperty().GetHero(m_iSelfID);
            if (m_pressState == PressState.Normal)
            {
                m_pressState = PressState.Press;
                m_fPressTime = Time.fixedTime;
            }
            else if (m_pressState == PressState.Press)
            {
                m_fReleaseTime = Time.fixedTime;
                m_pressState = PressState.Normal;
            }
            else if (m_pressState == PressState.Release)
            {
                m_pressState = PressState.Normal;
            }
        }
    }

    //英雄位置点击事件//
    private void OnHero(GUI_INPUT_INFO info, object[] args)
    {

        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS)
        {
            int m_iSelectHeroPos = (int)args[0];

            if (m_iSelectHeroPos < m_lstSelectID.Count)
            {
                m_iSelectHero = Role.role.GetHeroProperty().GetHero(m_lstSelectID[m_iSelectHeroPos]);
            }
            else
            {
                m_iSelectHero = null;
            }
            if (m_pressState == PressState.Normal)
            {
                m_pressState = PressState.Press;
                m_fPressTime = Time.fixedTime;
            }
            else if (m_pressState == PressState.Press)
            {
                m_fReleaseTime = Time.fixedTime;

                if ((m_fPressTime - m_fReleaseTime) < 0.1f)
                {
                    Hiden();
                    GUIHeroUpgradeSelect upgradeHeroSelect = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UPGRADEHEROSELECT) as GUIHeroUpgradeSelect;
                    upgradeHeroSelect.SetShowData(m_iSelfID, m_lstSelectID);
                    upgradeHeroSelect.Show();
                    m_fReleaseTime = 0;
                }

                m_pressState = PressState.Normal;
            }
            else if (m_pressState == PressState.Release)
            {
                m_pressState = PressState.Normal;
            }

        }
    }

    //属性面板显示事件//
    private void OnAttributeShow(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            m_bIsAttributeShow = !m_bIsAttributeShow;

            if (!m_bIsAttributeShow)  //属性显示 
            {
                UIImageButton btn = this.m_cBtnAttributeShow.GetComponent<UIImageButton>();
                btn.normalSprite = "btn_Pshow2";
                btn.hoverSprite = "btn_Pshow";
                btn.pressedSprite = "btn_Pshow";
                btn.disabledSprite = "btn_Pshow";
            }
            else  //属性关闭
            {
                UIImageButton btn = this.m_cBtnAttributeShow.GetComponent<UIImageButton>();
                btn.normalSprite = "btn_Pclose";
                btn.hoverSprite = "btn_Pclose2";
                btn.pressedSprite = "btn_Pclose2";
                btn.disabledSprite = "btn_Pclose2";
            }

            ShowAttribute();
        }
    }

    //英雄交换事件//
    private void OnChanage(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();
            m_bIsClear = false;
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UPGRADE).Show();
        }
    }

    /// <summary>
    /// 合成
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnCompound(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (m_lstSelectID.Count <= 0)
            {
                GUI_FUNCTION.MESSAGEM(null, "请选择素材!");
                return;
            }

            if (GetAllSelectedGolds() > Role.role.GetBaseProperty().m_iGold)
            {
                GUI_FUNCTION.MESSAGEM(null, "金币不够!");
                return;
            }

            if (JudgePropmt())
            {
                GUI_FUNCTION.MESSAGEM_(SendMessage, "有3级以上素材，是否确定要强化");
            }
            else
            {
                SendUp();
            }


        }
    }

    //查看详细信息//
    private void ShowHeroDetail(Hero hero)
    {
        GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;


        if (hero != null)
        {
            herodetail.Show(Show, hero);

            Hiden();
        }
    }

    //判断是否跳出框//
    private bool JudgePropmt()
    {
        foreach (int i in m_lstSelectID)
        {
            Hero hero = Role.role.GetHeroProperty().GetHero(i);

            if (hero.m_iStarLevel >= 3)
            {
                return true;
            }
        }

        return false;
    }

    //发送消息弹出确定//
    private void SendMessage(bool isSend)
    {
        if (isSend)
        {
            SendUp();
        }
    }

    //发送消息//
    private void SendUp()
    {
        m_cBase.SetActive(false);
        m_cBack.SetActive(true);

        Hiden();

        SendAgent.SendHeroUpgrade(Role.role.GetBaseProperty().m_iPlayerId, m_iSelfID, m_lstSelectID);

        //if (SessionManager.GetInstance().Refresh())
        //{
        //    SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UPGRADEHERORESULT).Show);
        //}
        //else
        //{
        //    this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_UPGRADEHERORESULT).Show();
        //}
    }

    //获取得到的经验值
    private int GetAllSelectedExps()
    {
        int exp = 0;

        Hero selfHero = Role.role.GetHeroProperty().GetHero(m_iSelfID);
        if (m_lstSelectID != null)
        {
            foreach (int heroID in m_lstSelectID)
            {
                Hero selectHero = Role.role.GetHeroProperty().GetHero(heroID);

                //被吃掉英雄常规计算
               int allexp = HeroEXPTableManager.GetInstance().GetMinExp(selectHero.m_iExpType, selectHero.m_iLevel);
               int tmpExp = selectHero.m_iCombineExp + (int)((allexp + selectHero.m_iCurrenExp) * 0.4f);
                //属性相同，经验在加成
                if (selfHero.m_eNature == selectHero.m_eNature)
                {
                    tmpExp = (int)(tmpExp * 1.5f);
                }

                exp += tmpExp;  //累加
            }
        }

        return exp;
    }

    //获取消费的金币
    private int GetAllSelectedGolds()
    {
        int gold = 0;

        Hero selfHero = Role.role.GetHeroProperty().GetHero(m_iSelfID);

        if (m_lstSelectID != null)
        {
            foreach (int heroID in m_lstSelectID)
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
        }

        return gold;
    }

    protected override void InitGUI()
    {
        base.Show();

        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_CLICK_SQUARE));

        CameraManager.GetInstance().ShowUIHeroUpgradeCamera();
        if (m_cGUIObject == null)
        {
            m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            m_cGUIObject.transform.localScale = Vector3.one;

            m_cHeroMain = GUI_FINDATION.FIND_GAME_OBJECT(HERO_MAIN);
            this.m_cHeroUpgrade = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_HERO_UPGRADE)) as GameObject;
            this.m_cHeroUpgrade.transform.parent = this.m_cHeroMain.transform;
            this.m_cHeroUpgrade.transform.localScale = Vector3.one;
            this.m_cHeroUpgrade.transform.localPosition = Vector3.zero;
            m_cHeroResult = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroUpgrade, HERO_RESULT);
            m_cHeroPanel = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroUpgrade, HERO_Panel);
            m_cHeroSelf = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroPanel, HERO_SELF);
            m_cBack = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroUpgrade, HERO_BACKGROUND_BACK);
            m_cBase = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroUpgrade, HERO_BACKGROUND);

            m_cSelfPos = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, SELF_POS);

            //m_cTeamPos = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, TEAM_POS);
            m_cAttribteList = new List<GameObject>();
            m_cHeroPosList = new List<GameObject>();
            m_gfxObjHeros = new List<GfxObject>();

            m_labExp = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cGUIObject, LAB_EXP);
            m_labGold = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cGUIObject, LAB_SPEND);
            m_labLv = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cGUIObject, LAB_LV);

            m_cLbLvFrom = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LAB_FROM);
            m_cLbLvTo = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LAB_TO);
            m_cSpArr = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SP_ARR);

            m_sliderUp = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cGUIObject, SLIDER_UP);
            m_sliderAfterUp = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(m_cGUIObject, SLIDER_AFTERUP);

            m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, BTN_CLOSE);
            GUIComponentEvent ceLeader = m_cBtnBack.AddComponent<GUIComponentEvent>();
            ceLeader.AddIntputDelegate(OnBack);

            m_cBtnAttributeShow = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, BTN_ATTRIBUTESHOW);
            GUIComponentEvent ceAttributeShow = m_cBtnAttributeShow.AddComponent<GUIComponentEvent>();
            ceAttributeShow.AddIntputDelegate(OnAttributeShow);

            m_cBtnChanage = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, BTN_CHANAGE);
            GUIComponentEvent ceChanage = m_cBtnChanage.AddComponent<GUIComponentEvent>();
            ceChanage.AddIntputDelegate(OnChanage);

            m_cBtnCompound = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, BTN_COMPOUND);
            GUIComponentEvent ceCompound = m_cBtnCompound.AddComponent<GUIComponentEvent>();
            ceCompound.AddIntputDelegate(OnCompound);

            GameObject m_cSelf = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, POS_SELF);
            GUIComponentEvent ceSelf = m_cSelf.AddComponent<GUIComponentEvent>();
            ceSelf.AddIntputDelegate(OnSelf);

            //m_lstSelectID = new List<int>();

            m_bIsAttributeShow = false;
            for (int i = 0; i < 5; i++)
            {
                GameObject uiPos = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, TEAM_POS + (i + 1));
                GameObject uiAttribte = GUI_FINDATION.GET_GAME_OBJECT(uiPos, HERO_ATTRIBUTE);
                m_cAttribteList.Add(uiAttribte);

                GameObject heroPos = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroPanel, HERO_POS + (i + 1));
                m_cHeroPosList.Add(heroPos);
                //添加人物事件//
                GUIComponentEvent cePos = uiPos.AddComponent<GUIComponentEvent>();
                cePos.AddIntputDelegate(OnHero, i);
            }
        }

        m_cBase.SetActive(true);
        m_cBack.SetActive(false);

        m_cHeroResult.SetActive(false);
        m_cHeroPanel.SetActive(true);

        m_bIsClear = true;
        Reflash();
        m_pressState = PressState.Normal;

        m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Hiden();
        this.m_cGUIMgr.SetCurGUIID(this.ID);
        SetLocalPos(Vector3.zero);

        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_HERO_UP3);
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_HERO_UP5);
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_HERO_UP7);
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

        if (!IsShow())
        {
            return false;
        }

        switch (this.m_eState)
        {
            case LOAD_STATE.START:
                break;
            case LOAD_STATE.LOAD:
                int per = (int)(ResourceMgr.GetAsyncProcess() * 100);

                if (per >= 100)
                {
                    GUI_FUNCTION.AYSNCLOADING_HIDEN();
                    this.m_eState++;
                }
                break;
            case LOAD_STATE.END:
                m_eState++;
                break;
            case LOAD_STATE.OUT:
                if ((Time.time - m_fPressTime) > 1 && (m_pressState == PressState.Press) && (m_iSelectHero != null))
                {
                    m_pressState = PressState.Release;
                    ShowHeroDetail(m_iSelectHero);
                }
                break;
        }

        return true;
    }

    // 设置本界面需要数据
    public void SetUpgradeData(int selfId, List<int> selectIds)
    {
        this.m_iSelfID = selfId;
        this.m_lstSelectID = selectIds;
    }

    // 设置本界面需要数据
    public void SetUpgradeData(int selfId)
    {
        this.m_iSelfID = selfId;
    }
}