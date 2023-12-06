using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        dbCSDLDataContext db = new dbCSDLDataContext();
        public Form1()
        {
            InitializeComponent();
        }
        private string GeneratorId()
        {
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            string hour = DateTime.Now.Hour.ToString();
            string minute = DateTime.Now.Minute.ToString();
            string second = DateTime.Now.Second.ToString();
            return "NV" + year + month + day + hour + minute + second;
        }
        private void showData()
        {
            dtgvNV.Rows.Clear();
            dtgvNV.Refresh();
            foreach (var nv in db.NHANVIENs.ToList())
            {
                dtgvNV.Rows.Add(nv.MANV, nv.HOTENNV, nv.NGAYSINH, nv.GIOITINH);
            }
            dtgvNV.Visible = true;
        }
        private bool FormValidation()
        {
            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Nhập tên nhân viên!");
                return false;
            }
            if (dtpDOB.Value == null)
            {
                MessageBox.Show("Nhập ngày sinh!");
                return false;
            }
            if (cbbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Nhập giới tính!");
                return false;
            }
            return true;
        }
        private void clearForm()
        {
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            dtpDOB.Text = null;
            cbbGender.SelectedIndex = -1;
        }
        private NHANVIEN storeNhanVien()
        {
            NHANVIEN nv = new NHANVIEN();
            nv.HOTENNV = txtName.Text;
            nv.NGAYSINH = dtpDOB.Value;
            nv.GIOITINH = cbbGender.Text;
            return nv;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            txtId.ReadOnly = true;
            dtgvNV.Visible = false;
        }

        private void btnReview_Click(object sender, EventArgs e)
        {
            showData();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (FormValidation())
            {
                NHANVIEN nv = storeNhanVien();
                nv.MANV = GeneratorId();
                db.NHANVIENs.InsertOnSubmit(nv);
                db.SubmitChanges();
                MessageBox.Show("Thêm thành công nhân viên " + nv.HOTENNV);
                showData();
                clearForm();
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (db.NHANVIENs.Where(s => s.MANV == txtId.Text).Count() == 1)
            {
                NHANVIEN nv = db.NHANVIENs.First(s => s.MANV == txtId.Text);
                if (txtName.Text != string.Empty)
                {
                    nv.HOTENNV = txtName.Text;
                }
                if (dtpDOB.Value != DateTime.MinValue)
                {
                    nv.NGAYSINH = dtpDOB.Value;
                }
                if (cbbGender.SelectedIndex != -1)
                {
                    nv.GIOITINH = cbbGender.Text;
                }
                db.SubmitChanges();
                showData();
                clearForm();
            }
        }

        private void dtgvNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = dtgvNV.Rows[e.RowIndex].Cells[0].Value;
            NHANVIEN nv = db.NHANVIENs.First(s => s.MANV == id);
            txtId.Text = nv.MANV;
            txtName.Text = nv.HOTENNV;
            dtpDOB.Value = nv.NGAYSINH.Value;
            cbbGender.Text = nv.GIOITINH;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int a = dtgvNV.SelectedRows.Count;
            if (a > 0)
            {
                MessageBox.Show("Xóa thành công " + a + " nhân viên");
                for (int i = 0; i < a; i++)
                {
                    var id = dtgvNV.SelectedRows[i].Cells[0].Value;
                    NHANVIEN nv = db.NHANVIENs.First(s => s.MANV == id);
                    db.NHANVIENs.DeleteOnSubmit(nv);
                    db.SubmitChanges();
                    showData();
                    clearForm();
                }
            }
            else if (db.NHANVIENs.Where(s => s.MANV == txtId.Text).Count() == 1)
            {
                NHANVIEN nv = db.NHANVIENs.First(s => s.MANV == txtId.Text);
                db.NHANVIENs.DeleteOnSubmit(nv);
                db.SubmitChanges();
                MessageBox.Show("Xóa thành công nhân viên " + txtName.Text);
                showData();
                clearForm();
            }
        }
    }
}
