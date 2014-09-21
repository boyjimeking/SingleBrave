using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Resource;
using UnityEngine;

//战斗菜单-英雄情报
//Author sunyi
//2014-3-26
public class GUIBattleMenuHeroIntelligence : GUIBase
{
    private const string RES_MAIN = "GUI_BattleMenuHeroIntelligence";//主资源地址

    private const string HERO_ITEM1 = "HeroIntelligence/MonsterItem1";//队长英雄
    private const string HERO_ITEM2 = "HeroIntelligence/MonsterItem2";//英雄
    private const string HERO_ITEM3 = "HeroIntelligence/MonsterItem3";//英雄
    private const string HERO_ITEM4 = "HeroIntelligence/MonsterItem4";//英雄
    private const string HERO_ITEM5 = "HeroIntelligence/MonsterItem5";//英雄
    private const string HERO_ITEM6 = "HeroIntelligence/MonsterItem6";//战友英雄

    private const string ITEM_BG = "ItemBg";//item中英雄属性背景地址
    private const string ITEM_FRAME = "ItemFrame";//item中英雄属性框地址
    private const string ITEM_MONSTER = "ItemMonster";//item中英雄头像地址
    private const string ITEM_LV = "LabelBottom";//item中等级标签地址
    private const string FRIEND_SKILL_LABEL = "HeroIntelligence/FriendSkill/Lab_FriendSkill";//战友技能标签地址
    private const string FRIEND_SKILL_PARENT = "HeroIntelligence/FriendSkill";//战友技能父对象地址
    private const string SPR_UNFRIEND = "HeroIntelligence/Spr_UnFriend";//非好友地址
    private const string LEADER_SKILL_LABEL = "HeroIntelligence/Lab_LeaderSkill";//队长技能标签地址
    private const string FRIEND_SKILL_DESC_LABEL = "HeroIntelligence/FriendSkill/Lab_FriendDesc";//战友技能详细标签地址
    private const string LEADER_SKILL__DESC_LABEL = "HeroIntelligence/Lab_LeaderDesc";//队长技能详细标签地址
    private const string LAB_TEAM_INDEX = "HeroIntelligence/Lab_Index";//当前队伍标签地址

    private GameObject m_cHeroItem1;//英雄对象1
    private GameObject m_cHeroItem2;//英雄对象2
    private GameObject m_cHeroItem3;//英雄对象3
    private GameObject m_cHeroItem4;//英雄对象4
    private GameObject m_cHeroItem5;//英雄对象5
    private GameObject m_cHeroItem6;//英雄对象6

    private GameObject m_cFriendSkillParent;//好友技能父对象
    private GameObject m_cSprUnfriend;//非好友精灵对象
    private UILabel m_cLabFriendSkill;//战友技能标签
    private UILabel m_cLabFriendSkillDesc;//战友技能详细标签
    private UILabel m_cLabLeaderSkill;//队长技能标签
    private UILabel m_cLabLeaderSkillDesc;//队长技能描述标签

    private UILabel m_cLabTeamIndex;//当前队伍标签

    private List<int> m_lstTeamHeroTabelId = new List<int>();//当前队伍所有英雄tableid列表
    private List<int> m_lstTeamHeroLv = new List<int>();//当前队伍所有英雄等级列表

    private List<Hero> m_lstHeros = new List<Hero>();//当前队伍中的英雄列表
    private List<GameObject> m_lstHeroItems = new List<GameObject>();//英雄对象列表

    public GUIBattleMenuHeroIntelligence(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_BATTLE_MENU_HERO_INTELLIGENCE, UILAYER.GUI_PANEL3)
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

            this.m_cLabFriendSkill = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, FRIEND_SKILL_LABEL);
            this.m_cLabFriendSkillDesc = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, FRIEND_SKILL_DESC_LABEL);
            this.m_cLabLeaderSkill = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LEADER_SKILL_LABEL);
            this.m_cLabLeaderSkillDesc = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LEADER_SKILL__DESC_LABEL);

            this.m_cHeroItem1 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, HERO_ITEM1);
            this.m_cHeroItem2 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, HERO_ITEM2);
            this.m_cHeroItem3 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, HERO_ITEM3);
            this.m_cHeroItem4 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, HERO_ITEM4);
            this.m_cHeroItem5 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, HERO_ITEM5);
            this.m_cHeroItem6 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, HERO_ITEM6);

            this.m_cLabTeamIndex = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LAB_TEAM_INDEX);

            this.m_cFriendSkillParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject,FRIEND_SKILL_PARENT);
            this.m_cSprUnfriend = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, SPR_UNFRIEND);
        }

        ShowHeroIntelligence();

        SetLocalPos(Vector3.zero);
    }

    private void ShowHeroIntelligence()
    {
        Hero leader = Role.role.GetHeroProperty().GetHeroByTableId(this.m_lstTeamHeroTabelId[0]);
        HeroTable friend = HeroTableManager.GetInstance().GetHeroTable(this.m_lstTeamHeroTabelId[this.m_lstTeamHeroTabelId.Count - 1]);
        //队长技能
        LeaderSkillTable ldSkill = LeaderSkillTableManager.GetInstance().GetLeaderSkillTable(leader.m_iLeaderSkillID);
        if (ldSkill == null)
        {
            this.m_cLabLeaderSkill.text = "";
            this.m_cLabLeaderSkillDesc.text = "";
        }
        else
        {
            this.m_cLabLeaderSkill.text = ldSkill.Name;
            this.m_cLabLeaderSkillDesc.text = ldSkill.Desc;
        }

        if (GLOBAL_DEFINE.m_cSelectBattleFriend.m_bIsFriend)
        {
            this.m_cFriendSkillParent.SetActive(true);
            this.m_cSprUnfriend.SetActive(false);
        }
        else {
            this.m_cFriendSkillParent.SetActive(false);
            this.m_cSprUnfriend.SetActive(true);
        }

        //好友技能
        LeaderSkillTable ldSkill2 = LeaderSkillTableManager.GetInstance().GetLeaderSkillTable(friend.LeaderSkill);
        if (ldSkill2 == null)
        {

            this.m_cLabFriendSkill.text = "";
            this.m_cLabFriendSkillDesc.text = "";
        }
        else
        {
            this.m_cLabFriendSkill.text = ldSkill2.Name;
            this.m_cLabFriendSkillDesc.text = ldSkill2.Desc;
        }

        this.m_lstHeroItems.Add(this.m_cHeroItem1);
        this.m_lstHeroItems.Add(this.m_cHeroItem2);
        this.m_lstHeroItems.Add(this.m_cHeroItem3);
        this.m_lstHeroItems.Add(this.m_cHeroItem4);
        this.m_lstHeroItems.Add(this.m_cHeroItem5);
        this.m_lstHeroItems.Add(this.m_cHeroItem6);

        this.m_cLabTeamIndex.text = "队伍" + Role.role.GetBaseProperty().m_iCurrentTeam;

        //英雄排序
        for (int i = 0; i < this.m_lstTeamHeroTabelId.Count; i++)
        {
            int index = 0;
            if (this.m_lstTeamHeroTabelId[i] == -1)
            {
                this.m_lstTeamHeroTabelId.RemoveAt(i);
                this.m_lstHeroItems[this.m_lstHeroItems.Count - 2].SetActive(false);
                this.m_lstHeroItems.RemoveAt(this.m_lstHeroItems.Count - 2);
                this.m_lstTeamHeroLv.RemoveAt(i);
                index++;
                i--;
            }
        }
        for (int i = 0; i < this.m_lstTeamHeroTabelId.Count; i++)
        {
            GameObject heroItem = this.m_lstHeroItems[i];

            UISprite sprItemBg = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(heroItem, ITEM_BG);
            UISprite sprItemFrame = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(heroItem, ITEM_FRAME);
            UISprite sprItemMonster = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(heroItem, ITEM_MONSTER);
            UILabel lvLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(heroItem, ITEM_LV);
            HeroTable hero = HeroTableManager.GetInstance().GetHeroTable(this.m_lstTeamHeroTabelId[i]);
            GUI_FUNCTION.SET_HeroBorderAndBack(sprItemFrame, sprItemBg, (Nature)hero.Property);
            GUI_FUNCTION.SET_AVATORS(sprItemMonster, hero.AvatorMRes);

            lvLabel.text = "LV." + this.m_lstTeamHeroLv[i];
        }
    }

    public void SetListHeros(List<int> lstTeamHeroTabelId, List<int> lstTeamHeroLv)
    {
        this.m_lstTeamHeroTabelId = new List<int>(lstTeamHeroTabelId);
        this.m_lstTeamHeroLv = new List<int>(lstTeamHeroLv);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        ResourceMgr.UnloadResource(RES_MAIN);

        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        this.m_cHeroItem1 = null;
        this.m_cHeroItem2 = null;
        this.m_cHeroItem3 = null;
        this.m_cHeroItem4 = null;
        this.m_cHeroItem5 = null;
        this.m_cHeroItem6 = null;

        this.m_cLabFriendSkill = null;
        this.m_cLabFriendSkillDesc = null;
        this.m_cLabLeaderSkill = null;
        this.m_cLabLeaderSkillDesc = null;

        if (this.m_lstHeroItems != null)
        {
            foreach (GameObject obj in this.m_lstHeroItems)
            {
                GameObject.Destroy(obj);
            }

            this.m_lstHeroItems.Clear();
        }

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
}

