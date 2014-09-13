using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//  MailProperty.cs
//  Author: Lu Zexi
//  2013-11-27

/// <summary>
/// 邮件属性
/// </summary>
public class MailProperty
{
    private List<Mail> m_lstEmail = new List<Mail>(); //邮件

    public MailProperty()
    {
    }

    /// <summary>
    /// 获取所有邮件
    /// </summary>
    /// <returns></returns>
    public List<Mail> GetAll()
    {
        return new List<Mail>(this.m_lstEmail);
    }


    /// <summary>
    /// 获取指定EMAIL
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Mail GetEmail(int id)
    {
        foreach (Mail item in this.m_lstEmail)
        {
            if (item.m_iID == id)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// 增加Email
    /// </summary>
    /// <param name="email"></param>
    public void AddEmail(Mail email)
    {
        this.m_lstEmail.Add(email);
    }


    /// <summary>
    /// 删除所有邮件
    /// </summary>
    public void ClearMails()
    {
        if (this.m_lstEmail != null)
        {
            this.m_lstEmail.Clear();
        }
    }
}
