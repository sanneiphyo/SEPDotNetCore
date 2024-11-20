using Microsoft.EntityFrameworkCore;
using SEPDotNetCore.MiniKpay.DataBase.AppDbContextModels;



namespace SEPDotNetCore.MiniKpay.Domain.features.WalletUser
{
    public class WalletUserService
    {
        private readonly AppDbContext _db;

        public WalletUserService(AppDbContext context)
        {
            _db = context;
        }

        public TblWalletUser Register(TblWalletUser newUser)
        {
            ValidateUser(newUser);
            _db.TblWalletUsers.Add(newUser);
            _db.SaveChanges();
            return newUser;
        }

        public TblWalletUser UpdateProfile(int id , TblWalletUser updatedUser)
        {
            var user = _db.TblWalletUsers.FirstOrDefault(x => x.UserId == id);

            if (user is null)
            {
                return null;
            } 
         
            ValidateUser(updatedUser);

            user.UserName = updatedUser.UserName;
            user.MobileNumber = updatedUser.MobileNumber;
            user.Balance = updatedUser.Balance;
            user.Status = updatedUser.Status;

            _db.SaveChanges();
            return user;
        }

        public TblWalletUser GetUser(int id)
        {

            var item = _db.TblWalletUsers.AsNoTracking().FirstOrDefault( x => x.UserId == id)!;
            return item;
    
        }

        private void ValidateUser(TblWalletUser user)
        {
            if (string.IsNullOrWhiteSpace(user.MobileNumber))
            {
                throw new ArgumentException("Mobile number cannot be empty.");
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(user.MobileNumber, @"^\+?[1-9]\d{1,14}$"))
            {
                throw new ArgumentException("Invalid mobile number format.");
            }

            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                throw new ArgumentException("User name cannot be empty.");
            }

            if (user.UserName.Length > 100)
            {
                throw new ArgumentException("User name cannot be more than 100 characters.");
            }

            if (string.IsNullOrWhiteSpace(user.PinCode))
            {
                throw new ArgumentException("Pin code cannot be empty.");
            }

            if (user.PinCode.Length != 6)
            {
                throw new ArgumentException("Pin code must be exactly 6 characters.");
            }

            //if (user.Balance.HasValue && user.Balance.Value < 0)
            //{
            //    throw new ArgumentException("Balance cannot be negative.");
            //}
        }
    }
}
