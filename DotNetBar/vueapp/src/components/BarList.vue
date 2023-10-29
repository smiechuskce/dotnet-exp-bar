<template>
    <div v-for="bar in allBars" :key="bar.id">
        <BarItem :instance="bar"/>
    </div>
</template>

<script lang="ts">
    import BarItem from './BarItem.vue';

    export default {
        components: {
            BarItem
        },
        data() {
            return {
                allBars: null
            }
        },
        mounted() {
            this.fetchBars();
        },
        methods: {
            fetchBars() {
                fetch('barmanagement/get-bars')
                    .then(response => response.json())
                    .then(bars => {
                        this.allBars = bars;
                        return;
                    })
            }
        },
    };
</script>

<style>
</style>