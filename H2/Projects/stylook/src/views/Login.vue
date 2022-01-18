<template>
  <h1 class="title">LOGIN</h1>
  <div class="login-form">
    <form v-on:submit.prevent="login">
    <label for="ass">Username</label>
    <input type="text" v-model="this.username">
    <label for="ass">Password</label>
    <input type="password" v-model="this.password">
    <input @click="login()" type="submit" value="LOGIN">
    </form>
    <p class="flash">{{this.flash}}</p>
  </div>
</template>

<script>
export default {
    data(){
        return {
            username: '',
            password: '',
            flash: ''
        }
    },
    methods: {
        login(){
            const username  = this.username
            const password  = this.password
            const users     = this.$store.getters['session/users']
            let found = false
            for (const user of users) {
                if (username == user.username && password == user.password) {
                    this.$store.commit('session/updateUser', user)
                    this.$router.push('/profile')
                    found = true
                }
            }
            console.log(found)
            if (!found) {
              this.flash = "Invalid username or password."
            }
        }
    }
}
</script>

<style scoped>

.login-form label{
  color: white;
  font-size: 0.7em;
}

.login-form input {
  display: block;
}

.flash {
  margin-top: 5em;
  color: #ff313f !important;
  font-size: 0.8em;
  text-align: center;
}

.login-form input[type="text"], input[type="password"] {
    background: white;
    border: none;
    margin-bottom: 1em;
    height: 2em;
    width: 12em;
    text-align: center;
    font-size: 1em;
}

.login-form button {
  float: right;
}


input[type="submit"] {
  background: #ff313f;
  color: white;
  cursor: pointer;
  border: none;
  width: 4em;
  font-size: 1.3em;
  transition: 100ms ease-in-out;
  float: right;
}
input[type="submit"]:hover{
  transform: scale(1.05);
}

</style>