using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V7.Widget;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
//using Xamarin;

using ChoreChomper.Model;

namespace ChoreChomper.ViewControllers
{
    class Controller
    {
        
    }

    class LoginController : Controller
    {
        EditText usernameText;
        EditText passwordText;
        Button loginButton;
        Button newUserButton;

        public LoginController(MainActivity act)
        {
            usernameText = act.FindViewById<EditText>(Resource.Id.editUsername);
            passwordText = act.FindViewById<EditText>(Resource.Id.editPassword);
            loginButton = act.FindViewById<Button>(Resource.Id.buttonLogin);
            newUserButton = act.FindViewById<Button>(Resource.Id.buttonNewUser);

            loginButton.Click += (sender, e) =>
            {
                // TODO: handle login attempt
                act.ChangeTo(Resource.Layout.choreListLayout);
            };

            newUserButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.newUserLayout);
            };
        }
    }

    class NewUserController : Controller
    {
        EditText usernameText;
        EditText passwordText;
        Button confirmButton;
        Button cancelButton;

        public NewUserController(MainActivity act)
        {
            usernameText = act.FindViewById<EditText>(Resource.Id.editUsername);
            passwordText = act.FindViewById<EditText>(Resource.Id.editPassword);
            confirmButton = act.FindViewById<Button>(Resource.Id.buttonConfirm);
            cancelButton = act.FindViewById<Button>(Resource.Id.buttonCancel);

            confirmButton.Click += (sender, e) =>
            {
                // TODO: handle login attempt
                act.ChangeTo(Resource.Layout.loginLayout);
            };

            cancelButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.loginLayout);
            };
        }
    }

    class HomeController:Controller
    {
        EditText choreNameText;
        Button addChoreButton;
        Button viewChoresButton;

        public HomeController(MainActivity act)
        {
            act.SetContentView(Resource.Layout.activity_main);

            choreNameText = act.FindViewById<EditText>(Resource.Id.editChoreName);
            addChoreButton = act.FindViewById<Button>(Resource.Id.buttonAddChore);
            viewChoresButton = act.FindViewById<Button>(Resource.Id.buttonViewChores);
            
            addChoreButton.Click += (sender, e) =>
            {
                // TODO: consider making addTestChore a bool and using that to determine if the chore was added
                AddTestChore(choreNameText.Text, act);
                choreNameText.Text = "chore added";
            };

            viewChoresButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.choreListLayout);
            };            
        }

        public static string AddTestChore(string name, MainActivity act)
        {
            Chore chore = new Chore(name, act.getMainUser().GetId(), "11/11/111");
            act.getMainGroup().AddChore(chore);
            return (act.getMainGroup().GetTaskList().GetHeadChoreName());
        }
    }

    class MenuController : Controller
    {
        Button testGoToTasksButton;
        Button testGoToGroupsButton;
        ListView menuListView;
        List<string> menuOptions = new List<string>
        {
            "Profile",
            "Groups",
            "Task List",
            "Back",
            "Log Out"
        };

        public MenuController(MainActivity act)
        {
            testGoToTasksButton = act.FindViewById<Button>(Resource.Id.buttonMenuToTaskList);
            testGoToGroupsButton = act.FindViewById<Button>(Resource.Id.buttonMenuToGroupList);

            testGoToTasksButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.choreListLayout);
            };

            testGoToGroupsButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.groupListLayout);
            };
        }
    }

    class ChoreListController:Controller
    {
        Button mainMenuButton;
        Button addChoreButton;
        ListView choreListView;

        public ChoreListController(MainActivity act)
        {
            mainMenuButton = act.FindViewById<Button>(Resource.Id.menuButtonChoreList);
            addChoreButton = act.FindViewById<Button>(Resource.Id.buttonAddChore);
            choreListView = act.FindViewById<ListView>(Resource.Id.listOfChores);

            SetupList(act);

            mainMenuButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.mainMenuLayout);
            };

            addChoreButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.choreCreateLayout);
            };
        }

        private void SetupTestList(MainActivity act)
        {
            List<string> itemList = new List<string>
            {
                "item 0",
                "item 1",
                "item 2",
                "item 3",
                "item 4",
                "item 5"
            };

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(act, Android.Resource.Layout.SimpleListItem1, itemList);
            choreListView.Adapter = adapter;
        }

        private void SetupList(MainActivity act)
        {
            List<string> choreNames = act.getMainGroup().GetTaskList().GetChoreNames();
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(act, Android.Resource.Layout.SimpleListItem1, choreNames);
            choreListView.Adapter = adapter;
            // TODO: add stuff to chore list and make it display them
        }
    }

    class ChoreCreationController : Controller
    {
        EditText newChoreNameText;
        Button confirmChoreButton;
        Button backButton;

        public ChoreCreationController(MainActivity act)
        {
            newChoreNameText = act.FindViewById<EditText>(Resource.Id.editNewChoreName);
            confirmChoreButton = act.FindViewById<Button>(Resource.Id.buttonAddChore);
            backButton = act.FindViewById<Button>(Resource.Id.buttonCreateChoreToChoreList);
            
            confirmChoreButton.Click += (sender, e) =>
            {
                // TODO: consider making addTestChore a bool and using that to determine if the chore was added
                AddTestChore(newChoreNameText.Text, act);
                newChoreNameText.Text = "chore added";
                act.ChangeTo(Resource.Layout.choreListLayout);
            };

            backButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.choreListLayout);
            };
        }

        public static string AddTestChore(string name, MainActivity act)
        {
            Chore chore = new Chore(name, act.getMainUser().GetId(), "11/11/111");
            act.getMainGroup().AddChore(chore);
            return (act.getMainGroup().GetTaskList().GetHeadChoreName());
        }
    }

    class ChoreEditController : Controller
    {
        EditText desiredChoreNameText;
        Button confirmChoreButton;
        Button completeChoreButton;
        Button backButton;

        public ChoreEditController(MainActivity act)
        {
            desiredChoreNameText = act.FindViewById<EditText>(Resource.Id.editChoreEditText);
            confirmChoreButton = act.FindViewById<Button>(Resource.Id.buttonConfirmEditChore);
            completeChoreButton = act.FindViewById<Button>(Resource.Id.buttonCompleteChore);
            backButton = act.FindViewById<Button>(Resource.Id.buttonChoreEditToChoreList);

            backButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.choreListLayout);
            };
        }

        public static string AddTestChore(string name, MainActivity act)
        {
            Chore chore = new Chore(name, act.getMainUser().GetId(), "11/11/111");
            act.getMainGroup().AddChore(chore);
            return (act.getMainGroup().GetTaskList().GetHeadChoreName());
        }
    }

    class GroupListController : Controller
    {
        Button mainMenuButton;
        Button joinGroupButton;
        Button createGroupButton;
        ListView groupListView;

        public GroupListController(MainActivity act)
        {
            mainMenuButton = act.FindViewById<Button>(Resource.Id.menuButtonGroupList);
            joinGroupButton = act.FindViewById<Button>(Resource.Id.buttonJoinGroup);
            createGroupButton = act.FindViewById<Button>(Resource.Id.buttonCreateGroup);
            groupListView = act.FindViewById<ListView>(Resource.Id.listOfGroups);

            SetupTestList(act);

            mainMenuButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.mainMenuLayout);
            };

            joinGroupButton.Click += (sender, e) =>
            {
                //act.ChangeTo(Resource.Layout.joinGroupLayout);
            };

            createGroupButton.Click += (sender, e) =>
            {
                //act.ChangeTo(Resource.Layout.createGroupLayout);
            };
        }

        private void SetupTestList(MainActivity act)
        {
            List<string> itemList = new List<string>
            {
                "item 0",
                "item 1",
                "item 2",
                "item 3",
                "item 4",
                "item 5"
            };

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(act, Android.Resource.Layout.SimpleListItem1, itemList);
            groupListView.Adapter = adapter;
        }

        private void SetupList(MainActivity act)
        {
            // TODO: add groups to group list and make it display them
        }
    }

    class GroupCreateController : Controller
    {
        EditText newGroupNameText;
        Button confirmGroupButton;
        Button backButton;

        public GroupCreateController(MainActivity act)
        {
            newGroupNameText = act.FindViewById<EditText>(Resource.Id.editNewGroupName);
            confirmGroupButton = act.FindViewById<Button>(Resource.Id.buttonConfirmNewGroup);
            backButton = act.FindViewById<Button>(Resource.Id.buttonGroupCreateLayoutToGroupList);

            confirmGroupButton.Click += (sender, e) =>
            {
                // TODO: consider making addTestChore a bool and using that to determine if the chore was added
                AddTestChore(newGroupNameText.Text, act);
                newGroupNameText.Text = "chore added";
                act.ChangeTo(Resource.Layout.choreListLayout);
            };

            backButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.choreListLayout);
            };
        }

        public static string AddTestChore(string name, MainActivity act)
        {
            Chore chore = new Chore(name, act.getMainUser().GetId(), "11/11/111");
            act.getMainGroup().AddChore(chore);
            return (act.getMainGroup().GetTaskList().GetHeadChoreName());
        }
    }

    class JoinGroupController : Controller
    {
        EditText newGroupNameText;
        Button joinGroupButton;
        Button backButton;

        public JoinGroupController(MainActivity act)
        {/*
            newGroupNameText = act.FindViewById<EditText>(Resource.Id.editJoinGroupName);
            joinGroupButton = act.FindViewById<Button>(Resource.Id.buttonConfirmJoinGroup);
            backButton = act.FindViewById<Button>(Resource.Id.buttonJoinGroupToChoreList);

            backButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.choreListLayout);
            };*/
        }
    }
}