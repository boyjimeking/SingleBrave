using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//商城-钻石价格 属性
//Author sunyi
//2014-02-28
public class StoreDiamondProperty
{
    private List<StoreDiamondPrice> m_lstDiamondPrice = new List<StoreDiamondPrice>();//列表

    public StoreDiamondProperty() { }

    /// <summary>
    /// 获取钻石价格列表
    /// </summary>
    /// <returns></returns>
    public List<StoreDiamondPrice> GetAll()
    {
        return new List<StoreDiamondPrice>(this.m_lstDiamondPrice);
    }

    public void AddDiamondPrice(StoreDiamondPrice price)
    {
        this.m_lstDiamondPrice.Add(price);
    }

    /// <summary>
    /// 清空
    /// </summary>
    public void ClearAll()
    {
        if (this.m_lstDiamondPrice != null)
        {
            this.m_lstDiamondPrice.Clear();
        }
        
    }

}

