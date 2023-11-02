<template>
    <div class="bar-item">
        <div class="bar-description-field">
            <label for="barId">Bar ID</label>:
            <span id="barId">{{ instance.id }}</span>
        </div>

        <div class="bar-description-field">
            <label for="barWarehouse">Bar warehouse</label>:
            <span id="barWarehouse">{{ instance.warehouseId }}</span>
        </div>
        
        <div class="inventory">
            <h4>Inventory</h4>
            <div v-for="ingredient in instance.inventory.ingredients" :key="ingredient.name">
                <span>{{ ingredient.name }}</span>: <span>{{ ingredient.count }}</span>
            </div>
        </div>
    </div>
    <div>
        <h4>Pick an item to decrease its count</h4>
        <select v-model="selected">
            <option disabled value="">Select one...</option>
            <option v-for="ingredient in instance.inventory.ingredients" :value="ingredient.name">{{ ingredient.name }}</option>
        </select>
        <button @click="onDecrease">Decrease</button>
    </div>
</template>

<script lang="ts">

    export default {
        props: ['instance'],
        emits: ['decrease-count'],
        data() {
            return {
                selected: "",
            }
        },
        methods: {
            onDecrease() {
                this.$emit("decrease-count", JSON.stringify({
                        barId: this.instance.id,
                        ingredientName: this.selected,
                        count: 1
                    }))
            }
        }
    };
</script>

<style>
    .bar-item {
        display: flex;
        flex-direction: column;
        max-width: 400px;
    }

    .bar-item .inventory {
        margin: 10px;
    }

</style>