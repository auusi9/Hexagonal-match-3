using UnityEngine;
using UnityEngine.Events;


public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int _scorePerMatch = 10;
    public event UnityAction<int> NewScore;

    private int _score = 0;
    private const int MinimumMatches = 3;

    private void Start()
    {
        InvokeNewScoreEvent();
    }

    private void InvokeNewScoreEvent()
    {
        if (NewScore != null)
        {
            NewScore(_score);
        }
    }

    public void NewMatch(int numberOfMatches)
    {
        _score += _scorePerMatch * numberOfMatches + (int)(0.5 *  _scorePerMatch * (numberOfMatches - MinimumMatches));
        InvokeNewScoreEvent();
    }

    public void RestartScore()
    {
        _score = 0;
        InvokeNewScoreEvent();
    }
}
