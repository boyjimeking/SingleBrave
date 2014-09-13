using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;
using Game.Base;



/// <summary>
/// 系统推送句柄
/// </summary>
public class SystemPushHandle : HTTPHandleBase
{
    public SystemPushHandle()
        : base()
    { 
        //
    }

    /// <summary>
    /// 获取ACTION
    /// </summary>
    /// <returns></returns>
    public override string GetAction()
    {
        return PACKET_DEFINE.SYSTEM_PUSH_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public override bool Excute(HTTPPacketBase packet)
    {
        SystemPushPktAck ack = packet as SystemPushPktAck;

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.m_iErrorCode != 0)
        {
            GUI_FUNCTION.MESSAGEM(null, ack.m_strErrorDes);
            return false;
        }

        //更新top显示
        Role.role.GetBaseProperty().m_iDiamond = ack.m_iDiamond;
        GUIBackFrameTop top = (GUIBackFrameTop)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP);
        top.UpdateDiamond(ack.m_iDiamond);
        //初始化体力和竞技点数据  
        //所有状态都只有m_fSportNext和m_fStrengthNext两个字段对体力和竞技点进行维护
        //m_fSportNext  到达满体力剩余的时间
        //m_fStrengthNext  到达满竞技点剩余的时间
        //在重新和服务器对体力和体力10分钟剩余时间 进行同步的时候，相当于重走一遍登陆接口，对唯一计算剩余值重新更新一次
        top.m_bIsUpdateIng = true;
        Role.role.GetBaseProperty().m_iSportPoint = ack.m_iSportPoint;
        Role.role.GetBaseProperty().m_iStrength = ack.m_iStrength;
         Role.role.GetBaseProperty().m_iStrengthTime=ack.m_iStrengthTime;
         Role.role.GetBaseProperty().m_iSportTime = ack.m_iSportTime;

        Role.role.GetBaseProperty().m_fTopTime = GAME_TIME.TIME_REAL();
        Role.role.GetBaseProperty().m_fTopTimeSport = GAME_TIME.TIME_REAL();
        long secnd = (GAME_DEFINE.m_lServerTime - Role.role.GetBaseProperty().m_iStrengthTime);
        int maxStrength = RoleExpTableManager.GetInstance().GetMaxStrength(Role.role.GetBaseProperty().m_iLevel);
        int nowStrength = Role.role.GetBaseProperty().strength;
        Role.role.GetBaseProperty().m_fStrengthNext = (maxStrength - nowStrength) * GUIBackFrameTop.STRENGTH_PER - (float)secnd;
        long secnd2 = (GAME_DEFINE.m_lServerTime - Role.role.GetBaseProperty().m_iSportTime);
        int maxPVP = 3;
        int nowPVP = Role.role.GetBaseProperty().sportpoint;
        Role.role.GetBaseProperty().m_fSportNext = (maxPVP - nowPVP) * GUIBackFrameTop.PVP_PER - (float)secnd2;
        top.m_bIsUpdateIng = false;

        Role.role.GetBaseProperty().m_iMailCounts = ack.m_iEmailNum;

        GUIMain main = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MAIN) as GUIMain;

        if (main.IsShow())
        {
            main.SetMailCountLabel(Role.role.GetBaseProperty().m_iMailCounts);
        }

        Role.role.GetBaseProperty().m_iFriendApplyCount = ack.m_iApplyNum;
        Role.role.GetBaseProperty().m_iFriendGiftCount = ack.m_iFriendGiftNum;
        Role.role.GetFriendProperty().RemoveAllFriends();
        for (int i = 0; i < ack.m_lstFriendData.Count; i++ )
        {
            Friend friend = ack.m_lstFriendData[i].GetFriend();
            Role.role.GetFriendProperty().AddFriend(friend);
        }

        GUIBackFrameBottom bottom = (GUIBackFrameBottom)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM);
        if (bottom.IsShow())
        { 
            bottom.SetFriendApllyNumber(Role.role.GetBaseProperty().m_iFriendApplyCount + Role.role.GetBaseProperty().m_iFriendGiftCount);
        }

        return true;
    }

}
