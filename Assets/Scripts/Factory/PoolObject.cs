﻿using System;

namespace Factory
{
    public class PoolObject : Transformable
    {
        private PoolFactory _factory;

        public event Action<PoolObject> PoolDisabled;
        public event Action<PoolObject> PoolEnabled;

        public void InitPool(PoolFactory factory)
        {
            _factory = factory;
        }

        public virtual void OnPoolEnable()
        {
            PoolEnabled?.Invoke(this);
        }

        public virtual void PoolDisable()
        {
            PoolDisabled?.Invoke(this);
            if (_factory != null)
                _factory.DisableObject(this);
            else
                Destroy(gameObject);
        }
    }
}