using System;
using MeshNumberSystem;
using UnityEngine;
using Utils;

namespace ScoreSystem
{
    public class ScoreBoard : MonoBehaviour
    {
        [Header(Keyword.References)]
        [SerializeField] private MeshNumber scoreCounter;
        [SerializeField] private MeshNumber highScoreCounter;


        private void Start()
        {
            UpdateHighScore(ScoreManager.HighScore);
        }


        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }


        private void OnScoreChanged(ScoreManager.EventResponse response)
        {
            var newScore = response.newScore;
            UpdateScore(newScore);
        }

        private void OnHighScoreChanged(ScoreManager.EventResponse response)
        {
            var newHighScore = response.newHighScore;
            UpdateHighScore(newHighScore);
        }


        private void UpdateScore(int score)
        {
            scoreCounter.Set(score);
        }
        
        private void UpdateHighScore(int highScore)
        {
            highScoreCounter.Set(highScore);
        }
        
        private void AddListeners()
        {
            ScoreManager.OnScoreChanged += OnScoreChanged;
            ScoreManager.OnHighScoreChanged += OnHighScoreChanged;
        }

        private void RemoveListeners()
        {
            ScoreManager.OnScoreChanged -= OnScoreChanged;
            ScoreManager.OnHighScoreChanged -= OnHighScoreChanged;
        }
    }
}