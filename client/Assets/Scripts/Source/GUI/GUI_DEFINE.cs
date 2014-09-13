using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Base;
using UnityEngine;


//  GUI_DEFINE.cs
//  Author: Lu Zexi
//  2013-11-12


/// <summary>
/// GUI层级
/// </summary>
public enum GUILAYER
{
    GUI_BACKGROUND = 0, //背景层
    GUI_MENU = 1,   //菜单层0
    GUI_MENU1,      //菜单层1
    GUI_PANEL,      //面板层
    GUI_PANEL1,     //面板1层
    GUI_PANEL2,     //面板2层
    GUI_PANEL3,     //面板3层
    GUI_FULL,       //满屏层
    GUI_LOADING,    //加载等待层
    GUI_MESSAGE,    //消息层0
    GUI_MESSAGE1,   //消息层1
    GUI_LOCKPANEL,  //Lock面板层
}


/// <summary>
/// GUI定义
/// </summary>
public class GUI_DEFINE
{
    //锚点位置
    public const string GUI_ANCHOR_CENTER = "ROOT/ANCHOR_CENTER";   //居中顶点
    public const string GUI_ANCHOR_TOP = "ROOT/ANCHOR_TOP";         //上顶点
    public const string GUI_ANCHOR_BOTTOM = "ROOT/ANCHOR_BOTTOM";   //下顶点
    public const string GUI_ANCHOR_LEFT = "ROOT/ANCHOR_LEFT";       //左顶点
    public const string GUI_ANCHOR_RIGHT = "ROOT/ANCHOR_RIGHT";     //右顶点
    public const string GUI_ANCHOR_TOP_RIGHT = "ROOT/ANCHOR_TOP_RIGHT"; //顶右点
    public const string GUI_ANCHOR_TOP_LEFT = "ROOT/ANCHOR_TOP_LEFT";   //顶左点
    public const string GUI_ANCHOR_BOTTOM_LEFT = "ROOT/ANCHOR_BOTTOM_LEFT"; //下左点
    public const string GUI_ANCHOR_BOTTOM_RIGHT = "ROOT/ANCHOR_BOTTOM_RIGHT";   //下右点

    //战斗场景特效父节点
    public const string SCENE_EFFECT_OBJECT = "Effect";   //场景特效父节点

    //GUI ID
    public const int GUIID_TITTLE = 100;   //标题GUI ID
    public const int GUIID_MAIN = 101;  //主界面GUI
    public const int GUIID_WORLD = 102; //世界GUI ID
    public const int GUIID_AREA = 103; //区域地图GUI ID
    public const int GUIID_AREADUNGEON = 104; //区域副本GUI ID
    public const int GUIID_FRIENDFIGHT = 105; //战友栏GUI ID
    public const int GUIID_FIGHTREADY = 106;  //战斗准备GUI ID
    public const int GUIID_ESPDUNGEON = 107;  //特殊副本GUI ID
    public const int GUIID_ESPDUNGEONGATE = 108;  //特殊副本关卡GUI ID
    //public const int GUIID_STORYINTRODUCE = 109;   //剧情插入GUI ID
    public const int GUIID_BACKFRAMETOP = 110;     //顶部状态栏GUI ID
    public const int GUIID_BACKFRAMEBOTTOM = 111;  //底部菜单栏GUI ID
    public const int GUIID_RESOURCE_LOADING = 112; //资源加载界面GUI ID
    public const int GUIID_LOADING = 113;   //等待统一页面GUI ID
    public const int GUIID_HIDEN = 114;   //隐藏等待统一界面GUI ID
    public const int GUIID_TEAM_EDITOR = 115;   //编队编辑GUI ID
    public const int GUIID_STORE = 116;   //商城主页面GUI
    public const int GUIID_GEM = 117;   //宝石购入GUI
    public const int GUIID_HERO_MENU = 118; //英雄菜单GUI
    public const int GUIID_BODYSTRENGTHRESTORATION = 119; //体力恢复页面GUI ID
    public const int GUIID_SUMMON = 120;   //召唤界面GUI ID
    public const int GUIID_UNITSLOTEXPANSION = 121;//单位槽扩张GUI ID
    public const int GUIID_PROPSSLOTEXPANSION = 122;//道具槽扩张GUIID
    public const int GUIID_FISTFIGHTPOINTRESTORATION = 123; //格斗点恢复
    //public const int GUIID_ALEAT = 124; //弹出框GUI ID
    public const int GUIID_MENU = 125;  //Menu菜单GUI ID
    public const int GUIID_FESTA = 126;//特典GUIID
    public const int GUIID_FESTAINVITE = 127;//特典-邀请好友GUIID
    public const int GUIID_HELP = 128;//菜单-帮助GUIID
    public const int GUIID_USERINFO = 129;//角色信息GUIID
    public const int GUIID_SETTING = 130;//设置GUIID
    public const int GUIID_INTELLIGENCE = 131;//情报界面GUIID 
    public const int GUIID_PROPSATLAS = 132;//道具图鉴GUIID
    public const int GUIID_GATE_BATTLE = 133;    //关卡战斗GUI
    public const int GUIID_MAIL = 134;//礼物盒界面
    public const int GUIID_HEROALTAS = 135; //英雄图鉴
    public const int GUIID_HERO_SHOW = 136; //英雄一览展示
    public const int GUIID_UPGRADE = 137;   //升级界面
    public const int GUIID_HERODETAIL = 138;//英雄详细页面GUIID
    public const int GUIID_EVOLUTION = 139; //英雄进化选择界面
    public const int GUIID_HEROEQUIPMENT = 140;  //英雄装备界面
    public const int GUIID_HEROSELL = 141;  //英雄出售界面 GUI ID
    public const int GUIID_TOWN = 142;//村界面GUIID
    public const int GUIID_PROPSWAREHOUSE = 143;//道具仓库GUIID
    public const int GUIID_FRIENDMENU = 144;   //好友菜单GUI ID
    public const int GUIID_FRIENDLIST = 145;   //好友列表GUI ID
    public const int GUIID_FRIENDAPPLY = 146;  //好友申请GUI ID
    public const int GUIID_FRIENDSEARCH = 147; //好友检索GUI ID
    public const int GUIID_FRIENDGIFT = 148;   //好友礼物GUI ID
    public const int GUIID_PROPSGROUP = 149;   //道具编成GUIID
    public const int GUIID_FRIENDINFO = 150;   //好友信息GUI ID
    public const int GUIID_PROPSPREVIEW = 151;//道具一览GUIID
    public const int GUIID_PROPSSALES = 152;//道具出售GUIID
    public const int GUIID_FRIENDINFOLIKE = 153; //好友喜欢GUI ID
    //public const int GUIID_PROPSPREVIEWDETAIL = 154;//道具详细GUII
    public const int GUIID_PROPSSALESDETAIL = 155;//道具售卖详细GUIID
    //public const int GUIID_FRIENDGIFTB = 157; //好友礼物B GUIID
    public const int GUIID_FRIENDGIFTSELECT = 158;  //好友选择礼物
    public const int GUIID_EQUIPUPGRADE = 159;   //设备强化GUI ID
    public const int GUIID_TEAMHERO = 160;  //团队人物选择界面//

    public const int GUIID_BATTLE_NEXT = 161;   //关卡战斗下一场GUI
    public const int GUIID_PROPSGROUPDETAIL = 162;//道具编成详细GUIID
    public const int GUIID_RECONCELIHOUSE = 163;//调和屋GUIID
    public const int GUIID_EQUIPMENTHOUSE = 164;//调和屋GUIID

    public const int GUIID_BACKGROUND = 169;    //背景GUI
    public const int GUIID_ROLE_CREATE = 170;   //角色创建界面
    public const int GUIID_MESSAGEL = 171;   //大消息界面
    public const int GUIID_BATTLE_REWARD = 172;   //战斗奖励
    public const int GUIID_BATTLEMENU = 173;//战斗菜单GUIID
    public const int GUIID_BATTLEHELP = 175;//战斗-帮助界面

    public const int GUIID_UPGRADEHERO = 176;   //人物升级界面//

    public const int GUIID_HEROEQUIPSELECT = 177; //英雄装备选择英雄界面

    public const int GUIID_UPGRADEHEROSELECT = 178; //升级选择经验怪界面//
    public const int GUIID_UPGRADEHERORESULT = 179; //升级结果界面//

    public const int GUIID_BATTLE_ITEM_SELECT = 180;  //战斗物品选择
    public const int GUIID_RECONCE_COMBINE = 181;  //消耗品合成

    public const int GUIID_EVOLUTIONHERO = 182; //英雄进化界面
    public const int GUIID_EVOLUTIONRESULT = 183;   //英雄进化结果界面//

    public const int GUIID_BATTLERECORD = 184;//菜单-战绩界面
    public const int GUIID_ACTIVITY_BATTLE = 185;   //活动战斗GUI
    public const int GUIID_MESSAGEM = 186;   //中消息界面
    public const int GUIID_MESSAGES = 187;   //小消息界面
    public const int GUIID_MESSAGESS = 188;   //迷你消息界面

    public const int GUIID_FRIENDGIFTEXPECTSELECT = 189;    //好友礼物期望选择//
    public const int GUIID_FRIENDGIFTGIVE = 190;    //好友赠送界面//
    public const int GUIID_HELPTYPEDETAIL = 191;//帮助类型详细
    public const int GUIID_HELPPROJECTDETAIL = 192;//帮助项目详细

    public const int GUIID_HERO_ALTAS_DETAIL = 193;  //英雄介绍详细界面
    public const int GUIID_ANNOUNCEMENT = 194;//公告界面

    public const int GUIID_FESTA_INPUT = 195;  //第一次输入邀请码界面GUIID
    public const int GUIID_ARENA = 196;//竞技场GUI
    public const int GUIID_ARENAFIGHTREADY = 197;//竞技场战斗GUI
    public const int GUIID_ARENAREWORDINFO = 198;//竞技场-奖励信息
    public const int GUIID_ARENABATTLEINTELLIGENCE = 199;//竞技场-战绩情报
    public const int GUIID_ARENARANKINGS = 200;//竞技场-排行榜
    public const int GUIID_ARENA_BATTLE = 201;  //竞技战斗GUI

    public const int GUIID_SUMMON_RESULT = 203; //英雄召唤
    public const int GUIID_SUMMON_DETAIL = 204;   //召唤详细提示GUIID

    public const int GUIID_ANRENA_BATTLE_RESULT = 205;  //竞技场战斗结果GUIID


    public const int GUIID_ANRENA_BATTLE_FRIEND = 207;  //竞技场结束加好友
    public const int GUIID_DUNGEON_BATTLE_FRIEND = 208; //副本结束加好友

    public const int GUIID_HERO_CHOOSE = 209;//英雄选择GUI

    public const int GUIID_BATTLE_LOSE = 210;   //战斗失败界面

    public const int GUIID_ACCOUNT = 211;   //帐号界面
    public const int GUIID_STORY = 212; //剧情界面
    public const int GUIID_GUIDE_BATTLE = 213; //新手引导战斗
    public const int GUIID_GUIDE_BATTLE_SECOND = 214;   //新手引导战斗2

    public const int GUIID_AYSNC_LOADING = 20000;//异步加载资源//
    public const int GUIID_LOCKPANEL = 21000;//Lock面板GUI
    public const int GUIID_GUIDE = 22000;   //新手引导GUI
    public const int GUIID_LOGINREWARD = 23000;//登录奖励GUI

    public const int GUIID_BATTLE_NEWHEROSHOW = 24000;//战斗获得新英雄展示 

    public const int GUIID_BATTLE_MENU_SETTING = 500;//战斗设定
    public const int GUIID_BATTLE_MENU_GETHERO = 501;//战斗菜单获得英雄
    public const int GUIID_BATTLE_MENU_GETITEM= 502;//战斗菜单获得物品
    public const int GUIID_BATTLE_MENU_HERO_INTELLIGENCE = 503;//战斗菜单英雄情报
    public const int GUIID_BATTLE_MENU_HELP = 504;//战斗菜单帮助
    public const int GUIID_BATTLE_MENU_GIVE_UP = 505;//战斗菜单放弃战斗
    public const int GUIID_PRODUCTION_STAFF = 506;//制作人员列表
}
