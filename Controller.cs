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
                // TODO: validate user
                act.GetSessionData().GenerateTestSession(usernameText.Text);
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
            Chore chore = new Chore(name, act.GetSessionData().GetCurrentUser().GetId(), "11/11/111");
            act.GetSessionData().GetTargetGroup().AddChore(chore);
            return (act.GetSessionData().GetTargetGroup().getTaskList().GetHeadChoreName());
        }
    }

    class MenuController : Controller
    {
        Button toTasksButton;
        Button toGroupsButton;
        Button toLoginButton;

        public MenuController(MainActivity act)
        {
            toTasksButton = act.FindViewById<Button>(Resource.Id.buttonMenuToTaskList);
            toGroupsButton = act.FindViewById<Button>(Resource.Id.buttonMenuToGroupList);
            toLoginButton = act.FindViewById<Button>(Resource.Id.buttonMenuToLogin);

            toTasksButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.choreListLayout);
            };

            toGroupsButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.groupListLayout);
            };

            toLoginButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.loginLayout);
            };
        }
    }

    class ChoreListController:Controller
    {
        List<Chore> chores;
        Button mainMenuButton;
        Button settingsButton;
        Button addChoreButton;
        ListView choreListView;
        
        public ChoreListController(MainActivity act)
        {
            chores = SetupChores(act);
            mainMenuButton = act.FindViewById<Button>(Resource.Id.navButtonChoreList);
            settingsButton = act.FindViewById<Button>(Resource.Id.settingsButtonChoreList);
            addChoreButton = act.FindViewById<Button>(Resource.Id.buttonAddChore);
            choreListView = act.FindViewById<ListView>(Resource.Id.listOfChores);
            
            SetupList(act);

            mainMenuButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.mainMenuLayout);
            };

            settingsButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.choreListSettingsLayout);
            };

            addChoreButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.choreCreateLayout);
            };

            choreListView.ItemClick += (sender, e) =>
            {
                Chore targetChore = chores[(int)e.Id];
                act.GetSessionData().SetTargetChore(targetChore);
                act.ChangeTo(Resource.Layout.choreEditLayout);
            };
        }

        private List<Chore> SetupChores (MainActivity act)
        {
            List<Chore> fullList = new List<Chore>();
            fullList = GenerateFullList(act);
            return GenerateFilteredList(fullList, act);
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
        }
        
        List<Chore> GenerateFullList(MainActivity act)
        {
            if (act.GetSessionData().GetChoreFilterGroup())
            {
                return GenerateFullListTargetGroup(act);
            }
            else
            {
                return GenerateFullListAllGroups(act);
            }
        }

        List<Chore> GenerateFullListAllGroups(MainActivity act)
        {
            List<Group> userGroups = act.GetSessionData().GetUsersGroups();
            List<Chore> allChores = new List<Chore>();
            foreach (Group g in userGroups)
            {
                allChores.AddRange(g.getTaskList().GetChoreList());
            }
            return allChores;
        }

        List<Chore> GenerateFullListTargetGroup(MainActivity act)
        {
            List<Chore> allChores = new List<Chore>();
            allChores = act.GetSessionData().GetTargetGroup().getTaskList().GetChoreList();
            return allChores;
        }

        List<Chore> GenerateFilteredList(List<Chore> fullList, MainActivity act)
        {
            List<Chore> filteredList = new List<Chore>();
            int localUserId = act.GetSessionData().GetCurrentUser().GetId();
            bool wantOnlyMine = act.GetSessionData().GetChoreFilterMine();
            bool wantOnlyComplete = act.GetSessionData().GetChoreFilterComplete();
            bool wantOnlyPriority = act.GetSessionData().GetChoreFilterPriority();

            foreach (Chore c in fullList)
            {
                if ((!wantOnlyMine || c.GetAssignment() == localUserId) && (wantOnlyComplete == c.isComplete()) && (!wantOnlyPriority || c.GetPriority()))
                    filteredList.Add(c);
            }

            return filteredList;
        }
    }

    class ChoreListSettingsController : Controller
    {
        Button backButton;
        CheckBox filterMineBox;
        CheckBox filterGroupBox;
        CheckBox filterPriorityBox;
        CheckBox filterCompleteBox;

        public ChoreListSettingsController(MainActivity act)
        {
            backButton = act.FindViewById<Button>(Resource.Id.buttonChoreListSettingsToChoreList);
            filterMineBox = act.FindViewById<CheckBox>(Resource.Id.checkChoreListSettingsMine);
            filterGroupBox = act.FindViewById<CheckBox>(Resource.Id.checkChoreListSettingsGroup);
            filterPriorityBox = act.FindViewById<CheckBox>(Resource.Id.checkChoreListSettingsPriority);
            filterCompleteBox = act.FindViewById<CheckBox>(Resource.Id.checkChoreListSettingsComplete);

            SetInitialValues(act);

            backButton.Click += (sender, e) =>
            {
                //TODO: store filter values
                act.GetSessionData().SetFilters(filterMineBox.Checked, filterGroupBox.Checked, filterPriorityBox.Checked, filterCompleteBox.Checked);
                act.ChangeTo(Resource.Layout.choreListLayout);
            };
        }

        void SetInitialValues(MainActivity act)
        {
            filterMineBox.Checked = act.GetSessionData().GetChoreFilterMine();
            filterGroupBox.Checked = act.GetSessionData().GetChoreFilterGroup();
            filterPriorityBox.Checked = act.GetSessionData().GetChoreFilterPriority();
            filterCompleteBox.Checked = act.GetSessionData().GetChoreFilterComplete();
        }
    }

    class ChoreCreationController : Controller
    {
        EditText newChoreNameText;
        EditText newChoreAssignmentText;
        EditText newChoreDeadlineText;
        CheckBox newChorePriorityBox;
        Button confirmChoreButton;
        Button backButton;

        public ChoreCreationController(MainActivity act)
        {
            newChoreNameText = act.FindViewById<EditText>(Resource.Id.editNewChoreName);
            newChoreAssignmentText = act.FindViewById<EditText>(Resource.Id.editNewChoreAssignment);
            newChoreDeadlineText = act.FindViewById<EditText>(Resource.Id.dateNewChoreDeadline);
            newChorePriorityBox = act.FindViewById<CheckBox>(Resource.Id.checkNewChorePriority);
            confirmChoreButton = act.FindViewById<Button>(Resource.Id.buttonAddChore);
            backButton = act.FindViewById<Button>(Resource.Id.buttonCreateChoreToChoreList);
            
            confirmChoreButton.Click += (sender, e) =>
            {
                // TODO: consider making addTestChore a bool and using that to determine if the chore was added
                if (newChoreNameText.Text != "")
                {
                    AddTestChore(newChoreNameText.Text, newChoreAssignmentText.Text, newChoreDeadlineText.Text, newChorePriorityBox.Checked, act);
                    newChoreNameText.Text = "chore added";
                    act.ChangeTo(Resource.Layout.choreListLayout);
                }
            };

            backButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.choreListLayout);
            };
        }

        public static string AddTestChore(string name, string assignment, string deadline, bool priority, MainActivity act)
        {
            Chore chore = new Chore(name, act.GetSessionData().GetIdOfUser(assignment), deadline, priority);
            act.GetSessionData().GetTargetGroup().AddChore(chore);
            return (act.GetSessionData().GetTargetGroup().getTaskList().GetHeadChoreName());
        }
    }

    class ChoreEditController : Controller
    {
        Chore targetChore;
        EditText desiredChoreNameText;
        EditText desiredChoreAssignmentText;
        EditText desiredChoreDeadlineText;
        CheckBox desiredChorePriorityBox;
        Button confirmChoreButton;
        Button completeChoreButton;
        Button backButton;

        public ChoreEditController(MainActivity act)
        {
            targetChore = act.GetSessionData().GetTargetChore();
            desiredChoreNameText = act.FindViewById<EditText>(Resource.Id.editChoreEditName);
            desiredChoreAssignmentText = act.FindViewById<EditText>(Resource.Id.editChoreEditAssignment);
            desiredChoreDeadlineText = act.FindViewById<EditText>(Resource.Id.editChoreEditDeadline);
            desiredChorePriorityBox = act.FindViewById<CheckBox>(Resource.Id.checkChoreEditPriority);
            confirmChoreButton = act.FindViewById<Button>(Resource.Id.buttonConfirmEditChore);
            completeChoreButton = act.FindViewById<Button>(Resource.Id.buttonCompleteChore);
            backButton = act.FindViewById<Button>(Resource.Id.buttonChoreEditToChoreList);

            desiredChoreNameText.Text = targetChore.GetName();
            
            SetInitialValues(act);

            confirmChoreButton.Click += (sender, e) =>
            {
                ApplyEdits(act);
                act.ChangeTo(Resource.Layout.choreListLayout);
            };
           
            completeChoreButton.Click += (sender, e) =>
            {
                targetChore.MarkComplete();
                act.ChangeTo(Resource.Layout.choreListLayout);
            };

            backButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.choreListLayout);
            };
        }

        void SetInitialValues(MainActivity act)
        {
            desiredChoreNameText.Text = targetChore.GetName();
            desiredChoreAssignmentText.Text = act.GetSessionData().GetNameOfUser(targetChore.GetAssignment());
            desiredChoreDeadlineText.Text = targetChore.GetDeadline().ToString();
            desiredChorePriorityBox.Checked = targetChore.GetPriority();

            if(targetChore.isComplete())
            {
                completeChoreButton.Enabled = false;
                confirmChoreButton.Enabled = false;
            }
        }

        void ApplyEdits(MainActivity act)
        {
            targetChore.SetName(desiredChoreNameText.Text);
            targetChore.SetAssignment(act.GetSessionData().GetIdOfUser(desiredChoreAssignmentText.Text));
            targetChore.SetDeadline(desiredChoreDeadlineText.Text);
            targetChore.SetPriority(desiredChorePriorityBox.Checked);
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
                act.GetSessionData().SetTargetGroup(targetGroup);
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
            groups = act.GetSessionData().GetUsersGroups();
            groups = CreateOrderedGroupList(groups, act);
            List<string> groupNames = GetNamesOfGroups(groups);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(act, Android.Resource.Layout.SimpleListItem1, groupNames);
            groupListView.Adapter = adapter;
        }

        private List<Group> CreateOrderedGroupList(List<Group> groups, MainActivity act)
        {
            List<Group> orderedGroups = new List<Group>();
            Group currentTargetGroup = act.GetSessionData().GetTargetGroup();
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
        EditText newGroupKeyText;
        Button confirmGroupButton;
        Button backButton;

        public GroupCreateController(MainActivity act)
        {
            newGroupNameText = act.FindViewById<EditText>(Resource.Id.editNewGroupName);
            newGroupKeyText = act.FindViewById<EditText>(Resource.Id.editNewGroupKey);
            confirmGroupButton = act.FindViewById<Button>(Resource.Id.buttonConfirmNewGroup);
            backButton = act.FindViewById<Button>(Resource.Id.buttonGroupCreateLayoutToGroupList);

            confirmGroupButton.Click += (sender, e) =>
            {
                if (newGroupNameText.Text != "group added" && newGroupNameText.Text != "")
                {
                    AddTestGroup(newGroupNameText.Text, act);
                    newGroupNameText.Text = "group added";
                    act.ChangeTo(Resource.Layout.groupListLayout);
                }
            };

            backButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.groupListLayout);
            };
        }

        public Group AddTestGroup(string name, MainActivity act)
        {
            Group group = new Group();
            group.GenerateTestGroup(name);
            //TODO: set a group password with the key
            return act.GetSessionData().JoinGroup(group);
        }
    }

    class JoinGroupController : Controller
    {
        EditText newGroupNameText;
        EditText newGroupKeyText;
        Button joinGroupButton;
        Button backButton;

        public JoinGroupController(MainActivity act)
        {
            newGroupNameText = act.FindViewById<EditText>(Resource.Id.editJoinGroupName);
            newGroupKeyText = act.FindViewById<EditText>(Resource.Id.editJoinGroupKey);
            joinGroupButton = act.FindViewById<Button>(Resource.Id.buttonConfirmJoinGroup);
            backButton = act.FindViewById<Button>(Resource.Id.buttonJoinGroupToChoreList);
            
            joinGroupButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.groupListLayout);
                // TODO: actually join the group with the entered credentials
                //  Check if group is in database
                //  Check if you have the correct key for that group
                //  Join group in database
            };
            
            backButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.groupListLayout);
            };
        }
    }
}