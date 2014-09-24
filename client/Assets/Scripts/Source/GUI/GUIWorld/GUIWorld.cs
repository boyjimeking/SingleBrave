using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;

/// <summary>
/// 世界GUI类
/// </summary>
public class GUIWorld : GUIBase
{
    private const string RES_MAIN = "GUI_World"; //主资源地址
    private const string RES_AREA = "GUI_World_ListCell";  //区域展示资源

    private const string BACK_PARENT = "BackParent";  //返回父节点
    private const string BACK_BUTTON = "BackParent/Button_Back";  //返回按钮地址
    private const string FUBEN_BUTTON = "Button_Fuben"; //副本按钮地址
    private const string LIST_CELL = "lst";   //列表资源地址
    private const string AREA_NAME = "Label_AreaName"; //区域名
    private const string AREA_DESC = "Label_AreaDescription"; //区域介绍
    private const string BG = "Bg";//背景图片地址

    private GameObject m_cBackParent;  //返回父节点
    private GameObject m_cLstParent;    //列表父节点

    private GameObject m_cBackButton;   //返回按钮
    private GameObject m_cFuBenButton;  //副本按钮
    private GameObject m_cAreaCell;        //区域地图关卡列表Cell
    private UILabel m_cAreaNameLab;           //区域名label
    private UILabel m_cAreaDescLab;           //区域介绍
    private GameObject m_cBg;//背景图片
    private UnityEngine.Object m_cWorldItem;//世界项

    private List<GameObject> m_lstArea = new List<GameObject>(); //区域列表

    //public int CurrentAreaId;  //当前选择的区域ID

    public GUIWorld(GUIManager guimgr)
        : base(guimgr, GUI_DEFINE.GUIID_WORLD, UILAYER.GUI_PANEL)
    { 

    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        this.m_eLoadingState = LOADING_STATE.NONE;
        if (this.m_cGUIObject == null)
        {
            this.m_eLoadingState = LOADING_STATE.START;
            GUI_FUNCTION.AYSNCLOADING_SHOW();
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
            ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_AREA);
        }
        else
        {
            InitGUI();
        }
    }

    /// <summary>
    /// 初始化GUI
    /// </summary>
    protected override void InitGUI()
    {
        base.Show();
        GUI_FUNCTION.AYSNCLOADING_HIDEN();
        if (this.m_cGUIObject == null)
        {
			this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourceMgr.LoadAsset(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cBackParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BACK_PARENT);
            this.m_cLstParent = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, LIST_CELL);

            this.m_cBackButton = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BACK_BUTTON);
            GUIComponentEvent guiBackEvent = this.m_cBackButton.AddComponent<GUIComponentEvent>();
            guiBackEvent.AddIntputDelegate(OnBackButton);

			this.m_cWorldItem = (UnityEngine.Object)ResourceMgr.LoadAsset(RES_AREA);

            this.m_cFuBenButton = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, FUBEN_BUTTON);
            GUIComponentEvent guiFuBenEvent = this.m_cFuBenButton.AddComponent<GUIComponentEvent>();
            guiFuBenEvent.AddIntputDelegate(OnFubenButtonClick);

            this.m_cBg = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BG);
        }

        this.m_cBg.transform.localScale = Vector3.one;

        foreach (GameObject item in this.m_lstArea)
        {
            GameObject.DestroyImmediate(item);
        }

        this.m_lstArea.Clear();

        int currentWorldId = 1;
		
		for (int i = 0; i < FuBen.Count; i++)
        {

			FuBen fubenitem = FuBen.Get(i);
			if (currentWorldId < fubenitem.m_iWorldID)
            {
				if (fubenitem.m_bActive)
                {
					currentWorldId = fubenitem.m_iWorldID;
                }
            }
        }

        //生成世界列表
        List<WorldTable> lstWorldTable = WorldManager.GetAllWorld();

        for (int i = 0; i < currentWorldId; i++)
        {
            GameObject worldCell = GameObject.Instantiate(this.m_cWorldItem) as GameObject;
            worldCell.transform.parent = this.m_cLstParent.transform;
            worldCell.transform.localScale = Vector3.one;
            worldCell.transform.localPosition = new Vector3(0, 210 - i * 210);
            UILabel nameLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(worldCell, AREA_NAME);
            nameLabel.text = lstWorldTable[i].Name;
            UILabel DescLabel = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(worldCell, AREA_DESC);
            DescLabel.text = lstWorldTable[i].Desc;
            GUIComponentEvent areaCellEvent = worldCell.AddComponent<GUIComponentEvent>();
            areaCellEvent.AddIntputDelegate(didSelectedCell, lstWorldTable[i].ID);
            this.m_lstArea.Add(worldCell);
        }

        SetLocalPos(Vector3.zero);
    }
    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();

        //SetLocalPos(Vector3.one * 0xFFFF);
        ResourceMgr.UnloadUnusedResources();
        //TODO:临时添加
        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        this.m_cWorldItem = null;

        if (this.m_lstArea != null)
        {
            foreach (GameObject obj in this.m_lstArea)
            {
                if (obj != null)
                {
                    GameObject.Destroy(obj);
                }
            }
        }

        if (this.m_lstArea != null)
        {
            this.m_lstArea.Clear();
        }

        this.m_cBackParent = null;
        this.m_cLstParent = null;

        this.m_cBackButton = null;
        this.m_cFuBenButton = null;
        this.m_cAreaCell = null;
        this.m_cAreaNameLab = null;
        this.m_cAreaDescLab = null;
        this.m_cBg = null;
        
        base.Destory();
    }

    /// <summary>
    /// 返回按钮事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnBackButton(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIMain main = (GUIMain)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MAIN);
            main.Show();

            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
            this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
   
        }
    }

    /// <summary>
    /// 特殊副本按钮事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnFubenButtonClick(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            Hiden();

            GUIEspDungeon espDungeon = (GUIEspDungeon)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_ESPDUNGEON);
            espDungeon.SetLastGuiId(GUI_DEFINE.GUIID_WORLD);
            espDungeon.Show();
        }
    }

    /// <summary>
    /// 点击关卡事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void didSelectedCell(GUI_INPUT_INFO info,object[] args) 
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            int id = (int)args[0];
           // CurrentAreaId = id;
            //Debug.Log("selected area cell " + args[0].ToString());
            WorldManager.s_iCurrentWorldId = id;

            //重置当前区域id，以便从主页进去的时候显示最新区域
            GUIArea area = (GUIArea)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_AREA);
            area.ResetCurrentAreaId();

			int newAreaIndex = FuBen.GetNewAreaIndex(WorldManager.s_iCurrentWorldId);
			int newDungeonIndex = FuBen.GetNewDungeonIndex(WorldManager.s_iCurrentWorldId, newAreaIndex);
            if (newDungeonIndex >= 0)
            {
                WorldManager.s_iLastNewDungeonIndex = newDungeonIndex - 1;
            }

            CTween.TweenScale(this.m_cBg, 0.0f, 0.3f, Vector3.one, Vector3.one * 20, BgScale);
        }
       
    }

    /// <summary>
    /// 背景图缩放
    /// </summary>
    private void BgScale()
    {
        SetLocalPos(Vector3.one * 0XFFFF);
        GUIArea area = (GUIArea)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_AREA);
        area.Show();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        //资源加载等待
        switch (this.m_eLoadingState)
        {
            case LOADING_STATE.START:
                this.m_eLoadingState++;
                return false;
            case LOADING_STATE.LOADING:
                if (ResourceMgr.GetProgress() >= 1f && ResourceMgr.IsComplete())
                {
                    this.m_eLoadingState++;
                }
                return false;
            case LOADING_STATE.END:
                InitGUI();
                this.m_eLoadingState++;
                break;
        }

        return base.Update();
    }
}
