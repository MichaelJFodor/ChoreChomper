using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;

using ChoreChomper.Controller;
using ChoreChomper.Model;
using ChoreChomper.Data;

namespace ChoreChomper
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ChoreChomper.Controller.Controller controller;
        SessionData currentSession = new SessionData();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // TODO: should likely be done in the loginLayoutController
            SetupUserData();

            // For future reference, remeber to set the content view before assigning the controller
            SetContentView(Resource.Layout.loginLayout);
            controller = new LoginController(this);
        }
        
        public User getMainUser() { return currentSession.getCurrentUser(); }
        public Group getMainGroup() { return currentSession.getUsersGroups()[0]; }
        public List<Group> getUsersGroups() { return currentSession.getUsersGroups(); }
        public Group GetTargetGroup() { return currentSession.GetTargetGroup(); }
        public Chore GetTargetChore() { return currentSession.GetTargetChore(); }
        public Group SetTargetGroup(Group group)
        {
            currentSession.SetTargetGroup(group);
            return GetTargetGroup();
        }
        public Chore SetTargetChore(Chore chore)
        {
            currentSession.SetTargetChore(chore);
            return GetTargetChore();
        }

        private void AttemptLogin(string username, string password)
        {
            bool credentialsAreValid = true;
            // TODO: check database to see if these credentials are valid
            if (credentialsAreValid)
            {
                currentSession.LoadUser(username);
            }
        }

        private void SetupUserData()
        {
            currentSession.GenerateTestSession();
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
            else if (layout == Resource.Layout.loginLayout)
            {
                SetContentView(Resource.Layout.loginLayout);
                controller = new LoginController(this);
                return true;
            }
            else if (layout == Resource.Layout.mainMenuLayout)
            {
                SetContentView(Resource.Layout.mainMenuLayout);
                controller = new MenuController(this);
                return true;
            }
            else if (layout == Resource.Layout.choreListLayout)
            {
                SetContentView(Resource.Layout.choreListLayout);
                controller = new ChoreListController(this);
                return true;
            }
            else if (layout == Resource.Layout.choreCreateLayout)
            {
                SetContentView(Resource.Layout.choreCreateLayout);
                controller = new ChoreCreationController(this);
                return true;
            }
            else if (layout == Resource.Layout.choreEditLayout)
            {
                SetContentView(Resource.Layout.choreEditLayout);
                controller = new ChoreEditController(this);
                return true;
            }
            else if (layout == Resource.Layout.groupListLayout)
            {
                SetContentView(Resource.Layout.groupListLayout);
                controller = new GroupListController(this);
                return true;
            }
            else if (layout == Resource.Layout.groupCreateLayout)
            {
                SetContentView(Resource.Layout.groupCreateLayout);
                controller = new GroupCreateController(this);
                return true;
            }/*
            else if (layout == Resource.Layout.joinGroupLayout)
            {
                SetContentView(Resource.Layout.groupCreateLayout);
                controller = new JoinGroupController(this);
                return true;
            }*/
            else
                return false;
        }
    }
}