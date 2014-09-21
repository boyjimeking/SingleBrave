//  GUITeamEditor.cs
//  Author: Cheng Xia
//  2013-12-16

using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Resource;
using Game.Gfx;
using Game.Base;
using Game.Media;

/// <summary>
/// 鼠标按下状态
/// </summary>
public enum PressState
{
    Normal,
    Press,
    Release
}

class GUITeamEditor : GUIBase
{
    public delegate void CALLBACK();    //回调委托
    private CALLBACK m_delCallBack; //回调方法

    private class HeroAttributes
    {
        private GameObject m_attributes;
        private const string OBJ_Attributes = "Attribute";
        private UILabel m_labLv;
        private const string LAB_LV = "Attribute/LvValue";
        private UILabel m_labHp;
        private const string LAB_HP = "Attribute/HpValue";
        private UILabel m_labCost;
        private const string LAB_COST = "Attribute/CostValue";
        private UILabel m_labAttack;
        private const string LAB_ATTACK = "Attribute/AttackValue";
        private UILabel m_labDefense;
        private const string LAB_DEFENSE = "Attribute/DefenseValue";
        private UILabel m_labRever;
        private const string LAB_REVER = "Attribute/RevertValue";
        private UISprite m_spProperty;
        private const string LAB_Property = "Attribute/Property";

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

    public enum TeamState
    {
        Main,
        Leader,
        Chanage
    }



    private const string RES_MAIN = "GUI_TeamEditor"; //主资源

    private const string TOP_PANEL = "Top"; //顶部面板
    private const string MAIN_PANEL = "Main"; //主面板    
    private const string TEAM_POS = "Main/team/group/pos";  //编队位置
    private const string OBJ_POS = "pos";   //实体站位
    private const string BTN_CLOSE = "Top/BtnBack"; //关闭按钮地址
    private const string BTN_CHANAGEBACK = "Top/BtnChanageBack";    //改变位置返回按钮//
    private const string BTN_LEADERBACK = "Top/BtnLeaderBack";  //改变队长返回按钮//
    private const string BTN_CHANAGE = "Main/BtnChanage";  //改变按钮//
    private const string BTN_LEADER = "Main/BtnLeader";   //队长按钮//
    private const string BACK_COLLER = "Main/team/group";    //背景碰撞体
    private const string HERO_MODEL = "Model";  //英雄名字//
    private const string HERO_ATTRIBUTE = "Attribute";

    private const string COST_SPEND = "Main/Cost/CostSpendValue";   //总体cost消费//
    private const string LEADER_ICON = "Leader";  //队长//
    private const string POINTER_ICON = "Pointer";  //指针//
    private const string SKILL_NAME = "Main/Bottom/Name";   //技能名字//
    private const string SKILL_EXPLAIN = "Main/Bottom/Explain";    //技能解释//

    private const string TEAM_NUMMBER = "Main/Bottom/Team"; //第几个队伍//
    private const string LIGHT_MARK = "Main/TeamMarks/LightMark";   //亮的标记//

    private const string BTN_LEFT = "BtnLeft";  //左边按钮地址//
    private const string BTN_RIGHT = "BtnRight";    //右边按钮地址//

    private const float OFFSET_DIS = 640; //偏移量

    private GameObject m_cTopPanel; //顶部面板
    private GameObject m_cMainPanel;    //主面板
    private GameObject m_cHeroPanel;    //英雄主界面//

    private GameObject m_cBtnBack;  //背景碰撞体
    private GameObject m_cBtnClose; //关闭按钮
    private GameObject m_cBtnChanageBack;   //换位置返回按钮//
    private GameObject m_cBtnLeaderBack;    //队长更换返回按钮//
    private GameObject m_cBtnChanage;   //换位置按钮//
    private GameObject m_cBtnLeader;    //换队长//
    private GameObject m_cBtnLeft;  //左边按钮//
    private GameObject m_cBtnRight; //右边按钮//

    private UILabel m_labTeamCost;
    private UILabel m_labSkillName;
    private UILabel m_labSkillExplain;

    private UILabel m_labTeamNum;

    private GameObject m_tLightMark;

    private const string RES_TEAM_EDITOR = "GUI_Battle_Editor";
    private const string TEAM_EDIT = "BATTLE_EDITOR";
    private const string HERO_Panel = "TEAM"; //3D模型主面板//
    private const string HERO_TEAM = "Team_";   //3D模型的队伍//
    private const string HERO_POS = "Pos_"; //3D模型的位置//

    private GameObject m_cTeamEditor;
    private GameObject[] m_vecTeamPos;  //位置//
    private GameObject[] m_heroTeamPos; //英雄位置//
    private List<GfxObject[]> m_lstGfxObj;    //渲染实体

    private List<GameObject> m_lstAsncModelObj = new List<GameObject>();

    //private Dictionary<int, List<ResourceRequireOwner>> m_lstNewAddRes = new Dictionary<int, List<ResourceRequireOwner>>();
    private List<int> m_lstTeamNums = new List<int>();
    private List<ResourceRequireOwner> m_lstOwners = new List<ResourceRequireOwner>();
    private List<string> m_lstResName = new List<string>();

    private int m_iIndex;   //当前面板位置
    private int m_cTeamNum; //当前队伍编号//
    public int m_iTeamPanelNum; //当前中间面板编号//
    private HeroTeam m_cTeam;   //当前队伍//
    private bool m_bDrag;   //拖拽状态

    private TeamState m_teamState;  //面板状态//
    private PressState m_pressState;    //按下状态//

    public int m_iSelectHeroPos;   //点击的英雄位置//

    private int m_iChangeHeroPos_1 = -1;    //第一个选择的位置//

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

    public GUITeamEditor(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_TEAM_EDITOR, UILAYER.GUI_PANEL)
    {
        this.m_lstGfxObj = new List<GfxObject[]>();
    }

    public override void Show()
    {
        this.m_eLoadingState = LOADING_STATE.START;
        GUI_FUNCTION.AYSNCLOADING_SHOW();

        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_TEAM_EDITOR);
        if (this.m_cGUIObject == null)
        {
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
        }
        //for (int i = 0; i < 10; i++)
        //{
        //    HeroTeam hTeam = new HeroTeam().GetTeam(i);
        //    for (int j = 0; j < 5; j++)
        //    {
        //        if (hTeam.m_vecTeam[j] != 0)
        //        {
        //            Hero hero = Role.role.GetHeroProperty().GetHero(hTeam.m_vecTeam[j]);

        //            if (hero != null)
        //            {
        //                ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_MODEL_PATH, hero.m_strModel);
        //            }
        //        }
        //    }

        //}
    }

    /// <summary>
    /// 有回调的show
    /// </summary>
    /// <param name="callback"></param>
    public void Show(CALLBACK callback)
    {
        this.m_delCallBack = callback;
        Show();
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        CameraManager.GetInstance().HidenUIModelCamera();
        m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();

        Role.role.GetBaseProperty().m_iCurrentTeam = m_cTeamNum;
        SendAgent.SendTeamEditor(Role.role.GetBaseProperty().m_iPlayerId, GetTeamListInt(), Role.role.GetBaseProperty().m_iCurrentTeam);

        ResourceMgr.UnloadUnusedResources();
        base.Hiden();
        Destory();

    }

    public void HidenNotSend()
    {
        ResourceMgr.UnloadUnusedResources();
        base.Hiden();
        Role.role.GetBaseProperty().m_iCurrentTeam = m_cTeamNum;
        //Debug.Log("Role.role.GetBaseProperty().m_iCurrentTeam_" + Role.role.GetBaseProperty().m_iCurrentTeam);
        CameraManager.GetInstance().HidenUIModelCamera();
        m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
        //SetLocalPos(Vector3.one * 0xFFFF);
        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        m_lstOwners.Clear();
        m_lstResName.Clear();
        m_lstTeamNums.Clear();

        foreach (GfxObject[] item in this.m_lstGfxObj)
        {
            for (int i = 0; i < item.Length; i++)
            {
                if (item[i] != null)
                {
                    item[i].Destory();
                }
            }
        }
        this.m_lstGfxObj.Clear();

        foreach (GameObject objj in m_lstAsncModelObj)
        {
            GameObject.DestroyImmediate(objj);
        }
        m_lstAsncModelObj.Clear();

        if (m_cTeamEditor!=null)
        {
            GameObject.DestroyImmediate(m_cTeamEditor);
        }
        m_cTeamEditor = null;

        m_cTopPanel = null; //顶部面板
        m_cMainPanel = null;    //主面板
        m_cHeroPanel = null;    //英雄主界面//

        m_cBtnBack = null;  //背景碰撞体
        m_cBtnClose = null; //关闭按钮
        m_cBtnChanageBack = null;   //换位置返回按钮//
        m_cBtnLeaderBack = null;    //队长更换返回按钮//
        m_cBtnChanage = null;   //换位置按钮//
        m_cBtnLeader = null;    //换队长//
        m_cBtnLeft = null;  //左边按钮//
        m_cBtnRight = null; //右边按钮//

        m_labTeamCost = null;
        m_labSkillName = null;
        m_labSkillExplain = null;
        m_labTeamNum = null;

        m_tLightMark = null;
        m_vecTeamPos = null;  //位置//
        m_heroTeamPos = null; //英雄位置//

        base.Destory();
    }

    //游戏刷新//
    private void Refresh()
    {
        m_pressState = PressState.Normal;
        m_teamState = TeamState.Main;

        m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Hiden();

        for (int i = 0; i < 3; i++)
        {
            //Debug.Log("m_cTeanmNum_" + m_cTeamNum);
            int index = m_cTeamNum + i - 1;

            //Debug.Log(m_iTeamPanelNum);
            int panelIndex = (m_iTeamPanelNum + i - 1) % 3;
            //Debug.Log("panelIndex_" + panelIndex);

            if (panelIndex < 0)
            {
                panelIndex += 3;
            }

            if (index < 0)
            {
                index += 10;
            }
            else if (index > 9)
            {
                index -= 10;
            }


            //for (int j = 0; j < 5; j++)
            //{
            //    RefreshHero(panelIndex, index, j);
            //}
            RefreshHeros(panelIndex, index);
        }
    }

    //刷新整个位置
    private void RefreshHeros(int teamPos, int teamNum)
    {
        //加载新5个之前 卸载旧5个
        RemoveOldTeamLoading();

        for (int i = 0; i < 3; i++)
        {
            if (i==teamPos)
            {
                continue;   
            }
            for (int j = 0; j < 5; j++)
            {
                if ((m_lstGfxObj[i])[j] != null)
                {
                    (m_lstGfxObj[i])[j].Destory();
                }
            }
        }
 

        for (int i = 0; i < 5; i++)
        {
            LoadHeroModel(teamPos, teamNum, i);
        }
    }

    //刷新当前队伍英雄 pos为英雄在队伍的位置//
    private void RefreshHero(int teamPos, int teamNum, int pos)
    {
        LoadHeroModel(teamPos, teamNum, pos);

        //GameObject uiPos = GUI_FINDATION.GET_GAME_OBJECT(m_vecTeamPos[teamPos], OBJ_POS + (pos + 1));
        //GameObject heroPos = GUI_FINDATION.GET_GAME_OBJECT(m_heroTeamPos[teamPos], HERO_POS + (pos + 1));

        //HeroTeam hTeam = new HeroTeam().GetTeam(teamNum);

        //Hero hero = Role.role.GetHeroProperty().GetHero(hTeam.m_vecTeam[pos]);
        //HeroAttributes ha = new HeroAttributes();

        //if ((m_lstGfxObj[teamPos])[pos] != null)
        //{
        //    (m_lstGfxObj[teamPos])[pos].Destory();
        //}

        //if (pos == hTeam.GetLeaderIndex())
        //{
        //    LeaderSkillTable ldSkill = LeaderSkillTableManager.GetInstance().GetLeaderSkillTable(hero.m_iLeaderSkillID);
        //    m_labSkillName.text = "";
        //    m_labSkillExplain.text = "";
        //    if (ldSkill != null)
        //    {
        //        m_labSkillName.text = ldSkill.Name;
        //        m_labSkillExplain.text = ldSkill.Desc;
        //    }


        //    GameObject teamLeaderObj = GUI_FINDATION.GET_GAME_OBJECT(m_vecTeamPos[teamPos], LEADER_ICON);
        //    teamLeaderObj.transform.parent = uiPos.transform;
        //    teamLeaderObj.transform.localScale = Vector3.one;
        //    teamLeaderObj.transform.localPosition = new Vector3(70f, 33.5f, 0);
        //    teamLeaderObj.transform.parent = m_vecTeamPos[teamPos].transform;
        //}


        //if (hero != null)
        //{
        //    ha.ShowAttributes(uiPos, true);
        //    ha.SetAttributes(uiPos, hero);

        //    GameObject obj = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.Load(hero.m_strModel)) as GameObject;
        //    obj.transform.parent = heroPos.transform;
        //    obj.transform.localPosition = new Vector3(0, 0.15f, 0);
        //    obj.transform.localScale = Vector3.one;
        //    obj.name = hero.m_strModel;

        //    (m_lstGfxObj[teamPos])[pos] = new GfxObject(obj);
        //    (m_lstGfxObj[teamPos])[pos].Initialize();
        //}
        //else
        //{
        //    ha.ShowAttributes(uiPos, false);
        //}
    }

    //队长变化设置//
    private void SetLeader(int pos)
    {
        Hero hero = Role.role.GetHeroProperty().GetHero(m_cTeam.m_vecTeam[pos]);
        //Debug.Log("m_cTeam.m_vecTeam_" + m_cTeam.m_vecTeam[pos]);

        if (hero == null)
        {
            Debug.Log("Hero is null!");
            return;
        }

        LeaderSkillTable ldSkill = LeaderSkillTableManager.GetInstance().GetLeaderSkillTable(hero.m_iLeaderSkillID);
        m_labSkillName.text = "";
        m_labSkillExplain.text = "";
        if (ldSkill != null)
        {
            m_labSkillName.text = ldSkill.Name;
            m_labSkillExplain.text = ldSkill.Desc;
        }

        //GameObject uiTeam = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, TEAM_POS + (m_cTeamNum + 1));
        GameObject uiPos = GUI_FINDATION.GET_GAME_OBJECT(m_vecTeamPos[m_iTeamPanelNum], OBJ_POS + (pos + 1));

        GameObject teamLeaderObj = GUI_FINDATION.GET_GAME_OBJECT(m_vecTeamPos[m_iTeamPanelNum], LEADER_ICON);

        teamLeaderObj.transform.parent = uiPos.transform;
        teamLeaderObj.transform.localScale = Vector3.one;
        teamLeaderObj.transform.localPosition = new Vector3(70f, 33.5f, 0);
        teamLeaderObj.transform.parent = m_vecTeamPos[m_iTeamPanelNum].transform;

        m_cTeam.m_iLeadID = hero.m_iID;
    }

    //变换位置//
    private void SetPosition(int pos)
    {


        if (m_iChangeHeroPos_1 == -1)
        {
            if (Role.role.GetHeroProperty().GetHero(m_cTeam.m_vecTeam[pos]) == null)
            {
                return;
            }

            m_iChangeHeroPos_1 = pos;
            GameObject obj = GUI_FINDATION.GET_GAME_OBJECT(m_vecTeamPos[m_iTeamPanelNum], OBJ_POS + (pos + 1));
            GameObject pointerObj = GUI_FINDATION.GET_GAME_OBJECT(obj, POINTER_ICON);
            pointerObj.SetActive(false);
        }
        else
        {
            //交换队伍里面的位置//
            int heroID = m_cTeam.m_vecTeam[m_iChangeHeroPos_1];
            m_cTeam.m_vecTeam[m_iChangeHeroPos_1] = m_cTeam.m_vecTeam[pos];
            m_cTeam.m_vecTeam[pos] = heroID;

            Debug.Log("m_cTeamNum_" + m_cTeamNum);

            RefreshHero(m_iTeamPanelNum, m_cTeamNum, m_iChangeHeroPos_1);
            RefreshHero(m_iTeamPanelNum, m_cTeamNum, pos);

            m_iChangeHeroPos_1 = -1;
            SetPointer(true);
        }


    }

    //设置队伍信息//
    private void SetTeamNum()
    {
        //Debug.Log("SetTeamNum");
        RoleExpTableManager.GetInstance().m_iCurrentCost = m_cTeam.GetCostValue();
        //cost超限 color警告
        string redColor = "[ffffff]";
        if (m_cTeam.GetCostValue() > RoleExpTableManager.GetInstance().GetMaxCost(Role.role.GetBaseProperty().m_iLevel))
        {
            redColor = "[ff0000]";
        }
        m_labTeamCost.text = redColor + m_cTeam.GetCostValue().ToString() + "[ffffff]/" + RoleExpTableManager.GetInstance().GetMaxCost(Role.role.GetBaseProperty().m_iLevel);
        m_labTeamNum.text = "队伍" + (m_cTeamNum + 1).ToString();
        m_tLightMark.transform.localPosition = new Vector3(-255 + m_cTeamNum * 40, -230f, 0);

        //int leaderID = new HeroTeam().GetTeam(Role.role.GetBaseProperty().m_iCurrentTeam).m_iLeadID;
        Hero tmphero = Role.role.GetHeroProperty().GetHero(m_cTeam.m_iLeadID);
        LeaderSkillTable ldSkill = LeaderSkillTableManager.GetInstance().GetLeaderSkillTable(tmphero.m_iLeaderSkillID);
        m_labSkillName.text = "";
        m_labSkillExplain.text = "";
        if (ldSkill != null)
        {
            m_labSkillName.text = ldSkill.Name;
            m_labSkillExplain.text = ldSkill.Desc;
        }
    }

    //设置当前队伍指针//
    private void SetPointer(bool isShow)
    {
        for (int i = 1; i <= 5; i++)
        {
            //GameObject uiTeam = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, TEAM_POS + (m_cTeamNum + 1));
            //Debug.Log("m_iTeamPanelNum_" + m_iTeamPanelNum);
            GameObject pos = GUI_FINDATION.GET_GAME_OBJECT(m_vecTeamPos[m_iTeamPanelNum], OBJ_POS + i);
            GameObject pointerObj = GUI_FINDATION.GET_GAME_OBJECT(pos, POINTER_ICON);
            pointerObj.SetActive(isShow);
        }
    }

    //显示所有信息//
    private void ShowAllHero()
    {
        //m_bIsLeave = false;
        HidenNotSend();
        GUITeamHero heropre = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_TEAMHERO) as GUITeamHero;
        //heropre.SetOldID(this.ID);
        heropre.Show();
    }

    //按钮控制//
    public void ShowBtn(TeamState teamState)
    {
        if (m_teamState == TeamState.Main)
        {
            m_cBtnClose.SetActive(true);
            m_cBtnChanageBack.SetActive(false);
            m_cBtnLeaderBack.SetActive(false);
        }
        else if (m_teamState == TeamState.Leader)
        {
            m_cBtnClose.SetActive(false);
            m_cBtnChanageBack.SetActive(false);
            m_cBtnLeaderBack.SetActive(true);
        }
        else if (m_teamState == TeamState.Chanage)
        {
            m_cBtnClose.SetActive(false);
            m_cBtnChanageBack.SetActive(true);
            m_cBtnLeaderBack.SetActive(false);
        }
    }

    //返回//
    private void OnClose(GUI_INPUT_INFO info, object[] arg)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {

            SetPointer(false);
            ShowBtn(m_teamState);

            Hiden();

            if (false)
            {
                if (this.m_delCallBack != null)
                {
                    SessionManager.GetInstance().SetCallBack(m_delCallBack.Invoke);
                }
                //SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERO_MENU).Show);
            }
            else
            {
                if (this.m_delCallBack != null)
                {
                    m_delCallBack.Invoke();
                }
                // this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERO_MENU).Show();
            }
        }
    }

    //变换位置返回//
    private void OnChanageBack(GUI_INPUT_INFO info, object[] arg)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            m_teamState = TeamState.Main;
            GUI_FINDATION.GET_GAME_OBJECT(m_cBtnChanage, "Background").GetComponent<UISprite>().gameObject.SetActive(true);
            GUI_FINDATION.GET_GAME_OBJECT(m_cBtnLeader, "Background").GetComponent<UISprite>().gameObject.SetActive(true);
            ShowBtn(m_teamState);
            SetPointer(false);
            m_iChangeHeroPos_1 = -1;
        }
    }

    //更换领导返回//
    private void OnLeaderBack(GUI_INPUT_INFO info, object[] arg)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            m_teamState = TeamState.Main;
            GUI_FINDATION.GET_GAME_OBJECT(m_cBtnChanage, "Background").GetComponent<UISprite>().gameObject.SetActive(true);
            GUI_FINDATION.GET_GAME_OBJECT(m_cBtnLeader, "Background").GetComponent<UISprite>().gameObject.SetActive(true);
            ShowBtn(m_teamState);
        }
    }

    //变换位置//
    private void OnChanage(GUI_INPUT_INFO info, object[] arg)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            GUI_FINDATION.GET_GAME_OBJECT(m_cBtnChanage, "Background").GetComponent<UISprite>().gameObject.SetActive(false);
            GUI_FINDATION.GET_GAME_OBJECT(m_cBtnLeader, "Background").GetComponent<UISprite>().gameObject.SetActive(true);
            SetPointer(true);
            m_teamState = TeamState.Chanage;
            ShowBtn(m_teamState);
            m_iChangeHeroPos_1 = -1;

            //下方提示
            GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
            gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_CHANGE_POS));
        }
    }

    //更换领导//
    private void OnLeader(GUI_INPUT_INFO info, object[] arg)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            GUI_FINDATION.GET_GAME_OBJECT(m_cBtnChanage, "Background").GetComponent<UISprite>().gameObject.SetActive(true);
            GUI_FINDATION.GET_GAME_OBJECT(m_cBtnLeader, "Background").GetComponent<UISprite>().gameObject.SetActive(false);
            SetPointer(false);
            m_teamState = TeamState.Leader;
            ShowBtn(m_teamState);

            //下方提示
            GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
            gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_CHANGE_HERO));
        }
    }

    //查看详细信息//
    private void ShowHeroDetail()
    {
        //m_bIsLeave = false;
        GUIHeroDetail herodetail = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_HERODETAIL) as GUIHeroDetail;
        Hero hero = Role.role.GetHeroProperty().GetHero(m_cTeam.m_vecTeam[m_iSelectHeroPos]);
        if (hero != null)
        {

            Hiden();

            herodetail.Show(Show, hero);
        }

    }

    //镜头切换//
    private void CameraTransfrom()
    {
        iTween.CameraFadeAdd();
        iTween.CameraFadeTo(iTween.Hash("amount", 1, "time", 1));
        iTween.CameraFadeTo(iTween.Hash("amount", 0, "time", 1, "delay", 1));
        MainApplication.GetInstance().TIME_EVENT(1, ShowHeroDetail);
    }

    private void OnTeamDrag(GUI_INPUT_INFO info, object[] arg)
    {
        if (m_teamState == TeamState.Main)
        {

            if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.DRAG)
            {
                //isSend = true;
                m_cBtnBack.transform.localPosition = m_cBtnBack.transform.localPosition + new Vector3(info.m_vecDelta.x, 0, 0);
                m_cHeroPanel.transform.localPosition -= new Vector3(info.m_vecDelta.x / 85.33f, 0, 0);

                m_bDrag = true;
            }
            else if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS)
            {
                //isSend = true;
                if (m_bDrag)
                {
                    if (Math.Abs(m_iIndex * OFFSET_DIS - m_cBtnBack.transform.localPosition.x) > 10)
                    {
                        if (m_iIndex * OFFSET_DIS - m_cBtnBack.transform.localPosition.x > 0)
                        {
                            m_iIndex--;
                            m_cTeamNum++;
                            if (m_cTeamNum < 0)
                            {
                                m_cTeamNum += 10;
                            }
                            else if (m_cTeamNum > 9)
                            {
                                m_cTeamNum -= 10;
                            }
                            m_cTeam = new HeroTeam().Get<HeroTeam>(m_cTeamNum);
                            int tmpIndex = (2 - m_iIndex) % 3;
                            while (tmpIndex < 0)
                            {
                                tmpIndex += 3;
                            }
                            m_iTeamPanelNum = tmpIndex - 1;

                            if (m_iTeamPanelNum < 0)
                            {
                                m_iTeamPanelNum += 3;
                            }

                            //Debug.Log("m_iTeamPanelNum_"+m_iTeamPanelNum);
                            //Debug.Log("m_iIndex_" + m_iIndex);
                            m_vecTeamPos[tmpIndex].transform.localPosition = new Vector3(OFFSET_DIS * -(m_iIndex - 1), m_vecTeamPos[tmpIndex].transform.localPosition.y, m_vecTeamPos[tmpIndex].transform.localPosition.z);
                            m_heroTeamPos[tmpIndex].transform.localPosition = new Vector3(-(OFFSET_DIS * -(m_iIndex - 1)) / 85.33f, m_heroTeamPos[tmpIndex].transform.localPosition.y, m_heroTeamPos[tmpIndex].transform.localPosition.z);
                            int index = m_cTeamNum + 1;

                            if (index < 0)
                            {
                                index += 10;
                            }
                            else if (index > 9)
                            {
                                index -= 10;
                            }
                            //for (int i = 0; i < 5; i++)
                            //{
                            //    RefreshHero(tmpIndex, index, i);
                            //}
                            RefreshHeros(m_iTeamPanelNum, m_cTeamNum);
                            SetTeamNum();
                        }
                        else
                        {
                            m_iIndex++;
                            m_cTeamNum--;
                            if (m_cTeamNum < 0)
                            {
                                m_cTeamNum += 10;
                            }
                            else if (m_cTeamNum > 9)
                            {
                                m_cTeamNum -= 10;
                            }
                            m_cTeam = new HeroTeam().Get<HeroTeam>(m_cTeamNum);
                            int tmpIndex = (2 - (2 + m_iIndex) % 3) % 3;

                            while (tmpIndex < 0)
                            {
                                tmpIndex += 3;
                            }

                            m_iTeamPanelNum = tmpIndex + 1;

                            if (m_iTeamPanelNum > 2)
                            {
                                m_iTeamPanelNum -= 3;
                            }

                            m_vecTeamPos[tmpIndex].transform.localPosition = new Vector3(OFFSET_DIS * -(m_iIndex + 1), m_vecTeamPos[tmpIndex].transform.localPosition.y, m_vecTeamPos[tmpIndex].transform.localPosition.z);
                            m_heroTeamPos[tmpIndex].transform.localPosition = new Vector3(-(OFFSET_DIS * -(m_iIndex + 1)) / 85.33f, m_heroTeamPos[tmpIndex].transform.localPosition.y, m_heroTeamPos[tmpIndex].transform.localPosition.z);
                            int index = m_cTeamNum - 1;

                            if (index < 0)
                            {
                                index += 10;
                            }
                            else if (index > 9)
                            {
                                index -= 10;
                            }
                            //for (int i = 0; i < 5; i++)
                            //{
                            //    RefreshHero(tmpIndex, index, i);
                            //}
                            RefreshHeros(m_iTeamPanelNum, m_cTeamNum);
                            SetTeamNum();
                        }
                        CTween.TweenPosition(m_cBtnBack, 0.4f, new Vector3(m_iIndex * OFFSET_DIS, m_cBtnBack.transform.localPosition.y, m_cBtnBack.transform.localPosition.z));
                        CTween.TweenPosition(m_cHeroPanel, 0.4f, new Vector3(-m_iIndex * OFFSET_DIS / 85.33f, m_cHeroPanel.transform.localPosition.y, m_cHeroPanel.transform.localPosition.z));

                        //翻页特效
                        MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SLIDE);

                    }
                    else
                    {
                        CTween.TweenPosition(m_cBtnBack, 0.2f, new Vector3(m_iIndex * OFFSET_DIS, m_cBtnBack.transform.localPosition.y, m_cBtnBack.transform.localPosition.z));
                        CTween.TweenPosition(m_cHeroPanel, 0.2f, new Vector3(-m_iIndex * OFFSET_DIS / 85.33f, m_cHeroPanel.transform.localPosition.y, m_cHeroPanel.transform.localPosition.z));
                        //翻页特效
                        MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SLIDE);
                    }
                }
                m_bDrag = false;
            }
        }
    }

    //人物身上事件//
    private void OnHero(GUI_INPUT_INFO info, object[] arg)
    {

        if (m_teamState == TeamState.Main)
        {

            if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.DRAG)
            {
                //isSend = true;
                //Debug.Log("Drag");
                m_cBtnBack.transform.localPosition = m_cBtnBack.transform.localPosition + new Vector3(info.m_vecDelta.x, 0, 0);
                m_cHeroPanel.transform.localPosition -= new Vector3(info.m_vecDelta.x / 85.33f, 0, 0);

                m_bDrag = true;
            }
            else if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.PRESS)
            {
                //isSend = true;
                //Debug.Log("info.m_vecDelta.x_" + info.m_vecDelta.x);
                //Debug.Log(m_bDrag);
                if (!m_bDrag)
                {
                    m_iSelectHeroPos = (int)arg[0];
                    if (m_pressState == PressState.Normal)
                    {

                        m_pressState = PressState.Press;
                        m_fPressTime = Time.fixedTime;
                    }
                    else if (m_pressState == PressState.Press)
                    {
                        //m_pressState = PressState.Release;
                        m_fReleaseTime = Time.fixedTime;

                        if ((m_fPressTime - m_fReleaseTime) < 0.1f)
                        {
                            ShowAllHero();
                            m_fReleaseTime = 0;
                        }

                        m_pressState = PressState.Normal;
                    }
                }
                else
                {
                    //isSend = true;
                    if (Math.Abs(m_iIndex * OFFSET_DIS - m_cBtnBack.transform.localPosition.x) > 10)
                    {
                        if (m_iIndex * OFFSET_DIS - m_cBtnBack.transform.localPosition.x > 0)
                        {
                            m_iIndex--;
                            m_cTeamNum++;
                            if (m_cTeamNum < 0)
                            {
                                m_cTeamNum += 10;
                            }
                            else if (m_cTeamNum > 9)
                            {
                                m_cTeamNum -= 10;
                            }
                            m_cTeam = new HeroTeam().Get<HeroTeam>(m_cTeamNum);
                            int tmpIndex = (2 - m_iIndex) % 3;
                            while (tmpIndex < 0)
                            {
                                tmpIndex += 3;
                            }
                            m_iTeamPanelNum = tmpIndex - 1;

                            if (m_iTeamPanelNum < 0)
                            {
                                m_iTeamPanelNum += 3;
                            }

                            //Debug.Log("m_iTeamPanelNum_" + m_iTeamPanelNum);
                            m_vecTeamPos[tmpIndex].transform.localPosition = new Vector3(OFFSET_DIS * -(m_iIndex - 1), m_vecTeamPos[tmpIndex].transform.localPosition.y, m_vecTeamPos[tmpIndex].transform.localPosition.z);
                            m_heroTeamPos[tmpIndex].transform.localPosition = new Vector3(-(OFFSET_DIS * -(m_iIndex - 1)) / 85.33f, m_heroTeamPos[tmpIndex].transform.localPosition.y, m_heroTeamPos[tmpIndex].transform.localPosition.z);
                            int index = m_cTeamNum + 1;

                            if (index < 0)
                            {
                                index += 10;
                            }
                            else if (index > 9)
                            {
                                index -= 10;
                            }
                            //for (int i = 0; i < 5; i++)
                            //{
                            //    RefreshHero(tmpIndex, index, i);
                            //}
                            RefreshHeros(m_iTeamPanelNum, m_cTeamNum);
                            SetTeamNum();
                        }
                        else
                        {
                            m_iIndex++;
                            m_cTeamNum--;
                            if (m_cTeamNum < 0)
                            {
                                m_cTeamNum += 10;
                            }
                            else if (m_cTeamNum > 9)
                            {
                                m_cTeamNum -= 10;
                            }
                            m_cTeam = new HeroTeam().Get<HeroTeam>(m_cTeamNum);
                            int tmpIndex = (2 - (2 + m_iIndex) % 3) % 3;

                            while (tmpIndex < 0)
                            {
                                tmpIndex += 3;
                            }

                            m_iTeamPanelNum = tmpIndex + 1;

                            if (m_iTeamPanelNum > 2)
                            {
                                m_iTeamPanelNum -= 3;
                            }

                            //Debug.Log("m_iTeamPanelNum_" + m_iTeamPanelNum);
                            m_vecTeamPos[tmpIndex].transform.localPosition = new Vector3(OFFSET_DIS * -(m_iIndex + 1), m_vecTeamPos[tmpIndex].transform.localPosition.y, m_vecTeamPos[tmpIndex].transform.localPosition.z);
                            m_heroTeamPos[tmpIndex].transform.localPosition = new Vector3(-(OFFSET_DIS * -(m_iIndex + 1)) / 85.33f, m_heroTeamPos[tmpIndex].transform.localPosition.y, m_heroTeamPos[tmpIndex].transform.localPosition.z);
                            int index = m_cTeamNum - 1;

                            if (index < 0)
                            {
                                index += 10;
                            }
                            else if (index > 9)
                            {
                                index -= 10;
                            }
                            //for (int i = 0; i < 5; i++)
                            //{
                            //    RefreshHero(tmpIndex, index, i);
                            //}
                            RefreshHeros(m_iTeamPanelNum, m_cTeamNum);
                            SetTeamNum();
                        }

                        CTween.TweenPosition(m_cBtnBack, 0.4f, new Vector3(m_iIndex * OFFSET_DIS, m_cBtnBack.transform.localPosition.y, m_cBtnBack.transform.localPosition.z));
                        CTween.TweenPosition(m_cHeroPanel, 0.4f, new Vector3(-m_iIndex * OFFSET_DIS / 85.33f, m_cHeroPanel.transform.localPosition.y, m_cHeroPanel.transform.localPosition.z));
                        //翻页特效
                        MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SLIDE);
                    }
                    else
                    {
                        CTween.TweenPosition(m_cBtnBack, 0.2f, new Vector3(m_iIndex * OFFSET_DIS, m_cBtnBack.transform.localPosition.y, m_cBtnBack.transform.localPosition.z));
                        CTween.TweenPosition(m_cHeroPanel, 0.2f, new Vector3(-m_iIndex * OFFSET_DIS / 85.33f, m_cHeroPanel.transform.localPosition.y, m_cHeroPanel.transform.localPosition.z));
                        //翻页特效
                        MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SLIDE);
                    }

                    m_pressState = PressState.Normal;
                }

                m_bDrag = false;

            }

        }

        if (m_teamState == TeamState.Leader)
        {
            //isSend = true;
            m_iSelectHeroPos = (int)arg[0];

            if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
            {
                SetLeader(m_iSelectHeroPos);
            }

        }

        if (m_teamState == TeamState.Chanage)
        {
            //isSend = true;
            m_iSelectHeroPos = (int)arg[0];
            //Debug.Log("m_iSelectHeroPos_" + m_iSelectHeroPos);
            if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
            {
                //Debug.Log("m_iSelectHeroPos_" + m_iSelectHeroPos);
                SetPosition(m_iSelectHeroPos);
            }

        }

    }

    //点击左边按钮//
    private void OnLeft(GUI_INPUT_INFO info, object[] arg)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            m_iIndex--;
            m_cTeamNum++;
            if (m_cTeamNum < 0)
            {
                m_cTeamNum += 10;
            }
            else if (m_cTeamNum > 9)
            {
                m_cTeamNum -= 10;
            }
            m_cTeam = new HeroTeam().Get<HeroTeam>(m_cTeamNum);
            int tmpIndex = (2 - m_iIndex) % 3;
            while (tmpIndex < 0)
            {
                tmpIndex += 3;
            }
            m_iTeamPanelNum = tmpIndex - 1;

            if (m_iTeamPanelNum < 0)
            {
                m_iTeamPanelNum += 3;
            }

            //Debug.Log("m_iTeamPanelNum_"+m_iTeamPanelNum);
            //Debug.Log("m_iIndex_" + m_iIndex);
            m_vecTeamPos[tmpIndex].transform.localPosition = new Vector3(OFFSET_DIS * -(m_iIndex - 1), m_vecTeamPos[tmpIndex].transform.localPosition.y, m_vecTeamPos[tmpIndex].transform.localPosition.z);
            m_heroTeamPos[tmpIndex].transform.localPosition = new Vector3(-(OFFSET_DIS * -(m_iIndex - 1)) / 85.33f, m_heroTeamPos[tmpIndex].transform.localPosition.y, m_heroTeamPos[tmpIndex].transform.localPosition.z);
            int index = m_cTeamNum + 1;

            if (index < 0)
            {
                index += 10;
            }
            else if (index > 9)
            {
                index -= 10;
            }
            //for (int i = 0; i < 5; i++)
            //{
            //    RefreshHero(tmpIndex, index, i);
            //}
            //Debug.LogError(m_iTeamPanelNum);
            RefreshHeros(m_iTeamPanelNum, m_cTeamNum);
            SetTeamNum();

            CTween.TweenPosition(m_cBtnBack, 0.4f, new Vector3(m_iIndex * OFFSET_DIS, m_cBtnBack.transform.localPosition.y, m_cBtnBack.transform.localPosition.z));
            CTween.TweenPosition(m_cHeroPanel, 0.4f, new Vector3(-m_iIndex * OFFSET_DIS / 85.33f, m_cHeroPanel.transform.localPosition.y, m_cHeroPanel.transform.localPosition.z));

            //翻页音效
            MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SLIDE);
        }
    }

    //点击右边按钮//
    private void OnRight(GUI_INPUT_INFO info, object[] arg)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            m_iIndex++;
            m_cTeamNum--;
            if (m_cTeamNum < 0)
            {
                m_cTeamNum += 10;
            }
            else if (m_cTeamNum > 9)
            {
                m_cTeamNum -= 10;
            }
            m_cTeam = new HeroTeam().Get<HeroTeam>(m_cTeamNum);
            int tmpIndex = (2 - (2 + m_iIndex) % 3) % 3;
        
            while (tmpIndex < 0)
            {
                tmpIndex += 3;
            }

            m_iTeamPanelNum = tmpIndex + 1;

            if (m_iTeamPanelNum > 2)
            {
                m_iTeamPanelNum -= 3;
            }

            m_vecTeamPos[tmpIndex].transform.localPosition = new Vector3(OFFSET_DIS * -(m_iIndex + 1), m_vecTeamPos[tmpIndex].transform.localPosition.y, m_vecTeamPos[tmpIndex].transform.localPosition.z);
            m_heroTeamPos[tmpIndex].transform.localPosition = new Vector3(-(OFFSET_DIS * -(m_iIndex + 1)) / 85.33f, m_heroTeamPos[tmpIndex].transform.localPosition.y, m_heroTeamPos[tmpIndex].transform.localPosition.z);
            int index = m_cTeamNum - 1;

            if (index < 0)
            {
                index += 10;
            }
            else if (index > 9)
            {
                index -= 10;
            }
            //for (int i = 0; i < 5; i++)
            //{
            //    RefreshHero(tmpIndex, index, i);
            //}
            //Debug.LogError(m_iTeamPanelNum);
            RefreshHeros(m_iTeamPanelNum, m_cTeamNum);

            SetTeamNum();

            CTween.TweenPosition(m_cBtnBack, 0.4f, new Vector3(m_iIndex * OFFSET_DIS, m_cBtnBack.transform.localPosition.y, m_cBtnBack.transform.localPosition.z));
            CTween.TweenPosition(m_cHeroPanel, 0.4f, new Vector3(-m_iIndex * OFFSET_DIS / 85.33f, m_cHeroPanel.transform.localPosition.y, m_cHeroPanel.transform.localPosition.z));

            //翻页音效
            MediaMgr.sInstance.PlaySE(SOUND_DEFINE.SE_SLIDE);
        }
    }

    //得到请求发送队伍数据//
    private List<int[]> GetTeamListInt()
    {
        List<int[]> teams = new List<int[]>();

        HeroTeam[] teamsInt = new HeroTeam().ToArray<HeroTeam>();

        for (int i = 0; i < teamsInt.Length; i++)
        {
            int[] team = new int[6];
            for (int j = 0; j < teamsInt[i].m_vecTeam.Length; j++)
            {
                team[j] = teamsInt[i].m_vecTeam[j];
            }
            team[5] = teamsInt[i].GetLeaderIndex();

            teams.Add(team);
        }

        return teams;
    }

    protected override void InitGUI()
    {
        base.Show();

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_MAKE_TEAM));

        if (m_cGUIObject == null)
        {
			m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            m_cGUIObject.transform.localScale = Vector3.one;

            m_cTopPanel = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, TOP_PANEL);
            m_cMainPanel = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, MAIN_PANEL);
			m_cTeamEditor = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_TEAM_EDITOR)) as GameObject;
            GameObject objEdit = GUI_FINDATION.FIND_GAME_OBJECT(TEAM_EDIT);
            m_cTeamEditor.transform.parent = objEdit.transform;
            m_cTeamEditor.transform.localPosition = Vector3.zero;
            m_cTeamEditor.transform.localScale = Vector3.one;

            m_cHeroPanel = GUI_FINDATION.GET_GAME_OBJECT(m_cTeamEditor, HERO_Panel);
            m_cHeroPanel.transform.localPosition = new Vector3(0, 0.4f, 0);

            m_labTeamCost = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cGUIObject, COST_SPEND);
            m_labSkillName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cGUIObject, SKILL_NAME);
            m_labSkillExplain = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cGUIObject, SKILL_EXPLAIN);
            m_labTeamNum = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(m_cGUIObject, TEAM_NUMMBER);

            m_tLightMark = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, LIGHT_MARK);

            m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, BACK_COLLER);

            //按钮的初始化//
            m_cBtnClose = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, BTN_CLOSE);
            GUIComponentEvent ceClose = m_cBtnClose.AddComponent<GUIComponentEvent>();
            ceClose.AddIntputDelegate(OnClose);

            m_cBtnChanageBack = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, BTN_CHANAGEBACK);
            GUIComponentEvent ceChanageBack = m_cBtnChanageBack.AddComponent<GUIComponentEvent>();
            ceChanageBack.AddIntputDelegate(OnChanageBack);

            m_cBtnLeaderBack = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, BTN_LEADERBACK);
            GUIComponentEvent ceLeaderBack = m_cBtnLeaderBack.AddComponent<GUIComponentEvent>();
            ceLeaderBack.AddIntputDelegate(OnLeaderBack);

            m_cBtnChanage = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, BTN_CHANAGE);
            GUIComponentEvent ceChanage = m_cBtnChanage.AddComponent<GUIComponentEvent>();
            ceChanage.AddIntputDelegate(OnChanage);

            m_cBtnLeader = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, BTN_LEADER);
            GUIComponentEvent ceLeader = m_cBtnLeader.AddComponent<GUIComponentEvent>();
            ceLeader.AddIntputDelegate(OnLeader);

            m_cBtnLeft = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, BTN_LEFT);
            GUIComponentEvent ceLeft = m_cBtnLeft.AddComponent<GUIComponentEvent>();
            ceLeft.AddIntputDelegate(OnRight);

            m_cBtnRight = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, BTN_RIGHT);
            GUIComponentEvent ceRight = m_cBtnRight.AddComponent<GUIComponentEvent>();
            ceRight.AddIntputDelegate(OnLeft);

            m_iIndex = 0;
            m_iTeamPanelNum = 1;

            m_vecTeamPos = new GameObject[3];
            m_heroTeamPos = new GameObject[3];

            for (int i = 0; i < 3; i++)
            {
                GameObject uiTeam = GUI_FINDATION.GET_GAME_OBJECT(m_cGUIObject, TEAM_POS + (i + 1));
                m_vecTeamPos[i] = uiTeam;

                GameObject heroTeam = GUI_FINDATION.GET_GAME_OBJECT(m_cHeroPanel, HERO_TEAM + (i + 1));
                heroTeam.transform.localPosition = new Vector3(7.5f - i * 7.5f, -0.2f, -15f);
                m_heroTeamPos[i] = heroTeam;
                //添加事件//
                GUIComponentEvent ceTmp = uiTeam.AddComponent<GUIComponentEvent>();
                ceTmp.AddIntputDelegate(OnTeamDrag);

                //3D渲染实例//
                GfxObject[] vecObj = new GfxObject[5];
                this.m_lstGfxObj.Add(vecObj);

                for (int j = 0; j < 5; j++)
                {
                    GameObject uiPos = GUI_FINDATION.GET_GAME_OBJECT(uiTeam, OBJ_POS + (j + 1));

                    //添加人物事件//
                    //NGUITools.AddWidgetCollider(uiPos);
                    GUIComponentEvent cePos = uiPos.AddComponent<GUIComponentEvent>();
                    cePos.AddIntputDelegate(OnHero, j);
                }

            }

            //isSend = false;
        }

        m_cTeamNum = Role.role.GetBaseProperty().m_iCurrentTeam;
        m_cTeam = new HeroTeam().Get<HeroTeam>(m_cTeamNum);

        //Refresh();

        CameraManager.GetInstance().ShowUIModelCamera();
        this.m_bDrag = false;
        this.m_cGUIMgr.SetCurGUIID(this.ID);
        SetLocalPos(Vector3.zero);
        SetTeamNum();
        //m_bIsLeave = true;

        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_HERO_EDITOR2);
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_HERO_EDITOR4);
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
                    this.m_eState = LOAD_STATE.END;

                    GUI_FUNCTION.AYSNCLOADING_HIDEN();
                }
                //GUI_FUNCTION.BLACKHIDEN();
                return false;
            case LOADING_STATE.END:
                InitGUI();

                //Debug.Log("loadtime: " + (GAME_TIME.TIME_FIXED() - testTime));
                LoadAsyncAllHeroModel();


                this.m_eLoadingState++;
                break;
        }

        switch (this.m_eState)
        {
            case LOAD_STATE.START:
                break;
            case LOAD_STATE.LOAD:
                this.m_eState++;
                break;
            case LOAD_STATE.END:
                this.m_eState++;
                break;
            case LOAD_STATE.OUT:
                if (m_teamState == TeamState.Main && !m_bDrag)
                {
                    if ((Time.time - m_fPressTime) > 1 && (m_pressState == PressState.Press))
                    {
                        //Debug.Log("Long press");
                        m_pressState = PressState.Normal;
                        ShowHeroDetail();
                    }
                }
                break;
        }

        return base.Update();
    }

    private void LoadAsyncAllHeroModel()
    {
        m_pressState = PressState.Normal;
        m_teamState = TeamState.Main;

        m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Hiden();

        for (int i = 1; i < 2; i++)
        {
            //Debug.Log("m_cTeanmNum_" + m_cTeamNum);
            int index = m_cTeamNum + i - 1;

            //Debug.Log(m_iTeamPanelNum);
            int panelIndex = (m_iTeamPanelNum + i - 1) % 3;
            //Debug.Log("panelIndex_" + panelIndex);

            if (panelIndex < 0)
            {
                panelIndex += 3;
            }

            if (index < 0)
            {
                index += 10;
            }
            else if (index > 9)
            {
                index -= 10;
            }
            for (int j = 0; j < 5; j++)
            {
                LoadHeroModel(panelIndex, index, j);
            }
        }
    }

    private void LoadHeroModel(int teamPos, int teamNum, int pos)
    {
        //Debug.Log(teamPos + "  " + teamNum + "   " + pos);
        GameObject uiPos = GUI_FINDATION.GET_GAME_OBJECT(m_vecTeamPos[teamPos], OBJ_POS + (pos + 1));
        GameObject heroPos = GUI_FINDATION.GET_GAME_OBJECT(m_heroTeamPos[teamPos], HERO_POS + (pos + 1));

        HeroTeam hTeam = new HeroTeam().Get<HeroTeam>(teamNum);

        Hero hero = Role.role.GetHeroProperty().GetHero(hTeam.m_vecTeam[pos]);
        HeroAttributes ha = new HeroAttributes();

        //ResourceMgr.UnRequestAssetBundle("");
        if ((m_lstGfxObj[teamPos])[pos] != null)
        {
            (m_lstGfxObj[teamPos])[pos].Destory();
        }

        //是否队长位置，如果是队长位置，就显示主将
        if (pos == hTeam.GetLeaderIndex())
        {
            LeaderSkillTable ldSkill = LeaderSkillTableManager.GetInstance().GetLeaderSkillTable(hero.m_iLeaderSkillID);
            m_labSkillName.text = "";
            m_labSkillExplain.text = "";
            if (ldSkill != null)
            {
                m_labSkillName.text = ldSkill.Name;
                m_labSkillExplain.text = ldSkill.Desc;
            }


            GameObject teamLeaderObj = GUI_FINDATION.GET_GAME_OBJECT(m_vecTeamPos[teamPos], LEADER_ICON);
            teamLeaderObj.transform.parent = uiPos.transform;
            teamLeaderObj.transform.localScale = Vector3.one;
            teamLeaderObj.transform.localPosition = new Vector3(70f, 33.5f, 0);
            teamLeaderObj.transform.parent = m_vecTeamPos[teamPos].transform;
        }


        if (hero != null)
        {
            ha.ShowAttributes(uiPos, true);
            ha.SetAttributes(uiPos, hero);

//            ResourceRequireOwner owner = ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_MODEL_PATH + hero.m_strModel + ".res", 0, GAME_DEFINE.RES_VERSION, hero.m_strModel,null, RESOURCE_TYPE.WEB_OBJECT, ENCRYPT_TYPE.NORMAL, new DownLoadCallBack((str, obj, arr) =>
//             {
//                 //Debug.Log("load " + str);
//                 if (IsShow())
//                 {
//
//                     if (obj != null)
//                     {
//                         GameObject gameobj = GameObject.Instantiate((UnityEngine.Object)obj) as GameObject;
//                         gameobj.transform.parent = heroPos.transform;
//                         gameobj.transform.localPosition = new Vector3(0, 0.15f, 0);
//                         gameobj.transform.localScale = Vector3.one;
//                         gameobj.name = hero.m_strModel;
//
//                         (m_lstGfxObj[teamPos])[pos] = new GfxObject(gameobj);
//                         (m_lstGfxObj[teamPos])[pos].Initialize();
//
//                         m_lstAsncModelObj.Add(gameobj);
//                     }
//                     else
//                     {
//                         Debug.LogError("none  " + str);
//                     }
//                 }
//                 else
//                 {
//                     if (obj != null)
//                     {
//                         ResourceMgr.UnRequestAssetBundle(str);
//                     }
//                 }
//
//             }));

//            m_lstOwners.Add(owner);
            m_lstTeamNums.Add(teamNum);
            m_lstResName.Add(hero.m_strModel);
        }
        else
        {
            ha.ShowAttributes(uiPos, false);
        }
    }

    /// <summary>
    /// 移除旧的队伍加载中的，将其卸载
    /// </summary>
    /// <param name="teamNum"></param>
    private void RemoveOldTeamLoading()
    {
        //删除旧加载
        for (int i = 0; i < m_lstTeamNums.Count; i++)
        {
			ResourceMgr.UnloadResource(m_lstResName[i], m_lstOwners[i]);
            m_lstOwners.RemoveAt(i);
            m_lstResName.RemoveAt(i);
            m_lstTeamNums.RemoveAt(i);
            i--;
        }
    }
}
