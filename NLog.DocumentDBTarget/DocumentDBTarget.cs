using Nlog.DocumentDBTarget.DocumentDB;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace Nlog.DocumentDBTarget
{
    [Target("DocumentDB")]
    public class DocumentDBTarget : TargetWithLayout
    {
        static Connection _connection;
        static readonly object _connectionLock = new object();

        Connection Connection
        {
            get
            {
                lock (_connectionLock)
                {
                    return _connection ?? (_connection = new Connection(EndPoint, AuthorizationKey, Database, Collection));
                }
            }
        }

        [RequiredParameter]
        public string EndPoint { get; set; }

        [RequiredParameter]
        public string AuthorizationKey { get; set; }

        [RequiredParameter]
        public string Database { get; set; }

        [RequiredParameter]
        public string Collection { get; set; }

        public string Application { get; set; }

        public string Entity { get; set; }

        protected override void Write(LogEventInfo logEvent)
        {
            logEvent.Properties.Add("Application", Application);
            logEvent.Properties.Add("Entity", Entity);

            string logMessage;

            if (!(Layout is JsonLayout))
            {
                logMessage = !string.IsNullOrEmpty(Entity) ? Layouts.Goto10.Layout.Render(logEvent) : Layouts.Default.Layout.Render(logEvent);
            }
            else
            {
                logMessage = Layout.Render(logEvent);
            }

            CreateLogEntry(logMessage);
        }

        private void CreateLogEntry(string logMessage)
        {
            Connection.CreateJson(logMessage);
        }
    }
}