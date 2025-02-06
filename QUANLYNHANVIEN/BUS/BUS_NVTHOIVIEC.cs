using System;
using DAL;
using DTO;
using System.Data;
namespace BUS
{
    public class BUS_NVTHOIVIEC
    {
        DAL_NVTHOIVIEC nvthoiviec = new DAL_NVTHOIVIEC();

        public DataTable getNVThoiViec()
        {
            return nvthoiviec.getNVThoiViec();
        }

        public bool ThemNVThoiViec(DTO_NVTHOIVIEC nvtv)
        {
            return nvthoiviec.ThemNVThoiViec(nvtv);
        }

        public bool SuaNVThoiViec(DTO_NVTHOIVIEC nvtv)
        {
            return nvthoiviec.SuaNVThoiViec(nvtv);
        }

        public bool XoaNVThoiViec(int manv)
        {
            return nvthoiviec.XoaNVThoiViec(manv);
        }

        public int SoLuongNhanVienNghiViec(int thang, int nam)
        {
            return nvthoiviec.SoLuongNhanVienNghiViec(thang, nam);
        }

    }
}
