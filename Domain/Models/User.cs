﻿namespace expense_tracker.Repositories
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Salt { get; set; }
        public string PasswordHash { get; set; }
    }
}