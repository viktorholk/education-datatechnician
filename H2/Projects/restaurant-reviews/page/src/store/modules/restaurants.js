import RestaurantDataService from "../../services/RestaurantDataService"


export default {
    namespaced: true,
    state: {
        selection: null,
        restaurants: []
    },
    mutations: {
        restaurants(state, restaurants) {
            state.restaurants = restaurants
            // Update the map
            
        },
        selection(state, selection) {

            if (state.selection !== selection) {
                state.selection = selection 
            } else {
                state.selection = null
            }
        },
        update(state, restaurant) {
            console.log('fou')

            const index = state.restaurants.findIndex( i => i._id === restaurant._id)
            if (index > -1) {
                console.log(state.restaurants[index])
                state.restaurants[index] = restaurant
                console.log(state.restaurants[index])
            }
        }
    },
    actions: {
        getRestaurants(context) {
            RestaurantDataService.getAll().then( response => {
                context.commit('restaurants', response.data)
                // context.commit('selection', response.data[0])
                context.dispatch('map/init', null, { root: true})
            })
            
        },
        async addReview(context, data) {
            return new Promise( (resolve, reject) => {
                const id = data._id

                const review = {
                    rating: data.rating,
                    description: data.description
                }
    
                RestaurantDataService.update(id, review).then( (response) => {
                    // Update the restaurant in the state after update
                    context.commit('update', response.data)

                    // Resolve the id of the updated restaurant
                    resolve(response.data._id)

                }).catch( (err) => {
                    reject(err)
                })
            })
        },
        selection(context, selection) {
            context.commit('selection', selection)
        }
    },
    getters: {
        restaurants: (state) => {
            return state.restaurants
        },
        selection: (state) => {
            return state.selection
        },
        rating: (state) => {
            const reviews = state.selection.reviews

            let total = 0
            
            for (const review of reviews) {
                total += review.rating
            }
    
            const average = total / reviews.length
            return parseFloat(average.toFixed(1))
        }
    }
}