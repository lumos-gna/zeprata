using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class MainCamera : MonoBehaviour
{
    private Transform transform;

    [SerializeField] private float camSpeed; 

    [SerializeField] private Bounds camBounds;
    [SerializeField] private Player player;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        Vector2 smoothPosition = Vector2.Lerp(transform.position, player.transform.position, camSpeed * Time.deltaTime);

        float campX = Mathf.Clamp(smoothPosition.x, camBounds.min.x, camBounds.max.x);
        float campY = Mathf.Clamp(smoothPosition.y, camBounds.min.y, camBounds.max.y);

        transform.position = new Vector3(campX, campY, transform.position.z);
    }
}
