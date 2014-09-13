using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base;
using Game.Resource;
using Game.Base;
using UnityEngine;


//  SoundManager.cs
//  Author: Lu Zexi
//  2014-01-14



/// <summary>
/// 音效地址
/// </summary>
public class SOUND_DEFINE
{
    public const string BGM_MAIN = "SD_BGM01主题乐";
    public const string BGM_ACTIVE = "SD_BGM02活动本";
    public const string BGM_ARENA = "SD_BGM04擂台";
    public const string BGM_TOWN = "SD_BGM03村界面";
    public const string SE_SLIDE = "SD_SE04翻页";
    public const string SE_UPGRADE = "SD_SE05升级2";
    public const string SE__EXP_ADD = "SD_SE06经验增加2";
    public const string SE_NUM_JUMP = "SD_SE07数字跳动";
    public const string SE_ITEM_GET = "SD_SE08收获公用";
    public const string SE_CONSUME_COMBINE = "SD_SE09制造道具";
    public const string SE_EQUIP_COMBINE = "SD_SE10制造装备";
    public const string SE_BATTLE_UPGRADE = "SD_SE05升级2";
    public const string SE_BATTLE_ADD = "SD_SE06经验增加2";
    public const string SE_TITTLE_JOIN_IN = "SD_SE01进入游戏"; //进入游戏

    //召唤
    public const string SE_SUMMON_ENTER = "SD_SE34进入召唤音效";
    public const string SE_SUMMON_DOOR_1 = "SD_SE35召唤开门音效普通";
    public const string SE_SUMMON_DOOR_2 = "SD_SE36召唤开门音效稀有";
    public const string SE_SUMMON_DOOR_3 = "SD_SE37召唤开门音效超稀有";
    public const string SE_SUMMON_STAR_2 = "SD_SE39获得英雄【猛将】";
    public const string SE_SUMMON_STAR_3 = "SD_SE40获得英雄【超 猛将】";
    public const string SE_SUMMON_STAR_4 = "SD_SE41获得英雄【超绝 猛将】";
    public const string SE_SUMMON_STAR_5 = "SD_SE42获得英雄【绝世无双】";

    //进化
    public const string SE_EVO_HERO = "SD_SE38进化特效音";

    //竞技场
    public const string SE_NAME_UP = "SD_SE12官位提升";
    public const string SE_NAME_DOWN = "SD_SE13官位下降";

    //战斗
    public const string SE_BATTLE_JOIN = "SD_SE15进入战斗";    //战斗进入音效
    public const string SE_BATTLE_NEXT_PROCESS = "SD_SE16关卡进度条";    //进度滚动音效
    public const string SE_BATTLE_BOSS_WARNING = "SD_SE17Boss出现";    //BOSS警告音效
    public const string SE_BATTLE_BOX_WARNING = "SD_SE18宝箱怪出现"; //宝箱警告音效
    public const string SE_BATTLE_WIN = "SD_SE20战斗胜利"; //战斗胜利音效
    public const string SE_BATTLE_LOSE = "SD_SE21战斗失败";    //战斗失败音效
    public const string SE_BATTLE_CONGRATULATE = "SD_SE30敌将击破";    //敌将击破音效
    public const string SE_BATTLE_OPEN_BOX = "SD_SE19开宝箱";    //开宝箱音效
    public const string SE_BATTLE_CLICK_HERO = "SD_SE43点击英雄头像";  //点击英雄
    public const string SE_BATTLE_GET_SHUIJING = "SD_SE32战斗水晶吸收";    //获得水晶
    public const string SE_BATTLE_GET_XIN = "SD_SE33治愈魂吸收"; //获得心
    public const string SE_BATTLE_GET_FARM = "SD_SE28吸收农场点";    //获得农场点
    public const string SE_BATTLE_GET_SOUL = "SD_SE27英雄吸收";    //吸收英雄
    public const string SE_BATTLE_GET_JINBI = "SD_SE11获得金币"; //吸收金币
    public const string SE_BATTLE_SKILL_CUTIN = "SD_SE29技能CUTIN";   //技能CUTIN
    public const string SE_BATTLE_DEAD = "SD_SE23英雄死亡";    //死亡音效
    public const string SE_BATTLE_ADD_HP = "SD_SE25回血";  //回血音效
    public const string SE_BATTLE_RELIVE = "SD_SE24英雄复活";  //复活
    public const string SE_BATTLE_DEC_HP = "SD_SE22扣血";   //中毒扣血
    public const string SE_BATTLE_PVP_JOIN = "SD_SE14竞技场开战";    //竞技场开始
}

/// <summary>
/// 声音管理类
/// </summary>
public class SoundManager : Singleton<SoundManager>
{
    private string AUDIO_PATH = "ROOT/CAMERA";   //聆听者地址

    private AudioListener m_cAudioListener; //聆听者
    private AudioSource m_cAudioSource; //声音源
    private AudioSource m_cAudioSourceTime;  //UI音乐

    //临时
    private AudioClip m_cReadyAudio;    //准备播放的背景音乐
    private float m_fStartTime; //准备开始背景音乐时间
    private const float BG_FADEOUT_TIME = 0.5F;   //fadeout时间
    private const float BG_FADEIN_TIME = 0.5F;   //fadein时间
    private SOUND_STATE m_eState;   //状态

    private enum SOUND_STATE
    { 
        NONE = 0,   //无
        START =1,  //开始
        BG_OLD_START,   //旧音乐处理开始
        BG_OLD, //旧音乐处理
        BG_OLD_END, //旧音乐处理结束
        BG_NEW_START,   //新音乐处理开始
        BG_NEW, //新音乐处理
        BG_NEW_END, //新音乐处理结束
        END,    //结束
    }

    public SoundManager()
    {
        this.m_eState = SOUND_STATE.NONE;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Inititalize()
    {
        this.m_cAudioListener = GameObject.Find(AUDIO_PATH).GetComponent<AudioListener>();
        AudioSource[] sous = this.m_cAudioListener.GetComponents<AudioSource>();
        m_cAudioSource = sous[0];
        m_cAudioSourceTime = sous[1];
    }

    /// <summary>
    /// 设置背景音乐音量
    /// </summary>
    /// <param name="volume"></param>
    public void SetBGMVolume(float volume)
    {
        this.m_cAudioSource.volume = volume;
        m_cAudioSourceTime.volume = volume;
    }

    /// <summary>
    /// 连续播放短促音乐
    /// </summary>
    /// <param name="snd"></param>
    public void PlaySoundContinue(string snd)
    {

        if (this.m_cAudioSourceTime.clip != null && this.m_cAudioSourceTime.clip.name == snd)
        {

            if (!this.m_cAudioSourceTime.isPlaying)
            {
                   this.m_cAudioSourceTime.Play();
            }
            return;
        }
        AudioClip clip = UnityEngine.Resources.Load(snd) as AudioClip;

        if (clip == null)
            return;

        //clip
        this.m_cAudioSourceTime.clip = clip;
        this.m_cAudioSourceTime.volume = GAME_SETTING.s_fBGM_Volume;
        this.m_cAudioSourceTime.loop = true;
        this.m_cAudioSourceTime.Play();

    }

    /// <summary>
    /// 停止播放
    /// </summary>
    public void StopSoundContinue()
    {
        if (this.m_cAudioSourceTime != null)
        {
            this.m_cAudioSourceTime.Stop();
        }
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="snd"></param>
    public void PlayBGM(string snd)
    {
        AudioClip clip = UnityEngine.Resources.Load(snd) as AudioClip;

        if (clip == null)
            return;

        this.m_cReadyAudio = clip;

        if (this.m_cAudioSource.clip != null && this.m_cReadyAudio != null && this.m_cAudioSource.clip.name != this.m_cReadyAudio.name)
        {
            this.m_eState = SOUND_STATE.START;
        }
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="clip"></param>
    public void PlayBGM(AudioClip clip)
    {
        if (clip == null)
            return;

        //clip
        this.m_cAudioSource.clip = clip;
        this.m_cAudioSource.volume = GAME_SETTING.s_fBGM_Volume;
        this.m_cAudioSource.loop = true;
        this.m_cAudioSource.Play();
    }

    /// <summary>
    /// 播放音乐接连上一次
    /// </summary>
    /// <param name="snd"></param>
    public void PlaySound(string snd)
    {
        AudioClip clip = UnityEngine.Resources.Load(snd) as AudioClip;

        if (clip == null)
            return;

        //clip
        this.m_cAudioSource.PlayOneShot(clip, GAME_SETTING.s_fSE_Volume);
    }

    /// <summary>
    /// 连续播放短促音乐
    /// </summary>
    /// <param name="snd"></param>
    public void PlaySound2(string snd)
    {
        AudioClip clip = UnityEngine.Resources.Load(snd) as AudioClip;

        if (clip == null)
            return;

        //clip
        this.m_cAudioSourceTime.clip = clip;
        this.m_cAudioSourceTime.volume = GAME_SETTING.s_fBGM_Volume;
        this.m_cAudioSourceTime.loop = false;
        this.m_cAudioSourceTime.Play();

    }

    /// <summary>
    /// 播放音乐
    /// </summary>
    /// <param name="snd"></param>
    public void PlaySound(AudioClip clip )
    {
        if (clip == null)
            return;

        this.m_cAudioSource.PlayOneShot(clip, GAME_SETTING.s_fSE_Volume);
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void StopBGM()
    { 
        if( this.m_cAudioSource != null)
        {
            this.m_cAudioSource.Stop();
        }
    }

    /// <summary>
    /// 更新
    /// </summary>
    public void Update()
    {
        switch (this.m_eState)
        { 
            case SOUND_STATE.START:
                this.m_eState++;
                break;
            case SOUND_STATE.BG_OLD_START:
                this.m_fStartTime = GAME_TIME.TIME_APP();
                if (this.m_cAudioSource.clip == null)
                {
                    this.m_eState = SOUND_STATE.BG_NEW_START;
                    break;
                }
                this.m_eState++;
                break;
            case SOUND_STATE.BG_OLD:
                float dis = GAME_TIME.TIME_APP() - this.m_fStartTime;
                float rate = dis / BG_FADEOUT_TIME;
                if (rate >= 1f)
                {
                    this.m_cAudioSource.volume = 0;
                    this.m_eState++;
                }
                else
                {
                    this.m_cAudioSource.volume = GAME_SETTING.s_fBGM_Volume * (1-rate);
                }
                break;
            case SOUND_STATE.BG_OLD_END:
                this.m_eState++;
                break;
            case SOUND_STATE.BG_NEW_START:
                this.m_fStartTime = GAME_TIME.TIME_APP();
                this.m_cAudioSource.clip = this.m_cReadyAudio;
                this.m_cAudioSource.volume = 0;
                this.m_cAudioSource.loop = true;
                this.m_cAudioSource.Play();
                this.m_eState++;
                break;
            case SOUND_STATE.BG_NEW:
                dis = GAME_TIME.TIME_APP() - this.m_fStartTime;
                rate = dis / BG_FADEIN_TIME;
                if (rate >= 1f)
                {
                    this.m_cAudioSource.volume = GAME_SETTING.s_fBGM_Volume;
                    this.m_eState++;
                }
                else
                {
                    this.m_cAudioSource.volume = GAME_SETTING.s_fBGM_Volume * rate;
                }
                break;
            case SOUND_STATE.BG_NEW_END:
                this.m_eState++;
                break;
            case SOUND_STATE.END:
                this.m_eState++;
                break;
        }

    }

}
