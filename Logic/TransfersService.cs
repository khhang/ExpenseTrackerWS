using expense_tracker.Domain.Models;
using expense_tracker.Domain.Requests.Transfers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker.Logic
{
    public interface ITransfersService
    {
        Task<IEnumerable<TransferDetail>> GetTransferDetails();
        Task CreateTransfer(CreateTransferRequest request);
        Task UpdateTransferById(int id, UpdateTransferRequest request);
        Task DeleteTransferById(int id);
        Task DeleteTranferLinkById(int id);
    }

    public class TransfersService : ITransfersService
    {
        public async Task CreateTransfer(CreateTransferRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteTranferLinkById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteTransferById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TransferDetail>> GetTransferDetails()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTransferById(int id, UpdateTransferRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
