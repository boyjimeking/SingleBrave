using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Gfx;
using Game.Base;
using Game.Resource;


//  BattleHeroGenerator.cs
//  Author: Lu Zexi
//  2013-11-29




/// <summary>
/// 战场英雄生成类
/// </summary>
public class BattleHeroGenerator
{

    /// <summary>
    /// 生成战斗数据
    /// </summary>
    /// <param name="go"></param>
    /// <param name="hero"></param>
    /// <returns></returns>
    public static BattleHero Generator(int index, bool bself, Hero hero, LeaderSkillTable selfLeaderSkill, LeaderSkillTable friendLeaderSkill, Item item)
    {
        if (hero == null)
        {
            return null;
        }
        HeroTable heroTable = HeroTableManager.GetInstance().GetHeroTable(hero.m_iTableID);
        HeroAttackTable attackTable = HeroAttackTableManager.GetInstance().GetAttackTable(hero.m_iTableID);
        BBSkillTable bbSkillTable = BBSkillTableManager.GetInstance().GetBBSkillTable(hero.m_iBBSkillTableID);

        //GameObject go = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_MODEL_PATH, heroTable.Modle)) as GameObject;
        //go.transform.parent = parent.transform;
        //go.transform.localPosition = startPos.transform.localPosition;
        //go.transform.localScale = startPos.transform.localScale;
        //BattleHero battleHero = new BattleHero(go , gui);
        //battleHero.GetGfxObject().Initialize();

        BattleHero battleHero = new BattleHero();

        //战斗数据
        battleHero.m_bSelf = bself;
        battleHero.m_iIndex = index;
        battleHero.m_iID = hero.m_iID;
        battleHero.m_iTableID = hero.m_iTableID;
        battleHero.m_iLevel = hero.m_iLevel;
        battleHero.m_iHp = hero.m_iHp;
        if (item != null)
        {
            battleHero.m_iHp = (int)(battleHero.m_iHp * (1f + item.m_fMaxHpInc));
        }
        battleHero.m_cMaxHP = new CPropertyValue(battleHero.m_iHp);
        battleHero.m_iDummyHP = battleHero.m_iHp;
        float attack = hero.m_iAttack;
        if (item != null)
        {
            attack = attack * (1f + item.m_fAttackInc);
        }
        battleHero.m_cAttack = new CPropertyValue(attack);
        float defence = hero.m_iDefence;
        if (item != null)
        {
            defence = defence * (1f + item.m_fDefenceInc);
        }
        battleHero.m_cDefence = new CPropertyValue(defence);
        float recover = hero.m_iRevert;
        if (item != null)
        {
            recover = recover * (1f + item.m_fRecoverInc);
        }
        battleHero.m_cRecover = new CPropertyValue(recover);
        battleHero.m_cCritical = new CPropertyValue(GAME_DEFINE.CRITICAL);
        battleHero.m_iBBSkillLevel = hero.m_iBBSkillLevel;
        battleHero.m_iAttackNum = 0;
        battleHero.m_iAttackMaxNum = 1;
        battleHero.m_iBBMaxHP = hero.m_iMaxBBHP;

        /////////队长技能影响
        if (selfLeaderSkill != null )
        {
            if (selfLeaderSkill.AttackProperty == hero.m_eNature)
            {
                battleHero.m_cAttack.AddRate(selfLeaderSkill.AttackRate);
            }
            battleHero.m_cAttack.AddRate(selfLeaderSkill.AllAttackRate);
        }
        if (friendLeaderSkill != null )
        {
            if (friendLeaderSkill.AttackProperty == hero.m_eNature)
            {
                battleHero.m_cAttack.AddRate(friendLeaderSkill.AttackRate);
            }
            battleHero.m_cAttack.AddRate(friendLeaderSkill.AllAttackRate);
        }

        //抗性
        battleHero.m_cOpposeDu = new CPropertyValue(hero.m_fOpposeDu);
        battleHero.m_cOpposeXuruo = new CPropertyValue(hero.m_fOpposeXuruo);
        battleHero.m_cOpposePojia = new CPropertyValue(hero.m_fOpposePojia);
        battleHero.m_cOpposePoren = new CPropertyValue(hero.m_fOpposePoren);
        battleHero.m_cOpposeFengyin = new CPropertyValue(hero.m_fOpposeFenying);
        battleHero.m_cOpposeMa = new CPropertyValue(hero.m_fOpposeMa);

        //状态概率
        battleHero.m_cDuRate = new CPropertyValue(0);
        battleHero.m_iDuRound = 0;
        battleHero.m_cXuRuoRate = new CPropertyValue(0);
        battleHero.m_iXuRuoRound = 0;
        battleHero.m_cPojiaRate = new CPropertyValue(0);
        battleHero.m_iPojiaRound = 0;
        battleHero.m_cPorenRate = new CPropertyValue(0);
        battleHero.m_iPorenRound = 0;
        battleHero.m_cMaRate = new CPropertyValue(0);
        battleHero.m_iMaRound = 0;
        battleHero.m_cFengyinRate = new CPropertyValue(0);
        battleHero.m_iFengyinRound = 0;


        //战斗临时数据
        Array.Clear(battleHero.m_vecHitStartTime, 0, battleHero.m_vecHitStartTime.Length);
        Array.Clear(battleHero.m_vecHitEndTime, 0, battleHero.m_vecHitEndTime.Length);
        battleHero.m_lstBUF.Clear();
        battleHero.m_lstBUF_Round.Clear();
#if GAME_TEST
        battleHero.m_fBBHP = battleHero.m_iBBMaxHP;
#else
        battleHero.m_fBBHP = 0;
#endif
        battleHero.m_bDefence = false;
        battleHero.m_bDead = false;
        battleHero.m_bDropJudge = false;
        
        //装备
        battleHero.m_iEquipActionNum = 0;
        if (item != null)
            battleHero.m_iEquipEvent = item.m_iEvent;
        else
            battleHero.m_iEquipEvent = -1;
        battleHero.m_cMissDefenceRate = new CPropertyValue(0);
        battleHero.m_cDecHurtRate = new CPropertyValue(0);
        battleHero.m_cDecHurtRateArg = new CPropertyValue(0);
        battleHero.m_cAbsorbDamageRate = new CPropertyValue(0);
        battleHero.m_cAbsorbDamageRateArg = new CPropertyValue(0);
        battleHero.m_cReboundRate = new CPropertyValue(0);
        battleHero.m_cReboundRateArg = new CPropertyValue(0);
        battleHero.m_cRecoverHurtRate = new CPropertyValue(0);
        battleHero.m_cRecoverHurtRateArg = new CPropertyValue(0);
        battleHero.m_cRecoverHurtRateArgEx = new CPropertyValue(0);
        battleHero.m_cHurtIncBBHPRate = new CPropertyValue(0);
        battleHero.m_cHurtIncBBHP = new CPropertyValue(0);

        //场景数据
        battleHero.m_cUIStartPos = Vector3.zero;
        battleHero.m_cStartPos = Vector3.zero;
        battleHero.m_cAttackPos = Vector3.zero;

        //攻击固定数据
        battleHero.m_strEffectRes1 = attackTable.SpellEffect;
        battleHero.m_strEffectRes2 = attackTable.HitEffect;
        battleHero.m_iHitDistance = attackTable.HitDis;
        battleHero.m_iHitRange = attackTable.HitRange;
        battleHero.m_iHitNum = attackTable.HitNum;
        battleHero.m_lstHitTime = attackTable.LstHitTime;
        battleHero.m_lstHitRate = attackTable.LstHitRate;
        battleHero.m_cResAttackSpell = null;
        battleHero.m_cResAttackDaoGuang = null;
        battleHero.m_cResAttackHit = null;
        //battleHero.m_cResAttackSpell = (UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_EFFECT_PATH, attackTable.SpellEffect);
        //battleHero.m_cResAttackDaoGuang = (UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_EFFECT_PATH, attackTable.DaoGuangEffect);
        //battleHero.m_cResAttackHit = (UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_EFFECT_PATH, attackTable.HitEffect);
        ////battleHero.m_cResAttackSpell = (UnityEngine.Object)ResourcesManager.GetInstance().Load(attackTable.SpellEffect);
        ////battleHero.m_cResAttackDaoGuang = (UnityEngine.Object)ResourcesManager.GetInstance().Load(attackTable.DaoGuangEffect);
        ////battleHero.m_cResAttackHit = (UnityEngine.Object)ResourcesManager.GetInstance().Load(attackTable.HitEffect);


        //BB技能
        if (bbSkillTable != null)
        {
            battleHero.m_strBBEffectRes1 = bbSkillTable.SpellEffect;
            battleHero.m_strBBEffectRes2 = bbSkillTable.HitEffect;
            battleHero.m_iBBHitDistance = bbSkillTable.HitDis;
            battleHero.m_iBBHitRange = bbSkillTable.HitRange;
            battleHero.m_iBBHitNum = bbSkillTable.HitNum;
            battleHero.m_strSkillName = bbSkillTable.Name;
            battleHero.m_cBBAttack = new CPropertyValue(bbSkillTable.VecLevelAttack[hero.m_iBBSkillLevel - 1]);
            battleHero.m_eBBType = bbSkillTable.Type;
            battleHero.m_eBBTargetType = bbSkillTable.TargetType;
            battleHero.m_eBBMoveType = bbSkillTable.MoveType;
            battleHero.m_lstBBHitTime = bbSkillTable.LstHitTime;
            battleHero.m_lstBBHitRate = bbSkillTable.LstHitRate;
            battleHero.m_cResSklillSpell = null;
            battleHero.m_cResSkillDaoGuang = null;
            battleHero.m_cResSkill = null;
            battleHero.m_cResSkillHit = null;
            //battleHero.m_cResSklillSpell = (UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_EFFECT_PATH, bbSkillTable.SpellEffect);
            //battleHero.m_cResSkillDaoGuang = (UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_EFFECT_PATH, bbSkillTable.DaoGuangEffect);
            //battleHero.m_cResSkill = (UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_EFFECT_PATH, bbSkillTable.SkillEffect);
            //battleHero.m_cResSkillHit = (UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_EFFECT_PATH, bbSkillTable.HitEffect);
            ////battleHero.m_cResSklillSpell = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.SpellEffect);
            ////battleHero.m_cResSkillDaoGuang = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.DaoGuangEffect);
            ////battleHero.m_cResSkill = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.SkillEffect);
            ////battleHero.m_cResSkillHit = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.HitEffect);
        }

        //掉落概率加成
        battleHero.m_cFarmDropRateInc = new CPropertyValue(0);
        battleHero.m_cJinbiDropRateInc = new CPropertyValue(0);
        battleHero.m_cXinDropRateInc = new CPropertyValue(0);
        battleHero.m_cShuijingDropRateInc = new CPropertyValue(0);
        battleHero.m_cItemDropRateInc = new CPropertyValue(0);

        //掉落率
        battleHero.m_fCatchRate = 0;
        battleHero.m_iCatchID = 0;
        battleHero.m_fFarmRate = 0;
        battleHero.m_iFarmDropNum = 0;
        battleHero.m_iFarmDropNumMax = 0;
        battleHero.m_fGoldRate = 0;
        battleHero.m_iGoldDropNum = 0;
        battleHero.m_iGoldDropNumMax = 0;
        battleHero.m_cHeartDropRate = new CPropertyValue(0);
        battleHero.m_iHeartDropNum = 0;
        battleHero.m_cShuijingDropRate = new CPropertyValue(0);
        battleHero.m_iShuijingDropMinNum = 0;
        battleHero.m_iShuijingDropNum = 0;
        battleHero.m_fDropItemRate = 0;

        //数据表固定数据
        battleHero.m_strName = heroTable.Name;
        battleHero.m_eNature = (Nature)heroTable.Property;
        battleHero.m_strModel = heroTable.Modle;
        battleHero.m_strAvatorM = heroTable.AvatorMRes;
        battleHero.m_strAvatorL = heroTable.AvatorLRes;
        battleHero.m_strAvatorA = heroTable.AvatorARes;
        battleHero.m_strSEHit1 = heroTable.SEHit1;
        battleHero.m_strSEHit2 = heroTable.SEHit2;
        battleHero.m_iBBSkillTableID = heroTable.BBSkill;
        battleHero.m_fMoveSpeed = heroTable.Speed;
        battleHero.m_eMoveType = (MoveType)heroTable.MoveType;

        //统计
        battleHero.m_iTotalDamage = 0;
        battleHero.m_fDeadTime = 0;
        battleHero.m_iTotalSuperDamage = 0;
        battleHero.m_iTotalSkillKillNum = 0;

        return battleHero;
    }

    /// <summary>
    /// 生成怪物英雄
    /// </summary>
    /// <param name="go"></param>
    /// <param name="startPos"></param>
    /// <param name="attackPos"></param>
    /// <param name="monster"></param>
    /// <param name="gui"></param>
    /// <returns></returns>
    public static BattleHero Generator(int index , IGUIBattle gui , MonsterTable monster, FAV_TYPE favType)
    {
        if (monster == null)
        {
            return null;
        }
        HeroTable heroTable = HeroTableManager.GetInstance().GetHeroTable(monster.HeroTableID);
        HeroAttackTable attackTable = HeroAttackTableManager.GetInstance().GetAttackTable(monster.HeroTableID);
        BBSkillTable bbSkillTable = BBSkillTableManager.GetInstance().GetBBSkillTable(monster.BBSkillID);
        if (bbSkillTable == null) bbSkillTable = BBSkillTableManager.GetInstance().GetBBSkillTable(heroTable.BBSkill);

        //GameObject go = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_MODEL_PATH, heroTable.Modle)) as GameObject;
        //go.transform.parent = parent.transform;
        //go.transform.localPosition = startPos.transform.localPosition;
        //go.transform.localScale = startPos.transform.localScale;
        //BattleHero battleHero = new BattleHero(go, gui);
        //battleHero.GetGfxObject().Initialize();

        BattleHero battleHero = new BattleHero();
        AITable aiTable = AITableManager.GetInstance().GetAI(monster.AIType);
        battleHero.m_cAITable = aiTable;
        ProAIControl ai = new ProAIControl(battleHero, gui, battleHero.m_cAITable);
        battleHero.SetAI(ai);

        //战斗数据
        battleHero.m_bSelf = false;
        battleHero.m_iIndex = index;
        battleHero.m_iID = 0;
        battleHero.m_iTableID = heroTable.ID;
        battleHero.m_iLevel = 0;
        battleHero.m_iHp = monster.HP;
        battleHero.m_cMaxHP = new CPropertyValue(monster.HP);
        battleHero.m_iDummyHP = battleHero.m_iHp;
        battleHero.m_cAttack = new CPropertyValue(monster.Attack);
        battleHero.m_cDefence = new CPropertyValue(monster.Defence);
        battleHero.m_cRecover = new CPropertyValue(0);
        battleHero.m_cCritical = new CPropertyValue(GAME_DEFINE.CRITICAL);
        battleHero.m_iBBSkillLevel = monster.BBLevel;
        battleHero.m_iAttackMaxNum = 0;
        battleHero.m_iAttackNum = 0;
        battleHero.m_iBBMaxHP = 0;

        //抗性
        battleHero.m_cOpposeDu = new CPropertyValue(monster.OpposeDu);
        battleHero.m_cOpposeXuruo = new CPropertyValue(monster.OpposeXuruo);
        battleHero.m_cOpposePojia = new CPropertyValue(monster.OpposePojia);
        battleHero.m_cOpposePoren = new CPropertyValue(monster.OpposePoren);
        battleHero.m_cOpposeFengyin = new CPropertyValue(monster.OpposeFenying);
        battleHero.m_cOpposeMa = new CPropertyValue(monster.OpposeMa);

        //状态概率
        battleHero.m_cDuRate = new CPropertyValue(0);
        battleHero.m_iDuRound = 0;
        battleHero.m_cXuRuoRate = new CPropertyValue(0);
        battleHero.m_iXuRuoRound = 0;
        battleHero.m_cPojiaRate = new CPropertyValue(0);
        battleHero.m_iPojiaRound = 0;
        battleHero.m_cPorenRate = new CPropertyValue(0);
        battleHero.m_iPorenRound = 0;
        battleHero.m_cMaRate = new CPropertyValue(0);
        battleHero.m_iMaRound = 0;
        battleHero.m_cFengyinRate = new CPropertyValue(0);
        battleHero.m_iFengyinRound = 0;
        switch ((BATTLE_BUF)monster.DEBUF)
        { 
            case BATTLE_BUF.DU:
                battleHero.m_cDuRate = new CPropertyValue(monster.DEBUFRate);
                battleHero.m_iDuRound = monster.DEBUFRound;
                break;
            case BATTLE_BUF.XURUO:
                battleHero.m_cXuRuoRate = new CPropertyValue(monster.DEBUFRate);
                battleHero.m_iXuRuoRound = monster.DEBUFRound;
                break;
            case BATTLE_BUF.POJIA:
                battleHero.m_cPojiaRate = new CPropertyValue(monster.DEBUFRate);
                battleHero.m_iPojiaRound = monster.DEBUFRound;
                break;
            case BATTLE_BUF.POREN:
                battleHero.m_cPorenRate = new CPropertyValue(monster.DEBUFRate);
                battleHero.m_iPorenRound = monster.DEBUFRound;
                break;
            case BATTLE_BUF.MA:
                battleHero.m_cMaRate = new CPropertyValue(monster.DEBUFRate);
                battleHero.m_iMaRound = monster.DEBUFRound;
                break;
            case BATTLE_BUF.FENGYIN:
                battleHero.m_cFengyinRate = new CPropertyValue(monster.DEBUFRate);
                battleHero.m_iFengyinRound = monster.DEBUFRound;
                break;
        }

        //战斗临时数据
        Array.Clear(battleHero.m_vecHitStartTime, 0, battleHero.m_vecHitStartTime.Length);
        Array.Clear(battleHero.m_vecHitEndTime, 0, battleHero.m_vecHitEndTime.Length);
        battleHero.m_lstBUF.Clear();
        battleHero.m_lstBUF_Round.Clear();
#if GAME_TEST
        battleHero.m_fBBHP = battleHero.m_iBBMaxHP;
#else
        battleHero.m_fBBHP = 0;
#endif
        battleHero.m_bDefence = false;
        battleHero.m_bDead = false;
        battleHero.m_bDropJudge = false;

        //装备
        battleHero.m_iEquipActionNum = 0;
        battleHero.m_iEquipEvent = -1;
        battleHero.m_cMissDefenceRate = new CPropertyValue(0);
        battleHero.m_cDecHurtRate = new CPropertyValue(0);
        battleHero.m_cDecHurtRateArg = new CPropertyValue(0);
        battleHero.m_cAbsorbDamageRate = new CPropertyValue(0);
        battleHero.m_cAbsorbDamageRateArg = new CPropertyValue(0);
        battleHero.m_cReboundRate = new CPropertyValue(0);
        battleHero.m_cReboundRateArg = new CPropertyValue(0);
        battleHero.m_cRecoverHurtRate = new CPropertyValue(0);
        battleHero.m_cRecoverHurtRateArg = new CPropertyValue(0);
        battleHero.m_cRecoverHurtRateArgEx = new CPropertyValue(0);
        battleHero.m_cHurtIncBBHPRate = new CPropertyValue(0);
        battleHero.m_cHurtIncBBHP = new CPropertyValue(0);

        //场景数据
        battleHero.m_cUIStartPos = Vector3.zero;
        battleHero.m_cStartPos = Vector3.zero;
        battleHero.m_cAttackPos = Vector3.zero;

        //攻击固定数据
        battleHero.m_strEffectRes1 = attackTable.SpellEffect;
        battleHero.m_strEffectRes2 = attackTable.HitEffect;
        battleHero.m_iHitDistance = attackTable.HitDis;
        battleHero.m_iHitRange = attackTable.HitRange;
        battleHero.m_iHitNum = attackTable.HitNum;
        battleHero.m_lstHitTime = attackTable.LstHitTime;
        battleHero.m_lstHitRate = attackTable.LstHitRate;
        battleHero.m_cResAttackSpell = null;
        battleHero.m_cResAttackDaoGuang = null;
        battleHero.m_cResAttackHit = null;
        //battleHero.m_cResAttackSpell = (UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_EFFECT_PATH, attackTable.SpellEffect);
        //battleHero.m_cResAttackDaoGuang = (UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_EFFECT_PATH, attackTable.DaoGuangEffect);
        //battleHero.m_cResAttackHit = (UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_EFFECT_PATH, attackTable.HitEffect);
        ////battleHero.m_cResAttackSpell = (UnityEngine.Object)ResourcesManager.GetInstance().Load(attackTable.SpellEffect);
        ////battleHero.m_cResAttackDaoGuang = (UnityEngine.Object)ResourcesManager.GetInstance().Load(attackTable.DaoGuangEffect);
        ////battleHero.m_cResAttackHit = (UnityEngine.Object)ResourcesManager.GetInstance().Load(attackTable.HitEffect);

        //BB技能
        if (bbSkillTable != null)
        {
            battleHero.m_strBBEffectRes1 = bbSkillTable.SpellEffect;
            battleHero.m_strBBEffectRes2 = bbSkillTable.HitEffect;
            battleHero.m_iBBHitDistance = bbSkillTable.HitDis;
            battleHero.m_iBBHitRange = bbSkillTable.HitRange;
            battleHero.m_iBBHitNum = bbSkillTable.HitNum;
            battleHero.m_cBBAttack = new CPropertyValue(bbSkillTable.VecLevelAttack[monster.BBLevel - 1]);
            battleHero.m_eBBType = bbSkillTable.Type;
            battleHero.m_eBBTargetType = bbSkillTable.TargetType;
            battleHero.m_eBBMoveType = bbSkillTable.MoveType;
            battleHero.m_lstBBHitTime = bbSkillTable.LstHitTime;
            battleHero.m_lstBBHitRate = bbSkillTable.LstHitRate;
            battleHero.m_cResSklillSpell = null;
            battleHero.m_cResSkillDaoGuang = null;
            battleHero.m_cResSkill = null;
            battleHero.m_cResSkillHit = null;
            //battleHero.m_cResSklillSpell = (UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_EFFECT_PATH, bbSkillTable.SpellEffect);
            //battleHero.m_cResSkillDaoGuang = (UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_EFFECT_PATH, bbSkillTable.DaoGuangEffect);
            //battleHero.m_cResSkill = (UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_EFFECT_PATH, bbSkillTable.SkillEffect);
            //battleHero.m_cResSkillHit = (UnityEngine.Object)ResourcesManager.GetInstance().Load(GAME_DEFINE.RESOURCE_EFFECT_PATH, bbSkillTable.HitEffect);
            ////battleHero.m_cResSklillSpell = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.SpellEffect);
            ////battleHero.m_cResSkillDaoGuang = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.DaoGuangEffect);
            ////battleHero.m_cResSkill = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.SkillEffect);
            ////battleHero.m_cResSkillHit = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.HitEffect);
        }

        //掉落概率加成
        battleHero.m_cFarmDropRateInc = new CPropertyValue(0);
        battleHero.m_cJinbiDropRateInc = new CPropertyValue(0);
        battleHero.m_cXinDropRateInc = new CPropertyValue(0);
        battleHero.m_cShuijingDropRateInc = new CPropertyValue(0);
        battleHero.m_cItemDropRateInc = new CPropertyValue(0);

        //掉落率
        battleHero.m_fCatchRate = monster.CatchRate;
        battleHero.m_iCatchID = monster.CatchID;
        battleHero.m_fFarmRate = monster.DropFarmRate;
        battleHero.m_iFarmDropNum = monster.DropFarm;
        battleHero.m_iFarmDropNumMax = monster.DropFarmMax;
        battleHero.m_fGoldRate = monster.DropGoldRate;
        battleHero.m_iGoldDropNum = monster.DropGold;
        battleHero.m_iGoldDropNumMax = monster.DropGoldMax;
        battleHero.m_cHeartDropRate = new CPropertyValue(GAME_DEFINE.GATEHeartRate);
        battleHero.m_iHeartDropNum = GAME_DEFINE.GATEHeartNum;
        battleHero.m_cShuijingDropRate = new CPropertyValue(GAME_DEFINE.GATEShuijingRate);
        battleHero.m_iShuijingDropMinNum = GAME_DEFINE.GATEShuijingMinNum;
        battleHero.m_iShuijingDropNum = GAME_DEFINE.GATEShuijingMaxNum;
        battleHero.m_fDropItemRate = monster.DropItemRate;
        if (favType == FAV_TYPE.ITEM_DROP_1_5)  //优惠奖励，掉落率增加1.5倍
            battleHero.m_fDropItemRate *= 1.5f;
        if (favType == FAV_TYPE.FARM_NUM_2) //优惠奖励，农场点增加2倍
        {
            battleHero.m_iFarmDropNum *= 2;
            battleHero.m_iFarmDropNumMax *= 2;
        }
        if (favType == FAV_TYPE.GOLD_NUM_2) //优惠奖励，金币数增加2倍
        {
            battleHero.m_iGoldDropNum *= 2;
            battleHero.m_iGoldDropNumMax *= 2;
        }
        if (favType == FAV_TYPE.CATCH_RATE_1_5) //优惠奖励，捕捉率1.5倍
        {
            battleHero.m_fCatchRate *= 1.5f;
        }

        //数据表固定数据
        battleHero.m_strName = heroTable.Name;
        battleHero.m_eNature = (Nature)heroTable.Property;
        battleHero.m_strModel = heroTable.Modle;
        battleHero.m_strAvatorM = heroTable.AvatorMRes;
        battleHero.m_strAvatorL = heroTable.AvatorLRes;
        battleHero.m_strAvatorA = heroTable.AvatorARes;
        battleHero.m_strSEHit1 = heroTable.SEHit1;
        battleHero.m_strSEHit2 = heroTable.SEHit2;
        battleHero.m_iBBSkillTableID = heroTable.BBSkill;
        if(monster.BBSkillID != 0 )
            battleHero.m_iBBSkillTableID = monster.BBSkillID;
        battleHero.m_fMoveSpeed = heroTable.Speed;
        battleHero.m_eMoveType = (MoveType)heroTable.MoveType;

        //统计
        battleHero.m_iTotalDamage = 0;
        battleHero.m_fDeadTime = 0;
        battleHero.m_iTotalSuperDamage = 0;
        battleHero.m_iTotalSkillKillNum = 0;

        return battleHero;
    }

    /// <summary>
    /// 生成渲染实体
    /// </summary>
    /// <param name="bHero"></param>
    public static void GeneratorGfx(BattleHero bHero, IGUIBattle gui, GameObject parent, GameObject uistartPos, GameObject startPos, GameObject attackPos)
    {
        if (bHero == null) return;

        GameObject go = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(bHero.m_strModel)) as GameObject;
        go.transform.parent = parent.transform;
        go.transform.localPosition = startPos.transform.localPosition;
        go.transform.localScale = startPos.transform.localScale;
        bHero.SetGfx(new GfxObject(go));
        CmdControl cmd = new CmdControl(bHero, gui,false);
        bHero.SetCmdControl(cmd);

        HeroAttackTable attackTable = HeroAttackTableManager.GetInstance().GetAttackTable(bHero.m_iTableID);
        BBSkillTable bbSkillTable = BBSkillTableManager.GetInstance().GetBBSkillTable(bHero.m_iBBSkillTableID);

        bHero.m_cResAttackSpell = (UnityEngine.Object)ResourcesManager.GetInstance().Load(attackTable.SpellEffect);
        bHero.m_cResAttackDaoGuang = (UnityEngine.Object)ResourcesManager.GetInstance().Load(attackTable.DaoGuangEffect);
        bHero.m_cResAttackHit = (UnityEngine.Object)ResourcesManager.GetInstance().Load(attackTable.HitEffect);

        if (bbSkillTable != null)
        {
            bHero.m_cResSklillSpell = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.SpellEffect);
            bHero.m_cResSkillDaoGuang = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.DaoGuangEffect);
            bHero.m_cResSkill = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.SkillEffect);
            bHero.m_cResSkillHit = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.HitEffect);
        }

        ProAIControl ai = new ProAIControl(bHero, gui, bHero.m_cAITable);
        bHero.SetAI(ai);

        bHero.m_cUIStartPos = uistartPos.transform.localPosition;
        bHero.m_cStartPos = startPos.transform.localPosition;
        bHero.m_cAttackPos = attackPos.transform.localPosition;
    }

    /// <summary>
    /// 异步生成英雄
    /// </summary>
    /// <param name="bHer"></param>
    public static void GeneratorHeroAysnc(BattleHero bHero)
    {
        if (bHero == null) return;

        HeroAttackTable attackTable = HeroAttackTableManager.GetInstance().GetAttackTable(bHero.m_iTableID);
        BBSkillTable bbSkillTable = BBSkillTableManager.GetInstance().GetBBSkillTable(bHero.m_iBBSkillTableID);

        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_MODEL_PATH, GAME_DEFINE.RES_VERSION, bHero.m_strModel);

        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, attackTable.SpellEffect);
        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, attackTable.DaoGuangEffect);
        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, attackTable.HitEffect);

        if (bbSkillTable != null)
        {
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, bbSkillTable.SpellEffect);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, bbSkillTable.DaoGuangEffect);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, bbSkillTable.SkillEffect);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, bbSkillTable.HitEffect);
        }

        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_TEX_PATH , GAME_DEFINE.RES_VERSION , bHero.m_strAvatorA);
    }

    /// <summary>
    /// 异步生成英雄
    /// </summary>
    /// <param name="bHer"></param>
    public static void GeneratorTargetHeroAysnc(BattleHero bHero)
    {
        if (bHero == null) return;

        HeroAttackTable attackTable = HeroAttackTableManager.GetInstance().GetAttackTable(bHero.m_iTableID);
        BBSkillTable bbSkillTable = BBSkillTableManager.GetInstance().GetBBSkillTable(bHero.m_iBBSkillTableID);

        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_MODEL_PATH, GAME_DEFINE.RES_VERSION, bHero.m_strModel);

        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, attackTable.SpellEffect);
        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, attackTable.DaoGuangEffect);
        ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, attackTable.HitEffect);

        if (bbSkillTable != null)
        {
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, bbSkillTable.SpellEffect);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, bbSkillTable.DaoGuangEffect);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, bbSkillTable.SkillEffect);
            ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_EFFECT_PATH, GAME_DEFINE.RES_VERSION, bbSkillTable.HitEffect);
        }

        //ResourcesManager.GetInstance().LoadResource(GAME_DEFINE.RESOURCE_TEX_PATH, bHero.m_strAvatorA);
    }

    /// <summary>
    /// 生成异步物体
    /// </summary>
    /// <param name="bHero"></param>
    /// <param name="parent"></param>
    /// <param name="uistartPos"></param>
    /// <param name="startPos"></param>
    /// <param name="attackPos"></param>
    public static void GeneratorHeroGfxAysnc(BattleHero bHero, IGUIBattle gui , GameObject parent, GameObject uistartPos, GameObject startPos, GameObject attackPos , bool canDrop )
    {
        if (bHero == null) return;

        Debug.Log(bHero.m_strModel);
        GameObject go = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(bHero.m_strModel)) as GameObject;
        go.transform.parent = parent.transform;
        go.transform.localPosition = startPos.transform.localPosition;
        go.transform.localScale = startPos.transform.localScale;
        bHero.SetGfx(new GfxObject(go));
        CmdControl cmd = new CmdControl(bHero, gui , canDrop);
        bHero.SetCmdControl(cmd);

        HeroAttackTable attackTable = HeroAttackTableManager.GetInstance().GetAttackTable(bHero.m_iTableID);
        BBSkillTable bbSkillTable = BBSkillTableManager.GetInstance().GetBBSkillTable(bHero.m_iBBSkillTableID);

        bHero.m_cResAttackSpell = (UnityEngine.Object)ResourcesManager.GetInstance().Load(attackTable.SpellEffect);
        bHero.m_cResAttackDaoGuang = (UnityEngine.Object)ResourcesManager.GetInstance().Load(attackTable.DaoGuangEffect);
        bHero.m_cResAttackHit = (UnityEngine.Object)ResourcesManager.GetInstance().Load(attackTable.HitEffect);

        if (bbSkillTable != null)
        {
            bHero.m_cResSklillSpell = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.SpellEffect);
            bHero.m_cResSkillDaoGuang = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.DaoGuangEffect);
            bHero.m_cResSkill = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.SkillEffect);
            bHero.m_cResSkillHit = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.HitEffect);
        }

        bHero.m_cUIStartPos = uistartPos.transform.localPosition;
        bHero.m_cStartPos = startPos.transform.localPosition;
        bHero.m_cAttackPos = attackPos.transform.localPosition;

        //音效
        bHero.m_cSEHit1 = Resources.Load(bHero.m_strSEHit1) as AudioClip;
        bHero.m_cSEHit2 = Resources.Load(bHero.m_strSEHit2) as AudioClip;

        //头像
        bHero.m_cResAvator = (Texture)ResourcesManager.GetInstance().Load(bHero.m_strAvatorA);
    }

    /// <summary>
    /// 生成异步物体
    /// </summary>
    /// <param name="bHero"></param>
    /// <param name="parent"></param>
    /// <param name="uistartPos"></param>
    /// <param name="startPos"></param>
    /// <param name="attackPos"></param>
    public static void GeneratorMonsterHeroGfxAysnc(BattleHero bHero, IGUIBattle gui, GameObject parent, GameObject uistartPos, GameObject startPos, GameObject attackPos, bool canDrop)
    {
        if (bHero == null) return;

        Debug.Log(bHero.m_strModel);
        GameObject go = GameObject.Instantiate((UnityEngine.Object)ResourcesManager.GetInstance().Load(bHero.m_strModel)) as GameObject;
        go.transform.parent = parent.transform;
        go.transform.localPosition = startPos.transform.localPosition;
        go.transform.localScale = startPos.transform.localScale;
        bHero.SetGfx(new GfxObject(go));
        CmdControl cmd = new CmdControl(bHero, gui, canDrop);
        bHero.SetCmdControl(cmd);

        HeroAttackTable attackTable = HeroAttackTableManager.GetInstance().GetAttackTable(bHero.m_iTableID);
        BBSkillTable bbSkillTable = BBSkillTableManager.GetInstance().GetBBSkillTable(bHero.m_iBBSkillTableID);

        bHero.m_cResAttackSpell = (UnityEngine.Object)ResourcesManager.GetInstance().Load(attackTable.SpellEffect);
        bHero.m_cResAttackDaoGuang = (UnityEngine.Object)ResourcesManager.GetInstance().Load(attackTable.DaoGuangEffect);
        bHero.m_cResAttackHit = (UnityEngine.Object)ResourcesManager.GetInstance().Load(attackTable.HitEffect);

        if (bbSkillTable != null)
        {
            bHero.m_cResSklillSpell = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.SpellEffect);
            bHero.m_cResSkillDaoGuang = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.DaoGuangEffect);
            bHero.m_cResSkill = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.SkillEffect);
            bHero.m_cResSkillHit = (UnityEngine.Object)ResourcesManager.GetInstance().Load(bbSkillTable.HitEffect);
        }

        bHero.m_cUIStartPos = uistartPos.transform.localPosition;
        bHero.m_cStartPos = startPos.transform.localPosition;
        bHero.m_cAttackPos = attackPos.transform.localPosition;

        //音效
        bHero.m_cSEHit1 = Resources.Load(bHero.m_strSEHit1) as AudioClip;
        bHero.m_cSEHit2 = Resources.Load(bHero.m_strSEHit2) as AudioClip;

        //头像
        //bHero.m_cResAvator = (Texture)ResourcesManager.GetInstance().Load(bHero.m_strAvatorA);
    }

}
