using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Factory
{
    public class PoolFactory : Transformable
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private List<PoolObject> _pool = new List<PoolObject>();

        public void DisableObject(PoolObject objectToRemove)
        {
            objectToRemove.gameObject.SetActive(false);
            objectToRemove.Transform.SetParent(Transform);
        }
        
        public T GetNewObject<T>(Vector3 position, Transform parent = null)
        {
            PoolObject newObject;
            foreach (var poolObject in _pool.Where(poolObject => ! poolObject.gameObject.activeSelf))
            {
                newObject = poolObject;
                PrepareObject(newObject, position, parent);
                return newObject.GetComponent<T>();
            }
            newObject = InstantiatePrefab(position, parent);
            newObject.OnPoolEnable();
            return newObject.GetComponent<T>();
        }

        private PoolObject InstantiatePrefab(Vector3 position, Transform parent)
        {
            PoolObject newObject = Instantiate(_prefab, position, Quaternion.identity, parent).GetComponent<PoolObject>();
            PrepareObject(newObject, position, parent);
            newObject.InitPool(this);
            _pool.Add(newObject);
            return newObject;
        }

        private static void PrepareObject(PoolObject newObject, Vector3 position, Transform parent)
        {
            newObject.gameObject.SetActive(true);
            newObject.Transform.position = position;
            newObject.Transform.SetParent(parent);
            newObject.OnPoolEnable();
        }

        private void OnValidate()
        {
            if (_prefab == null || _prefab.GetComponent<PoolObject>() != null)
                return;
            _prefab = null;
            throw new NullReferenceException($"PoolFactory prefab must contain PoolObject component.");
        }
    }
}