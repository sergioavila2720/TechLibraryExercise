import Vue from 'vue';
import VueRouter from 'vue-router';

Vue.use(VueRouter);

const Home = () => import(/* webpackChunkName: "Home" */ './components/Home.vue');
const Book = () => import(/* webpackChunkName: "Book" */ './components/Book.vue');
const AddBook = () => import(/* webpackChunkName: "AddBook" */ './components/AddBook.vue');

const router = new VueRouter({
  routes: [
    { path: '/', component: Home },
    {
      name: 'book_view',
      path: '/book/:id',
      component: Book,
      props: true
    },
        {
            name: 'book_add_view',
            path: '/book',
            component: AddBook,
            props: true
    },
  ]
});

export default router;