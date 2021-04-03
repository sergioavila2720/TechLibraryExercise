<template>
    <div v-if="book">
        <b-card 
                :title="book.title"
                :img-src="book.thumbnailUrl"
                img-alt="Image"
                img-top
                tag="article"
                style="max-width: 30rem;"
                class="mb-2">
            <b-card-text v-if="!isHidden">
                {{ book.descr }}
            </b-card-text>
            <textarea v-if="isHidden" rows="10" cols="58" v-model="book.descr"></textarea>
            <br />


            <b-button @click.stop="stop" variant="primary">Back</b-button>

            <button style="float: right; margin-left: 10px;" class="btn btn-success" v-on:click="Save()">Submit</button>
            <button style="float: right;" v-on:click="isHidden = !isHidden" class="btn btn-success">Toggle Edit</button>
        </b-card>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        name: 'Book',
        props: ["id"],
        data: () => ({
            book: null,
            isHidden: false
        }),
        mounted() {
            axios.get(`https://localhost:5001/books/${this.id}`)
                .then(response => {
                    this.book = response.data;
                });
            console.log('route', this.$route);
        },
        methods: {
            Save() {
                axios({
                    method: 'PUT',
                    url: 'https://localhost:5001/books/' + this.id,
                    data: this.book
                }).then(function(response) {
                    console.log(response);
                    
                }).catch(function (error)
                {
                    console.log(error);
                });

                this.isHidden = false;
                    
            },
            stop: function () {
                this.$router.push({ path: '/', query: { PageNumber: this.$route.params.PageNumber } })
            }
        }
    }
</script>