using System.Collections.Generic;
using System.Net.Http;
using EmployeeManager.Data;

namespace GorestClient.Eextensions
{
    internal static class UserExtensions
    {
        public static FormUrlEncodedContent GetFormUrlEncodedContent(this User user)
        {
            var values = new Dictionary<string, string>
            {
                {"name", user.Name},
                {"email", user.Email},
                {"gender", user.Gender.ToString()},
                {"status", user.Status.ToString()},
            };

            var content = new FormUrlEncodedContent(values);
            return content;
        }
    }
}
