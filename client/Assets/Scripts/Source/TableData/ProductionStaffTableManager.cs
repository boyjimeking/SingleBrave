using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Base;
using Game.Resource;
using UnityEngine;

public class ProductionStaffTableManager : Singleton<ProductionStaffTableManager>
{
    private List<ProductionStaffTable> m_lstProductionStaffList = new List<ProductionStaffTable>(); //制作人员列表
    public ProductionStaffTableManager()
    {
#if GAME_TEST_LOAD
        //加载制作人员列表
        LoadText((string)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_TABLE_PATH, TABLE_DEFINE.PRODUCTION_STAFF));
#endif
    }

    public void LoadText(string content)
    {
        try
        {
            this.m_lstProductionStaffList.Clear();
            List<string> lst = TABLE_READER.LOAD_CSV(content);
            lst = TABLE_READER.SPLIT_EMPTY(lst);
            int index = 0;
            for (; index < lst.Count; )
            {
                ProductionStaffTable ps = new ProductionStaffTable();
                ps.ReadText(lst, ref index);
                m_lstProductionStaffList.Add(ps);
            }
        }
        catch (System.Exception ex)
        {
            GUI_FUNCTION.MESSAGEL(Application.Quit, "制作人员配置表错误");
        }
    }

    public List<ProductionStaffTable> GetAll()
    {
        return this.m_lstProductionStaffList;
    }
}
