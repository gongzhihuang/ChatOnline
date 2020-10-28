<template>
  <div class="chat">
    <div class="chat-user">
      登录
      <el-input class="name" v-model="userName" placeholder="用户名"></el-input>
      <el-input v-model="password" placeholder="密码"></el-input>
      <el-button class="btn" size="small" @click="login()">登录</el-button>
    </div>
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
import * as signalR from "@microsoft/signalr";

export default {
  name: "Chat",
  props: {
    msg: String
  },
  data() {
    return {
      userName: "",
      password: "",
      access_token: "",
      connection: null,
      input: "",
      mesages: []
    };
  },
  created() {
    // this.connectServer();
  },
  methods: {
    /**
     * 用户登录
     */
    login() {
      let params = {
        name: this.userName,
        password: this.password
      };
      let that = this;
      this.$axios
        .post("https://localhost:5001/Account/login", params)
        .then(function(response) {
          console.log(response);
          that.access_token = response.data.access_token;
          that.connectServer(); //登录成功，连接到signar服务
        })
        .catch(function(error) {
          console.log(error);
        });
    },
    connectServer() {
      let that = this;
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl("https:localhost:5001/hubs/chathub", {
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets,
          accessTokenFactory: () => {
            return that.access_token;
          }
        })
        .configureLogging(signalR.LogLevel.Information)
        .build();

      //调用signalr服务方法，发送信息
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
        .invoke("SendMessageToCaller", message)
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
.chat-user {
  margin-bottom: 10px;
  .name {
    margin-top: 10px;
    margin-bottom: 10px;
  }
  .btn {
    margin-top: 10px;
  }
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
