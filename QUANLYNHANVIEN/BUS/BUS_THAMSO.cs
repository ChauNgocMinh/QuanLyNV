using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_THAMSO
    {
        DAL_THAMSO thamso = new DAL_THAMSO();

        public DataTable getThamSo()
        {
            return thamso.getThamSo();
        }

        public bool SuaThamSo(DTO_THAMSO ts)
        {
            return thamso.SuaThamSo(ts);
        }

        public double Get_soNgayNghiToiDa()
        {
            return thamso.Get_soNgayNghiToiDa();
        }

        public double Get_soNgayLamToiDa()
        {
            return thamso.Get_soNgayLamToiDa();
        }

        public double Get_tiLePhat()
        {
            return thamso.Get_tiLePhat();
        }

        public double Get_tienLamthem()
        {
            return thamso.Get_tienLamthem();
        }
        public double Get_soThangNghiSinh()
        {
            return thamso.Get_soThangNghiSinh();
        }

    }
}
