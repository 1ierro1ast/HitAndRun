using Codebase.Core;

namespace Codebase.Infrastructure.Services.Factories
{
    public interface ILevelFactory : IService
    {
        Level GetLevel();
    }
}