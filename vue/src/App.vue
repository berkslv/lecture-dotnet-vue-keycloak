<script>
import Keycloak from './main';
import { getWeather } from './service/weather.api';

export default {
  name: 'App',
  data() {
    return {
      weathers: []
    }
  },
  async mounted() {
    this.weathers = await getWeather();
  },
  methods: {
    logout() {
      Keycloak.logout();
    }
  },
}

</script>

<template>
  <button @click="logout" style="margin-bottom: 30px;">Logout</button>
  <table>
    <tr>
      <th>Date</th>
      <th>Summary</th>
    </tr>
    <tr v-for="weather in weathers">
      <td>{{ weather.date }}</td>
      <td>{{ weather.summary }}</td>
    </tr>
  </table>
</template>
