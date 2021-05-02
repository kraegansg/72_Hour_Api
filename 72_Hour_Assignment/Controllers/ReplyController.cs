using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _72_Hours.Controllers
{
    [Authorize]
    public class ReplyController : ApiController
    {
        public IHttpActionResult Get()
        {
            var rService = CreateReplyService();
            var replies = rService.GetReplies();
            return Ok(replies);
        }
        public IHttpActionResult Post(ReplyCreate reply)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rService = CreateReplyService();

            if (!rService.CreateReply(reply))
                return InternalServerError();

            return Ok();
        }
        public IHttpActionResult Get(int id)
        {
            var rService = CreateReplyService();

            if (!rService.DeleteReply(id))
                return InternalServerError();

            return Ok();
        }
        private ReplyService CreateReplyService()
        {
            var replyService = new ReplyService();
            return replyService;
        }
    }
}