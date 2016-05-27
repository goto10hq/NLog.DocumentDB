using System.Text;
using Nlog.DocumentDBTarget.Tools;
using NLog;
using NLog.LayoutRenderers;

namespace Nlog.DocumentDBTarget.Renderes
{
    [LayoutRenderer("epoch")]
    public class Epoch : LayoutRenderer
    {        
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            builder.Append(logEvent.TimeStamp.ToEpoch());            
        }
    }
}
