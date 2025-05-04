using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCameraController : MonoBehaviour
{
    [SerializeField] Camera cam;
   

    public void LerpMoveCamera(Vector2 targetPos, Bounds clampBounds, float speed)
    {
        Vector2 smoothPosition = Vector2.Lerp(cam.transform.position, targetPos, speed * Time.deltaTime);

        float campX = Mathf.Clamp(smoothPosition.x, clampBounds.min.x, clampBounds.max.x);
        float campY = Mathf.Clamp(smoothPosition.y, clampBounds.min.y, clampBounds.max.y);

        cam.transform.position = new Vector3(campX, campY, cam.transform.position.z);
    }
}
