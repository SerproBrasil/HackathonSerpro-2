using BigBirds.Domain.Entities;
using BigBirds.Domain.Repository;
using com.valgut.libs.bots.Wit;
using com.valgut.libs.bots.Wit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBirds.Services
{
    public class ORChatService : IORChatService
    {
        #region fields and properties

        private readonly IOrgaoRepository _orgaoRepository;
        private WitConversation<Dictionary<string, object>> _witConversation;
        private Dictionary<string, object> _context;
        private readonly string _token;
        private string _conversationId;
        private string _message;
        private bool didMerge = false;
        private bool didStop = false;

        private readonly string INTENT_KEY = "Intent";
        private readonly string ORGAO_KEY = Enum.GetName(typeof(Intents), Intents.orgao);
        private readonly string ORGAOS_KEY = "orgaos";
        private readonly string CATEGORIA_KEY = Enum.GetName(typeof(Intents), Intents.categoria);
        private readonly string PROBLEMA_KEY = Enum.GetName(typeof(Intents), Intents.problema);

        private const string GET_ORGAOS = "getOrgaos";


        #endregion

        #region constructors

        public ORChatService(string token, IOrgaoRepository orgaoRepository)
        {
            _token = token;
            _orgaoRepository = orgaoRepository;
        }

        #endregion

        #region IORChatService interface

        public async Task<Dictionary<string,object>> GetMessageAsync(Dictionary<string, object> context)
        {
            _conversationId = context.ContainsKey("conversation") ? context["conversation"].ToString() : Guid.NewGuid().ToString();
            _witConversation = new WitConversation<Dictionary<string, object>>(_token, _conversationId, context, DoMerge, DoSay, DoAction, DoStop);

            var message = context["message"].ToString();

            context.Remove("message");
            context.Remove("conversation");

            await _witConversation.SendMessageAsync(message);

            return context;
        }

        public Dictionary<string, object> DoAction(string conversationId, Dictionary<string, object> context, string action, double confidence)
        {
            if (action == GET_ORGAOS)
            {
                if (!context.ContainsKey(ORGAOS_KEY))
                {
                    var orgaos = _orgaoRepository.GetOrgaosByCategoria(context[CATEGORIA_KEY].ToString()).ToList();
                    context.Add(ORGAOS_KEY, string.Join(",", orgaos.Select(o => o.Nome).ToArray()));
                }
            }

            return context;
        }

        public Dictionary<string, object> DoMerge(string conversationId, Dictionary<string, object> context, Dictionary<string, List<Entity>> entities, double confidence)
        {
            if (entities != null)
            {
                if (entities.ContainsKey(INTENT_KEY) && entities.ContainsKey(CATEGORIA_KEY))
                {
                    context.Add(CATEGORIA_KEY, entities[CATEGORIA_KEY][0].value.ToString());
                }
            }
            return context;
        }

        public void DoSay(string conversationId, Dictionary<string, object> context, string msg, double confidence)
        {
            context["message"] = msg;
            context["conversation"] = conversationId;
        }

        public Dictionary<string, object> DoStop(string conversationId, Dictionary<string, object> context)
        {
            didStop = true;

            return context;
        }

        #endregion
    }

    public interface IORChatService
    {
        Task<Dictionary<string, object>> GetMessageAsync(Dictionary<string, object> context);
    }

    //public class BigBirdsContext
    //{
    //    public string InitialMessage { get; set; }
    //    public string Message { get; set; }
    //    public List<Orgao> Orgaos { get; set; }
    //}

    public enum Intents
    {
        categoria,
        orgao,
        problema
    }
}
