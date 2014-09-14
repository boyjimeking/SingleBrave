using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Game.Resource;
using Game.Base;

//  GUIGuide.cs
//  Author: Lu Zexi
//  2014-02-17




/// <summary>
/// 新手引导GUI
/// </summary>
public class GUIGuide : GUIBase
{
    private const string RES_MAIN = "GUI_Guide"; //主资源地址
    private const string RES_TIP1 = "GUI_GuideTip1"; //提示1资源
    private const string RES_TIP2 = "GUI_GuideTip2"; //提示2资源
    private const string RES_TIP3 = "GUI_GuideTip3"; //提示3资源

    private const string GUI_BLACK = "Parent/BLACK";    //遮挡背景
    private const string GUI_PARENT = "Parent"; //父节点
    private const string GUI_HIT = "Parent/HIT";   //新手点击遮罩
    private const string GUI_TIP = "Parent/Tip";   //提示
    private const string GUI_TIP_TXT = "Parent/Tip/txt";    //提示问题
    private const string GUI_IMG = "Parent/tex";    //图片

    private UnityEngine.Object m_cResTip1; //提示1
    private UnityEngine.Object m_cResTip2;  //提示2
    private UnityEngine.Object m_cResTip3;  //提示3

    private GameObject m_cParent;   //父节点
    private GameObject m_cBlack;    //遮挡
    private UITexture m_cImg;  //图片
    private GameObject m_cHit;  //点击点
    private GameObject m_cTip;  //提示
    private UILabel m_cTipTxt;  //提示问题
    private List<GameObject> m_lstTipPoint1 = new List<GameObject>();   //提示点1
    private List<GameObject> m_lstTipPoint2 = new List<GameObject>();   //提示点2
    private List<GameObject> m_lstTipPoint3 = new List<GameObject>();   //提示点2

    //临时
    private bool m_bNext;   //下一个
    private float m_fStartTime; //开始时间
    private float m_fDelayTime; //延迟时间

    public GUIGuide( GUIManager guiMgr )
        : base(guiMgr, GUI_DEFINE.GUIID_GUIDE, GUILAYER.GUI_FULL)
    { 
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        base.Show();
        if (this.m_cGUIObject == null)
        {
            this.m_cGUIObject = GameObject.Instantiate(ResourceMgr.LoadAsset(GAME_DEFINE.RESOURCE_GUI_CACHE, RES_MAIN) as UnityEngine.Object) as GameObject;
            this.m_cGUIObject.transform.parent = GUI_FINDATION.FIND_GAME_OBJECT(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localPosition = Vector3.zero;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            //提示资源
            this.m_cResTip1 = ResourceMgr.LoadAsset(GAME_DEFINE.RESOURCE_GUI_CACHE, RES_TIP1) as UnityEngine.Object;
            this.m_cResTip2 = ResourceMgr.LoadAsset(GAME_DEFINE.RESOURCE_GUI_CACHE, RES_TIP2) as UnityEngine.Object;
            this.m_cResTip3 = ResourceMgr.LoadAsset(GAME_DEFINE.RESOURCE_GUI_CACHE, RES_TIP3) as UnityEngine.Object;

            this.m_cParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_PARENT);

            this.m_cBlack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_BLACK);
            GUIComponentEvent ce = this.m_cBlack.AddComponent<GUIComponentEvent>();
            ce.AddIntputDelegate(OnBlack);

            this.m_cHit = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_HIT);

            this.m_cTip = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, GUI_TIP);
            this.m_cTipTxt = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_TIP_TXT);

            this.m_cImg = GUI_FINDATION.GET_OBJ_COMPONENT<UITexture>(this.m_cGUIObject, GUI_IMG);
        }

        foreach (GameObject item in this.m_lstTipPoint1)
        {
            GameObject.Destroy(item);
        }
        this.m_lstTipPoint1.Clear();
        foreach (GameObject item in this.m_lstTipPoint2)
        {
            GameObject.Destroy(item);
        }
        this.m_lstTipPoint2.Clear();
        foreach (GameObject item in this.m_lstTipPoint3)
        {
            GameObject.Destroy(item);
        }
        this.m_lstTipPoint3.Clear();


        this.m_fStartTime = GAME_TIME.TIME_FIXED();
        this.m_fDelayTime = -1;

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 设置图片
    /// </summary>
    /// <param name="path"></param>
    /// <param name="pos"></param>
    public void SetImg(string path, Vector3 pos)
    {
        UnityEngine.Object obj = ResourceMgr.LoadAsset(GAME_DEFINE.RESOURCE_GUI_CACHE, path) as UnityEngine.Object;
        if (obj != null)
        {
            this.m_cImg.enabled = true;
            this.m_cImg.mainTexture = obj as Texture;
            this.m_cImg.MakePixelPerfect();
            this.m_cImg.transform.localPosition = pos;
        }
        else
        {
            this.m_cImg.enabled = false;
        }
    }

    /// <summary>
    /// 设置延迟时间
    /// </summary>
    /// <param name="delay"></param>
    public void SetDelayTime(float delay)
    {
        this.m_bNext = false;
        this.m_fDelayTime = delay;
    }

    /// <summary>
    /// 设置提示开关
    /// </summary>
    /// <param name="tip"></param>
    public void SwitchTip(bool tip)
    {
        this.m_cTip.SetActive(tip);
    }

    /// <summary>
    /// 设置提示坐标
    /// </summary>
    /// <param name="pos"></param>
    public void SetTipPos(Vector3 pos)
    {
        this.m_cTip.transform.localPosition = pos;
    }

    /// <summary>
    /// 设置提示问题
    /// </summary>
    /// <param name="txt"></param>
    public void SetTipTxt(string txt)
    {
        this.m_cTipTxt.text = txt;
    }

    /// <summary>
    /// 生成提示1
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="txt"></param>
    public void GeneratorTip1(Vector3 pos, string txt)
    {
        GameObject obj = GameObject.Instantiate(this.m_cResTip1) as GameObject;
        obj.transform.parent = this.m_cParent.transform;
        obj.transform.localPosition = pos;
        obj.transform.localScale = Vector3.one;
        obj.GetComponentInChildren<UILabel>().text = txt;
        this.m_lstTipPoint1.Add(obj);
    }

    /// <summary>
    /// 生成提示2
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="txt"></param>
    public void GeneratorTip2(Vector3 pos, string txt)
    {
        GameObject obj = GameObject.Instantiate(this.m_cResTip2) as GameObject;
        obj.transform.parent = this.m_cParent.transform;
        obj.transform.localPosition = pos;
        obj.transform.localScale = Vector3.one;
        obj.GetComponentInChildren<UILabel>().text = txt;
        this.m_lstTipPoint2.Add(obj);
    }

    /// <summary>
    /// 生成提示3
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="txt"></param>
    public void GeneratorTip3(Vector3 pos)
    {
        GameObject obj = GameObject.Instantiate(this.m_cResTip3) as GameObject;
        obj.transform.parent = this.m_cParent.transform;
        obj.transform.localPosition = pos;
        obj.transform.localScale = Vector3.one;
        this.m_lstTipPoint3.Add(obj);
    }

    /// <summary>
    /// 遮罩开关
    /// </summary>
    /// <param name="black"></param>
    public void SwitchButton( bool button )
    {
        if (!button)
        {
            this.m_cBlack.SetActive(true);
            this.m_bNext = true;
            this.m_cHit.SetActive(false);
        }
        else
        {
            this.m_cBlack.SetActive(false);
            this.m_bNext = false;
            this.m_cHit.SetActive(true);
        }
    }

    /// <summary>
    /// 设置按钮背景透明度
    /// </summary>
    /// <param name="alpha"></param>
    public void SetButtonBackAlpha(float alpha)
    {
        UITexture[] tex = this.m_cHit.GetComponentsInChildren<UITexture>();
        foreach (UITexture item in tex)
        {
            item.alpha = alpha;
        }
    }

    /// <summary>
    /// 设置全背景alpha
    /// </summary>
    /// <param name="alpha"></param>
    public void SetBackAlpha(float alpha)
    {
        UITexture[] tex = this.m_cBlack.GetComponentsInChildren<UITexture>();
        foreach (UITexture item in tex)
        {
            item.alpha = alpha;
        }
    }

    /// <summary>
    /// 按钮位置大小
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="scal"></param>
    public void SetHitPos(Vector3 pos, Vector3 scal)
    {
        this.m_cHit.transform.localPosition = pos;
        this.m_cHit.transform.localScale = scal;
    }

    /// <summary>
    /// 设置全背景位置
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="scal"></param>
    public void SetBlackPos(Vector3 pos, Vector3 scal)
    {
        this.m_cBlack.transform.localPosition = pos;
        this.m_cBlack.transform.localScale = scal;
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();
        this.m_bNext = false;
        //ResourceMgr.UnloadUnusedResources();
        SetLocalPos(Vector3.one*0xFFFFF);
        //Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        this.m_cResTip1 = null;
        this.m_cResTip2 = null;
        this.m_cResTip3 = null;

        this.m_cParent = null;
        this.m_cBlack = null;
        this.m_cImg = null;
        this.m_cHit = null;
        this.m_cTip = null;
        this.m_cTipTxt = null;
        this.m_lstTipPoint1.Clear();
        this.m_lstTipPoint2.Clear();
        this.m_lstTipPoint3.Clear();

        base.Destory();
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        if (!this.m_bShow)
            return false;
        if (this.m_fDelayTime > 0)
        {
            if (GAME_TIME.TIME_FIXED() - this.m_fStartTime > this.m_fDelayTime)
            {
                GuideManager.GetInstance().ShowNext();
            }
        }
        return true;
    }

    /// <summary>
    /// 点击背景
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnBlack(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            //if (this.m_bNext)
            //{
            //    GuideManager.GetInstance().ShowNext();
            //}
        }
    }

}
