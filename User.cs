﻿namespace ChoreChomper.Model
{
    public class User
    {
        int userId;
        string userName;

        public User()
        {
            //TODO: creating new users
        }

        public User(string name)
        {
            userName = name;
            //TODO: fetch and check id in database
        }

        public bool CheckCredentials(string pass)
        {
            //TODO: compare against the databases' password for this userId
            return false;
        }

        public int GetId()
        {
            return userId;
        }

        public string GetName()
        {
            return userName;
        }

        public User GenerateTestUser(string name)
        {
            userId = 0;
            userName = name;

            return this;
        }
    }
}