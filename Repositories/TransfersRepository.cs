using expense_tracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace expense_tracker.Repositories
{
    public interface ITransfersRepository
    {
        Task<IEnumerable<TransferDetail>> GetTransferDetails();
        Task<TransferDetail> GetTransferDetailsById(int id);
        Task CreateTransfer(int sourceAccountId, int destinationAccountId, decimal amount, DateTime createDate);
        Task DeleteTransferById(int id);
        Task UpdateTransferById(int id, int sourceAccountId, int destinationAccountId, DateTime createDate);
    }

    public class TransfersRepository : ITransfersRepository
    {
        private readonly IDatabaseAccessor _databaseAccessor;
        public TransfersRepository(IDatabaseAccessor databaseAccessor)
        {
            _databaseAccessor = databaseAccessor;
        }

        public Task CreateTransfer(int sourceAccountId, int destinationAccountId, decimal amount, DateTime createDate)
        {
            var parameters = new
            {
                SourceAccountId = sourceAccountId,
                DestinationAccountId = destinationAccountId,
                Amount = amount,
                CreateDate = createDate
            };

            return _databaseAccessor.ExecuteProcedureAsync("sp_AddTransfer", parameters);
        }

        public Task DeleteTransferById(int id)
        {
            var parameters = new
            {
                Id = id
            };

            return _databaseAccessor.ExecuteProcedureAsync("sp_DeleteTransfer_ById", parameters);
        }

        public Task<IEnumerable<TransferDetail>> GetTransferDetails()
        {
            return _databaseAccessor.QueryMultipleProcedureAsync<TransferDetail>("sp_SelectTransferDetails");
        }

        public Task<TransferDetail> GetTransferDetailsById(int id)
        {
            var parameters = new
            {
                Id = id
            };

            return _databaseAccessor.QueryProcedureAsync<TransferDetail>("sp_SelectTransferDetails_ById", parameters);
        }

        public Task UpdateTransferById(int id, int sourceAccountId, int destinationAccountId, DateTime createDate)
        {
            throw new NotImplementedException();
        }
    }
}
