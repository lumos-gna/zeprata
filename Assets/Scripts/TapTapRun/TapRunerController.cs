using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TapRunnerController : MonoBehaviour
{
    bool isStart;
    bool isEndable;
    float runningTime;
    float currentSpeed;


    [SerializeField] int obstacleNeedCount;

    [SerializeField] float maxSpeed;
    [SerializeField] float startSpeed;

    [SerializeField] Vector2 obstaclesStartPos;
    [SerializeField] Vector2 obstaclesEndPos;

    [SerializeField] TapRunnerObstacleHandler[] obstacleHandlers;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI guideText;

    [SerializeField] TapRunner runner;

    public bool IsStart => isStart;
    public bool IsEndable => isEndable;

    private void Start()
    {
        runner.ReadyToStart();

        guideText.text = "Press To Start!";


        timerText.enabled = false;

        guideText.enabled = true;
    }


    private void Update()
    {
        if(isStart)
        {
            runningTime += Time.deltaTime;
            timerText.text = $"{(int)runningTime}";

            float moveSpeed = currentSpeed < maxSpeed ? currentSpeed += Time.deltaTime : maxSpeed;

            for (int i = 0; i < obstacleHandlers.Length; i++)
            {
                obstacleHandlers[i].MoveHandler(obstaclesEndPos, obstaclesStartPos, moveSpeed, out bool isLoop);

                if (isLoop)
                {
                    obstacleHandlers[i].InitChildObstacles(obstacleNeedCount);
                }
            }
        }
    }


    public void StartGame()
    {
        isStart = true;

        runningTime = 0;

        currentSpeed = startSpeed;

        for (int i = 0; i < obstacleHandlers.Length; i++)
        {
            obstacleHandlers[i].Transform.localPosition = obstaclesStartPos * i;

            obstacleHandlers[i].InitChildObstacles(obstacleNeedCount);
        }

        timerText.enabled = true;

        guideText.enabled = false;
    }

    public void EndGame()
    {
        Debug.Log("End Game");
    }


    public void GameOver()
    {
        isStart = false;

        timerText.enabled = false;

        guideText.enabled = true;

        guideText.text = "Game Over..";


        isEndable = true;

    }
}
