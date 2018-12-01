using Android.App;
using Android.OS;
using Android.Support.V7.App;

using ChoreChomper.Controller;
using ChoreChomper.Data;

namespace ChoreChomper
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Controller.Controller controller;
        SessionData currentSession = new SessionData();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // For future reference, remeber to set the content view before assigning the controller
            SetContentView(Resource.Layout.loginLayout);
            controller = new LoginController(this);
        }

        public SessionData GetSessionData() { return currentSession; }


        public bool ChangeTo(int layout)
        {
            bool valid = true;

            if (layout == Resource.Layout.newUserLayout)
            {
                SetContentView(layout);
                controller = new NewUserController(this);
            }
            else if (layout == Resource.Layout.loginLayout)
            {
                SetContentView(layout);
                controller = new LoginController(this);
            }
            else if (layout == Resource.Layout.mainMenuLayout)
            {
                SetContentView(layout);
                controller = new MenuController(this);
            }
            else if (layout == Resource.Layout.choreListLayout)
            {
                SetContentView(layout);
                controller = new ChoreListController(this);
            }
            else if (layout == Resource.Layout.choreListSettingsLayout)
            {
                SetContentView(layout);
                controller = new ChoreListSettingsController(this);
            }
            else if (layout == Resource.Layout.choreCreateLayout)
            {
                SetContentView(layout);
                controller = new ChoreCreationController(this);
            }
            else if (layout == Resource.Layout.choreEditLayout)
            {
                SetContentView(layout);
                controller = new ChoreEditController(this);
            }
            else if (layout == Resource.Layout.groupListLayout)
            {
                SetContentView(layout);
                controller = new GroupListController(this);
            }
            else if (layout == Resource.Layout.groupCreateLayout)
            {
                SetContentView(layout);
                controller = new GroupCreateController(this);
            }
            else if (layout == Resource.Layout.joinGroupLayout)
            {
                SetContentView(layout);
                controller = new JoinGroupController(this);
            }
            else
            {
                valid = false;
            }

            return valid;
        }
    }
}