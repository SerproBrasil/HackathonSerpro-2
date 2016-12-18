using BigBirds.Api.Messages.Requests;
using BigBirds.Api.Utils;
using BigBirds.Domain.Repository;
using BigBirds.Services;
using com.valgut.libs.bots.Wit;
using com.valgut.libs.bots.Wit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web.Http;

namespace BigBirds.Api.Controllers
{
    [RoutePrefix("api/v1/chat")]
    public class ORBotController : ApiController
    {
        #region fields and properties

        private IORChatService _chatService;
        private IOrgaoRepository _orgaoRepo;
        private WitConversation<Dictionary<string, object>> _client;

        #endregion

        #region constructors

        public ORBotController()
        {
            // _chatService = new ORChatService(ConfigConstants.WIT_ACCESS_TOKEN, new OrgaoRepository());
            _orgaoRepo = new OrgaoRepository();
        }

        #endregion

        #region events and methods

        [Route("message")]
        [HttpPost]
        public async Task<IHttpActionResult> PostMessage(ORBotChatMessage context)
        {

            var _dict = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(context.Categoria))
                _dict.Add("categoria", context.Categoria);

            if (!string.IsNullOrWhiteSpace(context.Orgaos))
                _dict.Add("orgaos", context.Orgaos);

            if (!string.IsNullOrWhiteSpace(context.Orgao))
                _dict.Add("orgao", context.Orgao);

            if (!string.IsNullOrWhiteSpace(context.Problema))
                _dict.Add("problema", context.Problema);

            if (!string.IsNullOrWhiteSpace(context.Conversation))
                _dict.Add("conversation", context.Conversation);
            else
            {
                _dict.Add("conversation", Guid.NewGuid().ToString());
            }
            if (!string.IsNullOrWhiteSpace(context.Message))
                _dict.Add("message", context.Message);


            //_chatService = GetCachedData(_dict["conversation"].ToString(), new DateTimeOffset(DateTime.Now.AddDays(1d)));

            //var conversation = await _chatService.GetMessageAsync(_dict);

            //return Ok(new
            //{
            //    Status = 200,
            //    Context = conversation
            //});

            var _session = _dict["conversation"].ToString();

            _client = new WitConversation<Dictionary<string, object>>(ConfigConstants.WIT_ACCESS_TOKEN, _session, _dict, Merge , Say, DoAction, Stop);

            await _client.SendMessageAsync(context.Message);

            //_context.Add("orgao", "Ministério da Saúde");

            //await _client.SendMessageAsync(string.Empty);

            //_context.Add("problem", "falta de remédio");

            //await _client.SendMessageAsync(string.Empty);

            return Ok(new
            {
                Status = 200,
                Context = _dict
            });
        }

        static IORChatService GetCachedData(string key, DateTimeOffset offset)
        {
            var lazyObject = new ORChatService(ConfigConstants.WIT_ACCESS_TOKEN, new OrgaoRepository());
            var returnedLazyObject = MemoryCache.Default.AddOrGetExisting(key, lazyObject, offset);
            return (IORChatService)returnedLazyObject;
        }


        #endregion

        public Dictionary<string, object> DoAction(string conversationId, Dictionary<string, object> context, string action, double confidence)
        {
            if (action == "getOrgaos")
            {
                if (!context.ContainsKey("orgaos"))
                    context.Add("orgaos", string.Join(",", _orgaoRepo.GetOrgaosByCategoria(context["categoria"].ToString()).Select(o => o.Nome).ToArray()));
            }

            return context;
        }

        public Dictionary<string, object> Merge(string conversationId, Dictionary<string, object> context, Dictionary<string, List<Entity>> entities, double confidence)
        {

            if (entities != null && entities.ContainsKey("categoria"))
            {
                context.Add("categoria", entities["categoria"][0].value.ToString());
            }

            return context;
        }

        public void Say(string conversationId, Dictionary<string, object> context, string msg, double confidence)
        {
            context["message"] = msg;
            context["conversation"] = conversationId;
        }

        public Dictionary<string, object> Stop(string conversationId, Dictionary<string, object> context)
        {
            return context;
        }

    }

    public class ORBotChatMessage
    {
        public string Categoria { get; set; }
        public string Orgaos { get; set; }
        public string Orgao { get; set; }
        public string Problema { get; set; }
        public string Conversation { get; set; }
        public string Message { get; set; }
    }
}
