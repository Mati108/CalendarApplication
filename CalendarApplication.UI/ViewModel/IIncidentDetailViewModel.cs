using System.Threading.Tasks;

namespace CalendarApplication.UI.ViewModel
{
    public interface IIncidentDetailViewModel
    {
        Task LoadAsync(int? incidentId);
        bool HasChanges { get; }
    }
}