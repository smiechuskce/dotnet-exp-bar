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
            <div v-for="ingredient in instance.inventory.ingredients" :key="ingredient.id">
                <span>{{ ingredient.name }}</span>: <span>{{ ingredient.count }}</span>
            </div>
        </div>
    </div>
</template>

<script lang="ts">

    export default {
        props: ['instance'],
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