<template>
  <vs-card>
    <div slot="header">
      <h3>Cadastro de cliente</h3>
    </div>

    <div>
      <vs-row>
        <vs-col vs-lg="6" vs-sm="12" class="px-2 pt-2">
          <vs-input
            label="Nome"
            v-model="nome"
            class="w-full"
            name="nome"
            v-validate="'required'"
            :danger="errors.has('nome')"
            :danger-text="errors.first('nome')"
          />
        </vs-col>

        <vs-col vs-lg="6" vs-sm="12" class="px-2 pt-2">
          <vs-input
            label="CPF"
            v-model="nome"
            class="w-full"
            name="nome"
            v-validate="'required'"
            :danger="errors.has('nome')"
            :danger-text="errors.first('nome')"
          />
        </vs-col>
      </vs-row>

      <vs-row>
        <vs-col vs-lg="6" vs-sm="12" class="px-2 pt-2">
          <vs-input
            label="Data de nascimento"
            v-model="nome"
            class="w-full"
            name="nome"
            v-validate="'required'"
            :danger="errors.has('nome')"
            :danger-text="errors.first('nome')"
          />
        </vs-col>
      </vs-row>

      <vs-row vs-type="flex" vs-justify="flex-end" class="mt-6 pr-2">
        <vs-button v-if="!produtoId" @click="salvar()" class="font-semibold" >Cadastrar</vs-button>

        <vs-button v-if="produtoId" @click="salvar()" class="font-semibold" type="filled">Editar</vs-button>
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