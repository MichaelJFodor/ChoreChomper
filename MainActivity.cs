using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using UserSpace;
using GroupSpace;
using ChoreSpace;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;



namespace ChoreChomper
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            EditText choreNameText = FindViewById<EditText>(Resource.Id.editChoreName);
            TextView choreNameView = FindViewById<TextView>(Resource.Id.textChoreDisp);
            Button addButton = FindViewById<Button>(Resource.Id.buttonAddChore);
            Button sendData = FindViewById<Button>(Resource.Id.sendToDataBase);
            addButton.Click += (sender, e) =>
            {
                choreNameView.Text = Controller.AddTestChore(choreNameText.Text);
            };
            string server = "192.168.0.12";
            string database = "chorechomper";
            string password = "chorechomper";
            string uid = "chorechomper";
            string connectionString;
            connectionString = "SERVER=" + server + "; PORT = 3306 ;" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            MySqlConnection mycon = new MySqlConnection(connectionString);
           
            string sql = "insert into chorechomper.chores(chore_title) values(@name1)";
            sendData.Click += (sender, e) =>
            {
                 mycon.Open();
                MySqlCommand cmd = new MySqlCommand(sql, mycon);
                cmd.Parameters.Add("@name1", MySqlDbType.VarChar);
                cmd.Parameters["@name"].Value = "testfromchorechomperapp";
                cmd.ExecuteNonQuery();
                mycon.Close();
            };

        }
    }

    public static class Controller
    {
        public static string AddTestChore(string name)
        {
            User user = new User().GenerateTestUser();
            Group group = new Group().GenerateTestGroup();
            group.AddUser(user);
            Chore chore = new Chore(name, user.GetId(), "11/11/111");
            group.AddChore(chore);
            return (group.GetTaskList().GetHeadChoreName());
        }
    }
}