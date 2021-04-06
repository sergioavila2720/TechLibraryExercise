<template>
    <div class="home">

        <div class="row" style="margin-top: 17px;">
            <div class="col-sm-11" >
                <input class="form-control" type="text"  v-model="search" placeholder="Search for book" />
            </div>
            <button class="btn btn-primary btn-sm" @click="searchPage" style="margin-left: 17px;">Search</button>
        </div>
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
            <button class="btn btn-danger btn-lg" style="margin-right: 10px;" @click="goFirstPage" :disabled="firstPage">
                <i class="fas fa-angle-double-left"></i>
            </button>
            <button class="btn btn-danger btn-lg" style="margin-right: 10px;" @click="prevPage" :disabled="firstPage"><i class="fas fa-angle-left"></i>  </button>
            <button class="btn btn-success btn-lg" @click="nextPage">
                <i class="fas fa-angle-right"></i>
            </button>
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
            items: [], // It don't think this is being used. 
            pageSize: 50,
            currentPage: 1, // this serves as the pageNumber
            search: '',
        }),        
        
        methods: {
            dataContext(ctx, callback) {

                // pageNumber is saved in the route params so that if the user goes back from the book detail, 
                // it will not go back to the first page set.Instead it'll go to the current page set
                if (this.$route.query.PageNumber != null) {
                    this.currentPage = this.$route.query.PageNumber;
                }
                // declare a query for the search bar. If the search obj is not null pass it to do some filtering.
                var querySearch = "&searchStr=" + this.search;
                console.log("serch", this.search);
                axios.get('https://localhost:5001/books?pageNumber=' + this.currentPage + '&pageSIze=' + this.pageSize + querySearch)
                    .then(response => {
                        callback(response.data);
                    });

            },
            // Handles next page set
            nextPage: function () {
                this.currentPage++;
                this.$refs.table.refresh();
                this.$router.replace('/')
            },
            // handles previous page set
            prevPage: function () {
                if (this.currentPage > 1) this.currentPage--;                
                this.$refs.table.refresh();
                this.$router.replace('/')
                console.log('Prev', this.$route.query.PageNumber);
            },
            // Goes to new book form
            AddBook: function () {
                this.$router.push('/book');
            },

            // Handles going back to the first page set
            goFirstPage: function () {
                this.currentPage = 1;
                this.$refs.table.refresh();
                this.$router.replace('/')
            },

            // Refreshes table so that datacontext is fired off again
            searchPage: function () {

                this.$refs.table.refresh();                
            }

        },
        computed: {
            firstPage() {
                return this.currentPage === 1;
            }
        }
    };


</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>

