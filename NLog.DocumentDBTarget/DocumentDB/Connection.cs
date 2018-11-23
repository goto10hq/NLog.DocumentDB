using System;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using Nlog.DocumentDBTarget.Tools;

namespace Nlog.DocumentDBTarget.DocumentDB
{
    public class Connection
    {
        public DocumentClient Client { get; }

        public string DatabaseId { get; }
        public string CollectionId { get; }

        /// <summary>
        /// Ctor.
        /// </summary>
        public Connection(string endpoint, string authorizationKey, string database, string collection)
        {
            var connectionPolicy = new ConnectionPolicy { UserAgentSuffix = "NLog.DocumentDBTarget" };
            Client = new DocumentClient(new Uri(endpoint), authorizationKey, connectionPolicy);
            DatabaseId = database;
            CollectionId = collection;
        }

        /// <summary>
        /// Create document(s) as pure json.
        /// </summary>
        public async Task<bool> CreateJsonAsync(string jsonString)
        {
            var document = JsonConvert.DeserializeObject(jsonString);
            await Core.ExecuteWithRetriesAsync(() => Client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), document)).ConfigureAwait(false);

            return true;
        }

        /// <summary>
        /// Create document(s) as pure json.
        /// </summary>
        public bool CreateJson(string jsonString)
        {
            return AsyncTools.RunSync(() => CreateJsonAsync(jsonString));
        }
    }
}