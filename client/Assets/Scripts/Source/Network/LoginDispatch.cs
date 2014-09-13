//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//
//using UnityEngine;
//using Game.Network;
//
////  LoginDispatch.cs
////  Author: Lu Zexi
////  2013-11-12
//
//
//
///// <summary>
///// 登录调度类
///// </summary>
//public class LoginDispatch : HTTPDispatch
//{
//    public LoginDispatch()
//    {
//        RegistFactory(new AccountBoundPktAckFactory());
//        RegistHandle(new AccountBoundHandle());
//
//        RegistFactory(new AccountAutoRegistPktAckFactory());
//        RegistHandle(new AccountAutoRegistHandle());
//
//        RegistHandle(new AccountLoginHandle());
//        RegistFactory(new AccountLoginPktAckFactory());
//
//        RegistHandle(new PlayerInfoGetHandle());
//        RegistFactory(new PlayerInfoGetPktAckFactory());
//
//        RegistHandle(new PlayerCreateHandle());
//        RegistFactory(new PlayerCreatePktAckFactory());
//
//        RegistHandle(new PlayerHeroInfoHandle());
//        RegistFactory(new PlayerHeroInfoPktAckFactory());
//
//        RegistHandle(new TeamInfoGetHandle());
//        RegistFactory(new TeamInfoGetPktAckFactory());
//
//        RegistHandle(new VersionHandle());
//        RegistFactory(new VersionAckPktFactory());
//
//        RegistHandle(new PlayerTaskInfoHandle());
//        RegistFactory(new PlayerTaskInfoPktAckFactory());
//
//        RegistHandle(new BuildingInfoGetHandle());
//        RegistFactory(new BuildingInfoGetPktAckFactory());
//
//        RegistHandle(new FriendPointSummonHandle());
//        RegistFactory(new FriendPointSummonPktAckFactory());
//
//        RegistHandle(new MoneySummonHandle());
//        RegistFactory(new MoneySummonPktAckFactory());
//
//        RegistHandle(new BattleGateStartHandle());
//        RegistFactory(new BattleGateStartFactory());
//
//        RegistHandle(new BattleGateEndHandle());
//        RegistFactory(new BattleGateEndFactory());
//
//        RegistHandle(new HeroSellHandle());
//        RegistFactory(new HeroSellPktAckFactory());
//        RegistHandle(new TeamEditorHandle());
//        RegistFactory(new TeamEditorPktAckFactory());
//
//        RegistHandle(new HeroEquipUpdateHandle());
//        RegistFactory(new HeroEquipUpdatePktAckFactory());
//
//        RegistHandle(new FriendFightHandle());
//        RegistFactory(new FriendFightPktAckFactory());
//
//        RegistHandle(new ItemGetHandle());
//        RegistFactory(new ItemGetPktAckFactory());
//
//        RegistHandle(new PlayerSignatureUpdateHandle());
//        RegistFactory(new SignatureUpdatePktAckFactory());
//
//        RegistHandle(new ItemCombineHandle());
//        RegistFactory(new ItemCombinePktAckFactory());
//
//        RegistHandle(new ItemCollectHandle());
//        RegistFactory(new ItemCollectPktAckFactory());
//
//        RegistHandle(new HeroUpgradeHandle());
//        RegistFactory(new HeroUpgradePktAckFactory());
//
//        RegistHandle(new BattleItemEditHandle());
//        RegistFactory(new BattleItemEditPktAckFactory());
//
//        RegistHandle(new BattleItemGetHandle());
//        RegistFactory(new BattleItemGetPktAckFactory());
//
//        RegistHandle(new PlayerStrengthRecoverHandle());
//        RegistFactory(new StrengthRecoverPktAckFactory());
//
//        RegistHandle(new PlayerPropsSlotExpansionHandle());
//        RegistFactory(new PropsSlotExpansionPktAckFactory());
//
//        RegistHandle(new HeroUnitSlotExpansionHandle());
//        RegistFactory(new UnitSlotExpansionPktAckFactory());
//
//        RegistHandle(new PlayerSportPointRecoverHandle());
//        RegistFactory(new SportPointRecoverPktAckFactory());
//
//        RegistHandle(new BuildingUpdateHandle());
//        RegistFactory(new BuildingUpdatePktAckFactory());
//
//        RegistHandle(new HeroBookHandle());
//        RegistFactory(new HeroBookPktFactory());
//
//        RegistHandle(new ItemBookHandle());
//        RegistFactory(new ItemBookPktFactory());
//
//        RegistHandle(new HeroEvolutionHandle());
//        RegistFactory(new HeroEvolutionPktAckFactory());
//
//        RegistHandle(new ItemSellHandle());
//        RegistFactory(new ItemSellPktAckFactory());
//
//        RegistHandle(new ActivityBattleStartHandle());
//        RegistFactory(new ActivityBattleStartPktFactory());
//
//        RegistHandle(new ActivityBattleEndHandle());
//        RegistFactory(new ActivityBattleEndPktFactory());
//
//        RegistHandle(new HeroLockHandle());
//        RegistFactory(new HeroLockPktAckFactory());
//
//        RegistHandle(new HeroUnlockHandle());
//        RegistFactory(new HeroUnlockPktAckFactory());
//
//        RegistHandle(new ActivityFubenFavourableHandle());
//        RegistFactory(new ActivityFubenFavourablePktAckFactory());
//
//        RegistHandle(new FriendGetListHandle());
//        RegistFactory(new FriendGetListPktAckFactory());
//
//        RegistHandle(new FriendLockLikeHandle());
//        RegistFactory(new FriendLockLikePktAckFactory());
//
//        RegistHandle(new FriendUnlockLikeHandle());
//        RegistFactory(new FriendUnlockLikePktAckFactory());
//
//        RegistHandle(new FriendGetApplyListHandle());
//        RegistFactory(new FriendGetApplyListPktAckFactory());
//
//        RegistHandle(new FriendAcceptApplyHandle());
//        RegistFactory(new FriendAcceptApplyPktAckFactory());
//
//        RegistHandle(new FriendFindHandle());
//        RegistFactory(new FriendFindPktAckFactory());
//
//        RegistHandle(new FriendApplyHandle());
//        RegistFactory(new FriendApplyPktAckFactory());
//
//        RegistHandle(new FriendCancelApplyHandle());
//        RegistFactory(new FriendCancelApplyPktAckFactory());
//
//        RegistHandle(new FriendDelHandle());
//        RegistFactory(new FriendDelPktAckFactory());
//
//        RegistHandle(new FriendGetGiftListHandle());
//        RegistFactory(new FriendGetGiftListPktAckFactory());
//
//        RegistHandle(new FriendAcceptGiftHandle());
//        RegistFactory(new FriendAcceptGiftPktAckFactory());
//
//        RegistHandle(new FriendSendGiftHandle());
//        RegistFactory(new FriendSendGiftPktAckFactory());
//
//        RegistHandle(new PlayerGetSystemMailHandle());
//        RegistFactory(new PlayerGetSystemMailPktAckFactory());
//
//        RegistHandle(new GuestZhaoDaiHandle());
//        RegistFactory(new GuestZhaoDaiPktAckFactory());
//
//        RegistHandle(new FriendWantGiftHandle());
//        RegistFactory(new FriendWantGiftPktAckFactory());
//
//        RegistHandle(new PlayerReceiveSystemMailHandle());
//        RegistFactory(new PlayerReceiveSystemMailPktAckFactory());
//
//        RegistHandle(new PVPInfoGetHandle());
//        RegistFactory(new PVPInfoGetPktAckFactory());
//
//        RegistHandle(new PVPEnemyGetHandle());
//        RegistFactory(new PVPEnemyGetPktAckFactory());
//
//        RegistHandle(new PVPDetailGetHandle());
//        RegistFactory(new PVPDetailGetPktAckFactory());
//
//        RegistHandle(new PVPBattleRankGetHandle());
//        RegistFactory(new PVPBattleRankGetPktAckAckFactory());
//
//        RegistHandle(new PVPBattleStartHandle());
//        RegistFactory(new PVPBattleStartPktAckFactory());
//
//        RegistHandle(new PVPBattleEndHandle());
//        RegistFactory(new PVPBattleEndPktAckFactory());
//
//        RegistHandle(new PVPWeekRankGetHandle());
//        RegistFactory(new PVPWeekRankGetPktAckFactory());
//
//        RegistHandle(new SystemPushHandle());
//        RegistFactory(new SystemPushPktAckFactory());
//
//        RegistHandle(new FubenStoryHandle());
//        RegistFactory(new FubenStoryPktAckFactory());
//
//        RegistHandle(new StoreDiamonGetHandle());
//        RegistFactory(new StoreDiamonGetPktAckFactory());
//
//        RegistHandle(new PlayerBattleRecordHandle());
//        RegistFactory(new PlayerBattleRecordGetPktAckFactory());
//
//        RegistHandle(new PVPEnemyGet5Handle());
//        RegistFactory(new PVPEnemyGet5PktAckFactory());
//
//        RegistHandle(new BattleReliveHandle());
//        RegistFactory(new BattleRelivePktAckFactory());
//
//        RegistHandle(new BattleGateFailHandle());
//        RegistFactory(new BattleGateFailPktAckFactory());
//
//        RegistHandle(new ActivityBattleFailHandle());
//        RegistFactory(new ActivityBattleFailPktAckFactory());
//
//        RegistHandle(new GameJoinHandle());
//        RegistFactory(new GameJoinPktAckFactory());
//
//        RegistHandle(new DeviceHandle());
//        RegistFactory(new DevicePktAckFactory());
//
//        RegistHandle(new PayHandle());
//        RegistFactory(new PayPktAckFactory());
//
//        RegistHandle(new PayIOSVerifyHandle());
//        RegistFactory(new PayIOSVerifyPktAckFactory());
//
//        RegistFactory(new AccountLoginIOSPPPktFactory());
//        RegistHandle(new AccountIOSPPLoginHandle());
//
//        RegistFactory(new PayIOSPPVerifyPktFactory());
//        RegistHandle(new PayIOSPPVerifyHandle());
//    }
//
//    /// <summary>
//    /// 数据错误事件
//    /// </summary>
//    public override void OnDataError()
//    {
//        GUI_FUNCTION.MESSAGEL(this.m_cSession.ReSend, "网络数据错误");
//        Debug.LogError("OnDataError");
//        return;
//    }
//
//
//    /// <summary>
//    /// 断开连接事件
//    /// </summary>
//    public override void OnDisconnect()
//    {
//        GUI_FUNCTION.MESSAGEL(this.m_cSession.ReSend, "网络信号不足,将重新尝试连接");
//        Debug.LogError("OnDisconnect");
//    }
//
//    /// <summary>
//    /// 超时事件
//    /// </summary>
//    public override void OnTimeOut()
//    {
//        GUI_FUNCTION.MESSAGEL(this.m_cSession.ReSend, "网络信号不足,将重新尝试连接");
//        Debug.LogError("OnTimeOut");
//    }
//
//}
//
