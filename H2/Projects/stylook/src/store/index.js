import { createStore } from 'vuex'

import SessionModule from './modules/session'

export default new createStore({
    modules: {
        session: SessionModule
    }
})