using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace _25_LeHoaiPhong_2001216035.Objects
{
    internal class LoaiPhong_DAO
    {
        string constr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
        public List<LoaiPhong> getData()
        {
            List<LoaiPhong> llp = new List<LoaiPhong>();
            SqlConnection con = new SqlConnection(constr);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.CommandText = "select * from LoaiPhong";
                da.SelectCommand.Connection = con;
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    LoaiPhong lp = new LoaiPhong();
                    lp.maLoaiPhong = row["maLoaiPhong"].ToString();
                    lp.tenLoaiPhong = row["tenLoaiPhong"].ToString();
                    llp.Add(lp);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return llp;
        }
        public LoaiPhong getLoaiPhong(string maloaiphong)
        {
            string sql = "select * from LoaiPhong Where maLoaiPhong = '" + maloaiphong + "' ";
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            LoaiPhong lp = new LoaiPhong();
            while (rdr.Read())
            {
                lp.maLoaiPhong = rdr.GetValue(0).ToString();
                lp.tenLoaiPhong= rdr.GetValue(1).ToString();
            }
            con.Close();
            return (lp);
        }
        public LoaiPhong getMaLoaiPhong(string tenloaiphong)
        {
            string sql = "select * from LoaiPhong Where tenLoaiPhong = N'" + tenloaiphong + "' ";
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            LoaiPhong lp = new LoaiPhong();
            while (rdr.Read())
            {
                lp.maLoaiPhong = rdr.GetValue(0).ToString();
                lp.tenLoaiPhong = rdr.GetValue(1).ToString();
            }
            con.Close();
            return (lp);
        }
        public bool addData(LoaiPhong lp)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string sql = "insert into dbo.LoaiPhong values('" + lp.maLoaiPhong + "',N'" + lp.tenLoaiPhong + "')";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        public bool deleteLoaiPhong(string maloaiphong)
        {
            int kt = 0;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string sql = "select count(*) from LoaiPhong where maLoaiPhong = '" + maloaiphong + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            kt = (int)cmd.ExecuteScalar();
            if (kt == 0)
            {
                MessageBox.Show("Không tồn tại loại phòng!");
                return false;
            }
            else
            {
                try
                {
                    string sql1 = "Delete From LoaiPhong where maLoaiPhong = '" + maloaiphong + "'";
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
