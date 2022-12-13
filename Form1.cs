using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace openApICaller
{
    public partial class Form1 : Form
    {
        int seq;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ComboBox1_init();
            comboBox2_init();
            
        }

        
        //public Label lb = new Label();
        //public TextBox txtB = new TextBox();

        Label lb1 = new Label();
        Label lb2 = new Label();
        Label lb3 = new Label();
        Label lb4 = new Label();
        Label lb5 = new Label();
        Label lb6 = new Label();
        TextBox txtB1 = new TextBox();
        TextBox txtB2 = new TextBox();
        TextBox txtB3 = new TextBox();
        TextBox txtB4 = new TextBox();
        TextBox txtB5 = new TextBox();
        TextBox txtB6 = new TextBox();

        public string results = string.Empty;
        private void btn_add_params_Click(object sender, EventArgs e)
        {
            List<string> lb_list = new List<string>(new string[] { "날짜 코드","조회기간_시작일(YYYYMMDD)",
                    "조회기간_종료일(YYYYMMDD)(전일(D-1)까지 제공)", "지점 번호" });
            List<string> txtB_list = new List<string>(new string[] { "DAY", "20100101", "20100101", "108" });
            TextBox txtB = new TextBox();
            Label lb = new Label();
            // 텍스트 박스 순차적으로 추가
            if (seq == txtB_list.Count)
            {
                MessageBox.Show("더 이상 생성할 수 없습니다.");
                return;
            }
            else
            {
                Console.WriteLine(seq);

                txtB.Name = String.Format("txtB_{0}", seq);
                lb.Location = new Point(0, 30 * seq);
                lb.Text = lb_list[seq];
                txtB.Location = new Point(100, 30 * seq);
                txtB.Text = txtB_list[seq];
                //txtB.KeyDown += new KeyEventHandler(KeyDown);
                //txtB.Location = new Point(100, 100 * seq);
                //txtB.KeyDown += new KeyEventHandler(t_KeyDown);  //KeyDown 이벤트

                panel1.Controls.Add(lb);
                panel1.Controls.Add(txtB);
                seq++;
                //MessageBox.Show(String.Format("txtB name : {0}, Location : {1}", txtB.Name, txtB.Location));
            }

        }




        private void btn_drop_paramas_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            seq = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 버튼 클릭, url request event
            string call_url=string.Empty;

            //url += "&ServiceKey=" + textBox2.Text;
            //url += "&dataType=" + comboBox1.SelectedItem.ToString();
            if (comboBox2.SelectedIndex == 0)
            {
                call_url = call_url_day();
                Console.WriteLine(call_url);
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                call_url = call_url_hr();
                Console.WriteLine(call_url);
            }
            Console.WriteLine(call_url);

            var request = (HttpWebRequest)WebRequest.Create(call_url);
            request.Method = "GET";


            HttpWebResponse response;
            using (response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                results = reader.ReadToEnd();
            }
            //Console.WriteLine(results);
            textBox3.Text = results;
            MessageBox.Show("Finish");
        }


        
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(comboBox2.SelectedItem.ToString());
            Console.WriteLine(comboBox2.SelectedIndex);
            panel1.Controls.Clear(); //초기화
            
            if (comboBox2.SelectedIndex == 0)
            {
                textBox1.Text = "http://apis.data.go.kr/1360000/AsosDalyInfoService/getWthrDataList?";
                List<string> lb_list = new List<string>(new string[] { "날짜 코드","조회기간_시작일(YYYYMMDD)",
                    "조회기간_종료일(YYYYMMDD)(전일(D-1)까지 제공)", "지점 번호" });
                List<string> txtB_list = new List<string>(new string[] {"DAY", "20100101", "20100101", "108" });
                

                // "조회 기간 시작일(YYYYMMDD)", "조회 기간 종료일(YYYYMMDD) (전일(D-1)까지 제공)", "지점 번호"
                lb1.Text = lb_list[0];
                lb2.Text = lb_list[1];
                lb3.Text = lb_list[2];
                lb4.Text = lb_list[3];

                lb1.Location = new Point(0, 30);
                lb2.Location = new Point(0, 60);
                lb3.Location = new Point(0, 90);
                lb4.Location = new Point(0, 120);

                panel1.Controls.Add(lb1);
                panel1.Controls.Add(lb2);
                panel1.Controls.Add(lb3);
                panel1.Controls.Add(lb4);

                txtB1.Location = new Point(200, 30);
                txtB2.Location = new Point(200, 60);
                txtB3.Location = new Point(200, 90);
                txtB4.Location = new Point(200, 120);
 
                txtB1.Text = txtB_list[0];
                txtB2.Text = txtB_list[1];
                txtB3.Text = txtB_list[2];
                txtB4.Text = txtB_list[3];

                panel1.Controls.Add(txtB1);
                panel1.Controls.Add(txtB2);
                panel1.Controls.Add(txtB3);
                panel1.Controls.Add(txtB4);

                


                //for (int i = 0; i < lb_list.Count; i++)
                //{
                //    Console.WriteLine(seq);
                //    lb.Name = String.Format("lb_{0}", i);
                //    lb.Text = lb_list[i];
                //    //lb.Margin = new Padding(24, 8, 0, 0);
                //    lb.Location = new Point(0, 30 * seq);
                //    Console.WriteLine(String.Format("lb name : {0}, Location : {1}", lb.Name, lb.Location));
                //    txtB.Location = new Point(100, 30 * seq);
                //    txtB.Text = txtB_list[i];

                //    panel1.Controls.Add(lb);
                //    panel1.Controls.Add(txtB);
                //    seq++;
                //}

            }
            else if (comboBox2.SelectedIndex == 1)
            {
                // 시간자료
                textBox1.Text = "http://apis.data.go.kr/1360000/AsosHourlyInfoService/getWthrDataList?";

                List<string> lb_list = new List<string>(new string[] {"날짜 코드", "조회 기간 시작일(YYYYMMDD)","조회 기간 시작시(HH)",
                    "조회 기간 종료일(YYYYMMDD) (전일(D-1)까지 제공)","조회 기간 종료시(HH)", "지점 번호" });
                List<string> txtB_list = new List<string>(new string[] { "HR","20100101","01", "20100101", "01", "108" });

                lb1.Text = lb_list[0];
                lb2.Text = lb_list[1];
                lb3.Text = lb_list[2];
                lb4.Text = lb_list[3];
                lb5.Text = lb_list[4];
                lb6.Text = lb_list[5];
                lb1.Location = new Point(0, 30);
                lb2.Location = new Point(0, 60);
                lb3.Location = new Point(0, 90);
                lb4.Location = new Point(0, 120);
                lb5.Location = new Point(0, 150);
                lb6.Location = new Point(0, 180);
                panel1.Controls.Add(lb1);
                panel1.Controls.Add(lb2);
                panel1.Controls.Add(lb3);
                panel1.Controls.Add(lb4);
                panel1.Controls.Add(lb5);
                panel1.Controls.Add(lb6);


                txtB1.Location = new Point(200, 30);
                txtB2.Location = new Point(200, 60);
                txtB3.Location = new Point(200, 90);
                txtB4.Location = new Point(200, 120);
                txtB5.Location = new Point(200, 150);
                txtB6.Location = new Point(200, 180);
                txtB1.Text = txtB_list[0];
                txtB2.Text = txtB_list[1];
                txtB3.Text = txtB_list[2];
                txtB4.Text = txtB_list[3];
                txtB5.Text = txtB_list[4];
                txtB6.Text = txtB_list[5];
                panel1.Controls.Add(txtB1);
                panel1.Controls.Add(txtB2);
                panel1.Controls.Add(txtB3);
                panel1.Controls.Add(txtB4);
                panel1.Controls.Add(txtB5);
                panel1.Controls.Add(txtB6);



            }
            else if (comboBox2.SelectedItem == "기타")
            {
                textBox1.Clear();

            }
        }

        public void ComboBox1_init()
        {
            comboBox1.Items.Add("XML");
            comboBox1.Items.Add("JSON");
            comboBox1.SelectedIndex = 0;
        }

        public void comboBox2_init()
        {
            comboBox2.Items.Add("기상청_지상(종관, ASOS) 일자료 조회서비스");
            comboBox2.Items.Add("기상청_지상(종관, ASOS) 시간자료 조회서비스");
            comboBox2.Items.Add("기타");
            comboBox2.SelectedIndex = 0;
        }

        public string call_url_hr()
        {
            string url = string.Empty;
            url = textBox1.Text;
            url += "&ServiceKey=" + textBox2.Text;
            url += "&dataType=" + comboBox1.Text;
            url += "&dataCd=ASOS";
            url += "&dateCd=" + txtB1.Text;
            url += "&startDt=" + txtB2.Text;
            url += "&startHh=" + txtB3.Text;
            url += "&endDt=" + txtB4.Text;
            url += "&endHh=" + txtB5.Text;
            url += "&stnIds=" + txtB6.Text;
            return url;
        }

        public string call_url_day()
        {
            string url = string.Empty;
            url = textBox1.Text;

            url += "ServiceKey=" + textBox2.Text;
            url += "&dataType=" + comboBox1.Text;
            url += "&dataCd=ASOS";
            url += "&dateCd=" + txtB1.Text;
            url += "&startDt=" + txtB2.Text;
            url += "&endDt=" + txtB3.Text;
            url += "&stnIds=" + txtB4.Text;
            return url;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // 파일로 저장하기
            string FileName;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "다른 이름으로 저장";
            sfd.OverwritePrompt = true;
            sfd.DefaultExt= comboBox1.SelectedItem.ToString();
            sfd.Filter = string.Format("{0}(*.{0})|*.{0}", comboBox1.SelectedItem.ToString()) +"|모든파일|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileName = sfd.FileName;
                FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
                StreamWriter stw = new StreamWriter(fs);
                stw.WriteLine(results);
                stw.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Text= comboBox1.SelectedItem.ToString();
            Console.WriteLine(comboBox1.Text);
        }
    }
}
