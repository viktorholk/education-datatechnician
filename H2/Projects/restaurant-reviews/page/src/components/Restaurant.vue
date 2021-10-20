<template>
  <div @click="emitClick" :class="{ 'selected' : selected }" class="restaurant">
      <div class="header">
        <h1>{{restaurant.title}}</h1>

        <Rating :rating="getAverageRating"/>

      </div>
  </div>
</template>

<script>
import Rating from './Rating.vue'
export default {
    props: {
        restaurant: Object
    },
    components: {
      Rating
    },
    methods: {
      emitClick() {
        this.$emit('restaurantClick', this.restaurant)

        // Pan over to the restaurant marker
        const map = this.$store.getters['map/get']

        map.flyTo({
            center: this.restaurant.coordinates,
            zoom: 13,
            speed: 5,
            curve: 1,
            });
      }
    },
    computed: {
      selected () {
        const selection = this.$store.getters['data/selection']
        if (selection != null) {
          return selection._id === this.restaurant._id
        }
        return false

      },

      getAverageRating() {
        const reviews = this.restaurant.reviews
        let total = 0
        
        for (const review of reviews) {
          total += review.rating
        }

        const average = total / reviews.length
        return parseFloat(average.toFixed(1))
      }
    }
}
</script>

<style scoped>

.restaurant {
    margin: 0.5em;
    cursor: pointer;

    transition: all 150ms ease-in-out;
}

.restaurant:hover {
  transform: scale(1.025);
}
.selected {
      border: 1px white solid !important;
}

.restaurant .header {
    border-bottom: 1px #ffd65150 solid;
    padding: 5px;
}

.restaurant .header h1 {
    color: #ffd551;
    font-size: 1.5em;
    margin: 0;
}

.restaurant .rating-item {
    width: 100%;
    margin: 0;
    padding: 0;
}

.rating .icon, .text{
    vertical-align: middle;
    display: inline-block;
}

.rating .icon {
  fill: #ffd551;
}

.rating .text {
  color: white;
  margin-left: 0.5em;
}

.reviews {
  text-align: center;
  width: 100%;
  height: 15em;
  overflow-y: hidden;
}
.reviews .item {
  margin-bottom: 5px;
  border-bottom: 1px solid #ffd551;
}
.reviews .text {
  color: white;
  margin: 0;
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
