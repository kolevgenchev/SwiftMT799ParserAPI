using Swift_MT799.Models;
using System.Collections.Generic;

namespace Swift_MT799.Helpers
{
    public class SwiftParser
    {
        public static SwiftMessage Parse(string content)
        {

            return new SwiftMessage
            {
                Content = content
            };
        }

    }
}
