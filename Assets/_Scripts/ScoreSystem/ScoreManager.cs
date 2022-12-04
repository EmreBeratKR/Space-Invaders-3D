using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace ScoreSystem
{
    public static class ScoreManager
    {
        private const string ScoreSaveKey = "Score";
        private const string HighScoreSaveKey = "HighScore";

        private const int DefaultScore = 0;
        private const int DefaultHighScore = 0;


        public static UnityAction<EventResponse> OnScoreChanged;
        public static UnityAction<EventResponse> OnHighScoreChanged;


        public static int Score
        {
            get => PlayerPrefs.GetInt(ScoreSaveKey, DefaultScore);
            private set => PlayerPrefs.SetInt(ScoreSaveKey, value);
        }

        public static int HighScore
        {
            get => PlayerPrefs.GetInt(HighScoreSaveKey, DefaultHighScore);
            private set => PlayerPrefs.SetInt(HighScoreSaveKey, value);
        }
    
        
        [InitializeOnEnterPlayMode]
        private static void InitializeForEnterPlayMode()
        {
            OnScoreChanged = null;
            OnHighScoreChanged = null;
        }
        
    
        public static void EarnScore(int score)
        {
            var oldScore = Score;
            var newScore = oldScore + score;
            Score = newScore;

            OnScoreChanged?.Invoke(new EventResponse()
            {
                oldScore = oldScore,
                newScore = newScore
            });
        }

        public static void ResetScore()
        {
            var oldScore = Score;
            var newScore = DefaultScore;
            Score = newScore;
        
            OnScoreChanged?.Invoke(new EventResponse()
            {
                oldScore = oldScore,
                newScore = newScore
            });
        }

        public static bool TryUpdateHighScore()
        {
            var score = Score;
            var highScore = HighScore;

            if (score < highScore) return false;

            HighScore = score;
            OnHighScoreChanged?.Invoke(new EventResponse()
            {
                oldHighScore = highScore,
                newHighScore = score
            });

            return true;
        }
    
    
    
    
        [Serializable]
        public struct EventResponse
        {
            public int oldScore;
            public int newScore;
            public int oldHighScore;
            public int newHighScore;
        }
    }
}