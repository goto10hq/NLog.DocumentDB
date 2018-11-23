# NLog.DocumentDB target

Extends NLog with a new target: Azure DocumentDB (CosmosDB). Easy to use with a nice default layout.

# Install using Nuget Package Manager

```Install-Package Nlog.DocumentDBTarget```

# Basic configuration

Just define connection string for DocumentDB and you are ready to go.

```xml
<nlog>
 <extensions> 
  <add assembly="NLog.DocumentDBTarget" />         
 </extensions>
 <targets>
  <target name="sample" type="DocumentDB" endPoint="https://myendpoint.documents.azure.com:443" authorizationKey="s0mes3cre7StuFf==" database="mydb" collection="mycollection"/>
 </targets>    
 <rules>
  <logger name="*" minlevel="Trace" writeTo="sample" />
 </rules>
</nlog>
```

# Custom layout configuration

You can define your custom JSON document. Keep in mind you *have* to use **JsonLayout**. Otherwise logger falls back to the default layout.

```xml
<target name="custom document" type="DocumentDB" endPoint="https://myendpoint.documents.azure.com:443" authorizationKey="s0mes3cre7StuFf==" database="mydb" collection="mycollection"/>
 <layout type="JsonLayout">
  <attribute name="time" layout="${longdate}" />
  <attribute name="timeEpoch" layout="${epoch}" />
  <attribute name="level" layout="${level:upperCase=true}"/>
  <attribute name="message" layout="${message}" />
 </layout>
</target>
```

# Own renderers

## {epoch}

Classic UNIX epoch. Useful for querying data from DocumentDB since datetime is not natively supported.

## {application}

You can set application name in the target configuration:

```xml
<target name="custom" type="DocumentDB" application="Calculator" ...>
```

If leaved empty than logger try to set **${iis-site-name}**, if empty (non web application then **${processname}** is used.

## {entity}

You can set entity name in the target configuration:

```xml
<target name="custom" type="DocumentDB" entity="logger/log" ...>
```

_It's for our internal purposes in Goto10 when we add this field to all the documents. You can freely ignore it._

# Layouts

Default layout looks like that.

```xml
<layout type="JsonLayout">
 <attribute name="application" layout="${application}" />
 <attribute name="logged" layout="${date}" />
 <attribute name="loggedEpoch" layout="${epoch}" />
 <attribute name="level" layout="${level}" />
 <attribute name="message" layout="${message}" />
 <attribute name="identity" layout="${identity}" />
 <attribute name="serverName" layout="${aspnet-request:serverVariable=SERVER_NAME}" />
 <attribute name="port" layout="${aspnet-request:serverVariable=SERVER_PORT}" />
 <attribute name="url" layout="${aspnet-request:serverVariable=HTTP_URL}" />
 <attribute name="serverAddress" layout="${aspnet-request:serverVariable=LOCAL_ADDR}" />
 <attribute name="remoteAddress" layout="${aspnet-request:serverVariable=REMOTE_ADDR}:${aspnet-request:serverVariable=REMOTE_PORT}" />
 <attribute name="logger" layout="${logger}" />
 <attribute name="machineName" layout="${machineName}" />
 <attribute name="exception" layout="${exception:tostring}" />
 <!-- if entity property is set -->
 <!-- <attribute name="entity" layout="${entity}" /> -->
</layout>
```
