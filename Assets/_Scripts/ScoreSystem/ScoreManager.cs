using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace ScoreSystem
{
    public static class ScoreManager
    {
        private const string HighScoreSaveKey = "HighScore";

        private const int DefaultScore = 0;
        private const int DefaultHighScore = 0;


        public static UnityAction<EventResponse> OnScoreChanged;
        public static UnityAction<EventResponse> OnHighScoreChanged;


        public static int Score { get; private set; }

        public static int HighScore
        {
            get => PlayerPrefs.GetInt(HighScoreSaveKey, DefaultHighScore);
            private set => PlayerPrefs.SetInt(HighScoreSaveKey, value);
        }

        public static int CurrentUfoScore => UfoScoreTable[ms_UfoScoreTableIndex];


        private static readonly int[] UfoScoreTable = new int[]
        {
            100, 50, 50, 100, 150, 100, 100, 50, 300, 100, 100, 100, 50, 150, 100
        };


        private static int ms_UfoScoreTableIndex;


#if UNITY_EDITOR
        
        [InitializeOnEnterPlayMode]
        private static void InitializeForEnterPlayMode()
        {
            OnScoreChanged = null;
            OnHighScoreChanged = null;
            ms_UfoScoreTableIndex = 0;
        }
        
#endif
    
        
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

        public static void OnGameStarted(Game.EventResponse response)
        {
            ResetScore();
            ResetUfoScoreTable();
        }

        public static void IncrementUfoScoreTable()
        {
            ms_UfoScoreTableIndex = (ms_UfoScoreTableIndex + 1) % UfoScoreTable.Length;
        }
    
    
        private static void ResetScore()
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
        
        private static void ResetUfoScoreTable()
        {
            ms_UfoScoreTableIndex = 0;
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