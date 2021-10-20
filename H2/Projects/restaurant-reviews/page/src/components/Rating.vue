<template>
<div>
    <div class="stars-outer">
        <div :style="{ width: procentage }" class="star stars-inner" ></div>
    </div>
    <span v-if="text" class="text">{{rating}}</span>
</div>

</template>
<script>
export default {
    name: "Rating",
    props: {
        rating: Number,
        text: {
            type: Boolean,
            default: true
        }
    },
    computed: {
        procentage() {
            const starsTotal = 5
            const starPercentage = ( this.rating / starsTotal ) * 100
            // Round it up and format it
            return `${(Math.round(starPercentage / 10) * 10)}%`;

        }
    }
}
</script>

<style scoped>
.star, .text{
    vertical-align: middle;
    display: inline-block;
}
.text {
  color: white;
  margin-left: 0.5em;
}

.stars-outer {
  display: inline-block;
  position: relative;
  font-family: FontAwesome;
}
 
.stars-outer::before {
  content: "\f006 \f006 \f006 \f006 \f006";
  color: #f8cd0b3b;
}
 
.stars-inner {
  position: absolute;
  top: 0;
  left: 0;
  white-space: nowrap;
  overflow: hidden;
  width: 50%;
}
 
.stars-inner::before {
  content: "\f005 \f005 \f005 \f005 \f005";
  color: #f8ce0b;
}
</style>