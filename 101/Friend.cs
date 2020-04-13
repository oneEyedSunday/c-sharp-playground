using System;
namespace _101
{
    public class Friend
    {

        public Friend(string first, string last)
        {
            firstName = first;
            lastName = last;
        }
        public string firstName { get; private set; }
        public string lastName { get; private set; }
    }
}

