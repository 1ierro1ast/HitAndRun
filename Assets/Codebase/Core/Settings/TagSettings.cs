using System;
using UnityEngine;

namespace Codebase.Core.Settings
{
    [Serializable]
    public class TagSettings
    {
        [SerializeField] private Color32 _taggedColor;
        [SerializeField] private float _taggedTime;

        public Color32 TaggedColor => _taggedColor;

        public float TaggedTime => _taggedTime;
    }
}