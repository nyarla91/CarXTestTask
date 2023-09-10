using System;

namespace Factory
{
    public class PoolObject : Transformable
    {
        private PoolFactory _factory;

        public void InitPool(PoolFactory factory)
        {
            _factory = factory;
        }

        public virtual void OnPoolEnable() { }

        public virtual void PoolDisable()
        {
            if (_factory != null)
                _factory.DisableObject(this);
            else
                Destroy(gameObject);
        }
    }
}