<template>
  <vs-card>
    <div slot="header">
      <h3>Cadastro de locaçao</h3>
    </div>

    <div>
      <vs-row>
        <vs-col vs-lg="6" vs-sm="12" class="px-2 pt-2">
          <span class="label-campo">Cliente</span>
          <v-select
            label="text"
            v-model="selectCliente"
            :options="optionsClientes"
            class="display-inline-block mar-1"
          ></v-select>
        </vs-col>

        <vs-col vs-lg="6" vs-sm="12" class="px-2 pt-2">
          <span class="label-campo">Filme</span>
          <v-select
            label="text"
            v-model="selectFilme"
            :options="optionsFilmes"
            class="display-inline-block mar-1"
          ></v-select>
        </vs-col>
      </vs-row>

      <vs-row vs-type="flex" vs-justify="flex-end" class="mt-6 pr-2">
        <vs-button
          v-if="!produtoId"
          @click="salvar()"
          class="font-semibold"
          type="filled"
        >Cadastrar</vs-button>
        
        <vs-button
          v-if="produtoId"
          @click="salvar()"
          class="font-semibold"
          type="filled"
        >Editar</vs-button>
      </vs-row>
    </div>
  </vs-card>
</template>

<script>
import vSelect from "vue-select";
export default {
  components: {
    "v-select": vSelect,
  },

  data() {
    return {
      selectProdutoGlobal: null,
      option_ProdutoGlobal: ["01"],
    };
  },
};
</script>

<style scoped>
</style>