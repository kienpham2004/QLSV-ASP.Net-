using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Doanbaove.AllClass;

namespace Doanbaove
{
    public partial class dsmonhocsv : System.Web.UI.Page
    {
        Clsconnect cls_con = new Clsconnect();
        SqlCommand sqlcm, sqlcm1;
        protected void Page_Load(object sender, EventArgs e)
        {
            string masv = Request.QueryString["user"].ToString();
            try
            {
                cls_con.connect_Data();
                string query1 = "Select tensv, masv, tencn,Case Gioitinh when 1 then N'Nữ' else N'Nam' end as 'Giới tính' from tbl_sinhvien inner join tbl_chuyennganh on chuyennganh = macn where masv=" + masv;
                string tensv = "", masv1 = "", cn = "", gt="";
                sqlcm1 = new SqlCommand(query1, cls_con.con);
                SqlDataReader re1 = sqlcm1.ExecuteReader();
                while (re1.Read())
                {
                    tensv = re1.GetValue(0).ToString();
                    masv1 = re1.GetValue(1).ToString();
                    cn = re1.GetValue(2).ToString();
                    gt = re1.GetValue(3).ToString();
                }
                re1.Close();
                lbl_tensv.Text =" Họ và tên: "+ tensv;
                lbl_masv.Text = "Mã sinh viên: "+ masv1;
                lbl_cn.Text ="Chuyên ngành:"+ cn;
                lbl_gt.Text = "Giới tính:" + gt;

                cls_con.connect_Data();
                string st_sql = "SELECT tbl_monhoc.MaMH, tenmh,tinchi FROM tbl_monhoc inner join tbl_addmonhoc on tbl_monhoc.mamh = tbl_addmonhoc.mamh inner join tbl_sinhvien on tbl_sinhvien.masv = tbl_addmonhoc.masv where tbl_sinhvien.masv=" + masv;

                sqlcm = new SqlCommand(st_sql, cls_con.con);
                SqlDataReader re = sqlcm.ExecuteReader();

                string st_kq = "";
                byte i = 0;
                while (re.Read())
                {
                    i++;
                    st_kq = st_kq + "  <tr><td>" + i.ToString() + "</td><td>" + re.GetValue(0).ToString() + "</td><td>" + re.GetValue(1).ToString() + "</td><td>" + re.GetValue(2).ToString() + "</td></tr > ";
                }
                re.Close();
                st_kq = st_kq + "</table>";
                dsach.Text = st_kq;
            }
            finally
            {
                cls_con.close_Data();
            }
        }

        protected void btt_del_Click(object sender, EventArgs e)
        {
            string masv = Request.QueryString["user"].ToString();
            try
            {
                string st_mmh, st_sql, st_mdk;
                st_mdk = tbx_mdk.Text.ToUpper();
                st_mmh = tbx_mamh.Text.ToUpper();
                cls_con.connect_Data();
                st_sql = "SELECT count(mamh) FROM tbl_addmonhoc WHERE mamh='" + st_mmh + "' and masv=" + masv;
                sqlcm = new SqlCommand(st_sql, cls_con.con);
                int k = Convert.ToInt16(sqlcm.ExecuteScalar());
                if (k == 0)
                {
                    lbl_tb.Text = "Lỗi: Sinh viên không học môn này";
                    lbl_tb.Visible = true;

                }
                else
                {

                    int check;
                    st_sql = "delete from tbl_diem where mamh=" + st_mmh + " and masv=" + masv;
                    sqlcm = new SqlCommand(st_sql, cls_con.con);
                    st_sql = "delete from tbl_addmonhoc where mamh=" + st_mmh + " and masv=" + masv;
                    sqlcm = new SqlCommand(st_sql, cls_con.con);
                    check = sqlcm.ExecuteNonQuery();
                    if (check == 1)
                    {
                        Response.Redirect("~/dsmonhocsv.aspx?user=" + masv);
                        lbl_tb.Text = "Xóa sinh viên thành công!";
                        lbl_tb.Visible = true;
                    }
                    else
                    {
                        lbl_tb.Text = "Lỗi: Xóa sinh viên không thành công!";
                        lbl_tb.Visible = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                Response.Write(ex);
            }
            finally
            {
                cls_con.close_Data();
            }
        }

        protected void btt_add_Click(object sender, EventArgs e)
        {
            string masv = Request.QueryString["user"].ToString();
            try
            {
                string st_mmh, st_sql, st_mdk;
                st_mdk = tbx_mdk.Text.ToUpper();
                st_mmh = tbx_mamh.Text.ToUpper();
                cls_con.connect_Data();
                st_sql = "SELECT count(madk) FROM tbl_addmonhoc WHERE madk='" + st_mdk + "'";
                sqlcm = new SqlCommand(st_sql, cls_con.con);
                int k = Convert.ToInt16(sqlcm.ExecuteScalar());
                st_sql = "SELECT count(masv) FROM tbl_addmonhoc WHERE mamh='" + st_mmh + "' and masv ='" + masv + "'";
                sqlcm = new SqlCommand(st_sql, cls_con.con);
                int l = Convert.ToInt16(sqlcm.ExecuteScalar());
                if (l > 0 && k > 0)
                {
                    lbl_tb.Text = "Lỗi: Mã điều kiện đã tồn tại và mã sinh viên đã có trong môn học này!";
                    lbl_tb.Visible = true;

                }
                else if (l > 0)
                {
                    lbl_tb.Text = "Lỗi: Mã sinh viên đã học môn học này!";
                    lbl_tb.Visible = true;
                }
                else if (k > 0)
                {
                    lbl_tb.Text = "Lỗi: Mã điều kiện đã tồn tại!";
                    lbl_tb.Visible = true;
                }
                else
                {

                    int check, check1;
                    st_sql = "INSERT INTO dbo.tbl_diem VALUES (" + st_mdk + ",'" + masv + "','" + st_mmh + "',0,0,0,0)";
                    sqlcm = new SqlCommand(st_sql, cls_con.con);
                    check1 = sqlcm.ExecuteNonQuery();
                    st_sql = "INSERT INTO tbl_addmonhoc (Masv,mamh, madk) VALUES('" + masv + "','" + st_mmh + "','" + st_mdk + "');";
                    sqlcm = new SqlCommand(st_sql, cls_con.con);
                    check = sqlcm.ExecuteNonQuery();
                    if (check > 0 && check1 > 0)
                    {
                        Response.Redirect("~/dsmonhocsv.aspx?user=" + masv);
                        lbl_tb.Text = "Thêm môn học thành công!";
                        lbl_tb.Visible = true;
                    }
                    else
                    {
                        lbl_tb.Text = "Lỗi: Thêm môn học không thành công!";
                        lbl_tb.Visible = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                Response.Write(ex);
            }
            finally
            {
                cls_con.close_Data();
            }
        }

        protected void tbx_mamh_TextChanged(object sender, EventArgs e)
        {

        }

        protected void tbx_del_TextChanged(object sender, EventArgs e)
        {

        }
    }
}