using System;
using System.Runtime.InteropServices;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using Nlog.DocumentDBTarget.Tools;

namespace Nlog.DocumentDBTarget.DocumentDB
{
    public class Connection : IDisposable
    {
        DocumentClient _client;
        Uri _feed;

        /// <summary>
        /// Ctor.
        /// </summary>
        public Connection(string endpoint, string authorizationKey, string database, string collection)
        {
            var defaultConnectionPolicy = new ConnectionPolicy
            {
                RetryOptions = new RetryOptions
                {
                    MaxRetryAttemptsOnThrottledRequests = 10,
                    MaxRetryWaitTimeInSeconds = 60
                }
            };

            // direct mode works only on Windows
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                defaultConnectionPolicy.ConnectionMode = ConnectionMode.Direct;
                defaultConnectionPolicy.ConnectionProtocol = Protocol.Tcp;
            }

            _client = new DocumentClient
                (
                new Uri(endpoint),
                authorizationKey,
                defaultConnectionPolicy
                );

            _feed = UriFactory.CreateDocumentCollectionUri(database, collection);
        }

        /// <summary>
        /// Create document(s) as pure json.
        /// </summary>
        public void CreateJson(string jsonString)
        {
            var document = JsonConvert.DeserializeObject(jsonString);
            AsyncTools.RunSync(() => _client.CreateDocumentAsync(_feed, document));
        }

        public void Dispose()
        {
            _client = null;
            _feed = null;
        }
    }
}