using cdod.Schema.InputTypes;
using cdod.Schema.Mutations;
using cdod.Services;
using cdod.Models;

namespace cdod.Schema
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationUser
    {
        // Перенести скорее всего регистрацию авторизацию и афунтификацию сюда!!!!
    }
}
