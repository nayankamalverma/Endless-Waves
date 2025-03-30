using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Utilities.VFX
{
    public class BloodVFXPool : BaseObjectPool
    {
        private VFXItem blood;

        public BloodVFXPool(VFXItem item)
        {
            blood = item;
        }

        public void GetBloodVFX(Vector3 pos)
        {
            GameObject bloodVFX = GetItem();
            bloodVFX.GetComponent<ParticleSystem>().Play();
            bloodVFX.transform.position = pos;
            CoroutineRunner.Instance.RunCoroutine(ReturnBloodVFX(bloodVFX));
            //remove coroutine and make it return to pool when vfx is done playing
        }

        private IEnumerator ReturnBloodVFX(GameObject bloodVfx)
        {
            yield return new WaitForSeconds(2f);
            ReturnItem(pooledItems.Find(i => i.item == bloodVfx));
        }

        protected override GameObject CreateItem()
        {
            return Object.Instantiate(blood.item, blood.parentTransform);
        }
    }
}