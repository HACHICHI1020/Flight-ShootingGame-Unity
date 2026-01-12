using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;
    public enum ScoreState
    {
        None, AddScore
    }
    public ScoreState scoreState = ScoreState.None; //점수 획득 상태
    public int addScore; //획득점수
    private float score; //현재점수
    private int targetScore; //목표점수

    public float scoreSpeed; //점수 변동 속도
    public Text scoreText;

    void Start()
    {
        instance = this;
    }

    
    void Update()
    {
        switch (scoreState)
        {
            case ScoreState.AddScore:
                {
                    //float scoreUp = score;
                    score = Mathf.MoveTowards(score, targetScore, scoreSpeed * Time.deltaTime);

                    string result = string.Format("{0:#,0}", Mathf.Floor(score));
                    scoreText.text = result;
                    
                    if(score == targetScore)
                    {
                        scoreState = ScoreState.None;
                    }
                    break;
                }
        }
    }

    //점수획득함수
    public void GetScore()
    {
        targetScore += addScore;
        scoreState = ScoreState.AddScore;
    }
}
