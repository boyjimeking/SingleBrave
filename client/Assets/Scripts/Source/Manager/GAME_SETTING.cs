
using System.Collections.Generic;
using UnityEngine;

//  GAME_SETTING.cs
//  Author: Lu Zexi
//  2013-12-04




/// <summary>
/// 游戏参数
/// </summary>
public class GAME_SETTING
{
    //游戏设置
	public const string SESSION_LOGIN_PATH = "http://sanguo.luzexi.com/index.php?r=";  //内网测试地址
    //public const string SESSION_LOGIN_PATH = "http://192.168.1.250/sanguo/server/service/index.php?r=";  //内网测试地址
    //public const string SESSION_LOGIN_PATH = "http://test.sanguo.youqingkeji.com/sanguo/index.php?r=";  //外网测试地址
    //public const string SESSION_LOGIN_PATH = "http://pp.ios.youqingkeji.com/sanguo/index.php?r=";  //外网PP测试地址
    
    public const int VERSION = 7;  //版本号
    public static bool s_bSKEffectSwitch; //技能特效开关
    private const string SKEFFECT_SWITCH = "SKEFFECT_SWITCH";   //技能特效关键词
    public static bool s_bATEffectSwitch;   //攻击特效开关
    private const string ATEFFECT_SWITCH = "ATEFFECT_SWITCH";   //攻击特效关键词
    public static bool s_bENEffectSwitch;   //环境特效开关
    private const string ENEFFECT_SWITCH = "ENEFFECT_SWITCH"; //环境特效开关
    public static float s_fBGM_Volume;  //背景音乐大小
    private const string BGM_VOLUME = "BGM_VOLUME";  //背景音乐关键词
    public static float s_fSE_Volume;   //音乐大小
    private const string SE_VOLUME = "SE_VOLUME"; //音乐关键词
    public static string s_strUserName; //用户名
    public const string USER_NAME = "UserName"; //用户名关键词
    public static string s_strPassWord; //密码
    public const string PASSWORD = "PassWord";//密码关键词
    public static int s_iUID;   //帐号ID
    public const string ACCOUNT_BOUND = "Account_Bound";    //帐号绑定
    public static bool s_bAccountBound;    //帐号是否已经绑定
    public static string CODEHASVIEW = "CodeHasView";  //是否显示过邀请码
    public static bool s_bCodeHasView;   //是否显示过邀请码
    public static string FIRSTCODESHOW = "FirstCodeShow"; //第一次进入提示输入推荐邀请码
    public static bool s_bFirstCodeShow;    //邀请码是否开启
    public const string BATTLE_ITEM_AUTO_FULL = "Battle_Item_Auto_Full";  //战斗准备自动填充
    public static bool s_bBattleAutoFull;   //自动填充
    public static string NEW_DUNGEON_OF_NEW_AREA = "New_Dungeon_Of_New_Area";//第一次开启新区域
    public static bool s_bNewDungeonOfNewArea;//第一次开启新区域
    public static SORT_TYPE m_eSortType;
    public const string HERO_SORT_TYPE = "HEROSORT";
    public static string LAST_LOGIN_DAYS = "Last_Login_Days";//上次持续登录天数
    public static int s_iLastLoginDays;//上次持续登录天数
    public static string EQUIPMENT_NEW = "EQUIPNEW";//上次未浏览的装备
    public static string LAST_EQUIPMENT_LIST; //上次的可制作装备列表
    public static Dictionary<int, int> EQUIPMENT_LIST;
    public static string ITEM_NEW = "ITEMNEW"; //上次未浏览过的药品
    public static string LAST_ITEM_LIST; //上次的可制作药品列表
    public static Dictionary<int, int> ITEM_LIST;
    public const string GAME_JOIN_STR = "GAME_JOIN";   //游戏加入字段
    public static bool GAME_JOIN;  //是否曾加入游戏
    public const string DEVICE_ID_STR = "DEVICE_ID";    //设备ID
    public static string DEVICE_ID = "";   //设备ID
    public const string WARN_HERO_JINHUA = "Warn_hero_Jinhua";  //有新装备，新可以进化英雄，新解锁药物和装备需要提示信息
    public const string WARN_HERO_EQUIP = "Warn_hero_equip";  //有新装备，新可以进化英雄，新解锁药物和装备需要提示信息
    public const string WARN_HOUSE_TIAOHE = "Warn_house_tiaohe";  //有新装备，新可以进化英雄，新解锁药物和装备需要提示信息
    public const string WARN_HOUSE_EQUIP = "Warn_house_equip";  //有新装备，新可以进化英雄，新解锁药物和装备需要提示信息
    public static Dictionary<int,int> s_dicWarnHeroJinhua = new Dictionary<int,int> ();  // 记录提示过得英雄ID与标志位：0不展示1需要展示
    public static string HeroJinhua;//可进化的英雄提示
    public static int s_iWarnHeroEquip;//0不展示1需要展示
    public static int s_iWarnHouseTiaohe;//0不展示1需要展示
    public static int s_iWarnHouseEquip;//0不展示1需要展示
    public const string EQUIPHOUSELEVELUP = "EQUIPHOUSELEVELUP";//者装备屋等级提高
    public static bool s_bEquipLevelUp; //装备屋等级提升后是否进入过装备屋界面
    public const string ITEMHOUSELEVELUP = "ITEMHOUSELEVELUP";//调和屋等级提高
    public static bool s_bItemLevelUp;//调和屋等级提升后是否进入过调和屋界面
    public const string EQUIPLEVELADD = "EQUIPLEVELADD";//装备屋等级差
    public static int s_iEquipLevelAdd;
    public const string ITEMLEVELADD = "ITEMLEVELADD";//调和屋等级差
    public static int s_iItemLevelAdd;
    //public static List<int> s_iWarnHouseTiaohe;//0不展示1需要展示
    //public static List<int> s_iWarnHouseEquip;//0不展示1需要展示
    public const string ISOVER = "ISOVER";//是否通关所有副本
    public static int s_iIsOver; //0未通关1通关


    /// <summary>
    /// 加载设置
    /// </summary>
    public static void LoadSetting()
    {
        //帐号
        s_strUserName = PlayerPrefs.GetString(USER_NAME);
        s_strPassWord = PlayerPrefs.GetString(PASSWORD);
        s_bAccountBound = PlayerPrefs.GetInt(ACCOUNT_BOUND) > 0;

        //s_strUserName = "";
        //s_strPassWord = "";

        //特效
        if (PlayerPrefs.HasKey(SKEFFECT_SWITCH))
            s_bSKEffectSwitch = PlayerPrefs.GetInt(SKEFFECT_SWITCH) > 0 ? true : false;
        else
            s_bSKEffectSwitch = true;
        if (PlayerPrefs.HasKey(ATEFFECT_SWITCH))
            s_bATEffectSwitch = PlayerPrefs.GetInt(ATEFFECT_SWITCH) > 0 ? true : false;
        else
            s_bATEffectSwitch = true;
        if (PlayerPrefs.HasKey(ENEFFECT_SWITCH))
            s_bENEffectSwitch = PlayerPrefs.GetInt(ENEFFECT_SWITCH) > 0 ? true : false;
        else
            s_bENEffectSwitch = true;

        //音乐
        if (PlayerPrefs.HasKey(BGM_VOLUME))
            s_fBGM_Volume = PlayerPrefs.GetFloat(BGM_VOLUME);
        else
            s_fBGM_Volume = 1f;
        if (PlayerPrefs.HasKey(SE_VOLUME))
            s_fSE_Volume = PlayerPrefs.GetFloat(SE_VOLUME);
        else
            s_fSE_Volume = 1f;

        //是否显示过邀请码
        if (PlayerPrefs.HasKey(CODEHASVIEW))
            s_bCodeHasView = (PlayerPrefs.GetInt(CODEHASVIEW) == 1);
        else
            s_bCodeHasView = false;

        //是否第一次展示过输入推荐邀请码
        if (PlayerPrefs.HasKey(FIRSTCODESHOW))
            s_bFirstCodeShow = (PlayerPrefs.GetInt(FIRSTCODESHOW) == 1);
        else
            s_bFirstCodeShow = false;

        //是否自动填充战斗物品
        if (PlayerPrefs.HasKey(BATTLE_ITEM_AUTO_FULL))
            s_bBattleAutoFull = (PlayerPrefs.GetInt(BATTLE_ITEM_AUTO_FULL) == 1);
        else
            s_bBattleAutoFull = false;

        if (PlayerPrefs.HasKey(NEW_DUNGEON_OF_NEW_AREA))
            s_bNewDungeonOfNewArea = (PlayerPrefs.GetInt(NEW_DUNGEON_OF_NEW_AREA) == 1);
        else
            s_bNewDungeonOfNewArea = false;


        //英雄排序记录
        if (PlayerPrefs.HasKey(HERO_SORT_TYPE))
            m_eSortType = (SORT_TYPE)PlayerPrefs.GetInt(HERO_SORT_TYPE);
        else
            m_eSortType = SORT_TYPE.NEW_OLD;

        if (PlayerPrefs.HasKey(LAST_LOGIN_DAYS))
        {
            s_iLastLoginDays = PlayerPrefs.GetInt(LAST_LOGIN_DAYS);
        }

        //装备制作记录
        EQUIPMENT_LIST = new Dictionary<int, int>();
        if (PlayerPrefs.HasKey(EQUIPMENT_NEW))
        {
            LAST_EQUIPMENT_LIST = PlayerPrefs.GetString(EQUIPMENT_NEW);
            char[] ch = { ',', ';' };
            string[] temp = LAST_EQUIPMENT_LIST.Split(ch);
            for (int i = 0; i < temp.Length / 2; ++i)
            {
                EQUIPMENT_LIST.Add(int.Parse(temp[2 * i]), int.Parse(temp[2 * i + 1]));
            }
        }
        else
        {
            LAST_EQUIPMENT_LIST = null;
        }

        //药品制作记录
        ITEM_LIST = new Dictionary<int, int>();
        if (PlayerPrefs.HasKey(ITEM_NEW))
        {
            LAST_ITEM_LIST = PlayerPrefs.GetString(ITEM_NEW);
            char[] ch = { ',', ';' };
            string[] temp = LAST_ITEM_LIST.Split(ch);
            for (int i = 0; i < temp.Length / 2; ++i)
            {
                ITEM_LIST.Add(int.Parse(temp[2 * i]), int.Parse(temp[2 * i + 1]));
            }

        }
        else
        {
            LAST_ITEM_LIST = null;
        }

        GAME_JOIN = PlayerPrefs.GetInt(GAME_JOIN_STR) > 0 ? true : false;
        DEVICE_ID = PlayerPrefs.GetString(DEVICE_ID_STR);

        //有新装备
        if (PlayerPrefs.HasKey(WARN_HERO_EQUIP))
        {
            s_iWarnHeroEquip = PlayerPrefs.GetInt(WARN_HERO_EQUIP);
        }
        else
        {
            s_iWarnHeroEquip = 0;
        }
        //新可以进化英雄
        if (PlayerPrefs.HasKey(WARN_HERO_JINHUA))
        {
            HeroJinhua = PlayerPrefs.GetString(WARN_HERO_JINHUA);
            char[] ch = { ',',';'};
            string[] temp = HeroJinhua.Split(ch);
            for (int i = 0; i < temp.Length / 2; ++i)
            {
                s_dicWarnHeroJinhua.Add(int.Parse(temp[2 * i]), int.Parse(temp[2 * i + 1]));
            }
        }
        else
        {
            HeroJinhua = null;
        }
        //新解锁装备
        if (PlayerPrefs.HasKey(WARN_HOUSE_EQUIP))
        {
            s_iWarnHeroEquip = PlayerPrefs.GetInt(WARN_HOUSE_EQUIP);
        }
        else
        {
            s_iWarnHouseEquip = 1;
        }
        //新解锁药物
        if (PlayerPrefs.HasKey(WARN_HOUSE_TIAOHE))
        {
            s_iWarnHouseTiaohe = PlayerPrefs.GetInt(WARN_HOUSE_TIAOHE);
        }
        else
        {
            s_iWarnHouseTiaohe = 1;
        }
        //装备屋浏览记录
        if (PlayerPrefs.HasKey(EQUIPHOUSELEVELUP))
        {
            s_bEquipLevelUp = (PlayerPrefs.GetInt(EQUIPHOUSELEVELUP) == 1);
        }
        else
            s_bEquipLevelUp = true;
        //调和屋浏览记录
        if (PlayerPrefs.HasKey(ITEMHOUSELEVELUP))
        {
            s_bItemLevelUp = (PlayerPrefs.GetInt(ITEMHOUSELEVELUP) == 1);
        }
        else
            s_bItemLevelUp = true;
        //装备等级差
        if(PlayerPrefs.HasKey(EQUIPLEVELADD))
        {
            s_iEquipLevelAdd = PlayerPrefs.GetInt(EQUIPLEVELADD);
        }
        else
            s_iEquipLevelAdd = 0;
        //调和屋等级差
        if(PlayerPrefs.HasKey(ITEMLEVELADD))
        {
            s_iItemLevelAdd = PlayerPrefs.GetInt(ITEMLEVELADD);
        }
        else
            s_iItemLevelAdd = 0;
        //是否通关
        if (PlayerPrefs.HasKey(ISOVER))
        {
            s_iIsOver = PlayerPrefs.GetInt(ISOVER);
        }
        else
            s_iIsOver = 0;
    }

    /// <summary>
    /// 设置英雄排序
    /// </summary>
    /// <param name="type"></param>
    public static void SetHeroSort(SORT_TYPE type)
    {
        PlayerPrefs.SetInt(HERO_SORT_TYPE, (int)type);
    }

    /// <summary>
    /// 设置是否看过邀请码
    /// </summary>
    /// <param name="viewed"></param>
    public static void SaveCodeHasView(bool viewed)
    {
        PlayerPrefs.SetInt(CODEHASVIEW, viewed ? 1 : 0);
        s_bCodeHasView = viewed;
    }

    /// <summary>
    /// 设置是否第一次展示过输入推荐邀请码
    /// </summary>
    /// <param name="viewed"></param>
    public static void SaveFirstCodeShow(bool viewed)
    {
        PlayerPrefs.SetInt(FIRSTCODESHOW, viewed ? 1 : 0);
        s_bFirstCodeShow = viewed;
    }

    /// <summary>
    /// 设置战斗物品自动填满
    /// </summary>
    /// <param name="ok"></param>
    public static void SaveBattelAutoFull(bool ok)
    {
        PlayerPrefs.SetInt(BATTLE_ITEM_AUTO_FULL, ok ? 1 : 0);
        s_bBattleAutoFull = ok;
    }

    /// <summary>
    /// 设置是否第一次开启新区域
    /// </summary>
    /// <param name="ok"></param>
    public static void SaveNewDungeonOfNewArea(bool opened)
    {
        PlayerPrefs.SetInt(NEW_DUNGEON_OF_NEW_AREA, opened ? 1 : 0);
        s_bNewDungeonOfNewArea = opened;
    }

    /// <summary>
    /// 设置上次持续登录天数
    /// </summary>
    /// <param name="days"></param>
    public static void SaveLastLoginDays(int days)
    {
        PlayerPrefs.SetInt(LAST_LOGIN_DAYS, days);
        s_iLastLoginDays = days;
    }

    /// <summary>
    /// 保存设置
    /// </summary>
    public static void SaveSetting()
    {
        //帐号
        PlayerPrefs.SetString(USER_NAME, s_strUserName);
        PlayerPrefs.SetString(PASSWORD, s_strPassWord);
        PlayerPrefs.SetInt(ACCOUNT_BOUND, s_bAccountBound ? 1 : 0);

        //特效
        PlayerPrefs.SetInt(SKEFFECT_SWITCH, s_bSKEffectSwitch ? 1 : 0);
        PlayerPrefs.SetInt(ATEFFECT_SWITCH, s_bATEffectSwitch ? 1 : 0);
        PlayerPrefs.SetInt(ENEFFECT_SWITCH, s_bENEffectSwitch ? 1 : 0);

        //音乐
        PlayerPrefs.SetFloat(BGM_VOLUME, s_fBGM_Volume);
        PlayerPrefs.SetFloat(SE_VOLUME, s_fSE_Volume);

        PlayerPrefs.Save();
    }

    public static void SaveEquipment()
    {
        LAST_EQUIPMENT_LIST = "";
        foreach (int key in EQUIPMENT_LIST.Keys)
        {
            LAST_EQUIPMENT_LIST += key.ToString() + "," + EQUIPMENT_LIST[key].ToString() + ";";
        }
        //装备屋
        PlayerPrefs.SetString(EQUIPMENT_NEW, LAST_EQUIPMENT_LIST);
    }

    public static void SaveItem()
    {
        LAST_ITEM_LIST = "";
        foreach (int key in ITEM_LIST.Keys)
        {
            LAST_ITEM_LIST += key.ToString() + "," + ITEM_LIST[key].ToString() + ";";
        }
        //调和屋
        PlayerPrefs.SetString(ITEM_NEW, LAST_ITEM_LIST);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 清除设置
    /// </summary>
    public static void ClearSetting()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 清除帐号信息
    /// </summary>
    public static void ClearAccount()
    {
        s_strUserName = "";
        s_strPassWord = "";
        s_bAccountBound = false;
        s_bCodeHasView = false;
        LAST_EQUIPMENT_LIST = null;
        LAST_ITEM_LIST = null;
        PlayerPrefs.SetString(USER_NAME, s_strUserName);
        PlayerPrefs.SetString(PASSWORD, s_strPassWord);
        PlayerPrefs.SetInt(ACCOUNT_BOUND, s_bAccountBound ? 1 : 0);
        PlayerPrefs.SetInt(CODEHASVIEW, s_bCodeHasView ? 1 : 0);
        PlayerPrefs.SetString(EQUIPMENT_NEW, LAST_EQUIPMENT_LIST);
        PlayerPrefs.SetString(ITEM_NEW, LAST_ITEM_LIST);

        s_iWarnHouseTiaohe = 0;
        s_iWarnHouseEquip = 0;
        s_iWarnHeroEquip = 0;
        s_iEquipLevelAdd = 1;
        s_iItemLevelAdd = 1;
        s_iIsOver = 0;
        s_dicWarnHeroJinhua = null;
        HeroJinhua = "";
        s_bEquipLevelUp = true;
        s_bItemLevelUp = true;
        PlayerPrefs.SetString(WARN_HOUSE_TIAOHE, "0|0");
        PlayerPrefs.SetString(WARN_HOUSE_EQUIP, "0|0");
        PlayerPrefs.SetInt(WARN_HERO_EQUIP, s_iWarnHeroEquip);
        PlayerPrefs.SetString(WARN_HERO_JINHUA, HeroJinhua);
        PlayerPrefs.SetInt(ITEMLEVELADD, s_iItemLevelAdd);
        PlayerPrefs.SetInt(EQUIPLEVELADD,s_iEquipLevelAdd);
        PlayerPrefs.SetInt(ISOVER, s_iIsOver);
        PlayerPrefs.SetInt(EQUIPHOUSELEVELUP, s_bEquipLevelUp ? 1 : 0);
        PlayerPrefs.SetInt(ITEMHOUSELEVELUP, s_bItemLevelUp ? 1 : 0);

        //副本信息清零
        WorldManager.s_iCurrentWorldId = 0;
        WorldManager.s_iCurrentAreaIndex = 0;
        (GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_AREA) as GUIArea).ResetNewAreaIndex();

        PlayerPrefs.Save();
    }

    /// <summary>
    /// 保存游戏加入字段
    /// </summary>
    public static void SaveGAME_JOIN( )
    {
        PlayerPrefs.SetInt(GAME_JOIN_STR, GAME_JOIN ? 1 : 0);
        PlayerPrefs.SetString(DEVICE_ID_STR, DEVICE_ID);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 保存新解锁调合屋提示
    /// </summary>
    public static void SaveWarnHouseTiaohe()
    {
        //新解锁药物
        PlayerPrefs.SetString(WARN_HOUSE_TIAOHE, s_iWarnHouseTiaohe.ToString());
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 保存新解锁装备屋提示
    /// </summary>
    public static void SaveWarnHouseEquip()
    {
        //新解锁药物
        PlayerPrefs.SetString(WARN_HOUSE_EQUIP, s_iWarnHouseEquip.ToString());
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 保存英雄可新装备提示
    /// </summary>
    public static void SaveWarnHeroEquip()
    {
        PlayerPrefs.SetInt(WARN_HERO_EQUIP, s_iWarnHeroEquip);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 保存英雄可进化提醒
    /// </summary>
    public static void SaveWarnHeroJinhua()
    {
        HeroJinhua = "";
        foreach (int key in s_dicWarnHeroJinhua.Keys)
        {
            HeroJinhua += key.ToString() + "," + s_dicWarnHeroJinhua[key].ToString() + ";";
        }
        PlayerPrefs.SetString(WARN_HERO_JINHUA, HeroJinhua);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 保存装备屋浏览状态
    /// </summary>
    public static void SaveEquipScane()
    {
        PlayerPrefs.SetInt(EQUIPHOUSELEVELUP, (s_bEquipLevelUp ? 1:0));
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 保存调和屋浏览状态
    /// </summary>
    public static void SaveItemScane()
    {
        PlayerPrefs.SetInt(ITEMHOUSELEVELUP, (s_bItemLevelUp ? 1:0));
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 保存装备屋升级数
    /// </summary>
    public static void SaveEquipLevelAdd()
    {
        PlayerPrefs.SetInt(EQUIPLEVELADD, s_iEquipLevelAdd);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 保存调和屋升级数
    /// </summary>
    public static void SaveItemLevelAdd()
    {
        PlayerPrefs.SetInt(ITEMLEVELADD, s_iItemLevelAdd);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 保存是否通关
    /// </summary>
    public static void SaveGuanKa()
    {
        PlayerPrefs.SetInt(ISOVER, s_iIsOver);
        PlayerPrefs.Save();
    }
}