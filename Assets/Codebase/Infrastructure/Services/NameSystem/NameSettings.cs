using System;
using UnityEngine;

namespace Codebase.Infrastructure.Services.NameSystem
{
    [Serializable]
    public class NameSettings
    {
        [SerializeField] private int _maxPlayerNameLength = 14;
        [SerializeField] private string _defaultName = "Player";

        public int MaxPlayerNameLength => _maxPlayerNameLength;
        
        public string DefaultName => _defaultName;
    }
}