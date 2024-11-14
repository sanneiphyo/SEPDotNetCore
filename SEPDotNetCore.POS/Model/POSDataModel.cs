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
    }
}
