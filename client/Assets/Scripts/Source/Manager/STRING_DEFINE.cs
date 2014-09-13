using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  STRING_DEFINE.cs
//  Author: Lu Zexi
//  2013-12-19

/// <summary>
/// 字符串定义
/// </summary>
public class STRING_DEFINE
{
    public const int NATURE = 1003; //属性
    public const int LEVEL = 1004;  //等级
    public const int HP = 1005; //HP
    public const int ATTACK = 1006; //攻击
    public const int DEFENCE = 1007;    //防御
    public const int REVERT = 1008; //回复
    public const int STAR = 1009;   //稀有度
    public const int COST = 1010;   //领导力
    public const int NEW_OLD = 1011;    //获得时间
    public const int STRENGTH_NOT_ENOUGH = 1012;    //您的体力不足

    public const int DIAMOND_NOT_ENOUGH = 1013; //您的钻石不足，\n 是否前往购买钻石？

    public const int WARNING_MAX_HERO = 1018;  //英雄数量已经达到上限，请出售或强化英雄。
    public const int WARNING_MAX_ITEM = 1019;   //物品数量已经到达上限，请出售或合成物品。

    public const int SUMMON_DIAMOND_INFO = 1020;  //砖石招募提示
    public const int SUMMON_FRIEND_INFO = 1021;  //好友招募提示
    public const int SUMMON_DIAMOND_MSG_TITLE= 1022;   //砖石招募消息框标题
    public const int SUMMON_FIREND_MSG_TITLE= 1023;  //好友招募消息框标题
    public const int SUMMON_DIAMOND_MSG_LINE1 = 1024; //砖石招募消息框第1行
    public const int SUMMON_DIAMOND_MSG_LINE2 = 1025; //砖石招募消息框第2行
    public const int SUMMON_DIAMOND_MSG_LINE3 = 1026; //砖石招募消息框第3行
    public const int SUMMON_DIAMOND_MSG_NOT_ENOUGH = 1027; //砖石招募不足提示
    public const int SUMMON_FRIEND_MSG_NOT_ENOUGH = 1028; //友情招募不足提示
    public const int SUMMON_FRIEND_MSG_LINE1 = 1029; //好友招募消息框第1行
    public const int SUMMON_FRIEND_MSG_LINE2 = 1030; //好友招募消息框第2行
    public const int SUMMON_FRIEND_MSG_LINE3 = 1031; //好友招募消息框第3行
    public const int HERO_TYPE_STR1 = 1032;  //经验素材
    public const int HERO_TYPE_STR2 = 1033;  //进化素材
    public const int HERO_TYPE_STR3 = 1034;  //金钱素材
    public const int HERO_TYPE_BALANCE = 1035;  //平衡性英雄
    public const int HERO_TYPE_HP = 1036;            //血量性英雄
    public const int HERO_TYPE_ATTACK = 1037;    //攻击性英雄
    public const int HERO_TYPE_DEFENCE = 1038;  //防御性英雄
    public const int HERO_TYPE_RECOVER = 1039;  //回复性英雄

    public const int INFO_MAIN_MENU = 1040;    // 1级主界面提示
    public const int INFO_PLAYER = 1041; //玩家自定义信息
    public const int INFO_ACCOUNT_NUMBER = 1042; //账号信息
    public const int INFO_SCANE_HERO = 1043; //英雄图鉴
    public const int INFO_ITEM = 1044; //道具图鉴
    public const int INFO_SYS_SETTING = 1045; //系统设定
    public const int INFO_HELP = 1046; //帮助
    public const int INFO_MAKE_TEAM = 1047; //队伍编成
    public const int INFO_CHANGE_HERO = 1048; //更换主帅
    public const int INFO_CHANGE_POS = 1049; // 更换位置
    public const int INFO_SELECT_HERO = 1050; // 选择英雄（进化）界面
    public const int INFO_ENHANCE_HERO = 1051; //进化界面
    public const int INFO_SELECT_EPUIP = 1052; //装备选择
    public const int INFO_SELECT_EQUIP_HERO = 1053; // 装备人物选择
    public const int INFO_SALE_HERO = 1054; //出售英雄
    public const int INFO_TOWN = 1055; //桃园
    public const int INFO_ENHANCE_HOUSE = 1056; //设施强化
    public const int INFO_PROPSWAREHOUSE = 1057; //调和屋
    public const int INFO_EQUIPMENTHOUSE = 1058; //装备屋
    public const int INFO_SCANE_ITEM = 1059; //道具一览
    public const int INFO_EQUIP_ITEM = 1060; //道具编成
    public const int INFO_SALE_ITEM = 1061; //道具出售
    public const int INFO_BUY_DIAMOND = 1062; //钻石购买
    public const int INFO_RENEW_POWER = 1063; //体力恢复
    public const int INFO_INCREASE_HERO_MAX_NUMBR = 1064; //英雄栏扩展
    public const int INFO_INCREASE_ITEM_MAX_NUMBER = 1065; //道具栏扩展
    public const int INFO_RENEW_ARENAPOINT = 1066; //竞技点恢复
    public const int INFO_RECRUIT_HERO = 1067; //英雄招募
    public const int INFO_CLICK_RECUIT = 1068; //点击招募
    public const int INFO_FRIEND_LIST = 1069; //好友列表
    public const int INFO_OPERATE_FRIEND = 1070; //好友操作
    public const int INFO_ADD_FRIEND = 1071; //添加好友
    public const int INFO_SEARCH_FRIEND = 1072; //好友搜索
    public const int INFO_RECEIVE_GIFT = 1073; // 礼物收取界面
    public const int INFO_GIVE_GIFT = 1074; // 礼物赠送界面
    public const int INFO_STRENGTHEN_HERO = 1075; //强化英雄
    public const int INFO_CLICK_SQUARE = 1076; //点击方阵
    public const int INFO_SELECT_SOURCE = 1077; // 强化素材选择
    public const int INFO_ARENA = 1078; //竞技场
    public const int INFO_ARENA_REWARD = 1079; //竞技场奖励信息
    public const int INFO_ARENA_RECORD = 1080; //竞技场战绩信息
    public const int INFO_ARENA_RANKING = 1081; //竞技场排行榜
    public const int INFO_ARENA_CHOOSE_OPPONENT = 1082; //竞技场选择对手
    public const int INFO_HERO_TYPE_BALANCE = 1083; //平衡型
    public const int INFO_HERO_TYPR_ATTACK = 1084; //攻击型
    public const int INFO_HERO_TYPE_DEFENCE = 1085; //防御型
    public const int INFO_HERO_TYPE_RECOVER = 1086; //回复型
    public const int INFO_HERO_TYPR_HP = 1087; //生命型
    public const int INFO_HERO_TYPE_BALANCE_DETAIL = 1088; //平衡型详细
    public const int INFO_HERO_TYPE_ATTACK_DETAIL = 1089; //攻击型详细
    public const int INFO_HERO_TYPE_DEFENCE_DETAIL = 1090; //防御详细
    public const int INFO_HERO_TYPE_RECOVER_DETAIL = 1091; //回复详细
    public const int INFO_HERO_TYPE_HP_DETAIL = 1092; //生命型详细
    public const int INFO_COST_OVER_MAX = 1093;//超出cost上限提示
}
