using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SEPDotNetCore.MiniKpay.DataBase.AppDbContextModels;
using SEPDotNetCore.MiniKpay.Domain.Models;

namespace SEPDotNetCore.MiniKpay.Domain.features.Transactions
{
    public class TransactionService
    {
        private readonly AppDbContext _db;

        public TransactionService(AppDbContext context)
        {
            _db = context;
        }
        //public TransferResponseModel Transfer (int senderId , int receiverId , decimal amount)
        //{
        //    TransferResponseModel model = new TransferResponseModel();

        //    var sender = _db.TblWalletUsers.FirstOrDefault(x => x.UserId == senderId);  
        //    var receiver = _db.TblWalletUsers.FirstOrDefault (x => x.UserId == receiverId);

        //    if (sender == null || receiver == null) 
        //    {
        //       model.responseModel = BaseResponseModel.ValidationError("999", "Sender or Receiver not found");
        //        goto Result;
        //    }

        //    if (sender.Balance < amount)
        //    {
        //          model.responseModel = BaseResponseModel.ValidationError("999", "Insufficient balance");
        //        goto Result;
        //    }

        //    sender.Balance -= amount;
        //    receiver.Balance += amount;

        //    var Transaction = new TblTransaction
        //    {
        //        SenderUserId = senderId,
        //        ReceiverUserId= receiverId,
        //        Amount = amount,
        //        TransactionDate = DateTime.UtcNow

        //    };

        //    _db.TblTransactions.Add(Transaction);
        //    _db.SaveChanges();

        //    model.Transaction = Transaction;
        //    model.responseModel = BaseResponseModel.Success("000","Success");

        //Result:
        //    return model;


        //}



        
        public async Task<Result<ResultTransferResponseModel>> Transfer(int senderId, int receiverId, decimal amount)
        {
            Result<ResultTransferResponseModel> model = new Result<ResultTransferResponseModel>();

            var sender = await _db.TblWalletUsers.FirstOrDefaultAsync(x => x.UserId == senderId);
            var receiver = await _db.TblWalletUsers.FirstOrDefaultAsync(x => x.UserId == receiverId);

            if (sender == null || receiver == null)
            {
                model = Result<ResultTransferResponseModel>.ValidationError( "Sender or Receiver not found");
                goto Result;
            }

            if (sender.Balance < amount)
            {
                model = Result<ResultTransferResponseModel>.ValidationError("Insufficient balance.");
                goto Result;
            }

            sender.Balance -= amount;
            receiver.Balance += amount;

            var Transaction = new TblTransaction
            {
                SenderUserId = senderId,
                ReceiverUserId = receiverId,
                Amount = amount,
                TransactionDate = DateTime.UtcNow

            };

            await _db.TblTransactions.AddAsync(Transaction);
            await _db.SaveChangesAsync();

            ResultTransferResponseModel item = new ResultTransferResponseModel()
            {
                Transaction = Transaction
            };
            model = Result<ResultTransferResponseModel>.Success(item, "Success.");
        Result:
            return model;


        }
    }
}
