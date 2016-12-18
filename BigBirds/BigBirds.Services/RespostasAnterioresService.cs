using BigBirds.Domain.Entities;
using BigBirds.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBirds.Services
{
    public class RespostasAnterioresService : IRespostasAnterioresService
    {
        private readonly IConclusaoReclamacaoRepository _conclusaoRepo;

        public RespostasAnterioresService(IConclusaoReclamacaoRepository conclusaoReclamacaoRepository)
        {
            _conclusaoRepo = conclusaoReclamacaoRepository;
        }

        public ConclusaoReclamacao GetRespostaAnterior(string keywords)
        {
            return _conclusaoRepo.GetConclusoesByKeyWords(keywords).FirstOrDefault();
        }
    }

    public interface IRespostasAnterioresService
    {
        ConclusaoReclamacao GetRespostaAnterior(string keywords); 
    }
}
