<template>
    <div>
        <h2> Add new book </h2>
        <form v-on:submit.prevent="submitForm">
            <div class="form-group">
                <label for="title">Title</label>
                <input type="text" required class="form-control" id="title" placeholder="Book title" v-model="bookForm.title">
            </div>
            <div class="form-group">
                <label for="isbn">ISBN #</label>
                <input type="text" class="form-control" id="isbn" placeholder="#" v-model="bookForm.isbn">
            </div>

            <div class="form-group">
                <label for="publishedDate">Published Date</label>
                <b-form-datepicker id="publishedDate" v-model="bookForm.publishedDate" class="mb-2" />
            </div>
            <div class="form-group">
                <label for="thumbnailUrl">Thumbnail Url</label>
                <input type="text" required class="form-control" id="thumbnailUrl" placeholder="https://www..." v-model="bookForm.thumbnailUrl">
            </div>
            <div class="form-group">
                <span>Description</span>
                <br>
                <textarea rows="4" cols="151" id="descr" v-model="bookForm.descr"></textarea>
            </div>

            <div class="form-group">
                <button style="float: right;" class="btn btn-primary">Submit</button>
                <b-button  to="/" variant="primary">Back</b-button>
            </div>
        </form>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        name: 'PostBook',
        data() {
            return {
                bookForm: {
                    title: '',
                    isbn: '',
                    publishedDate: '',
                    thumbnailUrl: '',
                    descr: ''
                }
            }
        },
        methods: {
            submitForm() {
                axios.post('https://localhost:5001/books/', this.bookForm)
                    .then((res) => {
                        //Perform Success Action
                        console.log(res);
                        this.$router.push('/');
                    })
                    .catch((error) => {
                        console.log(error);
                        // error.response.status Check status code
                    }).finally(() => {
                        //Perform action in always
                    });
            }
        }
    }
</script>