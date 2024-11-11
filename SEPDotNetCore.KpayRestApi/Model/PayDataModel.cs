namespace SEPDotNetCore.PayRestApi.Model
{
    public class UserDataModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Balance { get; set; }
        public string Pin { get; set; }
    }

    public class DepositDataModel
    {
        public int DepositedId { get; set; }
        public string MobileNumber { get; set; }
        public string Balance { get; set; }
    }

    public class WithDrawDataModel
    {
        public int WithDrawId { get; set; }
        public string MobileNumber { get; set; }
        public string Balance { get; set; }
    }

    public class TransferDataModel
    {
        public int TranferedId { get; set; }
        public string FromMobileNumber { get; set; }
        public string ToMobileNumber { get; set; }
        public string Amount { get; set; }
        public string Pin { get; set; }
    }
}
