using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using UnityEngine;


//  GuideManager.cs
//  Author: Lu Zexi
//  2014-02-17



/// <summary>
/// 新手引导管理类
/// </summary>
public class GuideManager : Singleton<GuideManager>
{
    private int m_iModelID;    //当前新手模块ID
    private int m_iStep;    //步骤

    private GUIDE_STATE m_eState;   //状态

    public float OFFSET_X;  //偏移量X
    public float OFFSET_Y;  //偏移量Y

    public enum GUIDE_STATE //新手引导状态
    { 
        NONE = 0 ,  //无
        START = 1,  //开始
        BUTTON_CLICK = 2, //按钮
        BUTTON_PRESS = 3,    //释放
        SLIP = 4,//滑动
        BUTTON_LONG_DOWN = 5,   //长按
        MAX,
    }

    public GuideManager()
    {
        this.m_eState = GUIDE_STATE.NONE;
    }

    /// <summary>
    /// 获取状态
    /// </summary>
    /// <returns></returns>
    public GUIDE_STATE GetState()
    {
        return this.m_eState;
    }

    /// <summary>
    /// 获取新手ID
    /// </summary>
    /// <returns></returns>
    public int GetModelID()
    {
        return this.m_iModelID;
    }

    /// <summary>
    /// 展示新手引导
    /// </summary>
    /// <param name="id"></param>
    public void ShowGuide(int model_id)
    {
        Debug.Log(" test model id " + model_id);
        if (Role.role.GetBaseProperty().m_iModelID != model_id)
        {
            Debug.Log("model id : " + model_id + " pro : " + Role.role.GetBaseProperty().m_iModelID);
            return;
        }
        Debug.Log(" test1 model id " + model_id);

        if (!GuideTableManager.GetInstance().ExistModelID(model_id))
            return;

        Debug.Log(" test2 model id " + model_id);

        //根据当前进入模块，记录下一个模块开始点
        switch (model_id)
        {
            case GUIDE_FUNCTION.MODEL_FIRST_FIGHT1:
                SendAgent.SendGuideStep(Role.role.GetBaseProperty().m_iPlayerId, GUIDE_FUNCTION.MODEL_HERO_UP1);
                break;
            case GUIDE_FUNCTION.MODEL_HERO_UP1:
                SendAgent.SendGuideStep(Role.role.GetBaseProperty().m_iPlayerId, GUIDE_FUNCTION.MODEL_BATTLE_SECOND1);
                break;
            case GUIDE_FUNCTION.MODEL_BATTLE_SECOND1:
                SendAgent.SendGuideStep(Role.role.GetBaseProperty().m_iPlayerId, GUIDE_FUNCTION.MODEL_TOWN1);
                break;
            case GUIDE_FUNCTION.MODEL_TOWN1:
                SendAgent.SendGuideStep(Role.role.GetBaseProperty().m_iPlayerId, GUIDE_FUNCTION.MODEL_BATTLE_THIRD1);
                break;
            case GUIDE_FUNCTION.MODEL_BATTLE_THIRD1:
                SendAgent.SendGuideStep(Role.role.GetBaseProperty().m_iPlayerId, GUIDE_FUNCTION.MODEL_SUMMON1);
                break;
            case GUIDE_FUNCTION.MODEL_SUMMON1:
                SendAgent.SendGuideStep(Role.role.GetBaseProperty().m_iPlayerId, -1);
                break;
        }

        this.m_iModelID = model_id;
        this.m_iStep = 0;

        ShowNext();
    }

    /// <summary>
    /// 展示下一个
    /// </summary>
    public void ShowNext()
    {
        this.m_iStep++;
        this.m_eState = GUIDE_STATE.NONE;

        GUIGuide guiGuide = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_GUIDE) as GUIGuide;
        guiGuide.Hiden();

        Debug.Log("model id : " + this.m_iModelID + " step id : " + this.m_iStep);

        if (!GuideTableManager.GetInstance().ExistStepID(this.m_iModelID , this.m_iStep))
        {
            Role.role.GetBaseProperty().m_iModelID++;
            Debug.Log("MODEL " + Role.role.GetBaseProperty().m_iModelID);
            if (!GuideTableManager.GetInstance().IsModelIDMax(Role.role.GetBaseProperty().m_iModelID))
                Role.role.GetBaseProperty().m_iModelID = -1;
            guiGuide.Destory();

            //SendAgent.SendGuideStep(Role.role.GetBaseProperty().m_iPlayerId, this.m_iModelID);
            return;
        }

        GuideTable table = GuideTableManager.GetInstance().GetGuideTable(this.m_iModelID, this.m_iStep);

        if (table == null)
        {
            return;
        }

        Debug.Log("in guide show ");

        //show新手引导
        guiGuide.Show();

        //时间缩放
        Time.timeScale = table.TimeScale;

        //设置文字提示
        guiGuide.SwitchTip(table.IsTip);
        if (table.IsTip)
        {
            guiGuide.SetTipPos(new Vector3(table.TipX, table.TipY));
            guiGuide.SetTipTxt(table.Tip);
        }

        guiGuide.SetImg(table.Img, new Vector3(table.ImgX, -table.ImgY, 0));

        guiGuide.SwitchButton(table.ButtonMask > 0 );

        this.m_eState = GUIDE_STATE.START;

        //设置三种小提示
        for (int i = 0; i < table.Point1Pos.Count; i++)
        {
            guiGuide.GeneratorTip1(table.Point1Pos[i], table.Point1Txt[i]);
        }
        for (int i = 0; i < table.Point2Pos.Count; i++)
        {
            guiGuide.GeneratorTip2(table.Point2Pos[i], table.Point2Txt[i]);
        }
        for (int i = 0; i < table.Point3Pos.Count; i++)
        {
            guiGuide.GeneratorTip3(table.Point3Pos[i]);
        }

        switch (table.Button)
        {
            case 1:
                this.m_eState = GUIDE_STATE.BUTTON_CLICK;
                break;
            case 2:
                this.m_eState = GUIDE_STATE.BUTTON_PRESS;
                break;
            case 3:
                this.m_eState = GUIDE_STATE.SLIP;
                this.OFFSET_X = table.OffsetX;
                this.OFFSET_Y = table.OffsetY;
                break;
        }

        //设置按钮
        if (table.ButtonMask > 0)
        {
            guiGuide.SetButtonBackAlpha(table.ButtonAlpha);
            guiGuide.SetHitPos(new Vector3(table.BtnX, table.BtnY), new Vector3(table.ButtonWidth, table.ButtonLength, 1));
        }
        else
        {
            //设置全背景透明度
            guiGuide.SetBackAlpha(table.ButtonAlpha);
            guiGuide.SetBlackPos(new Vector3(table.BtnX, table.BtnY), new Vector3(table.ButtonWidth, table.ButtonLength, 1));
            //设置延迟时间
            if (table.Delay)
            {
                guiGuide.SetDelayTime(table.DelayTime);
            }
        }
    }

}


/// <summary>
/// 新手引导工具
/// </summary>
public class GUIDE_FUNCTION
{
    public const int MODEL_FIRST_FIGHT1 = 1;  //第一场战斗1层
    public const int MODEL_FIRST_FIGHT2 = 2;  //第一场战斗2层
    public const int MODEL_FIRST_FIGHT3 = 3;  //第一场战斗3层
    public const int MODEL_HERO_UP1 = 4; //英雄强化步骤1
    public const int MODEL_HERO_UP2 = 5; //英雄强化步骤2
    public const int MODEL_HERO_UP3 = 6; //英雄强化步骤3
    public const int MODEL_HERO_UP4 = 7; //英雄强化步骤4
    public const int MODEL_HERO_UP5 = 8; //英雄强化步骤5
    public const int MODEL_HERO_UP6 = 9; //英雄强化步骤6
    public const int MODEL_HERO_UP7 = 10; //英雄强化步骤7
    public const int MODEL_HERO_EDITOR1 = 11; //英雄编辑步骤1
    public const int MODEL_HERO_EDITOR2 = 12; //英雄编辑步骤2
    public const int MODEL_HERO_EDITOR3 = 13; //英雄编辑步骤3
    public const int MODEL_HERO_EDITOR4 = 14; //英雄编辑步骤4
    public const int MODEL_BATTLE_SECOND1 = 15;  //第二场战斗步骤1
    public const int MODEL_BATTLE_SECOND2 = 16;  //第二场战斗步骤2
    public const int MODEL_BATTLE_SECOND3 = 17;  //第二场战斗步骤3
    public const int MODEL_BATTLE_SECOND4 = 18;  //第二场战斗第1层
    public const int MODEL_BATTLE_SECOND5 = 19;  //第二场战斗第2层
    public const int MODEL_BATTLE_SECOND6 = 20;  //第二场战斗第3层
    public const int MODEL_TOWN1 = 21;   //桃园步骤1
    public const int MODEL_TOWN2 = 22;   //桃园步骤2
    public const int MODEL_TOWN_STORY_ = 23;    //桃园剧情
    public const int MODEL_TOWN3 = 24;   //桃园步骤建筑升级1
    public const int MODEL_TOWN4 = 25;   //桃园步骤建筑升级2
    public const int MODEL_TOWN5 = 26;   //桃园步骤合成1
    public const int MODEL_TOWN6 = 27;   //桃园步骤合成2
    public const int MODEL_TOWN7 = 28;   //桃园步骤合成3
    public const int MODEL_TOWN8 = 29;   //桃园步骤合成4
    public const int MODEL_TOWN9 = 30;   //桃园步骤仓库1
    public const int MODEL_TOWN10 = 31;   //桃园步骤仓库2
    public const int MODEL_TOWN11 = 32;   //桃园步骤仓库3
    public const int MODEL_TOWN12 = 33;   //桃园步骤仓库4
    public const int MODEL_TOWN13 = 34;   //桃园步骤仓库5
    public const int MODEL_BATTLE_THIRD1 = 35;  //第三场战斗步骤1
    public const int MODEL_BATTLE_THIRD2 = 36;  //第三场战斗步骤2
    public const int MODEL_BATTLE_THIRD3 = 37;  //第三场战斗步骤3
    //public const int MODEL_BATTLE_THIRD4 = 38;  //第三场战斗第1层
    //public const int MODEL_BATTLE_THIRD5 = 39;  //第三场战斗第2层
    //public const int MODEL_BATTLE_THIRD6 = 40;  //第三场战斗第3层
    public const int MODEL_SUMMON1 = 38;    //召唤1
    public const int MODEL_SUMMON2 = 39;    //召唤2
    public const int MODEL_SUMMON3 = 40;    //召唤3

    //剧情常量
    public const int STORY_FIRST = 1;   //初始剧情
    public const int STORY_FIRST_FIGHT_END1 = 10001;    //第一场战斗结束
    public const int STORY_SECOND_FIGHT_START = 10002;  //第二场战斗开始
    public const int STORY_SECOND_FIGHT_END = 10003;    //第二场战斗结束
    public const int STORY_THIRD_FIGHT_START = 10004;   //第三长战斗开始
    public const int STORY_THIRD_FIGHT_END = 10005;     //第三场战斗结束剧情
    public const int STORY_TOWN = 10006;    //桃园剧情


    //新手引导副本
    public const int DUNGEONID_ID = 100000; //新手引导副本
    public const int GATE_ID = 100002;   //新手引导关卡ID
    public const int GATE_ID1 = 100003;  //新手引导关卡

    /// <summary>
    /// 展示剧情
    /// </summary>
    /// <param name="story_id"></param>
    /// <param name="call_back"></param>
    public static void SHOW_STORY(int story_id , GUIStory.CALL_BACK call_back )
    {
        GUIStory guiStory = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_STORY) as GUIStory;
        guiStory.Show(story_id, call_back);
    }

    /// <summary>
    /// 展示引导
    /// </summary>
    /// <param name="model_id"></param>
    public static void SHOW_GUIDE(int model_id)
    {
        GuideManager.GetInstance().ShowGuide(model_id);
    }
}