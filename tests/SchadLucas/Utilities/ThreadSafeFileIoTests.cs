using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nito.AsyncEx;
using SchadLucas.Tests.Basics;

namespace SchadLucas.Utilities.Tests
{
    [TestClass]
    public class ThreadSafeFileIoTests : EzTest
    {
        [TestMethod]
        public void ReadFromFile()
        {
            const string content = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?";
            File.WriteAllText(_filePath, content);
            Assert.IsTrue(File.Exists(_filePath), "Test-Setup failed CmonBruh.");

            Assert.AreEqual(content, ThreadSafeFileIo.ReadFromFile(_filePath));
        }

        [TestMethod]
        public void WriteToFile_Bytes()
        {
            async Task<int> AsyncWriteToFile(byte[] content)
            {
                return await Task.Run(() => { return TrueOnCatch(() => ThreadSafeFileIo.WriteToFile(_filePath, content)) ? 1 : 0; });
            }

            var raisedExceptions = 0;
            var runs = 0;
            Repeat(() => AsyncContext.Run(async () =>
            {
                var i = await AsyncWriteToFile(new byte[1000 * 1000 * 50]);
                raisedExceptions += i;
                runs++;
            }), 35);

            Assert.AreEqual(0, raisedExceptions);
            Assert.AreEqual(35, runs);
        }

        [TestMethod]
        public void WriteToFile_String()
        {
            async Task<int> AsyncWriteToFile(string content)
            {
                return await Task.Run(() => { return TrueOnCatch(() => ThreadSafeFileIo.WriteToFile(_filePath, content, Encoding.ASCII)) ? 1 : 0; });
            }

            var raisedExceptions = 0;
            var runs = 0;
            Repeat(() => AsyncContext.Run(async () =>
            {
                var i = await AsyncWriteToFile(new string('A', 1000 * 1000 * 50));
                raisedExceptions += i;
                runs++;
            }), 35);

            Assert.AreEqual(0, raisedExceptions);
            Assert.AreEqual(35, runs);
        }

        #region Setup

        private string _filePath;

        protected override void TestCleanup()
        {
            bool FileExists() => File.Exists(_filePath);

            if (FileExists())
            {
                File.Delete(_filePath);
                Assert.IsFalse(FileExists());
            }
        }

        protected override void TestInitialize()
        {
            _filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.tmp");
        }

        private static bool TrueOnCatch(Action a)
        {
            var catchedException = false;

            try
            {
                a();
            }
            catch (Exception e)
            {
                catchedException = true;
                Trace.WriteLine(e);
            }

            return catchedException;
        }

        #endregion
    }
}