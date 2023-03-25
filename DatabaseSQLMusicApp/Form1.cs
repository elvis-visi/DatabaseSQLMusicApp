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

        }
    }
}