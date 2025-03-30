using System.Collections.Generic;
using Assets.Scripts.UI.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyObjectPool
    {
        private Transform enemyParent;
        private EnemyService enemyService;
        public List<PooledItem> pooledItems = new List<PooledItem>();

        public EnemyObjectPool(EnemyService enemyService, Transform enemyParent)
        {
            this.enemyService = enemyService;
            this.enemyParent = enemyParent;
        }

        public EnemyController GetEnemy(EnemyType enemyType)
        {
            if (pooledItems.Count > 0)
            {
                PooledItem item = pooledItems.Find(i => i.enemy.GetEnemyType() == enemyType && !i.isUsed);
                if (item != null)
                {
                    item.isUsed = true;
                    item.enemy.gameObject.SetActive(true);
                    item.enemy.enabled = true;
                    return item.enemy;
                }
            }

            return CreateNewPooledItem(enemyType);
        }

        private EnemyController CreateNewPooledItem(EnemyType enemyType)
        {
            PooledItem item = new PooledItem();
            item.enemy = CreateItem(enemyType);
            item.isUsed = true;
            pooledItems.Add(item);
            return item.enemy;
        }

        protected EnemyController CreateItem(EnemyType enemyType)
        {
            EnemyScriptableObjects enemySO = enemyService.enemyList.Find(i => i.enemyType == enemyType);
            EnemyController enemy = Object.Instantiate<EnemyController>(enemySO.enemy, enemyParent);
            enemy.SetReferences(enemyService, enemySO, enemyService.playerTransform);
            return enemy;
        }


        public virtual void ReturnItem(EnemyController item)
        {
            PooledItem pooledItem = pooledItems.Find(i => i.enemy == item);
            pooledItem.enemy.gameObject.SetActive(false);
            pooledItem.isUsed = false;
        }

        public void ReturnAllItem()
        {
            foreach (var item in pooledItems)
            {
                item.enemy.gameObject.SetActive(false);
                item.isUsed = false;
            }
        }

        public class PooledItem
        {
            public EnemyController enemy;
            public bool isUsed;
        }
    }
}