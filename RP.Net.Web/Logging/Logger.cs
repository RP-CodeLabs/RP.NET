using System.Collections.Generic;
using System.Diagnostics;

namespace RP.Net.Web.Logging
{
    public class Logger : ILogger
    {
        public void OnArgumentNullOrEmpty(Dictionary<string, object> errors)
        {
            // Log error to flat file or DB .....
            foreach (var error in errors)
            {
                Debug.WriteLine($"Error key : {error.Key} and value : {error.Value}");
            }
        }
    }

    public interface ILogger
    {
        void OnArgumentNullOrEmpty(Dictionary<string, object> errors);
    }
}