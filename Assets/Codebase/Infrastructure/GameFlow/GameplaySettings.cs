using System;
using UnityEngine;

namespace Codebase.Infrastructure.GameFlow
{
    [Serializable]
    public class GameplaySettings
    {
        [SerializeField] private int _goalScore = 3;
        [SerializeField] private float _matchCooldown;
        
        public int GoalScore => _goalScore;
        public float MatchCooldown => _matchCooldown;
    }
}