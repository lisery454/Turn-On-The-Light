using System;
using UnityEngine;

namespace Moss
{
    /// <summary>
    /// 场景上下文
    /// </summary>
    [DefaultExecutionOrder(-50)]
    public abstract class SceneContext : MonoBehaviour, IContext
    {
        public Container Container { get; private set; }


        private void Awake()
        {
            var startTime = DateTime.Now;

            // 设置自己为当前的上下文对象
            Game.Instance.Context = this;
            // 根据配置初始化容器
            Container = new Container();
            Init(Container);

            var endTime = DateTime.Now;
            print($"初始化容器使用时间: {(endTime - startTime).TotalMilliseconds} ms");


            Container.Awake();
        }

        /// <summary>
        /// 用来注册容器
        /// </summary>
        /// <param name="container"></param>
        protected abstract void Init(ISetAble container);


        public void Update()
        {
            Container.Update();
        }

        public void FixedUpdate()
        {
            Container.FixedUpdate();
        }


        public void Start()
        {
            Container.Start();
        }

        public void LateUpdate()
        {
            Container.LateUpdate();
        }

        public void OnDestroy()
        {
            Container.OnDestroy();
        }
    }
}