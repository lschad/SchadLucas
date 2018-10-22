using System;
using System.Windows.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.Tests.Basics;
using SchadLucas.Wpf.EzMvvm.Commands;
using SchadLucas.Wpf.EzMvvm.Commands.Generic;

namespace SchadLucas.Wpf.EzMvvm.Tests.Commands
{
    [TestClass]
    [TestCategory("EzMvvm")]
    [TestCategory("Commands")]
    public class EzCommandTests : EzTest
    {
        [TestMethod]
        public void CanExecute_IsTrue_IfNotSpecified()
        {
            var command = new EzCommand(_ => { });
            Assert.IsTrue(command.CanExecute(null));
        }

        [TestMethod]
        public void CanExecuteAction_GetsExecuted_OnCanExecute()
        {
            var sut = new EzCommand(_ => { }, _ => false);

            Assert.IsFalse(sut.CanExecute(null));
        }

        [TestMethod]
        public void CanExecutedChanged_Raised_WhenRaiseCanExecuteChangedCalled()
        {
            // Ensure the invalidate is processed
            // https://stackoverflow.com/questions/17269702/commandmanager-invalidaterequerysuggested-does-not-fire-requerysuggested
            void Fix() => Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new Action(() => { }));

            void RaiseEvent(EzCommand cmd)
            {
                cmd.RaiseCanExecuteChanged();
                Fix();
            }

            var eventsRaised = 0;
            var command = new EzCommand(_ => { }, _ => true);

            void Handler(object s, EventArgs e) => eventsRaised++;

            GC.KeepAlive((EventHandler) Handler);

            command.CanExecuteChanged += Handler;

            for (var i = 0; i < 99; i++)
            {
                Assert.AreEqual(i, eventsRaised);
                RaiseEvent(command);
            }

            command.CanExecuteChanged -= Handler;
            var n = eventsRaised + 0;

            for (var i = 0; i < 99; i++)
            {
                Assert.AreEqual(n, eventsRaised);
                RaiseEvent(command);
            }
        }

        [TestMethod]
        public void Execute_Uses_TheProvidedObject()
        {
            object x = null;
            var command = new EzCommand(o => x = o);

            void ExecuteAndAssert(object o)
            {
                command.Execute(o);
                Assert.AreEqual(o, x);
            }

            ExecuteAndAssert(123);
            ExecuteAndAssert("abc");
            ExecuteAndAssert(null);
            ExecuteAndAssert(new EzCommand(_ => { }));
            ExecuteAndAssert(new object[23]);
            ExecuteAndAssert(new object());
            ExecuteAndAssert(Guid.NewGuid());
        }

        [TestMethod]
        public void ExecuteAction_GetsExecuted_OnExecute()
        {
            var executed = false;
            var sut = new EzCommand(_ => executed = true);

            Assert.IsFalse(executed);
            sut.Execute(null);
            Assert.IsTrue(executed);
        }

        [TestMethod]
        public void GenericVersion_CastsProperly()
        {
            var executeInput = string.Empty;
            var canExecuteInput = 0;

            var command = new EzCommand<string, int>(s => executeInput = s, i =>
            {
                canExecuteInput = i;
                return true;
            });

            command.CanExecute(92374);
            command.Execute("HelloWorld");

            Assert.AreEqual("HelloWorld", executeInput);
            Assert.AreEqual(92374, canExecuteInput);
        }
    }
}