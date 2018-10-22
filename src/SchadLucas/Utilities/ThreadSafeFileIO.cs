using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SchadLucas.Utilities
{
    public static class ThreadSafeFileIo
    {
        private static readonly Dictionary<string, object> Lock = new Dictionary<string, object>();

        public static string ReadFromFile(string filePath)
        {
            var output = string.Empty;

            DoItLocked(filePath, () => output = File.ReadAllText(filePath));

            return output;
        }

        public static void WriteToFile(string filePath, byte[] content)
        {
            DoItLocked(filePath, () => File.WriteAllBytes(filePath, content));
        }

        public static void WriteToFile(string filePath, string content, Encoding encoding = null)
        {
            DoItLocked(filePath, () => File.WriteAllText(filePath, content, encoding ?? Encoding.UTF8));
        }

        private static void DoItLocked(string lockName, Action action)
        {
            lock (GetLock(lockName))
            {
                action();
            }
        }

        private static object GetLock(string name)
        {
            if (!Lock.ContainsKey(name))
            {
                Lock.Add(name, new object());
            }

            return Lock[name];
        }
    }
}