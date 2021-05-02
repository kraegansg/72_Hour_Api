using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72_Hour.Services
{
    public class LikeService
    {

        // GuidOwnerId (Like) 
        private readonly Guid _ownerId;

        public LikeService(Guid ownerId)
        {
            _ownerId = ownerId;
        }

        // LIKE METHODS 
        // Create a Like on a post using a foreign key relationship 

        public bool CreateLike(LikeCreate model)
        {
            var entity = new Like()
            {
                OwnerId = _ownerId,
                CreatedUtc = DateTimeOffset.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // Get Likes by Post Id 

        public LikeDetail GetLikesByPostId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Likes
                        .Single(e => e.PostId == id && e.OwnerId == _ownerId);
                return
                    new LikeDetail
                    {
                        LikeId = entity.LikeId,
                        CreatedUtc = entity.CreatedUtc,

                    };
            }
        }


    }
}
