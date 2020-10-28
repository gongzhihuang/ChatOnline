<template>
  <div>
      <el-input v-model="input" placeholder="请输入内容"></el-input>
      测试
      <div><el-button @click="sendMessage(input)">发送消息</el-button></div>

      <div v-for="(item, index) in mesages" :key="index">
          {{ item }}
        </div>
  </div>
</template>

<script>
import * as signalR from "@microsoft/signalr";

export default {
  name: "Test",
  data() {
    return {
      connection: null,
      input: "",
      mesages: []
    };
  },
  mounted() {
    this.connectServer();
  },
  methods: {
    connectServer() {
      this.connection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5099/chathub")
        // .configureLogging(signalR.LogLevel.Information)
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
