import { createWebHistory, createRouter } from "vue-router";
import Home from "@/views/Home.vue";
import Browse from "@/views/Browse.vue";
import Design from "@/views/Design.vue";
import Login from "@/views/Login.vue";
import Logout from "@/views/Logout.vue";
import Profile from "@/views/Profile.vue";

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/browse",
    name: "Browse",
    component: Browse,
  },
  {
    path: "/design",
    name: "Design",
    component: Design,
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/logout',
    name: 'Logout',
    component: Logout
  },
  {
    path: '/profile',
    name: 'profile',
    component: Profile
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;