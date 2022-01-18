<template>
  <div id="nav">
    
    <router-link class="title" to="/"><h1>STYLOOK</h1></router-link>
    <router-link class="link" to="/browse">Browse</router-link>
    <router-link class="link" to="/design">Design</router-link> 
    <div v-if="isAuthenticated">
      <router-link class="link" to="/profile">Profile</router-link>
      <router-link class="link"  to="/logout">Logout</router-link>
    </div>
    <router-link class="link" v-if="!isAuthenticated" to="/login">Login</router-link>
  </div>
  <router-view />


  <img id="shopping-cart" :src="'/cart.png'">

  <div class="footer">
    <a href="#">About </a>
    <a href="#">Contact </a>
  </div>
</template>

<script>
export default {

  computed: {
    isAuthenticated(){
      return this.$store.getters['session/isAuthenticated']
    }
  }
}

</script>

<style>

#shopping-cart {
  position: fixed;
  top: 0;
  right: 0;
  margin: 1em;
  color: white;
  cursor: pointer;
  transition: all 250ms ease-in-out;
  height: 2em;
}
#shopping-cart:hover{
  transform: scale(1.05);
}


body, html {
  margin: 0;
  padding: 0;
  background: #121212;
}

* {
  font-family: 'Roboto', sans-serif;
}
h1 {
  color: #ff313f;
}
p {
  color: white !important;
}

.title {
  font-size: 1em !important;
  text-align: center;
}

#app {
  display: flex;
  align-items: center;
  flex-direction: column;
  min-height: 100vh;
}
#nav {
  margin: 1em;
  width: 100%;
  display: flex;
  justify-content: center;
}
#nav h1{
  display: inline-block;
  margin: 0;
  color: #ff313f;
  cursor:pointer;
}
.link {
  display: inline-block;
  margin: 0.3em;
  text-decoration: none ;
  color: #ffffff ;
  font-size: 1em;
  transition: 250ms;
  margin-inline: 0.5em;
  vertical-align: top;
  line-height: 2em;
}
.link:hover {
  transform: scale(1.05);
}
.link::after {
  display: block;
  content: '';
  border-bottom: solid 2px #ff313f;
  transform: scaleX(0);
  transition: transform 150ms ease-in-out;
}
.link:hover:after{
  transform: scaleX(1); 
}

button {
  background: #ff313f;
  color: white;
  cursor: pointer;
  border: none;
  width: 4em;
  font-size: 1.3em;
  transition: 100ms ease-in-out;
}
button:hover{
  transform: scale(1.05);
}

@media screen and (-webkit-min-device-pixel-ratio:0) {
    input[type='range'] {
      overflow: hidden;
      width: 80px;
      -webkit-appearance: none;
      background-color: #555555;
    }

    input[type='range']::-webkit-slider-thumb {
      width: 10px;
      -webkit-appearance: none;
      height: 10px;
      cursor: ew-resize;
      background: #121212;
      box-shadow: -80px 0 0 80px #ff313f;
    }
}

.footer{
  position: fixed;
  left: 0;
  bottom: 0;
  text-align: center;
  width: 100%;
  margin-bottom: 1em;
}
.footer a{
  text-decoration: none;
  color: white;
  margin-inline: 1em;
  transition: transform 150ms ease-in-out;
  display: inline-block;
}
.footer a:hover{
  transform: scale(1.05);
}


.design-browse {
  height: 15em;
  margin-inline: 0.5em;
  transition: all 250ms ease-in-out;
}
.design-browse:hover{
  transform: scale(1.015);
  margin-inline: 1em;
}

</style>
