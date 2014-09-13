using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  SendAgent.cs
//  Author: Lu Zexi
//  2013-11-12


/// <summary>
/// 发送代理
/// </summary>
public class SendAgent
{
    /// <summary>
    /// 发送版本数据请求
    /// </summary>
    public static void SendVersionReq()
    {
        VersionReqPkt packet = new VersionReqPkt();
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, packet);
    }

    /// <summary>
    /// 发送帐号绑定请求
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="passWord"></param>
    /// <param name="newUsername"></param>
    /// <param name="newPassword"></param>
    public static void SendAccountBoundReq(string userName, string passWord, string newUsername, string newPassword)
    {
        AccountBoundPktReq req = new AccountBoundPktReq();
        req.m_strUser = userName;
        req.m_strPassword = passWord;
        req.m_strNewUser = newUsername;
        req.m_strNewPassword = newPassword;

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 角色信息请求
    /// </summary>
    public static void SendAccountAutoRegistReq()
    {
        AccountAutoRegistPacketReq req = new AccountAutoRegistPacketReq();
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 帐号登录
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    public static void SendAccountLogin(string userName, string password)
    {
        AccountLoginPktReq req = new AccountLoginPktReq();
        req.m_strUserName = userName;
        req.m_strPassword = password;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 获取玩家信息请求
    /// </summary>
    public static void SendPlayerInfoGetPktReq(int uid )
    {
        PlayerInfoGetPktReq req = new PlayerInfoGetPktReq();
        req.m_iUID = uid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 创建玩家请求
    /// </summary>
    public static void SendPlayerCreatePktReq(string nickName, int uid, int select_hero_index)
    {
        PlayerCreatePktReq req = new PlayerCreatePktReq();
        req.m_strNickName = nickName;
        req.m_strUID = uid;
        req.m_iSelectHeroIndex = select_hero_index;
        req.m_strChannel = PlatformManager.GetInstance().GetChannelName();
        req.m_strDeviceID = PlatformManager.GetInstance().GetDeviceID();
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 获取玩家队伍信息
    /// </summary>
    /// <param name="pid">玩家Pid</param>
    public static void SendTeamInfoGetPktReq(int pid)
    {
        TeamInfoGetPktReq req = new TeamInfoGetPktReq();
        req.m_iPid = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送英雄数据请求
    /// </summary>
    /// <param name="pid"></param>
    public static void SendPlayerHeroInfoGetPktReq(int pid)
    {
        PlayerHeroInfoPacketReq req = new PlayerHeroInfoPacketReq();
        req.m_iPlayerId = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 获取副本任务数据请求
    /// </summary>
    /// <param name="pid"></param>
    public static void SendPlayerTaskInfoGetPktReq(int pid)
    {
        PlayerTaskInfoPktReq req = new PlayerTaskInfoPktReq();
        req.m_iPid = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 获取建筑信息数据请求
    /// </summary>
    /// <param name="pid"></param>
    public static void SendBuildInfoGetPktReq(int pid)
    {
        BuildingInfoGetPktReq req = new BuildingInfoGetPktReq();
        req.m_iPID = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送英雄图鉴请求
    /// </summary>
    /// <param name="pid"></param>
    public static void SendHeroBookReq(int pid)
    {
        HeroBookPktReq req = new HeroBookPktReq();
        req.m_iPid = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送物品图鉴请求
    /// </summary>
    /// <param name="pid"></param>
    public static void SendItemBookReq(int pid)
    {
        ItemBookPktReq req = new ItemBookPktReq();
        req.m_iPid = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 友情点召唤数据请求
    /// </summary>
    /// <param name="pid"></param>
    public static void SendFriendPointSummonPktReq(int pid)
    {
        FriendPointSummonPktReq req = new FriendPointSummonPktReq();
        req.m_iPID = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 金钱点召唤数据请求
    /// </summary>
    /// <param name="pid"></param>
    public static void SendMoneySummonPktReq(int pid)
    {
        MoneySummonPktReq req = new MoneySummonPktReq();
        req.m_iPID = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 出售英雄数据请求
    /// </summary>
    /// <param name="pid">玩家ID</param>
    /// <param name="heroIds">要出售的英雄ID数组</param>
    public static void SendHeroSellPktReq(int pid, int[] heroIds)
    {
        HeroSellPktReq req = new HeroSellPktReq();
        req.m_iPid = pid;
        req.m_vecHeros = heroIds;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送队伍编辑数据
    /// </summary>
    /// <param name="pid"></param>
    public static void SendTeamEditor(int pid, List<int[]> teams, int teamID)
    {
        TeamEditorPktReq req = new TeamEditorPktReq();
        req.m_iPid = pid;
        req.m_teams = teams;
        req.m_selectTeam = teamID;

        SessionManager.GetInstance().SendReady(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送战斗关卡开始请求
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="worldid"></param>
    /// <param name="areaIndex"></param>
    /// <param name="fubenIndex"></param>
    /// <param name="gateIndex"></param>
    public static void SendBattleGateStartReq(int pid, int worldid, int areaIndex, int fubenIndex, int gateIndex)
    {
        BattleGateStartPktReq req = new BattleGateStartPktReq();
        req.m_iPid = pid;
        req.m_iWorldID = worldid;
        req.m_iAreaIndex = areaIndex;
        req.m_iFubenIndex = fubenIndex;
        req.m_iGateIndex = gateIndex;

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送战斗关卡结束请求
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="worldid"></param>
    /// <param name="areaIndex"></param>
    /// <param name="fubenIndex"></param>
    /// <param name="gateIndex"></param>
    /// <param name="gold"></param>
    /// <param name="farm"></param>
    /// <param name="heros"></param>
    /// <param name="items"></param>
    /// <param name="itemsNum"></param>
    /// <param name="readyItem"></param>
    public static void SendBattleGateEndReq(int pid , int battle_id , int worldid, int areaIndex, int fubenIndex, int gateIndex, int gold, int farm, int friendbattle_id, List<int> heros, List<int> items, List<int> itemsNum, int[] readyItem,
        int RecordMaxShuijingNum, int TotalShuijingNum, int RecordMaxXinNum, int TotalXinNum, int RoundMaxHurt, int RoundMaxSparkNum, int TotalSparkNum, int TotalSkillNum , int totalBoxMonster)
    {
        BattleGateEndPktReq req = new BattleGateEndPktReq();
        req.m_iPid = pid;
        req.m_iBattleID = battle_id;
        req.m_iWorldID = worldid;
        req.m_iAreaIndex = areaIndex;
        req.m_iFubenIndex = fubenIndex;
        req.m_iGateIndex = gateIndex;
        req.m_iGold = gold;
        req.m_iFarm = farm;
        req.m_iFriendBattleID = friendbattle_id;
        req.m_lstHero = heros;
        req.m_lstItem = items;
        req.m_lstItemNum = itemsNum;
        req.m_vecConsume = readyItem;

        //统计
        req.m_iRecordMaxShuijingNum = RecordMaxShuijingNum;
        req.m_iTotalShuijingNum = TotalShuijingNum;
        req.m_iRecordMaxXinNum = RecordMaxXinNum;
        req.m_iTotalXinNum = TotalXinNum;
        req.m_iRoundMaxHurt = RoundMaxHurt;
        req.m_iRoundMaxSparkNum = RoundMaxSparkNum;
        req.m_iTotalSparkNum = TotalSparkNum;
        req.m_iTotalSkillNum = TotalSkillNum;
        req.m_iTotalBoxMonsterNum = totalBoxMonster;

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 英雄装备更新请求
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="heros"></param>
    /// <param name="items"></param>
    public static void SendHeroEquipUpdateReq(int pid, int[] heros, int[] items)
    {
        HeroEquipUpdatePktReq req = new HeroEquipUpdatePktReq();
        req.m_iPid = pid;
        req.m_vecHeros = heros;
        req.m_vecItems = items;

        SessionManager.GetInstance().SendReady(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 战友请求
    /// </summary>
    /// <param name="pid"></param>
    public static void SendFriendFightReq(int pid)
    {
        FriendFightPktReq req = new FriendFightPktReq();
        req.m_iPid = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 获取所有物品
    /// </summary>
    /// <param name="pid"></param>
    public static void SendItemGetReq(int pid)
    {
        ItemGetPktReq req = new ItemGetPktReq();
        req.m_iPid = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 更新签名请求
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="signature"></param>
    public static void SendSignatureUpdateReq(int pid, string signature)
    {
        PlayerSignatureUpdatePktReq req = new PlayerSignatureUpdatePktReq();
        req.m_strSign = signature;
        req.m_iPid = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 合成物品请求
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="ItemIds"></param>
    /// <param name="ItemNums"></param>
    public static void SendItemCombinedReq(int pid, List<int> ItemIds, List<int> ItemNums)
    {
        ItemCombinePktReq req = new ItemCombinePktReq();
        req.m_iPid = pid;
        req.m_iCombinedId = ItemIds;
        req.m_iCombineNum = ItemNums;
        SessionManager.GetInstance().SendReady(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送英雄升级数据
    /// </summary>
    /// <param name="pid"></param>
    public static void SendHeroUpgrade(int pid, int heroID, List<int> costHeroIDs)
    {
        HeroUpgradePktReq req = new HeroUpgradePktReq();
        req.m_iPID = pid;
        req.m_iHeroID = heroID;
        req.m_iCostHeroIDs = costHeroIDs;

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送物品获取数据
    /// </summary>
    /// <param name="pid"></param>
    public static void SendBattleItemGet(int pid)
    {
        BattleItemGetPktReq req = new BattleItemGetPktReq();
        req.m_iPid = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送编辑物品数据
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="items"></param>
    public static void SendBattleItemEdit(int pid, int[] items,int[] nums)
    {
        BattleItemEditPktReq req = new BattleItemEditPktReq();
        req.m_iPid = pid;
        req.m_vecItems = items;
        req.m_vecItemNums = nums;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送物品采集数据
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="shan_c"></param>
    /// <param name="chuan_c"></param>
    /// <param name="tian_c"></param>
    /// <param name="lin_c"></param>
    /// <param name="gold"></param>
    /// <param name="farmpoint"></param>
    /// <param name="itemTableId"></param>
    /// <param name="itemNum"></param>
    public static void SendItemCollectReq(int pid, int shan_c, int chuan_c, int tian_c, int lin_c, int gold, int farmpoint, int[] itemTableId, int[] itemNum)
    {
        ItemCollectPktReq req = new ItemCollectPktReq();
        req.m_iFarmPoint = farmpoint;
        req.m_iGold = gold;
        req.m_iPid = pid;
        req.m_iShanClick = shan_c;
        req.m_iChuanClick = chuan_c;
        req.m_iTianClick = tian_c;
        req.m_iLinClick = lin_c;
        req.m_vecItemId = itemTableId;
        req.m_vecItemNum = itemNum;
        SessionManager.GetInstance().SendReady(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送体力恢复
    /// </summary>
    /// <param name="pid"></param>
    public static void SendStrengthRecoverReq(int pid)
    {
        PlayerStrengthRecoverPktReq req = new PlayerStrengthRecoverPktReq();
        req.m_iPid = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送道具槽扩张
    /// </summary>
    /// <param name="pid"></param>
    public static void SendPropsSlotExpansionReq(int pid)
    {
        PlayerPropsSlotExpansionPktReq req = new PlayerPropsSlotExpansionPktReq();
        req.m_iPid = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送单位槽扩张
    /// </summary>
    /// <param name="pid"></param>
    public static void SendUnitSlotExpansionReq(int pid)
    {
        HeroUnitSlotExpansionPktReq req = new HeroUnitSlotExpansionPktReq();
        req.m_iPid = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送竞技点恢复
    /// </summary>
    /// <param name="pid"></param>
    public static void SendSportPointRecoverReq(int pid)
    {
        PlayerSportPointRecoverPktReq req = new PlayerSportPointRecoverPktReq();
        req.m_iPid = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送更新建筑等级数据
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="buildType"></param>
    /// <param name="buildLevel"></param>
    /// <param name="buildExp"></param>
    /// <param name="farmpoint"></param>
    public static void SendBuildingUpdateReq(int pid, List<int> buildType, List<int> buildLevel, List<int> buildExp, int farmpoint)
    {
        BuildingUpdatePktReq req = new BuildingUpdatePktReq();
        req.m_iPID = pid;
        req.m_iFarmPoint = farmpoint;
        req.m_lstBuildType = buildType;
        req.m_lstBuildLevel = buildLevel;
        req.m_lstBuildFarmPoint = buildExp;
        SessionManager.GetInstance().SendReady(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    public static void SendHeroEvolution(int pid, int heroID)
    {
        HeroEvolutionPktReq req = new HeroEvolutionPktReq();
        req.m_iPID = pid;
        req.m_iHeroID = heroID;

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }
    

    /// <summary>
    /// 发送道具出售数据请求
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="itemID"></param>
    /// <param name="itemNum"></param>
    public static void SendItemSellReq(int pid, int itemID, int itemNum)
    {
        ItemSellPktReq req = new ItemSellPktReq();
        req.m_iItemId = itemID;
        req.m_iItemNum = itemNum;
        req.m_iPid = pid;

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送活动战斗开始请求
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="fubenid"></param>
    /// <param name="gateIndex"></param>
    public static void SendActivityBattleStartReq(int pid, int fubenid, int gateIndex)
    {
        ActivityBattleStartPktReq req = new ActivityBattleStartPktReq();
        req.m_iPid = pid;
        req.m_iFubenID = fubenid;
        req.m_iGateIndex = gateIndex;

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送活动战斗结束请求
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="fubenID"></param>
    /// <param name="gateIndex"></param>
    /// <param name="gold"></param>
    /// <param name="farm"></param>
    /// <param name="heros"></param>
    /// <param name="items"></param>
    /// <param name="itemsNum"></param>
    /// <param name="readyItem"></param>
    public static void SendActivityBattleEndReq(int pid , int battle_id , int fubenID, int gateIndex, int gold, int farm, int friendbattle_id, List<int> heros, List<int> items, List<int> itemsNum, int[] readyItem,
        int RecordMaxShuijingNum, int TotalShuijingNum, int RecordMaxXinNum, int TotalXinNum, int RoundMaxHurt, int RoundMaxSparkNum, int TotalSparkNum, int TotalSkillNum , int totalBoxMonster
        )
    {
        ActivityBattleEndPktReq req = new ActivityBattleEndPktReq();
        req.m_iPid = pid;
        req.m_iBattleID = battle_id;
        req.m_iFubenID = fubenID;
        req.m_iGateIndex = gateIndex;
        req.m_iGold = gold;
        req.m_iFarm = farm;
        req.m_iFriendBattleID = friendbattle_id;
        req.m_lstHero = heros;
        req.m_lstItem = items;
        req.m_lstItemNum = itemsNum;
        req.m_vecConsume = readyItem;

        //统计
        req.m_iRecordMaxShuijingNum = RecordMaxShuijingNum;
        req.m_iTotalShuijingNum = TotalShuijingNum;
        req.m_iRecordMaxXinNum = RecordMaxXinNum;
        req.m_iTotalXinNum = TotalXinNum;
        req.m_iRoundMaxHurt = RoundMaxHurt;
        req.m_iRoundMaxSparkNum = RoundMaxSparkNum;
        req.m_iTotalSparkNum = TotalSparkNum;
        req.m_iTotalSkillNum = TotalSkillNum;
        req.m_iTotalBoxMonster = totalBoxMonster;

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送英雄锁定数据请求
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="heros"></param>
    public static void SendHeroLockReq(int pid, List<int> heros)
    {
        HeroLockPktReq req = new HeroLockPktReq();
        req.m_iPid = pid;
        req.m_lstHeros = heros;

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发视英雄解锁数据请求
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="heros"></param>
    public static void SendHeroUnlockReq(int pid, List<int> heros)
    {
        HeroUnlockPktReq req = new HeroUnlockPktReq();
        req.m_iPid = pid;
        req.m_lstHeros = heros;

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送获得特殊副本优惠类型请求
    /// </summary>
    /// <param name="pid"></param>
    public static void SendActivityFubenFavourableEeq(int pid)
    {
        ActivityFubenFavourablePktReq req = new ActivityFubenFavourablePktReq();
        req.m_iPid = pid;

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    //发送获取好友列表//
    public static void SendFriendGetListReq(int pid)
    {
        FriendGetListPktReq req = new FriendGetListPktReq();
        req.m_iPID = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    //发送好友标志喜欢//
    public static void SendFriendLockLikeReq(int pid, List<int> lstFriendPID)
    {
        FriendLockLikePktReq req = new FriendLockLikePktReq();
        req.m_iPID = pid;
        req.m_lstFriendPID = lstFriendPID;

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    //发送好友喜欢解锁//
    public static void SendFriendUnlockLikeReq(int pid, List<int> lstFriendPID)
    {
        FriendUnlockLikePktReq req = new FriendUnlockLikePktReq();
        req.m_iPID = pid;
        req.m_lstFriendPID = lstFriendPID;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    //发送好友申请列表//
    public static void SendFriendGetApplyListReq(int pid)
    {
        FriendGetApplyListPktReq req = new FriendGetApplyListPktReq();
        req.m_iPID = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    //答应好友申请
    public static void SendFriendAcceptApply(int pid, int fPid)
    {
        FriendAcceptApplyPktReq req = new FriendAcceptApplyPktReq();
        req.m_iPID = pid;
        req.m_iFriendPID = fPid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    //好友查找//
    public static void SendFriendFind(int pid, int fPid)
    {
        FriendFindPktReq req = new FriendFindPktReq();
        req.m_iPID = pid;
        req.m_iFriendPID = fPid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    //好友申请
    public static void SendFriendApply(int pid, int fPid)
    {
        FriendApplyPktReq req = new FriendApplyPktReq();
        req.m_iPID = pid;
        req.m_iFriendPID = fPid;

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    //好友取消//
    public static void SendFriendCancelApply(int pid, int fPid)
    {
        FriendCancelApplyPktReq req = new FriendCancelApplyPktReq();
        req.m_iPID = pid;
        req.m_iFriendPID = fPid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    //好友删除//
    public static void SendFriendDel(int pid, int fPid)
    {
        FriendDelPktReq req = new FriendDelPktReq();
        req.m_iPID = pid;
        req.m_iFriendPID = fPid;

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送获取邮件信息列表
    /// </summary>
    /// <param name="pid"></param>
    public static void SendPlayerGetSystemMail(int pid)
    {
        PlayerGetSystemMailPktReq req = new PlayerGetSystemMailPktReq();
        req.m_iPlayerId = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    //发送好友礼物列表//
    public static void SendFriendGetGiftList(int pid)
    {
        FriendGetGiftListPktReq req = new FriendGetGiftListPktReq();
        req.m_iPID = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    //接受好友礼物//
    public static void SendFriendAcceptGift(int pid, int[] vecWants , List<int> lstGifts)
    {
        FriendAcceptGiftPktReq req = new FriendAcceptGiftPktReq();
        req.m_iPID = pid;
        req.m_vecWantsGift = vecWants;
        req.m_lstFriendGifts = lstGifts;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    //赠送好友礼物//
    public static void SendFriendSendGift(int pid, List<FriendSendData> lstFriendSendData)
    {
        FriendSendGiftPktReq req = new FriendSendGiftPktReq();
        req.m_iPID = pid;
        req.m_lstFriendSendData = lstFriendSendData;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送招待ID请求数据
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="zhaodaiID"></param>
    public static void SendGuestZhaoDaiReq(int pid, string zhaodaiID)
    {
        GuestZhaoDaiPktReq req = new GuestZhaoDaiPktReq();
        req.m_iPid = pid;
        req.m_strZhaoDaiId = zhaodaiID;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    //希望好友礼物//
    public static void SendFriendWantGift(int pid, int[] wantGifts)
    {
        FriendWantGiftPktReq req = new FriendWantGiftPktReq();
        req.m_iPID = pid;
        req.m_iWantGiftIDs = wantGifts;
        SessionManager.GetInstance().SendReady(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送接收系统礼物请求
    /// </summary>
    /// <param name="pid"></param>
    public static void SendPlayerReceiveSystemGift(int pid, string giftIds)
    {
        PlayerReceiveSystemEmailPktReq req = new PlayerReceiveSystemEmailPktReq();
        req.m_iPlayerId = pid;
        req.m_strGiftIds = giftIds;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送竞技场基本信息请求
    /// </summary>
    /// <param name="pid"></param>
    public static void SendPVPInfoGetReq(int pid)
    {
        PVPInfoGetPktReq req = new PVPInfoGetPktReq();
        req.m_iPid = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送竞技场详细信息请求
    /// </summary>
    /// <param name="pid"></param>
    public static void SendPVPDetailGetReq(int pid)
    {
        PVPDetailGetPktReq req = new PVPDetailGetPktReq();
        req.m_iPid = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送竞技场对手获取请求
    /// </summary>
    /// <param name="pid"></param>
    public static void SendPVPEnemyGetReq(int pid)
    {
        PVPEnemyGetPktReq req = new PVPEnemyGetPktReq();
        req.m_iPid = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送竞技场5位对手获取请求
    /// </summary>
    /// <param name="pid"></param>
    public static void SendPVPEnemyGet5Req(int pid)
    {
        PVPEnemyGet5PktReq req = new PVPEnemyGet5PktReq();
        req.m_iPid = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 获取竞技场排行信息请求
    /// </summary>
    /// <param name="pid"></param>
    public static void SendPVPBattleRankReq(int pid)
    {
        PVPBattleRankGetPktReq req = new PVPBattleRankGetPktReq();
        req.m_iPid = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送PVP战斗开始
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="tpid"></param>
    public static void SendPVPBattleStart(int pid, int tpid)
    {
        PVPBattleStartPktReq req = new PVPBattleStartPktReq();
        req.m_iPid = pid;
        req.m_iTpid = tpid;

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送战斗结束
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="battle_id"></param>
    /// <param name="result"></param>
    public static void SendPVPBattleEnd(int pid , int tpid , int battle_id, int result ,
        List<int> killHeros , int totalSkillKillNum , int totalSuperHurt , int roundMaxHurt , int roundMaxSpark ,
        int totalHurt , int totalRecover , int totalSpark , int totalSkillNum , int winType
        )
    {

        PVPBattleEndPktReq req = new PVPBattleEndPktReq();
        req.m_iPid = pid;
        req.m_iTpid = tpid;
        req.m_iBattleID = battle_id;
        req.m_iResult = result;
        req.m_lstKillHeroTableID = killHeros;
        req.m_iTotalSkillKillNum = totalSkillKillNum;
        req.m_iTotalSuperHurt = totalSuperHurt;
        req.m_iRoundMaxHurt = roundMaxHurt;
        req.m_iRoundMaxSpark = roundMaxSpark;
        req.m_iTotalHurt = totalHurt;
        req.m_iTotalRecover = totalRecover;
        req.m_iTotalSpark = totalSpark;
        req.m_iTotalSkillNum = totalSkillNum;
        req.m_iWinType = winType;

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送周排行获取接口请求
    /// </summary>
    /// <param name="pid"></param>
    public static void SendPVPWeekRankReq(int pid)
    {
        PVPWeekRankGetPktReq req = new PVPWeekRankGetPktReq();
        req.m_iPid = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送推送请求
    /// </summary>
    public static void SendSystemPush( int pid )
    {
        SystemPushPktReq req = new SystemPushPktReq();
        req.m_iPID = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送副本剧情设置
    /// </summary>
    /// <param name="worldid"></param>
    /// <param name="area_index"></param>
    /// <param name="fuben_index"></param>
    /// <param name="gate_index"></param>
    public static void SendFubenStory( int pid , int worldid, int area_index, int fuben_index, int gate_index)
    {
        FubenStoryPktReq req = new FubenStoryPktReq();
        req.m_iPID = pid;
        req.m_iWorldID = worldid;
        req.m_iAreaIndex = area_index;
        req.m_iFubenIndex = fuben_index;
        req.m_iGateIndex = gate_index;

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送获取商场钻石价格
    /// </summary>
    public static void SendStoreDiamondPrice()
    {
        StoreDiamondGetPktReq req = new StoreDiamondGetPktReq();

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送战绩请求
    /// </summary>
    /// <param name="pid"></param>
    public static void SendBattleRecord(int pid)
    {
        PlayerBattleRecordGetPktReq req = new PlayerBattleRecordGetPktReq();
        req.m_iPid = pid;

        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送新手引导
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="guide_step"></param>
    public static void SendGuideStep(int pid, int guide_step)
    {
        GuideStepPktReq req = new GuideStepPktReq();
        req.m_iPID = pid;
        req.m_iGuideID = guide_step;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送战斗复活
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="num"></param>
    public static void SendBattleRelive(int pid, int num)
    {
        BattleRelivePktReq req = new BattleRelivePktReq();
        req.m_iPID = pid;
        req.m_iReliveNum = num;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送战斗关卡失败
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="vecItemNum"></param>
    public static void SendBattleGateFail(int pid , int battle_id , int[] vecItemNum)
    {
        BattleGateFailPktReq req = new BattleGateFailPktReq();
        req.m_iPID = pid;
        req.m_iBattleID = battle_id;
        req.m_vecReadyItemNum = vecItemNum;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送活动战斗失败
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="vecItemNum"></param>
    public static void SendActivityBattleFail(int pid , int battle_id , int[] vecItemNum)
    {
        ActivityBattleFailPktReq req = new ActivityBattleFailPktReq();
        req.m_iPID = pid;
        req.m_iBattleID = battle_id;
        req.m_vecReadyItemNum = vecItemNum;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }


    /// <summary>
    /// 发送游戏加入数据
    /// </summary>
    /// <param name="deviceID"></param>
    /// <param name="channelName"></param>
    public static void SendGameJoin(string deviceID, string channelName)
    {
        GameJoinPktReq req = new GameJoinPktReq();
        req.m_strDeviceID = deviceID;
        req.m_strChannelName = channelName;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 支付请求
    /// </summary>
    /// <param name="payNum"></param>
    /// <param name="payid"></param>
    public static void SendPay( int pid , int good_id)
    {
        PayPktReq req = new PayPktReq();
        req.m_iGoodID = good_id;
        req.m_iPID = pid;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// IOS支付验证
    /// </summary>
    /// <param name="payid"></param>
    /// <param name="verify"></param>
    public static void SendPayIOSVerify(int pid ,int payid, string verify)
    {
        PayIOSVerifyPktReq req = new PayIOSVerifyPktReq();
        req.m_iPID = pid;
        req.m_iPayID = payid;
        req.m_strVerify = verify;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// IOS PP助手验证
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="payid"></param>
    /// <param name="verify"></param>
    public static void SendPayIOSPPVerify(int pid, int payid)
    {
        PayIOSPPVerifyPktReq req = new PayIOSPPVerifyPktReq();
        req.m_iPID = pid;
        req.m_iPayID = payid;
        //req.m_strVerify = verify;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 发送PP助手登录
    /// </summary>
    public static void SendAccountIOSPPLogin( int uid , string token )
    {
        AccountLoginIOSPPPktReq req = new AccountLoginIOSPPPktReq();
        req.m_iPPUid = uid;
        req.m_strToken = token;
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }

    /// <summary>
    /// 获取唯一ID
    /// </summary>
    public static void SendGetDeviceID()
    {
        DevicePktReq req = new DevicePktReq();
        SessionManager.GetInstance().Send(SESSION_DEFINE.LOGIN_SESSION, req);
    }
}