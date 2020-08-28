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
		endpoints.MapHub<ChatHub>("/hubs/chathub");
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

| 属性   | 描述                           |
| ------ | ------------------------------ |
| All    | 所有连接的客户端               |
| Caller |                                |
| Others | 除当前连接外的所有连接的客户端 |



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
// npm install --save @aspnet/signalr

npm install --save @microsoft/signalr
```

SignalR库在vue中的使用：

```
// import * as signalR from "@aspnet/signalr";

import * as signalR from "@microsoft/signalr";
...

data() {
  return {
    access_token: "",
    connection: null,
    mesages: []
  };
},
created(){
  var that = this;
	this.connection = new signalR.HubConnectionBuilder()
    .withUrl("https:localhost:5001/chatHub", {
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets,
      accessTokenFactory: () => {
        return that.access_token;
      }
    })
    .configureLogging(signalR.LogLevel.Information)
    .build();

  this.connection.on("ReceiveMessage", message => {
    // 收到消息
    console.log(message);
    this.mesages.push(message);
  });

  this.connection.start();
}
```



调用服务端方法：

```
this.connection
    .invoke("SendMessage", message)
    .catch(err => console.error(err));
```

