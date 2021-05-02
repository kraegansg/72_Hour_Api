using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72_Hour.Services
{
    public class ReplyService
    {

        // GuidAuthorId (Reply)
        private readonly Guid _authorId;

        public ReplyService(Guid authorId)
        {
            _authorId = authorId;
        }


        //REPLY METHODS
        //Create a reply toa comment using a foreign key relationship

        public bool CreateReply(ReplyCreate model)
        {
            var entity = new Reply()
            {
                AuthorId = _authorId,
                Text = model.Text,
                CreatedUtc = DateTimeOffset.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Replies.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // Get Replies By Commen Id 

        public ReplyDetail GetReplyByCommentId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Reply
                        .Single(e => e.CommentId == id && e.AuthorId == _authorId);
                return
                    new ReplyDetail
                    {
                        ReplyId = entity.ReplyId,
                        Text = entity.Text,
                        CreatedUtc = entity.CreatedUtc,
                    };
            }
        }

    }
}
