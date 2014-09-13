

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;

//  ResourcesLoadManager.cs
//  Lu Zexi
//  2012-5-31


//////////////////////////// 资源加载系统 改进后 不再需要此类 2012-9-17 Lu Zexi
//namespace Game.Resource
//{


//    /// <summary>
//    /// 资源加载管理类
//    /// </summary>
//    public class ResourcesLoadManager : Singleton<ResourcesLoadManager>
//    {

//        private List<AsyncLoader> m_lstResLoads = new List<AsyncLoader>();  //加载器集合

//        public ResourcesLoadManager()
//        {
//        }

//        /// <summary>
//        /// 逻辑更新
//        /// </summary>
//        /// <returns></returns>
//        public bool Update()
//        {
//            foreach (AsyncLoader item in this.m_lstResLoads)
//            {
//                if (!item.Update())
//                {
//                    //do something
//                }
//            }
//            return true;
//        }


//        /// <summary>
//        /// 添加需求
//        /// </summary>
//        /// <param name="resLoad"></param>
//        public void AddRequire(AsyncLoader resLoad)
//        {
//            this.m_lstResLoads.Add(resLoad);
//        }


//        /// <summary>
//        /// 清除需求
//        /// </summary>
//        public void ClearRequire()
//        {
//            foreach (AsyncLoader item in this.m_lstResLoads)
//            {
//                item.Destory();
//            }
//            this.m_lstResLoads.Clear();
//        }


//        /// <summary>
//        /// 获取进度
//        /// </summary>
//        /// <returns></returns>
//        public float GetProgess()
//        {

//            float sum = 0;
//            foreach (AsyncLoader item in this.m_lstResLoads)
//            {
//                sum += item.GetProgess();
//            }
//            //Debug.Log(sum);
//            return sum;
//        }

//    }


//}