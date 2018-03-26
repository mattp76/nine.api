using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nine.Core.Interfaces
{
    public interface IJsonHelper
    {
        bool CheckForValidJson<T>(T model);
    }
}
