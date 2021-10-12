using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    [SerializeField] private MoveThimble moveThimble;

    [SerializeField] public GameObject ball;
    [SerializeField] private GameObject startButton;
    [SerializeField] private List<Vector3> positionsBall;
    [SerializeField] private Vector3 thimbleUp;

    [SerializeField] private GameObject thimble1;
    [SerializeField] private GameObject thimble2;
    [SerializeField] private GameObject thimble3;
    [SerializeField] public GameObject ballInGame;

    [SerializeField] private GameObject score;
    [SerializeField] private GameObject highScore;
    [SerializeField] private GameObject homeButton;

    public float duration;
    public PathType pathType;
    public PathMode pathMode;

    public Vector3[] pathOne;
    public Vector3[] pathTwo;
    public Vector3[] pathThree;

    public bool isShuffle;
    public bool next;

    private void OnEnable()
    {
        MoveThimble.OnReplay += NextGame;
    }

    private void OnDisable()
    {
        MoveThimble.OnReplay -= NextGame;
    }

    public void StartGame()
    {
        ActiveGame();
        score.SetActive(true);
        highScore.SetActive(false);
        startButton.SetActive(false);
        homeButton.SetActive(true);

        Invoke("BallPosition", 0.5f);
    }

    public void NextGame()
    {
        ball.GetComponent<SpriteRenderer>().enabled = true;
        Invoke("UnActiveGame", 1.1f);
        Invoke("ActiveGame", 1.1f);
        Invoke("BallPosition", 1.5f);
    }

    private void BallPosition()
    {
        int randomPos = Random.Range(0, positionsBall.Count);
        ball.transform.position = positionsBall[randomPos];

        for (int i = 0; i < moveThimble.thimble.Count; i++)
        {
            if (ball.transform.position == moveThimble.thimble[i].transform.position)
            {
                moveThimble.thimble[i].GetComponent<DetectionBall>().isBall = true;
                moveThimble.thimble[i].GetComponent<DetectionBall>().isDetected = true;
                isShuffle = true;

                if (moveThimble.thimble[i].transform.position == moveThimble.pos1 && moveThimble.thimble[i].GetComponent<DetectionBall>().isDetected == true 
                    && moveThimble.thimble[i].GetComponent<DetectionBall>().isBall == true)
                {
                    moveThimble.thimble[i].DOPath(pathOne, duration, pathType, pathMode, 1);
                }
                if (moveThimble.thimble[i].transform.position == moveThimble.pos2 && moveThimble.thimble[i].GetComponent<DetectionBall>().isDetected == true 
                    && moveThimble.thimble[i].GetComponent<DetectionBall>().isBall == true)
                {
                    moveThimble.thimble[i].DOPath(pathTwo, duration, pathType, pathMode, 1);
                }
                if (moveThimble.thimble[i].transform.position == moveThimble.pos3 && moveThimble.thimble[i].GetComponent<DetectionBall>().isDetected == true 
                    && moveThimble.thimble[i].GetComponent<DetectionBall>().isBall == true)
                {
                    moveThimble.thimble[i].DOPath(pathThree, duration, pathType, pathMode, 1);
                }
                Invoke("SetParent", 2f);
            }
            else
            {
                moveThimble.thimble[i].GetComponent<DetectionBall>().isDetected = false;
                moveThimble.thimble[i].GetComponent<DetectionBall>().isBall = false;
            }
        }
    }

    private void SetParent()
    {
        ball.GetComponent<SpriteRenderer>().enabled = false;
        for (int i = 0; i < moveThimble.thimble.Count; i++)
        {
            if (moveThimble.thimble[i].GetComponent<DetectionBall>().isBall == true)
            {
                ball.transform.parent = moveThimble.thimble[i].transform;
            }
        }
        StartCoroutine(moveThimble.Shuffle());
    }

    private void UnActiveGame()
    {
        thimble1.SetActive(false);
        thimble2.SetActive(false);
        thimble3.SetActive(false);
        ball.SetActive(false);
    }

    private void ActiveGame()
    {
        moveThimble.InitializePositionThimble();

        thimble1.SetActive(true);
        thimble2.SetActive(true);
        thimble3.SetActive(true);
        ball.SetActive(true);
    }

    private void UnactiveBall()
    {
        ball.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void InvokeBall()
    {
        Invoke("UnactiveBall", 5f);
    }
}