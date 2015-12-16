using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gvPagination {
    public partial class Form1 : Form {

        private string _connecString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        // 現在選取的分頁編號
        private int _currentPageIndex = 1;
        // 總共多少分頁
        private int _totalPage = 0;
        // 頁面大小
        private int _pageSize = 20;

        public Form1() {
            InitializeComponent();
        }

        private void btnFirstPage_Click(object sender, EventArgs e) {

        }

        private void btnPreviousPage_Click(object sender, EventArgs e) {

        }

        private void btnNextPage_Click(object sender, EventArgs e) {

        }

        private void btnLastPage_Click(object sender, EventArgs e) {

        }

        private void btnSet_Click(object sender, EventArgs e) {

        }
    }
}
