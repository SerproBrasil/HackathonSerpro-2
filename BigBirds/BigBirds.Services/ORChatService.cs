using BigBirds.Domain.Entities;
using BigBirds.Domain.Repository;
using com.valgut.libs.bots.Wit;
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
        private readonly WitClient _witClient;
        private BigBirdsContext _context;
        private readonly string _token;
        private readonly string _conversationId;
        private string _message;
        private bool didMerge = false;
        private bool didStop = false;

        #endregion
        #region constructors

        public ORChatService(string token, IOrgaoRepository orgaoRepository)
        {
            _context = new BigBirdsContext();
            _conversationId = Guid.NewGuid().ToString();
            _token = token;
            _orgaoRepository = orgaoRepository;
            _witClient = new WitClient(_token);
        }

        #endregion

        #region IORChatService interface

        public async Task<string> GetMessageAsync(string message)
        {
            _context.InitialMessage = message;
            _context.Message = message;

            var conversation = new WitConversation<BigBirdsContext>(_token, _conversationId, _context, doMerge, doSay, doAction, doStop);

            await conversation.SendMessageAsync(_context.Message);

            return _context.Message;
        }

        public BigBirdsContext doMerge(string conversationId, BigBirdsContext context, object entities, double confidence)
        {
            didMerge = true;
            return context;
        }

        public void doSay(string conversationId, BigBirdsContext context, string msg, double confidence)
        {
            context.Message = string.Format("{0}: {1}", msg, context.Orgaos != null && context.Orgaos.Count > 0 ? string.Join(",", context.Orgaos.Select(a => a.Nome)) : string.Empty);
        }

        public BigBirdsContext doAction(string conversationId, BigBirdsContext context, string action, double confidence)
        {

            if (action == "getOrgaos")
            {
                context.Orgaos = _orgaoRepository.GetOrgaosByCategoria(_context.InitialMessage).ToList();
                context.Message = _context.Orgaos[0].Nome;
            }

            return context;
        }

        public BigBirdsContext doStop(string conversationId, BigBirdsContext context)
        {
            didStop = true;

            return context;
        }

        #endregion
    }

    public interface IORChatService
    {
        Task<string> GetMessageAsync(string message);
    }

    public class BigBirdsContext
    {
        public string InitialMessage { get; set; }
        public string Message { get; set; }
        public List<Orgao> Orgaos { get; set; }
    }
}
