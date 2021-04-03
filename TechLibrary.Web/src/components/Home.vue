<template>
    <div class="home">
        <h1>{{ msg }}</h1>

        <b-table ref="table" striped hover :items="dataContext" :fields="fields" responsive="sm">
            <template v-slot:cell(thumbnailUrl)="data">
                <b-img :src="data.value" thumbnail fluid></b-img>
            </template>
            <template v-slot:cell(title_link)="data">
                <b-link :to="{ name: 'book_view', params: { 'id' : data.item.bookId, 'PageNumber' : currentPage } }">{{ data.item.title }}</b-link>
            </template>
        </b-table>
        <div style="padding-top: 15px;">
            <button class="btn btn-danger btn-sm" style="margin-right: 10px;" @click="goFirstPage" :disabled="firstPage">First Page</button>
            <button class="btn btn-danger btn-sm" style="margin-right: 10px;" @click="prevPage" :disabled="firstPage">Previous</button>
            <button class="btn btn-success btn-sm" @click="nextPage">Next</button>
            <button class="btn btn-info btn-lg" style="float: right; margin-bottom:25px;" @click="AddBook">Add Book</button>
        </div>

    </div>
</template>

<script>
    import axios from 'axios';    

    
    export default {
        name: 'Home',
        props: {
            msg: String
        },
        data: () => ({
            fields: [
                { key: 'thumbnailUrl', label: 'Book Image' },
                { key: 'title_link', label: 'Book Title', sortable: true, sortDirection: 'desc' },
                { key: 'isbn', label: 'ISBN', sortable: true, sortDirection: 'desc' },
                { key: 'descr', label: 'Description', sortable: true, sortDirection: 'desc' }

            ],
            items: [],
            pageSize: 10,
            currentPage: 1, // this serves as the pageNumber
        }),
        
        
        methods: {
            dataContext(ctx, callback) {
                if (this.$route.query.PageNumber != null) {
                    this.currentPage = this.$route.query.PageNumber;
                }
                axios.get('https://localhost:5001/books?pageNumber=' + this.currentPage + '&pageSIze=' + this.pageSize)
                    .then(response => {
                        
                        callback(response.data);
                    });

                console.log('home', this.$route);

            },
            nextPage: function () {
                this.currentPage++;
                this.$refs.table.refresh();
            },

            prevPage: function () {
                if (this.currentPage > 1) this.currentPage--;                
                this.$refs.table.refresh();
                this.$router.replace('/')
                console.log('Prev', this.$route.query.PageNumber);
            },
            AddBook: function () {
                this.$router.push('/book');
            },
            goFirstPage: function () {
                this.currentPage = 1;
                this.$refs.table.refresh();
                this.$router.replace('/')
            }

        },
        computed: {
            firstPage() {
                return this.currentPage === 1;
            },
        }
    };


</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>

