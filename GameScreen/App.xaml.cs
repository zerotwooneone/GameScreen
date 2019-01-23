using System.Windows;
using Autofac;

namespace GameScreen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            var builder = new ContainerBuilder();

            builder
                .RegisterAssemblyTypes(typeof (MainWindow).Assembly)
                .PublicOnly()
                .AsImplementedInterfaces()
                .AsSelf();

            var container = builder.Build();

            var window = container.Resolve<MainWindow>();
            window.Show();
        }
    }


}
