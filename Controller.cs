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

namespace ChoreChomper.Controller
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
        List<Chore> chores;
        Button mainMenuButton;
        Button addChoreButton;
        ListView choreListView;
        
        public ChoreListController(MainActivity act)
        {
            chores = SetupChores(act);
            mainMenuButton = act.FindViewById<Button>(Resource.Id.navButtonChoreList);
            addChoreButton = act.FindViewById<Button>(Resource.Id.buttonAddChore);
            choreListView = act.FindViewById<ListView>(Resource.Id.listOfChores);

            SetupChores(act);
            SetupList(act);

            mainMenuButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.mainMenuLayout);
            };

            addChoreButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.choreCreateLayout);
            };

            choreListView.ItemClick += (sender, e) =>
            {
                Chore targetChore = chores[(int)e.Id];
                act.SetTargetChore(targetChore);
                act.ChangeTo(Resource.Layout.choreEditLayout);
            };
        }

        private List<Chore> SetupChores (MainActivity act)
        {
            List<Chore> fullList = act.getMainGroup().GetTaskList().GetChoreList();
            List<Chore> ListOfIncomplete = new List<Chore>();
            foreach (Chore c in fullList)
            {
                if (!c.isComplete())
                    ListOfIncomplete.Add(c);
            }
            return ListOfIncomplete;
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
            List<string> choreNames = new List<string>();
            foreach (Chore c in chores)
            {
                choreNames.Add(c.GetName());
            }
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
        Chore targetChore;
        EditText desiredChoreNameText;
        Button confirmChoreButton;
        Button completeChoreButton;
        Button backButton;

        public ChoreEditController(MainActivity act)
        {
            targetChore = act.GetTargetChore();
            desiredChoreNameText = act.FindViewById<EditText>(Resource.Id.editChoreEditText);
            confirmChoreButton = act.FindViewById<Button>(Resource.Id.buttonConfirmEditChore);
            completeChoreButton = act.FindViewById<Button>(Resource.Id.buttonCompleteChore);
            backButton = act.FindViewById<Button>(Resource.Id.buttonChoreEditToChoreList);
            desiredChoreNameText.Text = targetChore.GetName();

            confirmChoreButton.Click += (sender, e) =>
            {
                ApplyEdits();
                act.ChangeTo(Resource.Layout.choreListLayout);
            };
            
            void ApplyEdits()
            {
                targetChore.SetName(desiredChoreNameText.Text);
            }

            completeChoreButton.Click += (sender, e) =>
            {
                targetChore.SetComplete(true);
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

    class GroupListController : Controller
    {
        List<Group> groups;
        Button mainMenuButton;
        Button joinGroupButton;
        Button createGroupButton;
        ListView groupListView;

        public GroupListController(MainActivity act)
        {
            mainMenuButton = act.FindViewById<Button>(Resource.Id.navButtonGroupList);
            joinGroupButton = act.FindViewById<Button>(Resource.Id.buttonJoinGroup);
            createGroupButton = act.FindViewById<Button>(Resource.Id.buttonCreateGroup);
            groupListView = act.FindViewById<ListView>(Resource.Id.listOfGroups);

            SetupList(act);

            mainMenuButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.mainMenuLayout);
            };

            groupListView.ItemClick += (sender, e) =>
            {
                Group targetGroup = groups[(int)e.Id];
                act.SetTargetGroup(targetGroup);
                SetupList(act);
            };

            joinGroupButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.joinGroupLayout);
            };

            createGroupButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.groupCreateLayout);
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
            groups = act.getUsersGroups();
            List<Group> orderedGroups = CreateOrderedGroupList(groups, act);
            List<string> groupNames = GetNamesOfGroups(orderedGroups);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(act, Android.Resource.Layout.SimpleListItem1, groupNames);
            groupListView.Adapter = adapter;
        }

        private List<Group> CreateOrderedGroupList(List<Group> groups, MainActivity act)
        {
            List<Group> orderedGroups = new List<Group>();
            Group currentTargetGroup = act.GetTargetGroup();
            foreach (Group g in groups)
            {
                if (g == currentTargetGroup)
                    orderedGroups.Insert(0, g);
                else
                    orderedGroups.Add(g);
            }
            return orderedGroups;
        }

        private List<string> GetNamesOfGroups(List<Group> orderedGroups)
        {
            List<string> names = new List<string>();
            foreach (Group g in orderedGroups)
            {
                names.Add(g.getName());
            }
            return names;
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
                act.ChangeTo(Resource.Layout.groupListLayout);
            };

            backButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.groupListLayout);
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
        {
            newGroupNameText = act.FindViewById<EditText>(Resource.Id.editJoinGroupName);
            joinGroupButton = act.FindViewById<Button>(Resource.Id.buttonConfirmJoinGroup);
            backButton = act.FindViewById<Button>(Resource.Id.buttonJoinGroupToChoreList);
            
            joinGroupButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.groupListLayout);
                // TODO: actually join the group with the entered credentials
            };
            
            backButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.groupListLayout);
            };
        }
    }
}