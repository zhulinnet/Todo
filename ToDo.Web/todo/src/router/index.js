import Vue from "vue";
import VueRouter from "vue-router";
// import Home from "../views/Home.vue";

Vue.use(VueRouter);

const routes = [
  // {
  //   path: "/",
  //   name: "Home",
  //   component: Home
  // },
  // {
  //   path: "/about",
  //   name: "About",
  //   // route level code-splitting
  //   // this generates a separate chunk (about.[hash].js) for this route
  //   // which is lazy-loaded when the route is visited.
  //   component: () =>
  //     import(/* webpackChunkName: "about" */ "../views/About.vue")
  // },
  {
    path: "/login",
    name: "login",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/login.vue"),
  },
  {
    path: "/register",
    name: "register",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/register.vue"),
  },
  {
    path: "/index",
    name: "index",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/layout.vue"),
    children: [
      {
        path: "/index",
        name: "index",
        component: () =>
          import(/* webpackChunkName: "about" */ "../views/index.vue"),
      },
      {
        path: "/share",
        name: "share",
        component: () =>
          import(/* webpackChunkName: "about" */ "../views/share.vue"),
      },
      {
        path: "/item/:id",
        name: "item",
        component: () =>
          import(/* webpackChunkName: "about" */ "../views/item.vue"),
      },
    ],
  },
];

const router = new VueRouter({
  routes,
});

export default router;
