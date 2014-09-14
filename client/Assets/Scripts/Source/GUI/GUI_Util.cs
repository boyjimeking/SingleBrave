using UnityEngine;
using Game.Base;
using Game.Resource;
using System;

//  GUI_Util.cs
//  Author: Lu Zexi
//  2013-11-13


/// <summary>
/// GUI组件工具类
/// </summary>
public class GUI_FINDATION
{
    /// <summary>
    /// 全局查找物体
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static GameObject FIND_GAME_OBJECT(string path)
    {
        return GameObject.Find(path);
    }

    /// <summary>
    /// 从指定地址中获取指定类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="root"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static GameObject GET_GAME_OBJECT(GameObject root, string path)
    {
        if (root == null)
        {
            Debug.LogError(path);
        }
        Transform tmp = root.transform.Find(path);
        if (tmp == null)
        {
            GAME_LOG.ERROR("Can't Find " + path + " Object .");
            return null;
        }
        return tmp.gameObject;
    }

    /// <summary>
    /// 从指定地址中获取指定类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="root"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static U GET_OBJ_COMPONENT<U>(GameObject root, string path) where U : MonoBehaviour
    {
        Transform tmp = root.transform.Find(path);
        if (tmp == null)
        {
            GAME_LOG.ERROR("Can't Find " + path + " Object .");
            return default(U);
        }
        return (U)tmp.GetComponent<U>();
    }
}



/// <summary>
/// GUI工具
/// </summary>
public class GUI_FUNCTION
{
    private const int ITEM_M_WIDTH = 88;    //物品像素宽度
    private const int ITEM_M_HEIGHT = 88;   //物品像素高度
    //sunyi add 2013-12-26
    private const int ITEM_S_WIDTH = 40;    //物品像素宽度
    private const int ITEM_S_HEIGHT = 40;   //物品像素高度
    //掉落物品大小
    private const int ITEM_D_WIDTH=60;
    private const int ITEM_D_HEIGHT = 60;

    private const int ITEM_SS_WIDTH = 1;    //物品像素宽度
    private const int ITEM_SS_HEIGHT = 1;   //物品像素高度

    private const int ITEM_HERO_DEAIL = 32;  //英雄详细界面装备显示32像素


    private const int AVATOR_SS_WIDTH = 80; //小小头像宽
    private const int AVATOR_SS_HEIGHT = 80;    //小小头像高
    private const int AVATOR_S_WIDTH = 88;    //小头像宽
    private const int AVATOR_S_HEIGHT = 88;   //小头像高
    private const int AVATOR_M_WIDTH = 105;   //中头像宽
    private const int AVATOR_M_HEIGHT = 105;  //中头像高
    private const int AVATOR_L_WIDTH = 130;   //大头像宽
    private const int AVATOR_L_HEIGHT = 325;  //大头像高
    private const int AVATOR_A_WIDTH = 1200;  //全身像宽
    private const int AVATOR_A_HEIGHT = 610; //全身像高
    private const float AVATOR_A_WIDTH_UV = 1;   //全身像UV
    private const float AVATOR_A_HEIGHT_UV = 1;   //全身像UV
    
    //英雄属性路径//
    private const string NATURE_S_FIRE = "attribute_mark0";
    private const string NATURE_S_WATER = "attribute_mark1";
    private const string NATURE_S_WOOD = "attribute_mark2";
    private const string NATURE_S_THUNDER = "attribute_mark3";
    private const string NATURE_S_BRIGHT = "attribute_mark4";
    private const string NATURE_S_DARK = "attribute_mark5";

    private const string NATURE_M_FIRE = "attribute_mark_M";
    private const string NATURE_M_WATER = "attribute_mark_M1";
    private const string NATURE_M_WOOD = "attribute_mark_M2";
    private const string NATURE_M_THUNDER = "attribute_mark_M3";
    private const string NATURE_M_BRIGHT = "attribute_mark_M4";
    private const string NATURE_M_DARK = "attribute_mark_M5";

    private const string NATURE_L_FIRE = "attribute_mark_L0";
    private const string NATURE_L_WATER = "attribute_mark_L1";
    private const string NATURE_L_WOOD = "attribute_mark_L2";
    private const string NATURE_L_THUNDER = "attribute_mark_L3";
    private const string NATURE_L_BRIGHT = "attribute_mark_L4";
    private const string NATURE_L_DARK = "attribute_mark_L5";

    //英雄底色路径
    private const string HERO_L_BORDER_BACK_FIRE = "fire-bg";
    private const string HERO_L_BORDER_BACK_WATER = "water-bg";
    private const string HERO_L_BORDER_BACK_WOOD = "wood-bg";
    private const string HERO_L_BORDER_BACK_THUNDER = "thunder-bg";
    private const string HERO_L_BORDER_BACK_BRIGHT = "light-bg";
    private const string HERO_L_BORDER_BACK_DARK = "dark-bg";

    private static UIAtlas[] s_vecAvatarM = new UIAtlas[3];    //头像图集资源
    private static UIAtlas[] s_vecItemM = new UIAtlas[2];   //物品图集资源

    //物品边框
    private const string ITEM_BORDER_CONSUME = "item_frame_1";
    private const string ITEM_BORDER_MATERIAL = "item_frame_2";
    private const string ITEM_BORDER_EQUIP = "item_frame_3";

    //英雄边框路径
    private const string HERO_BORDER_FIRE = "red-Picture-frame";
    private const string HERO_BORDER_WATER = "blue-Picture-frame";
    private const string HERO_BORDER_WOOD = "green-Picture-frame";
    private const string HERO_BORDER_THUNDER = "yellow-Picture-frame";
    private const string HERO_BORDER_BRIGHT = "light-Picture-frame";
    private const string HERO_BORDER_DARK = "dark-Picture-frame";

    //英雄底色路径
    private const string HERO_BORDER_BACK_FIRE = "red-Pf-bg";
    private const string HERO_BORDER_BACK_WATER = "blue-Pf-bg";
    private const string HERO_BORDER_BACK_WOOD = "green-Pf-bg";
    private const string HERO_BORDER_BACK_THUNDER = "yellow-Pf-bg";
    private const string HERO_BORDER_BACK_BRIGHT = "light-Pf-bg";
    private const string HERO_BORDER_BACK_DARK = "dark-Pf-bg";

    ////字体
    //private static UIFont m_cFont1;    //字体1
    //private static UIFont m_cFont2;    //字体2
    //private static UIFont m_cFont3;    //字体3
    //private static UIFont m_cFont4;    //字体4

    public static void DESTORY()
    {
        for (int i = 0; i < s_vecAvatarM.Length; i++)
            s_vecAvatarM[i] = null;
        for (int i = 0; i < s_vecItemM.Length; i++)
            s_vecItemM[i] = null;
    }

    /// <summary>
    /// 展示剧情
    /// </summary>
    /// <param name="id"></param>
    /// <param name="call"></param>
    public static void SHOW_STORY(int id, GUIStory.CALL_BACK call)
    {
        GUIStory guiStory = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_STORY) as GUIStory;
        guiStory.Show(id, call);
    }

    ///// <summary>
    ///// 获取字体1
    ///// </summary>
    ///// <returns></returns>
    //public static UIFont GetFont1()
    //{
    //    if (m_cFont1 == null)
    //    {
    //        m_cFont1 = (ResourceMgr.Load(GAME_DEFINE.RESOURCE_SHARE ,"BATTLE_Font_1") as GameObject).GetComponent<UIFont>();
    //    }
    //    return m_cFont1;
    //}

    ///// <summary>
    ///// 获取字体2
    ///// </summary>
    ///// <returns></returns>
    //public static UIFont GetFont2()
    //{
    //    if (m_cFont2 == null)
    //    {
    //        m_cFont2 = (ResourceMgr.Load(GAME_DEFINE.RESOURCE_SHARE, "BATTLE_Font_2") as GameObject).GetComponent<UIFont>();
    //    }
    //    return m_cFont2;
    //}

    ///// <summary>
    ///// 获取字体3
    ///// </summary>
    ///// <returns></returns>
    //public static UIFont GetFont3()
    //{
    //    if (m_cFont3 == null)
    //    {
    //        m_cFont3 = (ResourceMgr.Load(GAME_DEFINE.RESOURCE_SHARE, "BATTLE_Font_3") as GameObject).GetComponent<UIFont>();
    //    }
    //    return m_cFont3;
    //}

    ///// <summary>
    ///// 获取字体4
    ///// </summary>
    ///// <returns></returns>
    //public static UIFont GetFont4()
    //{
    //    if (m_cFont4 == null)
    //    {
    //        m_cFont4 = (ResourceMgr.Load(GAME_DEFINE.RESOURCE_SHARE, "BATTLE_Font_4") as GameObject).GetComponent<UIFont>();
    //    }
    //    return m_cFont4;
    //}

    /// <summary>
    /// 设置小英雄属性
    /// </summary>
    /// <param name="sprite"></param>
    /// <param name="nature"></param>
    public static void SET_NATURES(UISprite sprite, Nature nature)
    {
        switch (nature)
        {
            case Nature.Fire:
                sprite.spriteName = NATURE_S_FIRE;
                break;
            case Nature.Water:
                sprite.spriteName = NATURE_S_WATER;
                break;
            case Nature.Wood:
                sprite.spriteName = NATURE_S_WOOD;
                break;
            case Nature.Thunder:
                sprite.spriteName = NATURE_S_THUNDER;
                break;
            case Nature.Bright:
                sprite.spriteName = NATURE_S_BRIGHT;
                break;
            case Nature.Dark:
                sprite.spriteName = NATURE_S_DARK;
                break;
            default:
                Debug.Log("Nature is wrong!");
                return;
        }
    }

    /// <summary>
    /// 设置物品类型边框
    /// </summary>
    /// <param name="spBorder"></param>
    /// <param name="itemType"></param>
    public static void SET_ITEM_BORDER(UISprite spBorder, ITEM_TYPE itemType)
    {
        switch (itemType)
        {
            case ITEM_TYPE.CONSUME:
                if (spBorder != null) spBorder.spriteName = ITEM_BORDER_CONSUME;
                break;
            case ITEM_TYPE.MATERIAL:
                if (spBorder != null) spBorder.spriteName = ITEM_BORDER_MATERIAL;
                break;
            case ITEM_TYPE.EQUIP:
                if (spBorder != null) spBorder.spriteName = ITEM_BORDER_EQUIP;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 设置英雄边框和背景，边框中已经带有属性，无需额外设置
    /// </summary>
    /// <param name="spBorder"></param>
    /// <param name="spBack"></param>
    /// <param name="nature"></param>
    public static void SET_HeroBorderAndBack(UISprite spBorder, UISprite spBack, Nature nature)
    {
        switch (nature)
        {
            case Nature.Fire:
                if (spBorder != null) spBorder.spriteName = HERO_BORDER_FIRE;
                if (spBack != null) spBack.spriteName = HERO_BORDER_BACK_FIRE;
                break;
            case Nature.Water:
                if (spBorder != null) spBorder.spriteName = HERO_BORDER_WATER;
                if (spBack != null) spBack.spriteName = HERO_BORDER_BACK_WATER;
                break;
            case Nature.Wood:
                if (spBorder != null) spBorder.spriteName = HERO_BORDER_WOOD;
                if (spBack != null) spBack.spriteName = HERO_BORDER_BACK_WOOD;
                break;
            case Nature.Thunder:
                if (spBorder != null) spBorder.spriteName = HERO_BORDER_THUNDER;
                if (spBack != null) spBack.spriteName = HERO_BORDER_BACK_THUNDER;
                break;
            case Nature.Bright:
                if (spBorder != null) spBorder.spriteName = HERO_BORDER_BRIGHT;
                if (spBack != null) spBack.spriteName = HERO_BORDER_BACK_BRIGHT;
                break;
            case Nature.Dark:
                if (spBorder != null) spBorder.spriteName = HERO_BORDER_DARK;
                if (spBack != null) spBack.spriteName = HERO_BORDER_BACK_DARK;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 设置长英雄背景
    /// </summary>
    /// <param name="uISprite"></param>
    /// <param name="nature"></param>
    public static void SET_AVATORL_BG(UISprite uISprite, Nature nature)
    {
        switch (nature)
        {
            case Nature.Fire:
                if (uISprite != null) uISprite.spriteName = HERO_L_BORDER_BACK_FIRE;
                break;
            case Nature.Water: 
                if (uISprite != null) uISprite.spriteName = HERO_L_BORDER_BACK_WATER;
                break;
            case Nature.Wood:
                if (uISprite != null) uISprite.spriteName = HERO_L_BORDER_BACK_WOOD;
                break;
            case Nature.Thunder: 
                if (uISprite != null) uISprite.spriteName = HERO_L_BORDER_BACK_THUNDER;
                break;
            case Nature.Bright:
                if (uISprite != null) uISprite.spriteName = HERO_L_BORDER_BACK_BRIGHT;
                break;
            case Nature.Dark: 
                if (uISprite != null) uISprite.spriteName = HERO_L_BORDER_BACK_DARK;
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// 设置中英雄属性
    /// </summary>
    /// <param name="sprite"></param>
    /// <param name="nature"></param>
    public static void SET_NATUREM(UISprite sprite, Nature nature)
    {
        switch (nature)
        {
            case Nature.Fire:
                sprite.spriteName = NATURE_M_FIRE;
                break;
            case Nature.Water:
                sprite.spriteName = NATURE_M_WATER;
                break;
            case Nature.Wood:
                sprite.spriteName = NATURE_M_WOOD;
                break;
            case Nature.Thunder:
                sprite.spriteName = NATURE_M_THUNDER;
                break;
            case Nature.Bright:
                sprite.spriteName = NATURE_M_BRIGHT;
                break;
            case Nature.Dark:
                sprite.spriteName = NATURE_M_DARK;
                break;
            default:
                Debug.Log("Nature is wrong!");
                return;
        }
    }

    /// <summary>
    /// 设置大英雄属性
    /// </summary>
    /// <param name="sprite"></param>
    /// <param name="nature"></param>
    public static void SET_NATUREL(UISprite sprite, Nature nature)
    {
        switch (nature)
        {
            case Nature.Fire:
                sprite.spriteName = NATURE_L_FIRE;
                break;
            case Nature.Water:
                sprite.spriteName = NATURE_L_WATER;
                break;
            case Nature.Wood:
                sprite.spriteName = NATURE_L_WOOD;
                break;
            case Nature.Thunder:
                sprite.spriteName = NATURE_L_THUNDER;
                break;
            case Nature.Bright:
                sprite.spriteName = NATURE_L_BRIGHT;
                break;
            case Nature.Dark:
                sprite.spriteName = NATURE_L_DARK;
                break;
            default:
                Debug.Log("Nature is wrong!");
                return;
        }
    }

    /// <summary>
    /// 获取物品图集
    /// </summary>
    /// <param name="resName"></param>
    /// <returns></returns>
    public static UIAtlas GET_ITEM_ATLAS(string resName)
    {
        UIAtlas at = null;
        for (int i = 0; i < s_vecItemM.Length; i++)
        {
            if (s_vecItemM[i] == null)
            {
				s_vecItemM[i] = ((GameObject)(ResourceMgr.LoadAsset(GAME_DEFINE.RESOURCE_SHARE, "GUIItemM" + (i + 1)))).GetComponent<UIAtlas>();
                //s_vecItemM[i] = ((GameObject)(ResourceMgr.Load("GUIItemM" + (i + 1)))).GetComponent<UIAtlas>();
            }
            for (int j = 0; j < s_vecItemM[i].spriteList.Count; j++)
            {
                if (s_vecItemM[i].spriteList[j].name == resName)
                {
                    at = s_vecItemM[i];
                    break;
                }
            }
            if (at != null)
                break;
        }
        return at;
    }

    /// <summary>
    /// 获取头像图集
    /// </summary>
    /// <param name="resName"></param>
    /// <returns></returns>
    private static UIAtlas GET_AVATAR_ATLAS(string resName)
    {
        UIAtlas at = null;
        for (int i = 0; i < s_vecAvatarM.Length; i++)
        {
            if (s_vecAvatarM[i] == null)
            {
				GameObject tmpObj = (GameObject)ResourceMgr.LoadAsset(GAME_DEFINE.RESOURCE_SHARE, "GUIAvatarM" + (i + 1));
                if( tmpObj == null )
                {
                    Debug.LogError(" atlas is null.");
                }
                s_vecAvatarM[i] = tmpObj.GetComponent<UIAtlas>();
                //s_vecAvatarM[i] = ((GameObject)(ResourceMgr.Load("GUIAvatarM" + (i + 1)))).GetComponent<UIAtlas>();
            }
            for (int j = 0; j < s_vecAvatarM[i].spriteList.Count; j++)
            {
                if (s_vecAvatarM[i].spriteList[j].name == resName)
                {
                    at = s_vecAvatarM[i];
                    break;
                }
            }
            if (at != null)
                break;
        }
        return at;
    }

    /// <summary>
    /// 设置物品物体
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="resName"></param>
    public static GameObject GENERATOR_ITEM_OBJECT(string resName , int layer)
    {
        UIAtlas at = GET_ITEM_ATLAS(resName);
        return GENERATOR_SPRITE_OBJECT(resName, layer, at);
    }

    /// <summary>
    /// 生成精灵图物体
    /// </summary>
    /// <param name="resName">图集名</param>
    /// <param name="layer">物体层级</param>
    /// <param name="?">图集</param>
    /// <returns></returns>
    public static GameObject GENERATOR_SPRITE_OBJECT( string resName , int layer , UIAtlas at)
    {
        UISpriteData spData = null;
        //Debug.Log(" gen name " + resName);
        foreach (UISpriteData item in at.spriteList)
        {
            if (item.name == resName)
            {
                spData = item;
                break;
            }
        }

        if (spData != null)
        {
            GameObject obj = new GameObject();
            obj.layer = layer;
            MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
            MeshRenderer meshRender = obj.AddComponent<MeshRenderer>();
            meshRender.material = new Material(Shader.Find("Particles/Alpha Blended Ex"));
            meshRender.material.mainTexture = at.texture;
            Mesh mesh = new Mesh();

            Vector3[] vects = new Vector3[4];
            Vector2[] uvs = new Vector2[4];

            vects[0] = new Vector3(0, 0, 0);
            vects[1] = new Vector3(spData.width, 0, 0);
            vects[2] = new Vector3(0, spData.height, 0);
            vects[3] = new Vector3(spData.width, spData.height, 0);
            float reX = spData.width / 2f;
            float reY = spData.height / 2f;
            for (int i = 0; i<vects.Length ; i++ )
            {
                vects[i] -= new Vector3(reX, reY);
            }

            Rect uvSp = new Rect(spData.x, spData.y, spData.width, spData.height);
            uvSp = NGUIMath.ConvertToTexCoords(uvSp, at.texture.width, at.texture.height);

            uvs[0] = new Vector2(uvSp.xMin, uvSp.yMin);
            uvs[1] = new Vector2(uvSp.xMax, uvSp.yMin);
            uvs[2] = new Vector2(uvSp.xMin, uvSp.yMax);
            uvs[3] = new Vector2(uvSp.xMax, uvSp.yMax);

            int[] tri = new int[6];
            tri[0] = 0;
            tri[1] = 2;
            tri[2] = 1;

            tri[3] = 2;
            tri[4] = 3;
            tri[5] = 1;

            mesh.vertices = vects;
            mesh.uv = uvs;
            mesh.triangles = tri;
            mesh.RecalculateBounds();
            meshFilter.mesh = mesh;

            return obj;
        }

        return null;
    }

    /// <summary>
    /// 设置物品物体Alpha
    /// </summary>
    /// <param name="obj"></param>
    public static void SET_SPRITE_OBJECT_ALPHA(GameObject obj, float alpha)
    {
        MeshRenderer render = null;
        render = obj.transform.GetComponent<MeshRenderer>();
        if (render == null)
            render = obj.transform.GetComponentInChildren<MeshRenderer>();
        Material mat = render.material;
        Color tmpColor = mat.GetColor("_TintColor");
        Color newtmpColor = new Color(tmpColor.r, tmpColor.g, tmpColor.b, alpha);
        mat.SetColor("_TintColor", newtmpColor);
    }

    /// <summary>
    /// 生成数字
    /// </summary>
    public static GameObject GENERATOR_NUM( int num , UIFont font )
    {
        GameObject obj = new GameObject();
        obj.layer = GAME_DEFINE.U3D_OBJECT_LAYER_UI;
        MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
        MeshRenderer meshRender = obj.AddComponent<MeshRenderer>();
        meshRender.material = font.material;
        Mesh mesh = new Mesh();

        BetterList<Vector3> vects = new BetterList<Vector3>();
        BetterList<Vector2> uvs = new BetterList<Vector2>();
        BetterList<Color32> colors = new BetterList<Color32>();

        font.Print("" + num, 1, new Color32(0, 0, 0, 1), vects, uvs, colors, true, UIFont.SymbolStyle.None, TextAlignment.Center, 100, false);

        //三角形顶点排列
        int count = vects.size;
        int index = 0;
        int[] mIndices = new int[(count >> 1) * 3];
        for (int i = 0; i < count; i += 4)
        {
            mIndices[index++] = i;
            mIndices[index++] = i + 1;
            mIndices[index++] = i + 2;

            mIndices[index++] = i + 2;
            mIndices[index++] = i + 3;
            mIndices[index++] = i;
        }

        //模型居中偏移
        float minX = float.MaxValue;
        float minY = float.MaxValue;
        float maxX = float.MinValue;
        float maxY = float.MinValue;
        for (int i = 0; i < vects.size; i++ )
        {
            if (minX > vects[i].x)
                minX = vects[i].x;
            if (minY > vects[i].y)
                minY = vects[i].y;
            if (maxX < vects[i].x)
                maxX = vects[i].x;
            if (maxY < vects[i].y)
                maxY = vects[i].y;
        }
        float reX = (minX + maxX) / 2f;
        float reY = (minY + maxY) / 2f;
        for (int i = 0; i < vects.size; i++)
        {
            vects[i] += new Vector3(-reX, -reY, 0);
        }

        //模型渲染
        mesh.vertices = vects.ToArray();
        mesh.uv = uvs.ToArray();
        //mesh.colors32 = colors.ToArray();
        mesh.triangles = mIndices;
        mesh.RecalculateBounds();
        meshFilter.mesh = mesh;

        return obj;
    }

    /// <summary>
    /// 设置物品中图标
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="resName"></param>
    public static void SET_ITEMM(UISprite sp, string resName)
    {
        UIAtlas at = GET_ITEM_ATLAS(resName);

        sp.atlas = at;
        sp.spriteName = resName;
        sp.width = ITEM_M_WIDTH;
        sp.height = ITEM_M_HEIGHT;
    }

    ///// <summary>
    ///// 设置物品小图
    ///// </summary>
    ///// <param name="sp"></param>
    ///// <param name="itemID"></param>
    ///// <param name="resName"></param>
    //public static void SET_ITEMS(UISprite sp, int spIndex, string resName)
    //{
    //    if (spIndex <= 0)
    //        return;

    //    if (s_vecItemM[spIndex - 1] == null)
    //    {
    //        s_vecItemM[spIndex - 1] = ((GameObject)(ResourceMgr.Load(GAME_DEFINE.RESOURCE_TEX_PATH, "GUIItemM" + spIndex))).GetComponent<UIAtlas>();
    //    }
    //    sp.atlas = s_vecItemM[spIndex - 1];
    //    sp.spriteName = resName;
    //    sp.width = ITEM_S_WIDTH;
    //    sp.height = ITEM_S_HEIGHT;
    //}

    /// <summary>
    /// 设置物品小图标
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="resName"></param>
    public static void SET_ITEMS(UISprite sp, string resName)
    {
        UIAtlas at = GET_ITEM_ATLAS(resName);

        sp.atlas = at;
        sp.spriteName = resName;
        sp.width = ITEM_S_WIDTH;
        sp.height = ITEM_S_HEIGHT;
    }

    ///// <summary>
    ///// 设置掉落物品大小
    ///// </summary>
    ///// <param name="sp"></param>
    ///// <param name="spIndex"></param>
    ///// <param name="resName"></param>
    //public static void SET_ITEM_DOWN(UISprite sp, int spIndex, string resName)
    //{
    //    if (spIndex <= 0)
    //        return;

    //    if (s_vecItemM[spIndex - 1] == null)
    //    {
    //        s_vecItemM[spIndex - 1] = ((GameObject)(ResourceMgr.Load(GAME_DEFINE.RESOURCE_TEX_PATH, "GUIItemM" + spIndex))).GetComponent<UIAtlas>();
    //    }
    //    sp.atlas = s_vecItemM[spIndex - 1];
    //    sp.spriteName = resName;
    //    sp.width = ITEM_D_WIDTH;
    //    sp.height = ITEM_D_HEIGHT;
    //}

    ///// <summary>
    ///// 设置物品掉落图标
    ///// </summary>
    ///// <param name="sp"></param>
    ///// <param name="resName"></param>
    //public static void SET_ITEM_DOWN(UISprite sp, string resName)
    //{
    //    UIAtlas at = GET_ITEM_ATLAS(resName);

    //    sp.atlas = at;
    //    sp.spriteName = resName;
    //    sp.width = ITEM_D_WIDTH;
    //    sp.height = ITEM_D_HEIGHT;
    //}

    public static void SET_ITEM_HERO_DETAIL(UISprite sp, string resName)
    {
        UIAtlas at = GET_ITEM_ATLAS(resName);

        sp.atlas = at;
        sp.spriteName = resName;
        sp.width = ITEM_HERO_DEAIL;
        sp.height = ITEM_HERO_DEAIL;
    }

    ///// <summary>
    ///// 设置物品图
    ///// </summary>
    ///// <param name="sp"></param>
    ///// <param name="itemID"></param>
    ///// <param name="resName"></param>
    //public static void SET_ITEMSS(UISprite sp, int spIndex, string resName)
    //{
    //    if (spIndex <= 0)
    //        return;

    //    if (s_vecItemM[spIndex - 1] == null)
    //    {
    //        s_vecItemM[spIndex - 1] = ((GameObject)(ResourceMgr.Load(GAME_DEFINE.RESOURCE_TEX_PATH, "GUIItemM" + spIndex))).GetComponent<UIAtlas>();
    //    }
    //    sp.atlas = s_vecItemM[spIndex - 1];
    //    sp.spriteName = resName;
    //    sp.width = ITEM_SS_WIDTH;
    //    sp.height = ITEM_SS_HEIGHT;
    //}

    ///// <summary>
    ///// 设置物品SS图标
    ///// </summary>
    ///// <param name="sp"></param>
    ///// <param name="resName"></param>
    //public static void SET_ITEMSS(UISprite sp, string resName)
    //{
    //    UIAtlas at = GET_ITEM_ATLAS(resName);

    //    sp.atlas = at;
    //    sp.spriteName = resName;
    //    sp.width = ITEM_SS_WIDTH;
    //    sp.height = ITEM_SS_HEIGHT;
    //}

    /// <summary>
    /// 设置小小头像
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="resName"></param>
    public static void SET_AVATORSS(UISprite sp, string resName)
    {
        UIAtlas at = GET_AVATAR_ATLAS(resName);

        sp.atlas = at;
        sp.spriteName = resName;
        sp.width = AVATOR_SS_WIDTH;
        sp.height = AVATOR_SS_HEIGHT;
    }

    /// <summary>
    /// 设置小头像
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="resName"></param>
    public static void SET_AVATORS(UISprite sp, string resName)
    {
        UIAtlas at = GET_AVATAR_ATLAS(resName);

        sp.atlas = at;
        sp.spriteName = resName;
        sp.width = AVATOR_S_WIDTH;
        sp.height = AVATOR_S_HEIGHT;
    }

    /// <summary>
    /// 设置中头像
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="resName"></param>
    public static void SET_AVATORM(UISprite sp, string resName)
    {

        UIAtlas at = GET_AVATAR_ATLAS(resName);

        sp.atlas = at;
        sp.spriteName = resName;
        sp.width = AVATOR_M_WIDTH;
        sp.height = AVATOR_M_HEIGHT;
    }

    /// <summary>
    /// 界面过渡 淡进淡出
    /// </summary>
    public static void BLACKHIDEN_SHOW()
    {
        GUI_Hiden gh = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_HIDEN) as GUI_Hiden;
        gh.Show();
        gh.FadeAll();
    }

    /// <summary>
    /// 界面过渡 淡进
    /// </summary>
    public static void BLACKHIDEN()
    {
        GUI_Hiden gh = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_HIDEN) as GUI_Hiden;
        gh.Show();
        gh.FadeIn();
    }

    /// <summary>
    /// 界面过渡 淡出
    /// </summary>
    public static void BLACKSHOW()
    {
        GUI_Hiden gh = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_HIDEN) as GUI_Hiden;
        gh.Show();
        gh.FadeOut();
    }

    /// <summary>
    /// 展示等待加载界面
    /// </summary>
    public static void LOADING_SHOW()
    {
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_LOADING).Show();
    }

    /// <summary>
    /// 隐藏等待加载界面
    /// </summary>
    public static void LOADING_HIDEN()
    {
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_LOADING).Hiden();
    }

    /// <summary>
    /// 展示Lock面板界面
    /// </summary>
    public static void LOCKPANEL_SHOW()
    {
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_LOCKPANEL).Show();
    }


    /// <summary>
    /// 隐藏Lock面板界面
    /// </summary>
    public static void LOCKPANEL_HIDEN()
    {
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_LOCKPANEL).Hiden();
    }

    /// <summary>
    /// 自动隐藏Lock面板界面
    /// </summary>
    public static void LOCKPANEL_AUTO_HIDEN()
    {
        GUILockPanel panel = (GUILockPanel)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_LOCKPANEL);
        panel.Show();
        panel.SetStartTime(DateTime.Now);
        panel.SetDurationTime(GAME_DEFINE.FADEOUT_GUI_TIME);
    }

    /// <summary>
    /// 展示异步加载界面
    /// </summary>
    public static void AYSNCLOADING_SHOW()
    {
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_AYSNC_LOADING).Show();
    }

    /// <summary>
    /// 隐藏异步加载界面
    /// </summary>
    public static void AYSNCLOADING_HIDEN()
    {
        GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_AYSNC_LOADING).Hiden();
    }

    /// <summary>
    /// 消息展示
    /// </summary>
    /// <param name="cal"></param>
    /// <param name="content"></param>
    /// <param name="args"></param>
    public static void MESSAGEL(GUIMessageL.CALL_BACK cal, string content)
    {
        GUIMessageL gui = (GUIMessageL)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MESSAGEL);
        gui.Show(cal, content);
    }

    /// <summary>
    /// YES or NO 大消息框展示
    /// </summary>
    /// <param name="cal"></param>
    /// <param name="content"></param>
    public static void MESSAGEL_(GUIMessageL.CALL_BACK1 cal, string content)
    {
        MESSAGEL_(cal, content, "btn_ok", "btn_ok1", "btn_cancel", "btn_cancel1");
    }

    /// <summary>
    /// YES or No 大消息框
    /// </summary>
    /// <param name="cal"></param>
    /// <param name="content"></param>
    /// <param name="buttonNormal"></param>
    /// <param name="buttonPress"></param>
    public static void MESSAGEL_(GUIMessageL.CALL_BACK1 cal, string content, string buttonNormal, string buttonPress)
    {
        MESSAGEL_(cal, content, buttonNormal, buttonPress, "btn_cancel", "btn_cancel1");
    }

    /// <summary>
    /// YES or NO 大消息框展示
    /// </summary>
    /// <param name="cal"></param>
    /// <param name="content"></param>
    public static void MESSAGEL_(GUIMessageL.CALL_BACK1 cal, string content , string buttonNormal , string buttonPress , string cancelBtnNormal , string cancelBtnPress )
    {
        GUIMessageL gui = (GUIMessageL)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MESSAGEL);
        gui.Show(cal, content);
        gui.SetButtonImg(buttonNormal, buttonPress);
        gui.SetCancelButtonImg(cancelBtnNormal, cancelBtnPress);
    }

    /// <summary>
    /// 中消息框展示
    /// </summary>
    /// <param name="cal"></param>
    /// <param name="content"></param>
    public static void MESSAGEM(GUIMessageM.CALL_BACK cal, string content)
    {
        GUIMessageM gui = (GUIMessageM)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MESSAGEM);
        gui.Show(cal, content);
    }

    /// <summary>
    /// 中消息框Yes or No
    /// </summary>
    /// <param name="cal"></param>
    /// <param name="content"></param>
    public static void MESSAGEM_(GUIMessageM.CALL_BACK1 cal, string content)
    {
        MESSAGEM_(cal, content, "btn_ok", "btn_ok1", "btn_cancel", "btn_cancel1");
    }

    /// <summary>
    /// 中消息框Yes or No
    /// </summary>
    /// <param name="cal"></param>
    /// <param name="content"></param>
    /// <param name="buttonNormal"></param>
    /// <param name="buttonPress"></param>
    public static void MESSAGEM_(GUIMessageM.CALL_BACK1 cal, string content, string buttonNormal, string buttonPress)
    {
        MESSAGEM_(cal, content, buttonNormal, buttonPress, "btn_cancel", "btn_cancel1");
    }

    /// <summary>
    /// 中消息框Yes or No
    /// </summary>
    /// <param name="cal"></param>
    /// <param name="content"></param>
    public static void MESSAGEM_(GUIMessageM.CALL_BACK1 cal, string content, string buttonNormal, string buttonPress, string cancelBtnNormal, string cancelBtnPress)
    {
        GUIMessageM gui = (GUIMessageM)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MESSAGEM);
        gui.Show(cal, content);
        gui.SetButtonImg(buttonNormal, buttonPress);
        gui.SetCancelButtonImg(cancelBtnNormal, cancelBtnPress);
    }

    /// <summary>
    /// 小消息框
    /// </summary>
    /// <param name="cal"></param>
    /// <param name="content"></param>
    public static void MESSAGES(string content)
    {
        GUIMessageS gui = (GUIMessageS)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MESSAGES);
        gui.Show(content);
    }

    /// <summary>
    /// 小消息框隐藏
    /// </summary>
    public static void MESSAGES_HIDEN()
    {
        GUIMessageS gui = (GUIMessageS)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MESSAGES);
        gui.Hiden();
    }

    /// <summary>
    /// 迷你消息框
    /// </summary>
    /// <param name="content"></param>
    public static void MESSAGESS(string content)
    {
        GUIMessageSS gui = (GUIMessageSS)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MESSAGESS);
        gui.Show(content);
    }

    /// <summary>
    /// 迷你消息框隐藏
    /// </summary>
    public static void MESSAGESS_HIDEN()
    {
        GUIMessageSS gui = (GUIMessageSS)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_MESSAGESS);
        gui.Hiden();
    }

    /// <summary>
    /// 判断当前时间是否在指定时间段内
    /// </summary>
    /// <param name="timeStart"></param>
    /// <param name="timeEnd"></param>
    /// <returns></returns>
    public static bool IsInThisDuration(int timeStart,int timeEnd)
    {
        bool isInDuration = false;

        DateTime dtNow = GAME_FUNCTION.UNIXTimeToCDateTime(GAME_DEFINE.m_lServerTime);

        int timeNowNum = dtNow.Hour;
        if (timeNowNum >= timeStart && timeNowNum <= timeEnd)
        {
            isInDuration = true;
        }
        else {
            isInDuration = false;
        }

        return isInDuration;
    }

    /// <summary>
    /// 判断当前星期是否在指定星期内
    /// </summary>
    /// <param name="dayStart"></param>
    /// <param name="dayEnd"></param>
    /// <returns></returns>
    public static bool IsInThisWeek(int dayStart, int dayEnd)
    {
        bool isInWeek = false;
        int dayOfWeekIndex = 1;
        DateTime dtNow = GAME_FUNCTION.UNIXTimeToCDateTime(GAME_DEFINE.m_lServerTime);
        if ((int)dtNow.DayOfWeek == 0)
        {
            dayOfWeekIndex = 7;
        }
        else {
            dayOfWeekIndex = (int)dtNow.DayOfWeek;
        }

        if (dayOfWeekIndex >= dayStart & dayOfWeekIndex <= dayEnd)
        {
            isInWeek = true;
        }
        else {
            isInWeek = false;
        }

        return isInWeek;
    }

    /// <summary>
    /// 判断当天是否为指定的星期几
    /// </summary>
    /// <param name="dayOfWeek"></param>
    /// <returns></returns>
    public static bool IsTodayInWeek(int dayOfWeek)
    {
        int dayOfIndex = 0;
        bool isTodayInweek = false;
        if (dayOfWeek == 0)
        {
            dayOfIndex = 7;
        }
        else {
            dayOfIndex = dayOfWeek;
        }

        DateTime dtNow = GAME_FUNCTION.UNIXTimeToCDateTime(GAME_DEFINE.m_lServerTime);

        if (dayOfIndex == (int)dtNow.DayOfWeek)
        {
            isTodayInweek = true;
        }
        else {
            isTodayInweek = false;
        }

        return isTodayInweek;
    }

    /// <summary>
    /// 判断当前日期是否在指定日期内
    /// </summary>
    /// <param name="dateStart"></param>
    /// <param name="dateEnd"></param>
    /// <returns></returns>
    public static bool IsInThisDates(DateTime dateStart,DateTime dateEnd)
    {
        bool isInDates = false;
        DateTime dtNow = GAME_FUNCTION.UNIXTimeToCDateTime(GAME_DEFINE.m_lServerTime);
        if ((DateTime.Compare(dtNow, dateStart) >= 0) && (DateTime.Compare(dtNow, dateEnd) <= 0))
        {
            isInDates = true;
        }
        else {
            isInDates = false;
        }

        return isInDates;
    }

    /// <summary>
    /// 根据成长类型获取对应称号
    /// </summary>
    /// <param name="etype"></param>
    /// <returns></returns>
    public static string GetHeroGrowTypeName(GrowType etype)
    {
        string re = "";
        switch (etype)
        {
            case GrowType.Balance: re = GAME_FUNCTION.STRING(STRING_DEFINE.HERO_TYPE_BALANCE);
                break;
            case GrowType.Hp: re = GAME_FUNCTION.STRING(STRING_DEFINE.HERO_TYPE_HP);
                break;
            case GrowType.Attack: re = GAME_FUNCTION.STRING(STRING_DEFINE.HERO_TYPE_ATTACK);
                break;
            case GrowType.Defense: re = GAME_FUNCTION.STRING(STRING_DEFINE.HERO_TYPE_DEFENCE);
                break;
            case GrowType.Revert: re = GAME_FUNCTION.STRING(STRING_DEFINE.HERO_TYPE_RECOVER);
                break;
            default:
                break;
        }

        return re;
    }

    /// <summary>
    /// 获取英雄类型字符串
    /// </summary>
    /// <param name="heroTypeID"></param>
    /// <returns></returns>
    public static string GET_HERO_TYPE_STR(int heroTypeID)
    {
        string str = "";
        switch (heroTypeID)
        { 
            case 1:
                str = GAME_FUNCTION.STRING(STRING_DEFINE.HERO_TYPE_STR1);
                break;
            case 2:
                str = GAME_FUNCTION.STRING(STRING_DEFINE.HERO_TYPE_STR2);
                break;
            case 3:
                str = GAME_FUNCTION.STRING(STRING_DEFINE.HERO_TYPE_STR3);
                break;
        }
        return str;
    }
}