<template>
  <div>
    <h1 class="title">HOME</h1>
    <div class="showcase">
      <img src="/images/party-01.jpg"/>
      <img src="/images/party-02.jpg"/>
    </div>
    <div class="description">
      <p><span>STYLOOK</span> is a webshop where you can buy, browse and design custommade ultra violet clothing.</p>
      <p>We have successfully been delivering custom clothing to various events over the last 4 years.
        <br>With our <span>advanced designing tool</span> you can draw, display images, add text and much more on your desired clothing.
        </p>
        <p>Here are some of our favourite shirts, delivered to <span>happy customers</span>.</p>
        <img class="design-browse" src="/browse/holk.png"/>
        <img class="design-browse" src="/browse/timme.png"/>
        <p>What are you waiting for! Head over to the <router-link to="design">design page</router-link> and get your own custom made clothing.</p>
    </div>
  </div>
</template>

<script>
export default {

}
</script>

<style scoped>

.showcase img {
  height: 15em;
  transition: all 250ms ease-in-out;
  margin-inline: 0.5em;
}
.showcase img:hover{
  transform: scale(1.05);
  margin-inline: 1em;
}

.description {
  text-align: center;
}

.description span {
  color: #ff313f;
  font-size: 1em;
  border-bottom: 1px solid;
}
.description p {
  font-size: 0.9em;
}

.description a{
  text-decoration: none;
  color: #ff313f;
  transition: transform 250ms ease-in-out;
}
.description a:hover{
  transform: scale(1.1);
}

</style>