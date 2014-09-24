using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Network;
using UnityEngine;

//  ItemCombineHandle.cs
//  Author: sanvey
//  2013-12-24

//物品合成请求应答句柄
public class ItemCombineHandle
{
    /// <summary>
    /// 获得Action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.ITEM_COMBINED_REQ;
    }

    /// <summary>
    /// 执行句柄
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        ItemCombinePktAck ack = (ItemCombinePktAck)packet;

        GAME_LOG.LOG("code :" + ack.header.code);
        GAME_LOG.LOG("desc :" + ack.header.desc);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            return;
        }

        //返回Code 0表示成功
        if (ack.header.code == 0)
        {
            //清空零时
            Role.role.GetItemProperty().RemoveTmpItem();

            foreach (var q in ack.m_lstItems)
            {
                Item addtmp = new Item(q.m_iItem_id);
                addtmp.m_iID = q.m_iId;
                addtmp.m_iNum = q.m_iItem_num;
                addtmp.m_bNew = true;

                Role.role.GetItemProperty().AddItem(addtmp);  //加入客户端物品数据
                new ItemBook().AddItem(q.m_iItem_id); //物品图鉴更新

                if (addtmp.m_eType== ITEM_TYPE.EQUIP)  //如果有新合成装备，英雄装备界面提示有新装备可以装备
                {
                    GAME_SETTING.s_iWarnHeroEquip = 1;
                    GAME_SETTING.SaveWarnHeroEquip();
                }
            }

            foreach (int id in ack.m_lstDeleItemIDs)
            {
                Role.role.GetItemProperty().DeleteItem(id);
            }

            //test
            //foreach (Item item in Role.role.GetItemProperty().GetAllItem())
            //{
            //    if (item.m_iID < 10)
            //    {
            //        Debug.LogError("ID:  " + item.m_iID + "  TableID:  " + item.m_iTableID + "  Num:  " + item.m_iNum);
            //    }
            //}
        }

        //更新临时显示虚拟数据，本地操作不会对真实数据改动，等待服务器返回真实有效数据
        foreach (Item item in Role.role.GetItemProperty().GetAllItem())
        {
            item.m_iDummyNum = item.m_iNum;
        }

        SessionManager.GetInstance().CallBack();

        return;
    }
}