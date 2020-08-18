using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatOnline.Server.Models;
using ChatOnline.Server.ViewModels;

namespace ChatOnline.Server.Services
{
    public class IMUserService : IIMUserService
    {
        private readonly IMDbContext _dbContext;

        public IMUserService(IMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<IMUserDto> GetFriends(long iMNumber)
        {
            var friendIMNumbers = _dbContext.FriendsRelations.Where(x => x.UserIMNumber == iMNumber).Select(x => x.FriendIMNumber);
            var friends = _dbContext.IMUsers.Where(x => friendIMNumbers.Contains(x.IMNumber)).ToList();
            
            return friends.Select(x => new IMUserDto(){
                IMNumber = x.IMNumber,
                Name = x.Name
            }).ToList();
        }

        public async Task Offline(long iMNumber)
        {
            IMUser iMUser = _dbContext.IMUsers.FirstOrDefault( x => x.IMNumber == iMNumber);
            iMUser.ConnectionId = "";

            _dbContext.Update(iMUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Online(long iMNumber, string connectionId)
        {
            IMUser iMUser = _dbContext.IMUsers.FirstOrDefault( x => x.IMNumber == iMNumber);
            iMUser.ConnectionId = connectionId;

            _dbContext.Update(iMUser);
            await _dbContext.SaveChangesAsync();
        }
    }
}