using System.Threading.Tasks;

namespace CalendarApplication.UI.ViewModel
{
    public interface INavigationViewModel
    { 
        Task LoadAsync();
    }
}