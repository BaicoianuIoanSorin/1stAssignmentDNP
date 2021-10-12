﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Models;

namespace FirstAssignmentDNP.Data
{
    public class UsersJSONData : IUsersData
    {
        private IList<User> users;
        private string usersFile = "users.json";

        private static UsersJSONData instance;

        public UsersJSONData()
        {
            string content = File.ReadAllText(usersFile);
            users = JsonSerializer.Deserialize<List<User>>(content);
        }

        public IList<User> GetUsers()
        {
            List<User> tmp = new List<User>(users);
            return tmp;
        }

        public void AddUser(User user)
        {
            int max = users.Max(user => user.Id);
            user.Id = (++max);
            user.Family = null;
            user.Role = "Member";
            user.SecurityLevel = 0;
            users.Add(user);
            WriteUsersToFile();
        }
        
        public void RemoveUser(int userID)
        {
            User toRemove = users.First(u => u.Id == userID);
            users.Remove(toRemove);
            WriteUsersToFile();
        }

        public void Update(User user)
        {
            //to be updated
            User toUpdate = users.First(u => u.Id == user.Id);
            toUpdate.Username = user.Username;
            toUpdate.Role = user.Role;
            toUpdate.SecurityLevel = user.SecurityLevel;
            toUpdate.Password = user.Password;
            WriteUsersToFile();
        }

        public User Get(int userID)
        {
            return users.FirstOrDefault(u => u.Id == userID);
        }

        public void AddFamilyToUser(Family family, int userId)
        {
            User toUpdate = users.First(u => u.Id == userId);
            toUpdate.Family = family;
            WriteUsersToFile();
        }

        private void WriteUsersToFile()
        {
            string userAsJson = JsonSerializer.Serialize(users);
            File.WriteAllText(usersFile, userAsJson);
        }
    }
}