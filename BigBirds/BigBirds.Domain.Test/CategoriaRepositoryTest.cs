using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BigBirds.Domain.Repository;
using BigBirds.Domain.Entities;
using BigBirds.Domain.Contexts;
using System.Data.Entity;
using BigBirds.Domain.Entities.Seed;

namespace BigBirds.Domain.Test
{
    [TestClass]
    public class CategoriaRepositoryTest
    {
        [TestMethod]
        public void DeveRodarARotinaDeSeedDoBanco()
        {
            // verificar se ao criar o SEED foi executado
            var repo = new EFBaseRepository<Estado>(new BigBirdsContext());

            Database.SetInitializer<BigBirdsContext>(new EFBootstrap());

            Assert.IsTrue(repo.GetAll().Count > 0);
        }
    }
}
