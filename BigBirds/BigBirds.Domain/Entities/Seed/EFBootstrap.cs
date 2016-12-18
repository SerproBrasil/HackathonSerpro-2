using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigBirds.Domain.Repository;
using BigBirds.Domain.Contexts;

namespace BigBirds.Domain.Entities.Seed
{
    public class EFBootstrap : System.Data.Entity.DropCreateDatabaseIfModelChanges<BigBirdsContext>
    {
        protected override void Seed(BigBirdsContext context)
        {
            var estados = CarregarEstados(context);
            var categorias = CarregarCategorias(context);
            CarregarOrgaos(context, estados, categorias);
            CarregarSolucoes(context);
        }

        private void CarregarSolucoes(BigBirdsContext context)
        {
            var solucoes = new List<ConclusaoReclamacao>
            {
                new ConclusaoReclamacao {
                Conclusao= "Entrar em contato com a companhia aérea.",
                PalavrasChaves = "mala extraviada"
                }
            };

            context.Set<ConclusaoReclamacao>().AddRange(solucoes);
            context.SaveChanges();
        }
        private List<Estado> CarregarEstados(BigBirdsContext context)
        {
            var estados = new List<Estado>() {
                new Estado { Nome = "Distrito Federal", Sigla = "DF" }
            };

            context.Set<Estado>().AddRange(estados);
            context.SaveChanges();

            return estados;
        }

        private List<Categoria> CarregarCategorias(BigBirdsContext context)
        {
            var categorias = new List<Categoria> {
                new Categoria { Nome = "Alimentos" },
                new Categoria { Nome = "Contratos" },
                new Categoria { Nome = "Direitos do Consumidor" },
                new Categoria { Nome = "Educação" },
                new Categoria { Nome = "Habilitação" },
                new Categoria { Nome = "Internet" },
                new Categoria { Nome = "Meio Ambiente" },
                new Categoria { Nome = "Produtos" },
                new Categoria { Nome = "Saúde" },
                new Categoria { Nome = "Serviços" },
                new Categoria { Nome = "Transporte" }
            };

            context.Set<Categoria>().AddRange(categorias);
            context.SaveChanges();

            return categorias;
        }

        private void CarregarOrgaos(BigBirdsContext context, List<Estado> estados, List<Categoria> categorias)
        {
            var orgaos = new List<Orgao> {
                // Alimentos
                new Orgao {
                      Categoria = categorias[0],
                      Estado = estados[0],
                      Nome = "Centros de Vigilância Sanitária Estaduais",
                      Descricao = "Relação de endereço e telefone da Vigilância Sanitária em todos os Estados. Fiscalizam e recebem denúncia de alimentos, cosméticos, tabaco, medicamentos, produtos para a saúde, saneantes, sangue e hemoderivados, serviços de saúde, toxicologia",
                      UrlPagina = "http://www.anvisa.gov.br/institucional/snvs/centro_est.htm"
                },
                new Orgao {
                      Categoria = categorias[0],
                      Estado = estados[0],
                      Nome = "Órgãos de Defesa do Consumidor",
                      Descricao = "Relação de endereço e telefone da Vigilância Sanitária em todos os Estados. Fiscalizam e recebem denúncia de alimentos, cosméticos, tabaco, medicamentos, produtos para a saúde, saneantes, sangue e hemoderivados, serviços de saúde, toxicologia",
                      UrlPagina = "http://www.mj.gov.br/data/Pages/MJ5E813CF3PTBRIE.htm"
                },
                new Orgao {
                      Categoria = categorias[0],
                      Estado = estados[0],
                      Nome = "Ouvidoria da Agência Nacional de Vigilância Sanitária",
                      Descricao = "Por e-mail, recebe denúncias e queixas relativas à área de vigilância sanitária: alimentos, cosméticos, tabaco, medicamentos, produtos para a saúde (para diagnósticos e odontológicos), saneantes, sangue e hemoderivados, serviços de saúde, toxicologia.",
                      UrlPagina = "http://www.anvisa.gov.br/ouvidoria/fale_com.htm"
                },
                new Orgao {
                      Categoria = categorias[0],
                      Estado = estados[0],
                      Nome = "Procon-DF",
                      Descricao = "Relação de endereços do PROCON/DF. Para fazer uma reclamação, dirija-se ao Procon mais próximo de você e leve: os números da sua identidade e do CPF, um documento que comprove a relação de consumo e endereço completo e os telefones da empresa.",
                      UrlPagina = "http://www.procon.df.gov.br/005/00502001.asp?ttCD_CHAVE=4758"
                },

                // Transportes
                new Orgao {
                      Categoria = categorias[10],
                      Estado = estados[0],
                      Nome = "DETRAN - DF",
                      Descricao = "Ouvidoria do Detran-DF.",
                      UrlPagina = "http://www.detran.df.gov.br/106/10602002.asp?slCD_ORIGEM=337&btNovo=SIM"
                },
                new Orgao {
                      Categoria = categorias[10],
                      Estado = estados[0],
                      Nome = "ANTT - Agência Nacional de Transportes Terrestres",
                      Descricao = "Órgão que atua na fiscalização e regulamentação da prestação de serviços de transportes terrestres.",
                      UrlPagina = "http://www.antt.gov.br/faleconosco/faleconosco.asp"
                },
                new Orgao {
                      Categoria = categorias[10],
                      Estado = estados[0],
                      Nome = "ANAC - Agência Nacional de Aviação Civil",
                      Descricao = "A ANAC atua para promover a segurança da aviação civil e para estimular a concorrência e a melhoria da prestação dos serviços no setor.",
                      UrlPagina = "http://www2.anac.gov.br/arus/focus/faleconosco/validarUsuario.asp?FC=E"
                }
            };

            context.Set<Orgao>().AddRange(orgaos);
            context.SaveChanges();
        }
    }
}
