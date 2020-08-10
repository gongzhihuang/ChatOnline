<template>
  <div class="chat">
    <div class="chat-list">
      <el-card class="box-card">
        <div slot="header" class="clearfix">
          <span>聊天室</span>
          <el-button
            style="float: right; padding: 3px 0"
            type="text"
            @click="clearMessages()"
            >清空聊天记录</el-button
          >
        </div>
        <div v-for="(item, index) in mesages" :key="index" class="text item">
          {{ item }}
        </div>
      </el-card>
    </div>
    <div class="chat-operation">
      <div>
        <el-input v-model="input" placeholder="请输入内容"></el-input>
      </div>
      <div><el-button @click="sendMessage(input)">发送消息</el-button></div>
    </div>
  </div>
</template>

<script>
import * as signalR from "@aspnet/signalr";

export default {
  name: "Chat",
  props: {
    msg: String
  },
  data() {
    return {
      connection: null,
      input: "",
      mesages: []
    };
  },
  created() {
    this.connectServer();
  },
  methods: {
    connectServer() {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl("https:localhost:5001/hubs/chathub", {
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets,
          accessTokenFactory: () => {
            return "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYW1lIjoiYWEiLCJuYmYiOjE1OTcwMjg1MzEsImV4cCI6MTU5OTYyMDUzMSwiaXNzIjoiQ2hhdE9ubGluZSIsImF1ZCI6ImNoYXQtb25saW5lIn0.ZlQraacKAWVXzDySSljiuFpkeVpYADKcsE5UbtaqXcE"
            // Get and return the access token.
            // This function can return a JavaScript Promise if asynchronous
            // logic is required to retrieve the access token.
          }
        })
        .configureLogging(signalR.LogLevel.Information)
        .build();

      this.connection.on("ReceiveMessage", message => {
        this.mesages.push(message);
      });

      this.connection.start();
    },
    /**
     * 发送消息
     */
    sendMessage(message) {
      this.connection
        .invoke("SendMessage", message)
        .catch(err => console.error(err));

      this.input = "";
    },
    /**
     * 清空信息
     */
    clearMessages() {
      this.mesages = [];
    }
  }
};
</script>

<style scoped lang="scss">
.chat {
  display: flex;
  align-items: center;
  flex-direction: column;
}
.chat-list {
}
.chat-operation {
  display: flex;
  align-items: center;
  flex-direction: row;
  margin-top: 20px;
}
.text {
  font-size: 14px;
}

.item {
  text-align: left;
  margin-left: 20px;
  margin-bottom: 18px;
}

.clearfix:before,
.clearfix:after {
  display: table;
  content: "";
}
.clearfix:after {
  clear: both;
}

.box-card {
  width: 480px;
  height: 500px;
  overflow: auto;
}
</style>
