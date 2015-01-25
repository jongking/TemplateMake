using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Make.Model
{
    public class UsersModel
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name = "";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string password = "";
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private bool notused = false;
        public bool NotUsed
        {
            get { return notused; }
            set { notused = value; }
        }
        private string chinesename = "";
        public string ChineseName
        {
            get { return chinesename; }
            set { chinesename = value; }
        }
        private string englishname = "";
        public string EnglishName
        {
            get { return englishname; }
            set { englishname = value; }
        }
        private int shopid;
        public int ShopId
        {
            get { return shopid; }
            set { shopid = value; }
        }
        private string roleid = "";
        public string RoleId
        {
            get { return roleid; }
            set { roleid = value; }
        }
        private string cardno = "";
        public string CardNo
        {
            get { return cardno; }
            set { cardno = value; }
        }

        public UsersModel()
        {
        }
    }
}
