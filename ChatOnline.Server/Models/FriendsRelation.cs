namespace ChatOnline.Server.Models
{
    /// <summary>
    /// 好友关系
    /// </summary>
    public class FriendsRelation
    {

        public FriendsRelation(long id, long userId, long friendId)
        {
            Id = id;
            UserId = userId;
            FriendId = friendId;
        }
        public FriendsRelation(long userId, long friendId)
        {
            UserId = userId;
            FriendId = friendId;
        }

        public long Id { get; private set; }

        public long UserId { get; private set; }

        public long FriendId { get; private set; }
    }

    //public enum FriendState
    //{
    //    /// <summary>
    //    /// 未通过
    //    /// </summary>
    //    NotPass,
    //    /// <summary>
    //    /// 通过
    //    /// </summary>
    //    Pass,
    //    /// <summary>
    //    /// 拒绝
    //    /// </summary>
    //    Refuse
    //}
}