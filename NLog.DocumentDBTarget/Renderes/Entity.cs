using System.Text;
using NLog;
using NLog.LayoutRenderers;

namespace Nlog.DocumentDBTarget.Renderes
{
    [LayoutRenderer("entity")]
    public class Entity : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var entity = logEvent.Properties["Entity"];

            if (entity != null)
                builder.Append(entity);
        }
    }
}