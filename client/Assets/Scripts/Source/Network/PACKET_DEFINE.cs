using System;
using System.Collections.Generic;

using Game.Network;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;


//  PACKET_DEFINE.cs
//  Author: Lu Zexi
//  2013-11-13



/// <summary>
/// 数据包Action定义
/// </summary>
public class PACKET_DEFINE
{
    public const string ACCOUNT_BOUND_REQ = "public/bind&"; //帐号绑定
    public const string AUTO_REGIST_REQ = "public/autoreg&"; //自动注册
    public const string ACCOUNT_LOGIN_REQ = "public/login&";    //帐号登录
    public const string GET_PLAYINFO_REQ = "player/getinfo&";   //获取玩家信息
    public const string CREATE_PLAY_REQ = "player/create&";  //创建玩家
    public const string GET_PALYERHEROINFO_REQ = "hero/getheros&";//获取玩家英雄列表
    public const string GET_TEAMS_REQ = "team/getteams&";  //获取玩家队伍信息
    public const string VERSION_REQ = "public/getprops&";   //版本号获取
    public const string GET_PLAYERTASKINFO_REQ = "mission/getinfo&";//获取当前任务信息
    public const string GET_BUILDING_GET_REQ = "building/getinfo&";  //获取建筑信息
    public const string FRIENDPOINT_SUMMON_REQ = "summon/friendpointsummon&"; //友情点召唤
    public const string MONEY_SUMMON_REQ = "summon/moneysummon&";  //金钱召唤
    public const string BATTLE_GATE_START_REQ = "mission/startbattle&";  //关卡战斗开始请求
    public const string BATTLE_GATE_END_REQ = "mission/finishgate&";    //完成关卡请求
    public const string HERO_SELL_REQ = "hero/sell&";  //英雄出售
    public const string TEAM_EDITOR_REQ = "team/edit&"; //队伍编辑
    public const string HERO_EQUIP_UPDATE_REQ = "item/equip&";  //装备道具
    public const string FRIEND_FIGHT_REQ = "battle/gethelpers&";//获取战友
    public const string ITEM_GET_REQ = "item/getitems&";   //获取道具
    public const string SIGN_UPDATE_REQ = "player/updatesignature&";  //更新签名
    public const string ITEM_COMBINED_REQ = "item/combine&";  //物品合成
    public const string HERO_UPGRADE_REQ = "hero/strengthen&";  //英雄强化
    public const string BATTLE_ITEM_EDIT_REQ = "item/editreadyitem&";  //战斗物品编辑
    public const string BATTLE_ITEM_GET_REQ = "item/getreadyitems&";  //战斗物品获取
    public const string ITEM_COLLECT_REQ = "building/collect&";   //素材采集
    public const string STRENGTH_RECOVER_REQ = "shop/recoverstrength&";//体力恢复
    public const string PROPS_EXPANSION_REQ = "shop/add5itemslot&";//道具槽扩展
    public const string UNIT_EXPANSION_REQ = "shop/add5heroslot&";//单位槽扩张
    public const string BATTLEPOING_RECOVER_REQ = "shop/recoversportpoint&";//竞技点恢复
    public const string BUILDING_UPDATE_REQ = "building/strengthenbuilding&";  //建筑等级更新
    public const string HERO_EVOLUTION_REQ = "hero/evolve&";    //英雄进化
    public const string HERO_BOOK_REQ = "hero/tujian&"; //英雄图鉴
    public const string ITEM_BOOK_REQ = "item/tujian&"; //物品图鉴
    public const string ITEM_SELL_REQ = "item/sellitem&";  //物品出售
    public const string ACTIVITY_BATTLE_START_REQ = "activity/startbattle&"; //活动战斗开始
    public const string ACTIVITY_BATTLE_END_REQ = "activity/finishgate&";   //活动战斗结束
    public const string HERO_LOCK_REQ = "hero/lock&";  //英雄锁定
    public const string HERO_UNLOCK_REQ = "hero/unlock&";  //英雄解锁
    public const string ACTIVITY_FUBEN_FAVOURABLE_REQ = "activity/getfubeninfo&";//活动副本优惠类型
    public const string FRIEND_GETLIST_REQ = "friend/getfriends&";  //获取好友列表//
    public const string FRIEND_LOCKLIKE_REQ = "friend/like&";   //好友喜欢保存//
    public const string FRIEND_UNLOCKLIKE_REQ = "friend/unlike&";   //好友喜欢解除//
    public const string FRIEND_GETAPPLYLIST_REQ = "friend/getapply&";   //好友申请列表//
    public const string FRIEND_ACCEPTAPPLY_REQ = "friend/makefriend&";  //接受好友请求
    public const string FRIEND_FIND_REQ = "friend/find&";   //好友查找//
    public const string FRIEND_APPLY_REQ = "friend/applyfriend&";   //好友申请
    public const string FRIEND_CANCELAPPLY_REQ = "friend/cancelapply&"; //取消好友申请
    public const string FRIEND_DEL_REQ = "friend/delfriend&";    //好友删除
    public const string FRIEND_GETGIFTLIST_REQ = "friend/getgift&";    //好友礼物列表
    public const string FRIEND_ACCEPTGIFT_REQ = "friend/recvgift&"; //接受礼物//
    public const string FRIEND_SENDGIFT_REQ = "friend/sendgift&";   //送好友礼物//
    public const string PLAYER_GET_SYSTEM_MAIL_REQ = "system/getgift&";//获取系统邮件
    public const string PLAYER_RECEIVE_SYSTEM_MAIL_REQ = "system/recvgift&";//接收系统赠送的礼物
    public const string FRIEND_WANTGIFT_REQ = "friend/changewant&";   //好友期待礼物
    public const string GUEST_ZHAODAI_REQ = "system/zhaodaiplayer&";  //玩家招待
    public const string PVP_INFO_GET_REQ = "battlePVP/getpvpinfo&";  //竞技场基本信息获取
    public const string PVP_BATTLE_RANK_REQ = "battlePVP/getrank&";  //竞技场排名信息获取
    public const string PVP_ENEMY_GET_REQ = "battlePVP/pvprandom&";  //竞技场随机获取挑战对手
    public const string PVP_ENEMY_GET_5_REQ = "battlePVP/RandomChallengers&";  //竞技场随机获取5位对手
    public const string PVP_DETAIL_GET_REQ = "battlePVP/getpvpinfodetal&"; //竞技场详细信息获取
    public const string PVP_BATTLE_START_REQ = "battlePVP/pvpstart&";   //竞技战斗开始
    public const string PVP_BATTLE_END_REQ = "battlePVP/pvpend&";   //竞技战斗结束
    public const string PVP_WEEK_RANK_GET_REQ = "battlePVP/getweekrank&";  //获得竞技场周排名
    public const string SYSTEM_PUSH_REQ = "system/push&";   //推送
    public const string FUBEN_STORY_REQ = "mission/story&"; //副本剧情设置
    public const string STORE_DIAMOND_GET_REQ = "shop/crystalList";//商城钻石价格
    public const string BATTLE_RECORD_GET_REQ = "system/getbattlerecord&";//战绩数据
    public const string GUIDE_STEP_REQ = "player/updateguidstep&";  //新手引导数据
    public const string BATTLE_RELIVE_REQ = "battle/relive&"; //复活
    public const string BATTLE_GATE_FAIL_REQ = "mission/battlefail&";  //战斗关卡失败
    public const string ACTIVITY_BATTLE_FAIL_REQ = "activity/battlefail&";  //活动战斗失败
    public const string GAME_JOIN_REQ = "site/recordstart&"; //游戏进入请求
    public const string DEVICE_REQ = "site/device&";    //设备action

    public const string PAY_REQ = "pay/pay&";   //支付请求
    public const string PAY_IOS_VERIFY = "pay/iosverify&";    //支付验证

    public const string PAY_IOS_PP_VERIFY = "pay/iosppverify&"; //PP支付验证
    public const string ACCOUNT_IOS_PP_LOGIN = "login/pphelper&";    //登录验证

    //GameDispatch
    public const string GAME_TEST_REQ = "player/loadgame";  //测试Action


}



///// <summary>
///// 打包工具
///// </summary>
//public class PACKET_HEAD
//{
//    public static T PACKET_ACK_HEAD<T>(IJSonObject json) where T : HTTPPacketRequest, new()
//    {
//        T t = new T();
//        t.m_iErrorCode = json["code"].Int32Value;
//        t.m_strErrorDes = json["desc"].StringValue;
//        t.m_lServerTime = json["time"].Int32Value;
//        GAME_DEFINE.m_lServerTime = t.m_lServerTime;
//        //服务器时间精确到秒
//        GAME_DEFINE.m_lTimeSpan = DateTime.Now.Ticks / 10000000 - t.m_lServerTime;
//        return t;
//    }
//
//    /// <summary>
//    /// 数据包后结尾数据打包
//    /// </summary>
//    /// <param name="req"></param>
//    /// <returns></returns>
//    public static string PACKET_REQ_END( ref string req)
//    {
//        string[] arg = req.Split('&');
//        string res = GAME_DEFINE.SERVER_KEY + GAME_DEFINE.m_lServerTime + GAME_DEFINE.s_strToken+ "";
//        for (int i = 0; i < arg.Length; i++)
//        {
//            string[] tmp_arg = arg[i].Split('=');
//            if (tmp_arg.Length == 2)
//            {
//                res += tmp_arg[1];
//            }
//        }
//
//        UnityEngine.Debug.Log("md5 " + res);
//
//        MD5 md5 = new MD5CryptoServiceProvider();
//        byte[] buff = Encoding.UTF8.GetBytes(res);
//        byte[] op = md5.ComputeHash(buff);
//        string md5Str = BitConverter.ToString(op).Replace("-", "");
//
//        req += "&token=" + GAME_DEFINE.s_strToken + "&server_time=" + GAME_DEFINE.m_lServerTime + "&MD5=" + md5Str;
//
//        UnityEngine.Debug.Log(req);
//
//        return req;
//    }
//}