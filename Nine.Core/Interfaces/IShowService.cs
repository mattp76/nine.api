using Nine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nine.Core.Interfaces
{
    public interface IShowService
    {
        List<ShowResponseModel> ProcessShowResponse(ShowsRootModel model);
    }
}
