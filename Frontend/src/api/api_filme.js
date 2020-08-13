import apiUtils from "./api_utils.js";

export default {
    getAll() {
        return apiUtils.get("filme");
    },

    getOptionsSelect() {
        return apiUtils.get("filme/options-select");
    },

    salvar(data) {
        return apiUtils.post("filme", data);
    },

    editar(id, data) {
        return apiUtils.put('filme', data, id);
    },

    deletar(id) {
        return apiUtils.delete(`filme/${id}`);
    },

    getById(id){
        return apiUtils.get(`filme/${id}`);
    }
};