﻿using Coffee_Project.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coffee_Project
{
	public partial class fLogin : Form
	{
		public fLogin()
		{
			InitializeComponent();
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void Login_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (MessageBox.Show("Bạn có muốn thoát chương trình không?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
			{
				e.Cancel = true;
			}
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			string userName = txbUserName.Text;
			string passWord = txbPassWord.Text;
			if (Login(userName, passWord))
			{
			TableManager f = new TableManager();
			this.Hide();
			f.ShowDialog();
			this.Show();
			}
			else
			{
				MessageBox.Show("Sai tên tài khoản hoặc mật khẩu");
			}
		}

		bool Login(string userName, string passWord)
		{
			return AccountDAO.Instance.Login(userName, passWord);
		}
	}
}
