using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72_Hour.Services
{
    public class PostService
    {

        // GuidAuthorId (Post) 
        private readonly Guid _authorId;

        public PostService(Guid authorId)
        {
            _authorId = authorId;
        }

        //POST METHODS

        //Create Post
        public bool CreatePost(PostCreate model)
        {
            var entity = new Post()
            {
                AuthorId = _authorId,
                Title = model.Title,
                Text = model.Text,
                CreatedUtc = DateTimeOffset.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Posts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //Get All Posts
        public IEnumerable<PostListItem> GetAllPosts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Posts
                    .Where(e => e.AuthorId == _authorId)
                    .Select(
                        e =>
                        new NotePostItem
                        {
                            PostId = e.PostId,
                            Title = e.Title,
                            Text = e.Text,
                            CreatedUtc = e.CreatedUtc
                        }
                    );

                return query.ToArray();

            }
        }
    }
}
