using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SEPDotNetCore.MiniKpay.DataBase.AppDbContextModels;

namespace SEPDotNetCore.MiniKpay.Domain.features.User
{
    public class UserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext context)
        {
            _db = context;
        }


        public async Task<TblWalletUser> Register(TblWalletUser newUser)
        {

            _db.TblWalletUsers.Add(newUser);
            await _db.SaveChangesAsync();
            return newUser;
        }

        public async Task<TblWalletUser> UpdateProfileAsync (int id, TblWalletUser updatedUser)
        {
            var user =await _db.TblWalletUsers.FirstOrDefaultAsync(x => x.UserId == id);

            if (user is null)
            {
                return null;
            }

            ValidateUser(updatedUser);

            user.UserName = updatedUser.UserName;
            user.MobileNumber = updatedUser.MobileNumber;
            user.Status = updatedUser.Status;

            _db.SaveChanges();
            return user;
        }

        public async Task<TblWalletUser> GetUser(int id)
        {

            var item = _db.TblWalletUsers.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id)!;

            return item;

        }

        public async Task<TblWalletUser> ChangePin(int id, TblWalletUser newPin)
        {
            var item = _db.TblWalletUsers.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id)!;
            if (item is null)
            {
                return null;
            }

            item.PinCode = item.PinCode;
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return item;
        }



        //private void ValidateUser(TblWalletUser user)
        //{
        //    if (string.IsNullOrWhiteSpace(user.MobileNumber))
        //    {
        //        throw new ArgumentException("Mobile number cannot be empty.");
        //    }

        //    if (!System.Text.RegularExpressions.Regex.IsMatch(user.MobileNumber, @"^\+?[1-9]\d{1,14}$"))
        //    {
        //        throw new ArgumentException("Invalid mobile number format.");
        //    }

        //    if (string.IsNullOrWhiteSpace(user.UserName))
        //    {
        //        throw new ArgumentException("User name cannot be empty.");
        //    }

        //    if (user.UserName.Length > 100)
        //    {
        //        throw new ArgumentException("User name cannot be more than 100 characters.");
        //    }

        //    if (string.IsNullOrWhiteSpace(user.PinCode))
        //    {
        //        throw new ArgumentException("Pin code cannot be empty.");
        //    }

        //    if (user.PinCode.Length != 6)
        //    {
        //        throw new ArgumentException("Pin code must be exactly 6 characters.");
        //    }

            //if (user.Balance.HasValue && user.Balance.Value < 0)
            //{
            //    throw new ArgumentException("Balance cannot be negative.");
            //}
        }
    }
}
