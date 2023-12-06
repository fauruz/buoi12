using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
                return false;
            }
            if (txtTenHN.Text == string.Empty)
            {
                MessageBox.Show("Nhập tên hội nghị!");
                return false;
            }
            if (txtSoNguoi.Text == string.Empty)
            {
                MessageBox.Show("Nhập số người tham gia!");
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
            showData();
        }
        private void showData()
        {
            dataHoiNghi.Rows.Clear();
            dataHoiNghi.Refresh();
            LoaiPhong_DAO lpcn = new LoaiPhong_DAO();
            HoiNghi_DAO hncn = new HoiNghi_DAO();
            foreach (var hn in hncn.getData())
            {
                ; LoaiPhong lp = lpcn.getLoaiPhong(hn.maLoaiPhong);
                dataHoiNghi.Rows.Add(hn.maHoiNghi, hn.tenHoiNghi, hn.soNguoi, lp.tenLoaiPhong);
            }
            dataHoiNghi.Visible = true;
        }
        private HoiNghi storeData()
        {
            if(Annotation())
            {
                HoiNghi hn = new HoiNghi();
                hn.maHoiNghi = txtMaHN.Text;
                hn.tenHoiNghi = txtTenHN.Text;
                hn.soNguoi = Convert.ToInt32(txtSoNguoi.Text);
                return hn;
            }
            return null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            HoiNghi hn = storeData();
            LoaiPhong_DAO lpcn = new LoaiPhong_DAO();
            string id = lpcn.getMaLoaiPhong(cbbLoaiPhong.Text).maLoaiPhong;
            hn.maLoaiPhong = id;
            HoiNghi_DAO hncn = new HoiNghi_DAO();
            if(hncn.addData(hn))
            {
                MessageBox.Show("Thêm thành công!");
                showData();
            }
            else
            {
                MessageBox.Show("Thêm không thành công!");
            }    
        }
    }
}
