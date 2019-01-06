using System;
using System.Data.Entity;
using System.Threading.Tasks;
using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;

namespace FriendOrganizer.UI.Data.Repositories
{
    public class FriendRepository : IFriendRepository
    {

        private readonly FriendOrganizerDbContext _context;

        public FriendRepository(FriendOrganizerDbContext context)
        {
            _context = context;
        }
       
        public async Task<Friend> GetByIdAsync(int friendId)
        {

            return await _context.Friends.SingleAsync(f => f.Id == friendId);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }


        //private readonly Func<FriendOrganizerDbContext> _contextCreator;

        //public FriendRepository(Func<FriendOrganizerDbContext> contextCreator)
        //{
        //    _contextCreator = contextCreator;
        //}

        //public async Task<Friend> GetByIdAsync(int friendId)
        //{
        //    using (var ctx = _contextCreator())
        //    {
        //        return await ctx.Friends.AsNoTracking().SingleAsync(f => f.Id == friendId);
        //    }
        //}

        //public async Task SaveAsync(Friend friend)
        //{
        //    using (var ctx = _contextCreator())
        //    {
        //        ctx.Friends.Attach(friend);
        //        ctx.Entry(friend).State = EntityState.Modified;
        //        await ctx.SaveChangesAsync();
        //    }
        //}
    }
}
