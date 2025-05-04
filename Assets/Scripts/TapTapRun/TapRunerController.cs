using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TapRunnerController : MonoBehaviour
{
    bool isPlay;
    bool isEnd;
    float runningTime;
    float currentSpeed;

    //InputManager inputManager;


    [SerializeField] int obstacleNeedCount;

    [SerializeField] float maxSpeed;
    [SerializeField] float startSpeed;

    [SerializeField] Vector2 obstaclesStartPos;
    [SerializeField] Vector2 obstaclesEndPos;

    [SerializeField] TapRunnerObstacleHandler[] obstacleHandlers;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI guideText;

    [SerializeField] TapRunner runner;


    private void Start()
    {
        guideText.text = "Press To Start!";

        timerText.enabled = false;

        guideText.enabled = true;


      /*  inputManager = InputManager.Instance;

        inputManager.OnTapRunnerJumpEvent = () =>
        {
            if (isEnd)
            {
                EndGame();
            }
            else
            {
                if (!isPlay)
                {
                    StartGame();

                    runner.StartPlay();

                }
                else
                {
                    runner.Jump();
                }
            }
        };*/

        StartCoroutine(InputBlockDelay());
    }


    private void Update()
    {
        if (isPlay)
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


    void StartGame()
    {
        isPlay = true;

        runningTime = 0;

        currentSpeed = startSpeed;

        for (int i = 0; i < obstacleHandlers.Length; i++)
        {
            obstacleHandlers[i].transform.localPosition = obstaclesStartPos * i;

            obstacleHandlers[i].InitChildObstacles(obstacleNeedCount);
        }

        timerText.enabled = true;

        guideText.enabled = false;
    }

    void EndGame()
    {
        SceneManager.LoadScene("MainScene");

    }


    IEnumerator InputBlockDelay()
    {
        //inputManager.SwitchInputType(GameEnum.InputType.None);

        yield return new WaitForSeconds(0.33f);

        //inputManager.SwitchInputType(GameEnum.InputType.TapRunner);
    }



    public void GameOver()
    {
        isPlay = false;
        isEnd = true;

        timerText.enabled = false;

        guideText.enabled = true;

        guideText.text = $"Game Over..\n {(int)runningTime}";

        DataManager.Instance.TapRunnerScore = (int)runningTime;

        StartCoroutine(InputBlockDelay());
    }
}
