using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Users.Commands.ChangeUserCulture
{
    using System.Security.Principal;
    using MediatR;

    public class ChangeUserCultureCommand:IRequest
    {
        public ChangeUserCultureCommand(IPrincipal user, string uiCulture)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            UiCulture = uiCulture;
        }

        public IPrincipal User { get; }
        public string UiCulture { get;}
    }
}
