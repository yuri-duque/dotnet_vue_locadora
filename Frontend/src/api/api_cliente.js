import apiUtils from "./api_utils.js";

export default {
    getAll() {
        return apiUtils.get("cliente");
    },

    getOptionsSelect() {
        return apiUtils.get("cliente/options-select");
    },

    salvar(data) {
        return apiUtils.post("cliente", data);
    },

    editar(id, data) {
        return apiUtils.put('cliente', data, id);
    },

    deletar(id) {
        return apiUtils.delete(`cliente/${id}`);
    },

    getById(id){
        return apiUtils.get(`cliente/${id}`);
    }
};