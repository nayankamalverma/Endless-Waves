using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utilities.VFX
{
	public class VFXService : MonoBehaviour
	{
		[SerializeField]
		private List<VFXItem> VFXList;

		private static VFXService instance;
		public static VFXService Instance { get { return instance; } }

		private BloodVFXPool bloodVFXPool;

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
			CreateVFXPool();
		}

		private void CreateVFXPool()
		{
			bloodVFXPool = new BloodVFXPool(VFXList[(int)VFXType.Blood]);
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
