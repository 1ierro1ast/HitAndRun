using UnityEngine;

namespace Codebase.Core.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Create game settings", order = 51)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private CharacterMovementsSettings _characterMovementsSettings;
        [SerializeField] private ShiftImpulseSettings _shiftImpulseSettings;
        [SerializeField] private TagSettings _tagSettings;

        public CharacterMovementsSettings CharacterMovementsSettings => _characterMovementsSettings;
        public TagSettings TagSettings => _tagSettings;
        public ShiftImpulseSettings ShiftImpulseSettings => _shiftImpulseSettings;
    }
}
