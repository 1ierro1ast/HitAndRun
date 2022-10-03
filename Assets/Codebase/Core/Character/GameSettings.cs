using UnityEngine;

namespace Codebase.Core.Character
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Create game settings", order = 51)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private ShiftImpulse.Settings _shiftImpulseSettings;

        public ShiftImpulse.Settings ShiftImpulseSettings => _shiftImpulseSettings;
    }
}
