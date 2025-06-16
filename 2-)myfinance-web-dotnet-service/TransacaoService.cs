using myfinance_web_dotnet_domain.Entities;
using myfinance_web_dotnet_service.Interfaces;
using myfinance_web_dotnet_infra;
using Microsoft.EntityFrameworkCore;


namespace myfinance_web_dotnet_service.Interfaces
{
    public class TransacaoService : ITransacaoService
    {
        private readonly MyFinanceDbContext _dbContext;
        public TransacaoService(MyFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Cadastrar(Transacao Entidade)
        {
            var dbSet = _dbContext.Transacao;
            Console.WriteLine($"===============================");
            if (Entidade.Id == 0)
            {
                Console.WriteLine($"Entrei no if");
                Console.WriteLine($"{Entidade.Id}");
                dbSet.Add(Entidade);
            }
            else
            {
                Console.WriteLine($"Entrei no else");
                dbSet.Attach(Entidade);
                _dbContext.Entry(Entidade).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();

        }
        public void Excluir(int id)
        {
            var Transacao = new Transacao() { Id = id };
            _dbContext.Attach(Transacao);
            _dbContext.Remove(Transacao);
            _dbContext.SaveChanges();
        }
        public List<Transacao> ListarRegistros()
        {
            var dbSet = _dbContext.Transacao.Include(x => x.PlanoConta);
            return dbSet.ToList();
        }
        public Transacao RetornarRegistro(int id)
        {
            return _dbContext.Transacao.Where(x => x.Id == id).First();
        }
    }
}