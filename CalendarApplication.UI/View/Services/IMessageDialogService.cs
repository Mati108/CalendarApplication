using System.Threading.Tasks;

namespace CalendarApplication.UI.View.Services
{
    public interface IMessageDialogService
    {
        Task<MessageDialogResult> ShowOkCancelDialogAsync(string text, string title);
    }
}