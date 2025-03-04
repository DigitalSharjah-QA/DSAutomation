using SD;
using SD.Datasources;
using SD.Datasources.Users;
using SD.Helpers;

namespace SD.Datasources.Users
{
    public class User
    {
        public static string token;

        public User() { }
        public User(string name,UserRole role ,Stage stage,string eid,string mobile)
        {
            Name = name;
            Role = role;
            Stage = stage;
            EID = eid;
            Mobile = mobile;
        }

        public string Name { get; set; }
        public string EID { get; set; }
        public string Mobile { get; set; }
        public Stage Stage { get; set; }
        public UserRole Role { get; set; }

        
    }
}

namespace SD.Datasources
{
public enum UserRole
    {
        SOP1=1,
        SOP2=2,
        SOP3=3,
        Guest = 4
    }
}