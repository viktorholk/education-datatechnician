<template>
    <div id="app">
        <div class="container">
            <h1 class="title center-text">RESTAURANT REVIEWS</h1>

            <div class="dashboard">

                <div class="panel">
                    <div id="map"></div>
                </div>

                <div class="panel" id="restaurants">
                    <h1 class="title center-text">{{ restaurants.length }} Restaurants</h1>
                    <div class="list">
                        <Restaurant v-on:restaurantClick="restaurantClick" v-for="item in restaurants" :key="item.id" :restaurant="item"/>
                    </div>

                    <div class="review">
                        <div v-if="selection">
                            <textarea v-model="review.text" ref="review-text" placeholder="Write your review here"></textarea>
                            <div class="center">
                              <select v-model="review.rating">
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                              </select>
                              <button @click="submitReview()">Submit</button>

                            </div>

                        </div>
                        <div v-else class="no-selection">
                            <p>Select a restaurant to review it!</p>
                        </div>
                    </div>
                </div>

            </div>

            <transition name="slide-fade">
              <div v-if="selection" class="panel selection-reviews">
                <div>
                  <h1 class="title">{{ selection.title }}</h1>
                  <Rating :rating="selectionRating"/>

                  <div v-for="review in selection.reviews" :key="review._id" class="selection-review">
                    <Rating class="center" :rating="review.rating" :text="false"/>
                    <p>{{review.description}}</p>
                  </div>

                </div>
              </div>
            </transition>
        </div>
    </div>
</template>

<script>
import Restaurant from './components/Restaurant.vue'
import Rating from './components/Rating.vue'
export default {
  name: 'App',
  components: {
    Restaurant,
    Rating
  },
  data(){
    return {
      review: {
        text: "",
        rating: 5
      }
    }
  },
    mounted(){
      this.$store.dispatch('data/getRestaurants')
  },
  computed: {
    restaurants(){
      const restaurants = this.$store.getters['data/restaurants']
      return restaurants
    },
    selection() {
      const selection = this.$store.getters['data/selection']
      return selection
    },
    selectionRating() {
      const rating = this.$store.getters['data/rating']
      return rating
    }
  },

  methods: {
    restaurantClick(id) {
      this.$store.dispatch('data/selection', id)
    },
    submitReview() {
      console.log(this.review.text)
      if (this.review.text.length > 0 && this.review.text !== '*') {
        // Get the selected restaurant
        const restaurant = this.$store.getters['data/selection']

        const data = {
          _id:          restaurant._id,
          rating:       this.review.rating,
          description:  this.review.text
        }

        this.$store.dispatch('data/addReview', data).then( () => {

          // Update the stars and rating for the restaurant
          this.$root.$children.forEach( i => i.$forceUpdate())
        })
        // reset review text
        this.review.text = ""
      }
    }
  }
}
</script>

<style>
html, body {
  height: 100%;
  margin: 0;
}

body {
  background-color: #2f2f2ff5;
}

h1 {
  margin: 0;
}

h1, p {
    font-family: "Gill Sans", sans-serif;
}

.center {
  display: flex;
  justify-content: center;
  align-items: center;
}

#app {
    display: flex;
    justify-content: center;
    margin-top: 2em;
}

.dashboard {
    display: flex;
    flex-direction: row;
}

.title {
  font-weight: bold;
  color:#ffd551;
}

.center-text {
  text-align: center;
}

.panel {
  background-color: #2f2f2f;
  padding: 1em;
}

#map {
  width: 40em;
  height: 40em;
}

#restaurants {
  width: 20em;
}

#restaurants .list {
  scrollbar-width: none;
  overflow-y: scroll;
  height: 65%;
  background-color: #313131;
  border-radius: 1em;
}
#restaurants .list::-webkit-scrollbar {
  display: none;
}
.review {
  padding: 1em;
}

.review .no-selection {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 5em;
    color: white;
}
.review .no-selection p {
    margin: 0;
}

.review textarea {
  width: 100%;
  height: 4em;
  resize: none;
}

.review button{
  text-align: center;
  cursor: pointer;
}


.slide-fade-enter-active {
  transition: all 0.3s ease-out;
}

.slide-fade-leave-active {
  transition: all 0.8s cubic-bezier(1, 0.5, 0.8, 1);
}

.slide-fade-enter,
.slide-fade-leave-to {
  transform: translateY(40px);
  opacity: 0;
}

.selection-reviews {
  width: 52em;
}

.selection-reviews   p {
    color: rgba(214, 214, 214, 0.986);
    text-align: center;
  }


</style>
