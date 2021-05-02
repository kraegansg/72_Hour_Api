using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72_Hour.Services
{
    public class CommentService
    {

        // GuidAuthorId (Comment) 
        private readonly Guid _authorId;

        public CommentService(Guid authorId)
        {
            _authorId = authorId;
        }
    }

    //COMMENT METHODS
    // Create Comment on Post (using Foregn Key Relationship) 

    public bool CreateCommentOnPost(CommentCreate model)
    {
        var entity = new Comment()
        {
            AuthorId = _authorId,
            Text = model.Text,
            CreatedUtc = DateTimeOffset.Now
        };

        using (var ctx = new ApplicationDbContext())
        {
            ctx.Comments.Add(entity);
            return ctx.SaveChanges() == 1;
        }
    }

    // Get comment by Post Id 

    public CommentDetail GetCommentByPostId(int id)
    {
        using (var ctx = new ApplicationDbContext())
        {
            var entity =
                ctx
                    .Comments
                    .Single(e => e.PostId == id && e.AuthorId == _authorId);
            return
                new CommentDetail
                {
                    CommentId = entity.CommentId,
                    Text = entity.Text,
                    CreatedUtc = entity.CreatedUtc,
                };
        }
    }
}
