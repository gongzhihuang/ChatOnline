namespace ChatOnline.Server.Models
{
    /// <summary>
    /// IM用户
    /// </summary>
    public class IMUser
    {
        public IMUser(string id, long iMNumber, string name, string password)
        {
            Id = id;
            IMNumber = iMNumber;
            Name = name;
            Password = password;
        }

        /// <summary>
        /// 账户ID
        /// </summary>
        /// <value></value>
        public string Id { get; private set; }

        /// <summary>
        /// IM号码
        /// </summary>
        /// <value></value>
        public long IMNumber{ get; private set;}

        /// <summary>
        /// 连接ID
        /// </summary>
        /// <value></value>
        public string ConnectionId{ get ; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        /// <value></value>
        public string Name { get; private set; }

        /// <summary>
        /// 密码
        /// </summary>
        /// <value></value>
        public string Password { get; private set; }
    }
}