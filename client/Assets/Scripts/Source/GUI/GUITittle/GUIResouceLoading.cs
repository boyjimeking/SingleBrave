using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Resource;
using Game.Base;
using System.IO;

//  GUIResourceLoading.cs
//  Author: Lu Zexi
//  2013-11-19




/// <summary>
/// GUI资源加载等待界面
/// </summary>
public class GUIResouceLoading : GUIBase
{
    private const string RES_MAIN = "_GUI_ResourceLoading"; //主资源
    private const string LABEL_PATH = "font";   //文本地址

    private UILabel m_cLabel;   //文本
    private float m_fStartTime; //开始时间
    private const float WAIT_TIME = 1;  //加载完毕后等待时间

    private List<string> m_lstStreamAsset = new List<string>(); //Stream资源名
    private List<string> m_lstStreamResourceName = new List<string>();  //stream资源名

    private int m_iLoadIndex;   //加载索引
    private LOAD_STATE m_eState;    //状态

    enum LOAD_STATE
    { 
        START = 0,  //开始
        RES_TABLE_START,    //资源表加载开始
        RES_TABLE,  //资源表加载
        RES_TABLE_END,  //资源表加载结束
        RES_LOADING_START,  //资源加载开始
        RES_LOADING,    //资源加载
        RES_LOAD_END,    //资源加载结束
        RES_LOAD_STREAM1,    //本地数据流加载开始
        RES_LOAD_STREAM2,    //本地数据流加载
        RES_LOAD_STREAM3,    //本地数据流加载结束
        LOAD_TABLE_START,   //读取文本开始
        LOAD_TABLE, //读取文本数据
        LOAD_TABLE_END, //读取文本数据结束
        RES_WAIT_TIME_START,    //等待开始
        RES_WAIT_TIME,  //等待
        RES_WAIT_TIME_END,  //等待结束
        END,
    }

    public GUIResouceLoading(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_RESOURCE_LOADING, GUILAYER.GUI_BACKGROUND)
    {
        this.m_eLayer = GUILAYER.GUI_BACKGROUND;
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        base.Show();

        if (this.m_cGUIObject == null)
        {
            this.m_cGUIObject = GameObject.Instantiate(Resources.Load(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, LABEL_PATH);
        }

        this.m_eState = LOAD_STATE.START;
        this.m_cLabel.text = "0%";
        this.m_fStartTime = 0;
//        this.m_lstStreamAsset.Clear();

//        //List<FileInfo> lstFileInfo = new List<FileInfo>();
//#if UNITY_STANDALONE
//        //DirectoryInfo dirInfo = new DirectoryInfo(Application.streamingAssetsPath + "/win32");
//        this.m_lstStreamAsset.Add("file://"+Application.streamingAssetsPath+"/win32/GUI/gui.res");
//        this.m_lstStreamAsset.Add("file://"+Application.streamingAssetsPath+"/win32/Effect/effect.res");
//        this.m_lstStreamAsset.Add("file://"+Application.streamingAssetsPath+"/win32/Model/model.res");
//        this.m_lstStreamAsset.Add("file://"+Application.streamingAssetsPath+"/win32/Tex/tex.res");
//#elif UNITY_IPONE
//        //DirectoryInfo dirInfo = new DirectoryInfo(Application.streamingAssetsPath + "/ios");
//        this.m_lstStreamAsset.Add("file://"+Application.streamingAssetsPath+"/ios/GUI/gui.res");
//        this.m_lstStreamAsset.Add("file://"+Application.streamingAssetsPath+"/ios/Effect/effect.res");
//        this.m_lstStreamAsset.Add("file://"+Application.streamingAssetsPath+"/ios/Model/model.res");
//        this.m_lstStreamAsset.Add("file://"+Application.streamingAssetsPath+"/ios/Tex/tex.res");
//#elif UNITY_ANDROID
//        //"jar:file://"
//        //DirectoryInfo dirInfo = new DirectoryInfo(Application.streamingAssetsPath + "/android");
//        this.m_lstStreamAsset.Add("Application.streamingAssetsPath + "/android/Tex/tex.res");
//        this.m_lstStreamAsset.Add(Application.streamingAssetsPath + "/android/GUI/gui.res");
//        this.m_lstStreamAsset.Add(Application.streamingAssetsPath + "/android/Effect/effect.res");
//        this.m_lstStreamAsset.Add(Application.streamingAssetsPath + "/android/Model/model.res");
//#endif
//        this.m_lstStreamResourceName.Clear();
//        this.m_lstStreamResourceName.Add("tex");
//        this.m_lstStreamResourceName.Add("gui");
//        this.m_lstStreamResourceName.Add("effect");
//        this.m_lstStreamResourceName.Add("model");
//        ////Debug.Log("file://" + Application.streamingAssetsPath + "/win32");
//        //foreach (DirectoryInfo item in dirInfo.GetDirectories())
//        //{
//        //    foreach (FileInfo fitem in item.GetFiles("*.res"))
//        //    {
//        //        //lstFileInfo.Add(fitem);
//        //        this.m_lstStreamAsset.Add(fitem.FullName);
//        //        this.m_lstStreamResourceName.Add(fitem.Name.Split('.')[0]);
//        //    }
//        //}

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();
        //SetLocalPos(Vector3.one * 0xFFFFF);
        Destory();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        base.Update();

        if ( IsShow() )
        {
            switch (this.m_eState)
            {
                case LOAD_STATE.START:
                    this.m_eState++;
                    break;
                case LOAD_STATE.RES_TABLE_START:
                    this.m_eState++;
                    break;
                case LOAD_STATE.RES_TABLE:
                    float per = ResourcesManager.GetInstance().GetProgress()*100;
                    per = (float)Math.Round(per, 2);
                    this.m_cLabel.text =  per + "%\n资源验证中...";

                    if (per >= 100  && ResourcesManager.GetInstance().IsComplete() )
                    {
                        this.m_eState++;
                    }
                    break;
                case LOAD_STATE.RES_TABLE_END:
                    //TextAsset ta = (TextAsset)ResourcesManager.GetInstance().Load("res", RESOURCE_TYPE.WEB_TEXT_STR, "resource");
                    //ResTableManager.GetInstance().Load(ta.text);
                    string ta = (string)ResourcesManager.GetInstance().Load("res", RESOURCE_TYPE.WEB_TEXT_STR, "resource");
                    Debug.Log(ta);
                    ResTableManager.GetInstance().Load(ta);
                    long sumSize = 0;
                    for (int i = 0; i < ResTableManager.GetInstance().GetAll().Count; i++)
                    {
                        sumSize += ResTableManager.GetInstance().GetAll()[i].Length;
                    }
                    this.m_iLoadIndex = 0;
                    this.m_eState++;
                    break;
                case LOAD_STATE.RES_LOADING_START:
                    ResourcesManager.GetInstance().ClearProgress();
                    ResTable resTable = ResTableManager.GetInstance().GetAll()[this.m_iLoadIndex];
                    ResourcesManager.GetInstance().LoadResouce(resTable.Path, resTable.CRC, resTable.Version, resTable.ResName , null , Game.Resource.RESOURCE_TYPE.WEB_RESOURCES, Game.Resource.ENCRYPT_TYPE.NORMAL, DownLoadCallBack2);
                    //int version = PlayerPrefs.GetInt(resTable.ResName+"V");
                    //string md5 = PlayerPrefs.GetString(resTable.ResName + "MD5");
                    ////Debug.Log(md5 + " md5 --- " + resTable.MD5);
                    ////Debug.Log(resTable.ResName + " -- " + version);
                    //if (md5 != resTable.MD5)
                    //{
                    //    ResourcesManager.GetInstance().LoadResouce(resTable.Path, resTable.CRC, version + 1, resTable.ResName, Game.Resource.RESOURCE_TYPE.WEB_RESOURCES, Game.Resource.ENCRYPT_TYPE.NORMAL, DownLoadCallBack3, version + 1, resTable.MD5);
                    //}
                    //else
                    //{
                    //    ResourcesManager.GetInstance().LoadResouce(resTable.Path, resTable.CRC, version, resTable.ResName, Game.Resource.RESOURCE_TYPE.WEB_RESOURCES, Game.Resource.ENCRYPT_TYPE.NORMAL, DownLoadCallBack2, version, md5);
                    //}
                    this.m_eState++;
                    break;
                case LOAD_STATE.RES_LOADING:
                    per = ResourcesManager.GetInstance().GetProgress()*100;
                    per = (float)Math.Round(per, 2);
                    this.m_cLabel.text =  per + "%\n(" + (this.m_iLoadIndex+1) + "/" + ResTableManager.GetInstance().GetAll().Count + ")";
                    if (per >= 100 && ResourcesManager.GetInstance().IsComplete())
                    {
                        this.m_eState++;
                    }
                    break;
                case LOAD_STATE.RES_LOAD_END:
                    this.m_iLoadIndex++;
                    if (this.m_iLoadIndex >= ResTableManager.GetInstance().GetAll().Count)
                    {
                        this.m_eState++;
                        this.m_iLoadIndex = 0;
                    }
                    else
                    {
                        this.m_eState = LOAD_STATE.RES_LOADING_START;
                    }
                    break;
                case LOAD_STATE.RES_LOAD_STREAM1:
                    ResourcesManager.GetInstance().ClearProgress();
                    ResourcesManager.GetInstance().LoadResouce(GAME_DEFINE.RESOURCE_GUI_PATH + GAME_DEFINE.RESOURCE_SHARE + GAME_DEFINE.RESOURCE_POST, 0, -1, GAME_DEFINE.RESOURCE_SHARE,null, Game.Resource.RESOURCE_TYPE.WEB_RESOURCES, Game.Resource.ENCRYPT_TYPE.NORMAL, DownLoadCallBack2);
                    this.m_eState++;
                    break;
                case LOAD_STATE.RES_LOAD_STREAM2:
                    per = ResourcesManager.GetInstance().GetProgress()*100f;
                    per = (float)Math.Round(per, 2);
                    this.m_cLabel.text =  per + "%";
                    if (per >= 100 && ResourcesManager.GetInstance().IsComplete())
                    {
                        this.m_eState++;
                    }
                    break;
                case LOAD_STATE.RES_LOAD_STREAM3:
                    this.m_eState++;
                    break;
                //case LOAD_STATE.RES_LOAD_STREAM1:
                //    ResourcesManager.GetInstance().ClearProgress();
                //    if (this.m_lstStreamAsset.Count > this.m_iLoadIndex)
                //    {
                //        string streamName = this.m_lstStreamAsset[this.m_iLoadIndex];
                //        string streamResourceName = this.m_lstStreamResourceName[this.m_iLoadIndex];
                //        ResourcesManager.GetInstance().LoadResouce(streamName, 0, -1, streamResourceName, Game.Resource.RESOURCE_TYPE.WEB_RESOURCES, Game.Resource.ENCRYPT_TYPE.NORMAL, DownLoadCallBack2);
                //    }
                //    //for (int i = 0; i<this.m_lstStreamAsset.Count ; i++ )
                //    //{
                //    //    string streamName = this.m_lstStreamAsset[i];
                //    //    string streamResourceName = this.m_lstStreamResourceName[i];
                //    //    ResourcesManager.GetInstance().LoadResouce("file://" + streamName, 0, -1, streamResourceName, Game.Resource.RESOURCE_TYPE.WEB_RESOURCES, Game.Resource.ENCRYPT_TYPE.NORMAL, null);
                //    //}
                //    this.m_eState++;
                //    break;
                //case LOAD_STATE.RES_LOAD_STREAM2:
                //    per = ResourcesManager.GetInstance().GetProgress()*100;
                //    per = (float)Math.Round(per, 2);
                //    this.m_cLabel.text =  per + "%\n(" + (this.m_iLoadIndex+1) + "/" + this.m_lstStreamAsset.Count + ")";
                //    //this.m_cLabel.text = "正在加载本地资源...\n" + Math.Round(((this.m_iLoadIndex + 1)*100f/this.m_lstStreamAsset.Count) , 2) + "%";
                //    if (per >= 100 && ResourcesManager.GetInstance().IsComplete())
                //    {
                //        this.m_eState++;
                //    }
                //    break;
                //case LOAD_STATE.RES_LOAD_STREAM3:
                //    //this.m_eState++;
                //    this.m_iLoadIndex++;
                //    if (this.m_iLoadIndex >= this.m_lstStreamAsset.Count)
                //    {
                //        this.m_eState++;
                //    }
                //    else
                //    {
                //        this.m_eState = LOAD_STATE.RES_LOAD_STREAM1;
                //    }
                //    break;
                case LOAD_STATE.LOAD_TABLE_START:
                    ResourcesManager.GetInstance().ClearAsyncLoad();
                    this.m_cLabel.text = "正在拼命加载数据...";
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.GUEST_GIFT_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.HELP_PROJECT_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.HELP_TYPE_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.HERO_GROW_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.FRIEND_GIFTITEM_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.AI_TABLE_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ACTIVITY_MONSTER_TEAM_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ACTIVITY_MONSTER_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ACTIVITY_GATE_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ACTIVITY_DUNGEON_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ITEM_EVENT_ACTION);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ITEM_ACTION);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.HERO_SELL_MONEY_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ITEM_COMPOSITE_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ALTHLETICSEXP_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.GAME_SETTING);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.HERO_EXP_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.LEADER_SKILL_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.HERO_ATTACK_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.BBSKILL_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.BUILDING_EQUIP_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.BUILDING_ITEM_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.BUILDING_TIAN_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.BUILDING_SHAN_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.BUILDING_LIN_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.BUILDING_CHUAN_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.BUILDING_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ITEM_TABLE_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.WORLD_TABLE_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ROLE_EXP_TABLE_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.SP_MONSTER_TABLE_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.MONSTER_TEAM_TABLE_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.MONSTER_TABLE_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.HERO_TABLE_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.GATE_TABLE_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.DUNGEON_TABLE_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.AREA_TABLE_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.STRING_TABLE_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.GUIDE_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.STORY_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.PVP_WEEK_RANK_PATH);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.BATTLE_RECORD);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.ARENA_BATTLE_RECORD);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.LOGIN_REWARD);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.PLAYER_NAME);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.SENSITIVE_WORD);
                    ResourcesManager.GetInstance().LoadAsync(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.PRODUCTION_STAFF);
                    this.m_eState++;
                    break;
                case LOAD_STATE.LOAD_TABLE:
                    per = ResourcesManager.GetInstance().GetAsyncProcess() *100f;
                    this.m_cLabel.text = (float)Math.Round(per, 2) + "%\n正在拼命加载数据...";

                    if (per >= 100f)
                    {
                        this.m_eState++;
                    }
                    break;
                case LOAD_STATE.LOAD_TABLE_END:
#if GAME_TEST_LOAD
                    GameSettingTableManager.GetInstance().LoadText(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.GAME_SETTING) as string);
#else
                    //活动副本表
                    TextAsset txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.ACTIVITY_DUNGEON_PATH) as TextAsset;
                    if (txtAsset == null) Debug.LogError(" txtAsset is null.");
                    ActivityTableManager.GetInstance().LoadDungeonTable(txtAsset.text);
                    //活动关卡表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.ACTIVITY_GATE_PATH) as TextAsset;
                    ActivityTableManager.GetInstance().LoadGateTable(txtAsset.text);
                    //活动怪物表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.ACTIVITY_MONSTER_PATH) as TextAsset;
                    ActivityTableManager.GetInstance().LoadMonsterTable(txtAsset.text);
                    //活动怪物编队表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.ACTIVITY_MONSTER_TEAM_PATH) as TextAsset;
                    ActivityTableManager.GetInstance().LoadMonsterTeamTable(txtAsset.text);
                    //AI
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.AI_TABLE_PATH) as TextAsset;
                    AITableManager.GetInstance().Load(txtAsset.text);
                    //区域
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.AREA_TABLE_PATH) as TextAsset;
                    AreaTableManager.GetInstance().LoadText(txtAsset.text);
                    //竞技场经验列表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.ALTHLETICSEXP_PATH) as TextAsset;
                    AthleticsExpTableManager.GetInstance().LoadText(txtAsset.text);
                    //技能表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.BBSKILL_PATH) as TextAsset;
                    BBSkillTableManager.GetInstance().LoadText(txtAsset.text);
                    //建筑表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.BUILDING_PATH) as TextAsset;
                    BuildingTableManager.GetInstance().LoadTextBuilding(txtAsset.text);
                    //建筑川表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.BUILDING_CHUAN_PATH) as TextAsset;
                    BuildingTableManager.GetInstance().LoadTextChuan(txtAsset.text);
                    //建筑装备表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.BUILDING_EQUIP_PATH) as TextAsset;
                    BuildingTableManager.GetInstance().LoadTextEquip(txtAsset.text);
                    //建筑物品表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.BUILDING_ITEM_PATH) as TextAsset;
                    BuildingTableManager.GetInstance().LoadTextItem(txtAsset.text);
                    //建筑林表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.BUILDING_LIN_PATH) as TextAsset;
                    BuildingTableManager.GetInstance().LoadTextLin(txtAsset.text);
                    //建筑山表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.BUILDING_SHAN_PATH) as TextAsset;
                    BuildingTableManager.GetInstance().LoadTextShan(txtAsset.text);
                    //建筑田表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.BUILDING_TIAN_PATH) as TextAsset;
                    BuildingTableManager.GetInstance().LoadTextTian(txtAsset.text);
                    //普通副本表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.DUNGEON_TABLE_PATH) as TextAsset;
                    DungeonTableManager.GetInstance().LoadText(txtAsset.text);
                    //好友礼物表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.FRIEND_GIFTITEM_PATH) as TextAsset;
                    FriendGiftItemTableManager.GetInstance().LoadText(txtAsset.text);
                    //全局数据表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.GAME_SETTING) as TextAsset;
                    GameSettingTableManager.GetInstance().LoadText(txtAsset.text);
                    //关卡表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.GATE_TABLE_PATH) as TextAsset;
                    GateTableManager.GetInstance().LoadText(txtAsset.text);
                    //招待表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.GUEST_GIFT_PATH) as TextAsset;
                    GuestsAwardTableManager.GetInstance().LoadText(txtAsset.text);
                    //加载帮助类型表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.HELP_TYPE_PATH) as TextAsset;
                    HelpTableManager.GetInstance().LoadHelpTypeText(txtAsset.text);
                    //加载帮助项目表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.HELP_PROJECT_PATH) as TextAsset;
                    HelpTableManager.GetInstance().LoadHelpProjectText(txtAsset.text);
                    //英雄攻击表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.HERO_ATTACK_PATH) as TextAsset;
                    HeroAttackTableManager.GetInstance().LoadText(txtAsset.text);
                    //英雄成长经验曲线表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject( TABLE_DEFINE.HERO_EXP_PATH) as TextAsset;
                    HeroEXPTableManager.GetInstance().LoadText(txtAsset.text);
                    //英雄成长表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.HERO_GROW_PATH) as TextAsset;
                    HeroGrowTableManager.GetInstance().LoadText(txtAsset.text);
                    //英雄出售表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.HERO_SELL_MONEY_PATH) as TextAsset;
                    HeroSellMoneyTableManager.GetInstance().LoadText(txtAsset.text);
                    //英雄表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.HERO_TABLE_PATH) as TextAsset;
                    HeroTableManager.GetInstance().LoadText(txtAsset.text);
                    //物品合成表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.ITEM_COMPOSITE_PATH) as TextAsset;
                    ItemCompositeTableManager.GetInstance().LoadText(txtAsset.text);
                    //物品表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.ITEM_TABLE_PATH) as TextAsset;
                    ItemTableManager.GetInstance().LoadText(txtAsset.text);
                    //队长技能表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.LEADER_SKILL_PATH) as TextAsset;
                    LeaderSkillTableManager.GetInstance().LoadText(txtAsset.text);
                    //怪物表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.MONSTER_TABLE_PATH) as TextAsset;
                    MonsterTableManager.GetInstance().LoadMonsterGateTable(txtAsset.text);
                    //怪物编队表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.MONSTER_TEAM_TABLE_PATH) as TextAsset;
                    MonsterTableManager.GetInstance().LoadMonsterTeamTable(txtAsset.text);
                    //宝箱怪物表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.SP_MONSTER_TABLE_PATH) as TextAsset;
                    MonsterTableManager.GetInstance().LoadMonsterBaoxiangTable(txtAsset.text);
                    //角色经验表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.ROLE_EXP_TABLE_PATH) as TextAsset;
                    RoleExpTableManager.GetInstance().LoadText(txtAsset.text);
                    //字符串表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.STRING_TABLE_PATH) as TextAsset;
                    StringTableManager.GetInstance().Load(txtAsset.text);
                    //加载世界表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.WORLD_TABLE_PATH) as TextAsset;
                    WorldTableManager.GetInstance().LoadText(txtAsset.text);
                    //新手引导
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.GUIDE_PATH) as TextAsset;
                    GuideTableManager.GetInstance().LoadText(txtAsset.text);
                    //剧情表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.STORY_PATH) as TextAsset;
                    StoryTableManager.GetInstance().Load(txtAsset.text);
                    //周排名奖励
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.PVP_WEEK_RANK_PATH) as TextAsset;
                    PVPWeekRankTableManager.GetInstance().LoadText(txtAsset.text);
                    //战绩表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.BATTLE_RECORD) as TextAsset;
                    BattleRecordTableManager.GetInstance().LoadText(txtAsset.text);
                    //竞技场战绩表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.ARENA_BATTLE_RECORD) as TextAsset;
                    ArenaBattleRecordTableManager.GetInstance().LoadText(txtAsset.text);
                    //登录奖励表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.LOGIN_REWARD) as TextAsset;
                    LoginRewardTableManager.GetInstance().LoadText(txtAsset.text);
                    //玩家名称表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.PLAYER_NAME) as TextAsset;
                    PlayerNameTableManager.GetInstance().LoadText(txtAsset.text);
                    //敏感词汇表
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.SENSITIVE_WORD) as TextAsset;
                    PlayerNameSensitiveWordTableManager.GetInstance().LoadText(txtAsset.text);
                    //物品action
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.ITEM_ACTION) as TextAsset;
                    ActionManager.GetInstance().LoadText(txtAsset.text);
                    //action事件
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.ITEM_EVENT_ACTION) as TextAsset;
                    EventActionManager.GetInstance().LoadText(txtAsset.text);
                    //制作人员
                    txtAsset = ResourcesManager.GetInstance().GetAsyncObject(TABLE_DEFINE.PRODUCTION_STAFF) as TextAsset;
                    ProductionStaffTableManager.GetInstance().LoadText(txtAsset.text);
#endif
                    
                    this.m_eState++;
                    break;
                case LOAD_STATE.RES_WAIT_TIME_START:
                    ResourcesManager.GetInstance().ClearAsyncLoad();
                    ResourcesManager.GetInstance().UnloadResource(GAME_DEFINE.RESOURCE_TABLE_PATH);
                    Resources.UnloadUnusedAssets();
                    GC.Collect();
                    this.m_fStartTime = GAME_TIME.TIME_FIXED();
                    this.m_eState++;
                    break;
                case LOAD_STATE.RES_WAIT_TIME:
                    if (GAME_TIME.TIME_FIXED() - this.m_fStartTime >= WAIT_TIME)
                    {
                        this.m_eState++;
                    }
                    break;
                case LOAD_STATE.RES_WAIT_TIME_END:
                    this.m_eState++;
                    break;
                case LOAD_STATE.END:
                    GAME_DEFINE.Load();
                    Hiden();
                    SendAgent.SendPlayerInfoGetPktReq(GAME_SETTING.s_iUID );
                    //if (string.IsNullOrEmpty(GAME_SETTING.s_strUserName) || string.IsNullOrEmpty(GAME_SETTING.s_strPassWord))
                    //{
                    //    //自动注册
                    //    SendAgent.SendAccountAutoRegistReq();
                    //}
                    //else
                    //{
                    //    //登录
                    //    SendAgent.SendAccountLogin(GAME_SETTING.s_strUserName, GAME_SETTING.s_strPassWord);
                    //}
                    this.m_eState++;
                    break;
            }
        }

        return true;
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Destory();
    }


    /// <summary>
    /// 资源下载完成
    /// </summary>
    /// <param name="resName"></param>
    /// <param name="asset"></param>
    private void DownLoadCallBack2(string resName, object asset, object[] arg)
    {
        Debug.Log("ok 2 " + resName);
    }

    /// <summary>
    /// 资源下载完成
    /// </summary>
    /// <param name="resName"></param>
    /// <param name="asset"></param>
    private void DownLoadCallBack3(string resName, object asset, object[] arg)
    {
        Debug.Log("ok 3 " + resName);

        int version = (int)arg[0];
        string md5 = (string)arg[1];

        Debug.Log(version + " version");
        Debug.Log(md5 + " md5");

        PlayerPrefs.SetString(resName + "MD5", md5);
        PlayerPrefs.SetInt(resName + "V", version);
        PlayerPrefs.Save();
    }
}

