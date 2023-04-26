using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGenDomain
{
    //Det er her password genereringsprocessen ligger 
    public class PasswordEntity
    {

        public PasswordEntity()
        {

        }


        public string Password { get; private set; }
        public int Length { get; private set; }
        public bool MustContainUppercase { get; private set; }
        public bool MustContainLowercase { get; private set; }
        public bool MustContainNumbers { get; private set; }
        public bool MustContainSpecialCharacters { get; private set; }

        public string CreatePassword(int length, bool mustContainUppercase, bool mustContainLowercase, bool mustContainNumbers, bool mustContainSpecialCharacters)
        {
            return new NotImplementedException().ToString();
        }
    }
}
