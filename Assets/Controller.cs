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
using System.Net;
using System.IO;
using Newtonsoft.Json;
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
            //string username = usernameText.Text;
            //string password = passwordText.Text;
            loginButton.Click += (sender, e) =>
            {
                string username = usernameText.Text;
                string password = passwordText.Text;
                //usernameText.Text = "http://192.168.43.144/chorechomper/api/logincheck.php?Username_Attempt=" + username + "&Password_Attempt=" + password;
                // TODO: handle login attempt
                HttpWebRequest myRequestLogin = (HttpWebRequest)WebRequest.Create("http://192.168.43.144/chorechomper/api/logincheck.php?Username_Attempt=" + username + "&Password_Attempt=" + password);
                WebResponse myResponseLogin = myRequestLogin.GetResponse();
                StreamReader srLogin = new StreamReader(myResponseLogin.GetResponseStream(), System.Text.Encoding.UTF8);
                string resultLogin = srLogin.ReadToEnd();
                resultLogin = resultLogin.Replace('\n', ' ');
                srLogin.Close();
                myResponseLogin.Close();
                resultLogin = JsonConvert.DeserializeObject<string>(resultLogin);
                //usernameText.Text = resultLogin;
                if (resultLogin == "TRUE")
                {
                    //usernameText.Text = resultLogin;
                    act.ChangeTo(Resource.Layout.choreListLayout);

                }
                else { act.ChangeTo(Resource.Layout.loginLayout); 
                }

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
            EditText firstnameText = act.FindViewById<EditText>(Resource.Id.editFirstName);
            EditText lastnameText = act.FindViewById<EditText>(Resource.Id.editLastName);
            EditText emailText = act.FindViewById<EditText>(Resource.Id.editPhone);
            EditText phoneText = act.FindViewById<EditText>(Resource.Id.editEmail);
            confirmButton = act.FindViewById<Button>(Resource.Id.buttonConfirm);
            cancelButton = act.FindViewById<Button>(Resource.Id.buttonCancel);

            confirmButton.Click += (sender, e) =>
            {
                string username = usernameText.Text;
                string password = passwordText.Text;
                string firstname = firstnameText.Text;
                string lastname = lastnameText.Text;
                string email = emailText.Text;
                string phone = phoneText.Text;
                HttpWebRequest myRequestVar = (HttpWebRequest)WebRequest.Create("http://192.168.43.144/chorechomper/api/insertnewuser.php?FirstName=" + username + "&Email=" + email + "&LastName=" + lastname + "&Password=" + password + "&Phone=" + phone + "&Username=" + username);
                WebResponse myResponseVar = myRequestVar.GetResponse();
                StreamReader srVar = new StreamReader(myResponseVar.GetResponseStream(), System.Text.Encoding.UTF8);
                string resultVar = srVar.ReadToEnd();
                resultVar = resultVar.Replace('\n', ' ');
                srVar.Close();
                myResponseVar.Close();
                act.ChangeTo(Resource.Layout.loginLayout);
            };

            cancelButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.loginLayout);
            };
        }
    }

    class HomeController : Controller
    {
        EditText chorenameText;
        Button addChoreButton;
        Button viewChoresButton;

        public HomeController(MainActivity act)
        {
            act.SetContentView(Resource.Layout.activity_main);

            EditText chorenameText = act.FindViewById<EditText>(Resource.Id.editChoreName);
            addChoreButton = act.FindViewById<Button>(Resource.Id.buttonAddChore);
            viewChoresButton = act.FindViewById<Button>(Resource.Id.buttonViewChores);
            EditText completebyText = act.FindViewById<EditText>(Resource.Id.editCompleteBy);
            EditText assignedtoText = act.FindViewById<EditText>(Resource.Id.editAssignedTo);
            EditText priorityText = act.FindViewById<EditText>(Resource.Id.editPriority);
            addChoreButton.Click += (sender, e) =>
            {
                //TODO: consider making addTestChore a bool and using that to determine if the chore was added
                string chorename = chorenameText.Text;
                string completeBy = completebyText.Text;
                string assignedTo = assignedtoText.Text;
                string priority = priorityText.Text;
                HttpWebRequest myRequestChoreInsert = (HttpWebRequest)WebRequest.Create("http://192.168.43.144/chorechomper/api/insertnewchore.php?ChoreName=" + chorename + "&CompleteBy=" + completeBy + "&AssignedTo=" + assignedTo + "&Priority=" + priority);
                WebResponse myResponseChoreInsert = myRequestChoreInsert.GetResponse();
                StreamReader srChoreInsert = new StreamReader(myResponseChoreInsert.GetResponseStream(), System.Text.Encoding.UTF8);
                string resultVarChoreInsert = srChoreInsert.ReadToEnd();
                resultVarChoreInsert = resultVarChoreInsert.Replace('\n', ' ');
                srChoreInsert.Close();
                myResponseChoreInsert.Close();
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
            mainMenuButton = act.FindViewById<Button>(Resource.Id.menuButtonChoreList);
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
                act.ChangeTo(Resource.Layout.activity_main);
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
            mainMenuButton = act.FindViewById<Button>(Resource.Id.menuButtonGroupList);
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
            List<string> groupNames = new List<string>();
            foreach (Group g in groups)
            {
                groupNames.Add(g.getName());
            }
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(act, Android.Resource.Layout.SimpleListItem1, groupNames);
            groupListView.Adapter = adapter;
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
            
            /*
            joinGroupButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.groupListLayout);
                // TODO: actually join the group with the entered credentials
            };
            
            backButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.groupListLayout);
            };
            */
        }
    }
}