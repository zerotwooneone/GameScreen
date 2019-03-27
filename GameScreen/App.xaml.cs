using Autofac;
using GameScreen.Navigation;
using GameScreen.Node;
using MongoDB.Driver;
using System.Reactive.Subjects;
using System.Windows;

namespace GameScreen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ContainerBuilder builder = new ContainerBuilder();

            builder
                .RegisterAssemblyTypes(typeof(MainWindow).Assembly)
                .PublicOnly()
                .AsImplementedInterfaces()
                .AsSelf();

            builder
                .Register(c => new SubjectNodeNavigationService(new Subject<INavigationParam>()))
                .As<INodeNavigationService>()
                .SingleInstance();

            builder
                .Register(c => new MongoClient(new MongoClientSettings
                {
                    Server = new MongoServerAddress("localhost", 27017),
                }))
                .As<IMongoClient>()
                .SingleInstance();

            IContainer container = builder.Build();

            MainWindow window = container.Resolve<MainWindow>();
            window.Show();
        }
    }


}
