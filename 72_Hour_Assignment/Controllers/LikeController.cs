using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _72_Hours.Controllers
{
    [Authorize]
    public class LikeController : ApiController
    {
        public IHttpActionResult Get()
        {
            var lService = CreateLikeService();
            var likes = lService.GetLikes();
            return Ok(likes);
        }
        public IHttpActionResult Post(LikeCreate like)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var lService = CreateLikeService();

            if (!lService.CreateLike(like))
                return InternalServerError();

            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            var lService = CreateLikeService();

            if (!lService.DeleteLike(id))
                return InternalServerError();

            return Ok();
        }
        public LikeService CreateLikeService()
        {
            var likeService = new LikeService();
            return likeService;
        }
    }
}