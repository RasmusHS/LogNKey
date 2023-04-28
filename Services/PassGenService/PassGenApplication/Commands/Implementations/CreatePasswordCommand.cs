using PassGenApplication.Commands.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGenApplication.Commands.Implementations
{
    public class CreatePasswordCommand : ICreatePasswordCommand
    {
        public CreatePasswordCommand(/*IRepository implementation*/)
        {

        }

        void ICreatePasswordCommand.CreatePassword(PasswordCreateRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
