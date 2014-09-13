using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


//  GuideTable.cs
//  Author: Lu Zexi
//  2014-02-17



/// <summary>
/// 新手引导表
/// </summary>
public class GuideTable : TableBase
{
    private int m_iStepID;  //ID
    public int StepID
    {
        get { return this.m_iStepID; }
    }
    private int m_iMID;   //展示的模块ID
    public int MID
    {
        get { return this.m_iMID; }
    }
    private float m_fTimeScale; //时间缩放
    public float TimeScale
    {
        get { return this.m_fTimeScale; }
    }
    private int m_iButton; //按钮类型
    public int Button
    {
        get { return this.m_iButton; }
    }
    private float m_fOffsetX;   //偏移量x
    public float OffsetX
    {
        get { return this.m_fOffsetX; }
    }
    private float m_fOffsetY;   //偏移量y
    public float OffsetY
    {
        get { return this.m_fOffsetY; }
    }
    private int m_iButtonMask;  //按钮遮挡
    public int ButtonMask
    {
        get { return this.m_iButtonMask; }
    }
    private float m_fButtonAlpha; //透明度
    public float ButtonAlpha
    {
        get { return this.m_fButtonAlpha; }
    }
    private float m_fBtnWidth; //按钮宽度
    public float ButtonWidth
    {
        get { return this.m_fBtnWidth; }
    }
    private float m_fBtnLength; //按钮长度
    public float ButtonLength
    {
        get { return this.m_fBtnLength; }
    }
    private float m_fBtnX;  //按钮X
    public float BtnX
    {
        get { return this.m_fBtnX; }
    }
    private float m_fBtnY;  //按钮Y
    public float BtnY
    {
        get { return this.m_fBtnY; }
    }
    private bool m_bDelay;  //是否延迟下一步
    public bool Delay
    {
        get { return this.m_bDelay; }
    }
    private float m_fDelayTime; //倒计时时间
    public float DelayTime
    {
        get { return this.m_fDelayTime; }
    }
    private bool m_bIsTip;    //是否提示
    public bool IsTip
    {
        get { return this.m_bIsTip; }
    }
    private float m_fTipX; //提示坐标X
    public float TipX
    {
        get { return this.m_fTipX; }
    }
    private float m_fTipY; //提示坐标Y
    public float TipY
    {
        get { return this.m_fTipY; }
    }
    private string m_strTip;    //提示文字
    public string Tip
    {
        get { return this.m_strTip; }
    }
    private string m_strImg;    //图片展示
    public string Img
    {
        get { return this.m_strImg; }
    }
    private float m_fImgX;   //图片X
    public float ImgX
    {
        get { return this.m_fImgX; }
    }
    private float m_fImgY;  //图片Y
    public float ImgY
    {
        get { return this.m_fImgY; }
    }
    private List<Vector3> m_lstPoint1Pos = new List<Vector3>(); //指示针1落点
    public List<Vector3> Point1Pos
    {
        get { return this.m_lstPoint1Pos; }
    }
    private List<string> m_lstPoint1Txt = new List<string>();   //指示针1文字
    public List<string> Point1Txt
    {
        get { return this.m_lstPoint1Txt; }
    }
    private List<Vector3> m_lstPoint2Pos = new List<Vector3>(); //指示针1落点
    public List<Vector3> Point2Pos
    {
        get { return this.m_lstPoint2Pos; }
    }
    private List<string> m_lstPoint2Txt = new List<string>();   //指示针1文字
    public List<string> Point2Txt
    {
        get { return this.m_lstPoint2Txt; }
    }
    private List<Vector3> m_lstPoint3Pos = new List<Vector3>(); //指示针1落点
    public List<Vector3> Point3Pos
    {
        get { return this.m_lstPoint3Pos; }
    }

    public GuideTable()
    { 
        //
    }


    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="lstInfo"></param>
    /// <param name="index"></param>
    public override void ReadText(List<string> lstInfo, ref int index)
    {
        this.m_iStepID = INT(lstInfo[index++]);
        this.m_iMID = INT(lstInfo[index++]);
        this.m_fTimeScale = FLOAT(lstInfo[index++]);
        this.m_iButton = INT(lstInfo[index++]);
        this.m_fOffsetX = FLOAT(lstInfo[index++]);
        this.m_fOffsetY = FLOAT(lstInfo[index++]);
        this.m_iButtonMask = INT(lstInfo[index++]);
        this.m_fButtonAlpha = FLOAT(lstInfo[index++]);
        this.m_fBtnWidth = FLOAT(lstInfo[index++]);
        this.m_fBtnLength = FLOAT(lstInfo[index++]);
        this.m_fBtnX = FLOAT(lstInfo[index++]);
        this.m_fBtnY = -FLOAT(lstInfo[index++]);
        this.m_bDelay = BOOL(lstInfo[index++]);
        this.m_fDelayTime = FLOAT(lstInfo[index++]);
        this.m_bIsTip = BOOL(lstInfo[index++]);
        this.m_fTipX = FLOAT(lstInfo[index++]);
        this.m_fTipY = FLOAT(lstInfo[index++]);
        this.m_strTip = STRING(lstInfo[index++]);
        this.m_strImg = STRING(lstInfo[index++]);
        this.m_fImgX = FLOAT(lstInfo[index++]);
        this.m_fImgY = FLOAT(lstInfo[index++]);
        string point1 = STRING(lstInfo[index++]);
        string[] vecPoint1 = point1.Split('|');
        this.m_lstPoint1Pos.Clear();
        this.m_lstPoint1Txt.Clear();
        for (int i = 0; i < vecPoint1.Length; i++)
        {
            if (vecPoint1[i] != "")
            {
                string[] arg = vecPoint1[i].Split(',');
                this.m_lstPoint1Pos.Add( new Vector3(float.Parse(arg[0]) , -float.Parse(arg[1]) , 0));
                this.m_lstPoint1Txt.Add(arg[2]);
            }
        }
        string point2 = STRING(lstInfo[index++]);
        string[] vecPoint2 = point2.Split('|');
        this.m_lstPoint2Pos.Clear();
        this.m_lstPoint2Txt.Clear();
        for (int i = 0; i < vecPoint2.Length; i++)
        {
            if (vecPoint2[i] != "")
            {
                string[] arg = vecPoint2[i].Split(',');
                this.m_lstPoint2Pos.Add(new Vector3(float.Parse(arg[0]), -float.Parse(arg[1]), 0));
                this.m_lstPoint2Txt.Add(arg[2]);
            }
        }
        string point3 = STRING(lstInfo[index++]);
        string[] vecPoint3 = point3.Split('|');
        this.m_lstPoint3Pos.Clear();
        for (int i = 0; i < vecPoint3.Length; i++)
        {
            if (vecPoint3[i] != "")
            {
                string[] arg = vecPoint3[i].Split(',');
                this.m_lstPoint3Pos.Add(new Vector3(float.Parse(arg[0]), -float.Parse(arg[1]), 0));
            }
        }
    }

}
