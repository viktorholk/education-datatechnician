import Vue from 'vue'
import App from './App.vue'

Vue.config.productionTip = false

import store from './store'
// import vuetify from './plugins/vuetify'

new Vue({
  render: h => h(App),
  // vuetify,
  store: store
}).$mount('#app')
