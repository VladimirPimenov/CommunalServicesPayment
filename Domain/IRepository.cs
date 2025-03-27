using CommunalServices.Domain.Entities;

namespace CommunalServices.Domain
{
    public interface IRepository
    {
        public Task<Abonent> GetAbonentByIdAsync(int id);
		public Task<Abonent> GetAbonentByLoginAsync(string login);
		public Task<Abonent> AddAbonentAsync(Abonent newAbonent);
		public Task<Abonent> UpdateAbonentAsync(Abonent updatedAbonent);
		public Task<int> RemoveAbonentAsync(int abonentId);

        public Task<List<Flat>> GetAbonentFlatsAsync(Abonent abonent);

        public Task<Flat> GetFlatByPaymentNumberAsync(string paymentNumber);
        public Task<List<Debt>> GetFlatDebtsAsync(Flat flat);
    }
}