using Cape.Models;

namespace Cape.Interfaces
{
    interface ITransactionRepository
    {
        void Create(Transaction obj);
        void Update(Transaction obj);
        void Save();
    }
}