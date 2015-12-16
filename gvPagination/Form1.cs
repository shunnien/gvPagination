using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

        /// <summary>
        /// 計算頁數
        /// </summary>
        /// <param name="dt">The dt.</param>
        private void CalculateTotalPages(DataTable dt) {

            int rowCount = dt.Rows.Count;
            _totalPage = rowCount / _pageSize;

            //不足一個分頁行數的還是算一頁
            if (rowCount % _pageSize > 0)
                _totalPage += 1;
        }

        /// <summary>
        /// Gets the current records.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        private DataTable getCurrentRecords(int page) {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(_connecString)) {
                SqlCommand cmd = new SqlCommand();
                if (page == 1) {
                    cmd = new SqlCommand("Select TOP " + _pageSize +
                    " [OrderID],[CustomerID],[ShipVia],[Freight] FROM [Northwind].[dbo].[Orders] ORDER BY [OrderID]", con);
                }
                else {
                    //利用 SQL 語法來切換資料
                    int PreviousPageOffSet = (page - 1) * _pageSize;

                    cmd = new SqlCommand("Select TOP " + _pageSize + " [OrderID],[CustomerID],[ShipVia],[Freight] " +
                        "FROM [Northwind].[dbo].[Orders] WHERE [OrderID] " +
                        "NOT IN " +
                        "(Select TOP " + PreviousPageOffSet + " [OrderID] from [Northwind].[dbo].[Orders] ORDER BY [OrderID] ) "
                        , con);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                lbCurrentPage.Text = "第 " + _currentPageIndex + " 頁";
            }
            return dt;
        }

        private void btnFirstPage_Click(object sender, EventArgs e) {
            _currentPageIndex = 1;
            dataGridView1.DataSource = getCurrentRecords(_currentPageIndex);
        }

        private void btnPreviousPage_Click(object sender, EventArgs e) {
            if (this._currentPageIndex > 1) {
                _currentPageIndex--;
                dataGridView1.DataSource = getCurrentRecords(_currentPageIndex);
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e) {
            if (_currentPageIndex < _totalPage) {
                _currentPageIndex++;
                dataGridView1.DataSource = getCurrentRecords(_currentPageIndex);
            }
        }

        private void btnLastPage_Click(object sender, EventArgs e) {
            _currentPageIndex = _totalPage;
            dataGridView1.DataSource = getCurrentRecords(_currentPageIndex);
        }

        private void btnSet_Click(object sender, EventArgs e) {

        }
    }
}
