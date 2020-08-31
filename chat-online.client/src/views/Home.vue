<template>
  <div class="home">
    <!-- <HelloWorld msg="Welcome to Your Vue.js App" /> -->
    <!-- <Chat /> -->
    <TestChat />
  </div>
</template>

<script>
// @ is an alias to /src
// import HelloWorld from "@/components/HelloWorld.vue";
import TestChat from "@/components/TestChat.vue";
// import Chat from "@/components/Chat.vue";

export default {
  name: "Home",
  components: {
    // HelloWorld
    TestChat
    // Chat
  }
};
</script>
