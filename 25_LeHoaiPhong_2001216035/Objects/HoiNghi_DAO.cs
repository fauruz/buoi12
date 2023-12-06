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
    }
}
