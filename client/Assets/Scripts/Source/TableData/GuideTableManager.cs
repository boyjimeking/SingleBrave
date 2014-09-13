using System;
using System.Collections.Generic;
using System.Linq;
using Game.Base;
using Game.Resource;
using UnityEngine;


//  GuideTableManager.cs
//  Author: Lu Zexi
//  2014-02-17



/// <summary>
/// 新手引导数据管理类
/// </summary>
public class GuideTableManager : Singleton<GuideTableManager>
{
    private List<GuideTable> m_lstGuide = new List<GuideTable>();   //新手引导数据表

    public GuideTableManager()
    {
#if GAME_TEST_LOAD
        //新手引导
        LoadText(ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.GUIDE_PATH) as string);
#endif
    }

    /// <summary>
    /// 新手引导模块ID是否存在
    /// </summary>
    /// <param name="step"></param>
    /// <returns></returns>
    public bool ExistModelID(int id)
    {
        foreach (GuideTable item in this.m_lstGuide)
        {
            if (item.MID == id)
                return true;
        }
        return false;
    }

    /// <summary>
    /// 是否模块ID最大
    /// </summary>
    /// <param name="modelID"></param>
    /// <returns></returns>
    public bool IsModelIDMax(int modelID)
    {
        foreach (GuideTable item in this.m_lstGuide)
        {
            if (item.MID >= modelID)
                return true;
        }
        return false;
    }

    /// <summary>
    /// 是否存在步骤ID
    /// </summary>
    /// <param name="model_id"></param>
    /// <param name="step_id"></param>
    /// <returns></returns>
    public bool ExistStepID(int model_id, int step_id)
    {
        foreach (GuideTable item in this.m_lstGuide)
        {
            if (item.MID == model_id && item.StepID == step_id)
                return true;
        }
        return false;
    }

    /// <summary>
    /// 获取新手引导数据表
    /// </summary>
    /// <param name="id"></param>
    /// <param name="guiid"></param>
    /// <returns></returns>
    public GuideTable GetGuideTable(int model_id, int step_id)
    {
        foreach (GuideTable item in this.m_lstGuide)
        {
            if (item.StepID == step_id && item.MID == model_id)
                return item;
        }
        return null;
    }

    /// <summary>
    /// 加载数据
    /// </summary>
    /// <param name="content"></param>
    public void LoadText(string content)
    {
        try
        {
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            int index = 0;
            this.m_lstGuide.Clear();

            for (; index < lst.Count; )
            {
                GuideTable table = new GuideTable();
                table.ReadText(lst, ref index);
                this.m_lstGuide.Add(table);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "新手引导表错误");
        }
    }

}
