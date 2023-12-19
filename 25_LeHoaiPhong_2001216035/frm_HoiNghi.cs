using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using _25_LeHoaiPhong_2001216035.Objects;

namespace _25_LeHoaiPhong_2001216035
{
    public partial class frm_HoiNghi : Form
    {
        public frm_HoiNghi()
        {
            InitializeComponent();
        }

        public bool Annotation()
        {
            if(txtMaHN.Text == string.Empty)
            {
                MessageBox.Show("Nhập mã hội nghị!");
                txtMaHN.Select();
                return false;
            }
            if(!txtMaHN.Text.StartsWith("HN"))
            {
                MessageBox.Show("Mã hội nghị phải bắt đầu bằng HN");
                txtMaHN.Select(); 
                return false;
            }  
            if (txtTenHN.Text == string.Empty)
            {
                MessageBox.Show("Nhập tên hội nghị!");
                txtTenHN.Select();
                return false;
            }
            if(!Regex.IsMatch(txtTenHN.Text, @"^[\w\d\s]+$"))
            {
                MessageBox.Show("Tên hội nghị không được chứa ký tự đặc biệt");
                txtTenHN.Select();
                return false;
            }    
            if (txtSoNguoi.Text == string.Empty)
            {
                MessageBox.Show("Nhập số người tham gia!");
                txtSoNguoi.Select();
                return false;
            }
            int parsedValue;
            if (!int.TryParse(txtSoNguoi.Text, out parsedValue))
            {
                MessageBox.Show("Chỉ được nhập số!");
                txtSoNguoi.Text = string.Empty;
                txtSoNguoi.Select();
                return false;
            }
            if (Convert.ToInt32(txtSoNguoi.Text) < 50)
            {
                MessageBox.Show("Số người tham gia phải lớn hơn hoặc bằng 50");
                txtSoNguoi.Select();
                return false;
            }
            return true;
        }
        private void frm_HoiNghi_Load(object sender, EventArgs e)
        {
            // load combobox loai phong            
            LoaiPhong_DAO lpcn = new LoaiPhong_DAO();
            foreach (var item in lpcn.getData())
            {
                cbbLoaiPhong.Items.Add(item.tenLoaiPhong);
            }
            cbbLoaiPhong.SelectedIndex = 0;

            //load datagridview hoi nghi
            HoiNghi_DAO hncn = new HoiNghi_DAO();
            showData(hncn.getData());
        }
        private void showData(List<HoiNghi> lhn)
        {
            dataHoiNghi.Rows.Clear();
            dataHoiNghi.Refresh();
            LoaiPhong_DAO lpcn = new LoaiPhong_DAO();
            foreach (var hn in lhn)
            {
                LoaiPhong lp = lpcn.getLoaiPhong(hn.maLoaiPhong);
                dataHoiNghi.Rows.Add(hn.maHoiNghi, hn.tenHoiNghi, hn.soNguoi, lp.tenLoaiPhong);
            }
            dataHoiNghi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private HoiNghi storeData()
        {
            HoiNghi hn = new HoiNghi();
            hn.maHoiNghi = txtMaHN.Text;
            hn.tenHoiNghi = txtTenHN.Text;
            hn.soNguoi = Convert.ToInt32(txtSoNguoi.Text);
            return hn;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoaiPhong_DAO lpcn = new LoaiPhong_DAO();
            HoiNghi_DAO hncn = new HoiNghi_DAO();
            string idlp = lpcn.getMaLoaiPhong(cbbLoaiPhong.Text).maLoaiPhong;
            showData(hncn.dataToLoaiPhong(idlp));
        }

        private void dataHoiNghi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HoiNghi_DAO hncn = new HoiNghi_DAO();
            LoaiPhong_DAO lpcn = new LoaiPhong_DAO();
            string hnid = dataHoiNghi.Rows[e.RowIndex].Cells[0].Value.ToString();
            HoiNghi hn = hncn.getHoiNghi(hnid);
            LoaiPhong lp = lpcn.getLoaiPhong(hn.maLoaiPhong);
            txtMaHN.Text = hnid;
            txtTenHN.Text = hn.tenHoiNghi;
            txtSoNguoi.Text = hn.soNguoi.ToString();
            cbbLoaiPhong.Text = lp.tenLoaiPhong;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if(Annotation())
            {
                HoiNghi hn = storeData();
                LoaiPhong_DAO lpcn = new LoaiPhong_DAO();
                string id = lpcn.getMaLoaiPhong(cbbLoaiPhong.Text).maLoaiPhong;
                hn.maLoaiPhong = id;
                HoiNghi_DAO hncn = new HoiNghi_DAO();
                if (hncn.addData(hn))
                {
                    MessageBox.Show("Thêm thành công!");
                    btnClear_Click(sender, e);
                    showData(hncn.getData());
                }
                else
                {
                    MessageBox.Show("Thêm không thành công!");
                }
            }    
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtMaHN.Text = string.Empty;
            txtTenHN.Text = string.Empty;
            txtSoNguoi.Text = string.Empty;
            cbbLoaiPhong.SelectedIndex = 0;
            txtMaHN.Select();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to exit.", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
            else
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            HoiNghi_DAO hncn = new HoiNghi_DAO();
            DialogResult dr = MessageBox.Show("Do you want to delete this conference.", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (hncn.deleteHoiNghi(txtMaHN.Text))
                {
                    showData(hncn.getData());
                    btnClear_Click(sender, e);
                    MessageBox.Show("Xóa thành công");
                }
            }
            else
            { }
        }

        private void txtMaHN_TextChanged(object sender, EventArgs e)
        {
            HoiNghi_DAO hncn = new HoiNghi_DAO();
            LoaiPhong_DAO lpcn = new LoaiPhong_DAO();
            HoiNghi hn = hncn.getHoiNghi(txtMaHN.Text);
            LoaiPhong lp = lpcn.getLoaiPhong(hn.maLoaiPhong);
            txtTenHN.Text = hn.tenHoiNghi;
            txtSoNguoi.Text = hn.soNguoi.ToString();
            cbbLoaiPhong.Text = lp.tenLoaiPhong;
        }
    }
}
