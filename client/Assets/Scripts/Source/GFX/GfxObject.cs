using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;

//  GfxObject.cs
//  Author: Lu Zexi
//  2013-11-21



namespace Game.Gfx
{
    /// <summary>
    /// 图形渲染类
    /// </summary>
    public class GfxObject
    {
        protected GameObject m_cGameObj = null; //实体
        protected CAnimation m_cAnimation = null;   //动画类
        protected CTransform m_cTrans = null;   //坐标转换类
        protected StateControl m_cStateControl = null; //状态控制类

        public GfxObject(GameObject obj)
        {
            this.m_cGameObj = obj;
            this.m_cTrans = new CTransform(this.m_cGameObj.transform);
            this.m_cAnimation = new CAnimation(this.m_cGameObj.GetComponent<Animation>() , GAME_TIME.TIME_FIXED);
            this.m_cStateControl = new StateControl(this);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Initialize()
        {
            this.m_cStateControl.Idle();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public virtual void Destory()
        {
            if (this.m_cAnimation != null)
                this.m_cAnimation.Destory();

            this.m_cAnimation = null;

            if (this.m_cTrans != null)
                this.m_cTrans.Destory();

            this.m_cTrans = null;

            GameObject.Destroy(this.m_cGameObj);
            this.m_cGameObj = null;
        }

        /// <summary>
        /// 渲染更新
        /// </summary>
        public virtual void Render()
        { 
            //
        }

        /// <summary>
        /// 逻辑更新
        /// </summary>
        public virtual void Update()
        {
            this.m_cStateControl.Update();
        }

        /// <summary>
        /// 增加脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddComponent<T>() where T : MonoBehaviour
        {
            return this.m_cGameObj.AddComponent<T>();
        }

        /// <summary>
        /// 销毁脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T RemoveComponent<T>( T t) where T : MonoBehaviour
        {
            GameObject.Destroy(t);
            return t;
        }

        /// <summary>
        /// 获取脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T:MonoBehaviour
        {
            return this.m_cGameObj.GetComponent<T>();
        }

        ////////////////////////////////////////// 状态控制 ////////////////////////////////////

        /// <summary>
        /// 获取状态控制
        /// </summary>
        /// <returns></returns>
        public StateControl GetStateControl()
        {
            return this.m_cStateControl;
        }

        /// <summary>
        /// 攻击状态
        /// </summary>
        public void AttackState()
        {
            this.m_cStateControl.Attack();
        }

        /// <summary>
        /// 空闲状态
        /// </summary>
        public void IdleState()
        {
            this.m_cStateControl.Idle();
        }

        /// <summary>
        /// 移动状态
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="costTime"></param>
        public void MoveState( Vector3 pos , float costTime )
        {
            this.m_cStateControl.Move(pos, costTime);
        }

        /// <summary>
        /// 受伤状态
        /// </summary>
        public void HurtState()
        {
            this.m_cStateControl.Hurt();
        }

        /// <summary>
        /// 技能状态
        /// </summary>
        public void SkillState()
        {
            this.m_cStateControl.Skill();
        }

        //////////////////////////////////////////////////  坐标朝向转换 API ///////////////////////////////

        /// <summary>
        /// 设置可见性
        /// </summary>
        /// <param name="setting"></param>
        public virtual void SetVisible(bool setting)
        {
            if (this.m_cTrans == null)
                return;
            this.m_cTrans.active = setting;
        }

        /// <summary>
        /// 设置位置
        /// </summary>
        /// <param name="pos"></param>
        public virtual void SetPos(Vector3 pos)
        {
            this.m_cTrans.position = pos;
        }

        /// <summary>
        /// 获取位置
        /// </summary>
        /// <returns></returns>
        public virtual Vector3 GetPos()
        {
            return this.m_cTrans.position;
        }

        /// <summary>
        /// 设置相对位置
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public virtual void SetLocalPos(Vector3 pos)
        {
            this.m_cTrans.localPosition = pos;
        }

        /// <summary>
        /// 获取相对位置
        /// </summary>
        /// <returns></returns>
        public virtual Vector3 GetLocalPos()
        {
            return this.m_cTrans.localPosition;
        }

        /// <summary>
        /// 设置朝向
        /// </summary>
        /// <param name="rot"></param>
        public virtual void SetRot(Quaternion rot)
        {
            this.m_cTrans.rotation = rot;
        }

        /// <summary>
        /// 获取朝向
        /// </summary>
        /// <returns></returns>
        public virtual Quaternion GetRot()
        {
            return this.m_cTrans.rotation;
        }

        /// <summary>
        /// 设置相对朝向
        /// </summary>
        /// <param name="rot"></param>
        public virtual void SetLocalRot(Quaternion rot)
        {
            this.m_cTrans.localRotation = rot;
        }

        /// <summary>
        /// 获取相对朝向
        /// </summary>
        public virtual Quaternion GetLocalRot()
        {
            return this.m_cTrans.localRotation;
        }

        /// <summary>
        /// 获取相对缩放比率
        /// </summary>
        /// <returns></returns>
        public virtual Vector3 GetLocalScale()
        {
            return this.m_cTrans.localScale;
        }

        /// <summary>
        /// 设置相对缩放比率
        /// </summary>
        /// <param name="scale"></param>
        public virtual void SetLocalScale(Vector3 scale)
        {
            this.m_cTrans.localScale = scale;
        }

        /// <summary>
        /// 设置旋转
        /// </summary>
        /// <param name="qua"></param>
        public virtual void SetRotation(Quaternion qua)
        {
            this.m_cTrans.rotation = qua;
        }

        /// <summary>
        /// 设置本地旋转
        /// </summary>
        /// <param name="qua"></param>
        public virtual void SetLocalRotation(Quaternion qua)
        {
            this.m_cTrans.localRotation = qua;
        }

        /// <summary>
        /// Z方向
        /// </summary>
        public Vector3 forward
        {
            get { return this.m_cTrans.forward; }
        }

        /// <summary>
        /// Y方向
        /// </summary>
        public Vector3 up
        {
            get { return this.m_cTrans.up; }
        }

        /// <summary>
        /// X方向
        /// </summary>
        public Vector3 right
        {
            get { return this.m_cTrans.right; }
        }

        /// <summary>
        /// 旋转实体
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Rotate(float x, float y, float z)
        {
            if (this.m_cTrans == null)
                return;
            this.m_cTrans.Rotate(x, y, z);
        }

        /// <summary>
        /// 旋转实体
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Rotate(Vector3 rot)
        {
            if (this.m_cTrans == null)
                return;
            this.m_cTrans.Rotate(rot);
        }

        /// <summary>
        /// 旋转
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        public void RotateAround(Vector3 pos, Vector3 axis, float angle)
        {
            if (this.m_cTrans == null)
                return;
            this.m_cTrans.RotateAround(pos, axis, angle);
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="dir"></param>
        public void Translate(Vector3 dir)
        {
            if (this.m_cTrans == null)
                return;
            this.m_cTrans.Translate(dir);
        }

        /// <summary>
        /// 朝向某点
        /// </summary>
        /// <param name="pos"></param>
        public void LookAt(Vector3 pos)
        {
            if (this.m_cTrans == null)
                return;
            this.m_cTrans.LookAt(pos);
        }

        /// <summary>
        /// 获取父节点
        /// </summary>
        /// <returns></returns>
        public Transform GetParent()
        {
            return this.m_cGameObj.transform.parent;
        }

        /// <summary>
        /// 设置父节点
        /// </summary>
        /// <param name="tar"></param>
        public void SetParent(Transform tar)
        {
            this.m_cTrans.Parent = tar;
        }

        /// <summary>
        /// 查找子对象
        /// </summary>
        /// <param name="childName"></param>
        /// <returns></returns>
        public GfxObject FindChild(string childName)
        {
            if (this.m_cGameObj == null || childName == "")
            {
                return this;
            }

            GfxObject obj = null;
            foreach (Transform item in this.m_cGameObj.transform)
            {
                if (item.name == childName)
                {
                    obj = new GfxObject(item.gameObject);
                    break;
                }
            }
            return obj;
        }
        ////////////////////////////////////////////////////////

        ////////////////////////////////////////////  动画 API /////////////////////////////////////////////

        /// <summary>
        /// 停止动画
        /// </summary>
        public void Stop()
        {
            if (this.m_cAnimation == null)
                return;

            this.m_cAnimation.Stop();
        }

        /// <summary>
        /// 播放动作
        /// </summary>
        /// <param name="name"></param>
        /// <param name="wrap"></param>
        /// <param name="speed"></param>
        /// <param name="mode"></param>
        public virtual void Play(string name, WrapMode wrap, float speed, PLAY_MODE mode)
        {
            if (this.m_cAnimation == null)
                return;

            switch (mode)
            {
                case PLAY_MODE.CROSS_FADE:
                    this.m_cAnimation.CrossFade(name, wrap, speed);
                    break;
                case PLAY_MODE.PLAY:
                    this.m_cAnimation.Play(name, wrap, speed);
                    break;
                case PLAY_MODE.BLEND:
                    this.m_cAnimation.Blend(name, wrap, speed);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 是否正在播放指定动作
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual bool IsPlay(string name)
        {
            if (this.m_cAnimation == null)
                return false;
            return this.m_cAnimation.IsPlay(name);
        }

        
        /// <summary>
        /// 获取长度
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public float GetAnimationLength(string name)
        {
            if (this.m_cAnimation == null)
                return 0;
            return this.m_cAnimation.GeLength(name);
        }

        ///// <summary>
        ///// 获取GameObject
        ///// </summary>
        ///// <returns></returns>
        //public GameObject GetGameObject()
        //{
        //    return this.m_cGameObj;
        //}

        /////////////////////////////////////////////////////////////////////////////////////////////////////

    }

}


