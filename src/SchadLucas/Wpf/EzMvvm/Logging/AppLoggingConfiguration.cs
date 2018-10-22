using NLog.Config;
using NLog.Targets;

namespace SchadLucas.Wpf.EzMvvm.Logging
{
    internal class AppLoggingConfiguration : LoggingConfiguration
    {
        public AppLoggingConfiguration()
        {
            var targetDevelopment = new DebuggerTarget
            {
                Layout = "${pad:padding=-30:inner=[${level:format=FirstCharacter}\\:${logger}]} ${message} ${onexception:inner=${newline}${exception:format=toString,Data:maxInnerExceptionLevel=10}}",
                Name = "Developer Log"
            };
            var targetChainsaw = new ChainsawTarget("ChainsawTarget")
            {
                Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}",
                Address = "udp://localhost:4000"
            };

            AddTarget(targetDevelopment);
            AddTarget(targetChainsaw);
            AddRuleForAllLevels(targetDevelopment);
            AddRuleForAllLevels(targetChainsaw);
        }
    }
}