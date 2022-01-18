<template>
  
</template>

<script>
export default {
    mounted(){
        this.$store.dispatch('session/logout')
        this.$router.push('/')
    }
}
</script>

<style>

</style>