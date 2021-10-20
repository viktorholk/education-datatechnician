import mapboxgl from 'mapbox-gl';
// import { MglMap, MglMarker } from 'vue-mapbox';


export default {
    namespaced: true,
    state: {
        map: null,
        loaded: false
    },
    mutations: {
        load (state, map){
            state.map = map
            state.loaded = true 
        }
    },
    actions: {
        init (context) {
            if (!context.state.loaded) {
                // Set the access token and create the map
                // After the Map has been created it will go to the map container
                mapboxgl.accessToken = 'pk.eyJ1IjoidGFjdG9jIiwiYSI6ImNrdW1keXU0YzFjcnUyd282aDYyeXdlZXAifQ.QN19HB2LTkESP-xXUCBLcQ'
                const map = new mapboxgl.Map({
                    container: 'map',
                    style: 'mapbox://styles/tactoc/ckume4jx8fwdz17o4wz0zg24k?optimize=true',
                    attributionControl: false,
                })

                // Save the map
                context.commit('load', map)

                const restaurants = context.rootGetters['data/restaurants']

                for (const restaurant of restaurants) {
                    // Create the marker
                    const marker = new mapboxgl.Marker({
                        color: "#ffd551"
                    })

    
                    marker.setLngLat(restaurant.coordinates)
                    marker.addTo(map)
                    marker.properties = restaurant

                    // Create the popup
                    const markerHeight = 30;
                    const markerRadius = 10;
                    const linearOffset = 25;
                    const popupOffsets = {
                        'top': [0, 0],
                        'top-left': [0, 0],
                        'top-right': [0, 0],
                        'bottom': [0, -markerHeight],
                        'bottom-left': [linearOffset, (markerHeight - markerRadius + linearOffset) * -1],
                        'bottom-right': [-linearOffset, (markerHeight - markerRadius + linearOffset) * -1],
                        'left': [markerRadius, (markerHeight - markerRadius) * -1],
                        'right': [-markerRadius, (markerHeight - markerRadius) * -1]
                    };

                    const popup = new mapboxgl.Popup({
                        offset: popupOffsets,
                    })
                    popup.setLngLat(restaurant.coordinates)
                    popup.setHTML(restaurant.title)


                    marker.getElement().addEventListener('mouseenter', () => { 
                        popup.addTo(map)
                    })

                    marker.getElement().addEventListener('mouseleave', () => { 
                        popup.remove()
                    })

                    marker.getElement().addEventListener('click', () => {
                        // Update the selection
                        context.dispatch('data/selection', restaurant, { root: true })
                        map.flyTo({
                            center: restaurant.coordinates,
                            zoom: 13,
                            speed: 5,
                            curve: 1,
                            });
                    })
                }
            }
        },

        update (context, map) {
            context.commit('load', map)
        }
    },
    getters: {
        get (state) {
            return state.map
        }
    }
}