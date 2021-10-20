import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

import mapModule from './modules/map'
import restaurants from "./modules/restaurants";

export default new Vuex.Store({
    modules: {
        map: mapModule,
        data: restaurants
    }
})

