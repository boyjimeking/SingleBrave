using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Game.Resource;
using Game.Base;

//村界面类
//Author:sunyi
//2013-12-3

public class GUITown : GUIBase
{
    private const string RES_MAIN = "GUI_Town";//主资源地址
    private const string ITEM_PROPSWAREHOUSE = "Item_PropsWarehouse";//道具仓库地址
    private const string ITEM_EQUIPMENTSRENGTHENHOUSE = "Item_EquipmentStrengthenHouse";//设备强化地址
    private const string ITEM_RECONCILEHOUSE = "Item_ReconcileHouse";//调和屋地址
    private const string ITEM_EQUIPTHOUSE = "Item_EquiptHouse";//装备屋地址
    private const string WARNEPUIP = "Item_EquiptHouse/Warn";//装备屋提示图标地址
    private const string WARNITEM = "Item_ReconcileHouse/Warn";//调和屋提示图标地址
    private const string BTN_CHUAN = "BtnChuan";  //水流的按钮
    private const string BTN_LIN = "BtnLin";      //树林的按钮
    private const string BTN_SHAN = "BtnShan";    //山川的按钮
    private const string BTN_TIAN = "BtnTian";    //田林的按钮
    private const string BLACK = "Black";         //背景遮罩
    private const string BTN_BACK = "BtnBack"; //返回按钮
    private const string LB_CANCLICK = "Label";   //点击可获取素材，暂时代替动画
    private const string RES_ITEM_MESH = "GUI_Town_Item"; //物品
    private const string RES_FARM_MESH = "GUI_Town_Farm";    //农场点模型
    private const string RES_JINBI_MESH = "GUI_Town_Jinbi";   //金币模型
    private const string RES_DIAMOND_MESH = "GUI_Town_Diamond"; //砖石模型
    //private const string SP_TIAOHE_WARN = "Sp_Tiaohe";  //调合屋新满足提示
    //private const string SP_EQUIP_WARN = "Sp_Equip"; //装备屋新满足提示
    //3D场景
    private const string SCENE_ROOT = "VILLAGE";
    private const string Village_Bg = "effect_GUI_village";
    private const string EFFECT_TIAN = "GUI_village_lizi_ground_1";
    private const string EFFECT_TIAN1 = "GUI_village_lizi_ground_1_2";
    private const string EFFECT_SHAN = "GUI_village_lizi_mountain_1";
    private const string EFFECT_SHAN1 = "GUI_village_lizi_mountain_1_2";
    private const string EFFECT_LIN = "GUI_village_lizi_tree_1";
    private const string EFFECT_LIN1 = "GUI_village_lizi_tree_1_2";
    private const string EFFECT_CHUAN = "GUI_village_lizi_water_1";
    private const string EFFECT_CHUAN1 = "GUI_village_lizi_water_1_2";

    private GameObject m_cSceneRoot;
    private GameObject m_cVillageBg;
    private GameObject m_cEffectTian;
    private GameObject m_cEffectTian1;
    private GameObject m_cEffectShan;
    private GameObject m_cEffectShan1;
    private GameObject m_cEffectLin;
    private GameObject m_cEffectLin1;
    private GameObject m_cEffectChuan;
    private GameObject m_cEffectChuan1;
    private UISprite m_cEquipWarn;
    private UISprite m_cItemWarn;
    //private UISprite m_cSpTiaoheWarn;
    //private UISprite m_cSpEquipWarn;

    private GameObject m_cBtnPropsWarehouse;//道具仓库入口
    private GameObject m_cBtnEquipmentStrengthenHouse;//设备强化入口
    private GameObject m_cBtnReconcileHouse;//调和屋入口
    private GameObject m_cBtnEquiptHouse;//装备屋入口
    private GameObject m_cBtnChuan;  //水流的按钮
    private GameObject m_cBtnLin;    //树林的按钮
    private GameObject m_cBtnShan;   //山川的按钮
    private GameObject m_cBtnTian;   //田林的按钮
    private GameObject m_cBlack;     //背景遮罩
    private GameObject m_cBtnBack; //返回按钮
    private UnityEngine.Object m_cResItem;  //物品资源
    private UnityEngine.Object m_cResFarm;  //农场点资源
    private UnityEngine.Object m_cResGold;  //金币点资源
    private UnityEngine.Object m_cResDiamond;  //砖石点资源

    private long m_iClickTimeShan;    //单位时间可以点击采集
    private long m_iClickTimeChuan;   //单位时间可以点击采集
    private long m_iClickTimeTian;   //单位时间可以点击采集
    private long m_iClickTimeLin;     //单位时间可以点击采集

    private int m_iOneClickCollectNum;  //点击一次采集掉落的物品数量
    private int m_iCUR_CH_GUI = -1;        //Twon下级子界面 由于town在背景层，所以需要手动实现下面图标切换时候的隐藏

    private const float COLLECT_ITEM_DROP_TIME = 1f; //物品掉落时间
    private const float COLLECT_ITEM_MOVE_TIME = 0.6f; //物品移动至仓库时间
    private const float COLLECT_ITEM_WAIT_HIDEN = 1f; //隐藏等待
    private Vector3 VEC_CHUAN = new Vector3(-120, -245, 0);  //川生成物品位置
    private Vector3 VEC_TIAN = new Vector3(-25, -136, 0);
    private Vector3 VEC_SHAN = new Vector3(268, 276, 0);
    private Vector3 VEC_LIN = new Vector3(-242, 280, 0);

    private CollectItem m_cCollectItemShan = new CollectItem();                     //物品收集
    private CollectItem m_cCollectItemChuan = new CollectItem();                    //物品收集
    private CollectItem m_cCollectItemTian = new CollectItem();                     //物品收集
    private CollectItem m_cCollectItemLin = new CollectItem();                     //物品收集

    private bool m_bHasShow = false;  //加载过showobject

    /// 收集品类
    /// </summary>
    private class CollectItem
    {
        public Vector3 m_cHousePosition = new Vector3(-150, -80, 0);

        public List<GameObject> m_lstMesh;          //物体列表
        public List<Vector3> m_lstCurveStart;         //曲线起始点
        public List<float> m_lstCurveTop;                //曲线顶点
        public List<Vector3> m_lstCurveBottom;     //曲线底点
        public List<float> m_lstCurveStartTime;       //曲线开始时间
        public List<float> m_lstLineStartTime;          //直线开始时间
        public List<float> m_lstWaitTime;                 //等待时间
        public List<int> m_lstState;                          //状态
        public List<Vector3> m_lstLineTop;              //物品最终位置
        public List<bool> m_lstIsItem;                      //是否是物品
        public List<float> m_lstLineCost;                  //直线花费时间


        private const int STATE_NONE = -1;
        private const int STATE_BEGIN = 0;
        private const int STATE_CURVE = 1;
        private const int STATE_WAIT = 2;
        private const int STATE_LINE = 3;
        private const int STATE_WAIT_HIDEN = 4;
        private const int STATE_END = 5;

        private const float m_fWaiTime = 0.7f; //掉落等待

        public CollectItem()
        {

            //Vector3 start;
            //Vector3 end;
            //float cost = (start - end).magnitude / 10f;

            this.m_lstMesh = new List<GameObject>();
            this.m_lstCurveStart = new List<Vector3>();
            this.m_lstCurveTop = new List<float>();
            this.m_lstCurveBottom = new List<Vector3>();
            this.m_lstCurveStartTime = new List<float>();
            this.m_lstLineStartTime = new List<float>();
            this.m_lstWaitTime = new List<float>();
            this.m_lstState = new List<int>();
            this.m_lstLineTop = new List<Vector3>();
            this.m_lstIsItem = new List<bool>();
            this.m_lstLineCost = new List<float>();
        }

        /// <summary>
        /// 清除数据
        /// </summary>
        public void Clear()
        {
            this.m_lstCurveBottom.Clear();
            this.m_lstCurveStart.Clear();
            this.m_lstCurveStartTime.Clear();
            this.m_lstCurveTop.Clear();
            this.m_lstWaitTime.Clear();
            this.m_lstLineStartTime.Clear();
            this.m_lstState.Clear();
            this.m_lstLineTop.Clear();
            this.m_lstIsItem.Clear();
            this.m_lstLineCost.Clear();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Destory()
        {
            for (int i = 0; i < this.m_lstMesh.Count; i++)
            {
                if (this.m_lstMesh[i] != null)
                {
                    GameObject.DestroyImmediate(this.m_lstMesh[i]);
                }
                this.m_lstMesh[i] = null;
            }

            this.m_lstMesh.Clear();
            this.m_lstCurveStart.Clear();
            this.m_lstCurveBottom.Clear();
            this.m_lstCurveTop.Clear();
            this.m_lstCurveStartTime.Clear();
            this.m_lstLineStartTime.Clear();
            this.m_lstWaitTime.Clear();
            this.m_lstState.Clear();
            this.m_lstLineTop.Clear();
            this.m_lstIsItem.Clear();
            this.m_lstLineCost.Clear();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Destory(int i)
        {
            if (this.m_lstMesh[i] != null)
            {
                GameObject.DestroyImmediate(this.m_lstMesh[i]);
            }
            this.m_lstMesh[i] = null;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < m_lstState.Count; i++)
            {
                switch (m_lstState[i])
                {
                    case STATE_NONE:
                        break;
                    case STATE_BEGIN:
                        this.m_lstState[i]++;
                        this.m_lstMesh[i].SetActive(true);
                        break;
                    case STATE_CURVE:
                        if (UpdateCurve(i))
                        {
                            this.m_lstWaitTime[i] = GAME_TIME.TIME_FIXED();
                            this.m_lstState[i]++;
                        }
                        break;
                    case STATE_WAIT:
                        if (WaitTime(i))
                        {
                            this.m_lstLineStartTime[i] = GAME_TIME.TIME_FIXED();
                            if (this.m_lstIsItem[i])
                            {
                                this.m_lstLineTop[i] = m_cHousePosition;
                            }
                            else
                            {
                                this.m_lstLineTop[i] = this.m_lstMesh[i].transform.localPosition + new Vector3(0, 140, 0);
                            }
                            this.m_lstState[i]++;
                        }
                        break;
                    case STATE_LINE:
                        if (MoveToHouse(i))
                        {
                            this.m_lstLineStartTime[i] = GAME_TIME.TIME_FIXED();
                            this.m_lstState[i]++;
                        }
                        break;
                    case STATE_WAIT_HIDEN:
                        if (WaitHidden(i))
                        {
                            this.m_lstState[i]++;
                        }
                        break;
                    case STATE_END:
                        Destory(i);
                        break;
                }
            }
        }

        /// <summary>
        /// 更新曲线
        /// </summary>
        public bool UpdateCurve(int i)
        {
            bool finish = true;

            float dis = GAME_TIME.TIME_FIXED() - this.m_lstCurveStartTime[i];
            if (dis > COLLECT_ITEM_DROP_TIME)
            {
                this.m_lstMesh[i].transform.localPosition = this.m_lstCurveBottom[i];
                this.m_lstMesh[i].transform.localEulerAngles = new Vector3(0, 0, 360);
            }
            else
            {
                this.m_lstMesh[i].transform.localEulerAngles = new Vector3(0, 0, 360 * dis / COLLECT_ITEM_DROP_TIME);
                this.m_lstMesh[i].transform.localPosition = CMath.Curve(this.m_lstCurveStart[i], this.m_lstCurveBottom[i], this.m_lstCurveTop[i], dis / COLLECT_ITEM_DROP_TIME);
                finish = false;
            }

            return finish;
        }

        /// <summary>
        /// 移动至创库
        /// </summary>
        /// <returns></returns>
        public bool MoveToHouse(int i)
        {
            bool finish = true;

            float dis = GAME_TIME.TIME_FIXED() - m_lstLineStartTime[i];
            if (dis > COLLECT_ITEM_MOVE_TIME)
            {
                this.m_lstMesh[i].transform.localPosition = m_lstLineTop[i];
            }
            else
            {
                float posRate = dis / COLLECT_ITEM_MOVE_TIME;
                posRate = CMath.QuinticIn(posRate, 0, 1, 1);
                Vector3 pos = Vector3.Lerp(this.m_lstMesh[i].transform.localPosition, m_lstLineTop[i], posRate);
                this.m_lstMesh[i].transform.localPosition = pos;
                finish = false;
            }

            return finish;
        }

        /// <summary>
        /// 等待采集时间
        /// </summary>
        /// <returns></returns>
        public bool WaitTime(int i)
        {
            float dis = GAME_TIME.TIME_FIXED() - this.m_lstWaitTime[i];
            if (dis > m_fWaiTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 等待隐藏
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public bool WaitHidden(int i)
        {
            bool finish = true;

            float dis = GAME_TIME.TIME_FIXED() - m_lstLineStartTime[i];
            if (dis > COLLECT_ITEM_WAIT_HIDEN)
            {

                //this.m_lstMesh[i].GetComponent<UISprite>().alpha = 0;
                GUI_FUNCTION.SET_SPRITE_OBJECT_ALPHA(this.m_lstMesh[i], 0);
            }
            else
            {
                float posRate = dis / COLLECT_ITEM_MOVE_TIME;

                //this.m_lstMesh[i].GetComponent<UISprite>().alpha = 1 - posRate;
                GUI_FUNCTION.SET_SPRITE_OBJECT_ALPHA(this.m_lstMesh[i], 1 - posRate);

                finish = false;
            }

            return finish;
        }
    }

    /// <summary>
    /// 每一轮恢复内点击采集数据
    /// </summary>
    public class CollectInfo
    {
        public int m_iClickNumShan = 0;
        public int m_iClickNumChuan = 0;
        public int m_iClickNumTian = 0;
        public int m_iClickNumLin = 0;

        public int m_iGold = 0;
        public int m_iFarm = 0;
        public int m_iDiamond = 0;

        public List<int> m_vecItemTableIds = new List<int>();
        public List<int> m_vecItemNums = new List<int>();

    }

    public CollectInfo m_cCollectInfos;  //数据采集数据，提供发送接口用
    private bool m_bTownBlack = true;

    public GUITown(GUIManager guiMgr)
        : base(guiMgr, GUI_DEFINE.GUIID_TOWN, GUILAYER.GUI_BACKGROUND)
    { }

    /// <summary>
    /// 展示
    /// </summary>
    public override void Show()
    {
        if (Role.role.GetBaseProperty().m_iModelID == GUIDE_FUNCTION.MODEL_TOWN_STORY_)
        //if(false)
        {
            Role.role.GetBaseProperty().m_iModelID++;
            GUIDE_FUNCTION.SHOW_STORY(GUIDE_FUNCTION.STORY_TOWN, StoryCallBack);
            return;
        }

        //渐渐黑
        GUI_FUNCTION.BLACKSHOW();

        this.m_eLoadingState = LOADING_STATE.START;
        //GUI_FUNCTION.AYSNCLOADING_SHOW();

        if (this.m_cGUIObject == null)
        {
            //Debug.Log(" town show 2");
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_MAIN);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, Village_Bg);

            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_ITEM_MESH);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_JINBI_MESH);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_FARM_MESH);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_GUI_PATH, RES_DIAMOND_MESH);
        }
        else
        {
            InitGUI();
        }
    }

    /// <summary>
    /// 剧情回调
    /// </summary>
    private void StoryCallBack()
    {
        Show();
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM).Show();
    }

    /// <summary>
    /// 初始化GUI
    /// </summary>
    protected override void InitGUI()
    {
        base.Show();

        GUI_FUNCTION.AYSNCLOADING_HIDEN();

        //Town自己为背景层，所以将原背景层隐藏
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Hiden();
        //隐藏顶部
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Hiden();
        //开启摄像头
        CameraManager.GetInstance().ShowUITownCamera();

        //下方提示
        GUIBackFrameBottom gui = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        gui.ChangeBottomLabel(GAME_FUNCTION.STRING(STRING_DEFINE.INFO_TOWN));

        if (this.m_cGUIObject == null)
        {
            //Debug.Log(" town show ");
            this.m_cGUIObject = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_MAIN)) as GameObject;
            this.m_cGUIObject.transform.parent = GameObject.Find(GUI_DEFINE.GUI_ANCHOR_CENTER).transform;
            this.m_cGUIObject.transform.localScale = Vector3.one;

            this.m_cBtnPropsWarehouse = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, ITEM_PROPSWAREHOUSE);
            GUIComponentEvent propsWarehouseEvent = this.m_cBtnPropsWarehouse.AddComponent<GUIComponentEvent>();
            propsWarehouseEvent.AddIntputDelegate(OnClickPropsWarehouse);

            this.m_cBtnEquipmentStrengthenHouse = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, ITEM_EQUIPMENTSRENGTHENHOUSE);
            GUIComponentEvent equipmentStrengthenHouseEvent = this.m_cBtnEquipmentStrengthenHouse.AddComponent<GUIComponentEvent>();
            equipmentStrengthenHouseEvent.AddIntputDelegate(OnClickEquipmentStrengthenHouseEvent);

            this.m_cBtnReconcileHouse = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, ITEM_RECONCILEHOUSE);
            GUIComponentEvent reconcileHouseEvent = this.m_cBtnReconcileHouse.AddComponent<GUIComponentEvent>();
            reconcileHouseEvent.AddIntputDelegate(OnClickReconcileHouseEvent);

            this.m_cEquipWarn = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, WARNEPUIP);
            this.m_cItemWarn = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, WARNITEM);

            this.m_cBtnEquiptHouse = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, ITEM_EQUIPTHOUSE);
            GUIComponentEvent equiptHouseEvent = this.m_cBtnEquiptHouse.AddComponent<GUIComponentEvent>();
            equiptHouseEvent.AddIntputDelegate(OnClickEquiptHouseEvent);

            this.m_cBtnChuan = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_CHUAN);
            this.m_cBtnChuan.AddComponent<GUIComponentEvent>().AddIntputDelegate(Chuan_OnEvent);

            this.m_cBtnLin = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_LIN);
            this.m_cBtnLin.AddComponent<GUIComponentEvent>().AddIntputDelegate(Lin_OnEvent);

            this.m_cBtnShan = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_SHAN);
            this.m_cBtnShan.AddComponent<GUIComponentEvent>().AddIntputDelegate(Shan_OnEvent);

            this.m_cBtnTian = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_TIAN);
            this.m_cBtnTian.AddComponent<GUIComponentEvent>().AddIntputDelegate(Tian_OnEvent);

            this.m_cBlack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BLACK);

            this.m_cBtnBack = GUI_FINDATION.GET_GAME_OBJECT(this.m_cGUIObject, BTN_BACK);
            this.m_cBtnBack.AddComponent<GUIComponentEvent>().AddIntputDelegate(BackToMain);

            //this.m_cSpTiaoheWarn = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SP_TIAOHE_WARN);
            //this.m_cSpEquipWarn = GUI_FINDATION.GET_OBJ_COMPONENT<UISprite>(this.m_cGUIObject, SP_EQUIP_WARN);

            this.m_cResItem = (UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_ITEM_MESH);
            this.m_cResGold = (UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_JINBI_MESH);
            this.m_cResFarm = (UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_FARM_MESH);
            this.m_cResDiamond = (UnityEngine.Object)ResourcesManager.GetInstance().Load(RES_DIAMOND_MESH);

            this.m_cSceneRoot = GUI_FINDATION.FIND_GAME_OBJECT(SCENE_ROOT);
            this.m_cVillageBg = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(Village_Bg)) as GameObject;
            this.m_cVillageBg.transform.parent = this.m_cSceneRoot.transform;
            this.m_cVillageBg.transform.localPosition = Vector3.zero;
            this.m_cVillageBg.transform.localScale = Vector3.one;
            this.m_cEffectChuan = GUI_FINDATION.GET_GAME_OBJECT(this.m_cVillageBg, EFFECT_CHUAN);
            this.m_cEffectChuan1 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cVillageBg, EFFECT_CHUAN1);
            this.m_cEffectLin = GUI_FINDATION.GET_GAME_OBJECT(this.m_cVillageBg, EFFECT_LIN);
            this.m_cEffectLin1 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cVillageBg, EFFECT_LIN1);
            this.m_cEffectShan = GUI_FINDATION.GET_GAME_OBJECT(this.m_cVillageBg, EFFECT_SHAN);
            this.m_cEffectShan1 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cVillageBg, EFFECT_SHAN1);
            this.m_cEffectTian = GUI_FINDATION.GET_GAME_OBJECT(this.m_cVillageBg, EFFECT_TIAN);
            this.m_cEffectTian1 = GUI_FINDATION.GET_GAME_OBJECT(this.m_cVillageBg, EFFECT_TIAN1);
        }

        //播放村音效
        SoundManager.GetInstance().PlayBGM(SOUND_DEFINE.BGM_TOWN);

        //子界面
        m_iCUR_CH_GUI = -1;


        CheckWarnInfo();
        UpdateClickTimeWhenReShow();  //更新点击次数

        //村背景上移80 
        this.m_cGUIObject.transform.localPosition = new Vector3(0, 80, this.m_cGUIObject.transform.localPosition.z);

        this.m_cGUIMgr.SetCurGUIID(this.m_iID);

        this.m_cBlack.SetActive(false);

        SetLocalPos(Vector3.zero);

        //装备屋调和屋提示
        UpdateWarnShow();

        GUI_FUNCTION.BLACKHIDEN();

        //新手引导
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_TOWN3);
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_TOWN5);
        GUIDE_FUNCTION.SHOW_GUIDE(GUIDE_FUNCTION.MODEL_TOWN9);
    }

    /// <summary>
    /// 检查是否需要新可合成提示信息
    /// </summary>
    private void CheckWarnInfo()
    {
        //if (GAME_SETTING.s_vecWarnNew[2] == 1)
        //{
        //    this.m_cSpEquipWarn.enabled = true;
        //}
        //else
        //{
        //    this.m_cSpEquipWarn.enabled = false;
        //}
        //if (GAME_SETTING.s_vecWarnNew[3] == 1)
        //{
        //    this.m_cSpTiaoheWarn.enabled = true;
        //}
        //else
        //{
        //    this.m_cSpTiaoheWarn.enabled = false;
        //}
    }

    /// <summary>
    /// 渲染
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

        if (IsShow())
        {

            this.m_cCollectItemChuan.Update();
            this.m_cCollectItemLin.Update();
            this.m_cCollectItemShan.Update();
            this.m_cCollectItemTian.Update();
        }

        return true;
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public override void Destory()
    {
        base.Hiden();

        m_cCollectItemShan.Destory();
        m_cCollectItemChuan.Destory();
        m_cCollectItemTian.Destory();
        m_cCollectItemLin.Destory();

        if (m_cVillageBg != null )
            GameObject.DestroyImmediate(m_cVillageBg);
        this.m_cVillageBg = null;

        this.m_cSceneRoot = null;
        this.m_cVillageBg = null;
        this.m_cEffectTian = null;
        this.m_cEffectTian1 = null;
        this.m_cEffectShan = null;
        this.m_cEffectShan1 = null;
        this.m_cEffectLin = null;
        this.m_cEffectLin1 = null;
        this.m_cEffectChuan = null;
        this.m_cEffectChuan1 = null;

        this.m_cBtnPropsWarehouse = null;
        this.m_cBtnEquipmentStrengthenHouse = null;
        this.m_cBtnReconcileHouse = null;
        this.m_cBtnEquiptHouse = null;
        this.m_cBtnChuan = null; 
        this.m_cBtnLin = null; 
        this.m_cBtnShan = null; 
        this.m_cBtnTian = null; 
        this.m_cBlack = null; 
        this.m_cBtnBack = null;
        this.m_cResItem = null;
        this.m_cResFarm = null;
        this.m_cResGold = null;
        this.m_cResDiamond = null;
        this.m_cEquipWarn = null;
        this.m_cItemWarn = null;
        base.Destory();
    }

    /// <summary>
    /// 更新界面上可以点击的刷新
    /// </summary>
    private void UpdateClickTimeWhenReShow()
    {
        //上一次采集时间点
        long dtTian = Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.TIAN).m_lCollectTime;
        long dtLin = Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.LIN).m_lCollectTime;
        long dtChuan = Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.CHUAN).m_lCollectTime;
        long dtShan = Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.SHAN).m_lCollectTime;
        //距离上一次采集经过多少秒
        long sp1 = (DateTime.Now.Ticks / 10000000 - GAME_DEFINE.m_lTimeSpan - dtTian);
        long sp2 = (DateTime.Now.Ticks / 10000000 - GAME_DEFINE.m_lTimeSpan - dtLin);
        long sp3 = (DateTime.Now.Ticks / 10000000 - GAME_DEFINE.m_lTimeSpan - dtChuan);
        long sp4 = (DateTime.Now.Ticks / 10000000 - GAME_DEFINE.m_lTimeSpan - dtShan);
        //根据超过时间 回满
        bool canTian = sp1 > BuildingTableManager.GetInstance().GetTianTable(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.TIAN).m_iLevel).RecoveryTime;
        //回满
        if (canTian)
        {
            Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.TIAN).m_iCollectNum = BuildingTableManager.GetInstance().GetTianTable(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.TIAN).m_iLevel).HITNum;
        }

        bool canLin = sp2 > BuildingTableManager.GetInstance().GetLinTable(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.LIN).m_iLevel).RecoveryTime;
        //回满
        if (canLin)
        {
            Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.LIN).m_iCollectNum = BuildingTableManager.GetInstance().GetLinTable(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.LIN).m_iLevel).HITNum;
        }

        bool canChuan = sp3 > BuildingTableManager.GetInstance().GetChuanTable(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.CHUAN).m_iLevel).RecoveryTime;
        //回满
        if (canChuan)
        {
            Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.CHUAN).m_iCollectNum = BuildingTableManager.GetInstance().GetChuanTable(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.CHUAN).m_iLevel).HITNum;
        }

        bool canShuan = sp4 > BuildingTableManager.GetInstance().GetShanTable(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.SHAN).m_iLevel).RecoveryTime;
        //回满
        if (canShuan)
        {
            Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.SHAN).m_iCollectNum = BuildingTableManager.GetInstance().GetShanTable(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.SHAN).m_iLevel).HITNum;
        }


        //可采集次数为0，关闭可采集提示
        if (Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.SHAN).m_iCollectNum == 0)
        {
            this.m_cBtnShan.collider.enabled = false;
            this.m_cEffectShan.particleSystem.Stop();
            this.m_cEffectShan1.particleSystem.Stop();
        }
        else
        {
            this.m_cBtnShan.collider.enabled = true;
            this.m_cEffectShan.particleSystem.Play(true);
            this.m_cEffectShan1.particleSystem.Play(true);
        }
        if (Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.TIAN).m_iCollectNum == 0)
        {
            this.m_cBtnTian.collider.enabled = false;
            this.m_cEffectTian.particleSystem.Stop();
            this.m_cEffectTian1.particleSystem.Stop();
        }
        else
        {
            this.m_cBtnTian.collider.enabled = true;
            this.m_cEffectTian.particleSystem.Play(true);
            this.m_cEffectTian1.particleSystem.Play(true);
        }
        if (Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.LIN).m_iCollectNum == 0)
        {
            this.m_cBtnLin.collider.enabled = false;
            this.m_cEffectLin.particleSystem.Stop();
            this.m_cEffectLin1.particleSystem.Stop();
        }
        else
        {
            this.m_cBtnLin.collider.enabled = true;
            this.m_cEffectLin.particleSystem.Play(true);
            this.m_cEffectLin1.particleSystem.Play(true);
        }
        if (Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.CHUAN).m_iCollectNum == 0)
        {
            this.m_cBtnChuan.collider.enabled = false;
            this.m_cEffectChuan.particleSystem.Stop();
            this.m_cEffectChuan1.particleSystem.Stop();
        }
        else
        {
            this.m_cBtnChuan.collider.enabled = true;
            this.m_cEffectChuan.particleSystem.Play(true);
            this.m_cEffectChuan1.particleSystem.Play(true);
        }
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public override void Hiden()
    {
        //将原背景层显示
        this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();

        GUIBackFrameTop backtop = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP) as GUIBackFrameTop;
        backtop.Show();

        CameraManager.GetInstance().HidenUITownCamera();

        if (m_iCUR_CH_GUI != -1)
        {
            GUIBase tmp = this.m_cGUIMgr.GetGUI(this.m_iCUR_CH_GUI);
            if (tmp != null)
            {
                tmp.Hiden();
            }
        }

        SendCollectData();

        //SetLocalPos(Vector3.one * 0xFFFF);

        m_cCollectItemShan.Destory();
        m_cCollectItemChuan.Destory();
        m_cCollectItemTian.Destory();
        m_cCollectItemLin.Destory();

        Destory();
    }

    /// <summary>
    /// 发送采集数据
    /// </summary>
    private void SendCollectData()
    {
        if (m_cCollectInfos != null)
        {
            //string cw1 = "";
            //for (int i = 0; i < m_cCollectInfos.m_vecItemTableIds.Count; i++)
            //{
            //    cw1+=m_cCollectInfos.m_vecItemTableIds[i]+":"+m_cCollectInfos.m_vecItemNums[i]+"|";
            //}
            //Debug.Log("old:  "+cw1);

            //累加采集数据  Eg: 1001:1|1001:1 优化为1001:2
            List<int> SendDataTableID = new List<int>();
            List<int> SendDataNums = new List<int>();

            for (int i = 0; i < m_cCollectInfos.m_vecItemTableIds.Count; i++)
            {
                bool tag = false;
                for (int j = 0; j < SendDataTableID.Count; j++)
                {
                    if (SendDataTableID[j] == m_cCollectInfos.m_vecItemTableIds[i])  //发送数据中也有新采集tableid
                    {
                        tag = true;

                        SendDataNums[j] += m_cCollectInfos.m_vecItemNums[i];
                        break;
                    }
                }
                if (!tag)  //不存在
                {
                    SendDataTableID.Add(m_cCollectInfos.m_vecItemTableIds[i]);
                    SendDataNums.Add(m_cCollectInfos.m_vecItemNums[i]);
                }

            }

            //string cw2 = "";
            //for (int i = 0; i < SendDataTableID.Count; i++)
            //{
            //    cw2 += SendDataTableID[i] + ":" + SendDataNums[i] + "|";
            //}
            //Debug.Log("new:  " + cw2);

            SendAgent.SendItemCollectReq(Role.role.GetBaseProperty().m_iPlayerId,
                m_cCollectInfos.m_iClickNumShan,
                m_cCollectInfos.m_iClickNumChuan,
                m_cCollectInfos.m_iClickNumTian,
                m_cCollectInfos.m_iClickNumLin,
                m_cCollectInfos.m_iGold,
                m_cCollectInfos.m_iFarm,
                SendDataTableID.ToArray(),
                SendDataNums.ToArray());

            m_cCollectInfos = null;

        }
    }

    /// <summary>
    /// 道具仓库入口点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickPropsWarehouse(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            SendCollectData();

            if (SessionManager.GetInstance().Refresh())
            {
                SessionManager.GetInstance().SetCallBack(GUIPropsWareHouseShow);
            }
            else
            {
                GUIPropsWareHouseShow();
            }
        }
    }

    /// <summary>
    /// 仓库显示
    /// </summary>
    private void GUIPropsWareHouseShow()
    {


        GUIPropsWareHouse propsWareHouse = (GUIPropsWareHouse)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_PROPSWAREHOUSE);
        propsWareHouse.Show();
    }

    /// <summary>
    /// 设备强化入口点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickEquipmentStrengthenHouseEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            SendCollectData();

            if (SessionManager.GetInstance().Refresh())
            {
                SessionManager.GetInstance().SetCallBack(GUIEquipUpgradeShow);
            }
            else
            {
                GUIEquipUpgradeShow();
            }


        }
    }

    /// <summary>
    /// 装备升级显示
    /// </summary>
    private void GUIEquipUpgradeShow()
    {

        GUIEquipUpgrade guiep = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_EQUIPUPGRADE) as GUIEquipUpgrade;
        guiep.Show();
    }

    /// <summary>
    /// 调和屋入口点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickReconcileHouseEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            SendCollectData();

            if (SessionManager.GetInstance().Refresh())
            {
                SessionManager.GetInstance().SetCallBack(GUIReconceliHouse);
            }
            else
            {
                GUIReconceliHouse();
            }
        }
    }

    /// <summary>
    /// 消耗品合成显示
    /// </summary>
    private void GUIReconceliHouse()
    {
        ////已经查看过提示信息
        //GAME_SETTING.s_vecWarnNew[3] = 0;
        //GAME_SETTING.SaveWarnInfo();

        //更新临时显示虚拟数据，本地操作不会对真实数据改动，等待服务器返回真实有效数据
        foreach (Item item in Role.role.GetItemProperty().GetAllItem())
        {
            item.m_iDummyNum = item.m_iNum;
        }
        GUIReconceliHouse house = (GUIReconceliHouse)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_RECONCELIHOUSE);
        house.SetSendData(new List<int>(), new List<int>());
        house.Show();
    }

    /// <summary>
    /// 装备屋入口点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void OnClickEquiptHouseEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            SendCollectData();

            if (SessionManager.GetInstance().Refresh())
            {
                SessionManager.GetInstance().SetCallBack(GUIEquipmentHouseShow);
            }
            else
            {
                GUIEquipmentHouseShow();
            }
        }
    }

    /// <summary>
    /// 设备升级显示
    /// </summary>
    private void GUIEquipmentHouseShow()
    {
        ////已经查看过提示信息
        //GAME_SETTING.s_vecWarnNew[2] = 0;
        //GAME_SETTING.SaveWarnInfo();

        //更新临时显示虚拟数据，本地操作不会对真实数据改动，等待服务器返回真实有效数据
        foreach (Item item in Role.role.GetItemProperty().GetAllItem())
        {
            item.m_iDummyNum = item.m_iNum;
        }

        GUIEquipmentHouse equipmentHouse = (GUIEquipmentHouse)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_EQUIPMENTHOUSE);
        equipmentHouse.SetSendData(new List<int>(), new List<int>());
        equipmentHouse.Show();
    }

    /// <summary>
    /// 水流点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void Chuan_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.CHUAN).m_iCollectNum > 0)
            {
                //获得该川等级可采集的物品列表
                List<int> items = BuildingTableManager.GetInstance().GetChuanItem(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.CHUAN).m_iLevel);
                //获得该川等级可采集的物品列表权重
                List<int> itemsWeight = BuildingTableManager.GetInstance().GetChuanItemWeight(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.CHUAN).m_iLevel);
                //获得该川等级可采集的金币，农场点，砖石数量列表
                List<int> itemGFD = BuildingTableManager.GetInstance().GetChuanGoldFarmDiamond(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.CHUAN).m_iLevel);
                //获得该川等级可采集的金币，农场点，砖石权重列表
                List<int> itemGFDWeight = BuildingTableManager.GetInstance().GetChuanGoldFarmDiamondWeight(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.CHUAN).m_iLevel);
                //合并物品和金币农场点砖石权重去随机
                itemGFD.AddRange(items);
                itemGFDWeight.AddRange(itemsWeight);
                //点击一次，做三次权重采集
                float[] weights = ChangeToFloatWeight(itemGFDWeight);
                //得到的三个随机结果
                m_iOneClickCollectNum = GAME_FUNCTION.RANDOM(2, 4);
                int[] randomGet = GAME_FUNCTION.RANDOM_BET(m_iOneClickCollectNum, weights);
                //装备采集实体类
                CollectInfo tmp = new CollectInfo();
                for (int i = 0; i < randomGet.Length; i++)
                {
                    int index = randomGet[i];
                    if (index < 3)  //前三个是金币 农场点 砖石 非物品
                    {
                        //采集数量
                        int num = itemGFD[index];
                        if (index == 0)
                        {
                            GeneratorGold(VEC_CHUAN, BUILDING_TYPE.CHUAN);
                            //金币和农场点上下浮动20%
                            num = (int)GAME_FUNCTION.RANDOM(num * 0.8f, num * 1.2f); 
                            tmp.m_iGold += num;
                        }
                        if (index == 1)
                        {
                            GeneratorFarmPoint(VEC_CHUAN, BUILDING_TYPE.CHUAN);
                            //金币和农场点上下浮动20%
                            num = (int)GAME_FUNCTION.RANDOM(num * 0.8f, num * 1.2f); 
                            tmp.m_iFarm += num;
                        }
                        if (index == 2)
                        {
                            GeneratorDiamond(VEC_CHUAN, BUILDING_TYPE.CHUAN);
                            tmp.m_iDiamond += num;
                        }
                    }
                    else
                    {
                        if (!GeneratorItem(VEC_CHUAN, itemGFD[index], BUILDING_TYPE.CHUAN))
                        {
                            return;
                        }
                        tmp.m_vecItemTableIds.Add(itemGFD[index]);
                        tmp.m_vecItemNums.Add(1);
                    }
                }
                tmp.m_iClickNumChuan = 1;
                //将一次采集数据加入到所有采集列表，等待发送采集数据列表
                AddCollectData(tmp);

                Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.CHUAN).m_lCollectTime = DateTime.Now.Ticks / 10000000 - GAME_DEFINE.m_lTimeSpan; //当前时间减去时间戳就是现在服务器的时间

                //采集音效
                //SoundManager.GetInstance().PlaySound(SOUND_DEFINE.SE_ITEM_GET);
            }

            UpdateClickTimeWhenReShow();

        }
    }

    /// <summary>
    /// 树林点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void Lin_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.LIN).m_iCollectNum > 0)
            {
                //获得该林等级可采集的物品列表
                List<int> items = BuildingTableManager.GetInstance().GetLinItem(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.LIN).m_iLevel);
                //获得该林等级可采集的物品列表权重
                List<int> itemsWeight = BuildingTableManager.GetInstance().GetLinItemWeight(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.LIN).m_iLevel);
                //获得该林等级可采集的金币，农场点，砖石数量列表
                List<int> itemGFD = BuildingTableManager.GetInstance().GetLinGoldFarmDiamond(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.LIN).m_iLevel);
                //获得该林等级可采集的金币，农场点，砖石权重列表
                List<int> itemGFDWeight = BuildingTableManager.GetInstance().GetLinGoldFarmDiamondWeight(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.LIN).m_iLevel);
                //合并物品和金币农场点砖石权重去随机
                itemGFD.AddRange(items);
                itemGFDWeight.AddRange(itemsWeight);
                //点击一次，做三次权重采集
                float[] weights = ChangeToFloatWeight(itemGFDWeight);
                //得到的三个随机结果
                m_iOneClickCollectNum = GAME_FUNCTION.RANDOM(2, 4);
                int[] randomGet = GAME_FUNCTION.RANDOM_BET(m_iOneClickCollectNum, weights);
                //装备采集实体类
                CollectInfo tmp = new CollectInfo();
                for (int i = 0; i < randomGet.Length; i++)
                {
                    int index = randomGet[i];
                    if (index < 3)  //前三个是金币 农场点 砖石 非物品
                    {
                        //采集数量
                        int num = itemGFD[index];
                        if (index == 0)
                        {
                            GeneratorGold(VEC_LIN, BUILDING_TYPE.LIN);
                            //金币和农场点上下浮动20%
                            num = (int) GAME_FUNCTION.RANDOM(num * 0.8f, num * 1.2f); 
                            tmp.m_iGold += num;
                        }
                        if (index == 1)
                        {
                            GeneratorFarmPoint(VEC_LIN, BUILDING_TYPE.LIN);
                            //金币和农场点上下浮动20%
                            num = (int)GAME_FUNCTION.RANDOM(num * 0.8f, num * 1.2f); 
                            tmp.m_iFarm += num;
                        }
                        if (index == 2)
                        {
                            GeneratorDiamond(VEC_LIN, BUILDING_TYPE.LIN);
                            tmp.m_iDiamond += num;
                        }
                    }
                    else
                    {
                        if (!GeneratorItem(VEC_LIN, itemGFD[index], BUILDING_TYPE.LIN))
                        {
                            return;
                        }
                        tmp.m_vecItemTableIds.Add(itemGFD[index]);
                        tmp.m_vecItemNums.Add(1);
                    }
                }
                tmp.m_iClickNumLin = 1;
                //将一次采集数据加入到所有采集列表，等待发送采集数据列表
                AddCollectData(tmp);

                //采集音效
                //SoundManager.GetInstance().PlaySound(SOUND_DEFINE.SE_ITEM_GET);

                //当前时间减去时间戳就是现在服务器的时间
                Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.LIN).m_lCollectTime = DateTime.Now.Ticks / 10000000 - GAME_DEFINE.m_lTimeSpan;
            }

            UpdateClickTimeWhenReShow();

        }
    }

    /// <summary>
    /// 山川点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void Shan_OnEvent(GUI_INPUT_INFO info, object[] args)
    {

        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {

            if (Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.SHAN).m_iCollectNum > 0)
            {
                //获得该山等级可采集的物品列表
                List<int> items = BuildingTableManager.GetInstance().GetShanItem(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.SHAN).m_iLevel);
                //获得该山等级可采集的物品列表权重
                List<int> itemsWeight = BuildingTableManager.GetInstance().GetShanItemWeight(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.SHAN).m_iLevel);
                //获得该山等级可采集的金币，农场点，砖石数量列表
                List<int> itemGFD = BuildingTableManager.GetInstance().GetShanGoldFarmDiamond(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.SHAN).m_iLevel);
                //获得该山等级可采集的金币，农场点，砖石权重列表
                List<int> itemGFDWeight = BuildingTableManager.GetInstance().GetShanGoldFarmDiamondWeight(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.SHAN).m_iLevel);
                //合并物品和金币农场点砖石权重去随机
                itemGFD.AddRange(items);
                itemGFDWeight.AddRange(itemsWeight);
                //点击一次，做三次权重采集
                float[] weights = ChangeToFloatWeight(itemGFDWeight);
                //得到的三个随机结果
                m_iOneClickCollectNum = GAME_FUNCTION.RANDOM(2, 4);
                int[] randomGet = GAME_FUNCTION.RANDOM_BET(m_iOneClickCollectNum, weights);
                //装备采集实体类
                CollectInfo tmp = new CollectInfo();
                for (int i = 0; i < randomGet.Length; i++)
                {
                    int index = randomGet[i];
                    if (index < 3)  //前三个是金币 农场点 砖石 非物品
                    {
                        //采集数量
                        int num = itemGFD[index];
                        if (index == 0)
                        {
                            GeneratorGold(VEC_SHAN, BUILDING_TYPE.SHAN);
                            //金币和农场点上下浮动20%
                            num = (int)GAME_FUNCTION.RANDOM(num * 0.8f, num * 1.2f); 
                            tmp.m_iGold += num;
                        }
                        if (index == 1)
                        {
                            GeneratorFarmPoint(VEC_SHAN, BUILDING_TYPE.SHAN);
                            //金币和农场点上下浮动20%
                            num = (int)GAME_FUNCTION.RANDOM(num * 0.8f, num * 1.2f); 
                            tmp.m_iFarm += num;
                        }
                        if (index == 2)
                        {
                            GeneratorDiamond(VEC_SHAN, BUILDING_TYPE.SHAN);
                            tmp.m_iDiamond += num;
                        }
                    }
                    else
                    {
                        if (!GeneratorItem(VEC_SHAN, itemGFD[index], BUILDING_TYPE.SHAN))
                        {
                            return;
                        }

                        tmp.m_vecItemTableIds.Add(itemGFD[index]);
                        tmp.m_vecItemNums.Add(1);
                    }
                }
                tmp.m_iClickNumShan = 1;
                //将一次采集数据加入到所有采集列表，等待发送采集数据列表
                AddCollectData(tmp);

                //当前时间减去时间戳就是现在服务器的时间
                Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.SHAN).m_lCollectTime = DateTime.Now.Ticks / 10000000 - GAME_DEFINE.m_lTimeSpan;

                //采集音效
                //SoundManager.GetInstance().PlaySound(SOUND_DEFINE.SE_ITEM_GET);
            }

            UpdateClickTimeWhenReShow();

        }
    }

    /// <summary>
    /// 田林点击事件
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void Tian_OnEvent(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            if (Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.TIAN).m_iCollectNum > 0)
            {
                //获得该田等级可采集的物品列表
                List<int> items = BuildingTableManager.GetInstance().GetTianItem(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.TIAN).m_iLevel);
                //获得该田等级可采集的物品列表权重
                List<int> itemsWeight = BuildingTableManager.GetInstance().GetTianItemWeight(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.TIAN).m_iLevel);
                //获得该田等级可采集的金币，农场点，砖石数量列表
                List<int> itemGFD = BuildingTableManager.GetInstance().GetTianGoldFarmDiamond(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.TIAN).m_iLevel);
                //获得该田等级可采集的金币，农场点，砖石权重列表
                List<int> itemGFDWeight = BuildingTableManager.GetInstance().GetTianGoldFarmDiamondWeight(Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.TIAN).m_iLevel);
                //合并物品和金币农场点砖石权重去随机
                itemGFD.AddRange(items);
                itemGFDWeight.AddRange(itemsWeight);
                //点击一次，做三次权重采集
                float[] weights = ChangeToFloatWeight(itemGFDWeight);
                //得到的三个随机结果
                m_iOneClickCollectNum = GAME_FUNCTION.RANDOM(2, 4);
                int[] randomGet = GAME_FUNCTION.RANDOM_BET(m_iOneClickCollectNum, weights);
                //装备采集实体类
                CollectInfo tmp = new CollectInfo();
                for (int i = 0; i < randomGet.Length; i++)
                {
                    int index = randomGet[i];
                    if (index < 3)  //前三个是金币 农场点 砖石 非物品
                    {
                        //采集数量
                        int num = itemGFD[index];
                        if (index == 0)
                        {
                            GeneratorGold(VEC_TIAN, BUILDING_TYPE.TIAN);
                            //金币和农场点上下浮动20%
                            num = (int)GAME_FUNCTION.RANDOM(num * 0.8f, num * 1.2f); 
                            tmp.m_iGold += num;
                        }
                        if (index == 1)
                        {
                            GeneratorFarmPoint(VEC_TIAN, BUILDING_TYPE.TIAN);
                            //金币和农场点上下浮动20%
                            num = (int)GAME_FUNCTION.RANDOM(num * 0.8f, num * 1.2f); 
                            tmp.m_iFarm += num;
                        }
                        if (index == 2)
                        {
                            GeneratorDiamond(VEC_TIAN, BUILDING_TYPE.TIAN);
                            tmp.m_iDiamond += num;
                        }
                    }
                    else
                    {
                        if (!GeneratorItem(VEC_TIAN, itemGFD[index], BUILDING_TYPE.TIAN))
                        {
                            return;
                        }
                        tmp.m_vecItemTableIds.Add(itemGFD[index]);
                        tmp.m_vecItemNums.Add(1);
                    }
                }
                tmp.m_iClickNumTian = 1;
                //将一次采集数据加入到所有采集列表，等待发送采集数据列表
                AddCollectData(tmp);

                //当前时间减去时间戳就是现在服务器的时间
                Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.TIAN).m_lCollectTime = DateTime.Now.Ticks /10000000 - GAME_DEFINE.m_lTimeSpan;

                //采集音效
                //SoundManager.GetInstance().PlaySound(SOUND_DEFINE.SE_ITEM_GET);
            }

            UpdateClickTimeWhenReShow();

        }
    }

    /// <summary>
    /// 返回主界面
    /// </summary>
    /// <param name="info"></param>
    /// <param name="args"></param>
    private void BackToMain(GUI_INPUT_INFO info, object[] args)
    {
        if (info.m_eType == GUI_INPUT_INFO.GUI_INPUT_TYPE.CLICK)
        {
            this.m_cGUIMgr.HidenCurGUI();

            if (SessionManager.GetInstance().Refresh())
            {
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MAIN).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show);
                SessionManager.GetInstance().SetCallBack(this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show);
            }
            else
            {
                GUIMain guimain = (GUIMain)this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_MAIN);
                guimain.Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMETOP).Show();
                this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKGROUND).Show();
            }
        }
    }

    /// <summary>
    /// 设置城镇子节点ID
    /// </summary>
    /// <param name="childId"></param>
    public void SetTownChildId(int childId)
    {
        m_iCUR_CH_GUI = childId;
    }

    /// <summary>
    /// 设置城镇遮罩是否可见
    /// </summary>
    /// <param name="hiden"></param>
    public void SetTownBlack(bool hiden)
    {
        this.m_cBlack.SetActive(!hiden);
    }

    /// <summary>
    /// 添加采集数据
    /// </summary>
    /// <param name="data"></param>
    public void AddCollectData(CollectInfo data)
    {
        if (m_cCollectInfos == null)
        {
            m_cCollectInfos = new CollectInfo();
        }
        //加入素材采集列表
        m_cCollectInfos.m_iClickNumChuan += data.m_iClickNumChuan;
        m_cCollectInfos.m_iClickNumLin += data.m_iClickNumLin;
        m_cCollectInfos.m_iClickNumShan += data.m_iClickNumShan;
        m_cCollectInfos.m_iClickNumTian += data.m_iClickNumTian;
        m_cCollectInfos.m_iGold += data.m_iGold;
        m_cCollectInfos.m_iFarm += data.m_iFarm;
        m_cCollectInfos.m_vecItemNums.AddRange(data.m_vecItemNums);
        m_cCollectInfos.m_vecItemTableIds.AddRange(data.m_vecItemTableIds);

        //更新本地Property等内存数据
        //更新点击次数
        Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.TIAN).m_iCollectNum -= data.m_iClickNumTian;
        Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.SHAN).m_iCollectNum -= data.m_iClickNumShan;
        Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.CHUAN).m_iCollectNum -= data.m_iClickNumChuan;
        Role.role.GetBuildingProperty().GetBuilding(BUILDING_TYPE.LIN).m_iCollectNum -= data.m_iClickNumLin;
        //更新金币农场点砖石
        Role.role.GetBaseProperty().m_iGold += data.m_iGold;
        Role.role.GetBaseProperty().m_iFarmPoint += data.m_iFarm;
        Role.role.GetBaseProperty().m_iDiamond += data.m_iDiamond;
        //更新本地物品列表
        for (int i = 0; i < data.m_vecItemTableIds.Count; i++)
        {
            int tableID = data.m_vecItemTableIds[i];
            //从本地列表中读取该物品
            Item item = Role.role.GetItemProperty().GetItemByTableID(tableID);
            if (item == null) //如果客户端没有，则加入新的物品
            {
                item = new Item(tableID);
                item.m_iID = -2;
                item.m_iNum = 0;  //如果是新的，临时客户端加入虚拟数量1，但是实际为0，不存在
                item.m_iDummyNum = data.m_vecItemNums[i];
                item.m_bNew = true;
                Role.role.GetItemProperty().AddItem(item);
            }
            else  //如果客户端有，则更新物品数量
            {
                item.m_iDummyNum += data.m_vecItemNums[i];
                item.m_bNew = true;
                Role.role.GetItemProperty().AddItem(item);
            }
        }

        //打印每次点击获得数据 For test
        //Debug.Log("Gold: " + data.m_iGold);
        //Debug.Log("Farm: " + data.m_iFarm);
        //Debug.Log("Diamond: " + data.m_iDiamond);
        //Debug.Log("Chuan: " + data.m_iClickNumChuan);
        //Debug.Log("Lin: " + data.m_iClickNumLin);
        //Debug.Log("Shan: " + data.m_iClickNumShan);
        //Debug.Log("Tian: " + data.m_iClickNumTian);
        //for (int i = 0; i < data.m_vecItemNums.Count; i++)
        //{
        //    Debug.Log("ItemID: " + data.m_vecItemTableIds[i] + " ItemNum: " + data.m_vecItemNums[i]);
        //}

    }

    /// <summary>
    /// 生成物品
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="itemTableID"></param>
    public bool GeneratorItem(Vector3 pos, int itemTableID, BUILDING_TYPE type)
    {
        //计算将要采集到的物品是否会造成物品栏超出最大限度
        List<Item> roleItems = Role.role.GetItemProperty().GetAllItem();
        List<Item> TmpItems = new List<Item>();
        for (int i = 0; i < roleItems.Count; i++)
        {
            Item tmp = new Item(roleItems[i].m_iTableID);
            tmp.m_iID = roleItems[i].m_iID;
            tmp.m_iNum = roleItems[i].m_iNum;
            TmpItems.Add(tmp);
        }
        Item newItem = new Item(itemTableID);
        newItem.m_iNum = 1;
        newItem.m_iID = -2;
        TmpItems = Role.role.GetItemProperty().AddItem(TmpItems, newItem);
        if (Role.role.GetItemProperty().GetAllItemCount(TmpItems) > Role.role.GetBaseProperty().m_iMaxItem)
        {
            GUI_FUNCTION.MESSAGEM(null, "仓库已满，将无法拾取之后的物品\n#ff0000]" + newItem.m_strName);
            return false;
        }

        float bottomX = GAME_FUNCTION.RANDOM(0f, 100f) - 50;
        Vector3 bottom = new Vector3(bottomX + pos.x, pos.y - 60f, 0);
        float heigh = GAME_FUNCTION.RANDOM(60f, 80f);

        //GameObject obj = GameObject.Instantiate(this.m_cResItem) as GameObject;
        ItemTable table = ItemTableManager.GetInstance().GetItem(itemTableID);
        UIAtlas at = GUI_FUNCTION.GET_ITEM_ATLAS(table.SpiritName);
        GameObject obj = GUI_FUNCTION.GENERATOR_SPRITE_OBJECT(table.SpiritName, GAME_DEFINE.U3D_OBJECT_LAYER_UI, at);
        obj.transform.parent = this.m_cGUIObject.transform;
        obj.transform.localScale = Vector3.one * 0.7f;
        obj.transform.localPosition = pos;
        obj.SetActive(false);


        //UISprite sp = obj.GetComponent<UISprite>();
        //GUI_FUNCTION.SET_ITEM_DOWN(sp,table.SpiritName);

        AddCollectItem(type, obj, pos, bottom, heigh, true);

        return true;
    }

    /// <summary>
    /// 生成金币
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="?"></param>
    public void GeneratorGold(Vector3 pos, BUILDING_TYPE type)
    {
        float bottomX = GAME_FUNCTION.RANDOM(0f, 100f) - 50;
        Vector3 bottom = new Vector3(bottomX + pos.x, pos.y - 60f, 0);
        float heigh = GAME_FUNCTION.RANDOM(60f, 80f);

        //GameObject obj = GameObject.Instantiate(this.m_cResGold) as GameObject;
        UIAtlas at = (this.m_cResGold as GameObject).GetComponent<UISprite>().atlas;
        GameObject obj = GUI_FUNCTION.GENERATOR_SPRITE_OBJECT("zell_thum", GAME_DEFINE.U3D_OBJECT_LAYER_UI, at);
        obj.transform.parent = this.m_cGUIObject.transform;
        obj.transform.localScale = Vector3.one * 0.5f;
        obj.transform.localPosition = pos;
        obj.SetActive(false);

        AddCollectItem(type, obj, pos, bottom, heigh, false);

    }

    /// <summary>
    /// 生成农场点
    /// </summary>
    /// <param name="pos"></param>
    public void GeneratorFarmPoint(Vector3 pos, BUILDING_TYPE type)
    {
        float bottomX = GAME_FUNCTION.RANDOM(0f, 100f) - 50;
        Vector3 bottom = new Vector3(bottomX + pos.x, pos.y - 60f, 0);
        float heigh = GAME_FUNCTION.RANDOM(60f, 80f);

        //GameObject obj = GameObject.Instantiate(this.m_cResFarm) as GameObject;
        UIAtlas at = (this.m_cResFarm as GameObject).GetComponent<UISprite>().atlas;
        GameObject obj = GUI_FUNCTION.GENERATOR_SPRITE_OBJECT("karma_thum", GAME_DEFINE.U3D_OBJECT_LAYER_UI, at);
        obj.transform.parent = this.m_cGUIObject.transform;
        obj.transform.localScale = Vector3.one * 0.5f;
        obj.transform.localPosition = pos;
        obj.SetActive(false);

        AddCollectItem(type, obj, pos, bottom, heigh, false);

    }

    /// <summary>
    /// 生成砖石
    /// </summary>
    /// <param name="pos"></param>
    public void GeneratorDiamond(Vector3 pos, BUILDING_TYPE type)
    {
        float bottomX = GAME_FUNCTION.RANDOM(0f, 100f) - 50;
        Vector3 bottom = new Vector3(bottomX + pos.x, pos.y - 60f, 0);
        float heigh = GAME_FUNCTION.RANDOM(60f, 80f);

        //GameObject obj = GameObject.Instantiate(this.m_cResDiamond) as GameObject;
        UIAtlas at = (this.m_cResDiamond as GameObject).GetComponent<UISprite>().atlas;
        GameObject obj = GUI_FUNCTION.GENERATOR_SPRITE_OBJECT("gem", GAME_DEFINE.U3D_OBJECT_LAYER_UI, at);
        obj.transform.parent = this.m_cGUIObject.transform;
        obj.transform.localScale = Vector3.one * 0.5f;
        obj.transform.localPosition = pos;
        obj.SetActive(false);

        AddCollectItem(type, obj, pos, bottom, heigh, false);
    }

    /// <summary>
    /// 加入动画参数
    /// </summary>
    /// <param name="type"></param>
    private void AddCollectItem(BUILDING_TYPE type, GameObject obj, Vector3 pos, Vector3 bottom, float heigh, bool isItem)
    {
        float fixTime = GAME_FUNCTION.RANDOM(0f, 0.3f);
        switch (type)
        {
            case BUILDING_TYPE.SHAN:
                this.m_cCollectItemShan.m_lstMesh.Add(obj);
                this.m_cCollectItemShan.m_lstCurveStart.Add(pos);
                this.m_cCollectItemShan.m_lstCurveTop.Add(heigh);
                this.m_cCollectItemShan.m_lstCurveBottom.Add(bottom);
                this.m_cCollectItemShan.m_lstCurveStartTime.Add(GAME_TIME.TIME_FIXED() + fixTime);
                this.m_cCollectItemShan.m_lstLineStartTime.Add(GAME_TIME.TIME_FIXED());
                this.m_cCollectItemShan.m_lstState.Add(0);
                this.m_cCollectItemShan.m_lstWaitTime.Add(GAME_TIME.TIME_FIXED());
                this.m_cCollectItemShan.m_lstIsItem.Add(isItem);
                this.m_cCollectItemShan.m_lstLineTop.Add(Vector3.one);

                break;
            case BUILDING_TYPE.CHUAN:
                this.m_cCollectItemChuan.m_lstMesh.Add(obj);
                this.m_cCollectItemChuan.m_lstCurveStart.Add(pos);
                this.m_cCollectItemChuan.m_lstCurveTop.Add(heigh);
                this.m_cCollectItemChuan.m_lstCurveBottom.Add(bottom);
                this.m_cCollectItemChuan.m_lstCurveStartTime.Add(GAME_TIME.TIME_FIXED() + fixTime);
                this.m_cCollectItemChuan.m_lstLineStartTime.Add(GAME_TIME.TIME_FIXED());
                this.m_cCollectItemChuan.m_lstState.Add(0);
                this.m_cCollectItemChuan.m_lstWaitTime.Add(GAME_TIME.TIME_FIXED());
                this.m_cCollectItemChuan.m_lstIsItem.Add(isItem);
                this.m_cCollectItemChuan.m_lstLineTop.Add(Vector3.one);

                break;
            case BUILDING_TYPE.TIAN:
                this.m_cCollectItemTian.m_lstMesh.Add(obj);
                this.m_cCollectItemTian.m_lstCurveStart.Add(pos);
                this.m_cCollectItemTian.m_lstCurveTop.Add(heigh);
                this.m_cCollectItemTian.m_lstCurveBottom.Add(bottom);
                this.m_cCollectItemTian.m_lstCurveStartTime.Add(GAME_TIME.TIME_FIXED() + fixTime);
                this.m_cCollectItemTian.m_lstLineStartTime.Add(GAME_TIME.TIME_FIXED());
                this.m_cCollectItemTian.m_lstState.Add(0);
                this.m_cCollectItemTian.m_lstWaitTime.Add(GAME_TIME.TIME_FIXED());
                this.m_cCollectItemTian.m_lstIsItem.Add(isItem);
                this.m_cCollectItemTian.m_lstLineTop.Add(Vector3.one);

                break;
            case BUILDING_TYPE.LIN:
                this.m_cCollectItemLin.m_lstMesh.Add(obj);
                this.m_cCollectItemLin.m_lstCurveStart.Add(pos);
                this.m_cCollectItemLin.m_lstCurveTop.Add(heigh);
                this.m_cCollectItemLin.m_lstCurveBottom.Add(bottom);
                this.m_cCollectItemLin.m_lstCurveStartTime.Add(GAME_TIME.TIME_FIXED() + fixTime);
                this.m_cCollectItemLin.m_lstLineStartTime.Add(GAME_TIME.TIME_FIXED());
                this.m_cCollectItemLin.m_lstState.Add(0);
                this.m_cCollectItemLin.m_lstWaitTime.Add(GAME_TIME.TIME_FIXED());
                this.m_cCollectItemLin.m_lstIsItem.Add(isItem);
                this.m_cCollectItemLin.m_lstLineTop.Add(Vector3.one);

                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 根据权重生成指定格式的float比例
    /// </summary>
    /// <param name="itemsWeight"></param>
    /// <returns></returns>
    private float[] ChangeToFloatWeight(List<int> itemsWeight)
    {
        List<float> re = new List<float>();
        //求出总数
        float sum = itemsWeight.Sum();

        for (int i = 0; i < itemsWeight.Count; i++)
        {
            re.Add(itemsWeight[i] / sum);
        }

        return re.ToArray();
    }

    /// <summary>
    /// 更新提示显示
    /// </summary>
    public void UpdateWarnShow()
    {
        if (GAME_SETTING.s_iWarnHouseEquip == 1)
        {
            m_cEquipWarn.enabled = true;
        }
        else
        {
            if (GAME_SETTING.s_bEquipLevelUp)
            {
                if (Role.role.GetItemProperty().CheckNewEquipWarn(GAME_SETTING.s_iEquipLevelAdd))
                {
                    m_cEquipWarn.enabled = true;
                    GAME_SETTING.s_iWarnHouseEquip = 1;
                    GAME_SETTING.SaveWarnHouseEquip();
                }
                else
                    m_cEquipWarn.enabled = false;
            }
            else
                m_cEquipWarn.enabled = false;
        }
        if (GAME_SETTING.s_iWarnHouseTiaohe == 1)
        {
            m_cItemWarn.enabled = true;
        }
        else
        {
            if (GAME_SETTING.s_bItemLevelUp)
            {
                if (Role.role.GetItemProperty().CheckNewItemWarn(GAME_SETTING.s_iItemLevelAdd))
                {
                    m_cItemWarn.enabled = true;
                    GAME_SETTING.s_iWarnHouseTiaohe = 1;
                    GAME_SETTING.SaveWarnHouseTiaohe();
                }
                else
                    m_cItemWarn.enabled = false;
            }
            else
                m_cItemWarn.enabled = false;
        }
        GUIBackFrameBottom backbottom = this.m_cGUIMgr.GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;
        if (m_cItemWarn.enabled || m_cEquipWarn.enabled)
        {
            if (backbottom.m_cTwonWarn != null)
                backbottom.m_cTwonWarn.enabled = true;
        }
        else
        {
            if (backbottom.m_cTwonWarn != null)
                backbottom.m_cTwonWarn.enabled = false;
        }
    }
}