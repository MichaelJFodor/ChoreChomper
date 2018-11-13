using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using UserSpace;
using GroupSpace;
using ChoreSpace;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;



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
                choreNameView.Text = choreNameText.Text;
                //choreNameView.Text = Controller.AddTestChore(choreNameText.Text);
            };




            sendData.Click += (sender, e) =>
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://192.168.1.205/chorechomper/api/chore/read_one.php?c_id="+choreNameView.Text);
                WebResponse myResponse = myRequest.GetResponse();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                string result = sr.ReadToEnd();
                sr.Close();
                myResponse.Close();
                dynamic choreTemp = JsonConvert.DeserializeObject(result);
                string name = choreTemp.choreName;
                string date = choreTemp.deadlineTimestamp;
                //choreNameView.Text = choreTemp.choreName + " " + choreTemp.deadlineTimestamp;
                Chore c = new Chore(name, 0, date, false, 0);
                choreNameView.Text = c.GetName() + " due: " + c.GetDeadline();
                //choreNameView.Text = result;
                //Console.WriteLine(result);
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
            Chore chore = new Chore(name, user.GetId(), "1111/11/11", false, 0);
            group.AddChore(chore);
            return (group.GetTaskList().GetHeadChoreName());
        }
    }
}