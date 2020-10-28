<template>
<div>
    <div v-for="(item, index) in mesages" :key="index">{{ item.message }}</div>

    <div>
      <el-input v-model="access_token" placeholder="access_token"></el-input>
    </div>

    <div>
      <el-button @click="connectServer()">连接</el-button>
    </div>

    <div>
      <el-input v-model="to" placeholder="发送给谁"></el-input>
    </div>

    <div>
      <el-input v-model="input" placeholder="请输入内容"></el-input>
    </div>

    <div>
      <el-button @click="sendMessage()">发送消息</el-button>
    </div>
</div>  
</template>

<script>
import * as signalR from "@microsoft/signalr";

export default {
name: "TestChat",
  data() {
    return {
      access_token: "",
      connection: null,
      input: "",
      mesages: [],
      to: ""
    };
  },
  created() {
    // this.connectServer();
  },
  methods: {
    connectServer() {
      let that = this;
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl("https:localhost:8080/hubs/chathub", {
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
    },
    /**
     * 发送消息
     */
    sendMessage() {
      let message = {
        UserName: this.to.toString(),
        Message: this.input.toString()
      };
      console.log("sendMessage", message);
      this.connection
        .invoke("SendMessage", message)
        .catch(err => console.error(err));

    //   this.mesages.push(this.input);

      this.input = "";
    }
  }
}
</script>

<style>

</style>