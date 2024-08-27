using WebSiteBanSach.Models;
using X.PagedList;
using X.Web.PagedList;

namespace WebSiteBanSach.ViewModel
{
    public class SachListVM
    {
		//public List<SachVM> Sachs { get; set; }
		public List<SachVM> Sachs { get; set; }
		public List<ChuDe> ChuDes { get; set; }
        public List<NXBVM> NXBes { get; set; }
        public List<GioHang> GioHangs { get; set; }
        public List<SachVaChuDe> SachVaChuDes { get;set; }
    }
    
}
