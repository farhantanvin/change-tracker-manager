using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace GHIApplication.GlobalsStorages
{
    public class GlSessionControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        public GlSessionControllerHandler(RouteData routeData)
            : base(routeData)
        { }
    }


    public class GlSessionHttpControllerRouteHandler : HttpControllerRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new GlSessionControllerHandler(requestContext.RouteData);
        }
    }
}