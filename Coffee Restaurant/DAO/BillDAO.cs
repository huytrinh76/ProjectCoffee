﻿using Coffee_Project.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Project.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set => instance = value; 
        }

        private BillDAO() { }

        public int GetUncheckBillIDByTableID(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT *FROM dbo.Bill WHERE idTable="+id+" AND status=0");
			if (data.Rows.Count>0)
			{
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
			}
            return -1;
        }
        public void CheckOut(int id, int discount, int totalPrice)
		{
            string query = "UPDATE dbo.Bill SET dateCheckOut=GETDATE(), status=1, "+"discount= "+discount+", totalPrice= "+totalPrice+"WHERE id="+id;
            DataProvider.Instance.ExecuteNonQuery(query);
		}
        public DataTable GetBillListByDate(DateTime checkIn, DateTime checkOut)
		{
            return DataProvider.Instance.ExecuteQuery("EXEC USP_GetListBillByDate @checkIn , @checkOut", new object[] {checkIn, checkOut });
		}
        public void InsertBill(int id)
		{
            DataProvider.Instance.ExecuteNonQuery("EXEC USP_InsertBill @idTable", new object[] { id});
		}
        public int GetMaxIDBill()
		{
			try
			{
                return (int)DataProvider.Instance.ExecuteScalar("SELECT MAX(id) FROM dbo.Bill");
			}
			catch
			{
                return 1;
			}
		}
    }
}