using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapRunnerController : MonoBehaviour
{
    public enum GameState
    {
        Start,
        Play,
        End
    }
    public GameState gameState;

    [SerializeField] int obstacleNeedCount;

    [SerializeField] float maxSpeed;
    [SerializeField] float startSpeed;

    [SerializeField] Vector2 obstaclesStartPos;
    [SerializeField] Vector2 obstaclesEndPos;

    [SerializeField] TapRunnerObstacleHandler[] obstacleHandlers;
    [SerializeField] TapRunnerPlayerController playerController;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI guideText;


    float runningTime;
    float currentSpeed;


    private void Awake()
    {
        guideText.text = "Press Any Key!";

        timerText.enabled = false;

        guideText.enabled = true;

        gameState = GameState.Start;

        playerController.Init(this);
    }



    private void Update()
    {
        if (gameState == GameState.Play)
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

    public void GameOver()
    {
        gameState = GameState.End;

        timerText.enabled = false;

        guideText.enabled = true;

        guideText.text = $"Game Over..\n {(int)runningTime}";

        var playerData = playerController.Player.Data;

        if (playerData.tapRunnerScore < runningTime)
        {
            playerData.tapRunnerScore = (int)runningTime;


            var saveManager = SaveManager.Instance;

            saveManager.SaveData.tapRunnerScore = playerData.tapRunnerScore;

            saveManager.Save();
        };
    }


    public void StartGame()
    {
        gameState = GameState.Play;

        runningTime = 0;

        currentSpeed = startSpeed;

        for (int i = 0; i < obstacleHandlers.Length; i++)
        {
            obstacleHandlers[i].transform.localPosition = obstaclesStartPos * i;

            obstacleHandlers[i].InitChildObstacles(obstacleNeedCount);
        }

        timerText.enabled = true;

        guideText.enabled = false;

        playerController.StartRunner();
    }

    public void EndGame()
    {
        SceneLoadManager.Instance.LoadScene("MainScene", playerController.EndRunner);
    }
}
