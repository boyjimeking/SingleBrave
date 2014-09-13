using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  PayProperty.cs
//  Author: Lu Zexi
//  2014-04-02



/// <summary>
/// 支付属性
/// </summary>
public class PayProperty
{
    public int m_iState;    //支付状态
    public int m_iPayID;    //支付ID
    public int m_iGoodID;   //商品ID
    public int m_iPayNum;   //砖石数量
    public int m_iPrice;    //价格
    public string m_strTypeID; //类型ID
    public string m_strVerify;  //验证码
}
