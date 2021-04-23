using System.Collections.Generic;

namespace HotChocolateWithEfDemo.GraphqlApi.Common
{
    public abstract class Payload
    {
        protected Payload(IReadOnlyList<UserError> errors = null)
        {
            Errors = errors;
        }

        public IReadOnlyList<UserError> Errors { get; }
    }
}