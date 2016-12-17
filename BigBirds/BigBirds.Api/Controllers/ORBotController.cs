using BigBirds.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BigBirds.Api.Controllers
{
    [RoutePrefix("api/v1")]
    public class ORBotController : ApiController
    {
        #region fields and properties

        private readonly IORChatService _chatService;

        #endregion

        #region constructors

        public ORBotController(IORChatService chatService)
        {
            _chatService = chatService;
        }

        #endregion

        #region events and methods

        [Route("message")]
        public async Task<IHttpActionResult> GetMessage()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
