using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObjects", menuName = "Scriptable Objects/EnemyScriptableObjects")]
public class EnemyScriptableObjects : ScriptableObject
{
    public EnemyController enemy;
    public EnemyType enemyType;
    public float speed = 2.0f;
    public int enemyHeath = 2;
    public int damage = 10;
    public float attackCooldown = 1.0f;
    public GameObject blood;
    public float stoppingDistance = 1.0f; // Distance to stop from the player
}

public enum EnemyType
{
    pingu,
    jhingu,
}