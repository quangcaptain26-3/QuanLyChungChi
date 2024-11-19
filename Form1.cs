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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void loaiChungChiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoaiChungChi loaiChungChi = new LoaiChungChi();
            loaiChungChi.ShowDialog();
        }

        private void nhanVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NhanVien nhanVien = new NhanVien();
            nhanVien.ShowDialog();
        }

        private void chungChiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChungChi chungChi = new ChungChi();
            chungChi.ShowDialog();
        }
    }
}
