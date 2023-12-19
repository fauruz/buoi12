using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace _25_LeHoaiPhong_2001216035.Objects
{
    internal class HoiNghi_DAO
    {
        string constr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
        public List<HoiNghi> getData()
        {
            List<HoiNghi> lhn = new List<HoiNghi>();
            SqlConnection con = new SqlConnection(constr);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.CommandText = "select * from HoiNghi";
                da.SelectCommand.Connection = con;
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    HoiNghi hn = new HoiNghi();
                    hn.maHoiNghi = row["maHoiNghi"].ToString();
                    hn.tenHoiNghi = row["tenHoiNghi"].ToString();
                    hn.soNguoi = Convert.ToInt32(row["SoNguoi"].ToString());
                    hn.maLoaiPhong = row["maLoaiPhong"].ToString();
                    lhn.Add(hn);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return lhn;
        }
        public HoiNghi getHoiNghi(string mahoinghi)
        {
            HoiNghi hn = new HoiNghi();
            string sql = "select * from HoiNghi where maHoiNghi = '" + mahoinghi + "'";
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sql,con);
            cmd.CommandType = CommandType.Text;
            
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                hn.maHoiNghi = mahoinghi;
                hn.tenHoiNghi = rdr.GetValue(1).ToString();
                hn.soNguoi = Convert.ToInt32(rdr.GetValue(2).ToString());
                hn.maLoaiPhong = rdr.GetValue(3).ToString();
            }
            return hn;
        }
        public bool addData(HoiNghi hn)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string sql = "insert into dbo.HoiNghi values('" + hn.maHoiNghi + "',N'" + hn.tenHoiNghi + "','" + hn.soNguoi + "','" + hn.maLoaiPhong + "')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        public List<HoiNghi> dataToLoaiPhong(string maloaiphong)
        {
            List<HoiNghi> lhn = new List<HoiNghi>();
            SqlConnection con = new SqlConnection(constr);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.CommandText = "select * from HoiNghi Where maLoaiPhong = '"+ maloaiphong + "' ";
                da.SelectCommand.Connection = con;
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    HoiNghi hn = new HoiNghi();
                    hn.maHoiNghi = row["maHoiNghi"].ToString();
                    hn.tenHoiNghi = row["tenHoiNghi"].ToString();
                    hn.soNguoi = Convert.ToInt32(row["SoNguoi"].ToString());
                    hn.maLoaiPhong = row["maLoaiPhong"].ToString();
                    lhn.Add(hn);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return lhn;
        }
        public bool deleteHoiNghi(string mahoinghi)
        {
            int kt = 0;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string sql = "select count(*) from HoiNghi where maHoiNghi = '" + mahoinghi + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            kt = (int)cmd.ExecuteScalar();
            if (kt == 0)
            {
                MessageBox.Show("Không tồn tại hội nghị!");
                return false;
            }
            else
            {
                try
                {
                    string sql1 = "Delete From HoiNghi where maHoiNghi = '" + mahoinghi + "'";
                    SqlCommand cmd1 = new SqlCommand(sql1, con);
                    cmd1.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            con.Close();
            return true;
        }
    }
}
