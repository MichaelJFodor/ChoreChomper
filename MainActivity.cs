using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Runtime;
using Android.Widget;

using ChoreChomper.ViewControllers;
using ChoreChomper.Model;

namespace ChoreChomper
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ChoreChomper.ViewControllers.Controller controller;
        ChoreChomper.Model.User user;
        ChoreChomper.Model.Group group;
        public User getMainUser() { return user; }
        public Group getMainGroup() { return group; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // TODO: should likely be done in the loginLayoutController
            SetupUserData();

            // For future reference, remeber to set the content view before assigning the controller
            SetContentView(Resource.Layout.loginLayout);
            controller = new LoginController(this);
        }

        private void SetupUserData()
        {
            user = new User().GenerateTestUser();
            group = new Group().GenerateTestGroup();
            group.AddUser(user);
        }

        public bool ChangeTo(int layout)
        {
            if (layout == Resource.Layout.activity_main)
            {
                SetContentView(Resource.Layout.activity_main);
                controller = new HomeController(this);
                return true;
            }
            else if (layout == Resource.Layout.newUserLayout)
            {
                SetContentView(Resource.Layout.newUserLayout);
                controller = new NewUserController(this);
                return true;
            }
            else if (layout == Resource.Layout.choreListLayout)
            {
                SetContentView(Resource.Layout.choreListLayout);
                controller = new ChoreListController(this);
                return true;
            }
            else if (layout == Resource.Layout.loginLayout)
            {
                SetContentView(Resource.Layout.loginLayout);
                controller = new LoginController(this);
                return true;
            }
            else
                return false;
        }
    }
}