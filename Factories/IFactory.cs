using loginAndRegistration.Models;
using System.Collections.Generic;

namespace loginAndRegistration.Factory
{
    public interface IFactory<T> where T : BaseEntity
    {}
}