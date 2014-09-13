using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  Role.cs
//  Author: Lu Zexi
//  2013-11-13



/// <summary>
/// 角色类
/// </summary>
public class Role
{
    private RoleBaseProperty m_cBaseProperty;   //基本属性
    private HeroProperty m_cHeroProperty;   //英雄属性
    private TeamProperty m_cTeamProperty;   //队伍属性
    private BattleFriendProperty m_cBattleFriendProperty;   //战友属性
    private FriendProperty m_cFriendProperty;   //好友属性
    private ItemProperty m_cItemProperty;    //物品属性
    private MailProperty m_cMailProperty; //邮件属性
    private FubenProperty m_cFubenProperty;//副本任务属性
    private BuildingProperty m_cBuildingProperty; //建筑属性
    private HeroBookProperty m_cHeroBookProperty;   //英雄图鉴
    private ItemBookProperty m_cItemBookProperty;   //物品推荐
    private StoreDiamondProperty m_cStoreDiamondProperty;//商城钻石价格属性
    private BattleRecordProperty m_cBattleRecordProperty;//战绩属性
    private PayProperty m_cPayProperty; //支付属性

    private static Role s_cInstance;    //静态实例

    public Role()
    {
        this.m_cBaseProperty = new RoleBaseProperty();
        this.m_cHeroProperty = new HeroProperty();
        this.m_cTeamProperty = new TeamProperty();
        this.m_cFriendProperty = new FriendProperty();
        this.m_cBattleFriendProperty = new BattleFriendProperty();
        this.m_cItemProperty = new ItemProperty();
        this.m_cMailProperty = new MailProperty();
        this.m_cBuildingProperty=new BuildingProperty();
        this.m_cFubenProperty = new FubenProperty();
        this.m_cHeroBookProperty = new HeroBookProperty();
        this.m_cItemBookProperty = new ItemBookProperty();
        this.m_cStoreDiamondProperty = new StoreDiamondProperty();
        this.m_cBattleRecordProperty = new BattleRecordProperty();
        this.m_cPayProperty = new PayProperty();
    }


    /// <summary>
    /// 获取角色静态实例
    /// </summary>
    public static Role role
    {
        get
        {
            if (s_cInstance == null)
            {
                s_cInstance = new Role();
            }

            return s_cInstance;
        }
    }


    /// <summary>
    /// 销毁
    /// </summary>
    public void Destory()
    {
        this.m_cHeroProperty.Destory();
        this.m_cFriendProperty.Destory();
        this.m_cBattleFriendProperty.Destory();
        this.m_cItemProperty.Destory();
        this.m_cBuildingProperty.Destory();
        this.m_cFubenProperty.Destory();
        this.m_cHeroBookProperty.Destory();
        this.m_cItemBookProperty.Destory();
    }

    /// <summary>
    /// 获取角色基本属性
    /// </summary>
    /// <returns></returns>
    public RoleBaseProperty GetBaseProperty()
    {
        return this.m_cBaseProperty;
    }


    /// <summary>
    /// 获取英雄属性
    /// </summary>
    /// <returns></returns>
    public HeroProperty GetHeroProperty()
    {
        return this.m_cHeroProperty;
    }

    /// <summary>
    /// 获取团队属性
    /// </summary>
    /// <returns></returns>
    public TeamProperty GetTeamProperty()
    {
        return this.m_cTeamProperty;
    }

    /// <summary>
    /// 获取好友属性
    /// </summary>
    /// <returns></returns>
    public FriendProperty GetFriendProperty()
    {
        return this.m_cFriendProperty;
    }

    /// <summary>
    /// 获取战友属性
    /// </summary>
    /// <returns></returns>
    public BattleFriendProperty GetBattleFriendProperty()
    {
        return this.m_cBattleFriendProperty;
    }

    /// <summary>
    /// 获取物品属性
    /// </summary>
    /// <returns></returns>
    public ItemProperty GetItemProperty()
    {
        return this.m_cItemProperty;
    }

    /// <summary>
    /// 获取邮件属性
    /// </summary>
    /// <returns></returns>
    public MailProperty GetMailProperty()
    {
        return this.m_cMailProperty;
    }

    /// <summary>
    /// 获取副本任务属性
    /// </summary>
    /// <returns></returns>
    public FubenProperty GetFubenProperty()
    {
        return this.m_cFubenProperty;
    }

    /// <summary>
    /// 获取建筑属性
    /// </summary>
    /// <returns></returns>
    public BuildingProperty GetBuildingProperty()
    {
        return this.m_cBuildingProperty;
    }

    /// <summary>
    /// 获取英雄图鉴属性
    /// </summary>
    /// <returns></returns>
    public HeroBookProperty GetHeroBookProperty()
    {
        return this.m_cHeroBookProperty;
    }

    /// <summary>
    /// 获取物品图鉴属性
    /// </summary>
    /// <returns></returns>
    public ItemBookProperty GetItemBookProperty()
    {
        return this.m_cItemBookProperty;
    }

    /// <summary>
    /// 商城钻石价格属性
    /// </summary>
    /// <returns></returns>
    public StoreDiamondProperty GetStoreDiamondProperty()
    {
        return this.m_cStoreDiamondProperty;
    }

    /// <summary>
    /// 战绩属性
    /// </summary>
    /// <returns></returns>
    public BattleRecordProperty GetBattleRecordProperty()
    {
        return this.m_cBattleRecordProperty;
    }

    /// <summary>
    /// 获取支付属性
    /// </summary>
    /// <returns></returns>
    public PayProperty GetPayProperty()
    {
        return this.m_cPayProperty;
    }
}

