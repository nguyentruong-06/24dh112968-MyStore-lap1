using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _24dh112968_MyStore_lap1.Models.ViewModle
{
    public class ProductDetailsVM
    {
        public Product product { get; set; }
        public int quantity { get; set; } = 1;
        // Tính giá trị tạm thời
        public decimal estimatedValue => quantity * product.ProductPrice;

        // Các thuộc tính hỗ trợ phân trang
        public int pageNumber { get; set; } // Trang hiện tại
        public int pageSize { get; set; } = 3; // Số sản phẩm mỗi trang

        // danh sách 8 sản phẩm cùng danh mục
        public PagedList.IPagedList<Product> RelatedProducts { get; set; }
        public List<Product> RelatedProduct { get; set; }

        // danh sách 8 sản phẩm bán chạy nhất cùng danh mục
        public PagedList.IPagedList<Product> TopProducts { get; set; }
    }
}