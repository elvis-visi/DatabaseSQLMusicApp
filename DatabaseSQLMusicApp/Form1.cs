namespace DatabaseSQLMusicApp
{
    public partial class Form1 : Form
    {
        // BindingSource objects are used to bind data to controls like DataGridView.
        BindingSource albumsBindingSource = new BindingSource();
        BindingSource trackBindingSource = new BindingSource();

        List <Album>  albums = new List<Album> ();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();

            albums = albumsDAO.getAllAlbums();

            //The DataSource property of the albumsBindingSource object is then set to this List<Album> object. 
            // albumsBindingSource object now contains a reference to the list of album objects retrieved from the data source.

            albumsBindingSource.DataSource = albums;

            //The DataGridView control will automatically create columns for each public property of the Album
            //class and display the data in the corresponding rows.
            dataGridView1.DataSource = albumsBindingSource;

            pictureBox1.Load("https://upload.wikimedia.org/wikipedia/en/4/42/Beatles_-_Abbey_Road.jpg");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();


            albumsBindingSource.DataSource = albumsDAO.searchTitles(textBox1.Text);


            dataGridView1.DataSource = albumsBindingSource;
        }

        //event handler when user clicks on a cell of dataGridView1
        // sender  is the object that raised the event (in this case, the DataGridView control)
        //e provides information about the cell that was clicked (such as its row and column indices).
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //casts the sender object to a DataGridView control so that it can be manipulated in code.
            DataGridView dataGridView = (DataGridView)sender;

            //get the row number clicked

            int rowClicked = dataGridView.CurrentRow.Index; // the selected row
                                                            // MessageBox.Show("You clicked row" + rowClicked);

            //get the 4th column value of the currently clicked row
            String imageUrl = dataGridView.Rows[rowClicked].Cells[4].Value.ToString();

            //load the imageUrl to the pictureBox
            pictureBox1.Load(imageUrl);




            //List of Tracks is returned and the list is set as the data Source for the trackBindingSource object
            trackBindingSource.DataSource = albums[rowClicked].Tracks;

            //dataGridView2's Datasource is set to the trackBindingSource object, which causes
            //the DataGridView to display the data contained in the list of tracks
            dataGridView2.DataSource = trackBindingSource;
        }



        private void Add_Click(object sender, EventArgs e)
        {
            //add a new item to the database
            Album album = new Album
            {
                AlbumName = txt_albumName.Text,
                ArtistName = txt_albumArtist.Text,
                Year = Int32.Parse(txt_Year.Text),
                ImageUrl = txt_ImageURL.Text,
                Description = txt_description.Text
            };

            AlbumsDAO albumsDAO = new AlbumsDAO();
            int result = albumsDAO.addOneAlbum(album);
            MessageBox.Show(result + " new rows(s) inserted");


        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }

}