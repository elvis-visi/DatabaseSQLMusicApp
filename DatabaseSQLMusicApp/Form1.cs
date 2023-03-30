using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DatabaseSQLMusicApp
{
    public partial class Form1 : Form
    {
        // BindingSource objects are used to bind data to controls like DataGridView.
        BindingSource albumsBindingSource = new BindingSource();
        BindingSource trackBindingSource = new BindingSource();

        List<Album> albums = new List<Album>();
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
            //if checkBox is checked we want to update an album. Edit Album button has been clicked.
            if (checkBox1.Checked)
            {
                int albumID = int.Parse(label_albumID.Text);
                int result = albumsDAO.updateAlbum(album, albumID);
                MessageBox.Show(result + " row(s) updated");

            }
            else  // Adding a new album
            {
                int result = albumsDAO.addOneAlbum(album);
                MessageBox.Show(result + " new row(s) inserted");
            }

            //clear all the fields
            txt_albumArtist.Clear();
            txt_Year.Clear();
            txt_albumName.Clear();
            txt_description.Clear();
            txt_ImageURL.Clear();
            label_albumID.Text = "";
            checkBox1.Checked = false;

            //refresh the displayed list of albums in dataGridView1, and clear the dataGridView2 display.
            albums = albumsDAO.getAllAlbums();
            dataGridView1.DataSource = albums;
            dataGridView2.DataSource = null;

        }



        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //get the row number clicked

            int rowClicked = dataGridView2.CurrentRow.Index; // the selected row

            //get the 4th column value of the currently clicked row
            int trackID = (int)dataGridView2.Rows[rowClicked].Cells[0].Value;

            MessageBox.Show("ID of track : " + trackID);

            AlbumsDAO albumsDAO = new AlbumsDAO();

            int result = albumsDAO.deleteTrack(trackID); //1 row affected

            //after deletion delete the data Source, then reload the albums
            dataGridView2.DataSource = null;
            albums = albumsDAO.getAllAlbums();


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
        //album edit  Add Album textboxes get filled with the corresponding content of the clicked album
        private void button5_Click(object sender, EventArgs e)
        {
            int rowClicked = dataGridView1.CurrentRow.Index;

            Album editMe = albums[rowClicked];
            txt_albumName.Text = editMe.AlbumName;
            txt_albumArtist.Text = editMe.ArtistName;
            txt_Year.Text = editMe.Year.ToString();
            txt_ImageURL.Text = editMe.ImageUrl;
            txt_description.Text = editMe.Description;

            checkBox1.Checked = true;
            label_albumID.Text = editMe.ID.ToString();
        }

        //Add/update track
        private void button4_Click(object sender, EventArgs e)
        {

            // selected album, to which we will add the track
            Album albumSelected = (Album)combo_albumID.SelectedItem;

            Track newTrack = new Track
            {
                Name = txt_trackTitle.Text,
                lyrics = txt_trackLyrics.Text,
                Number = int.Parse(txt_trackNumber.Text),
                videoURL = txt_videoURL.Text,
                AlbumID = albumSelected.ID,
            };

            AlbumsDAO albumsDAO = new AlbumsDAO();
            // create new track
            int result = albumsDAO.newTrack(newTrack);
            MessageBox.Show(result + " items inserted");

            albums = albumsDAO.getAllAlbums();
            albumsBindingSource.ResetBindings(false); //refersh
            dataGridView2.DataSource = null;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();
            albums = albumsDAO.getAllAlbums();
            foreach (Album album in albums)
            {
                combo_albumID.Items.Add(album);

            }
            combo_albumID.SelectedIndex = 0;
        }

    }

}