using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    private void LateUpdate()
    {
        Vector3 playerPosition = GameManager.Instance.GetPlayerTransform().position;
        playerPosition.z = -10f;
        _camera.position = playerPosition;
    }
}
