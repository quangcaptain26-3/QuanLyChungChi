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
    public partial class NhanVien : Form
    {
        public NhanVien()
        {
            InitializeComponent();
            loadCbMaPhongBan();
            loadDgv();
        }
        private void loadDgv()
        {
            dataGridView1.DataSource = database.Query("SELECT MaNhanVien, HoTen, NgaySinh, NhanVien.MaPhongBan FROM NhanVien INNER JOIN PhongBan ON NhanVien.MaPhongBan=PhongBan.MaPhongBan");
            btnSua.Enabled = btnThem.Enabled = (dataGridView1.Rows.Count>0) ;
        }

        private void loadCbMaPhongBan()
        {
            cbMaPhongBan.DisplayMember = "TenPhongBan";
            cbMaPhongBan.ValueMember = "MaPhongBan";
            cbMaPhongBan.DataSource = database.Query("SELECT * FROM PhongBan");
        }
        private bool ktra()
        {
            bool kq = true;
            errorProvider1.Clear();
            if (txtTen.Text.Trim() == "")
            {
                errorProvider1.SetError(txtTen,"Chua dien ten");
                kq=false;
            }
            if (dtpNgaySinh.Text.Trim() == "")
            {
                errorProvider1.SetError(dtpNgaySinh, "Chua dien ngay sinh");
                kq = false;
            }
            return kq;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (ktra() == false) return;
            database.Execute("INSERT NhanVien VALUES(N'"+txtTen.Text+"', '"+dtpNgaySinh.Value.ToString()+"', '"+cbMaPhongBan.SelectedValue.ToString()+"')");
            loadDgv();
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (ktra() == false) return;
            string maNhanVien = dataGridView1.CurrentRow.Cells["clMaNhanVien"].Value.ToString();
            database.Execute("UPDATE NhanVien SET HoTen=N'"+txtTen.Text+"', NgaySinh='"+dtpNgaySinh.Value.ToString()+"', MaPhongBan='"+cbMaPhongBan.SelectedValue.ToString()+"' WHERE MaNhanVien="+maNhanVien+"");
            loadDgv();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            string maNhanVien = dataGridView1.CurrentRow.Cells["clMaNhanVien"].Value.ToString();
            string sql = "SELECT * FROM ChungChi WHERE MaNhanVien='" + maNhanVien + "'";
            if (database.Query(sql).Rows.Count > 0)
            {
                MessageBox.Show("Thong tin xoa co lien quan den bang Chung Chi", "Báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            database.Execute("DELETE NhanVien WHERE MaNhanVien=" + maNhanVien + "");
            loadDgv();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtMa.Text = dataGridView1.Rows[e.RowIndex].Cells["clMaNhanVien"].Value.ToString();
            txtTen.Text = dataGridView1.Rows[e.RowIndex].Cells["clHoTen"].Value.ToString();
            dtpNgaySinh.Text = dataGridView1.Rows[e.RowIndex].Cells["clNgaySinh"].Value.ToString();
            cbMaPhongBan.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["clMaPhongBan"].Value.ToString();
        }
    }
}
