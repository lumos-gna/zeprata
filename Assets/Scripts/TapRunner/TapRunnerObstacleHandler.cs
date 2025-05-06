using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TapRunnerObstacleHandler : MonoBehaviour, ICollisionEventable
{
    [SerializeField] Transform topObstaclesParent;
    [SerializeField] Transform bottomObstaclesParent;


    public void MoveHandler(Vector2 endPos, Vector2 startPos, float moveSpeed, out bool isLoop)
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        if (endPos.x >= transform.localPosition.x)
        {
            transform.localPosition = startPos;

            isLoop = true;

            return;
        }

        isLoop = false;
    }

    public void InitChildObstacles(int needCount)
    {
        DisableObstacles(topObstaclesParent);
        DisableObstacles(bottomObstaclesParent);

        while (true)
        {
            int topRandCount = Random.Range(0, 6);
            int bottomRandCount = Random.Range(0, 6);

            int totalRandCount = topRandCount + bottomRandCount;

            if (totalRandCount == needCount)
            {
                EnableObstacles(topRandCount, topObstaclesParent);
                EnableObstacles(bottomRandCount, bottomObstaclesParent);
               
                break;
            }
        }
    }

    void DisableObstacles(Transform obstaclesParent)
    {
        for (int i = 0; i < obstaclesParent.childCount; i++)
        {
            obstaclesParent.GetChild(i).gameObject.SetActive(false);
        }
    }

    void EnableObstacles(int enableCount, Transform obstaclesParent)
    {
        for (int i = 0; i < enableCount; i++)
        {
            obstaclesParent.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void OnCollisionEntered(GameObject source) { }
    public void OnCollisionExited(GameObject source) { }
   
}
