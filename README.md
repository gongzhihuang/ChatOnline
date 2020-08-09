## ChatOnline.Server

https://docs.microsoft.com/zh-cn/aspnet/core/signalr/introduction?view=aspnetcore-3.1



配置SignalR

```
services.AddSignalR();
```



```
app.UseEndpoints(endpoints =>
{
		endpoints.MapControllers();
		endpoints.MapHub<ChatHub>("/chathub");
});
```



创建ChatHub类，继承Hub类。



`Hub`类具有一个 `Context` 属性，该属性包含有关连接的信息的以下属性：

| 属性              | 描述 |
| ----------------- | ---- |
| ConnectionId      |      |
| UserIdentifier    |      |
| User              |      |
| Items             |      |
| Features          |      |
| ConnectionAborted |      |



| 方法           | 描述 |
| -------------- | ---- |
| GetHttpContext |      |
| Abort          |      |



`Hub`类具有一个 `Clients` 属性，该属性包含服务器和客户端之间的通信的以下属性：

| 属性   | 描述 |
| ------ | ---- |
| All    |      |
| Caller |      |
| Others |      |



| 方法          | 描述 |
| ------------- | ---- |
| AllExcept     |      |
| Client        |      |
| Clients       |      |
| Group         |      |
| GroupExcept   |      |
| Groups        |      |
| OthersInGroup |      |
| User          |      |
| Users         |      |





## ChatOnline.Client

vue框架开发的ChatOnline客户端

https://docs.microsoft.com/zh-cn/aspnet/core/signalr/javascript-client?view=aspnetcore-3.1



```
npm install --save @aspnet/signalr
```

