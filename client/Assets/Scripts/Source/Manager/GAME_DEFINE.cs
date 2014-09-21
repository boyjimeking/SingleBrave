using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


//  GAME_DEFINE.cs
//  Author: Lu Zexi
//  2013-11-19




/// <summary>
/// 游戏定义类
/// </summary>
public class GAME_DEFINE
{
    public const float FPS_FIXED = 1/30f;  //固定帧

    public const int U3D_OBJECT_LAYER_UI = 0;  //U3D物体UI层级
    public const int U3D_OBJECT_LAYER_MODEL = 1;   //U3D物体模型层级

	public const int TEAM_MAX_NUM = 5;  //队伍成员最大数量

    public static string RESOURCE_POST = ".res"; //后缀扩展名
    public static string RESOURCE_SHARE = "Share"; //共享名
    public const string RESOURCE_GUI_CACHE = "gui_cache";   //GUI缓存

    public static string DEVICE_ID = "";    //设备ID
    public static string CHANNEL_NAME = ""; //渠道名字

#if UNITY_STANDALONE
    public static string RESOURCE_GUI_PATH = "file://"+Application.streamingAssetsPath + "/win32/"+"GUI/";    //GUI地址
    public static string RESOURCE_EFFECT_PATH = "/win32/" + "Effect/";    //特效地址
    public static string RESOURCE_TEX_PATH = "/win32/" + "Tex/";    //图片地址
    public static string RESOURCE_MODEL_PATH = "/win32/" + "Model/";    //模型地址
    public static string RESOURCE_RES_PATH = "/win32/" + "resource.txt";  //资源名
    public static string RESOURCE_AVATAR_PATH = "/win32/" + "AvatarM/";   //头像地址
    public static string RESOURCE_ITEM_PATH = "/win32/" + "Item/";    //物品地址
#elif UNITY_IPHONE
    public static string RESOURCE_GUI_PATH = "file://" + Application.streamingAssetsPath + "/ios/" + "GUI/";    //GUI地址
    public static string RESOURCE_EFFECT_PATH = "/ios/" + "Effect/";    //特效地址
    public static string RESOURCE_TEX_PATH = "/ios/" + "Tex/";    //图片地址
    public static string RESOURCE_MODEL_PATH = "/ios/" + "Model/";    //模型地址
    public static string RESOURCE_RES_PATH = "/ios/" + "resource.txt";  //资源名
    public static string RESOURCE_AVATAR_PATH = "/ios/" + "AvatarM/";   //头像地址
    public static string RESOURCE_ITEM_PATH = "/ios/" + "Item/";    //物品地址
#elif UNITY_ANDRO
    public static string RESOURCE_GUI_PATH = Application.streamingAssetsPath + "/android/"+"GUI/";    //GUI地址
    public static string RESOURCE_EFFECT_PATH = "/android/" + "Effect/";    //特效地址
    public static string RESOURCE_TEX_PATH = "/android/" + "Tex/";    //图片地址
    public static string RESOURCE_MODEL_PATH = "/android/" + "Model/";    //模型地址
    public static string RESOURCE_RES_PATH = "/android/" + "resource.txt";  //资源名
    public static string RESOURCE_AVATAR_PATH = "/android/" + "AvatarM/";   //头像地址
    public static string RESOURCE_ITEM_PATH = "/android/" + "Item/";    //物品地址
#endif
    public static string RESOURCE_TABLE_PATH = "table";  //配置表资源地址
    public static string RES_PATH = ""; //资源地址
    public static int RES_VERSION = 0;  //资源版本

    public const float FADEOUT_GUI_TIME = 0.3f;   //GUI渐进时间
    public const float FADEIN_GUI_TIME = 0.2f;   //GUI渐出时间

    //public const int MAX_BB_HP = 100;   //BBHP最大值

    public static long m_lServerTime;
    public static long m_lTimeSpan;  //服务器客户端时间偏移量
    public static string s_strToken;    //token
    public const string SERVER_KEY = "sanguo_youqingkeji";  //校验KEY

    //客户端参数
    public static float CRITICAL;   //基础暴击率
    public static float BBRecoverRate;  //BB回复系数
    public static float XinRecoverRate; //心回复系数
    public static float ShuiJingRecover;    //水晶回复值
    public static float XURUORate;    //虚弱减益系数
    public static float POJIARate;  //破甲减益系数
    public static float PORENRate;  //破刃减益系数
    public static float DEBUFFDuRate;   //减益BUFF毒系数
    public static float GATEHeartRate;  //关卡心掉落率
    public static int GATEHeartNum; //关卡心掉落最大个数
    public static float GATEShuijingRate;   //关卡水晶掉落率
    public static int GATEShuijingMinNum;  //关卡水晶掉落最大个数
    public static int GATEShuijingMaxNum;  //关卡水晶掉落最大个数
    public static float PVPHeartRate;  //关卡心掉落率
    public static int PVPHeartNum; //PVP心掉落最大个数
    public static float PVPShuijingRate;   //PVP水晶掉落率
    public static int PVPShuijingMinNum;  //PVP水晶掉落最大个数
    public static int PVPShuijingMaxNum;  //PVP水晶掉落最大个数
    public static int DiamondStrenthCost;//体力全恢复使用的钻石数量
    public static int DiamondSportPointCost;//竞技点全恢复使用的钻石数量
    public static int DiamondUnitSlotCost;//增加单位曹使用的钻石数量
    public static int DiamondPropsSlotCost;//增加道具槽使用的钻石数量
    public static int DiamondRelive;    //砖石复活数量
    public static int UnitSlotAddPerDiamond;//使用钻石能够增加单位槽的数量
    public static int PropsSlotAddPerDiamond;//使用钻石能够增加道具槽的数量
    public static int DiamondSummon;    //砖石抽卡消费砖石数
    public static int FriendSummon; //友情抽卡消费点
    public static int GuideSkillStep;   //新手引导技能步骤点
    public static int GuideDefenceStep; //新手引导防御技能点
    public static string NOTIC_URL = "";    //公告地址

    public static int[] m_iEvolutionSpent;    //主角进化消耗
    public static int[] m_iOtherEvolutionSpent;    //其他进化消耗
    public static int[] m_vecEvolutionHeroID;    //主角英雄ID
    public static int[] m_vecSelectHero;    //可选择英雄ID




    /// <summary>
    /// 加载数据
    /// </summary>
    public static void Load()
    {
        CRITICAL = GameSettingTableManager.GetInstance().GetFloat("Critical");
        BBRecoverRate = GameSettingTableManager.GetInstance().GetFloat("BBRecoverRate");
        XinRecoverRate = GameSettingTableManager.GetInstance().GetFloat("XinRecoverRate");
        ShuiJingRecover = GameSettingTableManager.GetInstance().GetFloat("ShuiJingRecover");
        XURUORate = GameSettingTableManager.GetInstance().GetFloat("XURUORate");
        POJIARate = GameSettingTableManager.GetInstance().GetFloat("POJIARate");
        PORENRate = GameSettingTableManager.GetInstance().GetFloat("PORENRate");
        DEBUFFDuRate = GameSettingTableManager.GetInstance().GetFloat("DEBUFFDuRate");
        GATEHeartRate = GameSettingTableManager.GetInstance().GetFloat("GATEHeartRate");
        GATEHeartNum = GameSettingTableManager.GetInstance().GetInt("GATEHeartNum");
        GATEShuijingRate = GameSettingTableManager.GetInstance().GetFloat("GATEShuijingRate");
        string[] vecTmp = GameSettingTableManager.GetInstance().GetString("GATEShuijingNum").Split('|');
        GATEShuijingMinNum = int.Parse(vecTmp[0]);
        GATEShuijingMaxNum = int.Parse(vecTmp[1]);
        PVPHeartRate = GameSettingTableManager.GetInstance().GetFloat("PVPHeartRate");
        PVPHeartNum = GameSettingTableManager.GetInstance().GetInt("PVPHeartNum");
        PVPShuijingRate = GameSettingTableManager.GetInstance().GetFloat("PVPShuijingRate");
        vecTmp = GameSettingTableManager.GetInstance().GetString("PVPShuijingNum").Split('|');
        DiamondStrenthCost = GameSettingTableManager.GetInstance().GetInt("Diamond_Strenth_Cost");
        DiamondSportPointCost = GameSettingTableManager.GetInstance().GetInt("Diamond_SportPoint_Cost");
        DiamondUnitSlotCost = GameSettingTableManager.GetInstance().GetInt("Diamond_Unit_Slot_Cost");
        DiamondPropsSlotCost = GameSettingTableManager.GetInstance().GetInt("Diamond_Props_Slot_Cost");
        DiamondRelive = GameSettingTableManager.GetInstance().GetInt("Diamond_Relive");
        UnitSlotAddPerDiamond = GameSettingTableManager.GetInstance().GetInt("Unit_Slot_Add");
        PropsSlotAddPerDiamond = GameSettingTableManager.GetInstance().GetInt("Props_Slot_Add");
        DiamondSummon = GameSettingTableManager.GetInstance().GetInt("Diamond_Summon");
        FriendSummon = GameSettingTableManager.GetInstance().GetInt("Friend_Summon");
        PVPShuijingMinNum = int.Parse(vecTmp[0]);
        PVPShuijingMaxNum = int.Parse(vecTmp[1]);
        GuideSkillStep = GameSettingTableManager.GetInstance().GetInt("GuideSkillStep");
        GuideDefenceStep = GameSettingTableManager.GetInstance().GetInt("GuideDefenceStep");
        NOTIC_URL = GameSettingTableManager.GetInstance().GetString("Notice_Path");

        string[] tmp = GameSettingTableManager.GetInstance().GetString("Leading_role").Split('|');
        m_vecEvolutionHeroID = new int[tmp.Length];
        for (int i = 0; i < m_vecEvolutionHeroID.Length; i++)
        {
            m_vecEvolutionHeroID[i] = int.Parse(tmp[i].Trim());
        }

        string[] spent = GameSettingTableManager.GetInstance().GetString("Leading_role_evo_cost").Split('|');
        m_iEvolutionSpent = new int[spent.Length];
        for (int i = 0; i < m_iEvolutionSpent.Length; i++)
        {
            m_iEvolutionSpent[i] = int.Parse(spent[i]);
        }

        string[] otherSpent = GameSettingTableManager.GetInstance().GetString("Evo_cost").Split('|');
        m_iOtherEvolutionSpent = new int[otherSpent.Length];
        for (int i = 0; i < m_iOtherEvolutionSpent.Length; i++)
        {
            m_iOtherEvolutionSpent[i] = int.Parse(otherSpent[i]);
        }

        string[] selectHeroStr = GameSettingTableManager.GetInstance().GetString("Player_Select_Hero").Split('|');
        m_vecSelectHero = new int[4];
        for (int i = 0; i < m_vecSelectHero.Length; i++)
        {
            m_vecSelectHero[i] = int.Parse(selectHeroStr[i].Split(':')[0]);
        }
    }
}

