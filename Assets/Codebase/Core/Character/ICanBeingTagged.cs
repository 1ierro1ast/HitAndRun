using System;

namespace Codebase.Core.Character
{
    public interface ICanBeingTagged
    {
        bool CanTag { get; }
        void Tag(Action callback);
    }
}