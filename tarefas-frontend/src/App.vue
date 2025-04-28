<template>
  <div id="app">
    <ListaTarefas />
  </div>
</template>

<script>
import ListaTarefas from './components/ListaTarefas.vue'

export default {
  name: 'App',
  components: {
    ListaTarefas
  }
}
</script>

<style>
#app {
  font-family: 'Poppins', 'Avenir', Helvetica, Arial, sans-serif;
  background: #0f0f1c;
  color: #ffffff;
  min-height: 100vh;
  display: flex;
  justify-content: center;
  align-items: flex-start;
  padding-top: 50px;
}
</style>