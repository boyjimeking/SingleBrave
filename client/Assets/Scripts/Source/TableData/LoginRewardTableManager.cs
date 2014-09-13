using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Base;
using Game.Resource;
using UnityEngine;

//累计登录奖励表管理
//Author sunyi
//2014-03-07
public class LoginRewardTableManager : Singleton<LoginRewardTableManager>
{
    private List<LoginRewardTable> m_lstRewardTable = new List<LoginRewardTable>();    //登录奖励表列表

    public LoginRewardTableManager()
    {
#if GAME_TEST_LOAD
        //加载累计登录奖励表
        LoadText(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.LOGIN_REWARD) as string);
#endif
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    public void LoadText(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstRewardTable.Clear();

            for (; index < lst.Count; )
            {
                LoginRewardTable rewardTable = new LoginRewardTable();
                rewardTable.ReadText(lst, ref index);
                this.m_lstRewardTable.Add(rewardTable);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "累计登录奖励表错误");
        }

    }

    /// <summary>
    /// 获取所有表 
    /// </summary>
    /// <returns></returns>
    public List<LoginRewardTable> GetAll()
    {
        return new List<LoginRewardTable>(this.m_lstRewardTable);
    }

}

