using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    Camera camera;
    public Camera Camera
    {
        get 
        {
            if(camera == null)
            {
                camera = Camera.main;
            }

            return camera;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    public void LerpMoveCamera(Vector2 targetPos, Bounds clampBounds, float speed)
    {
        Vector2 smoothPosition = Vector2.Lerp(Camera.transform.position, targetPos, speed * Time.deltaTime);

        float campX = Mathf.Clamp(smoothPosition.x, clampBounds.min.x, clampBounds.max.x);
        float campY = Mathf.Clamp(smoothPosition.y, clampBounds.min.y, clampBounds.max.y);

        Camera.transform.position = new Vector3(campX, campY, Camera.transform.position.z);
    }
}
