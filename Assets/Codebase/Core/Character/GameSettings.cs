using UnityEngine;

namespace Codebase.Core.Character
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Create game settings", order = 51)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private ShiftImpulseSettings _shiftImpulseSettings;

        public ShiftImpulseSettings ShiftImpulseSettings => _shiftImpulseSettings;
    }
}
