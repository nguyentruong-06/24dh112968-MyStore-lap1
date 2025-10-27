using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _24dh112968_MyStore_lap1.Models.ViewModle
{
    public class ProductSearchVM
    {
        // search theo tên mô tả sản phẩm searchTerm
            public string SearchTerm { get; set; }
        //search theo giá tiền của sản phẩm 
            public decimal? MinPrice { get; set; }
            public decimal? MaxPrice { get; set; }
        //thứ tự săp xếp 
            public string SortOrder { get; set; }
        // các thuộc tính hỗ trợ phân trang
           public int? PageNumber { get; set; } // vị trí trang đang hiển thị
            public int? PageSize { get; set; } = 10; //sản phẩm của mỗi trang

        //danh sách sản phẩm thỏa điều kiện
            public PagedList.IPagedList<Product> Products { get; set; }
            //public List<Product> ProductsList { get; set; }


    }
}