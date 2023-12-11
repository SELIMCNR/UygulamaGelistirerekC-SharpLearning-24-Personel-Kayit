using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Personel_Kayıt
{
    public partial class Frm_AnaForm : Form
    {
        //Data Source=DESKTOP-BC3LOP2\SELM;Initial Catalog=PersonelVeritabanı;Integrated Security=True;Trust Server Certificate=True
        public Frm_AnaForm()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-BC3LOP2\\SELM;Initial Catalog=PersonelVeritabanı;Integrated Security=True;");
     
        void temizle()
        {
            Txt_id.Text = "";
            Txt_Ad.Text = "";
            Txt_Soyad.Text = "";
            Txt_Meslek.Text = "";
            mtxt_maas.Text = "";
            Cmb_Sehir.Text = "";
            Rdb_bekar.Checked=false;
            Rdb_evli.Checked=false;
            Txt_Ad.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           this.tbl_PersonelTableAdapter.Fill(this.personelVeritabanıDataSet.Tbl_Personel);
                 label8.Visible= false;

        }

        private void Btn_Listele_Click(object sender, EventArgs e)
        {
            this.tbl_PersonelTableAdapter.Fill(this.personelVeritabanıDataSet.Tbl_Personel); //veritabanındaki tablodan verileri getir
        }

        private void Btn_Kaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open(); //bağlantı aç

            SqlCommand komut = new SqlCommand("insert into Tbl_Personel(Perad, PerSoyad,PerSehir,PerMaas,PerDurum,PerMeslek) values(@p1,@p2,@p3,@p4,@p5,@p6)",baglanti); // tabloya parametre ile ekleme yap
            komut.Parameters.AddWithValue("@p1", Txt_Ad.Text); // ekleme yapılacak parametredeki değeri textboxtan al
            komut.Parameters.AddWithValue("@p2",Txt_Soyad.Text);
            komut.Parameters.AddWithValue("@p3", Cmb_Sehir.Text);
            komut.Parameters.AddWithValue("@p4", mtxt_maas.Text);
            komut.Parameters.AddWithValue("@p5",label8.Text);
            komut.Parameters.AddWithValue("@p6", Txt_Meslek.Text);
            komut.ExecuteNonQuery();    //komutu uygula çalıştır.
            MessageBox.Show("İşlem tamamlandı", "Veritabanı ekleme işlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            baglanti.Close(); //bağlantıyı kapat.
        }

        private void Rdb_evli_CheckedChanged(object sender, EventArgs e)
        {
            if(Rdb_evli.Checked == true)
            {
                label8.Text = "True";
            }
        }

        private void Rdb_bekar_CheckedChanged(object sender, EventArgs e)
        {
            if(Rdb_bekar.Checked == true)
            {
                label8.Text = "False";
            }
        }

        private void Btn_Temizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

       

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //data gridviewde seçilen hücreyi textboxa ekleme
            int secilen = dataGridView1.SelectedCells[0].RowIndex;


            Txt_id.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            Txt_Ad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            Txt_Soyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            Cmb_Sehir.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mtxt_maas.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString(); 
            label8.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            Txt_Meslek.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();    

        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            if(label8.Text == "True")
            {
                Rdb_evli.Checked = true;
            }
            if(label8.Text == "False")
            {
                Rdb_bekar.Checked = true;
            }
        }

        private void Btn_Sil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutSil = new SqlCommand("Delete From Tbl_Personel Where Perid =@k1",baglanti);
            komutSil.Parameters.AddWithValue("@k1", Txt_id.Text);
            komutSil.ExecuteNonQuery();
            baglanti.Close();
        }

        private void Btn_Güncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutguncelle = new SqlCommand("update Tbl_Personel Set PerAd=@a1,PerSoyad=@a2,PerSehir=@a3,PerMaas=@a4,PerDurum=@a5,PerMeslek=@a6 where Perid=@a7",baglanti);
            komutguncelle.Parameters.AddWithValue("@a1",Txt_Ad.Text);
            komutguncelle.Parameters.AddWithValue("@a2", Txt_Soyad.Text);
            komutguncelle.Parameters.AddWithValue("@a3",Cmb_Sehir.Text);
            komutguncelle.Parameters.AddWithValue("@a4",mtxt_maas.Text);
            komutguncelle.Parameters.AddWithValue("@a5", label8.Text);
            komutguncelle.Parameters.AddWithValue("@a6", Txt_Meslek.Text);
            komutguncelle.Parameters.AddWithValue("@a7",Txt_id.Text);
            komutguncelle.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Personel bilgi güncellendi.", "Personel bilgi güncelleme işlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void Btn_İstatistik_Click(object sender, EventArgs e)
        {
            Frm_İstatistik fr = new Frm_İstatistik();
            fr.ShowDialog();
        }

        private void Btn_Grafikler_Click(object sender, EventArgs e)
        {
            FrmGrafikler fr = new FrmGrafikler();
            fr.ShowDialog();
        }
    }
}
