using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Resource;
using UnityEngine;

//  GUIStory.cs
//  Author: Lu Zexi
//  2014-02-26




/// <summary>
/// GUI剧情
/// </summary>
public class GUIStory : GUIBase
{
    private const string RES_MAIN = "GUI_Story"; //主资源

    private const string SCENE_PARENT = "BATTLE"; //场景父节点
    private const string GUI_TXT = "Lab_Content";  //文字节点
    private const string GUI_TITTLE = "Lab_Title";  //标题节点
    private const string GUI_FACE = "Face"; //人物表情
    private const string BACK_GROUND = "BlackGround";   //背景

    private StoryTable m_cStoryTable;   //剧情表
    private int m_iIndex;   //剧情所以
    private STATE m_eState;   //状态

    private GameObject m_cScene;    //场景物体
    private GameObject m_cBackGround;   //背景
    private UILabel m_cTxt; //文本
    private UILabel m_cTittle;  //标题
    private UISprite m_cFace;   //表情

    public delegate void CALL_BACK();   //回调
    private CALL_BACK m_delFinish;    //结束回调

    /// <summary>
    /// 状态
    /// </summary>
    private enum STATE
    { 
        START = 1,
        LOADING1,
        LOADING2,
        LOADING3,
        TEXT1,
        TEXT2,
        TEXT3,
        END,
    }

    public GUIStory(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_STORY, GUILAYER.GUI_FULL)
    { 
        //
    }

    /// <summary>
    /// 展示
    /// </summary>
    /// <param name="storyID"></param>
    public void Show(int storyID , CALL_BACK call )
    {
        this.m_delFinish = call;
        this.m_cStoryTable = StoryTableManager.GetInstance().GetStory(storyID);
        if (this.m_cStoryTable == null)
        {
            this.m_delFinish();
            return;
        }
        this.m_iIndex = 0;
        base.Show();

        //ResourceMgr.ClearAsyncLoad();
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + RES_MAIN);
        ResourceMgr.RequestAssetBundle(GAME_DEFINE.RESOURCE_GUI_PATH + this.m_cStoryTable.SceneName);
        this.m_eState = STATE.START;
        GUI_FUNCTION.AYSNCLOADING_SHOW();
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        base.Show();
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        base.Hiden();
        CameraManager.GetInstance().HidenBattle3DCamera();
        ResourceMgr.UnloadUnusedResources();

        //ResourceMgr.ClearAsyncLoad();
        if( this.m_cGUIObject != null )
            GameObject.Destroy(this.m_cGUIObject);
        this.m_cGUIObject = null;
        if (this.m_cScene != null)
            GameObject.Destroy(this.m_cScene);
        this.m_cScene = null;
        Destory();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        if (this.m_cScene != null)
            GameObject.Destroy(this.m_cScene);
        this.m_cScene = null;

        this.m_cTxt = null;
        this.m_cTittle = null;
        this.m_cFace = null;

        base.Destory();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public override bool Update()
    {
        if (!this.m_bShow) return false;

        switch (this.m_eState)
        { 
            case STATE.START:
                this.m_eState++;
                break;
            case STATE.LOADING1:
                if (ResourceMgr.GetProgress() >= 1f && ResourceMgr.IsComplete() )
                {
                    GUI_FUNCTION.AYSNCLOADING_HIDEN();
                    this.m_eState++;
                }
                break;
            case STATE.LOADING2:
                foreach (GUIBase item in this.m_cGUIMgr.GetAll())
                { 
                    if( item.ID != this.m_iID )
                    {
                        item.Destory();
                    }
                }
                CameraManager.GetInstance().ShowBattle3DCamera();
                //gui
                this.m_cGUIObject = GameObject.Instantiate(ResourceMgr.LoadAsset(RES_MAIN) as UnityEngine.Object) as GameObject;
                this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
                this.m_cGUIObject.transform.localPosition = Vector3.zero;
                this.m_cGUIObject.transform.localScale = Vector3.one;
                SetLocalPos(Vector3.zero);
                GUIComponentEvent ce = this.m_cGUIObject.AddComponent<GUIComponentEvent>();
                ce.AddIntputDelegate(OnClick);
                this.m_cTxt = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_TXT);
                this.m_cTittle = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_TITTLE);
                this.m_cTittle.text = this.m_cStoryTable.Tittle;
                this.m_cFace = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, GUI_FACE);
                GameObject battle_parent = GameObject.Find(SCENE_PARENT);
                //场景
                this.m_cScene = GameObject.Instantiate(ResourceMgr.LoadAsset(this.m_cStoryTable.SceneName) as UnityEngine.Object) as GameObject;
                this.m_cScene.transform.parent = battle_parent.transform;
                this.m_cScene.transform.localPosition = Vector3.zero;
                this.m_cScene.transform.localScale = Vector3.one;
                //背景
                this.m_cBackGround = GUI_FINDATION.GET_GAME_OBJECT(battle_parent, BACK_GROUND);
                this.m_cBackGround.transform.localPosition = Vector3.one * 0xFFFFF;
                this.m_eState++;
                break;
            case STATE.LOADING3:
                this.m_eState++;
                break;
            case STATE.TEXT1:
                this.m_cTxt.text = this.m_cStoryTable.LstDialog[this.m_iIndex];
                this.m_eState++;
                break;
            case STATE.TEXT2:
                break;
            case STATE.TEXT3:
                this.m_iIndex++;
                if (this.m_cStoryTable.LstDialog.Count <= this.m_iIndex)
                {
                    this.m_eState++;
                }
                else
                {
                    this.m_eState = STATE.TEXT1;
                }
                break;
            case STATE.END:
                Hiden();
                if( this.m_delFinish != null )
                    this.m_delFinish();
                this.m_eState++;
                break;
        }

        return true;
    }

    /// <summary>
    /// 点击
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClick(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (this.m_eState == STATE.TEXT2)
            {
                this.m_eState++;
            }
        }
    }
}
