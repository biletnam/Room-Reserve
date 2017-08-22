using System;
using System.Collections;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RoomReserve
{
    public partial class _Default : Page
    {
        private ArrayList _hours; 
        
        protected void Page_Load(object sender, EventArgs e)
        {
            StyleBookButton();
            AuthenticateUser();
            CreateLegendColors();
            //if (Calendar1.SelectedDate < Calendar1.TodaysDate)
            //{
            //    //System.Diagnostics.Debug.WriteLine("Vliza");
            //    Calendar1.SelectedDate = Calendar1.TodaysDate;
            //    Calendar1.VisibleDate = Calendar1.TodaysDate;
            //}
            //else
            //{
            Calendar1.VisibleDate = Calendar1.SelectedDate;
            //}
            System.Diagnostics.Debug.WriteLine(Calendar1.VisibleDate);
            System.Diagnostics.Debug.WriteLine(Calendar1.SelectedDate);
            _hours = new ArrayList();
            _hours.AddRange(new [] {8,9,10,11,12,13,14,15,16,17});
            CreateRoomSigns();
            CreateHourSigns();
            CreateTableButtons();
            GetSqlBookingInfo();
        }

        private void StyleBookButton()
        {
            Button1.Style.Add("border-radius", "10px");
            Button1.Style.Add("width", "200px");
            Button1.Style.Add("margin-left", "620px");
            Button1.Style.Add("top", "-70px");
        }

        private void GetSqlBookingInfo()
        {
            SqlDataReader rdr = null;
            SqlConnection conn =
                new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\RoomDatabase.mdf;Initial Catalog=RoomDatabase;Integrated Security=True");
            conn.Open();
            SqlCommand sqlComm = new SqlCommand("SELECT Bookings.Room_Id, From_Time, Capacity, Multimedia FROM Bookings, Rooms WHERE Bookings.Room_Id = Rooms.Room_Id", conn);
            rdr = sqlComm.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    var roomId = rdr.GetInt32(0) - 1;
                    var fromTime = rdr.GetDateTime(1);

                    if (Calendar1.SelectedDate == fromTime.Date)
                    {
                        //TODO fromtime.hour+1) or hour) + 1
                        var button = Table1.Rows[roomId + 1].Cells[_hours.IndexOf(fromTime.Hour) + 1].Controls[0] as Button;
                        button.BackColor = Color.Red;
                        button.Text = rdr.GetInt32(2).ToString();
                        var multimedia = rdr.GetString(3);
                        if (multimedia.Equals("TV"))
                        {
                            button.Text += " + TV";
                        } else if (multimedia.Equals("Projector"))
                        {
                            button.Text += " + Pr";
                        }
                        button.Enabled = false;
                    }
                }
            }
            conn.Close();
        }

        private void CreateTableButtons()
        {
            int buttonId = 0;
            for (int j = 1; j < Table1.Rows.Count; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    var cell = new TableCell();
                    var button = CreateButton(ref buttonId);
                    button.Style.Add("border-radius", "10px");
                    cell.Controls.Add(button);
                    Table1.Rows[j].Cells.Add(cell);
                }
            }
        }

        private Button CreateButton(ref int buttonId)
        {
            Button button = new Button
            {
                Width = 80,
                Height = 40,
                BackColor = Color.LawnGreen,
                Enabled = true
            };
            button.Click += Button_OnClick;
            button.ID = buttonId.ToString();
            buttonId++;
            GetFreeRoomSize(buttonId, button);
            return button;
        }

        private static void GetFreeRoomSize(int buttonId, Button button)
        {
            SqlDataReader rdr = null;
            SqlConnection conn =
                new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\RoomDatabase.mdf;Initial Catalog=RoomDatabase;Integrated Security=True");
            conn.Open();
            SqlCommand sqlComm = new SqlCommand("SELECT * FROM Rooms", conn);
            rdr = sqlComm.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    if (rdr.GetInt32(0) == (buttonId + 9)/10)
                    {
                        button.Text = rdr.GetInt32(1).ToString();
                        var multimedia = rdr.GetString(2);
                        if (multimedia.Equals("TV"))
                        {
                            button.Text += "+TV";
                        }
                        else if (multimedia.Equals("Projector"))
                        {
                            button.Text += "+Pr";
                        }
                    }
                }
            }
            conn.Close();
        }

        private void CreateHourSigns()
        {
            for (int i = 0; i < 10; i++)
            {
                var cell = new TableCell();
                cell.Text = $"{i + 8}:00";
                Table1.Rows[0].Cells.Add(cell);
            }
        }

        private void CreateRoomSigns()
        {
            for (int i = 0; i <= 10; i++)
            {
                var cell = new TableCell();
                cell.Text = i == 0 ? string.Empty : $"Room {i}";
                Table1.Rows[i].Cells.Add(cell);
            }
        }

        private void CreateLegendColors()
        {
            Button2.BackColor = Color.LawnGreen;
            Button3.BackColor = Color.Red;
            Button2.Enabled = false;
            Button3.Enabled = false;
            Button2.BorderStyle = BorderStyle.None;
            Button3.BorderStyle = BorderStyle.None;
        }

        private void AuthenticateUser()
        {
            bool loggedIn = (System.Web.HttpContext.Current.User != null) &&
                            System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!loggedIn)
            {
                Response.Redirect("Account/Login.aspx");
            }
        }

        protected void Button_OnClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.BackColor != Color.Red)
            {
                if (button.BackColor == Color.Yellow)
                {
                    button.BackColor = Color.LawnGreen;
                }
                else
                {
                    button.BackColor = Color.Yellow;
                }
                
            }
        }
        

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\RoomDatabase.mdf;Initial Catalog=RoomDatabase;Integrated Security=True");
            conn.Open();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    var button = Table1.Rows[i].Cells[j].Controls[0] as Button;
                    if (button != null && button.BackColor == Color.Yellow)
                    {
                        int id = int.Parse(button.ID);
                        int room = id / (Table1.Rows.Count - 1);
                        int roomTime = id % (Table1.Rows[1].Cells.Count - 1);
                        var calendarTime = Calendar1.SelectedDate;
                        string fromTime = calendarTime.Date.ToString("yyyy-MM-dd");
                        var time = new TimeSpan(Convert.ToInt32(_hours[roomTime]), 0, 0);
                        fromTime += " " + time.ToString();
                        var comm = new SqlCommand("INSERT INTO Bookings VALUES ("+ (room + 1) + ",'" + fromTime + "','" + fromTime + "','kosyo2105@abv.bg');", conn);
                        comm.ExecuteNonQuery();
                        button.BackColor = Color.Red;
                        button.Enabled = false;
                    } 
                }
            }
                conn.Close();
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (TableRow row in Table1.Rows )
            {
                row.Cells.Clear();
                
            }
            Page_Load(sender, e);
        }
    }
}