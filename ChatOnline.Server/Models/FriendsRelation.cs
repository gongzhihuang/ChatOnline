namespace ChatOnline.Server.Models 
{
    /// <summary>
    /// 好友关系
    /// </summary>
    public class FriendsRelation 
    {

        public FriendsRelation (string id, long userIMNumber, long friendIMNumber) 
        {
            this.Id = id;
            this.UserIMNumber = userIMNumber;
            this.FriendIMNumber = friendIMNumber;
        }

        public string Id { get; private set; }

        public long UserIMNumber { get; private set; }

        public long FriendIMNumber { get; private set; }
    }
}