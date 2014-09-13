using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Resource;
using UnityEngine;

//帮助表管理类
//Author sunyi
//2014-1-15
public class HelpTableManager : Singleton<HelpTableManager>
{
    public List<HelpTypeTable> m_lstHelpTypeTable = new List<HelpTypeTable>();//帮助类型表
    public List<HelpProjectTable> m_lstHelpProjectTable = new List<HelpProjectTable>();//帮助项目表

    public HelpTableManager()
    {
#if GAME_TEST_LOAD
        //加载帮助类型表
        LoadHelpTypeText((string)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.HELP_TYPE_PATH) as string);
        //加载帮助项目表
        LoadHelpProjectText((string)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.HELP_PROJECT_PATH) as string);
#endif
    }

    /// <summary>
    /// 获取所有帮助类型表
    /// </summary>
    /// <returns></returns>
    public List<HelpTypeTable> GetAllHelpTypeTable()
    {
        return new List<HelpTypeTable>(this.m_lstHelpTypeTable);
    }

    /// <summary>
    /// 获取所有帮助项目表
    /// </summary>
    /// <returns></returns>
    public List<HelpProjectTable> GetAllHelpProjectTable()
    {
        return new List<HelpProjectTable>(this.m_lstHelpProjectTable);
    }

    /// <summary>
    /// 获取帮助类型表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public HelpTypeTable GetHelpTypeTable(int id)
    {
        foreach (HelpTypeTable item in this.m_lstHelpTypeTable)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// 获取帮助项目表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public HelpProjectTable GetHelpProjectTable(int id)
    {
        foreach (HelpProjectTable item in this.m_lstHelpProjectTable)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// 获取一个帮助类型里边的所有帮助项目
    /// </summary>
    /// <param name="typeId"></param>
    /// <returns></returns>
    public List<HelpProjectTable> GetAllHelpProjectWithTypeId(int typeId)
    {
        HelpTypeTable typeTable = HelpTableManager.GetInstance().GetHelpTypeTable(typeId);
        List<HelpProjectTable> lst = new List<HelpProjectTable>();
        for (int i = 0; i < typeTable.VecProject.Length; i++)
        {
            if (typeTable.VecProject[i] != 0)
            {
                HelpProjectTable projectTable = HelpTableManager.GetInstance().GetHelpProjectTable(typeTable.VecProject[i]);
                if (projectTable == null)
                {
                    GAME_LOG.ERROR("The HelpProjectTable is null.");
                    continue;
                }

                lst.Add(projectTable);
            }
        }

        Debug.Log("Count:" + lst.Count);
        return lst;
    }

    /// <summary>
    /// 加载帮助类型表
    /// </summary>
    /// <param name="content"></param>
    public void LoadHelpTypeText(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstHelpTypeTable.Clear();
            for (; index < lst.Count; )
            {
                HelpTypeTable table = new HelpTypeTable();
                table.ReadText(lst, ref index);
                this.m_lstHelpTypeTable.Add(table);
            }
        }
        catch(Exception ex) {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "帮助类型配置表错误");
        }
    }

    /// <summary>
    /// 加载帮助项目表
    /// </summary>
    /// <param name="content"></param>
    public void LoadHelpProjectText(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstHelpProjectTable.Clear();
            for (; index < lst.Count; )
            {
                HelpProjectTable table = new HelpProjectTable();
                table.ReadText(lst, ref index);
                this.m_lstHelpProjectTable.Add(table);
            }
        }
        catch (Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "帮助项目配置表错误");
        }
    }

}

