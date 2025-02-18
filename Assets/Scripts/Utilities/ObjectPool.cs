using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class ObjectPool
    {
        public List<PooledItem> pooledItems = new List<PooledItem>();

        protected virtual GameObject GetItem()
        {
            if (pooledItems.Count > 0)
            {
                PooledItem item = pooledItems.Find(i => !i.isUsed);
                if (item != null)
                {
                    item.isUsed = true;
                    item.item.SetActive(true);
                    return item.item;
                }
            }
            return CreateNewPooledItem();
        }

        private GameObject CreateNewPooledItem()
        {
            PooledItem item = new PooledItem();
            item.item = CreateItem();
            item.isUsed = true;
            pooledItems.Add(item);
            return item.item;
        }

        protected virtual GameObject CreateItem()
        {
            throw new NotImplementedException("CreateItem() method not implemented in derived class");
        }

        protected virtual void ReturnItem(PooledItem item)
        {
            item.item.SetActive(false);
            item.isUsed = false;
        }

        public void ReturnAllItem()
        {
            foreach (var item in pooledItems)
            {
                item.item.SetActive(false);
                item.isUsed = false;
            }
        }

    }
    public class PooledItem
    {
        public GameObject item;
        public bool isUsed;
    }
}