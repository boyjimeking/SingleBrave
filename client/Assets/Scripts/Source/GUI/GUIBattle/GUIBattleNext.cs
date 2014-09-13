using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Game.Resource;
using Game.Base;




//  GUIBattleNext.cs
//  Author: Lu Zexi
//  2013-12-09




/// <summary>
/// GUI战场下一个
/// </summary>
public class GUIBattleNext : GUIBase
{
    public const string RES_MAIN = "GUI_BattleNext"; //主资源

    private const string GUI_MAX_LAYER1 = "Battle_Bottom/Spr_MaxGate1";    //最大战斗层级
    private const string GUI_MAX_LAYER2 = "Battle_Bottom/Spr_MaxGate2";    //最大战斗层级
    private const string GUI_CUR_LAYER1 = "Battle_Bottom/Spr_CurGate1";    //当前战斗层级
    private const string GUI_CUR_LAYER2 = "Battle_Bottom/Spr_CurGate2";    //当前战斗层级
    private const string GUI_CUR_PROCESS = "Battle_Bottom/Sprite_SlideContent";  //当前进度
    private const string GUI_CUR_PROCESS_POINT = "Battle_Bottom/Sprite_fuck";    //当前进度指针
    private const string GUI_CUR_DUNGEON_NAME = "Battle_Top/Label_Name";  //副本名字
    private const string GUI_CUR_GATE_NAME = "Battle_Top/Label_Boss";  //关卡名字

    private UILabel m_cCurDungeonName; //当前副本名称
    private UILabel m_cCurGateName; //当前关卡名称
    private UISprite m_cMaxLayer1;    //最大战斗层级1
    private UISprite m_cMaxLayer2;    //最大战斗层级2
    private UISprite m_cCurLayer1;    //当前战斗层级1
    private UISprite m_cCurLayer2;    //当前战斗层级2
    private UISprite m_cCurProcess; //当前进度
    private UISprite m_cCurProcessPoint;    //当前进度指针

    private const int PROCESS_POINT_START = 228;    //指针起点
    private const int PROCESS_POINT_END = -210;  //指针终点
    private const int PROCESS_MAX_LENGTH = 100;   //最大长度
    private const float PROCESS_RATE_TIME = 0.5f;  //进度动画花费时间
    private const float WAIT_TIME = 0.5f; //等待时间
    private float m_fProcessSrcRate;    //进度原始比例
    private float m_fProcessTarRate;    //进度目标比例
    private float m_fProcessStartTime;  //进度开始时间
    private float m_fWaitStartTime; //开始等待时间
    private int m_iCurLayer;    //当前层级
    private int m_iLastLayer;   //最后层级

    public delegate void CALL_BACK(object[] args); //回调委托
    private CALL_BACK m_delCallBack;    //回调
    private object[] m_vecCallBackArgs;   //回调参数

    public GUIBattleNext(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_BATTLE_NEXT, GUILAYER.GUI_PANEL2)
    { 
        //
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        this.m_cCurDungeonName = null;
        this.m_cCurGateName = null;
        this.m_cMaxLayer1 = null;
        this.m_cMaxLayer2 = null;
        this.m_cCurLayer1 = null;
        this.m_cCurLayer2 = null;
        this.m_cCurProcess = null;
        this.m_cCurProcessPoint = null;

        base.Destory();
    }

    /// <summary>
    /// 设置层级
    /// </summary>
    /// <param name="curLayer"></param>
    /// <param name="maxLayer"></param>
    public void ShowLayer(int curLayer, int maxLayer , string curDungeonName , string curGateName , CALL_BACK callback , params object[] args )
    {
        Show();

        this.m_delCallBack = callback;
        this.m_vecCallBackArgs = args;

        this.m_cCurDungeonName.text = curDungeonName;
        this.m_cCurGateName.text = curGateName;
        this.m_iLastLayer = curLayer - 1 < 0 ? 0 : curLayer - 1;
        this.m_iCurLayer = curLayer;

        this.m_cCurLayer1.spriteName = "" + (this.m_iLastLayer / 10);
        this.m_cCurLayer2.spriteName = "" + (this.m_iLastLayer % 10);
        this.m_cMaxLayer1.spriteName = "" + (maxLayer/10);
        this.m_cMaxLayer2.spriteName = "" + (maxLayer%10);

        this.m_fProcessSrcRate = this.m_iLastLayer * 1f / maxLayer;
        this.m_fProcessTarRate = curLayer * 1f / maxLayer;

        this.m_cCurProcess.transform.localScale = new Vector3(this.m_fProcessSrcRate, 1, 1);
        float tmpPos = (PROCESS_POINT_END - PROCESS_POINT_START) * this.m_fProcessSrcRate + PROCESS_POINT_START;
        this.m_cCurProcessPoint.transform.localPosition = new Vector3(tmpPos, this.m_cCurProcessPoint.transform.localPosition.y, this.m_cCurProcessPoint.transform.localPosition.z);
        this.m_fProcessStartTime = GAME_TIME.TIME_FIXED();
        this.m_fWaitStartTime = 0;
        //音效
        SoundManager.GetInstance().PlaySound(SOUND_DEFINE.SE_BATTLE_NEXT_PROCESS);
    }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        InitGUI();
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
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GUI_FINDATION.FIND_GAME_OBJECT(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cMaxLayer1 = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, GUI_MAX_LAYER1);
            this.m_cMaxLayer2 = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, GUI_MAX_LAYER2);
            this.m_cCurLayer1 = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, GUI_CUR_LAYER1);
            this.m_cCurLayer2 = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, GUI_CUR_LAYER2);

            this.m_cCurProcess = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, GUI_CUR_PROCESS);
            this.m_cCurProcessPoint = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, GUI_CUR_PROCESS_POINT);
            this.m_cCurDungeonName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_CUR_DUNGEON_NAME);
            this.m_cCurGateName = GUI_FINDATION.GET_OBJ_COMPONENT<UILabel>(this.m_cGUIObject, GUI_CUR_GATE_NAME);
        }

        SetLocalPos(Vector3.zero);
    }

    /// <summary>
    /// 立即展示
    /// </summary>
    public override void ShowImmediately()
    {
        base.ShowImmediately();
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //SetLocalPos(Vector3.one * 0xFFFFFF);

        base.Hiden();

        Destory();
    }

    /// <summary>
    /// 立即隐藏
    /// </summary>
    public override void HidenImmediately()
    {
        base.HidenImmediately();

        Destory();
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
                if (ResourcesManager.GetInstance().GetProgress() >= 1f && ResourcesManager.GetInstance().IsComplete())
                {
                    this.m_eLoadingState++;
                }
                return false;
            case LOADING_STATE.END:
                InitGUI();
                this.m_eLoadingState++;
                break;
        }

        base.Update();

        if (!this.m_bShow)
        {
            return false;
        }

        if (this.m_fProcessStartTime > 0)
        {
            float dis = GAME_TIME.TIME_FIXED() - this.m_fProcessStartTime;
            if (dis > PROCESS_RATE_TIME)
            {
                this.m_fProcessStartTime = 0;
                this.m_fWaitStartTime = GAME_TIME.TIME_FIXED();
                this.m_cCurProcess.transform.localScale = new Vector3(this.m_fProcessTarRate, 1f, 1f);
                float tmpPos = (PROCESS_POINT_END - PROCESS_POINT_START) * this.m_fProcessTarRate + PROCESS_POINT_START;
                this.m_cCurProcessPoint.transform.localPosition = new Vector3(tmpPos, this.m_cCurProcessPoint.transform.localPosition.y, this.m_cCurProcessPoint.transform.localPosition.z);
                this.m_cCurLayer1.spriteName = "" + (this.m_iCurLayer / 10);
                this.m_cCurLayer2.spriteName = "" + (this.m_iCurLayer % 10);
            }
            else
            {
                float rate = dis / PROCESS_RATE_TIME;
                float tmpRate = rate * (this.m_fProcessTarRate - this.m_fProcessSrcRate) + this.m_fProcessSrcRate;
                this.m_cCurProcess.transform.localScale = new Vector3(tmpRate, 1, 1);
                float tmpPos = (PROCESS_POINT_END - PROCESS_POINT_START) * tmpRate + PROCESS_POINT_START;
                this.m_cCurProcessPoint.transform.localPosition = new Vector3(tmpPos, this.m_cCurProcessPoint.transform.localPosition.y, this.m_cCurProcessPoint.transform.localPosition.z);
            }
        }

        if (this.m_fWaitStartTime > 0)
        {
            float dis = GAME_TIME.TIME_FIXED() - this.m_fWaitStartTime;
            if (dis > WAIT_TIME)
            {
                this.m_fWaitStartTime = 0;
                this.m_delCallBack(this.m_vecCallBackArgs);
            }
        }

        return true;
    }

}
