using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEPDotNetCore.POS.Model
{
    public class POSDataModel
    {

        [Table("Tbl_Product")]
        public class ProductDataModel
        {
            [Key]
            public int ProductId { get; set; }
            public string ProductCode { get; set; }
            public string ProductCategoryCode { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public bool DeleteFlag { get; set; }
        }

        [Table("Tbl_ProductCategory")]
        public class ProductCategoryDataModel
        {
            [Key]
            public int ProductCategoryId { get; set; }
            public string ProductCategoryCode { get; set; }
         
            public string ProductCategoryName { get; set; }
            public bool DeleteFlag { get; set; }
        }

        [Table("Tbl_Shop")]
        public class ShopDataModel
        {
            [Key]
            public int ShopId { get; set; }
            public string ShopCode { get; set; }

            public string ShopName { get; set; }
            public string MobileNo { get; set; }
            public string Address { get; set; }
            public bool DeleteFlag { get; set; }
        }

        [Table("Tbl_Staff")]
        public class StaffDataModel
        {
            [Key]
            public int StaffId { get; set; }
            public string StaffCode { get; set; }

            public string StaffName { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string MobileNo { get; set; }
            public string Address { get; set; }
            public string Gender { get; set; }
            public string Position { get; set; }
            public string Password { get; set; }
            public bool DeleteFlag { get; set; }
        }

        [Table("Tbl_SaleInvoice")]
        public class SaleInvoiceDataModel
        {
            [Key]
            public int SaleInvoiceId { get; set; }
            public DateTime SaleInvoiceDateTime { get; set; }

            public string VoucherNo { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal Discount { get; set; }
            public string StaffCode { get; set; }
            public decimal Tax { get; set; }
            public string PaymentType { get; set; }
            public decimal PaymentAmount { get; set; }
            public decimal ReceiveAmount{ get; set; }
            public decimal Change { get; set; }
            public string CustomerCode { get; set; }
            public bool DeleteFlag { get; set; }
        }

        [Table("Tbl_SaleInvoiceDetail")]
        public class SaleInvoiceDetailDataModel
        {
            [Key]
            public int SaleInvoiceDetailId { get; set; }
            public DateTime SaleInvoiceDateTime { get; set; }

            public string VoucherNo { get; set; }
            public string ProductCode { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal Amount { get; set; }
          
            public bool DeleteFlag { get; set; }
        }

        [Table("Tbl_Customer")]
        public class CustomerDataModel
        {
            [Key]
            public int CustomerId { get; set; }
            public string FirstName { get; set; }

            public string LastName { get; set; }
            public string Email { get; set; }
            public string  PhoneNumber { get; set; }
            public string Address { get; set; }
            public TimestampAttribute CreatedAt { get; set; }
            public bool DeleteFlag { get; set; }
        }

        [Table("Tbl_Supplier")]
        public class SupplierDataModel
        {
            [Key]
            public int SupplierId { get; set; }
            public string Name { get; set; }

            public string Contact { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public bool DeleteFlag { get; set; }
        }

        [Table("Tbl_Inventory")]
        public class InventoryDataModel
        {
            [Key]
            public int InventoryId { get; set; }
            public int Quantity { get; set; }

            public bool DeleteFlag { get; set; }
        }

        [Table("Tbl_Payment")]
        public class PaymentDataModel
        {
            [Key]
            public int PaymentId { get; set; }
            public string Amount { get; set; }
            public string PaymentMethod { get; set; }
            public bool DeleteFlag { get; set; }
        }

        [Table("Tbl_Expense")]
        public class ExpenseDataModel
        {
            [Key]
            public int ExpenseId { get; set; }
            public string Description { get; set; }
            public string Amount { get; set; }
            public DateTime Date { get; set; }
            public bool DeleteFlag { get; set; }
        }
    }
}
