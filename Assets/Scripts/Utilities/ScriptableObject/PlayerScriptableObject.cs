using Assets.Scripts.player;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "Scriptable Objects/PlayerSO")]
public class PlayerScriptableObject : ScriptableObject
{
    public float moveSpeed = 10f;
    public int maxHealth = 100;
}
