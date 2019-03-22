using System;
using GameScreen.Dispatcher;
using Moq;

namespace GameScreenTests.NodeWindow
{
    public class TestDispatcherAccessor : DispatcherAccessor
    {
        public TestDispatcherAccessor(MockRepository mockRepository):this(mockRepository.Create<IDispatcher>())
        {
        }
        public TestDispatcherAccessor(Mock<IDispatcher> dispatcher)
        {
            MockDispatcher = dispatcher;
        }

        public Mock<IDispatcher> MockDispatcher { get; }

        public override IDispatcher Get()
        {
            return MockDispatcher.Object;
        }

        public static Action<Action> ExecuteCallback => a => a();

        public void SetupInvokeAction()
        {
            MockDispatcher
                .Setup(md=>md.Invoke(It.IsAny<Action>()))
                .Callback(ExecuteCallback)
                .Verifiable();
        }
    }
}