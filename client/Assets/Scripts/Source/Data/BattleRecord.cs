using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//战绩类
//Author sunyi
//2014-02-28
public class BattleRecord
{
    public int m_iLoginTimesLink;//连续登入天数
    public int m_iGoldEarned;//累计获得金币
    public int m_iGoldCosted;//金币使用总金额
    public int m_iRoleSaledGold;//角色出售总金额
    public int m_iPropsSaledGold;//道具出售总金额
    public int m_iYuanqiEarned;//累计获得元气数
    public int m_iYuanqiCosted;//元气使用总数
    public int m_iFriendPointEarned;//累计持有友情点数
    public int m_iFriendPointCosted;//友情点数使用总数
    public int m_iHeroTujianOpened;//英雄图鉴开启数
    public int m_iPropsTjianOpened;//道具图鉴开启数
    public int m_iFireHeroTujianOpened;//火属性英雄图鉴开启数
    public int m_iWaterHeroTujianOpened;//水属性英雄图鉴开启数
    public int m_iWoodHeroTujianOpened;//树属性英雄图鉴开启数
    public int m_iThunderHeroTujianOpened;//雷属性英雄图鉴开启数
    public int m_iLightHeroTujianOpened;//光属性英雄图鉴开启数
    public int m_iDarkHeroTujianOpened;//暗属性英雄图鉴开启数
    public int m_iHeroGetNum;//英雄获得总次数
    public int m_iPropsGetNum;//道具获得总次数
    public int m_iGiftSendNum;//送礼物总次数
    public int m_iGiftReceiveNum;//收礼物总次数
    public int m_iHeroUpgradNum;//强化英雄次数
    public int m_iHeroPropsUpgradNum;//强化素材英雄使用次数
    public int m_iEvolutionNum;//进化次数
    public int m_iReconceliNum;//调合次数
    public int m_iReconceliSucaiNum;//调和素材使用次数
    public int m_iProduceEquipNum;//生成装备次数
    public int m_iTownSourceGatherNum;//村落资源采集次数
    public int m_iAngerSoulMaxNum;//怒气斗魂最高出现数（一场战斗）
    public int m_iCureSoulMaxNum;//治愈之魂最高出现数（一场战斗）
    public int m_iAngerSoulTotalNum;//怒气斗魂累计出现数
    public int m_iCureSoulTotalNum;//治愈之魂累计出现数
    public int m_iMaxHurtValue;//1回合战斗最大伤害
    public int m_iMaxSparkValue;//1回合战斗最大SPARK次数
    public int m_iTotalSparkValue;//累计SPARK次数
    public int m_iTotalHeroSkillTimes;//累计英雄技能次数
    public int m_iGateBattleCount;//关卡挑战次数
    public int m_iGateSuccessCount;//关卡成功次数
    public int m_iBattleWinCount;//战斗胜利次数（关卡内每次胜利）
    public int m_iBoxHeroFindCount;//宝箱怪出现次数
}

