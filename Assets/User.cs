﻿using ChoreChomper.Data;
using Newtonsoft.Json;

namespace ChoreChomper.Model
{
    public class User
    {
        string userId;
        string userName;
        SessionData sData = new SessionData();

        public User()
        {
            //null constructor
        }

        public User(string username, string password)
        {
            // creating new users
            string result = sData.callAPI("insertnewuser.php?FirstName=" + username + "&Email=" + username + "&LastName=" + username + "&Password=" + password + "&Phone=" + username + "&Username=" + username);
            result = (string)JsonConvert.DeserializeObject(result, typeof(string));
            userId = result;
            userName = username;
        }

        public User(string name)
        {
            string result = sData.callAPI("returnUid.php?Username=" + name);
            result = (string)JsonConvert.DeserializeObject(result, typeof(string));
            userName = name;
            userId = result;
            //fetch and check id in database
        }

        public bool CheckCredentials(string pass)
        {
            // compare against the databases' password for this userId
            string result = sData.callAPI("logincheck.php?Username_Attempt=" + userId + "&Password_Attempt=" + pass);
            result = (string)JsonConvert.DeserializeObject(result, typeof(string));
            return result == "true";
        }

        public string GetId()
        {
            return userId;
        }

        public string GetName()
        {
            return userName;
        }

        public User GenerateTestUser()
        {
            userId = "0";
            userName = "Bob";

            return this;
        }
    }
}