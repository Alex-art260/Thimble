using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

public class MoveThimble : MonoBehaviour
{
    public static event Action OnReplay;

    [SerializeField] private Ball ball;
    [SerializeField] private GameObject ballInGame;
    [SerializeField] private ScoreCount scoreCount;

    [SerializeField] private PositionsOne posOne;
    [SerializeField] private PositionsTwo posTwo;
    [SerializeField] private PositionsThree posThree;

    public float duration;
    public List<Transform> thimble;
    public PathType pathType;
    public PathMode pathMode;

    public Vector3 pos1 = new Vector3(-2, -3, 0);
    public Vector3 pos2 = new Vector3(0, -3, 0);
    public Vector3 pos3 = new Vector3(2, -3, 0);

    private int random;
    private int counter;
    private int countShuffle;

    private bool isDetected;
    private bool isShuffle = true;

    private void OnEnable()
    {
        InitializePositionThimble();
        countShuffle = Random.Range(3, 9);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isShuffle == false)
        {
            isShuffle = true;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                for (int i = 0; i < thimble.Count; i++)
                {
                    if (hit.collider.gameObject.transform.position == pos1)
                    {
                        if (hit.collider.GetComponent<DetectionBall>().isDetected == true)
                        {
                            ballInGame.SetActive(true);
                            ballInGame.transform.localPosition = pos1;
                            scoreCount.SetScore();

                            if (ball.ball.GetComponent<SpriteRenderer>().enabled == false)
                            {
                                ball.ball.GetComponent<SpriteRenderer>().enabled = true;
                            }
                            ball.InvokeBall();
                            Invoke("UnActiveBall", 3f);
                        }
                        else
                        {
                            ball.ball.GetComponent<SpriteRenderer>().enabled = false;

                        }
                        hit.collider.transform.DOPath(posOne.path3, duration, pathType, pathMode, 1);
                    }
                    if (hit.collider.gameObject.transform.position == pos2)
                    {
                        if (hit.collider.GetComponent<DetectionBall>().isDetected == true)
                        {
                            ballInGame.SetActive(true);
                            ballInGame.transform.localPosition = pos2;
                            scoreCount.SetScore();
                            if (ball.ball.GetComponent<SpriteRenderer>().enabled == false)
                            {
                                ball.ball.GetComponent<SpriteRenderer>().enabled = true;
                            }
                            ball.InvokeBall();
                            Invoke("UnActiveBall", 3f);
                        }
                        else
                        {
                            ball.ball.GetComponent<SpriteRenderer>().enabled = false;
                        }
                        hit.collider.transform.DOPath(posTwo.path3, duration, pathType, pathMode, 1);
                    }
                    if (hit.collider.gameObject.transform.position == pos3)
                    {
                        if (hit.collider.GetComponent<DetectionBall>().isDetected == true)
                        {
                            ballInGame.SetActive(true);
                            ballInGame.transform.localPosition = pos3;
                            scoreCount.SetScore();
                            if (ball.ball.GetComponent<SpriteRenderer>().enabled == false)
                            {
                                ball.ball.GetComponent<SpriteRenderer>().enabled = true;
                            }
                            ball.InvokeBall();
                            Invoke("UnActiveBall", 3f);
                        }
                        else
                        {
                            ball.ball.GetComponent<SpriteRenderer>().enabled = false;
                        }
                        hit.collider.transform.DOPath(posThree.path3, duration, pathType, pathMode, 1);
                    }
                }
                isDetected = false;
                ball.ball.transform.parent = null;
                OnReplay?.Invoke();
            }
        }
    }
    public IEnumerator Shuffle()
    {
        while (true)
        {
            isShuffle = true;
            ShuffleThimble();
            counter++;
            yield return new WaitForSeconds(1f);

            if (counter == countShuffle)
            {
                counter = 0;
                countShuffle = Random.Range(3, 9);
                isShuffle = false;
                isDetected = true;

                for (int i = 0; i < thimble.Count; i++)
                {
                    if (thimble[i].GetComponent<DetectionBall>().isBall == true)
                    {
                        ballInGame.transform.position = thimble[i].transform.position;
                    }
                }
                yield break;
            }
        }
    }
    private void ShuffleThimble()
    {
        random = UnityEngine.Random.Range(0, 75);

        for (int i = 0; i < thimble.Count; i++)
        {
            if (random >= 0 && random <= 25)
            {
                if (thimble[i].transform.position == pos1)
                {
                    thimble[i].DOPath(posOne.path, duration, pathType, pathMode, 1);
                }
                else if (thimble[i].transform.position == pos2)
                {
                    thimble[i].DOPath(posTwo.path, duration, pathType, pathMode, 1);
                }
            }
            if (random > 25 && random < 50)
            {
                if (thimble[i].transform.position == pos1)
                {
                    thimble[i].DOPath(posOne.path2, duration, pathType, pathMode, 1);
                }
                else if (thimble[i].transform.position == pos3)
                {
                    thimble[i].DOPath(posThree.path2, duration, pathType, pathMode, 1);
                }
            }
            if (random >= 50 && random < 75)
            {
                if (thimble[i].transform.position == pos2)
                {
                    thimble[i].DOPath(posTwo.path2, duration, pathType, pathMode, 1);
                }
                else if (thimble[i].transform.position == pos3)
                {
                    thimble[i].DOPath(posThree.path, duration, pathType, pathMode, 1);
                }
            }
        }
    }
    private void UnActiveBall()
    {
        ball.ballInGame.SetActive(false);
    }

    public void InitializePositionThimble()
    {
        for (int i = 0; i < thimble.Count; i++)
        {
            thimble[0].transform.position = pos1;
            thimble[1].transform.position = pos2;
            thimble[2].transform.position = pos3;
        }
    }
}

[Serializable]
public class PositionsOne
{
    public Vector3[] path;
    public Vector3[] path2;
    public Vector3[] path3;
}

[Serializable]
public class PositionsTwo
{
    public Vector3[] path;
    public Vector3[] path2;
    public Vector3[] path3;
}

[Serializable]
public class PositionsThree
{
    public Vector3[] path;
    public Vector3[] path2;
    public Vector3[] path3;
}