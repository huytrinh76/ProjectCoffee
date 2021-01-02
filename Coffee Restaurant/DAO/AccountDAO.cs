﻿using Coffee_Project.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Project.DAO
{
	public class AccountDAO
	{
		private static AccountDAO instance;
		public static AccountDAO Instance
		{
			get { if (instance == null) instance = new AccountDAO(); return instance; }
			private set  => instance = value; 
		}

		private AccountDAO(){}
		public bool Login(string userName, string passWord)
		{
			//ma hoa mat khau
			/*byte[] temp = ASCIIEncoding.ASCII.GetBytes(passWord);
			byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);
			string hasPass = "";
			foreach (byte item in hasData)
			{
				hasPass += item;
			}*/
			//lat nguoc mat khau
			//var list = hasData.ToString();
			//list.Reverse();

			string query = "USP_Login @userName , @passWord";
			DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] {userName, passWord /*list*/ });
			return result.Rows.Count>0;
		}
		public Account GetAccountByUserName(string userName)
		{
			DataTable data= DataProvider.Instance.ExecuteQuery("SELECT* FROM dbo.Account WHERE UserName='"+userName+"'");
			foreach(DataRow item in data.Rows)
			{
				return new Account(item);
			}
			return null;
		}
		public bool UpdateAccount(string userName, string displayName, string passWord, string newPass)
		{
			int result = DataProvider.Instance.ExecuteNonQuery("EXEC USP_UpdateAccount @userName , @displayName , @passWord , @newPassword", new object[] {userName, displayName, passWord, newPass });
			return result > 0;
		}
		public DataTable GetListAccount()
		{
			return DataProvider.Instance.ExecuteQuery("SELECT UserName, DisplayName, Type  FROM dbo.Account");
		}

		public bool InsertAccount(string name, string displayName, int type)
		{
			string query = string.Format("INSERT dbo.Account(UserName, DisplayName, Type) VALUES(N'{0}', N'{1}', {2})", name, displayName, type);
			int result = DataProvider.Instance.ExecuteNonQuery(query);
			return result > 0;
		}
		public bool UpdateAccount(string name, string displayName, int type)
		{
			string query = string.Format("UPDATE dbo.Account SET DisplayName=N'{1}', Type={2} WHERE UserName=N'{0}'", name, displayName, type);
			int result = DataProvider.Instance.ExecuteNonQuery(query);
			return result > 0;
		}
		public bool DeleteAccount(string name)
		{
			string query = string.Format("DELETE dbo.Account WHERE UserName=N'{0}'", name);
			int result = DataProvider.Instance.ExecuteNonQuery(query);
			return result > 0;
		}
		public bool ResetPass(string name)
		{
			string query = string.Format("UPDATE dbo.Account SET PassWord=N'1' WHERE UserName=N'{0}'", name);
			int result = DataProvider.Instance.ExecuteNonQuery(query);
			return result > 0;
		}
	}
}