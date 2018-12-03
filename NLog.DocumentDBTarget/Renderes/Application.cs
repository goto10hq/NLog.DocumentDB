using System.Text;
using NLog;
using NLog.LayoutRenderers;
using NLog.Web.LayoutRenderers;

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

        static string GetSiteName(LogEventInfo info)
        {
            var layout = new IISInstanceNameLayoutRenderer();
            return layout.Render(info);
        }

        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var app = logEvent.Properties["Application"];
            string appName;

            if (app == null)
            {
                appName = GetSiteName(logEvent);

                if (string.IsNullOrEmpty(appName))
                    appName = GetProcessName(logEvent);
            }
            else
            {
                appName = app.ToString();
            }

            builder.Append(appName);
        }
    }
}