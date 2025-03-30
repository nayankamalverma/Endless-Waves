using UnityEngine;

namespace Assets.Scripts.UI.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "Scriptable Objects/PlayerSO")]
    public class PlayerScriptableObject : ScriptableObject
    {
        public float moveSpeed = 10f;
        public int maxHealth = 100;
    }
}