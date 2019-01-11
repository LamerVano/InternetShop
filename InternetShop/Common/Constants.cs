using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Constants
    {
        public const int WAIT_TIME = 10;
        public const string COOKIE_ROLE = "Role";
        public const string COOKIE_USER_ID = "UserId";
        public const string MODER = "Moderator";
        public const string ADMIN = "Admin";
        public const string USER = "User";
        public const string APROVE = "Aprooved";
        public const string DELIVER = "Delivered";
        public const string PAID = "Paid";
        public static string IMAGES_PATH = Directory.GetCurrentDirectory();
        public const string IMAGES_TYPE = ".bmp";
    }
}
