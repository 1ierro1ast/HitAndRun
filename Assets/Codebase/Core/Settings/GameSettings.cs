using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services.NameSystem;
using UnityEngine;

namespace Codebase.Core.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Create game settings", order = 51)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private CharacterMovementsSettings _characterMovementsSettings;
        [SerializeField] private ShiftImpulseSettings _shiftImpulseSettings;
        [SerializeField] private TagSettings _tagSettings;
        [SerializeField] private NameSettings _nameSettings;
        [SerializeField] private GameplaySettings _gameplaySettings;
        
        public CharacterMovementsSettings CharacterMovementsSettings => _characterMovementsSettings;
        public TagSettings TagSettings => _tagSettings;
        public ShiftImpulseSettings ShiftImpulseSettings => _shiftImpulseSettings;
        public NameSettings NameSettings => _nameSettings;
        public GameplaySettings GameplaySettings => _gameplaySettings;
    }
}
