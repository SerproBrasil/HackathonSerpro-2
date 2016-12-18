using BigBirds.Domain.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBirds.Api.Tests
{
    [TestClass]
    public class TestConclusaoReclamacao
    {
        [TestMethod]
        public void CreateConclusao()
        {
            ConclusaoReclamacaoRepository repConclusao = new ConclusaoReclamacaoRepository();

            repConclusao.Add(new Domain.Entities.ConclusaoReclamacao() {
                Conclusao= "Entrar em contato com a companhia aérea.",
                PalavrasChaves = "mala extraviada"
            });

            var result = repConclusao.GetAll();
            if (result != null)
                Assert.IsTrue(true);
        }
    }
}
