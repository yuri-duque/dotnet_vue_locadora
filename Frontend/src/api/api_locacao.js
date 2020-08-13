import apiUtils from "./api_utils.js";

export default {
    getAll() {
        return apiUtils.get("locacao");
    },

    alugar(data) {
        return apiUtils.post("locacao/alugar", data);
    },

    devolver(data) {
        return apiUtils.post("locacao/devolver", data);
    },

    editar(id, data) {
        return apiUtils.put('locacao', data, id);
    },

    deletar(id) {
        return apiUtils.delete(`locacao/${id}`);
    },

    getById(id){
        return apiUtils.get(`locacao/${id}`);
    }
};