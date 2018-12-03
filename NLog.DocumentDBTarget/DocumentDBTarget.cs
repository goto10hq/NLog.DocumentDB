using System;
using Nlog.DocumentDBTarget.DocumentDB;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace Nlog.DocumentDBTarget
{
    [Target("DocumentDB")]
    public class DocumentDBTarget : TargetWithLayout
    {
        Connection _connection;

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

        protected override void InitializeTarget()
        {
            if (string.IsNullOrWhiteSpace(EndPoint))
                throw new NLogConfigurationException("Cannot resolve DocumentDB EndPoint. Please make sure EndPoint property is set.");

            if (string.IsNullOrWhiteSpace(AuthorizationKey))
                throw new NLogConfigurationException("Cannot resolve DocumentDB AuthorizationKey. Please make sure AuthorizationKey property is set.");

            if (string.IsNullOrWhiteSpace(Database))
                throw new NLogConfigurationException("Cannot resolve DocumentDB Database. Please make sure Database property is set.");

            if (string.IsNullOrWhiteSpace(Collection))
                throw new NLogConfigurationException("Cannot resolve DocumentDB Collection. Please make sure Collection property is set.");

            _connection = new Connection(EndPoint, AuthorizationKey, Database, Collection);
        }

        protected override void Write(LogEventInfo logEvent)
        {
            try
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
            catch (Exception ex)
            {
                InternalLogger.Error($"Error while sending log messages to DocumentDB: message=\"{ex.Message}\"");
            }
        }

        void CreateLogEntry(string logMessage)
        {
            _connection.CreateJson(logMessage);
        }
    }
}