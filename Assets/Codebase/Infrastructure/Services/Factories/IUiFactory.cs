using Codebase.Core.UI.Popups;

namespace Codebase.Infrastructure.Services.Factories
{
    public interface IUiFactory : IService
    {
        StartPopup CreateStartPopup();
        RoomPopup CreateRoomPopup();
        OverlayPopup CreateOverlayPopup();
    }
}