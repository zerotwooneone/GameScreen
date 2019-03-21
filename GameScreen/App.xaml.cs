using System.Reactive.Subjects;
using System.Windows;
using Autofac;
using GameScreen.Navigation;
using GameScreen.Node;

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

            builder
                .Register(c => new SubjectNodeNavigationService(new Subject<INavigationParam>()))
                .As<INodeNavigationService>();    

            var container = builder.Build();

            var window = container.Resolve<MainWindow>();
            window.Show();
        }
    }


}
