namespace DatabaseSQLMusicApp
{
    public partial class Form1 : Form
    {
        //will serve as the data source for a DataGridView control on the form.
        BindingSource albumsBindingSource = new BindingSource();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();

            //The DataSource property of the albumsBindingSource object is then set to this List<Album> object. 
            // albumsBindingSource object now contains a reference to the list of album objects retrieved from the data source.
            albumsBindingSource.DataSource = albumsDAO.getAllAlbums();

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView =(DataGridView) sender;

            //get the row number clicked

            int rowClicked = dataGridView.CurrentRow.Index; // the selected row
           // MessageBox.Show("You clicked row" + rowClicked);

            String imageUrl = dataGridView.Rows[rowClicked].Cells[4].Value.ToString();

            // MessageBox.Show("Url " + imageUrl);
            pictureBox1.Load(imageUrl);
        }


    }
}