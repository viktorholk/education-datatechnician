<template>
  <div>
      <h1 class="title">BROWSE</h1>
      <div class="browser">
      <h2>THERE ARE {{this.images.length}} CLOTHES TO BROWSE FROM</h2>

        <div class="items">
          <div @click="openImage(img)" class="item" v-for="img in images" :key="img.id">
              <img :src="img.pathLong">
          </div>
        </div>
      </div>
  </div>
</template>

<script>
export default {
    data(){
        return {
            images: []
        }
    },
  mounted() {
    this.importAll(require.context('../assets/browse', true, /\.png$/));
  },

  methods: {
    importAll(r) {
      r.keys().forEach(key => (this.images.push({ pathLong: r(key), pathShort: key })));
    },
    openImage(img){
      window.open(img.pathLong)
    }
  },
}
</script>

<style scoped>
.browser {
  display: inline-block;
  padding: 1em;
  
}
.browser h2{
  color: white;
  text-align: center;
}

.browser .items{
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  justify-content: center;
}

.browser .item {
  cursor: pointer;
  transition: all 250ms ease-in-out;
  background-color: #1d1d1d;
  border-radius: 1em;
  margin: 1em;
  text-align: center;
}
.browser .item:hover{
  transform: scale(1.05);
}
.browser .item img{
  height: 10em;
}
</style>