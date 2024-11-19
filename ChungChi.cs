using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyChungChi
{
    public partial class ChungChi : Form
    {
        public ChungChi()
        {
            InitializeComponent();
            loadDgv();
            loadCbMaLoaiCC();
            loadCbMaNhanVien();
        }

        private void loadDgv()
        {
            dataGridView1.DataSource = database.Query(
                "SELECT MaChungChi, ChungChi.MaLoaiChungChi, ChungChi.MaNhanVien, NgayCap, NoiCap FROM ChungChi INNER JOIN LoaiChungChi ON ChungChi.MaLoaiChungChi=LoaiChungChi.MaLoaiChungChi INNER JOIN NhanVien ON ChungChi.MaNhanVien=NhanVien.MaNhanVien\r\n"
            );
            btnSua.Enabled = btnXoa.Enabled = (dataGridView1.Rows.Count > 1);
        }

        private void loadCbMaLoaiCC()
        {
            cbMaLoaiCC.DisplayMember = "TenLoaiChungChi";
            cbMaLoaiCC.ValueMember = "MaLoaiChungChi";
            cbMaLoaiCC.DataSource = database.Query("SELECT * FROM LoaiChungChi");
        }

        private void loadCbMaNhanVien()
        {
            cbMaNhanVien.DisplayMember = "HoTen";
            cbMaNhanVien.ValueMember = "MaNhanVien";
            cbMaNhanVien.DataSource = database.Query("SELECT * FROM NhanVien");
        }

        private bool ktra()
        {
            bool kq = true;
            errorProvider1.Clear();
            if (txtNoiCap.Text.Trim() == "")
            {
                errorProvider1.SetError(txtNoiCap, "Chua dien noi cap");
                kq = false;
            }
            return kq;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (ktra() == false)
                return;
            database.Execute(
                "INSERT ChungChi VALUES('"
                    + cbMaLoaiCC.SelectedValue.ToString()
                    + "','"
                    + cbMaNhanVien.SelectedValue.ToString()
                    + "','"
                    + dtpNgayCap.Value.ToString()
                    + "',N'"
                    + txtNoiCap.Text
                    + "')"
            );
            loadDgv();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (ktra() == false)
                return;
            string maChungChi = dataGridView1.CurrentRow.Cells["clMaChungChi"].Value.ToString();
            database.Execute(
                "UPDATE ChungChi SET MaLoaiChungChi='"
                    + cbMaLoaiCC.SelectedValue.ToString()
                    + "', MaNhanVien='"
                    + cbMaNhanVien.SelectedValue.ToString()
                    + "', NgayCap='"
                    + dtpNgayCap.Value.ToString()
                    + "', NoiCap='"
                    + txtNoiCap.Text
                    + "' WHERE MaChungChi="
                    + maChungChi
                    + ""
            );
            loadDgv();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show(
                    "Bạn có thực sự muốn xóa không?",
                    "Thông báo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                ) == DialogResult.No
            )
                return;
            string maChungChi = dataGridView1.CurrentRow.Cells["clMaChungChi"].Value.ToString();
            database.Execute("DELETE ChungChi WHERE MaChungChi=" + maChungChi + "");
            loadDgv();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtMaCC.Text = dataGridView1.Rows[e.RowIndex].Cells["clMaChungChi"].Value.ToString();
            cbMaLoaiCC.SelectedValue = dataGridView1
                .Rows[e.RowIndex]
                .Cells["clMaLoaiCC"]
                .Value.ToString();
            cbMaNhanVien.SelectedValue = dataGridView1
                .Rows[e.RowIndex]
                .Cells["clMaNhanVien"]
                .Value.ToString();
            dtpNgayCap.Text = dataGridView1.Rows[e.RowIndex].Cells["clNgayCap"].Value.ToString();
            txtNoiCap.Text = dataGridView1.Rows[e.RowIndex].Cells["clNoiCap"].Value.ToString();
        }
    }
}
