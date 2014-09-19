using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//  IGUIBattle.cs
//  Author: Lu Zexi
//  2014-02-07



/// <summary>
/// 战斗GUI接口
/// </summary>
public abstract class IGUIBattle : GUIBase
{
    public UnityEngine.Object m_cResDebuffDu;    //毒DEBUFF
    public UnityEngine.Object m_cResDebuffXuruo;    //虚弱DEBUFF
    public UnityEngine.Object m_cResDebuffPojia;    //破甲DEBUFF
    public UnityEngine.Object m_cResDebuffPoren;    //破刃DEBUFF
    public UnityEngine.Object m_cResDebuffFengyin;    //封印DEBUFF
    public UnityEngine.Object m_cResDebuffMa;    //麻痹DEBUFF

    public List<int> m_lstDropItem; //掉率物品
    public List<float> m_lstDropItemRate;   //掉落物品概率

    public IGUIBattle(GUIManager guiMgr, int id, UILAYER layer)
        : base(guiMgr, id, layer)
    { 
        //
    }

    /// <summary>
    /// 设置战斗状态
    /// </summary>
    /// <param name="state"></param>
    public virtual void SetBattleState( int state )
    { 
        //
    }

    /// <summary>
    /// 获取回合数
    /// </summary>
    /// <returns></returns>
    public abstract int GetRoundNum();

    /// <summary>
    /// 获取己方自动选择英雄
    /// </summary>
    /// <returns></returns>
    public abstract BattleHero GetSelfAuto();

    /// <summary>
    /// 获取目标自动选择英雄
    /// </summary>
    /// <returns></returns>
    public virtual BattleHero GetTargetAuto()
    {
        return null;
    }

    /// <summary>
    /// 获取己方HP最小英雄
    /// </summary>
    /// <returns></returns>
    public abstract BattleHero GetMinHPSelf();

    /// <summary>
    /// 获取最小目标HP英雄
    /// </summary>
    /// <returns></returns>
    public virtual BattleHero GetMinHPTarget()
    {
        return null;
    }

    /// <summary>
    /// 获取己方HP最大英雄
    /// </summary>
    /// <returns></returns>
    public abstract BattleHero GetMaxHPSelf();

    /// <summary>
    /// 获取目标最大HP英雄
    /// </summary>
    /// <returns></returns>
    public virtual BattleHero GetMaxHPTarget()
    {
        return null;
    }

    /// <summary>
    /// 获取自身列表
    /// </summary>
    /// <returns></returns>
    public abstract BattleHero[] GetVecSelf();

    /// <summary>
    /// 获取敌方列表
    /// </summary>
    /// <returns></returns>
    public abstract BattleHero[] GetVecEnemy();

    /// <summary>
    /// 获取BB释放点
    /// </summary>
    /// <returns></returns>
    public abstract GameObject GetBattleBBPos();

    /// <summary>
    /// 隐藏目标BUFF
    /// </summary>
    /// <param name="index"></param>
    public abstract void HidenTargetBUF(int index);

    /// <summary>
    /// 设置角色攻击状态
    /// </summary>
    /// <param name="hero"></param>
    public abstract void SetUIHeroAttackNum(BattleHero hero);


    /// <summary>
    /// 设置目标英雄数据条
    /// </summary>
    /// <param name="hero"></param>
    public abstract void SetUITargetData(BattleHero hero);

    /// <summary>
    /// 设置英雄HP
    /// </summary>
    /// <param name="hero"></param>
    public abstract void SetUIHeroHP(BattleHero hero);

    /// <summary>
    /// 设置英雄BBHP条
    /// </summary>
    /// <param name="hero"></param>
    public abstract void SetUIHeroBBHP(BattleHero hero);

    /// <summary>
    /// 生成回复数字
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="num"></param>
    public abstract void GeneratorRecoverNum(Vector3 pos, int num , BattleHero target );

    /// <summary>
    /// 生成伤害值
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="num"></param>
    public abstract void GeneratorHurtNum(Vector3 pos, int num , BattleHero target );

    /// <summary>
    /// 克制时的伤害数字
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="num"></param>
    public abstract void GeneratorHurtBaneNum(Vector3 pos, int num , BattleHero target );

    /// <summary>
    /// 被克制时的伤害数字
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="num"></param>
    public abstract void GeneratorHurtBeBaneNum(Vector3 pos, int num , BattleHero target );

    /// <summary>
    /// 获取己方领导技能
    /// </summary>
    /// <returns></returns>
    public abstract LeaderSkillTable GetSelfLeaderSkill();

    /// <summary>
    /// 获取敌方队长技能
    /// </summary>
    /// <returns></returns>
    public abstract LeaderSkillTable GetTargetLeaderSkill();

    /// <summary>
    /// 获取队友领导技能
    /// </summary>
    /// <returns></returns>
    public abstract LeaderSkillTable GetFriendLeaderSkill();

    /// <summary>
    /// 生成Spark特效
    /// </summary>
    /// <param name="pos"></param>
    public abstract void GeneratorSpark(Vector3 pos , BattleHero target );

    /// <summary>
    /// 生成暴击特效
    /// </summary>
    /// <param name="pos"></param>
    public abstract void GeneratorCritical(Vector3 pos);

    /// <summary>
    /// 生成心
    /// </summary>
    /// <param name="pos"></param>
    public abstract void GeneratorXin(Vector3 pos);

    /// <summary>
    /// 生成水晶
    /// </summary>
    public abstract void GeneratorShuijing(Vector3 pos);

    /// <summary>
    /// 生成金币
    /// </summary>
    public abstract void GeneratorJinbi(Vector3 pos, int val);

    /// <summary>
    /// 生成农场点
    /// </summary>
    public abstract void GeneratorFarm(Vector3 pos, int val);

    /// <summary>
    /// 生成物品
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="itemTableID"></param>
    public abstract void GeneratorItem(Vector3 pos, int itemTableID);

    /// <summary>
    /// 生成技能展示
    /// </summary>
    /// <param name="hero"></param>
    public abstract void GeneratorSkillShow(BattleHero hero);
}
