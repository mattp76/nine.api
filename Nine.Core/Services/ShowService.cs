using Nine.Core.Interfaces;
using Nine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nine.Core.Services
{
    public class ShowService : IShowService
    {
        /// <summary>
        /// Process the show response. Only return where DRM is true and episode count is greater than 0
        /// </summary>
        /// <returns>
        /// List<ShowResponseModel>
        /// </returns>
        public List<ShowResponseModel> ProcessShowResponse(ShowsRootModel model)
        {
            try
            {
                if (model.payload != null)
                {
                    if (model.payload.Count > 0)
                    {
                        //var resp = model.payload.Where(x => x.drm && x.episodeCount > 0).ToList();
                        var resp = (from c in model.payload
                                    where c.episodeCount > 0 && c.drm
                                    select new ShowResponseModel()
                                    {
                                        image = c.image.showImage,
                                        slug = c.slug,
                                        title = c.title
                                    }
                                    ).ToList();

                        if (resp != null)
                        {
                            if (resp.Count > 0)
                            {
                                return resp;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //silent error for the purpose of this task.
            }

            return null;
        }
    }
}
