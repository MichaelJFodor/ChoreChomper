using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using UserSpace;
using GroupSpace;
using ChoreSpace;

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

            addButton.Click += (sender, e) =>
            {
                choreNameView.Text = Controller.AddTestChore(choreNameText.Text);
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