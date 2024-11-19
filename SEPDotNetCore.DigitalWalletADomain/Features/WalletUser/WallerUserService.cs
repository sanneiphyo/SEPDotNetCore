using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http.HttpResults;
using SEPDotNetCore.MiniKpay.Domain.Models;

namespace SEPDotNetCore.MiniKpay.Domain.Features.WalletUser
{
    public class WalletUserService
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DigitalWallet;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";

        public async Task<TblWalletUser> Register(TblWalletUser newUser)
        {
            string query = $@" INSERT INTO[dbo].[TblWalletUser]
                ([MobileNumber]
                , [UserName]
                , [PinCode]
                , [Balance])
          VALUES
                (@MobileNumber
                , @UserName
                , @PinCode
                , @Balance) ";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                ValidateUser(newUser);

                var result = await db.ExecuteAsync(query, new TblWalletUser
                {
                    MobileNumber = newUser.MobileNumber,
                    UserName = newUser.UserName,
                    Pincode = newUser.Pincode,
                    Balance = newUser.Balance,
                });

                if (result > 0)
                {
                    return newUser;
                }
                else
                {
                    throw new Exception("User registration failed.");
                }
            }
        }

        //public async Task<TblWalletUser?> GetUser(int id)
        //{
        //    string query = "select * from TblWalletUser WHERE UserId = @UserId";

        //    using (IDbConnection db = new SqlConnection(_connectionString))
        //    {
        //        var item = await db.QueryAsync<TblWalletUser>(query, new TblWalletUser
                
        //        {
        //            UserId = id
        //        }).FirstOrDefault();

        //        return item.FirstOrDefault();
        //    }
        //}



        private void ValidateUser(TblWalletUser user)
        {
            if (string.IsNullOrWhiteSpace(user.MobileNumber))
            {
                throw new ArgumentException("Mobile Number cannot be empty");
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(user.MobileNumber, @"^\+?[1-9]\d{1,14}$"))
            {
                throw new ArgumentException("Invalid mobile number format.");
            }

            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                throw new ArgumentException("User name cannot be empty");
            }

            if (user.UserName.Length > 100)
            {
                throw new ArgumentException("User name cannot be more than 100 characters.");
            }

            if (string.IsNullOrWhiteSpace(user.Pincode))
            {
                throw new ArgumentException("Pin code cannot be empty");
            }

            if (user.Pincode.Length != 6)
            {
                throw new ArgumentException("Pin code must be exactly 6 characters.");
            }

            if (user.Balance.HasValue && user.Balance.Value < 0)
            {
                throw new ArgumentException("Balance cannot be negative");
            }
        }
    }
}
