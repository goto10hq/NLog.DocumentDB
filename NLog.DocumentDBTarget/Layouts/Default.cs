using NLog.Layouts;

namespace Nlog.DocumentDBTarget.Layouts
{    
    public static class Default
    {        
        public static readonly JsonLayout Layout = new JsonLayout
                                          {
                                              Attributes =
                                              {                                                  
                                                  new JsonAttribute("application", "${application}", true),                                                  
                                                  new JsonAttribute("logged", "${date}", true),
                                                  new JsonAttribute("loggedEpoch", "${epoch}", true),
                                                  new JsonAttribute("level", "${level}", true),
                                                  new JsonAttribute("message", "${message}", true),
                                                  new JsonAttribute("identity", "${identity}", true),
                                                  new JsonAttribute("serverName", "${aspnet-request:serverVariable=SERVER_NAME}", true),
                                                  new JsonAttribute("port", "${aspnet-request:serverVariable=SERVER_PORT}", true),
                                                  new JsonAttribute("url", "${aspnet-request:serverVariable=HTTP_URL}", true),
                                                  new JsonAttribute("serverAddress", "${aspnet-request:serverVariable=LOCAL_ADDR}", true),
                                                  new JsonAttribute("remoteAddress", "${aspnet-request:serverVariable=REMOTE_ADDR}:${aspnet-request:serverVariable=REMOTE_PORT}", true),
                                                  new JsonAttribute("logger", "${logger}", true),
                                                  new JsonAttribute("machineName", "${machineName}", true),                                                  
                                                  new JsonAttribute("exception", "${exception:tostring}", true),                                                  
                                              }
                                          };                
    }
}
