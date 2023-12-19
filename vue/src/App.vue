<script>
import Keycloak from './main';
import { getBooks, updateBook, deleteBook, createBook } from './service/book.api';

export default {
  name: 'App',
  data() {
    return {
      books: [],
      id: '',
      name: '',
      showUpsert: false,
    }
  },
  watch: {
    book(newVal, oldVal) {
      console.log(newVal, oldVal)
    }
  },
  async mounted() {
    this.books = await getBooks();
  },
  watch: {
    async name(newVal, oldVal) {
      console.log(newVal, oldVal)
    }
  },
  methods: {
    logout() {
      Keycloak.logout();
    },
    async deleteHandler(id) {
      await deleteBook(id);
      this.books = await getBooks();
    },
    toggleCreate(value) {
      this.showUpsert = value;
      this.id = '';
      this.name = '';
    },
    toggleUpdate(value, id) {
      this.showUpsert = value;
      const book = this.books.find(book => book.id === id);
      this.id = book.id;
      this.name = book.name;
    },
    async createHandler() {
      console.log(this.name)
      await createBook({
        name: this.name,
      });
      this.books = await getBooks();
      this.toggleCreate(false);
    },
    async updateHandler() {
      await updateBook(this.id, {
        id: this.id,
        name: this.name,
      });
      this.books = await getBooks();
      this.toggleCreate(false);
    },
  },
}

</script>

<template>
  <main class="bg-slate-900 w-screen min-h-screen text-slate-50">
    <div class="w-11/12 md:w-8/12 lg:w-6/12 mx-auto py-5">
      <div class="flex justify-between mb-5">
        <h2 class="text-2xl font-bold">Keyclok / Kütüphane</h2>
        <div class="flex gap-2">
          <button @click="toggleCreate(true)"
            class="border rounded px-3 py-1 border-slate-500 hover:bg-slate-900">+</button>
          <button @click="logout()" class="border rounded px-3 py-1 border-slate-500 hover:bg-slate-900">Çıkış
            yap</button>
        </div>
      </div>
      <div class="relative overflow-x-auto">
        <table class="w-full text-sm text-left rtl:text-right text-slate-500 dark:text-slate-400">
          <thead class="text-xs text-slate-700 uppercase bg-slate-50 dark:bg-slate-700 dark:text-slate-400">
            <tr>
              <th scope="col" class="px-6 py-3 text-left">
                Id
              </th>
              <th scope="col" class="px-6 py-3 text-center">
                Kitap
              </th>
              <th scope="col" class="px-6 py-3 text-right">
                Aksiyon
              </th>
            </tr>
          </thead>
          <tbody>
            <tr class="bg-white border-b dark:bg-slate-800 dark:border-slate-700 text-right" v-for="item in books">
              <th scope="row" class="px-6 py-4 font-medium text-slate-900 whitespace-nowrap dark:text-white text-left">
                {{ item.id }}
              </th>
              <td class="px-6 py-4 text-center">
                {{ item.name }}
              </td>
              <td class="px-6 py-4">
                <button @click="toggleUpdate(true, item.id)"
                  class="border rounded px-3 py-1 mx-1 border-slate-500 hover:bg-slate-900">Güncelle</button>
                <button @click="deleteHandler(item.id)"
                  class="border rounded px-3 py-1 mx-1 border-slate-500 hover:bg-red-900">Sil</button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <div v-show="showUpsert"
        class="z-50 absolute top-0 left-0 w-screen min-h-screen bg-slate-900 bg-opacity-75 flex justify-center items-center">
        <div class="w-11/12 md:w-8/12 lg:w-6/12 mx-auto p-5 bg-slate-700 rounded">
          <div class="flex justify-between">
            <h2 class="text-2xl font-bold">
              <span v-if="id == ''">Kitap Ekle</span>
              <span v-else-if="id != ''">Kitap Güncelle</span>
            </h2>
            <button class="border rounded px-3 py-1 border-slate-500 hover:bg-slate-900">X</button>
          </div>
          <div class="mt-4">
            <label for="name" class="block mb-2 text-sm font-medium text-slate-900 dark:text-white">İsim</label>
            <input type="text" id="name" v-model="name"
              class="bg-slate-50 border border-slate-300 text-slate-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-slate-700 dark:border-slate-600 dark:placeholder-slate-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
              placeholder="deneme" required>
          </div>
          <div class="flex mt-5 justify-end">
            <button v-if="id == ''" @click="createHandler"
              class="border rounded px-3 py-1 border-slate-500 hover:bg-slate-900">Ekle</button>
            <button v-else-if="id != ''" @click="updateHandler"
              class="border rounded px-3 py-1 border-slate-500 hover:bg-slate-900">Güncelle</button>
          </div>
        </div>
      </div>
    </div>
  </main>
</template>
