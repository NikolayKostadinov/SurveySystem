using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Users.Commands.LockUser
{
    using MediatR;

    public class LockUserCommand:IRequest
    {
        public LockUserCommand(int id, LockOperation lockOperation)
        {
            Id = id;
            LockOperation = lockOperation;
        }

        public int Id { get;}
        public LockOperation LockOperation { get; }
    }

    public enum LockOperation
    {
        Lock, Unlock
    }
}
