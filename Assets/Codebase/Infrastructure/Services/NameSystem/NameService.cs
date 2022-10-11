using Codebase.Core.Settings;
using Codebase.Infrastructure.Services.AssetManagement;

namespace Codebase.Infrastructure.Services.NameSystem
{
    public class NameService : INameService
    {
        private readonly NameSettings _nameSettings;

        private string _playerName = "";

        public string PlayerName => _playerName == "" ? _nameSettings.DefaultName : _playerName;
        public int MaxPlayerNameLength => _nameSettings.MaxPlayerNameLength;


        public NameService(IAssetProvider assetProvider)
        {
            _nameSettings = assetProvider.GetScriptableObject<GameSettings>(AssetPath.GameSettingsPath).NameSettings;
        }

        public void SetPlayerName(string name)
        {
            _playerName = name;
        }
    }
}