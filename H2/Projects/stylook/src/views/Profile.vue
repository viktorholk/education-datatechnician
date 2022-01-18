<template>
  <div>
      <h1 class="title">PROFILE</h1>
      <div v-if="user">
          <p>Welcome back <span>{{user.username}}</span></p>
      </div>
      <p v-else>You need to be authenticated to use this page.</p>
  </div>
</template>

<script>
export default {
  computed: {
    user(){
      return this.$store.getters['session/user']
    }
  }
}
</script>

<style>
span {
    color: #ff313f;
    font-size: 1em;
    border-bottom: 1px solid;
}
</style>