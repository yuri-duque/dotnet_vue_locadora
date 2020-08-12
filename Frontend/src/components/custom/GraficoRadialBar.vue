<template>
  <vs-card class="d-theme-dark-bg mx-2">
    <h3>{{titulo}}</h3>

    <apexchart
      type="radialBar"
      height="350"
      :options="chartOptions"
      :series="series"
      :color="color"
    ></apexchart>
  </vs-card>
</template>

<script>
export default {
  props: {
    titulo: "",
  },

  data() {
    return {
      series: [45],
      chartOptions: {
        chart: {
          height: 50,
          type: "radialBar",
        },
        plotOptions: {
          radialBar: {
            hollow: {
              size: "75%",
            },
          },
        },
        labels: ["Total"],

        colors: ["#F44336"],
      },
    };
  },
};
</script>

<style scoped>
</style>