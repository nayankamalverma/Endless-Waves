using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utilities.VFX
{
	public class VFXService : GenericMonoSingleton<VFXService>
	{
		[SerializeField]
		private List<VFXItem> vfxList;

		private BloodVFXPool bloodVFXPool;

		protected override void Awake()
		{
			base.Awake();
            CreateVFXPool();
		}

		private void CreateVFXPool()
		{
			bloodVFXPool = new BloodVFXPool(vfxList[(int)VFXType.Blood]);
		}

		public void PlayVFX(VFXType vfxType, Vector3 position)
		{
			switch (vfxType)
			{
				case VFXType.Blood:
					bloodVFXPool.GetBloodVFX(position);
					break;
				default:
					Debug.LogError("VFX type not found");
					break;
			}
		}
	}

	[Serializable]
	public struct VFXItem
	{
		public GameObject item;
		public Transform parentTransform;
		public VFXType type;
	}

	public enum VFXType
	{
		Blood,
		Explosion,
		Fire,
		Smoke
	}
}
