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
    public partial class LoaiChungChi : Form
    {
        public LoaiChungChi()
        {
            InitializeComponent();
            loadDgv();
            loadCbMaNgonNgu();
        }

        private void loadDgv()
        {
            dataGridView1.DataSource = database.Query("SELECT MaLoaiChungChi, TenLoaiChungChi, LoaiChungChi.MaNgonNgu, ThoiHan FROM LoaiChungChi INNER JOIN NgonNgu ON LoaiChungChi.MaNgonNgu=NgonNgu.MaNgonNgu\r\n");
            btnSua.Enabled = btnThem.Enabled = (dataGridView1.Rows.Count > 0);
        }

        private void loadCbMaNgonNgu()
        {
            cbNgonNgu.DisplayMember = "TenNgonNgu";
            cbNgonNgu.ValueMember = "MaNgonNgu";
            cbNgonNgu.DataSource = database.Query("SELECT * FROM NgonNgu");
        }
        private bool ktra()
        {
            bool kq = true;
            errorProvider1.Clear();
            if (txtTen.Text.Trim() == "")
            {
                errorProvider1.SetError(txtTen, "Chua dien");
                kq = false;
            }
            return kq;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (ktra() == false) return;
            bool kq = true;
            string sql = "SELECT * FROM LoaiChungChi WHERE TenLoaiChungChi=N'"+txtTen.Text+"'";
            if (database.Query(sql).Rows.Count > 0)
            {
                errorProvider1.SetError(txtTen, "Trung ten");
                txtTen.Clear();
                kq = false;
            }
            if (kq == false) return;
            database.Execute("INSERT LoaiChungChi VALUES(N'"+txtTen.Text+"', '"+cbNgonNgu.SelectedValue.ToString()+"', '"+nmthoiHan.Value.ToString()+"')");
            loadDgv();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(ktra() == false) return;
            bool kq = true;
            string sql = "SELECT * FROM LoaiChungChi WHERE MaLoaiChungChi!='"+txtMa.Text+"' AND TenLoaiChungChi=N'"+txtTen.Text+"'";
            if (database.Query(sql).Rows.Count > 0)
            {
                errorProvider1.SetError(txtTen, "Trung ten");
                txtTen.Clear();
                kq = false;
            }
            if (kq == false) return;
            string maLoaiChungChi = dataGridView1.CurrentRow.Cells["clMaLoaiChungChi"].Value.ToString();
            database.Execute("UPDATE LoaiChungChi SET TenLoaiChungChi=N'"+txtTen.Text+"', MaNgonNgu='"+cbNgonNgu.SelectedValue.ToString()+"', ThoiHan='"+nmthoiHan.Value.ToString()+"' WHERE MaLoaiChungChi='"+maLoaiChungChi+"'");
            loadDgv();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ban co chac chan muon xoa khong","Thong bao",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            string maLoaiChungChi = dataGridView1.CurrentRow.Cells["clMaLoaiChungChi"].Value.ToString();
            string sql = "SELECT * FROM ChungChi WHERE MaLoaiChungChi='" + maLoaiChungChi + "'";
            if(database.Query(sql).Rows.Count > 0)
            {
                MessageBox.Show("Thong tin xoa co lien quan den bang Chung Chi", "Báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            database.Execute("DELETE LoaiChungChi WHERE MaLoaiChungChi='" + maLoaiChungChi + "'");
            loadDgv();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtMa.Text = dataGridView1.Rows[e.RowIndex].Cells["clMaLoaiChungChi"].Value.ToString();
            txtTen.Text = dataGridView1.Rows[e.RowIndex].Cells["clTenLoaiChungChi"].Value.ToString();
            cbNgonNgu.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["clMaNgonNgu"].Value.ToString();
            nmthoiHan.Text = dataGridView1.Rows[e.RowIndex].Cells["clThoiHan"].Value.ToString();

        }
    }
}
