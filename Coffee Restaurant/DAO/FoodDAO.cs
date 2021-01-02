﻿using Coffee_Project.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Project.DAO
{
	public class FoodDAO
	{
		private static FoodDAO instance;

		public static FoodDAO Instance {
			get { if (instance == null) instance=new FoodDAO(); return FoodDAO.instance; }
			private set => instance = value; 
		}
		private FoodDAO() { }
		public List<Food> GetFoodByCategory(int id)
		{
			List<Food> list = new List<Food>();
			string query = "SELECT *FROM dbo.Food WHERE idCategory="+id;
			DataTable data = DataProvider.Instance.ExecuteQuery(query);
			foreach (DataRow item in data.Rows)
			{
				Food food = new Food(item);
				list.Add(food);
			}
			return list;
		}
		public List<Food> GetListFood()
		{
			List<Food> list = new List<Food>();
			string query = "SELECT*FROM dbo.Food";
			DataTable data = DataProvider.Instance.ExecuteQuery(query);
			foreach (DataRow item in data.Rows)
			{
				Food food = new Food(item);
				list.Add(food);
			}
			return list;
		}
		public bool InsertFood(string name, int id, int price)
		{
			string query =string.Format ("INSERT dbo.Food(name, idCategory, price) VALUES(N'{0}', {1}, {2})", name, id, price);
			int result= DataProvider.Instance.ExecuteNonQuery(query);
			return result>0;
		}
		public bool UpdateFood(int idFood, string name, int id, int price)
		{
			string query = string.Format("UPDATE dbo.Food SET name=N'{0}', idCategory={1}, price={2} WHERE id={3}", name, id, price, idFood);
			int result = DataProvider.Instance.ExecuteNonQuery(query);
			return result > 0;
		}
		public bool DeleteFood(int idFood)
		{
			BillInforDAO.Instance.DeleteBillInforByFoodID(idFood);
			string query = string.Format("DELETE dbo.Food WHERE id={0}", idFood);
			int result = DataProvider.Instance.ExecuteNonQuery(query);
			return result > 0;
		}
		public List<Food> SearchFoodByName(string name)
		{
			List<Food> list = new List<Food>();
			string query =string.Format("SELECT *FROM dbo.Food WHERE dbo.fuConvertToUnsign1(name) LIKE N'%' +dbo.fuConvertToUnsign1(N'{0}')+'%'", name);
			DataTable data = DataProvider.Instance.ExecuteQuery(query);
			foreach (DataRow item in data.Rows)
			{
				Food food = new Food(item);
				list.Add(food);
			}
			return list;
		}
	}
}