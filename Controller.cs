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
using System.Threading.Tasks;
using System.IO;

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
                act.ChangeTo(Resource.Layout.activity_main);
            };

            newUserButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.newUserLayout);
            };
        }
    }

    class NewUserController : Controller
    {
        //EditText usernameText;
       // EditText passwordText;
        Button confirmButton;
        Button cancelButton;

        public NewUserController(MainActivity act)
        {
           confirmButton = act.FindViewById<Button>(Resource.Id.buttonConfirm);
           cancelButton = act.FindViewById<Button>(Resource.Id.buttonCancel);
            confirmButton.Click += (sender, e) =>
            {
                EditText usernameText = act.FindViewById<EditText>(Resource.Id.editUsername);
                EditText passwordText = act.FindViewById<EditText>(Resource.Id.editPassword);
                EditText firstnameText = act.FindViewById<EditText>(Resource.Id.editFirstName);
                EditText lastnameText = act.FindViewById<EditText>(Resource.Id.editLastName);
                EditText emailText = act.FindViewById<EditText>(Resource.Id.editPhone);
                EditText phoneText = act.FindViewById<EditText>(Resource.Id.editEmail);
                string username = usernameText.Text;
                string password = passwordText.Text;
                string firstname = firstnameText.Text;
                string lastname = lastnameText.Text;
                string email = emailText.Text;
                string phone = phoneText.Text;
                HttpWebRequest myRequestVar = (HttpWebRequest)WebRequest.Create("http://10.0.2.2/chorechomper/insertnewuser.php?FirstName=" + username + "&Email=" + email + "&LastName=" + lastname + "&Password=" + password + "&Phone=" + phone + "&Username=" + username);
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

    class HomeController:Controller
    {
        EditText choreNameText;
        Button addChoreButton;
        Button viewChoresButton;
        EditText test;
        public HomeController(MainActivity act)
        {
            act.SetContentView(Resource.Layout.activity_main);

            choreNameText = act.FindViewById<EditText>(Resource.Id.editChoreName);
            addChoreButton = act.FindViewById<Button>(Resource.Id.buttonAddChore);
            viewChoresButton = act.FindViewById<Button>(Resource.Id.buttonViewChores);
            addChoreButton.Click += (sender, e) =>
            {
                EditText chorenameText = act.FindViewById<EditText>(Resource.Id.editChoreName);
                EditText completebyText = act.FindViewById<EditText>(Resource.Id.editCompleteBy);
                EditText assignedtoText = act.FindViewById<EditText>(Resource.Id.editAssignedTo);
                EditText priorityText = act.FindViewById<EditText>(Resource.Id.editPriority);
                string chorename = chorenameText.Text;
                string completeBy = completebyText.Text;
                string assignedTo = assignedtoText.Text;
                string priority = priorityText.Text;
                
                
                    HttpWebRequest myRequestChoreInsert = (HttpWebRequest)WebRequest.Create("http://10.0.2.2/chorechomper/insertnewchore.php?ChoreName=" + chorename + "&CompleteBy=" + completeBy + "&AssignedTo=" + assignedTo + "&Priority=" + priority);
                    WebResponse myResponseChoreInsert = myRequestChoreInsert.GetResponse();
                    StreamReader srChoreInsert = new StreamReader(myResponseChoreInsert.GetResponseStream(), System.Text.Encoding.UTF8);
                    string resultVarChoreInsert = srChoreInsert.ReadToEnd();
                    resultVarChoreInsert = resultVarChoreInsert.Replace('\n', ' ');
                    srChoreInsert.Close();
                    myResponseChoreInsert.Close();
                
                
                
                // TODO: consider making addTestChore a bool and using that to determine if the chore was added
                
               
            };
            //AddTestChore(choreNameText.Text, act);
           // choreNameText.Text = "chore added";
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

    class ChoreListController:Controller
    {
        Button homeButton;
        //Xamarin.Forms.ListView choreListView;
        ListView choreListView;

        public ChoreListController(MainActivity act)
        {
            homeButton = act.FindViewById<Button>(Resource.Id.buttonHome);
            choreListView = act.FindViewById<ListView>(Resource.Id.listOfChores);

            homeButton.Click += (sender, e) =>
            {
                act.ChangeTo(Resource.Layout.activity_main);
            };
        }

        private void SetupList(MainActivity act)
        {
            List<string> choreNames = act.getMainGroup().GetTaskList().GetChoreNames();
            // TODO: add stuff to chore list and make it display them
            //choreListView.ItemsSource.Add();

        }
    }
}