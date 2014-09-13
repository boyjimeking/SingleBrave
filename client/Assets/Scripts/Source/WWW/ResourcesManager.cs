
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  ResourceManager.cs
//  Lu Zexi
//  2012-7-5

namespace Game.Resource
{
    public delegate void DownLoadCallBack( string resName , object asset , object[] arg ); //下载回调
    public delegate byte[] DecryptBytesFunc(byte[] datas);  //解密接口
    public delegate void CALLBACK(UnityEngine.Object asset);

    /// <summary>
    /// 资源类型
    /// </summary>
    public enum RESOURCE_TYPE
    { 
        WEB_RESOURCES = 0,  //网络资源
        WEB_TEXTURE,    //网络贴图资源
        WEB_OBJECT,     //网络物体资源
        WEB_TEXT_STR,   //网络文本文件资源
        WEB_TEXT_BYTES, //网络2进制文件资源
        WEB_MAX,        //网络资源定义结束
        PC_RESOURCES,   //软件资源
        PC_TEXTURE,     //软件贴图资源
        PC_OBJECT,      //软件物体资源
        PC_TEXT_STR,    //软件文本文件资源
        PC_TEXT_BYTES,  //软件2进制文件资源
        PC_MAX,         //软件资源定义结束
        LOC_RESOURCES,  //本地资源
        LOC_TEXTURE,    //本地贴图资源
        LOC_OBJECT,     //本地物体资源
        LOC_TEXT_STR,   //本地文本文件资源
        LOC_TEXT_BYTES, //本地2进制文件资源
        LOC_MAX,        //本地资源定义结束
        MAX,
    }

    /// <summary>
    /// 加密类型
    /// </summary>
    public enum ENCRYPT_TYPE
    { 
        NORMAL,     //无加密
        ENCRYPT,    //有加密
    }

    /// <summary>
    /// 资源需求者
    /// </summary>
    public class ResourceRequireOwner
    {
        public string m_cResName;               //资源名
        public string m_strResValue;    //资源值
        public RESOURCE_TYPE m_eResType;        //资源类型
        public DownLoadCallBack m_delCallBack;  //回调方法
        public object[] m_vecArg;   //参数
    }

    /// <summary>
    /// 资源管理类
    /// </summary>
    public class ResourcesManager
    {
        private const int LOAD_MAX_NUM = 3;
        private const string RESOURCE_POST = ".res";    //资源名后缀
        private Dictionary<string, ResourceRequireData> m_mapRes = new Dictionary<string, ResourceRequireData>();   //资源集合
        private List<ResourceRequireData> m_lstRequires = new List<ResourceRequireData>();  //需求数据
        private DecryptBytesFunc m_delDecryptFunc;  //解密接口

        //异步加载
        private RESOURCE_TYPE m_eLoadType; //异步加载方式
        private Dictionary<string, object> m_mapAsyncLoader = new Dictionary<string, object>();    //异步映射

        private static ResourcesManager m_cInstance;    //静态实例

        public ResourcesManager()
        {
            this.m_delDecryptFunc = CEncrypt.DecryptBytes;
//#if UNITY_EDITOR
            this.m_eLoadType = RESOURCE_TYPE.LOC_OBJECT;
//#else
//            this.m_eLoadType = RESOURCE_TYPE.WEB_OBJECT;
//#endif
        }

        /// <summary>
        /// 获取静态实例
        /// </summary>
        /// <returns></returns>
        public static ResourcesManager GetInstance()
        {
            if (m_cInstance == null)
            {
                m_cInstance = System.Activator.CreateInstance<ResourcesManager>();
            }

            return m_cInstance;
        }

        /// <summary>
        /// 清除异步加载
        /// </summary>
        public void ClearAsyncLoad()
        {
            this.m_mapAsyncLoader.Clear();
        }

        /// <summary>
        /// 获取异步进程
        /// </summary>
        /// <returns></returns>
        public float GetAsyncProcess()
        {
            float rate = 0;
            if (this.m_eLoadType == RESOURCE_TYPE.WEB_OBJECT)
            {
                foreach (KeyValuePair<string, object> item in this.m_mapAsyncLoader)
                {
                    rate += ((AsyncLoader)item.Value).Progress();
                }
            }
            else
            {
                rate = this.m_mapAsyncLoader.Count;
            }

            if (this.m_mapAsyncLoader.Count <= 0)
                return 1f;

            rate /= this.m_mapAsyncLoader.Count;

            return rate;
        }

        /// <summary>
        /// 获取异步加载的资源
        /// </summary>
        /// <param name="resName"></param>
        /// <returns></returns>
        public UnityEngine.Object GetAsyncObject(string resName)
        {
            if (this.m_eLoadType == RESOURCE_TYPE.WEB_OBJECT)
            {
                if (this.m_mapAsyncLoader.ContainsKey(resName))
                {
                    return ((AsyncLoader)this.m_mapAsyncLoader[resName]).GetAsset();
                }
                else
                {
                    Debug.LogError("Error: the async object is not exist." + resName);
                }
            }
            else
            {
                if (this.m_mapAsyncLoader.ContainsKey(resName))
                {
                    return (UnityEngine.Object)this.m_mapAsyncLoader[resName];
                }
                else
                {
                    Debug.LogError("Error: the async object is not exist." + resName);
                }
            }

            return null;
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="path"></param>
        public void LoadAsync(string resName)
        {
            LoadAsync(resName, resName);
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="path"></param>
        /// <param name="resName"></param>
        /// <returns></returns>
        public void LoadAsync(string path , string resName )
        {
            if ( this.m_eLoadType == RESOURCE_TYPE.WEB_OBJECT )
            {
                if (this.m_mapRes.ContainsKey(path))
                {
                    if (!this.m_mapAsyncLoader.ContainsKey(resName))
                    {
                        AsyncLoader loader = AsyncLoader.StartLoad(((AssetBundle)this.m_mapRes[path].GetAssetObject()), resName);
                        this.m_mapAsyncLoader.Add(resName, loader);
                    }
                    else
                    {
                        Debug.Log("The resources is already in the list. " + resName);
                    }
                }
                else
                {
                    Debug.LogError("The resources is not exist. " + path );
                }
            }
            else
            {
                if (!this.m_mapAsyncLoader.ContainsKey(resName))
                {
#if UNITY_EDITOR
                    UnityEngine.Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesWWW/" + resName + ".prefab", typeof(UnityEngine.Object));
                    if (obj == null)
                    {
                        obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesWWW/" + resName + ".png", typeof(UnityEngine.Object));
                    }
                    if (obj == null)
                    {
                        obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesWWW/" + resName + ".jpg", typeof(UnityEngine.Object));
                    }
                    if (obj == null)
                    {
                        obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesWWW/" + resName + ".txt", typeof(UnityEngine.Object));
                    }
                    //cache
                    if (obj == null)
                    {
                        obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesCache/" + resName + ".prefab", typeof(UnityEngine.Object));
                    }
                    if (obj == null)
                    {
                        obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesCache/" + resName + ".png", typeof(UnityEngine.Object));
                    }
                    if (obj == null)
                    {
                        obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesCache/" + resName + ".jpg", typeof(UnityEngine.Object));
                    }
                    if (obj == null)
                    {
                        obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesCache/" + resName + ".txt", typeof(UnityEngine.Object));
                    }
                    //avatarM
                    if (obj == null)
                    {
                        obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/AvatarM/" + resName + ".prefab", typeof(UnityEngine.Object));
                    }
                    if (obj == null)
                    {
                        obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/AvatarM/" + resName + ".png", typeof(UnityEngine.Object));
                    }
                    if (obj == null)
                    {
                        obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/AvatarM/" + resName + ".jpg", typeof(UnityEngine.Object));
                    }
                    if (obj == null)
                    {
                        obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/AvatarM/" + resName + ".txt", typeof(UnityEngine.Object));
                    }
                    //item
                    if (obj == null)
                    {
                        obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/Item/" + resName + ".prefab", typeof(UnityEngine.Object));
                    }
                    if (obj == null)
                    {
                        obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/Item/" + resName + ".png", typeof(UnityEngine.Object));
                    }
                    if (obj == null)
                    {
                        obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/Item/" + resName + ".jpg", typeof(UnityEngine.Object));
                    }
                    if (obj == null)
                    {
                        obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/Item/" + resName + ".txt", typeof(UnityEngine.Object));
                    }
                    this.m_mapAsyncLoader.Add(resName, obj);
#else
                    UnityEngine.Object obj = UnityEngine.Resources.Load(resName);
                    this.m_mapAsyncLoader.Add(resName, obj);
#endif
                }
                else
                {
                    Debug.Log("The resources is already in the list. " + resName);
                }
            }
        }

        ///// <summary>
        ///// 读取资源
        ///// </summary>
        ///// <param name="resName"></param>
        ///// <returns></returns>
        //public object Load(string resName)
        //{
        //    return Load(this.m_eLoadType, resName);
        //}

        ///// <summary>
        ///// 读取资源
        ///// </summary>
        ///// <param name="resType"></param>
        ///// <param name="resName"></param>
        ///// <returns></returns>
        //public object Load( RESOURCE_TYPE resType , string resName)
        //{
        //    if ((int)resType >= (int)RESOURCE_TYPE.WEB_RESOURCES && (int)resType <= (int)RESOURCE_TYPE.WEB_MAX)
        //    {
        //        if (this.m_mapRes.ContainsKey(resName))
        //        {
        //            if (resType == RESOURCE_TYPE.WEB_TEXT_STR)
        //            {
        //                UnityEngine.Object obj = ((AssetBundle)this.m_mapRes[resName].GetAssetObject()).mainAsset;
        //                return obj;
        //            }
        //            else
        //            {
        //                Object res = ((AssetBundle)this.m_mapRes[resName].GetAssetObject()).Load(resName);
        //                return res;
        //            }
        //        }
        //    }
        //    else if ((int)resType >= (int)RESOURCE_TYPE.LOC_RESOURCES && (int)resType <= (int)RESOURCE_TYPE.LOC_MAX)
        //    {
        //        return UnityEngine.Resources.Load(resName);
        //    }
        //    return null;
        //}

        /// <summary>
        /// 读取资源
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public object Load(string path)
        {

            if (this.m_mapRes.ContainsKey(path))
            {
                this.m_mapRes[path].Used();
                if (this.m_mapRes[path].GetAssetObject() is AssetBundle)
                {
                    return (this.m_mapRes[path].GetAssetObject() as AssetBundle).mainAsset;
                }
                return this.m_mapRes[path].GetAssetObject();
            }
            else
            {
                Debug.LogError("Resource is null. path " + path);
            }
            return null;
        }

        /// <summary>
        /// 读取资源
        /// </summary>
        /// <param name="path"></param>
        /// <param name="resName"></param>
        /// <returns></returns>
        public object Load(string path, string resName)
        {
#if GAME_TEST_LOAD
            if (path == GAME_DEFINE.RESOURCE_TABLE_PATH)
            {
                return Load(path, RESOURCE_TYPE.PC_TEXT_STR, resName);
            }
#endif
            return Load(path, this.m_eLoadType, resName);
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public object Load(string path, RESOURCE_TYPE resType, string resName)
        {
            if ((int)resType >= (int)RESOURCE_TYPE.WEB_RESOURCES && (int)resType <= (int)RESOURCE_TYPE.WEB_MAX )
            {
                if (this.m_mapRes.ContainsKey(path))
                {
                    this.m_mapRes[path].Used();
                    if (resType == RESOURCE_TYPE.WEB_TEXT_STR)
                    {
                        string obj = (string)this.m_mapRes[path].GetAssetObject();
                        //UnityEngine.Object obj = ((AssetBundle)this.m_mapRes[path].GetAssetObject()).mainAsset;
                        return obj;
                    }
                    else
                    {
                        UnityEngine.Object res = ((AssetBundle)this.m_mapRes[path].GetAssetObject()).Load(resName);
                        //if (res == null)
                        //{
                        //    Debug.LogError("Resource is null. path " + path + " resName " + resName);
                        //}
                        return res;
                    }
                }
                else
                {
                    Debug.LogError("Resource is null. path " + path + " resName " + resName);
                }
            }
            else if ((int)resType >= (int)RESOURCE_TYPE.LOC_RESOURCES && (int)resType <= (int)RESOURCE_TYPE.LOC_MAX)
            {
#if UNITY_EDITOR
                UnityEngine.Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesWWW/" + resName + ".prefab", typeof(UnityEngine.Object));
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesWWW/" + resName + ".png", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesWWW/" + resName + ".jpg", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesWWW/" + resName + ".txt", typeof(UnityEngine.Object));
                }
                //cache
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesCache/" + resName + ".prefab", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesCache/" + resName + ".png", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesCache/" + resName + ".jpg", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesCache/" + resName + ".txt", typeof(UnityEngine.Object));
                }
                //avatarM
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/AvatarM/" + resName + ".prefab", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/AvatarM/" + resName + ".png", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/AvatarM/" + resName + ".jpg", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/AvatarM/" + resName + ".txt", typeof(UnityEngine.Object));
                }
                //item
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/Item/" + resName + ".prefab", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/Item/" + resName + ".png", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/Item/" + resName + ".jpg", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/Item/" + resName + ".txt", typeof(UnityEngine.Object));
                }
                return obj;
#else
                return UnityEngine.Resources.Load(resName);
#endif
            }
            else if ((int)resType >= (int)RESOURCE_TYPE.PC_RESOURCES && (int)resType <= (int)RESOURCE_TYPE.PC_MAX)
            {
                string str = System.IO.File.ReadAllText(Application.dataPath + "/Table/" + resName, System.Text.Encoding.Default);
                return str;
            }
            return null;
        }

        /// <summary>
        /// 卸载资源
        /// </summary>
        /// <param name="name"></param>
        public void UnloadResource(string name)
        {
            if (this.m_mapRes.ContainsKey(name))
            {
                if (!CanUnload(name))
                    return;

                this.m_mapRes[name].Destory( false );
                this.m_lstRequires.Remove(this.m_mapRes[name]);
                this.m_mapRes.Remove(name);
                Resources.UnloadUnusedAssets();
            }
        }

        /// <summary>
        /// 删除资源资源需求
        /// </summary>
        /// <param name="name"></param>
        /// <param name="owner"></param>
        public void UnloadResource(string name, ResourceRequireOwner owner)
        {
            if (this.m_mapRes.ContainsKey(name))
            {
                this.m_mapRes[name].RemoveRequireOwner(owner);
                //倘若资源需求为空
                if (this.m_mapRes[name].IsOwnerEmpty())
                {
                    //如果可以卸载 或者 加载未完成则卸载资源
                    if (CanUnload(name) || !this.m_mapRes[name].Complete)
                    {
                        this.m_mapRes[name].Destory(false);
                        this.m_lstRequires.Remove(this.m_mapRes[name]);
                        this.m_mapRes.Remove(name);
                        Resources.UnloadUnusedAssets();
                    }
                }
            }
            else
            {
                Debug.LogError("Has not resource " + name);
            }
        }

        /// <summary>
        /// 是否可以卸载
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool CanUnload(string name)
        {
            if (name == "Share" || name == "gui_cache" || name.StartsWith("MODEL_") || name.StartsWith("effect_") || name.StartsWith("Tex_") || name.StartsWith("item_"))
                return false;
            return true;
        }

        /// <summary>
        /// 卸载所有不使用资源
        /// </summary>
        public void UnloadUnusedResources()
        {
            List<string> lst = new List<string>();
            foreach (KeyValuePair<string, ResourceRequireData> item in this.m_mapRes)
            {
                //|| Time.fixedTime - item.Value.GetLastUsedTime() <= 10f
                if (!CanUnload(item.Key))
                    continue;

                lst.Add(item.Key);

                //if (item.Value.GetAssetObject() is AssetBundle)
                //{
                //    lst.Add(item.Key);
                //    //(item.Value.GetAssetObject() as AssetBundle).Unload(false);
                //}
            }
            foreach (string key in lst)
            {
                this.m_mapRes[key].Destory( false );
                this.m_mapRes.Remove(key);
            }
            this.m_lstRequires.Clear();
            this.m_mapAsyncLoader.Clear();
            GUI_FUNCTION.DESTORY();
            //Resources.UnloadUnusedAssets();
            //GC.Collect();
        }

        /// <summary>
        /// 装载资源
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        public ResourceRequireOwner LoadResource(string path, string name)
        {

            if (this.m_eLoadType != RESOURCE_TYPE.WEB_OBJECT)
            {
#if UNITY_EDITOR
                UnityEngine.Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesWWW/" + name + ".prefab", typeof(UnityEngine.Object));
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesWWW/" + name + ".png", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesWWW/" + name + ".jpg", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesWWW/" + name + ".txt", typeof(UnityEngine.Object));
                }
                //cache
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesCache/" + name + ".prefab", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesCache/" + name + ".png", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesCache/" + name + ".jpg", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesCache/" + name + ".txt", typeof(UnityEngine.Object));
                }
                //avatarM
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/AvatarM/" + name + ".prefab", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/AvatarM/" + name + ".png", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/AvatarM/" + name + ".jpg", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/AvatarM/" + name + ".txt", typeof(UnityEngine.Object));
                }
                //item
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/Item/" + name + ".prefab", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/Item/" + name + ".png", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/Item/" + name + ".jpg", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/Item/" + name + ".txt", typeof(UnityEngine.Object));
                }
                if (!this.m_mapRes.ContainsKey(name))
                {
                    ResourceRequireData data = new ResourceRequireData(obj);
                    this.m_mapRes.Add(name, data);
                }
                return null;
#endif
            }
            return LoadResouce(path + name + RESOURCE_POST, 0, -1, name , null , RESOURCE_TYPE.WEB_OBJECT, ENCRYPT_TYPE.NORMAL, null);
        }

        /// <summary>
        /// 装载资源
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        public ResourceRequireOwner LoadResource(string path , int version , string name)
        {

            if (this.m_eLoadType != RESOURCE_TYPE.WEB_OBJECT)
            {
#if UNITY_EDITOR
                UnityEngine.Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesWWW/" + name + ".prefab", typeof(UnityEngine.Object));
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesWWW/" + name + ".png", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesWWW/" + name + ".jpg", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesWWW/" + name + ".txt", typeof(UnityEngine.Object));
                }
                //cache
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesCache/" + name + ".prefab", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesCache/" + name + ".png", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesCache/" + name + ".jpg", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesCache/" + name + ".txt", typeof(UnityEngine.Object));
                }
                //avatarM
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/AvatarM/" + name + ".prefab", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/AvatarM/" + name + ".png", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/AvatarM/" + name + ".jpg", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/AvatarM/" + name + ".txt", typeof(UnityEngine.Object));
                }
                //item
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/Item/" + name + ".prefab", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/Item/" + name + ".png", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/Item/" + name + ".jpg", typeof(UnityEngine.Object));
                }
                if (obj == null)
                {
                    obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/ResourcesHeroCache/Item/" + name + ".txt", typeof(UnityEngine.Object));
                }
                if (!this.m_mapRes.ContainsKey(name))
                {
                    ResourceRequireData data = new ResourceRequireData(obj);
                    this.m_mapRes.Add(name, data);
                }
                return null;
#endif
            }
            return LoadResouce(path + name + RESOURCE_POST, 0, version, name, null, RESOURCE_TYPE.WEB_OBJECT, ENCRYPT_TYPE.NORMAL, null);
        }

        /// <summary>
        /// 装载资源
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ResourceRequireOwner LoadResouce(string path, uint crc, int version, string resName , string resValue , RESOURCE_TYPE resType, ENCRYPT_TYPE encrypt_type, DownLoadCallBack func, params object[] arg)
        {
            if (this.m_delDecryptFunc == null)
            {
                // Error
                // 没有资源解密接口
                return null;
            }

            ResourceRequireOwner owner = new ResourceRequireOwner();
            owner.m_cResName = resName;
            owner.m_strResValue = resValue;
            owner.m_delCallBack = func;
            owner.m_eResType = resType;
            owner.m_vecArg = arg;

            if (this.m_mapRes.ContainsKey(resName))
            {
                //增加需求者
                this.m_mapRes[resName].AddRequireOwner(owner);
                this.m_mapRes[resName].Used();
                if (this.m_mapRes[resName].Complete)
                {
                    this.m_mapRes[resName].CompleteCallBack();
                }
            }
            else
            {
                ResourceRequireData rrd = new ResourceRequireData(path , crc , version, resType, encrypt_type, this.m_delDecryptFunc);
                this.m_lstRequires.Add(rrd);
                //Debug.Log(resName + " add load ");
                this.m_mapRes.Add(resName, rrd);
                rrd.AddRequireOwner(owner);
                //rrd.Initialize();
            }

            return owner;
        }

        /// <summary>
        /// 删除所有资源
        /// </summary>
        public void Destory()
        {
            foreach (ResourceRequireData item in this.m_mapRes.Values)
            {
                item.Destory( true );
            }
            this.m_mapRes.Clear();
            this.m_lstRequires.Clear();
            this.m_mapAsyncLoader.Clear();
        }

        /// <summary>
        /// 清楚进度
        /// </summary>
        public void ClearProgress()
        {
            this.m_lstRequires.Clear();
        }

        /// <summary>
        /// 获取进度
        /// </summary>
        /// <returns></returns>
        public float GetProgress()
        {
            float progess = 0;
            foreach (ResourceRequireData item in this.m_lstRequires)
            {
                progess += item.GetProgress();
            }

            if ( this.m_lstRequires.Count > 0)
            {
                return progess / this.m_lstRequires.Count;
            }

            return 1f;
        }

        /// <summary>
        /// 完成加载
        /// </summary>
        /// <returns></returns>
        public bool IsComplete()
        {
            bool finish = true;
            foreach (ResourceRequireData item in this.m_lstRequires)
            {
                if (!item.Complete)
                    finish = false;
            }

            return finish;
        }

        /// <summary>
        /// 逻辑更新
        /// </summary>
        public void Update()
        {
            int sum = 0;
            foreach (ResourceRequireData item in this.m_lstRequires)
            {
                if (item.Start && !item.Complete)
                    sum++;
            }

            if (sum < LOAD_MAX_NUM)
            {
                sum = 0;
                foreach (ResourceRequireData item in this.m_lstRequires)
                {
                    if (!item.Start && !item.Complete)
                    {
                        item.Initialize();
                        sum++;
                        if (sum >= LOAD_MAX_NUM)
                            break;
                    }
                }
            }
        }

    }


    /// <summary>
    /// 资源需求数据
    /// </summary>
    public class ResourceRequireData
    {
        private string m_strFilePath;       //资源地址
        private uint m_iCRC; //CRC码
        private int m_iVersion; //资源版本
        private float m_fLastUseTime;   //最近使用时间
        private RESOURCE_TYPE m_eResType = RESOURCE_TYPE.MAX;   //资源类型
        private ENCRYPT_TYPE m_eEncryType = ENCRYPT_TYPE.NORMAL;   //加密类型
        private List<ResourceRequireOwner> m_lstOwners = new List<ResourceRequireOwner>();    //需求者

        private object m_cAsset = null;       //资源包
        private LoadPackage m_cLoader = null;      //加载器

        private DecryptBytesFunc m_funDecryptFunc;  //加密解密接口
        private bool m_bComplete = false;   //是否完成
        private bool m_bStart = false;  //是否开始
        public bool Start
        {
            get { return this.m_bStart; }
        }

        public ResourceRequireData( object asset )
        {
            this.m_cAsset = asset;
            this.m_bComplete = true;
            this.m_bStart = true;
            this.m_fLastUseTime = Time.fixedTime;
        }

        public ResourceRequireData(string path, uint crc, int version, RESOURCE_TYPE type, ENCRYPT_TYPE encrypt_type, DecryptBytesFunc decryptFunc)
        {
            this.m_strFilePath = path;
            this.m_iCRC = crc;
            this.m_iVersion = version;
            this.m_eResType = type;
            this.m_eEncryType = encrypt_type;
            this.m_funDecryptFunc = decryptFunc;
            this.m_fLastUseTime = Time.fixedTime;
        }

        /// <summary>
        /// 获取最近使用时间
        /// </summary>
        /// <returns></returns>
        public float GetLastUsedTime()
        {
            return this.m_fLastUseTime;
        }

        /// <summary>
        /// 被使用接口
        /// </summary>
        public void Used()
        {
            this.m_fLastUseTime = Time.fixedTime;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            this.m_bStart = true;

            if (((int)this.m_eResType) >= ((int)RESOURCE_TYPE.WEB_RESOURCES) && ((int)this.m_eResType) <= ((int)RESOURCE_TYPE.WEB_MAX))
            {
                //GameObject gameObject = new GameObject("Loader");
                this.m_cLoader = LoadPackage.StartWWW(this.m_strFilePath, this.m_iCRC, this.m_iVersion, LoaderCallBack, this.m_eResType, this.m_eEncryType, this.m_funDecryptFunc);
                //this.m_cLoader.Init(this.m_strFilePath, this.m_iCRC, this.m_iVersion, LoaderCallBack, this.m_eResType, this.m_eEncryType, this.m_funDecryptFunc);
                //this.m_cLoader.BeginLoad();
                return;
            }

            if (((int)this.m_eResType) >= ((int)RESOURCE_TYPE.PC_RESOURCES) && ((int)this.m_eResType) <= ((int)RESOURCE_TYPE.PC_MAX))
            {
                LoaderCallBack(this.m_strFilePath, Resources.Load(this.m_strFilePath));
                return;
            }

            if (((int)this.m_eResType) >= ((int)RESOURCE_TYPE.LOC_RESOURCES) && ((int)this.m_eResType) <= ((int)RESOURCE_TYPE.LOC_MAX))
            {
                AssetBundle asset = AssetBundle.CreateFromFile(this.m_strFilePath);
                LoaderCallBack(this.m_strFilePath, asset);
                return;
            }
        }

        /// <summary>
        /// 获取资源物体
        /// </summary>
        /// <returns></returns>
        public object GetAssetObject()
        {
            return this.m_cAsset;
        }

        /// <summary>
        /// 加载器回调
        /// </summary>
        /// <param name="path"></param>
        /// <param name="www"></param>
        private void LoaderCallBack(string path, object asset)
        {
            //资源
            this.m_cAsset = asset;
            //回调
            CompleteCallBack();

            this.m_bComplete = true;

            //删除加载器
            if (this.m_cLoader != null)
                GameObject.Destroy(this.m_cLoader.gameObject);
            this.m_cLoader = null;
        }

        /// <summary>
        /// 是否完成加载
        /// </summary>
        public bool Complete
        {
            get
            {
                return this.m_bComplete;
            }
        }

        /// <summary>
        /// 完成回调
        /// </summary>
        public void CompleteCallBack()
        {
            //为了没有死循环，先把所有请求者存为另外的列表
            //将源请求者清除
            List<ResourceRequireOwner> lst = new List<ResourceRequireOwner>(this.m_lstOwners);
            this.m_lstOwners.Clear();
            foreach (ResourceRequireOwner item in lst)
            {
                if (this.m_cAsset is AssetBundle)
                {
                    UnityEngine.Object obj = null;
                    if (string.IsNullOrEmpty(item.m_strResValue))
                    {
                        obj = ((AssetBundle)this.m_cAsset).mainAsset;
                    }
                    else
                    {
                        obj = ((AssetBundle)this.m_cAsset).Load(item.m_strResValue);
                    }
                    if (item.m_delCallBack != null)
                    {
                        item.m_delCallBack(item.m_cResName, obj, item.m_vecArg);
                    }
                }
                else
                {
                    if (item.m_delCallBack != null)
                    {
                        item.m_delCallBack(item.m_cResName, this.m_cAsset, item.m_vecArg);
                    }
                }
            }
        }

        /// <summary>
        /// 增加资源需求者
        /// </summary>
        /// <param name="owner"></param>
        public bool AddRequireOwner(ResourceRequireOwner owner)
        {
            //foreach (ResourceRequireOwner item in this.m_lstOwners)
            //{
            //    if (owner.m_cResName.CompareTo(item.m_cResName) == 0)
            //    {
            //        return false;
            //    }
            //}
            this.m_lstOwners.Add(owner);
            return true;
        }

        /// <summary>
        /// 删除需求者
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public bool RemoveRequireOwner(ResourceRequireOwner owner)
        {
            return this.m_lstOwners.Remove(owner);
        }

        /// <summary>
        /// 是否拥有者为空
        /// </summary>
        /// <returns></returns>
        public bool IsOwnerEmpty()
        {
            return this.m_lstOwners.Count <= 0;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Destory( bool force )
        {
            if (this.m_cLoader != null)
            {
                this.m_cLoader.StopAllCoroutines();
                UnityEngine.Object.Destroy(this.m_cLoader.gameObject);
                UnityEngine.Object.Destroy(this.m_cLoader);
                this.m_cLoader = null;
            }
            if (this.m_cAsset != null && this.m_cAsset is AssetBundle)
            {
                ((AssetBundle)this.m_cAsset).Unload(force);
                this.m_cAsset = null;
            }
        }

        /// <summary>
        /// 停止加载
        /// </summary>
        public void StopLoader()
        {
            if (this.m_cLoader != null)
            {
                this.m_cLoader.StopAllCoroutines();
                UnityEngine.Object.Destroy(this.m_cLoader.gameObject);
                UnityEngine.Object.Destroy(this.m_cLoader);
                this.m_cLoader = null;
            }
        }

        /// <summary>
        /// 获取加载进度
        /// </summary>
        /// <returns></returns>
        public float GetProgress()
        {
            if (this.m_cLoader != null)
            {
                return this.m_cLoader.Progess;
            }
            if (this.m_cAsset != null)
            {
                return 1f;
            }
            if (this.m_bComplete)
                return 1f;

            return 0f;
        }
    }

}