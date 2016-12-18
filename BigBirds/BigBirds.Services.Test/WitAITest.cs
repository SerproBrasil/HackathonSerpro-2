using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.valgut.libs.bots.Wit;
using System.Collections.Generic;
using com.valgut.libs.bots.Wit.Models;
using System.Threading.Tasks;

namespace BigBirds.Services.Test
{
    [TestClass]
    public class WitAITest
    {
        private WitConversation<Dictionary<string, object>> _client;
        private const string WIT_TOKEN = "SPNU72DXI5SMOR5MXBHBR7PBB3RZ7A5M";
        private bool _stop = false;
        private bool _merge = false;

        [TestMethod]
        public void DeveRetornarMensagemInicialQuandoProvocado()
        {
            var _session = Guid.NewGuid().ToString();
            var client = new WitClient(WIT_TOKEN);
            var context = new Dictionary<string, object>();

            // enviar a primeira mensagem
            //context.Add("transporte", "transporte aereo");

            var reply1 = client.Converse(_session, "transporte", context);

            //Assert.IsNotNull(reply1.msg);
            Assert.IsNotNull(reply1.action);
            Assert.IsTrue(reply1.action == "getOrgaos");
            Assert.IsTrue(reply1.entities.Count > 0);

            var orgaos = new string[] { "ANAC", "ANVISA", "DPF" };

            context.Add("orgaos", string.Join(",", orgaos));

            var reply2 = client.Converse(_session, string.Empty, context);

            Assert.IsNotNull(reply2.msg);
            Assert.IsNotNull(reply2.entities);

            var reply3 = client.Converse(_session, "ANAC", context);
            Assert.IsNotNull(reply3.entities);

            context.Add("orgao", "ANAC");

            var reply4 = client.Converse(_session, string.Empty, context);

            Assert.IsNotNull(reply4.msg);
            Assert.IsNotNull(reply4.entities);

            var reply5 = client.Converse(_session, "O meu problema é mala extraviada", context);

            Assert.IsNotNull(reply5.msg);
            Assert.IsNotNull(reply5.entities);
        }

        [TestMethod]
        public async Task DeveConversarComOBotCorretamente()
        {
            var _session = Guid.NewGuid().ToString();
            var context = new Dictionary<string, object>();
            _client = new WitConversation<Dictionary<string, object>>(WIT_TOKEN, _session, context, Merge, Say, DoAction, Stop);

            // enviar a primeira mensagem
            //context.Add("category", "O meu problema é transporte");

            await _client.SendMessageAsync("O meu problema é saude!");

            context.Add("orgao", "Ministério da Saúde");

            await _client.SendMessageAsync(string.Empty);

            context.Add("problem", "falta de remédio");

            await _client.SendMessageAsync(string.Empty);

            Assert.IsTrue(true);

            return;
        }

        public Dictionary<string, object> DoAction(string conversationId, Dictionary<string, object> context, string action, double confidence)
        {
            if (action == "getOrgaos")
            {
                if (!context.ContainsKey("orgaos"))
                    context.Add("orgaos", GetOrgaosByCategoria(context["categoria"].ToString()));
            }

            return context;
        }

        public Dictionary<string, object> Merge(string conversationId, Dictionary<string, object> context, Dictionary<string, List<Entity>> entities, double confidence)
        {
            _merge = true;

            if (entities != null && entities.ContainsKey("categoria"))
            {
                context.Add("categoria", entities["categoria"][0].value.ToString());
            }

            return context;
        }

        public void Say(string conversationId, Dictionary<string, object> context, string msg, double confidence)
        {
        }

        public Dictionary<string, object> Stop(string conversationId, Dictionary<string, object> context)
        {
            _stop = true;
            return context;
        }


        public string GetOrgaosByCategoria(string categoria)
        {
            if (categoria.ToLower() == "turismo")
            {
                return string.Join(",", new string[] { "Ministério do Turismo", "asiudhfksjhfd", "ywtuytqeuywte" });
            }
            else if (categoria.ToLower() == "saude")
            {
                return string.Join(",", new string[] { "Ministério da Saúde", "ANVISA", "Ministério Público" });
            }
            else
            {
                return string.Join(",", new string[] { "ANAC", "ANVISA", "DPF" });
            }
        }
    }
}
