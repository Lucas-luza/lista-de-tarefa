import axios from 'axios';

const API_URL = 'http://localhost:5167/api/tarefas';

export default {
    listar() {
        return axios.get(API_URL);
    },
    criar(tarefa) {
        return axios.post(API_URL, tarefa);
    },
    atualizar(id, tarefa) {
        return axios.put(`${API_URL}/${id}`, tarefa);
    },
    excluir(id) {
        return axios.delete(`${API_URL}/${id}`);
    }
};