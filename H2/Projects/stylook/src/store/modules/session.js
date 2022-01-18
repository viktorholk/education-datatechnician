export default {
    namespaced: true,
    state: {
        users: [
            {
                id:         1,
                username:   'admin',
                password:   'admin'
            },
            {
                id:         2,
                username:   'holk',
                password:   'deeznutz'
            },
            {
                id:         3,
                username:   'Freddy_Fraek',
                password:   'deeznutz2'
            },
        ],
        user: null,
    },
    mutations: {
        updateUser(state, data) {
            state.user = data
            console.log(state.user)
        }
    },
    actions: {
        logout({commit}) {
            commit('updateUser', null)
        }
    },
    getters: {
        users(state) {
            return state.users
        },
        isAuthenticated(state){
            return state.user !== null
        },
        user(state) {
            if (state.user !== null) {
                return state.user
            }
            return null
        }
    }
}