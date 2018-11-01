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

        public LoginController(MainActivity act)
        {
            usernameText = act.FindViewById<EditText>(Resource.Id.editUsername);
            passwordText = act.FindViewById<EditText>(Resource.Id.editPassword);
            loginButton = act.FindViewById<Button>(Resource.Id.buttonLogin);

            loginButton.Click += (sender, e) =>
            {
                // TODO: handle login attempt
                act.ChangeTo(Resource.Layout.activity_main);
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