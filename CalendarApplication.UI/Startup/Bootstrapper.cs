using Autofac;
using CalendarApplication.DataAccess;
using CalendarApplication.UI.Data;
using CalendarApplication.UI.Data.Repositories;
using CalendarApplication.UI.View.Services;
using CalendarApplication.UI.ViewModel;
using Prism.Events;

namespace CalendarApplication.UI.Startup
{
    /// <summary>Klasa odpowiadająca za inicjalizację kontenerów, wykorzystująca bibliotekę <b><see cref="Autofac"/></b>.</summary>
    public class Bootstrapper
    {
        /// <summary>Metoda przekazująca poszczególne kontenery do jednego.</summary>
        /// <returns>Zbriór zarejestrowanych kontenerów.</returns>
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<CalendarApplicationDbContext>().AsSelf();

            builder.RegisterType<MainWindow>().AsSelf();

            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<IncidentDetailViewModel>().As<IIncidentDetailViewModel>();

            builder.RegisterType<IncidentLookupDataService>().As<IIncidentLookupDataService>();
            builder.RegisterType<IncidentRepository>().As<IIncidentRepository>();

            return builder.Build();
        }
    }
}
