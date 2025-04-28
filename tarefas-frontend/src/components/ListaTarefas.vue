<template>
  <div class="container">
    <h1>Lista de Tarefas</h1>

    <!-- Formulário Nova Tarefa -->
    <div class="input-group">
      <input v-model="novaTarefaDescricao" placeholder="Descrição da tarefa" @keyup.enter="adicionarTarefa"/>
      <input v-model="novoResponsavel" placeholder="Responsável (opcional)" @keyup.enter="adicionarTarefa"/>
      <button @click="adicionarTarefa">➕Adicionar</button>
    </div>
        <div class="filtros">
      <button @click="filtro = 'todas'" :class="{ ativo: filtro === 'todas' }">Todas</button>
      <button @click="filtro = 'pendentes'" :class="{ ativo: filtro === 'pendentes' }">Pendentes</button>
      <button @click="filtro = 'concluidas'" :class="{ ativo: filtro === 'concluidas' }">Concluídas</button>
    </div>

    <ul class="tarefas-lista">
      <li v-for="tarefa in tarefasFiltradas" :key="tarefa.id" :class="{ concluida: tarefa.dataConclusao }">
        <div class="descricao" :class="{ editando: tarefa.editando }">
          <button @click="alternarConclusao(tarefa)" class="check-button">
            {{ tarefa.dataConclusao ? '✅' : '⭕' }}
          </button>

          <span v-if="!tarefa.editando">
            <span :class="{ riscada: tarefa.dataConclusao }">
              {{ tarefa.descricao }}
            </span>
            <small v-if="tarefa.responsavel" class="responsavel">({{ tarefa.responsavel }})</small>
          </span>

          <div v-if="tarefa.editando" class="edicao">
            <input v-model="tarefa.novaDescricao" placeholder="Edite a descrição" @keyup.enter="salvarEdicao(tarefa)"/>
            <input v-model="tarefa.novoResponsavel" placeholder="Edite o responsável (opcional)" @keyup.enter="salvarEdicao(tarefa)"/>
          </div>
        </div>

        <div class="botoes">
          <template v-if="!tarefa.editando">
            <button @click="editarTarefa(tarefa)">✏️</button>
            <button @click="excluirTarefa(tarefa.id)">🗑️</button>
          </template>
          <template v-else>
            <button @click="salvarEdicao(tarefa)">💾</button>
          </template>
        </div>
      </li>
    </ul>

    <!-- Toast -->
    <div v-if="toast.mostrar" :class="['toast', toast.tipo]">
      {{ toast.mensagem }}
    </div>
  </div>
</template>

<script>
import TarefasService from '@/services/TarefasService';

export default {
  data() {
  return {
    tarefas: [],
    novaTarefaDescricao: '',
    novoResponsavel: '',
    filtro: 'todas',
    toast: {
      mostrar: false,
      mensagem: '',
      tipo: 'sucesso'
    }
  };
},
computed: {
  tarefasFiltradas() {
    if (this.filtro === 'pendentes') {
      return this.tarefas.filter(tarefa => !tarefa.dataConclusao);
    }
    if (this.filtro === 'concluidas') {
      return this.tarefas.filter(tarefa => tarefa.dataConclusao);
    }
    return this.tarefas;
  }
},
  methods: {
    async carregarTarefas() {
      const resposta = await TarefasService.listar();
      this.tarefas = resposta.data.map(tarefa => ({
        ...tarefa,
        concluida: !!tarefa.dataConclusao,
      }));
    },
    async adicionarTarefa() {
      if (this.novaTarefaDescricao.trim() === '') {
        return this.mostrarToast('Preencha a descrição!', 'erro');
      }

      await TarefasService.criar({
        descricao: this.novaTarefaDescricao,
        responsavel: this.novoResponsavel || null,
      });

      this.novaTarefaDescricao = '';
      this.novoResponsavel = '';
      this.mostrarToast('Tarefa adicionada!', 'sucesso');
      this.carregarTarefas();
    },
    async excluirTarefa(id) {
      await TarefasService.excluir(id);
      this.mostrarToast('Tarefa excluída!', 'excluir');
      this.carregarTarefas();
    },
    editarTarefa(tarefa) {
      tarefa.editando = true;
      tarefa.novaDescricao = tarefa.descricao;
      tarefa.novoResponsavel = tarefa.responsavel || '';
    },
    async salvarEdicao(tarefa) {
      if (tarefa.novaDescricao.trim() !== '') {
        tarefa.descricao = tarefa.novaDescricao;
        tarefa.responsavel = tarefa.novoResponsavel || null;
        tarefa.editando = false;
        await TarefasService.atualizar(tarefa.id, tarefa);
        this.carregarTarefas();
        this.mostrarToast('Tarefa atualizada com sucesso!', 'sucesso');
      } else {
        tarefa.editando = false;
      }
    },
    async alternarConclusao(tarefa) {
      tarefa.dataConclusao = tarefa.dataConclusao ? null : new Date().toISOString();
      await TarefasService.atualizar(tarefa.id, tarefa);
      const mensagem = tarefa.dataConclusao ? 'Tarefa concluída!' : 'Tarefa marcada como pendente!';
      this.mostrarToast(mensagem, 'sucesso');
      this.carregarTarefas();
    },
    mostrarToast(mensagem, tipo) {
      this.toast.mostrar = true;
      this.toast.mensagem = mensagem;
      this.toast.tipo = tipo;
      setTimeout(() => {
        this.toast.mostrar = false;
      }, 2500);
    },
  },
  mounted() {
    this.carregarTarefas();
  }
};
</script>

<style scoped>
.container {
  width: 100%;
  max-width: 500px;
  margin: 0 auto;
  background: #1a1a2e;
  padding: 30px;
  border-radius: 12px;
  box-shadow: 0 0 15px rgba(0, 255, 255, 0.2);
}

h1 {
  margin-bottom: 20px;
  font-size: 28px;
  color: #00f5d4;
  text-align: center;
}

.input-group {
  display: flex;
  gap: 10px;
  margin-bottom: 20px;
  flex-wrap: wrap;
}

input {
  flex: 1;
  padding: 10px 15px;
  border: none;
  border-radius: 8px;
  background: #2c2c3f;
  color: white;
  outline: none;
}

button {
  padding: 10px 15px;
  border: none;
  border-radius: 8px;
  background: #008cff;
  color: #1a1a2e;
  font-weight: bold;
  cursor: pointer;
  transition: 0.3s;
}

button:hover {
  background: #00cfc0;
}

.tarefas-lista {
  list-style: none;
  padding: 0;
}

.tarefas-lista li {
  background:rgb(36, 36, 53);
  padding: 12px;
  margin-bottom: 10px;
  border-radius: 8px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
  transition: 0.3s;
}

.tarefas-lista li.concluida {
  background: #283747;
}

.botoes button {
  margin-left: 5px;
  background: #008cff;
  color: white;
  border-radius: 6px;
}

.botoes button:hover {
  opacity: 0.8;
}

.descricao {
  display: flex;
  align-items: center;
  gap: 8px;
  flex-wrap: wrap;
}

.descricao.editando {
  flex-direction: column;
  align-items: flex-start;
  gap: 10px;
  width: 100%; 
}

.descricao.editando input {
  width: 100%;
}

.riscada {
  text-decoration: line-through;
  color: #a9a9a9;
}

.edicao input {
  margin-top: 8px;
}

.responsavel {
  font-size: 12px;
  color: #00cfc0;
  margin-left: 5px;
}

.check-button {
  background: none;
  border: none;
  font-size: 20px;
  cursor: pointer;
  color: #00ff88;
  transition: transform 0.2s;
  text-decoration: none !important;
}

.check-button:hover {
  transform: scale(1.2);
}

.toast {
  position: fixed;
  bottom: 20px;
  right: 20px;
  background: #2c2c3f;
  color: white;
  padding: 10px 20px;
  border-radius: 8px;
  box-shadow: 0 0 10px rgba(0,255,255,0.3);
  animation: fadeIn 0.5s;
}

.toast.sucesso {
  background: #00cfc0;
}

.toast.excluir {
  background: #ff4d4d;
}

.toast.erro {
  background: #ff4d4d;
}

@keyframes fadeIn {
  from { opacity: 0; transform: translateY(20px); }
  to { opacity: 1; transform: translateY(0); }
}

/*filtros*/
.filtros {
  display: flex;
  justify-content: center;
  gap: 10px;
  margin-bottom: 20px;
}

.filtros button {
  background: #2c2c3f;
  color: #00f5d4;
  padding: 8px 14px;
  border: 1px solid #00f5d4;
  border-radius: 8px;
  cursor: pointer;
  transition: background 0.3s;
}

.filtros button:hover {
  background: #00f5d4;
  color: #1a1a2e;
}

.filtros button.ativo {
  background: #00cfc0;
  color: #1a1a2e;
  border: 1px solid #00cfc0;
}
</style>