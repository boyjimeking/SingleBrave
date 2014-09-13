using System;
using System.Collections.Generic;
using System.Collections;
using Game.Base;
using Game.Gfx;
using Game.Resource;
using UnityEngine;

//  BATTLE_UTIL.cs
//  Author: Lu Zexi
//  2013-12-04



/// <summary>
/// 战斗工具
/// </summary>
public class BATTLE_FUNCTION
{
    /// <summary>
    /// 装备action 
    /// </summary>
    /// <param name="timescene"></param>
    /// <param name="hero"></param>
    public static void EQUIP_ACTION(BATTLE_TIME_SCENE timescene, BattleHero hero)
    {
        if (hero != null && !hero.m_bDead && hero.m_iHp > 0 )
        {
            BattleHeroEquipActionInput input = new BattleHeroEquipActionInput();
            input.SetBattleHero(hero);
            input.SetTimeScene(timescene);
            ACTION_ERROR_CODE code = EventActionManager.GetInstance().ExcuteReq(hero.m_iEquipEvent, input);

            if (code == ACTION_ERROR_CODE.NONE)
            {
                hero.m_iEquipActionNum++;
                EventActionManager.GetInstance().Excute(hero.m_iEquipEvent, input);
            }
            else if (code == ACTION_ERROR_CODE.ERROR)
            {
                hero.m_iEquipActionNum = 0;
                EventActionManager.GetInstance().RollBack(hero.m_iEquipEvent, input);
            }
        }
    }

    /// <summary>
    /// 展示BUF图
    /// </summary>
    /// <param name="hero"></param>
    /// <param name="sp"></param>
    /// <param name="index"></param>
    public static void BUF_SPRITE(BattleHero hero, UISprite sp, int index)
    {
        BATTLE_BUF buf = hero.m_lstBUF[index % hero.m_lstBUF.Count];
        switch (buf)
        { 
            case BATTLE_BUF.DU:
                sp.spriteName = "poisoning";
                break;
            case BATTLE_BUF.XURUO:
                sp.spriteName = "ill";
                break;
            case BATTLE_BUF.POJIA:
                sp.spriteName = "weak";
                break;
            case BATTLE_BUF.POREN:
                sp.spriteName = "hurt";
                break;
            case BATTLE_BUF.MA:
                sp.spriteName = "paralysis";
                break;
            case BATTLE_BUF.FENGYIN:
                sp.spriteName = "curse";
                break;
            case BATTLE_BUF.ATTACK_UP:
                sp.spriteName = "attack_up";
                break;
            case BATTLE_BUF.DEFENCE_UP:
                sp.spriteName = "defense_up";
                break;
            case BATTLE_BUF.CRITICAL_UP:
                sp.spriteName = "baoji_up";
                break;
            case BATTLE_BUF.RECOVER_UP:
                sp.spriteName = "recover_up";
                break;
            case BATTLE_BUF.SHUIJING_RATE_UP:
                sp.spriteName = "nuqi_chuxian_up";
                break;
            case BATTLE_BUF.DEAD_NOT:
                sp.spriteName = "zhisi_yiji";
                break;
            case BATTLE_BUF.HEART_RATE_UP:
                sp.spriteName = "zhiyu_chuxian_up";
                break;
            case BATTLE_BUF.HP_RECOVER:
                sp.spriteName = "HP_REcover";
                break;
            default:
                sp.spriteName = "";
                break;
        }
    }

    /// <summary>
    /// 更新战斗英雄BUF
    /// </summary>
    public static void BUF_UPDATE(BattleHero hero, IGUIBattle gui)
    {
        for (int i = 0; i < hero.m_lstBUF.Count; i++)
        {
            if (hero.m_lstBUF_Round[i] % 2 == 1)
            {
                switch (hero.m_lstBUF[i])
                {
                    case BATTLE_BUF.DU: //毒更新
                        hero.DecHP((int)(hero.m_cMaxHP.GetFinalData() * GAME_DEFINE.DEBUFFDuRate));
                        SoundManager.GetInstance().PlaySound(SOUND_DEFINE.SE_BATTLE_DEC_HP);
                        gui.GeneratorHurtNum(hero.m_cUIStartPos, (int)(hero.m_cMaxHP.GetFinalData() * GAME_DEFINE.DEBUFFDuRate) , hero);
                        gui.SetUITargetData(hero);
                        gui.SetUIHeroHP(hero);
                        break;
                    case BATTLE_BUF.HP_RECOVER: //HP持续恢复更新
                        float recoverHp = hero.m_lstBUF_Arg[i].GetFinalData() * hero.m_cRecover.GetFinalData();
                        hero.AddHP((int)recoverHp);
                        gui.GeneratorRecoverNum(hero.m_cUIStartPos, (int)recoverHp , hero);
                        gui.SetUITargetData(hero);
                        gui.SetUIHeroHP(hero);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 技能回复
    /// </summary>
    /// <param name="self"></param>
    /// <param name="target"></param>
    /// <param name="gui"></param>
    public static void SKILL_RECOVER(BattleHero self, BattleHero target, float rate, IGUIBattle gui)
    {
        float recover = self.m_cBBAttack.GetFinalData() * rate * self.m_cRecover.GetFinalData() + GAME_DEFINE.BBRecoverRate * target.m_cRecover.GetFinalData();

        //Debug.Log( "" + self.m_cBBAttack.GetFinalData() + " -- " + self.m_cRecover.GetFinalData() + " -- " + GAME_DEFINE.BBRecoverRate + " -- " + target.m_cRecover.GetFinalData() );
        //Debug.Log("" + recover);

        recover = Mathf.CeilToInt(recover);

        SoundManager.GetInstance().PlaySound(SOUND_DEFINE.SE_BATTLE_ADD_HP);
        target.AddHP((int)recover);

        gui.GeneratorRecoverNum(target.m_cUIStartPos, (int)recover , target);

        //判定BB技能BUF
        BBSkillTable bbSkillTable = BBSkillTableManager.GetInstance().GetBBSkillTable(self.m_iBBSkillTableID);
        for (int i = 0; i < bbSkillTable.VecBUF.Length; i++)
        {
            if (bbSkillTable.VecBUF[i] > 0)
            {
                if (GAME_FUNCTION.RANDOM_ONE() < bbSkillTable.VecBUFRate[i])
                {
                    //如果BUFF参数大于0，则使用BUFF参数，否则使用技能参数
                    float arg = self.m_cBBAttack.GetFinalData();
                    if (bbSkillTable.VecBUFFArg[i] > 0)
                        arg = bbSkillTable.VecBUFFArg[i];
                    target.BUFAdd((BATTLE_BUF)bbSkillTable.VecBUF[i], bbSkillTable.VecBUFRound[i], arg);
                }
            }
        }

        gui.SetUIHeroHP(target);
    }

    /// <summary>
    /// 技能BUFF
    /// </summary>
    /// <param name="self"></param>
    /// <param name="target"></param>
    /// <param name="gui"></param>
    public static void SKILL_BUFF(BattleHero self, BattleHero target, IGUIBattle gui)
    {
        //判定BB技能BUF
        BBSkillTable bbSkillTable = BBSkillTableManager.GetInstance().GetBBSkillTable(self.m_iBBSkillTableID);
        for (int i = 0; i < bbSkillTable.VecBUF.Length; i++)
        {
            if (bbSkillTable.VecBUF[i] > 0)
            {
                if (GAME_FUNCTION.RANDOM_ONE() < bbSkillTable.VecBUFRate[i])
                {
                    //如果BUFF参数大于0，则使用BUFF参数，否则使用技能参数
                    float arg = self.m_cBBAttack.GetFinalData();
                    if (bbSkillTable.VecBUFFArg[i] > 0)
                        arg = bbSkillTable.VecBUFFArg[i];
                    target.BUFAdd((BATTLE_BUF)bbSkillTable.VecBUF[i], bbSkillTable.VecBUFRound[i], arg);
                }
            }
        }
    }



    /// <summary>
    /// 单体技能攻击伤害
    /// </summary>
    /// <param name="self"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static BATTLE_BUF SKILL_HURT(BattleHero self, BattleHero target, float rate, float randomNum, IGUIBattle gui, ref bool spark, ref bool critical,bool canDrop)
    {
        // attack - defence/3 = basehurt
        // sparkHurt = basehurt*rate*1.5f   //分段判定
        // criticalHurt = basehurt*rate*1.5f //全局判定
        // 属性相克 hurt =  basehurt*rate*2f  //全局判定
        // 属性被相克 hurt =  basehurt*rate*0.5f  //全局判定 排除 光和暗
        // 防御状态 hurt = basehurt*rate*0.25f   //全局判定

        SoundManager.GetInstance().PlaySound(self.m_cSEHit1);
        SoundManager.GetInstance().PlaySound(self.m_cSEHit2);

        BATTLE_BUF buf = BATTLE_BUF.NONE;

        LeaderSkillTable selfLeaderSkill = null ;
        if (self.m_bSelf)
            selfLeaderSkill = gui.GetSelfLeaderSkill();
        else
            selfLeaderSkill = gui.GetTargetLeaderSkill();

        LeaderSkillTable friendLeaderSkill = null;
        if (self.m_bSelf)
            friendLeaderSkill = gui.GetFriendLeaderSkill();

        //基础伤害
        float defence = target.m_cDefence.GetFinalData();
        //无视防御
        if (randomNum <= target.m_cMissDefenceRate.GetFinalData())
        {
            defence = 0;
        }
        float hurt = (self.m_cBBAttack.GetFinalData() * self.m_cAttack.GetFinalData() - defence / 3f) * rate;
        if (hurt < 0)
            hurt = 1;
        float nowTime = GAME_TIME.TIME_FIXED();

        BBSkillTable skillTable = BBSkillTableManager.GetInstance().GetBBSkillTable(self.m_iBBSkillTableID);

        float addXinRate = self.m_cXinDropRateInc.GetFinalData();
        float addShuijingRate = self.m_cShuijingDropRateInc.GetFinalData();
        float addJinbiRate = self.m_cJinbiDropRateInc.GetFinalData();
        float addItemRate = self.m_cItemDropRateInc.GetFinalData();
        float addFarmRate = self.m_cFarmDropRateInc.GetFinalData();

        if (skillTable != null)
        {
            addXinRate += skillTable.XinDropRate;
            addShuijingRate += skillTable.ShuijingDropRate;
        }

        //Spark伤害
        for (int i = 0; i < target.m_vecHitEndTime.Length; i++)
        {
            if ( i!=self.m_iIndex && nowTime >= target.m_vecHitStartTime[i] && nowTime <= target.m_vecHitEndTime[i])
            {
                if (!spark)
                {
                    spark = true;
                    gui.GeneratorSpark(target.m_cStartPos , target);
                }

                float sp_rate = 0;
                if (selfLeaderSkill != null)
                {
                    addJinbiRate += selfLeaderSkill.SparkGoldRate;
                    addShuijingRate += selfLeaderSkill.SparkShuijingRate;
                    addXinRate += selfLeaderSkill.SparkHeartRate;
                    addFarmRate += selfLeaderSkill.SparkFarmRate;
                    sp_rate += selfLeaderSkill.SparkHurtRate;
                }

                if (friendLeaderSkill != null)
                {
                    addJinbiRate += friendLeaderSkill.SparkGoldRate;
                    addShuijingRate += friendLeaderSkill.SparkShuijingRate;
                    addXinRate += friendLeaderSkill.SparkHeartRate;
                    addFarmRate += friendLeaderSkill.SparkFarmRate;
                    sp_rate += friendLeaderSkill.SparkHurtRate;
                }

                hurt = hurt * (BATTLE_DEFINE.SPARK_RATE + sp_rate);
                break;
            }
        }
        //暴击伤害
        if (randomNum < self.m_cCritical.GetFinalData())
        {
            if (!critical)
            {
                critical = true;
                gui.GeneratorCritical(target.m_cStartPos);
            }
            hurt *= BATTLE_DEFINE.CRITICAL_RATE;
        }

        int isBane = 0;
        if ((self.m_eNature == Nature.Fire && target.m_eNature == Nature.Wood) || (self.m_eNature == Nature.Wood && target.m_eNature == Nature.Thunder)
            || (self.m_eNature == Nature.Thunder && target.m_eNature == Nature.Water) || (self.m_eNature == Nature.Water && target.m_eNature == Nature.Fire)
            || (self.m_eNature == Nature.Bright && target.m_eNature == Nature.Dark) || (self.m_eNature == Nature.Dark && target.m_eNature == Nature.Bright)
            )
        {
            isBane = 1;
            hurt = hurt * BATTLE_DEFINE.NATURE_RATE;
        }

        if ((target.m_eNature == Nature.Fire && self.m_eNature == Nature.Wood) || (target.m_eNature == Nature.Wood && self.m_eNature == Nature.Thunder)
            || (target.m_eNature == Nature.Thunder && self.m_eNature == Nature.Water) || (target.m_eNature == Nature.Water && self.m_eNature == Nature.Fire)
            )
        {
            isBane = 2;
            hurt = hurt * BATTLE_DEFINE.BENATURE_RATE;
        }

        if (target.m_bDefence)
        {
            hurt = hurt * BATTLE_DEFINE.DEFENCE_RATE;
        }

        hurt = Mathf.CeilToInt(hurt);

        ////队长技能影响
        if (target.m_bSelf)
        {
            if ( selfLeaderSkill != null && target.m_eNature == selfLeaderSkill.HurtProperty )
            {
                hurt -= (int)(hurt * selfLeaderSkill.HurtRate);
            }
            if ( friendLeaderSkill != null && target.m_eNature == friendLeaderSkill.HurtProperty )
            {
                hurt -= (int)(hurt * friendLeaderSkill.HurtRate);
            }
        }

        //反弹伤害
        if (randomNum <= target.m_cReboundRate.GetFinalData())
        {
            int rebHurt = (int)(hurt * target.m_cReboundRateArg.GetFinalData());
            self.DecHP(rebHurt);
            if (self.m_iHp <= 0)
                self.m_iHp = 1;
            gui.SetUIHeroHP(self);
        }

        //吸血判定
        if (randomNum <= self.m_cAbsorbDamageRate.GetFinalData())
        {
            int abHp = (int)(hurt * target.m_cAbsorbDamageRateArg.GetFinalData());
            self.AddHP(abHp);
            gui.SetUIHeroHP(self);
        }

        //伤害减少判定
        if (randomNum <= target.m_cDecHurtRate.GetFinalData())
        {
            hurt *= (1 - target.m_cDecHurtRateArg.GetFinalData());
        }

        //受伤时有概率BB槽上升
        if (GAME_FUNCTION.RANDOM_ONE() <= target.m_cHurtIncBBHPRate.GetFinalData())
        {
            target.m_fBBHP += target.m_cHurtIncBBHP.GetFinalData();
            gui.SetUIHeroBBHP(target);
        }

        int tmpHp = target.m_iHp;
        target.DecHP((int)hurt);
        target.GetGfxObject().HurtState();
        //伤害回复判定
        if (target.m_iHp > 0 && randomNum <= target.m_cRecoverHurtRate.GetFinalData())
        {
            int recoverHP = (int)(hurt * target.m_cRecoverHurtRateArg.GetData() + target.m_cRecover.GetFinalData() * target.m_cRecoverHurtRateArgEx.GetFinalData());

            target.AddHP(recoverHP);
            gui.GeneratorRecoverNum(target.m_cStartPos, recoverHP , target);
        }

        //目标打死时装备影响
        if (target.m_iHp <= 0 && tmpHp > 0)
        {
            BATTLE_FUNCTION.EQUIP_ACTION(BATTLE_TIME_SCENE.BATTLE_TARGET_DEAD, self);
        }

        //统计
        self.m_iTotalDamage += (int)hurt;
        if (target.m_iHp <= 0 && tmpHp > 0) //死亡时间
        {
            target.m_fDeadTime = GAME_TIME.TIME_FIXED();
            self.m_iTotalSkillKillNum++;
        }
        if (target.m_iHp <= 0 && tmpHp <= 0)    //鞭尸伤害
        {
            self.m_iTotalSuperDamage += (int)hurt;
        }

        switch (isBane)
        {
            case 1:
                gui.GeneratorHurtBaneNum(target.m_cUIStartPos, (int)hurt ,target);
                break;
            case 2:
                gui.GeneratorHurtBeBaneNum(target.m_cUIStartPos, (int)hurt ,target);
                break;
            default:
                gui.GeneratorHurtNum(target.m_cUIStartPos, (int)hurt , target);
                break;
        }

        //判定BB技能BUF
        BBSkillTable bbSkillTable = BBSkillTableManager.GetInstance().GetBBSkillTable(self.m_iBBSkillTableID);
        for (int i = 0; i < bbSkillTable.VecBUF.Length; i++)
        {
            if (bbSkillTable.VecBUF[i] > 0)
            {
                float buffRate = bbSkillTable.VecBUFRate[i];
                switch ((BATTLE_BUF)bbSkillTable.VecBUF[i])
                { 
                    case BATTLE_BUF.DU:
                        buffRate *= 1f - target.m_cOpposeDu.GetFinalData();
                        break;
                    case BATTLE_BUF.XURUO:
                        buffRate *= 1f - target.m_cOpposeXuruo.GetFinalData();
                        break;
                    case BATTLE_BUF.POJIA:
                        buffRate *= 1f - target.m_cOpposePojia.GetFinalData();
                        break;
                    case BATTLE_BUF.POREN:
                        buffRate *= 1f - target.m_cOpposePoren.GetFinalData();
                        break;
                    case BATTLE_BUF.MA:
                        buffRate *= 1f - target.m_cOpposeMa.GetFinalData();
                        break;
                    case BATTLE_BUF.FENGYIN:
                        buffRate *= 1f - target.m_cOpposeFengyin.GetFinalData();
                        break;
                }
                if (GAME_FUNCTION.RANDOM_ONE() < buffRate)
                {
                    buf = target.BUFAdd((BATTLE_BUF)bbSkillTable.VecBUF[i], bbSkillTable.VecBUFRound[i], 0);
                }
            }
        }

        //if (self.m_bSelf)
        if (canDrop)
        {
            int res = GAME_FUNCTION.BET(target.m_cHeartDropRate.GetFinalData() + addXinRate, target.m_cShuijingDropRate.GetFinalData() + addShuijingRate, target.m_fGoldRate + addJinbiRate, target.m_fFarmRate + addFarmRate, target.m_fDropItemRate + addItemRate);
            int num = 0;
            switch (res)
            {
                case 0:
                    num = GAME_FUNCTION.RANDOM(1, target.m_iHeartDropNum + 1);
                    for (int i = 0; i < num; i++)
                        gui.GeneratorXin(target.m_cStartPos);
                    break;
                case 1:
                    num = GAME_FUNCTION.RANDOM(target.m_iShuijingDropMinNum, target.m_iShuijingDropNum + 1);
                    for (int i = 0; i < num; i++)
                        gui.GeneratorShuijing(target.m_cStartPos);
                    break;
                case 2:
                    num = GAME_FUNCTION.RANDOM(1, 6);
                    for (int i = 0; i < num; i++)
                        gui.GeneratorJinbi(target.m_cStartPos, GAME_FUNCTION.RANDOM(target.m_iGoldDropNum ,target.m_iGoldDropNumMax));
                    break;
                case 3:
                    num = GAME_FUNCTION.RANDOM(1, 6);
                    for (int i = 0; i < num; i++)
                        gui.GeneratorFarm(target.m_cStartPos, GAME_FUNCTION.RANDOM(target.m_iFarmDropNum, target.m_iFarmDropNumMax));
                    break;
                case 4:
                    if (gui.m_lstDropItem.Count > 0)
                    {
                        num = GAME_FUNCTION.RANDOM_BET(1, gui.m_lstDropItemRate.ToArray())[0];
                        gui.GeneratorItem(target.m_cStartPos, gui.m_lstDropItem[num]);
                    }
                    break;
            }
        }

        gui.SetUIHeroHP(target);
        gui.SetUITargetData(target);

        return buf;
    }

    /// <summary>
    /// 普通攻击伤害
    /// </summary>
    /// <param name="self"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static BATTLE_BUF ATTACK_HURT(BattleHero self, BattleHero target, float rate, float randomNum, IGUIBattle gui, ref bool spark, ref bool critical , bool canDrop)
    {
        // attack - defence/3 = basehurt
        // sparkHurt = basehurt*rate*1.5f   //分段判定
        // criticalHurt = basehurt*rate*1.5f //全局判定
        // 属性相克 hurt =  basehurt*rate*2f  //全局判定
        // 属性被相克 hurt =  basehurt*rate*0.5f  //全局判定 排除 光和暗
        // 防御状态 hurt = basehurt*rate*0.25f   //全局判定

        SoundManager.GetInstance().PlaySound(self.m_cSEHit1);
        SoundManager.GetInstance().PlaySound(self.m_cSEHit2);

        BATTLE_BUF buf = BATTLE_BUF.NONE;

        LeaderSkillTable selfLeaderSkill = null;
        if (self.m_bSelf)
            selfLeaderSkill = gui.GetSelfLeaderSkill();
        else
            selfLeaderSkill = gui.GetTargetLeaderSkill();

        LeaderSkillTable friendLeaderSkill = null;
        if (self.m_bSelf)
            friendLeaderSkill = gui.GetFriendLeaderSkill();

        //基础伤害
        float defence = target.m_cDefence.GetFinalData();
        //无视防御
        if (randomNum  <= target.m_cMissDefenceRate.GetFinalData())
        {
            defence = 0;
        }
        float hurt = (self.m_cAttack.GetFinalData() - defence / 3f) * rate;
        if (hurt < 0)
            hurt = 1;
        float nowTime = GAME_TIME.TIME_FIXED();

        float addXinRate = self.m_cXinDropRateInc.GetFinalData();
        float addShuijingRate = self.m_cShuijingDropRateInc.GetFinalData();
        float addJinbiRate = self.m_cJinbiDropRateInc.GetFinalData();
        float addItemRate = self.m_cItemDropRateInc.GetFinalData();
        float addFarmRate = self.m_cFarmDropRateInc.GetFinalData();

        //Spark伤害
        for (int i = 0 ; i < target.m_vecHitEndTime.Length ; i++)
        {
            if (i != self.m_iIndex && nowTime >= target.m_vecHitStartTime[i] && nowTime <= target.m_vecHitEndTime[i])
            {
                if (!spark)
                {
                    spark = true;
                    gui.GeneratorSpark(target.m_cStartPos , target);
                }
                float sp_rate = 0;
                if (selfLeaderSkill != null)
                {
                    addJinbiRate += selfLeaderSkill.SparkGoldRate;
                    addShuijingRate += selfLeaderSkill.SparkShuijingRate;
                    addXinRate += selfLeaderSkill.SparkHeartRate;
                    addFarmRate += selfLeaderSkill.SparkFarmRate;
                    sp_rate += selfLeaderSkill.SparkHurtRate;
                }
                if (friendLeaderSkill != null)
                {
                    addJinbiRate += friendLeaderSkill.SparkGoldRate;
                    addShuijingRate += friendLeaderSkill.SparkShuijingRate;
                    addXinRate += friendLeaderSkill.SparkHeartRate;
                    addFarmRate += friendLeaderSkill.SparkFarmRate;
                    sp_rate += friendLeaderSkill.SparkHurtRate;
                }
                hurt = hurt * (BATTLE_DEFINE.SPARK_RATE + sp_rate);
                break;
            }
        }
        //暴击伤害
        if ( Role.role.GetBaseProperty().m_iModelID < 0 && randomNum < self.m_cCritical.GetFinalData())
        {
            if (!critical)
            {
                critical = true;
                gui.GeneratorCritical(target.m_cStartPos);
            }
            hurt *= BATTLE_DEFINE.CRITICAL_RATE;
        }

        int isBane = 0;
        if ((self.m_eNature == Nature.Fire && target.m_eNature == Nature.Wood) || (self.m_eNature == Nature.Wood && target.m_eNature == Nature.Thunder)
            || (self.m_eNature == Nature.Thunder && target.m_eNature == Nature.Water) || (self.m_eNature == Nature.Water && target.m_eNature == Nature.Fire)
            || (self.m_eNature == Nature.Bright && target.m_eNature == Nature.Dark) || (self.m_eNature == Nature.Dark && target.m_eNature == Nature.Bright)
            )
        {
            isBane = 1;
            hurt = hurt * BATTLE_DEFINE.NATURE_RATE;
        }

        if ((target.m_eNature == Nature.Fire && self.m_eNature == Nature.Wood) || (target.m_eNature == Nature.Wood && self.m_eNature == Nature.Thunder)
            || (target.m_eNature == Nature.Thunder && self.m_eNature == Nature.Water) || (target.m_eNature == Nature.Water && self.m_eNature == Nature.Fire)
            )
        {
            isBane = 2;
            hurt = hurt * BATTLE_DEFINE.BENATURE_RATE;
        }

        if (target.m_bDefence)
        {
            hurt = hurt * BATTLE_DEFINE.DEFENCE_RATE;
        }
        hurt = Mathf.CeilToInt(hurt);

        ////队长技能影响
        if (target.m_bSelf)
        {
            if (selfLeaderSkill != null && target.m_eNature == selfLeaderSkill.HurtProperty)
            {
                hurt -= (int)(hurt * selfLeaderSkill.HurtRate);
            }
            if (friendLeaderSkill != null && target.m_eNature == friendLeaderSkill.HurtProperty)
            {
                hurt -= (int)(hurt * friendLeaderSkill.HurtRate);
            }
        }

        //反弹伤害
        if (randomNum <= target.m_cReboundRate.GetFinalData())
        {
            int rebHurt = (int)(hurt * target.m_cReboundRateArg.GetFinalData());
            self.DecHP(rebHurt);
            if (self.m_iHp <= 0)
                self.m_iHp = 1;
            gui.SetUIHeroHP(self);
        }

        //吸血判定
        if (randomNum <= self.m_cAbsorbDamageRate.GetFinalData())
        {
            int abHp = (int)(hurt * self.m_cAbsorbDamageRateArg.GetFinalData());
            self.AddHP(abHp);
            gui.SetUIHeroHP(self);
        }

        //伤害减少判定
        if (randomNum <= target.m_cDecHurtRate.GetFinalData())
        {
            hurt *= (1 - target.m_cDecHurtRateArg.GetFinalData());
        }

        //受伤时有概率BB槽上升
        if (GAME_FUNCTION.RANDOM_ONE() <= target.m_cHurtIncBBHPRate.GetFinalData())
        {
            target.m_fBBHP += target.m_cHurtIncBBHP.GetFinalData();
            gui.SetUIHeroBBHP(target);
        }

        int tmpHp = target.m_iHp;
        target.DecHP((int)hurt);
        //伤害回复判定
        if ( target.m_iHp > 0 && randomNum <= target.m_cRecoverHurtRate.GetFinalData())
        {
            int recoverHP = (int)(hurt * target.m_cRecoverHurtRateArg.GetData() * target.m_cRecover.GetFinalData() * target.m_cRecoverHurtRateArgEx.GetFinalData());
            target.AddHP(recoverHP);
            gui.GeneratorRecoverNum(target.m_cStartPos, recoverHP , target);
        }
        target.GetGfxObject().HurtState();

        //目标打死时装备影响
        if (target.m_iHp <= 0 && tmpHp > 0)
        {
            BATTLE_FUNCTION.EQUIP_ACTION(BATTLE_TIME_SCENE.BATTLE_TARGET_DEAD, self);
        }

        //统计
        self.m_iTotalDamage += (int)hurt;
        if (target.m_iHp <= 0 && tmpHp > 0 )    //死亡时间
        {
            target.m_fDeadTime = GAME_TIME.TIME_FIXED();
        }
        if (target.m_iHp <= 0 && tmpHp <= 0)    //鞭尸伤害
        {
            self.m_iTotalSuperDamage += (int)hurt;
        }

        switch (isBane)
        { 
            case 1:
                gui.GeneratorHurtBaneNum(target.m_cUIStartPos, (int)hurt, target);
                break;
            case 2:
                gui.GeneratorHurtBeBaneNum(target.m_cUIStartPos, (int)hurt ,target);
                break;
            default:
                gui.GeneratorHurtNum(target.m_cUIStartPos, (int)hurt, target);
                break;
        }

        //中毒
        if (self.m_cDuRate.GetFinalData()> 0 && GAME_FUNCTION.RANDOM_ONE() < self.m_cDuRate.GetFinalData() * (1f - target.m_cOpposeDu.GetFinalData()))
        //if(true)
        {
            buf = target.BUFAdd(BATTLE_BUF.DU, self.m_iDuRound, 0);
            //buf = target.BUFAdd(BATTLE_BUF.ATTACK_UP, self.m_iDuRound, 0);
        }

        //虚弱
        if (self.m_cXuRuoRate.GetFinalData() > 0 && GAME_FUNCTION.RANDOM_ONE() < self.m_cXuRuoRate.GetFinalData() * (1f - target.m_cOpposeXuruo.GetFinalData()))
        {
            buf = target.BUFAdd(BATTLE_BUF.XURUO, self.m_iXuRuoRound, 0);
        }

        //破甲
        if (self.m_cPojiaRate.GetFinalData() > 0 && GAME_FUNCTION.RANDOM_ONE() < self.m_cPojiaRate.GetFinalData() *(1f - target.m_cOpposePojia.GetFinalData()))
        {
            buf = target.BUFAdd(BATTLE_BUF.POJIA, self.m_iPojiaRound, 0);
        }

        //破刃
        if (self.m_cPorenRate.GetFinalData() > 0 && GAME_FUNCTION.RANDOM_ONE() < self.m_cPorenRate.GetFinalData()*(1f - target.m_cOpposePoren.GetFinalData()))
        {
            buf = target.BUFAdd(BATTLE_BUF.POREN, self.m_iPorenRound, 0);
        }

        //封印
        if (self.m_cFengyinRate.GetFinalData() > 0 && GAME_FUNCTION.RANDOM_ONE() < self.m_cFengyinRate.GetFinalData() *(1f - target.m_cOpposeFengyin.GetFinalData()))
        {
            buf = target.BUFAdd(BATTLE_BUF.FENGYIN, self.m_iFengyinRound, 0);
        }

        //麻痹
        if (self.m_cMaRate.GetFinalData() > 0 && GAME_FUNCTION.RANDOM_ONE() < self.m_cMaRate.GetFinalData() * (1f - target.m_cOpposeMa.GetFinalData()))
        //if(true)
        {
            buf = target.BUFAdd(BATTLE_BUF.MA, self.m_iMaRound, 0);
            //buf = target.BUFAdd(BATTLE_BUF.MA, 2, 0);
        }

        if (canDrop)
        {
            if (Role.role.GetBaseProperty().m_iModelID == GUIDE_FUNCTION.MODEL_BATTLE_SECOND5)
            {
                addShuijingRate = 2;
                target.m_iShuijingDropMinNum = 15;
                target.m_iShuijingDropNum = 16;
            }

            int res = GAME_FUNCTION.BET(target.m_cHeartDropRate.GetFinalData() + addXinRate, target.m_cShuijingDropRate.GetFinalData() + addShuijingRate, target.m_fGoldRate + addJinbiRate, target.m_fFarmRate + addFarmRate , target.m_fDropItemRate + addItemRate);
            int num = 0;
            switch (res)
            {
                case 0:
                    num = GAME_FUNCTION.RANDOM(1, target.m_iHeartDropNum + 1);
                    for (int i = 0; i < num; i++)
                        gui.GeneratorXin(target.m_cStartPos);
                    break;
                case 1:
                    num = GAME_FUNCTION.RANDOM(target.m_iShuijingDropMinNum, target.m_iShuijingDropNum + 1);
                    for (int i = 0; i < num; i++)
                        gui.GeneratorShuijing(target.m_cStartPos);
                    break;
                case 2:
                    num = GAME_FUNCTION.RANDOM(1, 3);
                    for (int i = 0; i < num; i++)
                        gui.GeneratorJinbi(target.m_cStartPos, GAME_FUNCTION.RANDOM(target.m_iGoldDropNum, target.m_iGoldDropNumMax));
                    break;
                case 3:
                    num = GAME_FUNCTION.RANDOM(1, 3);
                    for (int i = 0; i < num; i++)
                        gui.GeneratorFarm(target.m_cStartPos, GAME_FUNCTION.RANDOM(target.m_iFarmDropNum, target.m_iFarmDropNumMax));
                    break;
                case 4:
                    if (gui.m_lstDropItem.Count > 0)
                    {
                        num = GAME_FUNCTION.RANDOM_BET(1, gui.m_lstDropItemRate.ToArray())[0];
                        gui.GeneratorItem(target.m_cStartPos, gui.m_lstDropItem[num]);
                    }
                    break;
            }
        }

        gui.SetUIHeroHP(target);
        gui.SetUITargetData(target);

        return buf;
    }

}
