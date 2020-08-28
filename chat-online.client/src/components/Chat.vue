<template>
  <div class="chat">
    <div class="chat-user">
      登录
      <el-input class="name" v-model="iMNumber" placeholder="账号"></el-input>
      <el-input v-model="password" placeholder="密码"></el-input>
      <el-button class="btn" size="small" @click="login()">登录</el-button>
    </div>

    <div class="friends">
      <el-select v-model="friendimNumber" placeholder="请选择">
        <el-option v-for="item in friends" :key="item.imNumber" :label="item.name" :value="item.imNumber" @click.native="selectCurrentFriend(item)"></el-option>
      </el-select>
    </div>

    <div class="chat-list">
      <el-card class="box-card">
        <div slot="header" class="clearfix">
          <span>chat {{ currentFriend.name }}</span>
          <el-button
            style="float: right; padding: 3px 0"
            type="text"
            @click="clearMessages()"
          >清空聊天记录</el-button>
        </div>
        <div v-for="(item, index) in mesages" :key="index" :class="{text:true, item:true}">{{ item }}</div>
      </el-card>
    </div>
    <div class="chat-operation">
      <div>
        <el-input v-model="input" placeholder="请输入内容"></el-input>
      </div>
      <div>
        <el-button @click="sendMessage()">发送消息</el-button>
      </div>
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
      iMNumber: "",
      password: "",
      access_token: "",
      connection: null,
      input: "",
      mesages: [],
      friends: [],
      friendimNumber: "",
      currentFriend: {}
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
        IMNumber: parseInt(this.iMNumber),
        Password: this.password
      };
      let that = this;
      this.$axios
        .post("https://localhost:5001/api/Account/login", params)
        .then(function(response) {
          console.log(response);
          that.access_token = response.data.access_token;
          that.getFriends();
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

      this.connection.on("ReceiveMessage", message => {
        // 收到消息
        console.log(message);
        this.mesages.push(message);
      });

      this.connection.start();
    },
    /**
     * 加载好友列表
     */
    getFriends() {
      let params = {
        iMNumber: parseInt(this.iMNumber)
      };
      let that = this;
      this.$axios
        .get(
          "https://localhost:5001/api/IMUser/friends?iMNumber=" +
            params.iMNumber
        )
        .then(function(response) {
          console.log(response);
          that.friends = response.data;
        })
        .catch(function(error) {
          console.log(error);
        });
    },
    /**
     * 选择聊天好友
     */
    selectCurrentFriend(friend) {
      console.log(friend);
      this.currentFriend = friend;
    },
    /**
     * 发送消息
     */
    sendMessage() {
      let message = {
        IMNumber: this.currentFriend.imNumber.toString(),
        Message: this.input.toString()
      };
      console.log(message);
      this.connection
        .invoke("SendMessage", message)
        .catch(err => console.error(err));

      // this.mesages.push(this.input);

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
.friends {
  margin-bottom: 10px;
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
