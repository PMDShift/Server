using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Reviews
{
    public interface ILocation : IEquatable<ILocation>
    {
        string GetDescription();
    }
}
