using System.Text;
using NLog;
using NLog.LayoutRenderers;

namespace Nlog.DocumentDBTarget.Renderes
{
    [LayoutRenderer("application")]
    public class Application : LayoutRenderer
    {
        static string GetProcessName(LogEventInfo info)
        {
            var layout = new ProcessNameLayoutRenderer { FullName = false };
            return layout.Render(info);
        }
        
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var app = logEvent.Properties["Application"];
            string appName = app == null ? GetProcessName(logEvent) : app.ToString();
            builder.Append(appName);
        }
    }
}