using myfinance_web_dotnet_domain.Entities;
using myfinance_web_dotnet_service.Interfaces;
using myfinance_web_dotnet_infra;
using Microsoft.EntityFrameworkCore;


namespace myfinance_web_dotnet_service.Interfaces
{
    public class PlanoContaService : IPlanoContaService
    {
        private readonly MyFinanceDbContext _dbContext;
        public PlanoContaService(MyFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Cadastrar(PlanoConta Entidade)
        {
            var dbSet = _dbContext.PlanoConta;

            if (Entidade.Id == 0)
            {
                
                dbSet.Add(Entidade);
            }
            else
            {
                dbSet.Attach(Entidade);
                _dbContext.Entry(Entidade).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }
        public void Excluir(int id)
        {
            var PlanoConta = new PlanoConta() { Id = id };
            _dbContext.Attach(PlanoConta);
            _dbContext.Remove(PlanoConta);
            _dbContext.SaveChanges();
        }
        public List<PlanoConta> ListarRegistros()
        {
            var dbSet = _dbContext.PlanoConta;
            return dbSet.ToList();
        }
        public PlanoConta RetornarRegistro(int id)
        {
            return _dbContext.PlanoConta.Where(x => x.Id == id).First();
        }
    }
}