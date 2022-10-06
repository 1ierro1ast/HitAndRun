using System;
using UnityEngine;

namespace Codebase.Core.Settings
{
    [Serializable]
    public class TagSettings
    {
        [SerializeField] private Color _taggedColor;
        [SerializeField] private float _taggedTime;

        public Color TaggedColor => _taggedColor;

        public float TaggedTime => _taggedTime;
    }
}