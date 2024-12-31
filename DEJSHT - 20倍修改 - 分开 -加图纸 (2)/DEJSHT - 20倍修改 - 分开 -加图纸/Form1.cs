using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields.OMath;
using Spire.Doc.Fields;
using AutoCAD;
using Image = System.Drawing.Image;
using System.IO;
using Section = Spire.Doc.Section;
using System.Collections;

namespace DEJSHT
{
    public partial class Form1 : Form
    {
       //public AutoCAD.AcadApplication AcadApp;
       //public AutoCAD.AcadDocument AcadDoc;
        //这是tabpage1的变量，后续pag2+LP2，后续pag3+MQ1
        double YG = 0;
        double YH = 0;
        double YL1 = 0;
        double YL2 = 0;


        double YDD = 0;//动力系数；

        double Fg = 0;// 破断力总和；
        double K = 8;//  安全系数；//这个要改
        double α = 0;//不均匀系数；
        double rFg = 0;//钢丝绳容许荷载；
        double rFh = 0;//卡环容许荷载；


        double Ya = 0;
        double Yb = 0;
        double Yd0 = 0;
        double Yt = 0;
        double Yc = 0;
        double Yf = 0;


        double Ye = 0;
        double Yff = 0;
        double Yg = 0;


        double YSθ = 0;//表示角度的正弦
        double YN = 0;//N合股设计值
        double YN2 = 0; //N分股标准值
        double YN3 = 0; //N合股标准值

        string Gss = "";

        double Ybe = 0;
        double Yσ1 = 0;
        double Yb1 = 0;
        double Yσ2 = 0;
        double Yτ = 0;
        double Yfv = 0;
        double YZ = 0;
        double Yfc = 0;

        double YCθ = 0;//表示角度的余弦
        double NF = 0;//表示竖向力
        double NV = 0;//表示水平力
        double YσF1 = 0;//表示竖向力引起的耳板底部正应力
        double YσF2 = 0;//表示弯矩引起的的耳板底部正应力
        double Yτ1 = 0;//表示水平力引起的耳板底部剪应力
        double YσFZ = 0;//表示综合应力
        double YBJ = 0;//表示加劲肋
        double Yσc = 0;

        string[] Z1 = { "6.2", "7.7", "9.3", "11.0", "11.0", "12.5", "14.0", "15.5", "17.0", "18.5", "20.0", "21.5", "23.0", "24.5", "26.0", "28.0", "31.0", "34.0", "37.0", "40.0", "43.0", "46.0" };
        string[] Z2 = { "8.7", "11.0", "13.0", "15.0", "17.5", "19.5", "21.5", "24.0", "26.0", "28.0", "30.0", "32.5", "34.5", "36.5", "39.0", "43.0", "47.5", "52.0", "56.0", "60.5", "65.0" };
        string[] Z3 = { "11.0 ", "14.0", "16.5", "19.5", "22.0", "25.0", "27.5", "30.5", "33.0", "36.0", "38.5", "41.5", "44.0", "47.0", "50.0", "55.5", "61.0", "66.5", "72.0", "77.5", "83.0" };


        //这是tabpage2的变量，后续pag2+LP2，后续pag3+MQ1
        double YGLP2 = 0;
        double YH1LP2 = 0;
        double YL1LP2 = 0;
        double YS1LP2 = 0;
        double cosDS1 = 0;
        double sinDS1 = 0;
        double cosDH1 = 0;
        double sinDH1 = 0;
        double NS1 = 0;//合股标准值
        double NS1F = 0;// 分股标准值
        double NS1S = 0;//合股设计值

        double YDDLP2 = 0;//第二页的动力系数；

        double YH2LP2 = 0;
        double YL2LP2 = 0;
        double YS2LP2 = 0;
        double cosDS2 = 0;
        double sinDS2 = 0;
        double cosDH2 = 0;
        double sinDH2 = 0;
        double NS2 = 0;//合股标准值
        double NS2F = 0;// 分股标准值
        double NS2S = 0;//合股设计值

        double YsgLP2 = 0;
        double FgLP2 = 0;// 破断力总和；
        double KLP2 = 8;//  安全系数；//这个要改
        double αLP2 = 0;//不均匀系数；
        double rFgLP2 = 0;//钢丝绳容许荷载；
        double rFhLP2 = 0;//卡环容许荷载；


        double YaLP2 = 0;
        double YbLP2 = 0;
        double Yd0LP2 = 0;
        double YtLP2 = 0;
        double YcLP2 = 0;
        double YfLP2 = 0;//一个f是承载力，两个ff是尺寸
        double YfcLP2 = 0;//这个是承压承载力

        double YeLP2 = 0;
        double YffLP2 = 0;
        double YgLP2 = 0;



        double YNLP2 = 0;//N合股设计值
        double YN2LP2 = 0; //N分股标准值
        double YN3LP2 = 0; //N合股标准值

        string GssLP2 = "";

        double YbeLP2 = 0;
        double Yσ1LP2 = 0;
        double Yb1LP2 = 0;
        double Yσ2LP2 = 0;
        double YτLP2 = 0;
        double YfvLP2 = 0;
        double YZLP2 = 0;
        double YσcLP2 = 0;

        double NFLP2S1 = 0;//表示竖向力
        double NVLP2S1 = 0;//表示水平力
        double NFLP2S2 = 0;//表示竖向力
        double NVLP2S2 = 0;//表示水平力



        double YσF1LP2S1 = 0;//表示竖向力引起的耳板底部正应力
        double YσF2LP2S1 = 0;//表示弯矩引起的的耳板底部正应力
        double Yτ1LP2S1 = 0;//表示水平力引起的耳板底部剪应力
        double YσFZLP2S1 = 0;//表示综合应力

        double YσF1LP2S2 = 0;//表示竖向力引起的耳板底部正应力
        double YσF2LP2S2 = 0;//表示弯矩引起的的耳板底部正应力
        double Yτ1LP2S2 = 0;//表示水平力引起的耳板底部剪应力
        double YσFZLP2S2 = 0;//表示综合应力


        double YBJLP2 = 0;//表示加劲肋

        string[] Z1LP2 = { "6.2", "7.7", "9.3", "11.0", "11.0", "12.5", "14.0", "15.5", "17.0", "18.5", "20.0", "21.5", "23.0", "24.5", "26.0", "28.0", "31.0", "34.0", "37.0", "40.0", "43.0", "46.0" };
        string[] Z2LP2 = { "8.7", "11.0", "13.0", "15.0", "17.5", "19.5", "21.5", "24.0", "26.0", "28.0", "30.0", "32.5", "34.5", "36.5", "39.0", "43.0", "47.5", "52.0", "56.0", "60.5", "65.0" };
        string[] Z3LP2 = { "11.0 ", "14.0", "16.5", "19.5", "22.0", "25.0", "27.5", "30.5", "33.0", "36.0", "38.5", "41.5", "44.0", "47.0", "50.0", "55.5", "61.0", "66.5", "72.0", "77.5", "83.0" };



        //这是tabpage3的变量，后续pag2+LP2，后续pag3+MQ1
        double YGMQ1 = 0;
        //double YHMQ1 = 0;
        //double YL1MQ1 = 0;
        //double YL2MQ1 = 0;
        double YsgMQ1 = 0;
        double FgMQ1 = 0;// 破断力总和；
        double KMQ1 = 8;//  安全系数；//这个要改
        double αMQ1 = 0;//不均匀系数；
        double rFgMQ1 = 0;//钢丝绳容许荷载；
        double rFhMQ1 = 0;//卡环容许荷载；

        double YDDMQ1 = 0;//第三页的动力系数；

        double YaMQ1 = 0;
        double YbMQ1 = 0;
        double Yd0MQ1 = 0;
        double YtMQ1 = 0;
        double YcMQ1 = 0;
        double YfMQ1 = 0;


        double YeMQ1 = 0;
        double YffMQ1 = 0;
        double YgMQ1 = 0;


        //double YSθMQ1 = 0;//表示角度的正弦
        double YNMQ1 = 0;//N合股设计值
        double YN2MQ1 = 0; //N分股标准值
        double YN3MQ1 = 0; //N合股标准值

        string GssMQ1 = "";

        double YbeMQ1 = 0;
        double Yσ1MQ1 = 0;
        double Yb1MQ1 = 0;
        double Yσ2MQ1 = 0;
        double YτMQ1 = 0;
        double YfvMQ1 = 0;
        double YZMQ1 = 0;
        double YfcMQ1 = 0;//这个是承压承载力
        double YσcMQ1 = 0;
        //double YCθMQ1 = 0;//表示角度的余弦
        double NFMQ1 = 0;//表示竖向力
        //double NVMQ1 = 0;//表示水平力
        double YσF1MQ1 = 0;//表示竖向力引起的耳板底部正应力
                           //double YσF2MQ1 = 0;//表示弯矩引起的的耳板底部正应力

        double YσFZMQ1 = 0;//表示综合应力
        double YBJMQ1 = 0;//表示加劲肋

        string[] Z1MQ1 = { "6.2", "7.7", "9.3", "11.0", "11.0", "12.5", "14.0", "15.5", "17.0", "18.5", "20.0", "21.5", "23.0", "24.5", "26.0", "28.0", "31.0", "34.0", "37.0", "40.0", "43.0", "46.0" };
        string[] Z2MQ1 = { "8.7", "11.0", "13.0", "15.0", "17.5", "19.5", "21.5", "24.0", "26.0", "28.0", "30.0", "32.5", "34.5", "36.5", "39.0", "43.0", "47.5", "52.0", "56.0", "60.5", "65.0" };
        string[] Z3MQ1 = { "11.0 ", "14.0", "16.5", "19.5", "22.0", "25.0", "27.5", "30.5", "33.0", "36.0", "38.5", "41.5", "44.0", "47.0", "50.0", "55.5", "61.0", "66.5", "72.0", "77.5", "83.0" };


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox14.Visible = false;
            pictureBox15.Visible = false;
            pictureBox13.Visible = false;
        }




        


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 这里传入

            if (tG.Text == "") { YG = 0; } else { YG = Convert.ToDouble(tG.Text); }
            if (tH.Text == "") { YH = 0; } else { YH = Convert.ToDouble(tH.Text); }
            if (tL1.Text == "") { YL1 = 0; } else { YL1 = Convert.ToDouble(tL1.Text); }
            if (tL2.Text == "") { YL2 = 0; } else { YL2 = Convert.ToDouble(tL2.Text); }
            //  if (tsg.Text == "") { Ysg = 0; } else { Ysg = Convert.ToDouble(tsg.Text); }
            //YL1 = Convert.ToDouble(tL1.Text);
            //YL2 = Convert.ToDouble(tL2.Text);
            //Ysg = Convert.ToDouble(tsg.Text);

            if (ta.Text == "") { Ya = 0; } else { Ya = Convert.ToDouble(ta.Text); }
            if (tb.Text == "") { Yb = 0; } else { Yb = Convert.ToDouble(tb.Text); }
            if (td0.Text == "") { Yd0 = 0; } else { Yd0 = Convert.ToDouble(td0.Text); }
            if (tt.Text == "") { Yt = 0; } else { Yt = Convert.ToDouble(tt.Text); }
            if (tc.Text == "") { Yc = 0; } else { Yc = Convert.ToDouble(tc.Text); }
            //Ya = Convert.ToDouble(ta.Text);
            //Yb = Convert.ToDouble(tb.Text);
            //Yd0 = Convert.ToDouble(td0.Text);
            //Yt =Convert.ToDouble(tt.Text);
            //Yc = Convert.ToDouble(tc.Text);

            if (te.Text == "") { Ye = 0; } else { Ye = Convert.ToDouble(te.Text); }
            if (tf.Text == "") { Yff = 0; } else { Yff = Convert.ToDouble(tf.Text); }
            if (ttg.Text == "") { Yg = 0; } else { Yg = Convert.ToDouble(ttg.Text); }
            //Ye= Convert.ToDouble(te.Text);
            //Yff = Convert.ToDouble(tf.Text);
            //Yg = Convert.ToDouble(ttg.Text);

            if (BoxDD.Text == "") { YDD = 0; }
            else
            {
                Regex rx = new Regex("^(([0-9]+.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*.[0-9]+)|([0-9]*[1-9][0-9]*))$");
                if (rx.IsMatch(BoxDD.Text))
                {
                    YDD = Convert.ToDouble(BoxDD.Text);
                }
                else
                {
                    YDD = 0;
                }
            }
            //钢丝绳拉力计算,钢丝绳选型用分股标准值，用于吊耳计算的是合股的设计值
            if (YG < 0) { YG = -1 * YG; }

            YSθ = YH / Math.Sqrt(YL1 / 2 * YL1 / 2 + YL2 / 2 * YL2 / 2 + YH * YH);
            //MessageBox.Show(YSθ.ToString());

            YN2 = YG * 10 / 4 * YDD / YSθ / 1;//分股标准值，动力系数1.3//动力系数变成了
            YN3 = YG * 10 / 4 * YDD / YSθ;//合股标准值，动力系数1.3//动力系数变了
            YN = YG * 10 / 4 * YDD * 1.5 / YSθ;//合股设计值，动力系数1.3//动力系数变了
            //记住下面还要有个判定条件，让动力系数为0时，结果出不来，是单独的else if




            //钢丝绳容许应力计算
            if (BOXS.Text == "" || BoxX.Text == "" || BoxZJ.Text == "" || BoxZJ.Text == "") { MessageBox.Show("请输全钢丝绳信息"); }
            else
            {
                switch (BoxX.Text)
                {
                    case "6x19":
                        α = 0.85;
                        break;

                    case "6x37":
                        α = 0.82;
                        break;

                    case "6x61":
                        α = 0.80;
                        break;
                }

                Gss = BOXS.Text + " " + BoxX.Text + " " + BoxZJ.Text;


                switch (Gss)
                {
                    //6x19 1400MPa列
                    case "1400MPa 6x19 6.2":
                        Fg = 20.0;
                        break;
                    case "1400MPa 6x19 7.7":
                        Fg = 31.3;
                        break;
                    case "1400MPa 6x19 9.3":
                        Fg = 45.1;
                        break;
                    case "1400MPa 6x19 11.0":
                        Fg = 61.3;
                        break;
                    case "1400MPa 6x19 12.5":
                        Fg = 80.1;
                        break;
                    case "1400MPa 6x19 14.0":
                        Fg = 101.0;
                        break;
                    case "1400MPa 6x19 15.5":
                        Fg = 125.0;
                        break;
                    case "1400MPa 6x19 17.0":
                        Fg = 151.5;
                        break;
                    case "1400MPa 6x19 18.5":
                        Fg = 180.0;
                        break;
                    case "1400MPa 6x19 20.0":
                        Fg = 211.5;
                        break;
                    case "1400MPa 6x19 21.5":
                        Fg = 245.5;
                        break;
                    case "1400MPa 6x19 23.0":
                        Fg = 281.5;
                        break;
                    case "1400MPa 6x19 24.5":
                        Fg = 320.5;
                        break;
                    case "1400MPa 6x19 26.0":
                        Fg = 362.0;
                        break;
                    case "1400MPa 6x19 28.0":
                        Fg = 405.5;
                        break;
                    case "1400MPa 6x19 31.0":
                        Fg = 501.0;
                        break;
                    case "1400MPa 6x19 34.0":
                        Fg = 606.0;
                        break;
                    case "1400MPa 6x19 37.0":
                        Fg = 721.5;
                        break;
                    case "1400MPa 6x19 40.0":
                        Fg = 846.5;
                        break;
                    case "1400MPa 6x19 43":
                        Fg = 982.0;
                        break;
                    case "1400MPa 6x19 46":
                        Fg = 1125.0;
                        break;


                    //6x19 1550MPa列
                    case "1550MPa 6x19 6.2":
                        Fg = 22.1;
                        break;
                    case "1550MPa 6x19 7.7":
                        Fg = 34.6;
                        break;
                    case "1550MPa 6x19 9.3":
                        Fg = 49.9;
                        break;
                    case "1550MPa 6x19 11.0":
                        Fg = 67.9;
                        break;
                    case "1550MPa 6x19 12.5":
                        Fg = 88.7;
                        break;
                    case "1550MPa 6x19 14.0":
                        Fg = 112.0;
                        break;
                    case "1550MPa 6x19 15.5":
                        Fg = 138.5;
                        break;
                    case "1550MPa 6x19 17.0":
                        Fg = 167.5;
                        break;
                    case "1550MPa 6x19 18.5":
                        Fg = 199.5;
                        break;
                    case "1550MPa 6x19 20.0":
                        Fg = 234.0;
                        break;
                    case "1550MPa 6x19 21.5":
                        Fg = 271.5;
                        break;
                    case "1550MPa 6x19 23.0":
                        Fg = 312.0;
                        break;
                    case "1550MPa 6x19 24.5":
                        Fg = 355.0;
                        break;
                    case "1550MPa 6x19 26.0":
                        Fg = 400.5;
                        break;
                    case "1550MPa 6x19 28.0":
                        Fg = 449.0;
                        break;
                    case "1550MPa 6x19 31.0":
                        Fg = 554.5;
                        break;
                    case "1550MPa 6x19 34.0":
                        Fg = 671.0;
                        break;
                    case "1550MPa 6x19 37.0":
                        Fg = 798.5;
                        break;
                    case "1550MPa 6x19 40.0":
                        Fg = 937.5;
                        break;
                    case "1550MPa 6x19 43":
                        Fg = 1085.0;
                        break;
                    case "1550MPa 6x19 46":
                        Fg = 1245.0;
                        break;


                    //6x19 1700MPa列
                    case "1700MPa 6x19 6.2":
                        Fg = 24.3;
                        break;
                    case "1700MPa 6x19 7.7":
                        Fg = 38.0;
                        break;
                    case "1700MPa 6x19 9.3":
                        Fg = 54.7;
                        break;
                    case "1700MPa 6x19 11.0":
                        Fg = 74.5;
                        break;
                    case "1700MPa 6x19 12.5":
                        Fg = 97.3;
                        break;
                    case "1700MPa 6x19 14.0":
                        Fg = 123.0;
                        break;
                    case "1700MPa 6x19 15.5":
                        Fg = 152.0;
                        break;
                    case "1700MPa 6x19 17.0":
                        Fg = 184.0;
                        break;
                    case "1700MPa 6x19 18.5":
                        Fg = 219.0;
                        break;
                    case "1700MPa 6x19 20.0":
                        Fg = 257.0;
                        break;
                    case "1700MPa 6x19 21.5":
                        Fg = 298.0;
                        break;
                    case "1700MPa 6x19 23.0":
                        Fg = 342.0;
                        break;
                    case "1700MPa 6x19 24.5":
                        Fg = 389.0;
                        break;
                    case "1700MPa 6x19 26.0":
                        Fg = 439.0;
                        break;
                    case "1700MPa 6x19 28.0":
                        Fg = 492.5;
                        break;
                    case "1700MPa 6x19 31.0":
                        Fg = 608.5;
                        break;
                    case "1700MPa 6x19 34.0":
                        Fg = 736.0;
                        break;
                    case "1700MPa 6x19 37.0":
                        Fg = 876.0;
                        break;
                    case "1700MPa 6x19 40.0":
                        Fg = 1025.0;
                        break;
                    case "1700MPa 6x19 43":
                        Fg = 1190.0;
                        break;
                    case "1700MPa 6x19 46":
                        Fg = 1365.0;
                        break;

                    //6x19 1850MPa列
                    case "1850MPa 6x19 6.2":
                        Fg = 26.3;
                        break;
                    case "1850MPa 6x19 7.7":
                        Fg = 41.3;
                        break;
                    case "1850MPa 6x19 9.3":
                        Fg = 59.6;
                        break;
                    case "1850MPa 6x19 11.0":
                        Fg = 81.1;
                        break;
                    case "1850MPa 6x19 12.5":
                        Fg = 105.5;
                        break;
                    case "1850MPa 6x19 14.0":
                        Fg = 134.0;
                        break;
                    case "1850MPa 6x19 15.5":
                        Fg = 165.5;
                        break;
                    case "1850MPa 6x19 17.0":
                        Fg = 200.0;
                        break;
                    case "1850MPa 6x19 18.5":
                        Fg = 238.0;
                        break;
                    case "1850MPa 6x19 20.0":
                        Fg = 279.5;
                        break;
                    case "1850MPa 6x19 21.5":
                        Fg = 324.5;
                        break;
                    case "1850MPa 6x19 23.0":
                        Fg = 372.5;
                        break;
                    case "1850MPa 6x19 24.5":
                        Fg = 423.5;
                        break;
                    case "1850MPa 6x19 26.0":
                        Fg = 478.0;
                        break;
                    case "1850MPa 6x19 28.0":
                        Fg = 536.0;
                        break;
                    case "1850MPa 6x19 31.0":
                        Fg = 662.0;
                        break;
                    case "1850MPa 6x19 34.0":
                        Fg = 801.0;
                        break;
                    case "1850MPa 6x19 37.0":
                        Fg = 953.5;
                        break;
                    case "1850MPa 6x19 40.0":
                        Fg = 1115.0;
                        break;
                    case "1850MPa 6x19 43":
                        Fg = 1295.0;
                        break;
                    case "1850MPa 6x19 46":
                        Fg = 1490.0;
                        break;

                    //6x37 1400MPa列
                    case "1400MPa 6x37 8.7":
                        Fg = 39.0;
                        break;
                    case "1400MPa 6x37 11.0":
                        Fg = 60.9;
                        break;
                    case "1400MPa 6x37 13.0":
                        Fg = 87.8;
                        break;
                    case "1400MPa 6x37 15.0":
                        Fg = 119.5;
                        break;
                    case "1400MPa 6x37 17.5":
                        Fg = 156.0;
                        break;
                    case "1400MPa 6x37 19.5":
                        Fg = 197.5;
                        break;
                    case "1400MPa 6x37 21.5":
                        Fg = 243.5;
                        break;
                    case "1400MPa 6x37 24.0":
                        Fg = 295.0;
                        break;
                    case "1400MPa 6x37 26.0":
                        Fg = 351.0;
                        break;
                    case "1400MPa 6x37 28.0":
                        Fg = 412.0;
                        break;
                    case "1400MPa 6x37 30.0":
                        Fg = 478.0;
                        break;
                    case "1400MPa 6x37 32.5":
                        Fg = 522.5;
                        break;
                    case "1400MPa 6x37 34.5":
                        Fg = 624.5;
                        break;
                    case "1400MPa 6x37 36.5":
                        Fg = 705.0;
                        break;
                    case "1400MPa 6x37 39.0":
                        Fg = 790.0;
                        break;
                    case "1400MPa 6x37 43.0":
                        Fg = 975.5;
                        break;
                    case "1400MPa 6x37 47.5":
                        Fg = 1180.0;
                        break;
                    case "1400MPa 6x37 52.0":
                        Fg = 1450.0;
                        break;
                    case "1400MPa 6x37 56.0":
                        Fg = 1645.0;
                        break;
                    case "1400MPa 6x37 60.5":
                        Fg = 1910.0;
                        break;
                    case "1400MPa 6x37 65.0":
                        Fg = 2195.0;
                        break;


                    //6x37 1550MPa列
                    case "1550MPa 6x37 8.7":
                        Fg = 43.2;
                        break;
                    case "1550MPa 6x37 11.0":
                        Fg = 67.5;
                        break;
                    case "1550MPa 6x37 13.0":
                        Fg = 97.2;
                        break;
                    case "1550MPa 6x37 15.0":
                        Fg = 132.0;
                        break;
                    case "1550MPa 6x37 17.5":
                        Fg = 172.5;
                        break;
                    case "1550MPa 6x37 19.5":
                        Fg = 213.5;
                        break;
                    case "1550MPa 6x37 21.5":
                        Fg = 270.0;
                        break;
                    case "1550MPa 6x37 24.0":
                        Fg = 326.5;
                        break;
                    case "1550MPa 6x37 26.0":
                        Fg = 388.5;
                        break;
                    case "1550MPa 6x37 28.0":
                        Fg = 456.5;
                        break;
                    case "1550MPa 6x37 30.0":
                        Fg = 529.0;
                        break;
                    case "1550MPa 6x37 32.5":
                        Fg = 607.5;
                        break;
                    case "1550MPa 6x37 34.5":
                        Fg = 691.5;
                        break;
                    case "1550MPa 6x37 36.5":
                        Fg = 780.5;
                        break;
                    case "1550MPa 6x37 39.0":
                        Fg = 875.0;
                        break;
                    case "1550MPa 6x37 43.0":
                        Fg = 1080.0;
                        break;
                    case "1550MPa 6x37 47.5":
                        Fg = 1305.0;
                        break;
                    case "1550MPa 6x37 52.0":
                        Fg = 1555.0;
                        break;
                    case "1550MPa 6x37 56.0":
                        Fg = 1825.0;
                        break;
                    case "1550MPa 6x37 60.5":
                        Fg = 2115.0;
                        break;
                    case "1550MPa 6x37 65.0":
                        Fg = 2430.0;
                        break;


                    //6x37 1700MPa列
                    case "1700MPa 6x37 8.7":
                        Fg = 47.3;
                        break;
                    case "1700MPa 6x37 11.0":
                        Fg = 74.0;
                        break;
                    case "1700MPa 6x37 13.0":
                        Fg = 106.5;
                        break;
                    case "1700MPa 6x37 15.0":
                        Fg = 145.0;
                        break;
                    case "1700MPa 6x37 17.5":
                        Fg = 189.5;
                        break;
                    case "1700MPa 6x37 19.5":
                        Fg = 239.5;
                        break;
                    case "1700MPa 6x37 21.5":
                        Fg = 296.0;
                        break;
                    case "1700MPa 6x37 24.0":
                        Fg = 358.0;
                        break;
                    case "1700MPa 6x37 26.0":
                        Fg = 426.5;
                        break;
                    case "1700MPa 6x37 28.0":
                        Fg = 500.5;
                        break;
                    case "1700MPa 6x37 30.0":
                        Fg = 580.5;
                        break;
                    case "1700MPa 6x37 32.5":
                        Fg = 666.5;
                        break;
                    case "1700MPa 6x37 34.5":
                        Fg = 758.5;
                        break;
                    case "1700MPa 6x37 36.5":
                        Fg = 856.0;
                        break;
                    case "1700MPa 6x37 39.0":
                        Fg = 959.5;
                        break;
                    case "1700MPa 6x37 43.0":
                        Fg = 1185.0;
                        break;
                    case "1700MPa 6x37 47.5":
                        Fg = 1430.0;
                        break;
                    case "1700MPa 6x37 52.0":
                        Fg = 1705.0;
                        break;
                    case "1700MPa 6x37 56.0":
                        Fg = 2000.0;
                        break;
                    case "1700MPa 6x37 60.5":
                        Fg = 2320.0;
                        break;
                    case "1700MPa 6x37 65.0":
                        Fg = 2665.0;
                        break;



                    //6x37 1850MPa列
                    case "1850MPa 6x37 8.7":
                        Fg = 51.5;
                        break;
                    case "1850MPa 6x37 11.0":
                        Fg = 80.6;
                        break;
                    case "1850MPa 6x37 13.0":
                        Fg = 116.5;
                        break;
                    case "1850MPa 6x37 15.0":
                        Fg = 157.5;
                        break;
                    case "1850MPa 6x37 17.5":
                        Fg = 206.0;
                        break;
                    case "1850MPa 6x37 19.5":
                        Fg = 261.0;
                        break;
                    case "1850MPa 6x37 21.5":
                        Fg = 322.0;
                        break;
                    case "1850MPa 6x37 24.0":
                        Fg = 390.0;
                        break;
                    case "1850MPa 6x37 26.0":
                        Fg = 464.0;
                        break;
                    case "1850MPa 6x37 28.0":
                        Fg = 544.5;
                        break;
                    case "1850MPa 6x37 30.0":
                        Fg = 631.5;
                        break;
                    case "1850MPa 6x37 32.5":
                        Fg = 725.0;
                        break;
                    case "1850MPa 6x37 34.5":
                        Fg = 758.0;
                        break;
                    case "1850MPa 6x37 36.5":
                        Fg = 856.0;
                        break;
                    case "1850MPa 6x37 39.0":
                        Fg = 959.5;
                        break;
                    case "1850MPa 6x37 43.0":
                        Fg = 1185.0;
                        break;
                    case "1850MPa 6x37 47.5":
                        Fg = 1430.0;
                        break;
                    case "1850MPa 6x37 52.0":
                        Fg = 1705.0;
                        break;
                    case "1850MPa 6x37 56.0":
                        Fg = 2000.0;
                        break;
                    case "1850MPa 6x37 60.5":
                        Fg = 2320.0;
                        break;
                    case "1850MPa 6x37 65.0":
                        Fg = 2665.0;
                        break;



                    //6x61 1400MPa列
                    case "1400MPa 6x61 11.0":
                        Fg = 64.3;
                        break;
                    case "1400MPa 6x61 14.0":
                        Fg = 100.5;
                        break;
                    case "1400MPa 6x61 16.5":
                        Fg = 144.5;
                        break;
                    case "1400MPa 6x61 19.5":
                        Fg = 197.0;
                        break;
                    case "1400MPa 6x61 22.0":
                        Fg = 257.0;
                        break;
                    case "1400MPa 6x61 25.0":
                        Fg = 325.5;
                        break;
                    case "1400MPa 6x61 27.5":
                        Fg = 402.0;
                        break;
                    case "1400MPa 6x61 30.5":
                        Fg = 226.5;
                        break;
                    case "1400MPa 6x61 33.0":
                        Fg = 579.0;
                        break;
                    case "1400MPa 6x61 36.0":
                        Fg = 679.5;
                        break;
                    case "1400MPa 6x61 38.5":
                        Fg = 788.0;
                        break;
                    case "1400MPa 6x61 41.5":
                        Fg = 905.0;
                        break;
                    case "1400MPa 6x61 44.0":
                        Fg = 1025.0;
                        break;
                    case "1400MPa 6x61 47.0":
                        Fg = 1160.0;
                        break;
                    case "1400MPa 6x61 50.0":
                        Fg = 1300.0;
                        break;
                    case "1400MPa 6x61 55.5":
                        Fg = 1605.0;
                        break;
                    case "1400MPa 6x61 61.0":
                        Fg = 1945.0;
                        break;
                    case "1400MPa 6x61 66.5":
                        Fg = 2315.0;
                        break;
                    case "1400MPa 6x61 72.0":
                        Fg = 2715.5;
                        break;
                    case "1400MPa 6x61 77.5":
                        Fg = 3150.0;
                        break;
                    case "1400MPa 6x61 83.0":
                        Fg = 3620.0;
                        break;



                    //6x61 1550MPa列
                    case "1550MPa 6x61 11.0":
                        Fg = 71.2;
                        break;
                    case "1550MPa 6x61 14.0":
                        Fg = 111.0;
                        break;
                    case "1550MPa 6x61 16.5":
                        Fg = 160.0;
                        break;
                    case "1550MPa 6x61 19.5":
                        Fg = 218.0;
                        break;
                    case "1550MPa 6x61 22.0":
                        Fg = 285.0;
                        break;
                    case "1550MPa 6x61 25.0":
                        Fg = 360.5;
                        break;
                    case "1550MPa 6x61 27.5":
                        Fg = 445.0;
                        break;
                    case "1550MPa 6x61 30.5":
                        Fg = 538.5;
                        break;
                    case "1550MPa 6x61 33.0":
                        Fg = 641.0;
                        break;
                    case "1550MPa 6x61 36.0":
                        Fg = 752.5;
                        break;
                    case "1550MPa 6x61 38.5":
                        Fg = 872.5;
                        break;
                    case "1550MPa 6x61 41.5":
                        Fg = 1000.0;
                        break;
                    case "1550MPa 6x61 44.0":
                        Fg = 1140.0;
                        break;
                    case "1550MPa 6x61 47.0":
                        Fg = 1285.0;
                        break;
                    case "1550MPa 6x61 50.0":
                        Fg = 1440.0;
                        break;
                    case "1550MPa 6x61 55.5":
                        Fg = 1780.0;
                        break;
                    case "1550MPa 6x61 61.0":
                        Fg = 2155.0;
                        break;
                    case "1550MPa 6x61 66.5":
                        Fg = 2565.0;
                        break;
                    case "1550MPa 6x61 72.0":
                        Fg = 3010.0;
                        break;
                    case "1550MPa 6x61 77.5":
                        Fg = 3490.0;
                        break;
                    case "1550MPa 6x61 83.0":
                        Fg = 4005.0;
                        break;


                    //6x61 1700MPa列
                    case "1700MPa 6x61 11.0":
                        Fg = 78.1;
                        break;
                    case "1700MPa 6x61 14.0":
                        Fg = 122.0;
                        break;
                    case "1700MPa 6x61 16.5":
                        Fg = 175.5;
                        break;
                    case "1700MPa 6x61 19.5":
                        Fg = 239.0;
                        break;
                    case "1700MPa 6x61 22.0":
                        Fg = 312.5;
                        break;
                    case "1700MPa 6x61 25.0":
                        Fg = 395.5;
                        break;
                    case "1700MPa 6x61 27.5":
                        Fg = 228.0;
                        break;
                    case "1700MPa 6x61 30.5":
                        Fg = 591.0;
                        break;
                    case "1700MPa 6x61 33.0":
                        Fg = 703.0;
                        break;
                    case "1700MPa 6x61 36.0":
                        Fg = 825.0;
                        break;
                    case "1700MPa 6x61 38.5":
                        Fg = 957.0;
                        break;
                    case "1700MPa 6x61 41.5":
                        Fg = 1095.0;
                        break;
                    case "1700MPa 6x61 44.0":
                        Fg = 1250.0;
                        break;
                    case "1700MPa 6x61 47.0":
                        Fg = 1410.0;
                        break;
                    case "1700MPa 6x61 50.0":
                        Fg = 1580.0;
                        break;
                    case "1700MPa 6x61 55.5":
                        Fg = 1950.0;
                        break;
                    case "1700MPa 6x61 61.0":
                        Fg = 2360.0;
                        break;
                    case "1700MPa 6x61 66.5":
                        Fg = 2810.0;
                        break;
                    case "1700MPa 6x61 72.0":
                        Fg = 3300.0;
                        break;
                    case "1700MPa 6x61 77.5":
                        Fg = 3825.0;
                        break;
                    case "1700MPa 6x61 83.0":
                        Fg = 4395.0;
                        break;



                    //6x61 1850MPa列
                    case "1850MPa 6x61 11.0":
                        Fg = 85.0;
                        break;
                    case "1850MPa 6x61 14.0":
                        Fg = 132.0;
                        break;
                    case "1850MPa 6x61 16.5":
                        Fg = 191.0;
                        break;
                    case "1850MPa 6x61 19.5":
                        Fg = 260.0;
                        break;
                    case "1850MPa 6x61 22.0":
                        Fg = 340.0;
                        break;
                    case "1850MPa 6x61 25.0":
                        Fg = 430.5;
                        break;
                    case "1850MPa 6x61 27.5":
                        Fg = 531.5;
                        break;
                    case "1850MPa 6x61 30.5":
                        Fg = 643.0;
                        break;
                    case "1850MPa 6x61 33.0":
                        Fg = 765.0;
                        break;
                    case "1850MPa 6x61 36.0":
                        Fg = 898.0;
                        break;
                    case "1850MPa 6x61 38.5":
                        Fg = 1040.0;
                        break;
                    case "1850MPa 6x61 41.5":
                        Fg = 1195.0;
                        break;
                    case "1850MPa 6x61 44.0":
                        Fg = 1360.0;
                        break;
                    case "1850MPa 6x61 47.0":
                        Fg = 1535.0;
                        break;
                    case "1850MPa 6x61 50.0":
                        Fg = 1720.0;
                        break;
                    case "1850MPa 6x61 55.5":
                        Fg = 2125.0;
                        break;
                    case "1850MPa 6x61 61.0":
                        Fg = 2570.0;
                        break;
                    case "1850MPa 6x61 66.5":
                        Fg = 3060.0;
                        break;
                    case "1850MPa 6x61 72.0":
                        Fg = 3590.0;
                        break;
                    case "1850MPa 6x61 77.5":
                        Fg = 4165.0;
                        break;
                    case "1850MPa 6x61 83.0":
                        Fg = 4780.0;
                        break;

                }
                if (BoxQX1.Text == "") { K = 0; }
                else
                {


                    K = Convert.ToDouble(BoxQX1.Text);



                }





                //这是卡环的承载力
                if (BoxKH.Text == "") { MessageBox.Show("请输入卡环型号"); }
                else
                {
                    switch (BoxKH.Text)
                    {
                        case "0.2":
                            rFh = 2.45;
                            break;
                        case "0.4":
                            rFh = 3.92;
                            break;
                        case "0.6":
                            rFh = 5.88;
                            break;
                        case "0.9":
                            rFh = 8.82;
                            break;
                        case "1.2":
                            rFh = 12.25;
                            break;
                        case "1.7":
                            rFh = 17.15;
                            break;
                        case "2.1":
                            rFh = 20.58;
                            break;
                        case "2.7":
                            rFh = 26.95;
                            break;
                        case "3.5":
                            rFh = 34.30;
                            break;
                        case "4.5":
                            rFh = 44.10;
                            break;
                        case "6.0":
                            rFh = 58.80;
                            break;
                        case "7.5":
                            rFh = 73.50;
                            break;
                        case "9.5":
                            rFh = 93.10;
                            break;
                        case "11.0":
                            rFh = 107.80;
                            break;
                        case "14.0":
                            rFh = 137.20;
                            break;
                        case "17.5":
                            rFh = 171.50;
                            break;
                        case "21.0":
                            rFh = 205.80;
                            break;
                    }





                    switch (BoxB.Text)
                    {
                        case "Q235B":
                            if (0 < Yt && Yt <= 16)
                            { Yf = 215; Yfv = 125; Yfc = 405; }
                            else if (Yt > 16 && Yt <= 40)
                            { Yf = 205; Yfv = 120; Yfc = 405; }
                            else if (Yt > 40 && Yt <= 100)
                            { Yf = 200; Yfv = 115; Yfc = 405; }
                            else if (Yt < 0 || Yt > 100)
                            { Yf = 1; Yfv = 1; Yfc = 405; }
                            break;
                        case "Q355B":
                            if (0 < Yt && Yt <= 16)
                            { Yf = 305; Yfv = 175; Yfc = 510; }
                            else if (Yt > 16 && Yt <= 40)
                            { Yf = 295; Yfv = 170; Yfc = 510; }
                            else if (Yt > 40 && Yt <= 63)
                            { Yf = 290; Yfv = 165; Yfc = 510; }
                            else if (Yt > 63 && Yt <= 80)
                            { Yf = 280; Yfv = 160; Yfc = 510; }
                            else if (Yt > 80 && Yt <= 100)
                            { Yf = 270; Yfv = 155; Yfc = 510; }
                            else if (Yt < 0 || Yt > 100)
                            { Yf = 1; Yfv = 1; Yfc = 510; }
                            break;
                        case "Q390B":
                            if (0 < Yt && Yt <= 16)
                            { Yf = 345; Yfv = 200; Yfc = 530; }
                            else if (Yt > 16 && Yt <= 40)
                            { Yf = 330; Yfv = 190; Yfc = 530; }
                            else if (Yt > 40 && Yt <= 63)
                            { Yf = 310; Yfv = 180; Yfc = 530; }
                            else if (Yt > 63 && Yt <= 100)
                            { Yf = 295; Yfv = 170; Yfc = 530; }
                            else if (Yt <= 0 || Yt > 100)
                            { Yf = 1; Yfv = 1; }
                            break;
                        case "Q420B":
                            if (0 < Yt && Yt <= 16)
                            { Yf = 375; Yfv = 215; Yfc = 560; }
                            else if (Yt > 16 && Yt <= 40)
                            { Yf = 355; Yfv = 205; Yfc = 560; }
                            else if (Yt > 40 && Yt <= 63)
                            { Yf = 320; Yfv = 185; Yfc = 560; }
                            else if (Yt > 63 && Yt <= 100)
                            { Yf = 305; Yfv = 175; Yfc = 560; }
                            else if (Yt <= 0 || Yt > 100)
                            { Yf = 1; Yfv = 1; }
                            break;
                    }
                    Ybe = 2 * Yt + 16;
                    //MessageBox.Show(Ybe.ToString());
                    //MessageBox.Show(Ya.ToString());
                    Yb1 = Math.Min(2 * Yt + 16, Yb - Yd0 / 3);
                    if (BoxB.Text == "")
                    { MessageBox.Show("请选择耳板材质"); GSSLL.Text = "暂时不知道"; GSSRXLL.Text = "暂时不知道"; KHFH.Text = "暂时不知道"; GSSLLs.Text = "暂时不知道"; GZ.Text = "暂时不知道"; JKL.Text = "暂时不知道"; PK.Text = "暂时不知道"; KJ.Text = "暂时不知道"; FH.Text = "暂时不知道"; DJ.Text = "暂时不知道"; }
                    else if (Yf == 1)
                    { MessageBox.Show("厚度t一般大于0mm且小于100mm,重新输入看看吧"); GSSLL.Text = "暂时不知道"; GSSRXLL.Text = "暂时不知道"; KHFH.Text = "暂时不知道"; GSSLLs.Text = "暂时不知道"; GZ.Text = "暂时不知道"; JKL.Text = "暂时不知道"; PK.Text = "暂时不知道"; KJ.Text = "暂时不知道"; FH.Text = "暂时不知道"; DJ.Text = "暂时不知道"; }
                    else if (YDD == 0)
                    { MessageBox.Show("动力系数输入有误"); GSSLL.Text = "暂时不知道"; GSSRXLL.Text = "暂时不知道"; KHFH.Text = "暂时不知道"; GSSLLs.Text = "暂时不知道"; GZ.Text = "暂时不知道"; JKL.Text = "暂时不知道"; PK.Text = "暂时不知道"; KJ.Text = "暂时不知道"; FH.Text = "暂时不知道"; DJ.Text = "暂时不知道"; }
                    else if (K <= 0)
                    { MessageBox.Show("安全系数输入有误"); GSSLL.Text = "暂时不知道"; GSSRXLL.Text = "暂时不知道"; KHFH.Text = "暂时不知道"; GSSLLs.Text = "暂时不知道"; GZ.Text = "暂时不知道"; JKL.Text = "暂时不知道"; PK.Text = "暂时不知道"; KJ.Text = "暂时不知道"; FH.Text = "暂时不知道"; DJ.Text = "暂时不知道"; }


                    else if (YG <= 0 || YH <= 0 || YL1 <= 0 || YL2 <= 0 || Ya <= 0 || Yb <= 0 || Yd0 <= 0 || Yc <= 0 || Ye <= 0 || Yff <= 0 || Yg <= 0 || YDD == 0)
                    { MessageBox.Show("实际构件尺寸一般为正值,实际重量一般不等于0"); GSSLL.Text = "暂时不知道"; GSSRXLL.Text = "暂时不知道"; KHFH.Text = "暂时不知道"; GSSLLs.Text = "暂时不知道"; GZ.Text = "暂时不知道"; JKL.Text = "暂时不知道"; PK.Text = "暂时不知道"; KJ.Text = "暂时不知道"; FH.Text = "暂时不知道"; DJ.Text = "暂时不知道"; }
                    else if (YL1 / 2 / YH >= 0.70710678118654752440084436210225)
                    { MessageBox.Show("吊索与所吊构件的水平夹角宜大于45°"); GSSLL.Text = "暂时不知道"; GSSRXLL.Text = "暂时不知道"; KHFH.Text = "暂时不知道"; GSSLLs.Text = "暂时不知道"; GZ.Text = "暂时不知道"; JKL.Text = "暂时不知道"; PK.Text = "暂时不知道"; KJ.Text = "暂时不知道"; FH.Text = "暂时不知道"; DJ.Text = "暂时不知道"; }
                    else
                    {
                        rFg = α * Fg / K;
                        GSSLL.Text = YN2.ToString("#.##");//分股标准值
                        GSSLLs.Text = YN.ToString("#.##");//合股设计值
                        //那两个放进来
                        if (rFg < YN2)
                        { GSSRXLL.Text = rFg.ToString("#.##") + "，" + "钢丝绳不满足要求"; GSSRXLL.ForeColor = Color.Red; }
                        else
                        { GSSRXLL.Text = rFg.ToString("#.##") + "，" + "钢丝绳满足要求"; GSSRXLL.ForeColor = Color.Black; }
                        if (rFh < YN3)
                        { KHFH.Text = rFh.ToString("#.##") + "<" + YN2.ToString("#.##") + "，" + "卡环不满足要求"; KHFH.ForeColor = Color.Red; }//判定语句要重写
                        else
                        { KHFH.Text = rFh.ToString("#.##") + "，" + "卡环满足要求"; KHFH.ForeColor = Color.Black; }



                        if (Ya < (Ybe / 3 * 4)) { GZ.Text = "a" + ":" + Ya.ToString() + "< " + "4/3be" + ":" + (Ybe * 4 / 3).ToString("#.##") + "," + "构造不满足"; GZ.ForeColor = Color.Red; }
                        else if (Yb < Ybe) { GZ.Text = "b" + ":" + Yb.ToString() + "< " + "be" + ":" + (Ybe).ToString("#.##") + "," + "构造不满足"; GZ.ForeColor = Color.Red; }
                        else { GZ.Text = "构造满足"; GZ.ForeColor = Color.Black; }
                        Yσ1 = YN * 1000 / (2 * Yt * Yb1);
                        Yσ2 = YN * 1000 / 2 / Yt / (Ya - 2 * Yd0 / 3);
                        Yσc = YN * 1000 / Yt / (Yd0 - 1);
                        YZ = Math.Sqrt((Ya + Yd0 / 2) * (Ya + Yd0 / 2) - (Yd0 / 2) * (Yd0 / 2));
                        // MessageBox.Show(YZ.ToString());
                        Yτ = YN * 1000 / 2 / Yt / YZ;//这个改过了

                        //这一段是吊耳板《钢结构设计标准》的计算内容

                        if (Yσ1 < Yf)
                        { JKL.Text = "σ" + ":" + Yσ1.ToString("#.##") + "<" + "f" + ":" + Yf.ToString() + "," + "满足"; JKL.ForeColor = Color.Black; }
                        else
                        { JKL.Text = "σ" + ":" + Yσ1.ToString("#.##") + ">" + "f" + ":" + Yf.ToString() + "," + "不满足"; JKL.ForeColor = Color.Red; }

                        if (Yσ2 < Yf)
                        { PK.Text = "σ" + ":" + Yσ2.ToString("#.##") + "<" + "f" + ":" + Yf.ToString() + "," + "满足"; PK.ForeColor = Color.Black; }
                        else
                        { PK.Text = "σ" + ":" + Yσ2.ToString("#.##") + ">" + "f" + ":" + Yf.ToString() + "," + "不满足"; PK.ForeColor = Color.Red; }

                        if (Yτ < Yfv)
                        { KJ.Text = "τ" + ":" + Yτ.ToString("#.##") + "<" + "fv" + ":" + Yfv.ToString() + "," + "满足"; KJ.ForeColor = Color.Black; }
                        else
                        { KJ.Text = "τ" + ":" + Yτ.ToString("#.##") + ">" + "fv" + ":" + Yfv.ToString() + "," + "不满足"; KJ.ForeColor = Color.Red; }

                        if (Yσc < Yfc)
                        { CY.Text = "σ" + ":" + Yσc.ToString("#.##") + "<" + "f" + ":" + Yfc.ToString() + "," + "满足"; CY.ForeColor = Color.Black; }
                        else
                        { CY.Text = "σ" + ":" + Yσc.ToString("#.##") + ">" + "f" + ":" + Yfc.ToString() + "," + "不满足"; CY.ForeColor = Color.Red; }

                        //这一段是吊耳板《钢结构设计标准》底部受力及加劲板是否干涉的计算
                        YBJ = Math.Sqrt((Yff - 6) * (Yff - 6) + (Math.Abs(Yc - Yg) + Yd0 / 2) * (Math.Abs(Yc - Yg) + Yd0 / 2)) - Yd0 / 2;
                        if ((2 * Ye + 2 * Yff) < (2 * Yb + Yd0)) { MessageBox.Show("底部一般不小于顶部宽度，可以增加e，f值"); }
                        else if (Yc < Yg || Yc < Yb) { MessageBox.Show("底部补长a一般比加劲肋边长g高，且不小于双侧边缘净距b"); }
                        else if ((YBJ) < 50) { MessageBox.Show("加劲肋距离孔边有点近，可以增加e，f值"); }
                        else
                        {
                            YCθ = Math.Sqrt(1 - YSθ * YSθ);//表示角度的余弦
                            NF = YN * YSθ;
                            NV = YN * YCθ;//表示水平力
                            YσF1 = NF * 1000 / Yt / (2 * Ye + 2 * Yff);//表示竖向力引起的耳板底部正应力
                                                                       // MessageBox.Show(YσF1.ToString());
                            YσF2 = NV * 1000 * (Yd0 / 2 + Yc) / Yt / (2 * Ye + 2 * Yff) / (2 * Ye + 2 * Yff) * 6;//表示弯矩引起的的耳板底部正应力
                                                                                                                 // MessageBox.Show(YσF2.ToString());
                            Yτ1 = NV * 1000 / Yt / (2 * Ye + 2 * Yff);//表示水平力引起的耳板底部剪应力
                            //MessageBox.Show(Yτ1.ToString());
                            YσFZ = Math.Sqrt((YσF1 + YσF2) * (YσF1 + YσF2) + 3 * Yτ1 * Yτ1);
                            // MessageBox.Show(YσFZ.ToString());
                            if (YσFZ < (1.1 * Yf))
                            { FH.Text = "σ" + ":" + YσFZ.ToString("#.##") + "<" + "1.1f" + ":" + (1.1 * Yf).ToString("#.#") + "," + "满足"; FH.ForeColor = Color.Black; }
                            else
                            { FH.Text = "σ" + ":" + YσFZ.ToString("#.##") + ">" + "1.1f" + ":" + (1.1 * Yf).ToString("#.#") + "," + "不满足"; FH.ForeColor = Color.Red; }

                            if (YσF1 < Yf)
                            { DJ.Text = "σ" + ":" + YσF1.ToString("#.##") + "<" + "f" + ":" + Yf.ToString() + "," + "满足"; DJ.ForeColor = Color.Black; }
                            else
                            { DJ.Text = "σ" + ":" + YσF1.ToString("#.##") + ">" + "f" + ":" + Yf.ToString() + "," + "不满足"; DJ.ForeColor = Color.Red; }

                        }


                    }

                }






            }
        }
        private void tL1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }



        private void BoxX_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox14.Visible = false;

            BoxZJ.Items.Clear();
            if (BoxX.SelectedIndex == 0)
            {
                BoxZJ.Items.Clear();
                BoxZJ.Items.AddRange(Z1);
            }
            else if (BoxX.SelectedIndex == 1)
            {
                BoxZJ.Items.Clear();
                BoxZJ.Items.AddRange(Z2);
            }
            else if (BoxX.SelectedIndex == 2)
            {
                BoxZJ.Items.Clear();
                BoxZJ.Items.AddRange(Z3);
            }
        }

        private void BoxXMQ1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox15.Visible = false;
            BoxZJMQ1.Items.Clear();
            if (BoxXMQ1.SelectedIndex == 0)
            {
                BoxZJMQ1.Items.Clear();
                BoxZJMQ1.Items.AddRange(Z1MQ1);
            }
            else if (BoxXMQ1.SelectedIndex == 1)
            {
                BoxZJMQ1.Items.Clear();
                BoxZJMQ1.Items.AddRange(Z2MQ1);
            }
            else if (BoxXMQ1.SelectedIndex == 2)
            {
                BoxZJMQ1.Items.Clear();
                BoxZJMQ1.Items.AddRange(Z3MQ1);
            }
        }

        //第一框
        private void tL1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

 
        private void BoxQX1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void tH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
         }

        private void tG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        //第二框
        private void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void ta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void td0_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void tc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void te_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void tf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void ttg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void tt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void BoxQX1LP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void tL2LP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void tL1LP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void tS2LP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void tS1LP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void tGLP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void tbLP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void taLP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void td0LP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void tcLP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void teLP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void tfLP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void ttgLP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void ttLP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void BoxQX1MQ1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void tGMQ1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void tbMQ1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void taMQ1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void td0MQ1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void tcMQ1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void teMQ1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void tfMQ1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void ttgMQ1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void ttMQ1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            { e.Handled = true; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //添加的输入要考虑后面的条件判断语句
            if (tS1LP2.Text == "") { YS1LP2 = 0; } else { YS1LP2 = Convert.ToDouble(tS1LP2.Text); }//
            if (tS2LP2.Text == "") { YS2LP2 = 0; } else { YS2LP2 = Convert.ToDouble(tS2LP2.Text); }//

            if (tGLP2.Text == "") { YGLP2 = 0; } else { YGLP2 = Convert.ToDouble(tGLP2.Text); }//
                                                                                               // if (tHLP2.Text == "") { YH1LP2 = 0; } else { YH1LP2 = Convert.ToDouble(tHLP2.Text); }//
                                                                                               //if (tHLP2.Text == "") { YH2LP2 = 0; } else { YH2LP2 = Convert.ToDouble(tHLP2.Text); }//
            if (tL1LP2.Text == "") { YL1LP2 = 0; } else { YL1LP2 = Convert.ToDouble(tL1LP2.Text); }//
            if (tL2LP2.Text == "") { YL2LP2 = 0; } else { YL2LP2 = Convert.ToDouble(tL2LP2.Text); }//



            if (taLP2.Text == "") { YaLP2 = 0; } else { YaLP2 = Convert.ToDouble(taLP2.Text); }//
            if (tbLP2.Text == "") { YbLP2 = 0; } else { YbLP2 = Convert.ToDouble(tbLP2.Text); }//
            if (td0LP2.Text == "") { Yd0LP2 = 0; } else { Yd0LP2 = Convert.ToDouble(td0LP2.Text); }//
            if (ttLP2.Text == "") { YtLP2 = 0; } else { YtLP2 = Convert.ToDouble(ttLP2.Text); }//
            if (tcLP2.Text == "") { YcLP2 = 0; } else { YcLP2 = Convert.ToDouble(tcLP2.Text); }//


            if (teLP2.Text == "") { YeLP2 = 0; } else { YeLP2 = Convert.ToDouble(teLP2.Text); }
            if (tfLP2.Text == "") { YffLP2 = 0; } else { YffLP2 = Convert.ToDouble(tfLP2.Text); }
            if (ttgLP2.Text == "") { YgLP2 = 0; } else { YgLP2 = Convert.ToDouble(ttgLP2.Text); }

            if (BoxDDLP2.Text == "") { YDDLP2 = 0; }
            else
            {
                Regex rx = new Regex("^(([0-9]+.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*.[0-9]+)|([0-9]*[1-9][0-9]*))$");
                if (rx.IsMatch(BoxDDLP2.Text))
                {
                    YDDLP2 = Convert.ToDouble(BoxDDLP2.Text);
                }
                else
                {
                    YDDLP2 = 0;
                }
            }

            //钢丝绳拉力计算,钢丝绳选型用分股标准值，用于吊耳计算的是合股的设计值
            if (YGLP2 < 0) { YGLP2 = -1 * YGLP2; }
            //这里的计算式要改，求标准值和设计值
            // 这里传入
            //double YGLP2 = 0;
            //double YH1LP2 = 0;
            //double YL1LP2 = 0;
            //double YS1LP2 = 0;
            //double cosDS1 = 0;
            //double sinDS1 = 0;
            //double cosDH1 = 0;
            //double sinDH1 = 0;
            //double NS1 = 0;//合股标准值
            //double NS1F = 0;// 分股标准值
            //double NS1S = 0;//合股设计值


            //double YH2LP2 = 0;
            //double YL2LP2 = 0;
            //double YS2LP2 = 0;
            //double cosDS2 = 0;
            //double sinDS2 = 0;
            //double cosDH2 = 0;
            //double sinDH2 = 0;
            //double NS2 = 0;//合股标准值
            //double NS2F = 0;// 分股标准值
            //double NS2S = 0;//合股设计值

            //double YH1LP2 = 0;
            //double YL1LP2 = 0;
            //double YS1LP2 = 0;
            //double YH2LP2 = 0;
            //double YL2LP2 = 0;
            //double YS2LP2 = 0;



            //钢丝绳容许应力计算
            if (BOXSLP2.Text == "" || BoxXLP2.Text == "" || BoxZJLP2.Text == "") { MessageBox.Show("请输全钢丝绳信息"); }
            else
            {
                switch (BoxXLP2.Text)
                {
                    case "6x19":
                        αLP2 = 0.85;
                        break;

                    case "6x37":
                        αLP2 = 0.82;
                        break;

                    case "6x61":
                        αLP2 = 0.80;
                        break;
                }

                GssLP2 = BOXSLP2.Text + " " + BoxXLP2.Text + " " + BoxZJLP2.Text;


                switch (GssLP2)
                {
                    //6x19 1400MPa列
                    case "1400MPa 6x19 6.2":
                        FgLP2 = 20.0;
                        break;
                    case "1400MPa 6x19 7.7":
                        FgLP2 = 31.3;
                        break;
                    case "1400MPa 6x19 9.3":
                        FgLP2 = 45.1;
                        break;
                    case "1400MPa 6x19 11.0":
                        FgLP2 = 61.3;
                        break;
                    case "1400MPa 6x19 12.5":
                        FgLP2 = 80.1;
                        break;
                    case "1400MPa 6x19 14.0":
                        FgLP2 = 101.0;
                        break;
                    case "1400MPa 6x19 15.5":
                        FgLP2 = 125.0;
                        break;
                    case "1400MPa 6x19 17.0":
                        FgLP2 = 151.5;
                        break;
                    case "1400MPa 6x19 18.5":
                        FgLP2 = 180.0;
                        break;
                    case "1400MPa 6x19 20.0":
                        FgLP2 = 211.5;
                        break;
                    case "1400MPa 6x19 21.5":
                        FgLP2 = 245.5;
                        break;
                    case "1400MPa 6x19 23.0":
                        FgLP2 = 281.5;
                        break;
                    case "1400MPa 6x19 24.5":
                        FgLP2 = 320.5;
                        break;
                    case "1400MPa 6x19 26.0":
                        FgLP2 = 362.0;
                        break;
                    case "1400MPa 6x19 28.0":
                        FgLP2 = 405.5;
                        break;
                    case "1400MPa 6x19 31.0":
                        FgLP2 = 501.0;
                        break;
                    case "1400MPa 6x19 34.0":
                        FgLP2 = 606.0;
                        break;
                    case "1400MPa 6x19 37.0":
                        FgLP2 = 721.5;
                        break;
                    case "1400MPa 6x19 40.0":
                        FgLP2 = 846.5;
                        break;
                    case "1400MPa 6x19 43":
                        FgLP2 = 982.0;
                        break;
                    case "1400MPa 6x19 46":
                        FgLP2 = 1125.0;
                        break;


                    //6x19 1550MPa列
                    case "1550MPa 6x19 6.2":
                        FgLP2 = 22.1;
                        break;
                    case "1550MPa 6x19 7.7":
                        FgLP2 = 34.6;
                        break;
                    case "1550MPa 6x19 9.3":
                        FgLP2 = 49.9;
                        break;
                    case "1550MPa 6x19 11.0":
                        FgLP2 = 67.9;
                        break;
                    case "1550MPa 6x19 12.5":
                        FgLP2 = 88.7;
                        break;
                    case "1550MPa 6x19 14.0":
                        FgLP2 = 112.0;
                        break;
                    case "1550MPa 6x19 15.5":
                        FgLP2 = 138.5;
                        break;
                    case "1550MPa 6x19 17.0":
                        FgLP2 = 167.5;
                        break;
                    case "1550MPa 6x19 18.5":
                        FgLP2 = 199.5;
                        break;
                    case "1550MPa 6x19 20.0":
                        FgLP2 = 234.0;
                        break;
                    case "1550MPa 6x19 21.5":
                        FgLP2 = 271.5;
                        break;
                    case "1550MPa 6x19 23.0":
                        FgLP2 = 312.0;
                        break;
                    case "1550MPa 6x19 24.5":
                        FgLP2 = 355.0;
                        break;
                    case "1550MPa 6x19 26.0":
                        FgLP2 = 400.5;
                        break;
                    case "1550MPa 6x19 28.0":
                        FgLP2 = 449.0;
                        break;
                    case "1550MPa 6x19 31.0":
                        FgLP2 = 554.5;
                        break;
                    case "1550MPa 6x19 34.0":
                        FgLP2 = 671.0;
                        break;
                    case "1550MPa 6x19 37.0":
                        FgLP2 = 798.5;
                        break;
                    case "1550MPa 6x19 40.0":
                        FgLP2 = 937.5;
                        break;
                    case "1550MPa 6x19 43":
                        FgLP2 = 1085.0;
                        break;
                    case "1550MPa 6x19 46":
                        FgLP2 = 1245.0;
                        break;


                    //6x19 1700MPa列
                    case "1700MPa 6x19 6.2":
                        FgLP2 = 24.3;
                        break;
                    case "1700MPa 6x19 7.7":
                        FgLP2 = 38.0;
                        break;
                    case "1700MPa 6x19 9.3":
                        FgLP2 = 54.7;
                        break;
                    case "1700MPa 6x19 11.0":
                        FgLP2 = 74.5;
                        break;
                    case "1700MPa 6x19 12.5":
                        FgLP2 = 97.3;
                        break;
                    case "1700MPa 6x19 14.0":
                        FgLP2 = 123.0;
                        break;
                    case "1700MPa 6x19 15.5":
                        FgLP2 = 152.0;
                        break;
                    case "1700MPa 6x19 17.0":
                        FgLP2 = 184.0;
                        break;
                    case "1700MPa 6x19 18.5":
                        FgLP2 = 219.0;
                        break;
                    case "1700MPa 6x19 20.0":
                        FgLP2 = 257.0;
                        break;
                    case "1700MPa 6x19 21.5":
                        FgLP2 = 298.0;
                        break;
                    case "1700MPa 6x19 23.0":
                        FgLP2 = 342.0;
                        break;
                    case "1700MPa 6x19 24.5":
                        FgLP2 = 389.0;
                        break;
                    case "1700MPa 6x19 26.0":
                        FgLP2 = 439.0;
                        break;
                    case "1700MPa 6x19 28.0":
                        FgLP2 = 492.5;
                        break;
                    case "1700MPa 6x19 31.0":
                        FgLP2 = 608.5;
                        break;
                    case "1700MPa 6x19 34.0":
                        FgLP2 = 736.0;
                        break;
                    case "1700MPa 6x19 37.0":
                        FgLP2 = 876.0;
                        break;
                    case "1700MPa 6x19 40.0":
                        FgLP2 = 1025.0;
                        break;
                    case "1700MPa 6x19 43":
                        FgLP2 = 1190.0;
                        break;
                    case "1700MPa 6x19 46":
                        FgLP2 = 1365.0;
                        break;

                    //6x19 1850MPa列
                    case "1850MPa 6x19 6.2":
                        FgLP2 = 26.3;
                        break;
                    case "1850MPa 6x19 7.7":
                        FgLP2 = 41.3;
                        break;
                    case "1850MPa 6x19 9.3":
                        FgLP2 = 59.6;
                        break;
                    case "1850MPa 6x19 11.0":
                        FgLP2 = 81.1;
                        break;
                    case "1850MPa 6x19 12.5":
                        FgLP2 = 105.5;
                        break;
                    case "1850MPa 6x19 14.0":
                        FgLP2 = 134.0;
                        break;
                    case "1850MPa 6x19 15.5":
                        FgLP2 = 165.5;
                        break;
                    case "1850MPa 6x19 17.0":
                        FgLP2 = 200.0;
                        break;
                    case "1850MPa 6x19 18.5":
                        FgLP2 = 238.0;
                        break;
                    case "1850MPa 6x19 20.0":
                        FgLP2 = 279.5;
                        break;
                    case "1850MPa 6x19 21.5":
                        FgLP2 = 324.5;
                        break;
                    case "1850MPa 6x19 23.0":
                        FgLP2 = 372.5;
                        break;
                    case "1850MPa 6x19 24.5":
                        FgLP2 = 423.5;
                        break;
                    case "1850MPa 6x19 26.0":
                        FgLP2 = 478.0;
                        break;
                    case "1850MPa 6x19 28.0":
                        FgLP2 = 536.0;
                        break;
                    case "1850MPa 6x19 31.0":
                        FgLP2 = 662.0;
                        break;
                    case "1850MPa 6x19 34.0":
                        FgLP2 = 801.0;
                        break;
                    case "1850MPa 6x19 37.0":
                        FgLP2 = 953.5;
                        break;
                    case "1850MPa 6x19 40.0":
                        FgLP2 = 1115.0;
                        break;
                    case "1850MPa 6x19 43":
                        FgLP2 = 1295.0;
                        break;
                    case "1850MPa 6x19 46":
                        FgLP2 = 1490.0;
                        break;

                    //6x37 1400MPa列
                    case "1400MPa 6x37 8.7":
                        FgLP2 = 39.0;
                        break;
                    case "1400MPa 6x37 11.0":
                        FgLP2 = 60.9;
                        break;
                    case "1400MPa 6x37 13.0":
                        FgLP2 = 87.8;
                        break;
                    case "1400MPa 6x37 15.0":
                        FgLP2 = 119.5;
                        break;
                    case "1400MPa 6x37 17.5":
                        FgLP2 = 156.0;
                        break;
                    case "1400MPa 6x37 19.5":
                        FgLP2 = 197.5;
                        break;
                    case "1400MPa 6x37 21.5":
                        FgLP2 = 243.5;
                        break;
                    case "1400MPa 6x37 24.0":
                        FgLP2 = 295.0;
                        break;
                    case "1400MPa 6x37 26.0":
                        FgLP2 = 351.0;
                        break;
                    case "1400MPa 6x37 28.0":
                        FgLP2 = 412.0;
                        break;
                    case "1400MPa 6x37 30.0":
                        FgLP2 = 478.0;
                        break;
                    case "1400MPa 6x37 32.5":
                        FgLP2 = 522.5;
                        break;
                    case "1400MPa 6x37 34.5":
                        FgLP2 = 624.5;
                        break;
                    case "1400MPa 6x37 36.5":
                        FgLP2 = 705.0;
                        break;
                    case "1400MPa 6x37 39.0":
                        FgLP2 = 790.0;
                        break;
                    case "1400MPa 6x37 43.0":
                        FgLP2 = 975.5;
                        break;
                    case "1400MPa 6x37 47.5":
                        FgLP2 = 1180.0;
                        break;
                    case "1400MPa 6x37 52.0":
                        FgLP2 = 1450.0;
                        break;
                    case "1400MPa 6x37 56.0":
                        FgLP2 = 1645.0;
                        break;
                    case "1400MPa 6x37 60.5":
                        FgLP2 = 1910.0;
                        break;
                    case "1400MPa 6x37 65.0":
                        FgLP2 = 2195.0;
                        break;


                    //6x37 1550MPa列
                    case "1550MPa 6x37 8.7":
                        FgLP2 = 43.2;
                        break;
                    case "1550MPa 6x37 11.0":
                        FgLP2 = 67.5;
                        break;
                    case "1550MPa 6x37 13.0":
                        FgLP2 = 97.2;
                        break;
                    case "1550MPa 6x37 15.0":
                        FgLP2 = 132.0;
                        break;
                    case "1550MPa 6x37 17.5":
                        FgLP2 = 172.5;
                        break;
                    case "1550MPa 6x37 19.5":
                        FgLP2 = 213.5;
                        break;
                    case "1550MPa 6x37 21.5":
                        FgLP2 = 270.0;
                        break;
                    case "1550MPa 6x37 24.0":
                        FgLP2 = 326.5;
                        break;
                    case "1550MPa 6x37 26.0":
                        FgLP2 = 388.5;
                        break;
                    case "1550MPa 6x37 28.0":
                        FgLP2 = 456.5;
                        break;
                    case "1550MPa 6x37 30.0":
                        FgLP2 = 529.0;
                        break;
                    case "1550MPa 6x37 32.5":
                        FgLP2 = 607.5;
                        break;
                    case "1550MPa 6x37 34.5":
                        FgLP2 = 691.5;
                        break;
                    case "1550MPa 6x37 36.5":
                        FgLP2 = 780.5;
                        break;
                    case "1550MPa 6x37 39.0":
                        FgLP2 = 875.0;
                        break;
                    case "1550MPa 6x37 43.0":
                        FgLP2 = 1080.0;
                        break;
                    case "1550MPa 6x37 47.5":
                        FgLP2 = 1305.0;
                        break;
                    case "1550MPa 6x37 52.0":
                        FgLP2 = 1555.0;
                        break;
                    case "1550MPa 6x37 56.0":
                        FgLP2 = 1825.0;
                        break;
                    case "1550MPa 6x37 60.5":
                        FgLP2 = 2115.0;
                        break;
                    case "1550MPa 6x37 65.0":
                        FgLP2 = 2430.0;
                        break;


                    //6x37 1700MPa列
                    case "1700MPa 6x37 8.7":
                        FgLP2 = 47.3;
                        break;
                    case "1700MPa 6x37 11.0":
                        FgLP2 = 74.0;
                        break;
                    case "1700MPa 6x37 13.0":
                        FgLP2 = 106.5;
                        break;
                    case "1700MPa 6x37 15.0":
                        FgLP2 = 145.0;
                        break;
                    case "1700MPa 6x37 17.5":
                        FgLP2 = 189.5;
                        break;
                    case "1700MPa 6x37 19.5":
                        FgLP2 = 239.5;
                        break;
                    case "1700MPa 6x37 21.5":
                        FgLP2 = 296.0;
                        break;
                    case "1700MPa 6x37 24.0":
                        FgLP2 = 358.0;
                        break;
                    case "1700MPa 6x37 26.0":
                        FgLP2 = 426.5;
                        break;
                    case "1700MPa 6x37 28.0":
                        FgLP2 = 500.5;
                        break;
                    case "1700MPa 6x37 30.0":
                        FgLP2 = 580.5;
                        break;
                    case "1700MPa 6x37 32.5":
                        FgLP2 = 666.5;
                        break;
                    case "1700MPa 6x37 34.5":
                        FgLP2 = 758.5;
                        break;
                    case "1700MPa 6x37 36.5":
                        FgLP2 = 856.0;
                        break;
                    case "1700MPa 6x37 39.0":
                        FgLP2 = 959.5;
                        break;
                    case "1700MPa 6x37 43.0":
                        FgLP2 = 1185.0;
                        break;
                    case "1700MPa 6x37 47.5":
                        FgLP2 = 1430.0;
                        break;
                    case "1700MPa 6x37 52.0":
                        FgLP2 = 1705.0;
                        break;
                    case "1700MPa 6x37 56.0":
                        FgLP2 = 2000.0;
                        break;
                    case "1700MPa 6x37 60.5":
                        FgLP2 = 2320.0;
                        break;
                    case "1700MPa 6x37 65.0":
                        FgLP2 = 2665.0;
                        break;



                    //6x37 1850MPa列
                    case "1850MPa 6x37 8.7":
                        FgLP2 = 51.5;
                        break;
                    case "1850MPa 6x37 11.0":
                        FgLP2 = 80.6;
                        break;
                    case "1850MPa 6x37 13.0":
                        FgLP2 = 116.5;
                        break;
                    case "1850MPa 6x37 15.0":
                        FgLP2 = 157.5;
                        break;
                    case "1850MPa 6x37 17.5":
                        FgLP2 = 206.0;
                        break;
                    case "1850MPa 6x37 19.5":
                        FgLP2 = 261.0;
                        break;
                    case "1850MPa 6x37 21.5":
                        FgLP2 = 322.0;
                        break;
                    case "1850MPa 6x37 24.0":
                        FgLP2 = 390.0;
                        break;
                    case "1850MPa 6x37 26.0":
                        FgLP2 = 464.0;
                        break;
                    case "1850MPa 6x37 28.0":
                        FgLP2 = 544.5;
                        break;
                    case "1850MPa 6x37 30.0":
                        FgLP2 = 631.5;
                        break;
                    case "1850MPa 6x37 32.5":
                        FgLP2 = 725.0;
                        break;
                    case "1850MPa 6x37 34.5":
                        FgLP2 = 758.0;
                        break;
                    case "1850MPa 6x37 36.5":
                        FgLP2 = 856.0;
                        break;
                    case "1850MPa 6x37 39.0":
                        FgLP2 = 959.5;
                        break;
                    case "1850MPa 6x37 43.0":
                        FgLP2 = 1185.0;
                        break;
                    case "1850MPa 6x37 47.5":
                        FgLP2 = 1430.0;
                        break;
                    case "1850MPa 6x37 52.0":
                        FgLP2 = 1705.0;
                        break;
                    case "1850MPa 6x37 56.0":
                        FgLP2 = 2000.0;
                        break;
                    case "1850MPa 6x37 60.5":
                        FgLP2 = 2320.0;
                        break;
                    case "1850MPa 6x37 65.0":
                        FgLP2 = 2665.0;
                        break;



                    //6x61 1400MPa列
                    case "1400MPa 6x61 11.0":
                        FgLP2 = 64.3;
                        break;
                    case "1400MPa 6x61 14.0":
                        FgLP2 = 100.5;
                        break;
                    case "1400MPa 6x61 16.5":
                        FgLP2 = 144.5;
                        break;
                    case "1400MPa 6x61 19.5":
                        FgLP2 = 197.0;
                        break;
                    case "1400MPa 6x61 22.0":
                        FgLP2 = 257.0;
                        break;
                    case "1400MPa 6x61 25.0":
                        FgLP2 = 325.5;
                        break;
                    case "1400MPa 6x61 27.5":
                        FgLP2 = 402.0;
                        break;
                    case "1400MPa 6x61 30.5":
                        FgLP2 = 226.5;
                        break;
                    case "1400MPa 6x61 33.0":
                        FgLP2 = 579.0;
                        break;
                    case "1400MPa 6x61 36.0":
                        FgLP2 = 679.5;
                        break;
                    case "1400MPa 6x61 38.5":
                        FgLP2 = 788.0;
                        break;
                    case "1400MPa 6x61 41.5":
                        FgLP2 = 905.0;
                        break;
                    case "1400MPa 6x61 44.0":
                        FgLP2 = 1025.0;
                        break;
                    case "1400MPa 6x61 47.0":
                        FgLP2 = 1160.0;
                        break;
                    case "1400MPa 6x61 50.0":
                        FgLP2 = 1300.0;
                        break;
                    case "1400MPa 6x61 55.5":
                        FgLP2 = 1605.0;
                        break;
                    case "1400MPa 6x61 61.0":
                        FgLP2 = 1945.0;
                        break;
                    case "1400MPa 6x61 66.5":
                        FgLP2 = 2315.0;
                        break;
                    case "1400MPa 6x61 72.0":
                        FgLP2 = 2715.5;
                        break;
                    case "1400MPa 6x61 77.5":
                        FgLP2 = 3150.0;
                        break;
                    case "1400MPa 6x61 83.0":
                        FgLP2 = 3620.0;
                        break;



                    //6x61 1550MPa列
                    case "1550MPa 6x61 11.0":
                        FgLP2 = 71.2;
                        break;
                    case "1550MPa 6x61 14.0":
                        FgLP2 = 111.0;
                        break;
                    case "1550MPa 6x61 16.5":
                        FgLP2 = 160.0;
                        break;
                    case "1550MPa 6x61 19.5":
                        FgLP2 = 218.0;
                        break;
                    case "1550MPa 6x61 22.0":
                        FgLP2 = 285.0;
                        break;
                    case "1550MPa 6x61 25.0":
                        FgLP2 = 360.5;
                        break;
                    case "1550MPa 6x61 27.5":
                        FgLP2 = 445.0;
                        break;
                    case "1550MPa 6x61 30.5":
                        FgLP2 = 538.5;
                        break;
                    case "1550MPa 6x61 33.0":
                        FgLP2 = 641.0;
                        break;
                    case "1550MPa 6x61 36.0":
                        FgLP2 = 752.5;
                        break;
                    case "1550MPa 6x61 38.5":
                        FgLP2 = 872.5;
                        break;
                    case "1550MPa 6x61 41.5":
                        FgLP2 = 1000.0;
                        break;
                    case "1550MPa 6x61 44.0":
                        FgLP2 = 1140.0;
                        break;
                    case "1550MPa 6x61 47.0":
                        FgLP2 = 1285.0;
                        break;
                    case "1550MPa 6x61 50.0":
                        FgLP2 = 1440.0;
                        break;
                    case "1550MPa 6x61 55.5":
                        FgLP2 = 1780.0;
                        break;
                    case "1550MPa 6x61 61.0":
                        FgLP2 = 2155.0;
                        break;
                    case "1550MPa 6x61 66.5":
                        FgLP2 = 2565.0;
                        break;
                    case "1550MPa 6x61 72.0":
                        FgLP2 = 3010.0;
                        break;
                    case "1550MPa 6x61 77.5":
                        FgLP2 = 3490.0;
                        break;
                    case "1550MPa 6x61 83.0":
                        FgLP2 = 4005.0;
                        break;


                    //6x61 1700MPa列
                    case "1700MPa 6x61 11.0":
                        FgLP2 = 78.1;
                        break;
                    case "1700MPa 6x61 14.0":
                        FgLP2 = 122.0;
                        break;
                    case "1700MPa 6x61 16.5":
                        FgLP2 = 175.5;
                        break;
                    case "1700MPa 6x61 19.5":
                        FgLP2 = 239.0;
                        break;
                    case "1700MPa 6x61 22.0":
                        FgLP2 = 312.5;
                        break;
                    case "1700MPa 6x61 25.0":
                        FgLP2 = 395.5;
                        break;
                    case "1700MPa 6x61 27.5":
                        FgLP2 = 228.0;
                        break;
                    case "1700MPa 6x61 30.5":
                        FgLP2 = 591.0;
                        break;
                    case "1700MPa 6x61 33.0":
                        FgLP2 = 703.0;
                        break;
                    case "1700MPa 6x61 36.0":
                        FgLP2 = 825.0;
                        break;
                    case "1700MPa 6x61 38.5":
                        FgLP2 = 957.0;
                        break;
                    case "1700MPa 6x61 41.5":
                        FgLP2 = 1095.0;
                        break;
                    case "1700MPa 6x61 44.0":
                        FgLP2 = 1250.0;
                        break;
                    case "1700MPa 6x61 47.0":
                        FgLP2 = 1410.0;
                        break;
                    case "1700MPa 6x61 50.0":
                        FgLP2 = 1580.0;
                        break;
                    case "1700MPa 6x61 55.5":
                        FgLP2 = 1950.0;
                        break;
                    case "1700MPa 6x61 61.0":
                        FgLP2 = 2360.0;
                        break;
                    case "1700MPa 6x61 66.5":
                        FgLP2 = 2810.0;
                        break;
                    case "1700MPa 6x61 72.0":
                        FgLP2 = 3300.0;
                        break;
                    case "1700MPa 6x61 77.5":
                        FgLP2 = 3825.0;
                        break;
                    case "1700MPa 6x61 83.0":
                        FgLP2 = 4395.0;
                        break;



                    //6x61 1850MPa列
                    case "1850MPa 6x61 11.0":
                        FgLP2 = 85.0;
                        break;
                    case "1850MPa 6x61 14.0":
                        FgLP2 = 132.0;
                        break;
                    case "1850MPa 6x61 16.5":
                        FgLP2 = 191.0;
                        break;
                    case "1850MPa 6x61 19.5":
                        FgLP2 = 260.0;
                        break;
                    case "1850MPa 6x61 22.0":
                        FgLP2 = 340.0;
                        break;
                    case "1850MPa 6x61 25.0":
                        FgLP2 = 430.5;
                        break;
                    case "1850MPa 6x61 27.5":
                        FgLP2 = 531.5;
                        break;
                    case "1850MPa 6x61 30.5":
                        FgLP2 = 643.0;
                        break;
                    case "1850MPa 6x61 33.0":
                        FgLP2 = 765.0;
                        break;
                    case "1850MPa 6x61 36.0":
                        FgLP2 = 898.0;
                        break;
                    case "1850MPa 6x61 38.5":
                        FgLP2 = 1040.0;
                        break;
                    case "1850MPa 6x61 41.5":
                        FgLP2 = 1195.0;
                        break;
                    case "1850MPa 6x61 44.0":
                        FgLP2 = 1360.0;
                        break;
                    case "1850MPa 6x61 47.0":
                        FgLP2 = 1535.0;
                        break;
                    case "1850MPa 6x61 50.0":
                        FgLP2 = 1720.0;
                        break;
                    case "1850MPa 6x61 55.5":
                        FgLP2 = 2125.0;
                        break;
                    case "1850MPa 6x61 61.0":
                        FgLP2 = 2570.0;
                        break;
                    case "1850MPa 6x61 66.5":
                        FgLP2 = 3060.0;
                        break;
                    case "1850MPa 6x61 72.0":
                        FgLP2 = 3590.0;
                        break;
                    case "1850MPa 6x61 77.5":
                        FgLP2 = 4165.0;
                        break;
                    case "1850MPa 6x61 83.0":
                        FgLP2 = 4780.0;
                        break;

                }


                if (BoxQX1LP2.Text == "") { KLP2 = 0; }
                else
                {

                    KLP2 = Convert.ToDouble(BoxQX1LP2.Text);


                }




                //这是卡环的承载力
                if (BoxKHLP2.Text == "") { MessageBox.Show("请输入卡环型号"); }
                else
                {
                    switch (BoxKHLP2.Text)
                    {
                        case "0.2":
                            rFhLP2 = 2.45;
                            break;
                        case "0.4":
                            rFhLP2 = 3.92;
                            break;
                        case "0.6":
                            rFhLP2 = 5.88;
                            break;
                        case "0.9":
                            rFhLP2 = 8.82;
                            break;
                        case "1.2":
                            rFhLP2 = 12.25;
                            break;
                        case "1.7":
                            rFhLP2 = 17.15;
                            break;
                        case "2.1":
                            rFhLP2 = 20.58;
                            break;
                        case "2.7":
                            rFhLP2 = 26.95;
                            break;
                        case "3.5":
                            rFhLP2 = 34.30;
                            break;
                        case "4.5":
                            rFhLP2 = 44.10;
                            break;
                        case "6.0":
                            rFhLP2 = 58.80;
                            break;
                        case "7.5":
                            rFhLP2 = 73.50;
                            break;
                        case "9.5":
                            rFhLP2 = 93.10;
                            break;
                        case "11.0":
                            rFhLP2 = 107.80;
                            break;
                        case "14.0":
                            rFhLP2 = 137.20;
                            break;
                        case "17.5":
                            rFhLP2 = 171.50;
                            break;
                        case "21.0":
                            rFhLP2 = 205.80;
                            break;
                    }





                    switch (BoxBLP2.Text)
                    {
                        case "Q235B":
                            if (0 < YtLP2 && YtLP2 <= 16)
                            { YfLP2 = 215; YfvLP2 = 125; YfcLP2 = 405; }
                            else if (YtLP2 > 16 && YtLP2 <= 40)
                            { YfLP2 = 205; YfvLP2 = 120; YfcLP2 = 405; }
                            else if (YtLP2 > 40 && YtLP2 <= 100)
                            { YfLP2 = 200; YfvLP2 = 115; YfcLP2 = 405; }
                            else if (YtLP2 < 0 || YtLP2 > 100)
                            { YfLP2 = 1; YfvLP2 = 1; }
                            break;
                        case "Q355B":
                            if (0 < YtLP2 && YtLP2 <= 16)
                            { YfLP2 = 305; YfvLP2 = 175; YfcLP2 = 510; }
                            else if (YtLP2 > 16 && YtLP2 <= 40)
                            { YfLP2 = 295; YfvLP2 = 170; YfcLP2 = 510; }
                            else if (YtLP2 > 40 && YtLP2 <= 63)
                            { YfLP2 = 290; YfvLP2 = 165; YfcLP2 = 510; }
                            else if (YtLP2 > 63 && YtLP2 <= 80)
                            { YfLP2 = 280; YfvLP2 = 160; YfcLP2 = 510; }
                            else if (YtLP2 > 80 && YtLP2 <= 100)
                            { YfLP2 = 270; YfvLP2 = 155; YfcLP2 = 510; }
                            else if (YtLP2 < 0 || YtLP2 > 100)
                            { YfLP2 = 1; YfvLP2 = 1; }
                            break;
                        case "Q390B":
                            if (0 < YtLP2 && YtLP2 <= 16)
                            { YfLP2 = 345; YfvLP2 = 200; YfcLP2 = 530; }
                            else if (YtLP2 > 16 && YtLP2 <= 40)
                            { YfLP2 = 330; YfvLP2 = 190; YfcLP2 = 530; }
                            else if (YtLP2 > 40 && YtLP2 <= 63)
                            { YfLP2 = 310; YfvLP2 = 180; YfcLP2 = 530; }
                            else if (YtLP2 > 63 && YtLP2 <= 100)
                            { YfLP2 = 295; YfvLP2 = 170; YfcLP2 = 530; }
                            else if (YtLP2 <= 0 || YtLP2 > 100)
                            { YfLP2 = 1; YfvLP2 = 1; }
                            break;
                        case "Q420B":
                            if (0 < YtLP2 && YtLP2 <= 16)
                            { YfLP2 = 375; YfvLP2 = 215; YfcLP2 = 560; }
                            else if (YtLP2 > 16 && YtLP2 <= 40)
                            { YfLP2 = 355; YfvLP2 = 205; YfcLP2 = 560; }
                            else if (YtLP2 > 40 && YtLP2 <= 63)
                            { YfLP2 = 320; YfvLP2 = 185; YfcLP2 = 560; }
                            else if (YtLP2 > 63 && YtLP2 <= 100)
                            { YfLP2 = 305; YfvLP2 = 175; YfcLP2 = 560; }
                            else if (YtLP2 <= 0 || YtLP2 > 100)
                            { YfLP2 = 1; YfvLP2 = 1; }
                            break;
                    }
                    YbeLP2 = 2 * YtLP2 + 16;
                    Yb1LP2 = Math.Min((2 * YtLP2 + 16), (YbLP2 - Yd0LP2 / 3));
                    if (BoxBLP2.Text == "")
                    { MessageBox.Show("请选择耳板材质"); GSSLLLP2.Text = "暂时不知道"; GSSRXLLLP2.Text = "暂时不知道"; KHFHLP2.Text = "暂时不知道"; GSSLLsLP2.Text = "暂时不知道"; GZLP2.Text = "暂时不知道"; JKLLP2.Text = "暂时不知道"; PKLP2.Text = "暂时不知道"; KJLP2.Text = "暂时不知道"; FHLP2.Text = "暂时不知道"; DJLP2.Text = "暂时不知道"; }
                    else if (YfLP2 == 1)
                    { MessageBox.Show("厚度t一般大于0mm且小于100mm,重新输入看看吧"); GSSLLLP2.Text = "暂时不知道"; GSSRXLLLP2.Text = "暂时不知道"; KHFHLP2.Text = "暂时不知道"; GSSLLsLP2.Text = "暂时不知道"; GZLP2.Text = "暂时不知道"; JKLLP2.Text = "暂时不知道"; PKLP2.Text = "暂时不知道"; KJLP2.Text = "暂时不知道"; FHLP2.Text = "暂时不知道"; DJLP2.Text = "暂时不知道"; }
                    else if (YDDLP2 == 0)
                    { MessageBox.Show("动力系数输入有误"); GSSLLLP2.Text = "暂时不知道"; GSSRXLLLP2.Text = "暂时不知道"; KHFHLP2.Text = "暂时不知道"; GSSLLsLP2.Text = "暂时不知道"; GZLP2.Text = "暂时不知道"; JKLLP2.Text = "暂时不知道"; PKLP2.Text = "暂时不知道"; KJLP2.Text = "暂时不知道"; FHLP2.Text = "暂时不知道"; DJLP2.Text = "暂时不知道"; }
                    else if (KLP2 <= 0)
                    { MessageBox.Show("安全系数输入有误"); GSSLLLP2.Text = "暂时不知道"; GSSRXLLLP2.Text = "暂时不知道"; KHFHLP2.Text = "暂时不知道"; GSSLLsLP2.Text = "暂时不知道"; GZLP2.Text = "暂时不知道"; JKLLP2.Text = "暂时不知道"; PKLP2.Text = "暂时不知道"; KJLP2.Text = "暂时不知道"; FHLP2.Text = "暂时不知道"; DJLP2.Text = "暂时不知道"; }

                    else if (YGLP2 <= 0 || YS1LP2 <= 0 || YS2LP2 <= 0 || YL1LP2 <= 0 || YL2LP2 <= 0 || YaLP2 <= 0 || YbLP2 <= 0 || Yd0LP2 <= 0 || YcLP2 <= 0 || YeLP2 <= 0 || YffLP2 <= 0 || YgLP2 <= 0)
                    { MessageBox.Show("实际构件尺寸一般为正值,实际重量一般不等于0"); GSSLLLP2.Text = "暂时不知道"; GSSRXLLLP2.Text = "暂时不知道"; KHFHLP2.Text = "暂时不知道"; GSSLLsLP2.Text = "暂时不知道"; GZLP2.Text = "暂时不知道"; JKLLP2.Text = "暂时不知道"; PKLP2.Text = "暂时不知道"; KJLP2.Text = "暂时不知道"; FHLP2.Text = "暂时不知道"; DJLP2.Text = "暂时不知道"; }
                    //double YH1LP2 = 0;
                    //double YL1LP2 = 0;
                    //double YS1LP2 = 0;

                    //double YH2LP2 = 0;
                    //double YL2LP2 = 0;
                    //double YS2LP2 = 0;


                    else if (YL2LP2 < Math.Abs(YL1LP2 - (YS1LP2 + YS2LP2)) || YL1LP2 < Math.Abs(YL2LP2 - (YS1LP2 + YS2LP2)) || (YS1LP2 + YS2LP2) < Math.Abs(YL2LP2 - YL1LP2))
                    { MessageBox.Show("三个边边长尺寸不能形成三角形"); GSSLLLP2.Text = "暂时不知道"; GSSRXLLLP2.Text = "暂时不知道"; KHFHLP2.Text = "暂时不知道"; GSSLLsLP2.Text = "暂时不知道"; GZLP2.Text = "暂时不知道"; JKLLP2.Text = "暂时不知道"; PKLP2.Text = "暂时不知道"; KJLP2.Text = "暂时不知道"; FHLP2.Text = "暂时不知道"; DJLP2.Text = "暂时不知道"; }



                    else
                    {
                        cosDH1 = Math.Abs(((YS1LP2 + YS2LP2) * (YS1LP2 + YS2LP2) + (YL1LP2 * YL1LP2) - YL2LP2 * YL2LP2) / 2 / (YS1LP2 + YS2LP2) / YL1LP2);
                        cosDH2 = Math.Abs(((YS1LP2 + YS2LP2) * (YS1LP2 + YS2LP2) + (YL2LP2 * YL2LP2) - YL1LP2 * YL1LP2) / 2 / (YS1LP2 + YS2LP2) / YL2LP2);
                        YH1LP2 = Math.Sqrt(YL1LP2 * YL1LP2 + YS1LP2 * YS1LP2 - 2 * YL1LP2 * YS1LP2 * cosDH1);
                        YH2LP2 = Math.Sqrt(YL2LP2 * YL2LP2 + YS2LP2 * YS2LP2 - 2 * YL2LP2 * YS2LP2 * cosDH2);
                        // MessageBox.Show(cosDH1.ToString() + "," + cosDH2.ToString() + "," + YH1LP2.ToString()+ ","+ YH2LP2.ToString());
                        cosDS1 = Math.Abs((YL1LP2 * YL1LP2 + YH1LP2 * YH1LP2 - YS1LP2 * YS1LP2) / 2 / YL1LP2 / YH1LP2);
                        cosDS2 = Math.Abs((YL2LP2 * YL2LP2 + YH2LP2 * YH2LP2 - YS2LP2 * YS2LP2) / 2 / YL2LP2 / YH2LP2);
                        // MessageBox.Show("cosDS1:" + cosDS1.ToString());//this 
                        // MessageBox.Show("cosDS2:" + cosDS2.ToString());//this 

                        sinDS1 = Math.Sqrt(1 - cosDS1 * cosDS1);
                        sinDS2 = Math.Sqrt(1 - cosDS2 * cosDS2);

                        //MessageBox.Show("sinDS1:" + sinDS1.ToString());//this 
                        //MessageBox.Show("sinDS2:" + sinDS2.ToString());//this 




                        //MessageBox.Show("cosDH1:" + cosDH1.ToString());//this 
                        //MessageBox.Show("cosDH2:" + cosDH2.ToString());//this 

                        sinDH1 = Math.Sqrt(1 - cosDH1 * cosDH1);
                        sinDH2 = Math.Sqrt(1 - cosDH2 * cosDH2);

                        //MessageBox.Show("sinDH1:" + sinDH1.ToString());//this 
                        //MessageBox.Show("sinDH2:" + sinDH2.ToString());//this 
                        if (sinDH1 < 0.70710678118654752440084436210225 || sinDH2 < 0.70710678118654752440084436210225)
                        { MessageBox.Show("吊索与所吊构件的水平夹角宜大于45°"); GSSLL.Text = "暂时不知道"; GSSRXLL.Text = "暂时不知道"; KHFH.Text = "暂时不知道"; GSSLLs.Text = "暂时不知道"; GZ.Text = "暂时不知道"; JKL.Text = "暂时不知道"; PK.Text = "暂时不知道"; KJ.Text = "暂时不知道"; FH.Text = "暂时不知道"; DJ.Text = "暂时不知道"; }

                        else
                        {
                            rFgLP2 = αLP2 * FgLP2 / KLP2;
                            NS1 = YDDLP2 * YGLP2 * 10 / (cosDS1 + (sinDS1 / sinDS2 * cosDS2));//合股标准值1#，动力系数1.3//动力系数是YDDLP2
                                                                                              // MessageBox.Show(NS1.ToString());
                            NS1F = NS1;//合股标准值1#
                                       // MessageBox.Show(NS1F.ToString());
                            NS1S = NS1 * 1.5;//合股标准值1#
                                             //  MessageBox.Show(NS1S.ToString());

                            NS2 = NS1 * sinDS1 / sinDS2;//合股标准值1#
                            NS2F = NS2;//分股标准值1#
                            NS2S = NS2 * 1.5;//合股标准值1#


                            YN2LP2 = Math.Max(NS1F, NS2F);//分股标准值//
                            YN3LP2 = Math.Max(NS1, NS2);// 合股标准值//
                            YNLP2 = Math.Max(NS1S, NS2S);//合股设计值//
                                                         //  MessageBox.Show(YNLP2.ToString());
                                                         //这里的分股标准值和合股设计值要改下右边的变量名，且要算两个最大值 //不改了，因为之前的就是最大值
                            GSSLLLP2.Text = YN2LP2.ToString("#.##");//分股标准值
                            GSSLLsLP2.Text = YNLP2.ToString("#.##");//合股设计值
                                                                    //那两个放进来
                          
                            if (rFgLP2 < YN2LP2)
                            { GSSRXLLLP2.Text = rFgLP2.ToString("#.##") + "，" + "钢丝绳不满足要求"; GSSRXLLLP2.ForeColor = Color.Red; }
                            else
                            { GSSRXLLLP2.Text = rFgLP2.ToString("#.##") + "，" + "钢丝绳满足要求"; GSSRXLLLP2.ForeColor = Color.Black; }
                            if (rFhLP2 < YN3LP2)
                            { KHFHLP2.Text = rFhLP2.ToString("#.##") + "<" + YN2LP2.ToString("#.##") + "，" + "卡环不满足要求"; KHFHLP2.ForeColor = Color.Red; }
                            else
                            { KHFHLP2.Text = rFhLP2.ToString("#.##") + "，" + "卡环满足要求"; KHFHLP2.ForeColor = Color.Black; }


                            //这一段是吊耳板《钢结构设计标准》的计算内容
                            if (YaLP2 < (YbeLP2 / 3 * 4)) { GZLP2.Text = "a" + ":" + YaLP2.ToString() + "< " + "4/3be" + ":" + (YbeLP2 * 4 / 3).ToString("#.##") + "," + "构造不满足"; GZLP2.ForeColor = Color.Red; }
                            else if (YbLP2 < YbeLP2) { GZLP2.Text = "b" + ":" + YbLP2.ToString() + "< " + "be" + ":" + (YbeLP2).ToString("#.##") + "," + "构造不满足"; GZLP2.ForeColor = Color.Red; }
                            else { GZLP2.Text = "构造满足"; GZLP2.ForeColor = Color.Black; }
                            Yσ1LP2 = YNLP2 * 1000 / (2 * YtLP2 * Yb1LP2);
                            Yσ2LP2 = YNLP2 * 1000 / 2 / YtLP2 / (YaLP2 - 2 * Yd0LP2 / 3);
                            YσcLP2 = YNLP2 * 1000 / YtLP2 / (Yd0LP2 - 1);
                            // MessageBox.Show(YNLP2.ToString()+","+ YtLP2.ToString() + "," + (Ya - 2 * Yd0LP2 / 3).ToString() + "," + (Ya).ToString() + "," + (Ya).ToString());
                            YZLP2 = Math.Sqrt((YaLP2 + Yd0LP2 / 2) * (YaLP2 + Yd0LP2 / 2) - (Yd0LP2 / 2) * (Yd0LP2 / 2));
                            YτLP2 = YNLP2 * 1000 / 2 / YtLP2 / YZLP2;



                            if (Yσ1LP2 < YfLP2)
                            { JKLLP2.Text = "σ" + ":" + Yσ1LP2.ToString("#.##") + "<" + "f" + ":" + YfLP2.ToString() + "," + "满足"; JKLLP2.ForeColor = Color.Black; }
                            else
                            { JKLLP2.Text = "σ" + ":" + Yσ1LP2.ToString("#.##") + ">" + "f" + ":" + YfLP2.ToString() + "," + "不满足"; JKLLP2.ForeColor = Color.Red; }

                            if (Yσ2LP2 < YfLP2)
                            { PKLP2.Text = "σ" + ":" + Yσ2LP2.ToString("#.##") + "<" + "f" + ":" + YfLP2.ToString() + "," + "满足"; PKLP2.ForeColor = Color.Black; }
                            else
                            { PKLP2.Text = "σ" + ":" + Yσ2LP2.ToString("#.##") + ">" + "f" + ":" + YfLP2.ToString() + "," + "不满足"; PKLP2.ForeColor = Color.Red; }

                            if (YτLP2 < YfvLP2)
                            { KJLP2.Text = "τ" + ":" + YτLP2.ToString("#.##") + "<" + "fv" + ":" + YfvLP2.ToString() + "," + "满足"; KJLP2.ForeColor = Color.Black; }
                            else
                            { KJLP2.Text = "τ" + ":" + YτLP2.ToString("#.##") + ">" + "fv" + ":" + YfvLP2.ToString() + "," + "不满足"; KJLP2.ForeColor = Color.Red; }

                            if (YσcLP2 < YfcLP2)
                            { CYLP2.Text = "σ" + ":" + YσcLP2.ToString("#.##") + "<" + "f" + ":" + YfcLP2.ToString() + "," + "满足"; CYLP2.ForeColor = Color.Black; }
                            else
                            { CYLP2.Text = "σ" + ":" + YσcLP2.ToString("#.##") + ">" + "f" + ":" + YfcLP2.ToString() + "," + "不满足"; CYLP2.ForeColor = Color.Red; }



                            //这一段是吊耳板《钢结构设计标准》底部受力及加劲板是否干涉的计算
                            YBJLP2 = Math.Sqrt((YffLP2 - 6) * (YffLP2 - 6) + (Math.Abs(YcLP2 - YgLP2) + Yd0LP2 / 2) * (Math.Abs(YcLP2 - YgLP2) + Yd0LP2 / 2)) - Yd0LP2 / 2;
                            if ((2 * YeLP2 + 2 * YffLP2) < (2 * YbLP2 + Yd0LP2)) { MessageBox.Show("底部一般不小于顶部宽度，可以增加e，f值"); }
                            else if (YcLP2 < YgLP2 || YcLP2 < YbLP2) { MessageBox.Show("底部补长a一般比加劲肋边长g高，且不小于双侧边缘净距b"); }
                            else if ((YBJLP2) < 50) { MessageBox.Show("加劲肋距离孔边有点近，可以增加e，f值"); }
                            else
                            {
                                //NS1 = 1.1 * YGLP2 * 10 / (cosDS1 + (sinDS1 / sinDS2 * cosDS2));//合股标准值1#
                                //NS1F = NS1 / YsgLP2;//合股标准值1#
                                //NS1S = NS1 * 1.5;//合股标准值1#

                                //NS2 = NS1 * sinDS1 / sinDS2;//合股标准值1#
                                //NS2F = NS2 / YsgLP2;//合股标准值1#
                                //NS2S = NS2 * 1.5;//合股标准值1#


                                //YN2LP2 = Math.Max(NS1F, NS2F);//分股标准值//
                                //YN3LP2 = Math.Max(NS1, NS2);// 合股标准值//
                                //YNLP2 = Math.Max(NS1S, NS2S);//合股设计值//

                                //double YσF1LP2S1 = 0;//表示竖向力引起的耳板底部正应力
                                //double YσF2LP2S1 = 0;//表示弯矩引起的的耳板底部正应力
                                //double Yτ1LP2S1 = 0;//表示水平力引起的耳板底部剪应力
                                //double YσFZLP2S1 = 0;//表示综合应力

                                //double YσF1LP2S2 = 0;//表示竖向力引起的耳板底部正应力
                                //double YσF2LP2S2 = 0;//表示弯矩引起的的耳板底部正应力
                                //double Yτ1LP2S2 = 0;//表示水平力引起的耳板底部剪应力
                                //double YσFZLP2S2 = 0;//表示综合应力

                                //cosDH1 = (YS1LP2 * YS1LP2 + YL1LP2 * YL1LP2 - YH1LP2 * YH1LP2) / 2 / YS1LP2 / YL1LP2;
                                //cosDH2 = (YS2LP2 * YS2LP2 + YL2LP2 * YL2LP2 - YH2LP2 * YH2LP2) / 2 / YS2LP2 / YL2LP2;

                                //sinDH1 = Math.Sqrt(1 - cosDH1 * cosDH1);
                                //sinDH2 = Math.Sqrt(1 - cosDH2 * cosDH2);

                                //这里的算拉力和算弯矩的NFLP2及NVLP2的角度正余弦要更正，且要算两个最大值
                                //先算第一个
                                NFLP2S1 = NS1S * sinDH1;//表示水平力
                                NVLP2S1 = NS1S * cosDH1;//表示水平力
                                YσF1LP2S1 = NFLP2S1 * 1000 / YtLP2 / (2 * YeLP2 + 2 * YffLP2);//表示竖向力引起的耳板底部正应力
                                YσF2LP2S1 = NVLP2S1 * 1000 * (Yd0LP2 / 2 + YcLP2) / YtLP2 / (2 * YeLP2 + 2 * YffLP2) / (2 * YeLP2 + 2 * YffLP2) * 6;//表示弯矩引起的的耳板底部正应力
                                Yτ1LP2S1 = NVLP2S1 * 1000 / YtLP2 / (2 * YeLP2 + 2 * YffLP2);//表示水平力引起的耳板底部剪应力
                                YσFZLP2S1 = Math.Sqrt((YσF1LP2S1 + YσF2LP2S1) * (YσF1LP2S1 + YσF2LP2S1) + 3 * Yτ1LP2S1 * Yτ1LP2S1);

                                NFLP2S2 = NS2S * sinDH2;//表示水平力
                                NVLP2S2 = NS2S * cosDH2;//表示水平力
                                YσF1LP2S2 = NFLP2S2 * 1000 / YtLP2 / (2 * YeLP2 + 2 * YffLP2);//表示竖向力引起的耳板底部正应力
                                YσF2LP2S2 = NVLP2S2 * 1000 * (Yd0LP2 / 2 + YcLP2) / YtLP2 / (2 * YeLP2 + 2 * YffLP2) / (2 * YeLP2 + 2 * YffLP2) * 6;//表示弯矩引起的的耳板底部正应力
                                Yτ1LP2S2 = NVLP2S2 * 1000 / YtLP2 / (2 * YeLP2 + 2 * YffLP2);//表示水平力引起的耳板底部剪应力
                                YσFZLP2S2 = Math.Sqrt((YσF1LP2S2 + YσF2LP2S2) * (YσF1LP2S2 + YσF2LP2S2) + 3 * Yτ1LP2S2 * Yτ1LP2S2);




                                if (Math.Max(YσFZLP2S1, YσFZLP2S2) < (1.1 * YfLP2))
                                { FHLP2.Text = "σ" + ":" + Math.Max(YσFZLP2S1, YσFZLP2S2).ToString("#.##") + "<" + "1.1f" + ":" + (1.1 * YfLP2).ToString("#.#") + "," + "满足"; FHLP2.ForeColor = Color.Black; }
                                else
                                { FHLP2.Text = "σ" + ":" + Math.Max(YσFZLP2S1, YσFZLP2S2).ToString("#.##") + ">" + "1.1f" + ":" + (1.1 * YfLP2).ToString("#.#") + "," + "不满足"; FHLP2.ForeColor = Color.Red; }

                                if (Math.Max(YσF1LP2S1, YσF1LP2S2) < YfLP2)
                                { DJLP2.Text = "σ" + ":" + Math.Max(YσF1LP2S1, YσF1LP2S2).ToString("#.##") + "<" + "f" + ":" + YfLP2.ToString() + "," + "满足"; DJLP2.ForeColor = Color.Black; }
                                else
                                { DJLP2.Text = "σ" + ":" + Math.Max(YσF1LP2S2, YσF1LP2S2).ToString("#.##") + ">" + "f" + ":" + YfLP2.ToString() + "," + "不满足"; DJLP2.ForeColor = Color.Red; }
                            }
                        }
                    }
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 这里传入全部加上MQ1

            if (tGMQ1.Text == "") { YGMQ1 = 0; } else { YGMQ1 = Convert.ToDouble(tGMQ1.Text); }//




            if (taMQ1.Text == "") { YaMQ1 = 0; } else { YaMQ1 = Convert.ToDouble(taMQ1.Text); }//
            if (tbMQ1.Text == "") { YbMQ1 = 0; } else { YbMQ1 = Convert.ToDouble(tbMQ1.Text); }
            if (td0MQ1.Text == "") { Yd0MQ1 = 0; } else { Yd0MQ1 = Convert.ToDouble(td0MQ1.Text); }
            if (ttMQ1.Text == "") { YtMQ1 = 0; } else { YtMQ1 = Convert.ToDouble(ttMQ1.Text); }
            if (tcMQ1.Text == "") { YcMQ1 = 0; } else { YcMQ1 = Convert.ToDouble(tcMQ1.Text); }


            if (teMQ1.Text == "") { YeMQ1 = 0; } else { YeMQ1 = Convert.ToDouble(teMQ1.Text); }
            if (tfMQ1.Text == "") { YffMQ1 = 0; } else { YffMQ1 = Convert.ToDouble(tfMQ1.Text); }
            if (ttgMQ1.Text == "") { YgMQ1 = 0; } else { YgMQ1 = Convert.ToDouble(ttgMQ1.Text); }

            if (BoxDDMQ1.Text == "") { YDDMQ1 = 0; }
            else
            {
                Regex rx = new Regex("^(([0-9]+.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*.[0-9]+)|([0-9]*[1-9][0-9]*))$");
                if (rx.IsMatch(BoxDDMQ1.Text))
                {
                    YDDMQ1 = Convert.ToDouble(BoxDDMQ1.Text);
                }
                else
                {
                    YDDMQ1 = 0;
                }
            }

            //钢丝绳拉力计算,钢丝绳选型用分股标准值，用于吊耳计算的是合股的设计值
            if (YGMQ1 < 0) { YGMQ1 = -1 * YGMQ1; }

            //YSθMQ1 = YHMQ1 / Math.Sqrt(YL1MQ1 / 2 * YL1MQ1 / 2 + YL2MQ1 / 2 * YL2MQ1 / 2 + YHMQ1 * YHMQ1);

            YN2MQ1 = YGMQ1 * 10 * YDDMQ1;//分股标准值，动力系数1.3//动力系数改为YDDMQ1
            YN3MQ1 = YGMQ1 * 10 * YDDMQ1;//合股标准值，动力系数1.3//动力系数改为YDDMQ1

            YNMQ1 = YGMQ1 * 10 * YDDMQ1 * 1.5;//合股设计值，动力系数1.3//动力系数改为YDDMQ1



            //钢丝绳容许应力计算
            if (BOXSMQ1.Text == "" || BoxXMQ1.Text == "" || BoxZJMQ1.Text == "") { MessageBox.Show("请输全钢丝绳信息"); }
            else
            {
                switch (BoxXMQ1.Text)
                {
                    case "6x19":
                        αMQ1 = 0.85;
                        break;

                    case "6x37":
                        αMQ1 = 0.82;
                        break;

                    case "6x61":
                        αMQ1 = 0.80;
                        break;
                }

                GssMQ1 = BOXSMQ1.Text + " " + BoxXMQ1.Text + " " + BoxZJMQ1.Text;


                switch (GssMQ1)
                {
                    //6x19 1400MPa列
                    case "1400MPa 6x19 6.2":
                        FgMQ1 = 20.0;
                        break;
                    case "1400MPa 6x19 7.7":
                        FgMQ1 = 31.3;
                        break;
                    case "1400MPa 6x19 9.3":
                        FgMQ1 = 45.1;
                        break;
                    case "1400MPa 6x19 11.0":
                        FgMQ1 = 61.3;
                        break;
                    case "1400MPa 6x19 12.5":
                        FgMQ1 = 80.1;
                        break;
                    case "1400MPa 6x19 14.0":
                        FgMQ1 = 101.0;
                        break;
                    case "1400MPa 6x19 15.5":
                        FgMQ1 = 125.0;
                        break;
                    case "1400MPa 6x19 17.0":
                        FgMQ1 = 151.5;
                        break;
                    case "1400MPa 6x19 18.5":
                        FgMQ1 = 180.0;
                        break;
                    case "1400MPa 6x19 20.0":
                        FgMQ1 = 211.5;
                        break;
                    case "1400MPa 6x19 21.5":
                        FgMQ1 = 245.5;
                        break;
                    case "1400MPa 6x19 23.0":
                        FgMQ1 = 281.5;
                        break;
                    case "1400MPa 6x19 24.5":
                        FgMQ1 = 320.5;
                        break;
                    case "1400MPa 6x19 26.0":
                        FgMQ1 = 362.0;
                        break;
                    case "1400MPa 6x19 28.0":
                        FgMQ1 = 405.5;
                        break;
                    case "1400MPa 6x19 31.0":
                        FgMQ1 = 501.0;
                        break;
                    case "1400MPa 6x19 34.0":
                        FgMQ1 = 606.0;
                        break;
                    case "1400MPa 6x19 37.0":
                        FgMQ1 = 721.5;
                        break;
                    case "1400MPa 6x19 40.0":
                        FgMQ1 = 846.5;
                        break;
                    case "1400MPa 6x19 43":
                        FgMQ1 = 982.0;
                        break;
                    case "1400MPa 6x19 46":
                        FgMQ1 = 1125.0;
                        break;


                    //6x19 1550MPa列
                    case "1550MPa 6x19 6.2":
                        FgMQ1 = 22.1;
                        break;
                    case "1550MPa 6x19 7.7":
                        FgMQ1 = 34.6;
                        break;
                    case "1550MPa 6x19 9.3":
                        FgMQ1 = 49.9;
                        break;
                    case "1550MPa 6x19 11.0":
                        FgMQ1 = 67.9;
                        break;
                    case "1550MPa 6x19 12.5":
                        FgMQ1 = 88.7;
                        break;
                    case "1550MPa 6x19 14.0":
                        FgMQ1 = 112.0;
                        break;
                    case "1550MPa 6x19 15.5":
                        FgMQ1 = 138.5;
                        break;
                    case "1550MPa 6x19 17.0":
                        FgMQ1 = 167.5;
                        break;
                    case "1550MPa 6x19 18.5":
                        FgMQ1 = 199.5;
                        break;
                    case "1550MPa 6x19 20.0":
                        FgMQ1 = 234.0;
                        break;
                    case "1550MPa 6x19 21.5":
                        FgMQ1 = 271.5;
                        break;
                    case "1550MPa 6x19 23.0":
                        FgMQ1 = 312.0;
                        break;
                    case "1550MPa 6x19 24.5":
                        FgMQ1 = 355.0;
                        break;
                    case "1550MPa 6x19 26.0":
                        FgMQ1 = 400.5;
                        break;
                    case "1550MPa 6x19 28.0":
                        FgMQ1 = 449.0;
                        break;
                    case "1550MPa 6x19 31.0":
                        FgMQ1 = 554.5;
                        break;
                    case "1550MPa 6x19 34.0":
                        FgMQ1 = 671.0;
                        break;
                    case "1550MPa 6x19 37.0":
                        FgMQ1 = 798.5;
                        break;
                    case "1550MPa 6x19 40.0":
                        FgMQ1 = 937.5;
                        break;
                    case "1550MPa 6x19 43":
                        FgMQ1 = 1085.0;
                        break;
                    case "1550MPa 6x19 46":
                        FgMQ1 = 1245.0;
                        break;


                    //6x19 1700MPa列
                    case "1700MPa 6x19 6.2":
                        FgMQ1 = 24.3;
                        break;
                    case "1700MPa 6x19 7.7":
                        FgMQ1 = 38.0;
                        break;
                    case "1700MPa 6x19 9.3":
                        FgMQ1 = 54.7;
                        break;
                    case "1700MPa 6x19 11.0":
                        FgMQ1 = 74.5;
                        break;
                    case "1700MPa 6x19 12.5":
                        FgMQ1 = 97.3;
                        break;
                    case "1700MPa 6x19 14.0":
                        FgMQ1 = 123.0;
                        break;
                    case "1700MPa 6x19 15.5":
                        FgMQ1 = 152.0;
                        break;
                    case "1700MPa 6x19 17.0":
                        FgMQ1 = 184.0;
                        break;
                    case "1700MPa 6x19 18.5":
                        FgMQ1 = 219.0;
                        break;
                    case "1700MPa 6x19 20.0":
                        FgMQ1 = 257.0;
                        break;
                    case "1700MPa 6x19 21.5":
                        FgMQ1 = 298.0;
                        break;
                    case "1700MPa 6x19 23.0":
                        FgMQ1 = 342.0;
                        break;
                    case "1700MPa 6x19 24.5":
                        FgMQ1 = 389.0;
                        break;
                    case "1700MPa 6x19 26.0":
                        FgMQ1 = 439.0;
                        break;
                    case "1700MPa 6x19 28.0":
                        FgMQ1 = 492.5;
                        break;
                    case "1700MPa 6x19 31.0":
                        FgMQ1 = 608.5;
                        break;
                    case "1700MPa 6x19 34.0":
                        FgMQ1 = 736.0;
                        break;
                    case "1700MPa 6x19 37.0":
                        FgMQ1 = 876.0;
                        break;
                    case "1700MPa 6x19 40.0":
                        FgMQ1 = 1025.0;
                        break;
                    case "1700MPa 6x19 43":
                        FgMQ1 = 1190.0;
                        break;
                    case "1700MPa 6x19 46":
                        FgMQ1 = 1365.0;
                        break;

                    //6x19 1850MPa列
                    case "1850MPa 6x19 6.2":
                        FgMQ1 = 26.3;
                        break;
                    case "1850MPa 6x19 7.7":
                        FgMQ1 = 41.3;
                        break;
                    case "1850MPa 6x19 9.3":
                        FgMQ1 = 59.6;
                        break;
                    case "1850MPa 6x19 11.0":
                        FgMQ1 = 81.1;
                        break;
                    case "1850MPa 6x19 12.5":
                        FgMQ1 = 105.5;
                        break;
                    case "1850MPa 6x19 14.0":
                        FgMQ1 = 134.0;
                        break;
                    case "1850MPa 6x19 15.5":
                        FgMQ1 = 165.5;
                        break;
                    case "1850MPa 6x19 17.0":
                        FgMQ1 = 200.0;
                        break;
                    case "1850MPa 6x19 18.5":
                        FgMQ1 = 238.0;
                        break;
                    case "1850MPa 6x19 20.0":
                        FgMQ1 = 279.5;
                        break;
                    case "1850MPa 6x19 21.5":
                        FgMQ1 = 324.5;
                        break;
                    case "1850MPa 6x19 23.0":
                        FgMQ1 = 372.5;
                        break;
                    case "1850MPa 6x19 24.5":
                        FgMQ1 = 423.5;
                        break;
                    case "1850MPa 6x19 26.0":
                        FgMQ1 = 478.0;
                        break;
                    case "1850MPa 6x19 28.0":
                        FgMQ1 = 536.0;
                        break;
                    case "1850MPa 6x19 31.0":
                        FgMQ1 = 662.0;
                        break;
                    case "1850MPa 6x19 34.0":
                        FgMQ1 = 801.0;
                        break;
                    case "1850MPa 6x19 37.0":
                        FgMQ1 = 953.5;
                        break;
                    case "1850MPa 6x19 40.0":
                        FgMQ1 = 1115.0;
                        break;
                    case "1850MPa 6x19 43":
                        FgMQ1 = 1295.0;
                        break;
                    case "1850MPa 6x19 46":
                        FgMQ1 = 1490.0;
                        break;

                    //6x37 1400MPa列//
                    case "1400MPa 6x37 8.7":
                        FgMQ1 = 39.0;
                        break;
                    case "1400MPa 6x37 11.0":
                        FgMQ1 = 60.9;
                        break;
                    case "1400MPa 6x37 13.0":
                        FgMQ1 = 87.8;
                        break;
                    case "1400MPa 6x37 15.0":
                        FgMQ1 = 119.5;
                        break;
                    case "1400MPa 6x37 17.5":
                        FgMQ1 = 156.0;
                        break;
                    case "1400MPa 6x37 19.5":
                        FgMQ1 = 197.5;
                        break;
                    case "1400MPa 6x37 21.5":
                        FgMQ1 = 243.5;
                        break;
                    case "1400MPa 6x37 24.0":
                        FgMQ1 = 295.0;
                        break;
                    case "1400MPa 6x37 26.0":
                        FgMQ1 = 351.0;
                        break;
                    case "1400MPa 6x37 28.0":
                        FgMQ1 = 412.0;
                        break;
                    case "1400MPa 6x37 30.0":
                        FgMQ1 = 478.0;
                        break;
                    case "1400MPa 6x37 32.5":
                        FgMQ1 = 522.5;
                        break;
                    case "1400MPa 6x37 34.5":
                        FgMQ1 = 624.5;
                        break;
                    case "1400MPa 6x37 36.5":
                        FgMQ1 = 705.0;
                        break;
                    case "1400MPa 6x37 39.0":
                        FgMQ1 = 790.0;
                        break;
                    case "1400MPa 6x37 43.0":
                        FgMQ1 = 975.5;
                        break;
                    case "1400MPa 6x37 47.5":
                        FgMQ1 = 1180.0;
                        break;
                    case "1400MPa 6x37 52.0":
                        FgMQ1 = 1450.0;
                        break;
                    case "1400MPa 6x37 56.0":
                        FgMQ1 = 1645.0;
                        break;
                    case "1400MPa 6x37 60.5":
                        FgMQ1 = 1910.0;
                        break;
                    case "1400MPa 6x37 65.0":
                        FgMQ1 = 2195.0;
                        break;


                    //6x37 1550MPa列
                    case "1550MPa 6x37 8.7":
                        FgMQ1 = 43.2;
                        break;
                    case "1550MPa 6x37 11.0":
                        FgMQ1 = 67.5;
                        break;
                    case "1550MPa 6x37 13.0":
                        FgMQ1 = 97.2;
                        break;
                    case "1550MPa 6x37 15.0":
                        FgMQ1 = 132.0;
                        break;
                    case "1550MPa 6x37 17.5":
                        FgMQ1 = 172.5;
                        break;
                    case "1550MPa 6x37 19.5":
                        FgMQ1 = 213.5;
                        break;
                    case "1550MPa 6x37 21.5":
                        FgMQ1 = 270.0;
                        break;
                    case "1550MPa 6x37 24.0":
                        FgMQ1 = 326.5;
                        break;
                    case "1550MPa 6x37 26.0":
                        FgMQ1 = 388.5;
                        break;
                    case "1550MPa 6x37 28.0":
                        FgMQ1 = 456.5;
                        break;
                    case "1550MPa 6x37 30.0":
                        FgMQ1 = 529.0;
                        break;
                    case "1550MPa 6x37 32.5":
                        FgMQ1 = 607.5;
                        break;
                    case "1550MPa 6x37 34.5":
                        FgMQ1 = 691.5;
                        break;
                    case "1550MPa 6x37 36.5":
                        FgMQ1 = 780.5;
                        break;
                    case "1550MPa 6x37 39.0":
                        FgMQ1 = 875.0;
                        break;
                    case "1550MPa 6x37 43.0":
                        FgMQ1 = 1080.0;
                        break;
                    case "1550MPa 6x37 47.5":
                        FgMQ1 = 1305.0;
                        break;
                    case "1550MPa 6x37 52.0":
                        FgMQ1 = 1555.0;
                        break;
                    case "1550MPa 6x37 56.0":
                        FgMQ1 = 1825.0;
                        break;
                    case "1550MPa 6x37 60.5":
                        FgMQ1 = 2115.0;
                        break;
                    case "1550MPa 6x37 65.0":
                        FgMQ1 = 2430.0;
                        break;


                    //6x37 1700MPa列
                    case "1700MPa 6x37 8.7":
                        FgMQ1 = 47.3;
                        break;
                    case "1700MPa 6x37 11.0":
                        FgMQ1 = 74.0;
                        break;
                    case "1700MPa 6x37 13.0":
                        FgMQ1 = 106.5;
                        break;
                    case "1700MPa 6x37 15.0":
                        FgMQ1 = 145.0;
                        break;
                    case "1700MPa 6x37 17.5":
                        FgMQ1 = 189.5;
                        break;
                    case "1700MPa 6x37 19.5":
                        FgMQ1 = 239.5;
                        break;
                    case "1700MPa 6x37 21.5":
                        FgMQ1 = 296.0;
                        break;
                    case "1700MPa 6x37 24.0":
                        FgMQ1 = 358.0;
                        break;
                    case "1700MPa 6x37 26.0":
                        FgMQ1 = 426.5;
                        break;
                    case "1700MPa 6x37 28.0":
                        FgMQ1 = 500.5;
                        break;
                    case "1700MPa 6x37 30.0":
                        FgMQ1 = 580.5;
                        break;
                    case "1700MPa 6x37 32.5":
                        FgMQ1 = 666.5;
                        break;
                    case "1700MPa 6x37 34.5":
                        FgMQ1 = 758.5;
                        break;
                    case "1700MPa 6x37 36.5":
                        FgMQ1 = 856.0;
                        break;
                    case "1700MPa 6x37 39.0":
                        FgMQ1 = 959.5;
                        break;
                    case "1700MPa 6x37 43.0":
                        FgMQ1 = 1185.0;
                        break;
                    case "1700MPa 6x37 47.5":
                        FgMQ1 = 1430.0;
                        break;
                    case "1700MPa 6x37 52.0":
                        FgMQ1 = 1705.0;
                        break;
                    case "1700MPa 6x37 56.0":
                        FgMQ1 = 2000.0;
                        break;
                    case "1700MPa 6x37 60.5":
                        FgMQ1 = 2320.0;
                        break;
                    case "1700MPa 6x37 65.0":
                        FgMQ1 = 2665.0;
                        break;



                    //6x37 1850MPa列
                    case "1850MPa 6x37 8.7":
                        FgMQ1 = 51.5;
                        break;
                    case "1850MPa 6x37 11.0":
                        FgMQ1 = 80.6;
                        break;
                    case "1850MPa 6x37 13.0":
                        FgMQ1 = 116.5;
                        break;
                    case "1850MPa 6x37 15.0":
                        FgMQ1 = 157.5;
                        break;
                    case "1850MPa 6x37 17.5":
                        FgMQ1 = 206.0;
                        break;
                    case "1850MPa 6x37 19.5":
                        FgMQ1 = 261.0;
                        break;
                    case "1850MPa 6x37 21.5":
                        FgMQ1 = 322.0;
                        break;
                    case "1850MPa 6x37 24.0":
                        FgMQ1 = 390.0;
                        break;
                    case "1850MPa 6x37 26.0":
                        FgMQ1 = 464.0;
                        break;
                    case "1850MPa 6x37 28.0":
                        FgMQ1 = 544.5;
                        break;
                    case "1850MPa 6x37 30.0":
                        FgMQ1 = 631.5;
                        break;
                    case "1850MPa 6x37 32.5":
                        FgMQ1 = 725.0;
                        break;
                    case "1850MPa 6x37 34.5":
                        FgMQ1 = 758.0;
                        break;
                    case "1850MPa 6x37 36.5":
                        FgMQ1 = 856.0;
                        break;
                    case "1850MPa 6x37 39.0":
                        FgMQ1 = 959.5;
                        break;
                    case "1850MPa 6x37 43.0":
                        FgMQ1 = 1185.0;
                        break;
                    case "1850MPa 6x37 47.5":
                        FgMQ1 = 1430.0;
                        break;
                    case "1850MPa 6x37 52.0":
                        FgMQ1 = 1705.0;
                        break;
                    case "1850MPa 6x37 56.0":
                        FgMQ1 = 2000.0;
                        break;
                    case "1850MPa 6x37 60.5":
                        FgMQ1 = 2320.0;
                        break;
                    case "1850MPa 6x37 65.0":
                        FgMQ1 = 2665.0;
                        break;



                    //6x61 1400MPa列
                    case "1400MPa 6x61 11.0":
                        FgMQ1 = 64.3;
                        break;
                    case "1400MPa 6x61 14.0":
                        FgMQ1 = 100.5;
                        break;
                    case "1400MPa 6x61 16.5":
                        FgMQ1 = 144.5;
                        break;
                    case "1400MPa 6x61 19.5":
                        FgMQ1 = 197.0;
                        break;
                    case "1400MPa 6x61 22.0":
                        FgMQ1 = 257.0;
                        break;
                    case "1400MPa 6x61 25.0":
                        FgMQ1 = 325.5;
                        break;
                    case "1400MPa 6x61 27.5":
                        FgMQ1 = 402.0;
                        break;
                    case "1400MPa 6x61 30.5":
                        FgMQ1 = 226.5;
                        break;
                    case "1400MPa 6x61 33.0":
                        FgMQ1 = 579.0;
                        break;
                    case "1400MPa 6x61 36.0":
                        FgMQ1 = 679.5;
                        break;
                    case "1400MPa 6x61 38.5":
                        FgMQ1 = 788.0;
                        break;
                    case "1400MPa 6x61 41.5":
                        FgMQ1 = 905.0;
                        break;
                    case "1400MPa 6x61 44.0":
                        FgMQ1 = 1025.0;
                        break;
                    case "1400MPa 6x61 47.0":
                        FgMQ1 = 1160.0;
                        break;
                    case "1400MPa 6x61 50.0":
                        FgMQ1 = 1300.0;
                        break;
                    case "1400MPa 6x61 55.5":
                        FgMQ1 = 1605.0;
                        break;
                    case "1400MPa 6x61 61.0":
                        FgMQ1 = 1945.0;
                        break;
                    case "1400MPa 6x61 66.5":
                        FgMQ1 = 2315.0;
                        break;
                    case "1400MPa 6x61 72.0":
                        FgMQ1 = 2715.5;
                        break;
                    case "1400MPa 6x61 77.5":
                        FgMQ1 = 3150.0;
                        break;
                    case "1400MPa 6x61 83.0":
                        FgMQ1 = 3620.0;
                        break;



                    //6x61 1550MPa列
                    case "1550MPa 6x61 11.0":
                        FgMQ1 = 71.2;
                        break;
                    case "1550MPa 6x61 14.0":
                        FgMQ1 = 111.0;
                        break;
                    case "1550MPa 6x61 16.5":
                        FgMQ1 = 160.0;
                        break;
                    case "1550MPa 6x61 19.5":
                        FgMQ1 = 218.0;
                        break;
                    case "1550MPa 6x61 22.0":
                        FgMQ1 = 285.0;
                        break;
                    case "1550MPa 6x61 25.0":
                        FgMQ1 = 360.5;
                        break;
                    case "1550MPa 6x61 27.5":
                        FgMQ1 = 445.0;
                        break;
                    case "1550MPa 6x61 30.5":
                        FgMQ1 = 538.5;
                        break;
                    case "1550MPa 6x61 33.0":
                        FgMQ1 = 641.0;
                        break;
                    case "1550MPa 6x61 36.0":
                        FgMQ1 = 752.5;
                        break;
                    case "1550MPa 6x61 38.5":
                        FgMQ1 = 872.5;
                        break;
                    case "1550MPa 6x61 41.5":
                        FgMQ1 = 1000.0;
                        break;
                    case "1550MPa 6x61 44.0":
                        FgMQ1 = 1140.0;
                        break;
                    case "1550MPa 6x61 47.0":
                        FgMQ1 = 1285.0;
                        break;
                    case "1550MPa 6x61 50.0":
                        FgMQ1 = 1440.0;
                        break;
                    case "1550MPa 6x61 55.5":
                        FgMQ1 = 1780.0;
                        break;
                    case "1550MPa 6x61 61.0":
                        FgMQ1 = 2155.0;
                        break;
                    case "1550MPa 6x61 66.5":
                        FgMQ1 = 2565.0;
                        break;
                    case "1550MPa 6x61 72.0":
                        FgMQ1 = 3010.0;
                        break;
                    case "1550MPa 6x61 77.5":
                        FgMQ1 = 3490.0;
                        break;
                    case "1550MPa 6x61 83.0":
                        FgMQ1 = 4005.0;
                        break;


                    //6x61 1700MPa列
                    case "1700MPa 6x61 11.0":
                        FgMQ1 = 78.1;
                        break;
                    case "1700MPa 6x61 14.0":
                        FgMQ1 = 122.0;
                        break;
                    case "1700MPa 6x61 16.5":
                        FgMQ1 = 175.5;
                        break;
                    case "1700MPa 6x61 19.5":
                        FgMQ1 = 239.0;
                        break;
                    case "1700MPa 6x61 22.0":
                        FgMQ1 = 312.5;
                        break;
                    case "1700MPa 6x61 25.0":
                        FgMQ1 = 395.5;
                        break;
                    case "1700MPa 6x61 27.5":
                        FgMQ1 = 228.0;
                        break;
                    case "1700MPa 6x61 30.5":
                        FgMQ1 = 591.0;
                        break;
                    case "1700MPa 6x61 33.0":
                        FgMQ1 = 703.0;
                        break;
                    case "1700MPa 6x61 36.0":
                        FgMQ1 = 825.0;
                        break;
                    case "1700MPa 6x61 38.5":
                        FgMQ1 = 957.0;
                        break;
                    case "1700MPa 6x61 41.5":
                        FgMQ1 = 1095.0;
                        break;
                    case "1700MPa 6x61 44.0":
                        FgMQ1 = 1250.0;
                        break;
                    case "1700MPa 6x61 47.0":
                        FgMQ1 = 1410.0;
                        break;
                    case "1700MPa 6x61 50.0":
                        FgMQ1 = 1580.0;
                        break;
                    case "1700MPa 6x61 55.5":
                        FgMQ1 = 1950.0;
                        break;
                    case "1700MPa 6x61 61.0":
                        FgMQ1 = 2360.0;
                        break;
                    case "1700MPa 6x61 66.5":
                        FgMQ1 = 2810.0;
                        break;
                    case "1700MPa 6x61 72.0":
                        FgMQ1 = 3300.0;
                        break;
                    case "1700MPa 6x61 77.5":
                        FgMQ1 = 3825.0;
                        break;
                    case "1700MPa 6x61 83.0":
                        FgMQ1 = 4395.0;
                        break;



                    //6x61 1850MPa列
                    case "1850MPa 6x61 11.0":
                        FgMQ1 = 85.0;
                        break;
                    case "1850MPa 6x61 14.0":
                        FgMQ1 = 132.0;
                        break;
                    case "1850MPa 6x61 16.5":
                        FgMQ1 = 191.0;
                        break;
                    case "1850MPa 6x61 19.5":
                        FgMQ1 = 260.0;
                        break;
                    case "1850MPa 6x61 22.0":
                        FgMQ1 = 340.0;
                        break;
                    case "1850MPa 6x61 25.0":
                        FgMQ1 = 430.5;
                        break;
                    case "1850MPa 6x61 27.5":
                        FgMQ1 = 531.5;
                        break;
                    case "1850MPa 6x61 30.5":
                        FgMQ1 = 643.0;
                        break;
                    case "1850MPa 6x61 33.0":
                        FgMQ1 = 765.0;
                        break;
                    case "1850MPa 6x61 36.0":
                        FgMQ1 = 898.0;
                        break;
                    case "1850MPa 6x61 38.5":
                        FgMQ1 = 1040.0;
                        break;
                    case "1850MPa 6x61 41.5":
                        FgMQ1 = 1195.0;
                        break;
                    case "1850MPa 6x61 44.0":
                        FgMQ1 = 1360.0;
                        break;
                    case "1850MPa 6x61 47.0":
                        FgMQ1 = 1535.0;
                        break;
                    case "1850MPa 6x61 50.0":
                        FgMQ1 = 1720.0;
                        break;
                    case "1850MPa 6x61 55.5":
                        FgMQ1 = 2125.0;
                        break;
                    case "1850MPa 6x61 61.0":
                        FgMQ1 = 2570.0;
                        break;
                    case "1850MPa 6x61 66.5":
                        FgMQ1 = 3060.0;
                        break;
                    case "1850MPa 6x61 72.0":
                        FgMQ1 = 3590.0;
                        break;
                    case "1850MPa 6x61 77.5":
                        FgMQ1 = 4165.0;
                        break;
                    case "1850MPa 6x61 83.0":
                        FgMQ1 = 4780.0;
                        break;

                }

                if (BoxQX1MQ1.Text == "") { KMQ1 = 0; }
                else
                {
                    KMQ1 = Convert.ToDouble(BoxQX1MQ1.Text);
                }





                //这是卡环的承载力
                if (BoxKHMQ1.Text == "") { MessageBox.Show("请输入卡环型号"); }
                else
                {
                    switch (BoxKHMQ1.Text)
                    {
                        case "0.2":
                            rFhMQ1 = 2.45;
                            break;
                        case "0.4":
                            rFhMQ1 = 3.92;
                            break;
                        case "0.6":
                            rFhMQ1 = 5.88;
                            break;
                        case "0.9":
                            rFhMQ1 = 8.82;
                            break;
                        case "1.2":
                            rFhMQ1 = 12.25;
                            break;
                        case "1.7":
                            rFhMQ1 = 17.15;
                            break;
                        case "2.1":
                            rFhMQ1 = 20.58;
                            break;
                        case "2.7":
                            rFhMQ1 = 26.95;
                            break;
                        case "3.5":
                            rFhMQ1 = 34.30;
                            break;
                        case "4.5":
                            rFhMQ1 = 44.10;
                            break;
                        case "6.0":
                            rFhMQ1 = 58.80;
                            break;
                        case "7.5":
                            rFhMQ1 = 73.50;
                            break;
                        case "9.5":
                            rFhMQ1 = 93.10;
                            break;
                        case "11.0":
                            rFhMQ1 = 107.80;
                            break;
                        case "14.0":
                            rFhMQ1 = 137.20;
                            break;
                        case "17.5":
                            rFhMQ1 = 171.50;
                            break;
                        case "21.0":
                            rFhMQ1 = 205.80;
                            break;
                    }





                    switch (BoxBMQ1.Text)
                    {
                        case "Q235B":
                            if (0 < YtMQ1 && YtMQ1 <= 16)
                            { YfMQ1 = 215; YfvMQ1 = 125; YfcMQ1 = 405; }
                            else if (YtMQ1 > 16 && YtMQ1 <= 40)
                            { YfMQ1 = 205; YfvMQ1 = 120; YfcMQ1 = 405; }
                            else if (YtMQ1 > 40 && YtMQ1 <= 100)
                            { YfMQ1 = 200; YfvMQ1 = 115; YfcMQ1 = 405; }
                            else if (YtMQ1 < 0 || YtMQ1 > 100)
                            { YfMQ1 = 1; YfvMQ1 = 1; }
                            break;
                        case "Q355B":
                            if (0 < YtMQ1 && YtMQ1 <= 16)
                            { YfMQ1 = 305; YfvMQ1 = 175; YfcMQ1 = 510; }
                            else if (YtMQ1 > 16 && YtMQ1 <= 40)
                            { YfMQ1 = 295; YfvMQ1 = 170; YfcMQ1 = 510; }
                            else if (YtMQ1 > 40 && YtMQ1 <= 63)
                            { YfMQ1 = 290; YfvMQ1 = 165; YfcMQ1 = 510; }
                            else if (YtMQ1 > 63 && YtMQ1 <= 80)
                            { YfMQ1 = 280; YfvMQ1 = 160; YfcMQ1 = 510; }
                            else if (YtMQ1 > 80 && YtMQ1 <= 100)
                            { YfMQ1 = 270; YfvMQ1 = 155; YfcMQ1 = 510; }
                            else if (YtMQ1 < 0 || YtMQ1 > 100)
                            { YfMQ1 = 1; YfvMQ1 = 1; }
                            break;
                        case "Q390B":
                            if (0 < YtMQ1 && YtMQ1 <= 16)
                            { YfMQ1 = 345; YfvMQ1 = 200; YfcMQ1 = 530; }
                            else if (YtMQ1 > 16 && YtMQ1 <= 40)
                            { YfMQ1 = 330; YfvMQ1 = 190; YfcMQ1 = 530; }
                            else if (YtMQ1 > 40 && YtMQ1 <= 63)
                            { YfMQ1 = 310; YfvMQ1 = 180; YfcMQ1 = 530; }
                            else if (YtMQ1 > 63 && YtMQ1 <= 100)
                            { YfMQ1 = 295; YfvMQ1 = 170; YfcMQ1 = 530; }
                            else if (YtMQ1 <= 0 || YtMQ1 > 100)
                            { YfMQ1 = 1; YfvMQ1 = 1; }
                            break;
                        case "Q420B":
                            if (0 < YtMQ1 && YtMQ1 <= 16)
                            { YfMQ1 = 375; YfvMQ1 = 215; YfcMQ1 = 560; }
                            else if (YtMQ1 > 16 && YtMQ1 <= 40)
                            { YfMQ1 = 355; YfvMQ1 = 205; YfcMQ1 = 560; }
                            else if (YtMQ1 > 40 && YtMQ1 <= 63)
                            { YfMQ1 = 320; YfvMQ1 = 185; YfcMQ1 = 560; }
                            else if (YtMQ1 > 63 && YtMQ1 <= 100)
                            { YfMQ1 = 305; YfvMQ1 = 175; YfcMQ1 = 560; }
                            else if (YtMQ1 <= 0 || YtMQ1 > 100)
                            { YfMQ1 = 1; YfvMQ1 = 1; }
                            break;
                    }
                    YbeMQ1 = (2 * YtMQ1 + 16);
                    Yb1MQ1 = Math.Min((2 * YtMQ1 + 16), (YbMQ1 - Yd0MQ1 / 3));
                    if (BoxBMQ1.Text == "")
                    { MessageBox.Show("请选择耳板材质"); GSSLLMQ1.Text = "暂时不知道"; GSSRXLLMQ1.Text = "暂时不知道"; KHFHMQ1.Text = "暂时不知道"; GSSLLsMQ1.Text = "暂时不知道"; GZMQ1.Text = "暂时不知道"; JKLMQ1.Text = "暂时不知道"; PKMQ1.Text = "暂时不知道"; KJMQ1.Text = "暂时不知道"; FHMQ1.Text = "暂时不知道"; }
                    else if (YfMQ1 == 1)
                    { MessageBox.Show("厚度t一般大于0mm且小于100mm,重新输入看看吧"); GSSLLMQ1.Text = "暂时不知道"; GSSRXLLMQ1.Text = "暂时不知道"; KHFHMQ1.Text = "暂时不知道"; GSSLLsMQ1.Text = "暂时不知道"; GZMQ1.Text = "暂时不知道"; JKLMQ1.Text = "暂时不知道"; PKMQ1.Text = "暂时不知道"; KJMQ1.Text = "暂时不知道"; FHMQ1.Text = "暂时不知道"; }
                    else if (YDDMQ1 <= 0)
                    { MessageBox.Show("动力系数输入有误"); GSSLLMQ1.Text = "暂时不知道"; GSSRXLLMQ1.Text = "暂时不知道"; KHFHMQ1.Text = "暂时不知道"; GSSLLsMQ1.Text = "暂时不知道"; GZMQ1.Text = "暂时不知道"; JKLMQ1.Text = "暂时不知道"; PKMQ1.Text = "暂时不知道"; KJMQ1.Text = "暂时不知道"; FHMQ1.Text = "暂时不知道"; }
                    else if (KMQ1 <= 0)
                    { MessageBox.Show("安全系数输入有误"); GSSLLMQ1.Text = "暂时不知道"; GSSRXLLMQ1.Text = "暂时不知道"; KHFHMQ1.Text = "暂时不知道"; GSSLLsMQ1.Text = "暂时不知道"; GZMQ1.Text = "暂时不知道"; JKLMQ1.Text = "暂时不知道"; PKMQ1.Text = "暂时不知道"; KJMQ1.Text = "暂时不知道"; FHMQ1.Text = "暂时不知道"; }
                    else if (YGMQ1 <= 0 || YaMQ1 <= 0 || YbMQ1 <= 0 || Yd0MQ1 <= 0 || YcMQ1 <= 0 || YeMQ1 <= 0 || YffMQ1 <= 0 || YgMQ1 <= 0)
                    { MessageBox.Show("实际构件尺寸一般为正值,实际重量一般不等于0"); GSSLLMQ1.Text = "暂时不知道"; GSSRXLLMQ1.Text = "暂时不知道"; KHFHMQ1.Text = "暂时不知道"; GSSLLsMQ1.Text = "暂时不知道"; GZMQ1.Text = "暂时不知道"; JKLMQ1.Text = "暂时不知道"; PKMQ1.Text = "暂时不知道"; KJMQ1.Text = "暂时不知道"; FHMQ1.Text = "暂时不知道"; }
                    //if (tGMQ1.Text == "") { YGMQ1 = 0; } else { YGMQ1 = Convert.ToDouble(tGMQ1.Text); }//

                    //if (tsgMQ1.Text == "") { YsgMQ1 = 0; } else { YsgMQ1 = Convert.ToDouble(tsgMQ1.Text); }//


                    //if (taMQ1.Text == "") { YaMQ1 = 0; } else { YaMQ1 = Convert.ToDouble(taMQ1.Text); }//
                    //if (tbMQ1.Text == "") { YbMQ1 = 0; } else { YbMQ1 = Convert.ToDouble(tbMQ1.Text); }
                    //if (td0MQ1.Text == "") { Yd0MQ1 = 0; } else { Yd0MQ1 = Convert.ToDouble(td0MQ1.Text); }
                    //if (ttMQ1.Text == "") { YtMQ1 = 0; } else { YtMQ1 = Convert.ToDouble(ttMQ1.Text); }
                    //if (tcMQ1.Text == "") { YcMQ1 = 0; } else { YcMQ1 = Convert.ToDouble(tcMQ1.Text); }


                    //if (teMQ1.Text == "") { YeMQ1 = 0; } else { YeMQ1 = Convert.ToDouble(teMQ1.Text); }
                    //if (tfMQ1.Text == "") { YffMQ1 = 0; } else { YffMQ1 = Convert.ToDouble(tfMQ1.Text); }
                    //if (ttgMQ1.Text == "") { YgMQ1 = 0; } else { YgMQ1 = Convert.ToDouble(ttgMQ1.Text); }


                    else
                    {
                        rFgMQ1 = αMQ1 * FgMQ1 / KMQ1;
                        GSSLLMQ1.Text = YN2MQ1.ToString("#.##");//分股标准值
                        GSSLLsMQ1.Text = YNMQ1.ToString("#.##");//合股设计值
                        //那两个放进来
                        if (rFgMQ1 < YN2MQ1)
                        { GSSRXLLMQ1.Text = rFgMQ1.ToString("#.##") + "，" + "钢丝绳不满足要求"; GSSRXLLMQ1.ForeColor = Color.Red; }
                        else
                        { GSSRXLLMQ1.Text = rFgMQ1.ToString("#.##") + "，" + "钢丝绳满足要求"; GSSRXLLMQ1.ForeColor = Color.Black; }
                        if (rFhMQ1 < YN3MQ1)
                        { KHFHMQ1.Text = rFhMQ1.ToString("#.##") + "<" + YN2MQ1.ToString("#.##") + "，" + "卡环不满足要求"; KHFHMQ1.ForeColor = Color.Red; }
                        else
                        { KHFHMQ1.Text = rFhMQ1.ToString("#.##") + "，" + "卡环满足要求"; KHFHMQ1.ForeColor = Color.Black; }



                        if (YaMQ1 < (YbeMQ1 / 3 * 4)) { GZMQ1.Text = "a" + ":" + YaMQ1.ToString() + "< " + "4/3be" + ":" + (YbeMQ1 * 4 / 3).ToString("#.##") + "," + "构造不满足"; GZMQ1.ForeColor = Color.Red; }
                        else if (YbMQ1 < YbeMQ1) { GZMQ1.Text = "b" + ":" + YbMQ1.ToString() + "< " + "be" + ":" + (YbeMQ1).ToString("#.##") + "," + "构造不满足"; GZMQ1.ForeColor = Color.Red; }
                        else { GZMQ1.Text = "构造满足"; GZMQ1.ForeColor = Color.Black; }
                        Yσ1MQ1 = YNMQ1 * 1000 / (2 * YtMQ1 * Yb1MQ1);
                        Yσ2MQ1 = YNMQ1 * 1000 / 2 / YtMQ1 / (YaMQ1 - 2 * Yd0MQ1 / 3);
                        YσcMQ1 = YNMQ1 * 1000 / YtMQ1 / (Yd0MQ1 - 1);
                        YZMQ1 = Math.Sqrt((YaMQ1 + Yd0MQ1 / 2) * (YaMQ1 + Yd0MQ1 / 2) - (Yd0MQ1 / 2) * (Yd0MQ1 / 2));
                        YτMQ1 = YNMQ1 * 1000 / 2 / YtMQ1 / YZMQ1;

                        //这一段是吊耳板《钢结构设计标准》的计算内容

                        if (Yσ1MQ1 < YfMQ1)
                        { JKLMQ1.Text = "σ" + ":" + Yσ1MQ1.ToString("#.##") + "<" + "f" + ":" + YfMQ1.ToString() + "," + "满足"; ; JKLMQ1.ForeColor = Color.Black; }
                        else
                        { JKLMQ1.Text = "σ" + ":" + Yσ1MQ1.ToString("#.##") + ">" + "f" + ":" + YfMQ1.ToString() + "," + "不满足"; JKLMQ1.ForeColor = Color.Red; }

                        if (Yσ2MQ1 < YfMQ1)
                        { PKMQ1.Text = "σ" + ":" + Yσ2MQ1.ToString("#.##") + "<" + "f" + ":" + YfMQ1.ToString() + "," + "满足"; PKMQ1.ForeColor = Color.Black; }
                        else
                        { PKMQ1.Text = "σ" + ":" + Yσ2MQ1.ToString("#.##") + ">" + "f" + ":" + YfMQ1.ToString() + "," + "不满足"; PKMQ1.ForeColor = Color.Red; }

                        if (YτMQ1 < YfvMQ1)
                        { KJMQ1.Text = "τ" + ":" + YτMQ1.ToString("#.##") + "<" + "fv" + ":" + YfvMQ1.ToString() + "," + "满足"; KJMQ1.ForeColor = Color.Black; }
                        else
                        { KJMQ1.Text = "τ" + ":" + YτMQ1.ToString("#.##") + ">" + "fv" + ":" + YfvMQ1.ToString() + "," + "不满足"; KJMQ1.ForeColor = Color.Red; }

                        if (YσcMQ1 < YfcMQ1)
                        { CYMQ1.Text = "σ" + ":" + YσcMQ1.ToString("#.##") + "<" + "f" + ":" + YfcMQ1.ToString() + "," + "满足"; CYMQ1.ForeColor = Color.Black; }
                        else
                        { CYMQ1.Text = "σ" + ":" + YσcMQ1.ToString("#.##") + ">" + "f" + ":" + YfcMQ1.ToString() + "," + "不满足"; CYMQ1.ForeColor = Color.Red; }


                        //这一段是吊耳板《钢结构设计标准》底部受力及加劲板是否干涉的计算
                        YBJMQ1 = Math.Sqrt((YffMQ1 - 6) * (YffMQ1 - 6) + (Math.Abs(YcMQ1 - YgMQ1) + Yd0MQ1 / 2) * (Math.Abs(YcMQ1 - YgMQ1) + Yd0MQ1 / 2)) - Yd0MQ1 / 2;
                        if ((2 * YeMQ1 + 2 * YffMQ1) < (2 * YbMQ1 + Yd0MQ1)) { MessageBox.Show("底部一般不小于顶部宽度，可以增加e，f值"); }
                        else if (YcMQ1 < YgMQ1 || YcMQ1 < YbMQ1) { MessageBox.Show("底部补长a一般比加劲肋边长g高，且不小于双侧边缘净距b"); }
                        else if ((YBJMQ1) < 50) { MessageBox.Show("加劲肋距离孔边有点近，可以增加e，f值"); }


                        else
                        {
                            //YCθMQ1 = Math.Sqrt(1 - YSθMQ1 * YSθMQ1);//表示角度的余弦
                            NFMQ1 = YNMQ1;
                            // NVMQ1 = YNMQ1 * YCθMQ1;//表示水平力
                            YσF1MQ1 = NFMQ1 * 1000 / YtMQ1 / (2 * YeMQ1 + 2 * YffMQ1);//表示竖向力引起的耳板底部正应力
                            //YσF2MQ1 = NVMQ1 * 1000 * (Yd0MQ1 / 2 + YcMQ1) / YtMQ1 / (2 * YeMQ1 + 2 * YffMQ1) / (2 * YeMQ1 + 2 * YffMQ1) * 6;//表示弯矩引起的的耳板底部正应力
                            //Yτ1MQ1 = NVMQ1 * 1000 / YtMQ1 / (2 * YeMQ1 + 2 * YffMQ1);//表示水平力引起的耳板底部剪应力
                            YσFZMQ1 = YσF1MQ1;

                            if (YσFZMQ1 < YfMQ1)
                            { FHMQ1.Text = "σ" + ":" + YσFZMQ1.ToString("#.##") + "<" + "f" + ":" + YfMQ1.ToString() + "," + "满足"; FHMQ1.ForeColor = Color.Black; }
                            else
                            { FHMQ1.Text = "σ" + ":" + YσFZMQ1.ToString("#.##") + ">" + "f" + ":" + YfMQ1.ToString() + "," + "不满足"; FHMQ1.ForeColor = Color.Red; }

                        }
                    }
                }
            }
        }

        private void BoxXLP2_MouseHover(object sender, EventArgs e)
        {
            //ToolTip toollTip1=new ToolTip();

            //toollTip1.AutoPopDelay = 500000;
            //toolTip1.InitialDelay = 500;
            //toolTip1.ReshowDelay = 500;
            //toolTip1.ShowAlways = true;

            //toolTip1.SetToolTip(BoxXLP2, "heihei");
            if (BoxXLP2.Text != "")
            {
                if (BoxXLP2.SelectedIndex == 0)
                { pictureBox13.Visible = true; pictureBox13.Image = Image.FromFile("./TP/6x19.jpg"); pictureBox13.Height = 227; }
                else if (BoxXLP2.SelectedIndex == 1)
                { pictureBox13.Visible = true; pictureBox13.Image = Image.FromFile("./TP/6x37.jpg"); pictureBox13.Height = 407; }
                else if (BoxXLP2.SelectedIndex == 2)
                { pictureBox13.Visible = true; pictureBox13.Image = Image.FromFile("./TP/6x61.jpg"); pictureBox13.Height = 407; }
            }
        }

        private void BoxXLP2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox13.Visible = false;
        }

        private void BoxKHLP2_MouseHover(object sender, EventArgs e)
        {
            if (BoxKHLP2.Text != "")
            { pictureBox13.Visible = true; pictureBox13.Image = Image.FromFile("./TP/KH.jpg"); pictureBox13.Height = 227; }
        }

        private void BoxKHLP2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox13.Visible = false;
        }

        private void BoxX_MouseHover(object sender, EventArgs e)
        {
            if (BoxX.Text != "")
            {
                if (BoxX.SelectedIndex == 0)
                { pictureBox14.Visible = true; pictureBox14.Image = Image.FromFile("./TP/6x19.jpg"); pictureBox14.Height = 227; }
                else if (BoxX.SelectedIndex == 1)
                { pictureBox14.Visible = true; pictureBox14.Image = Image.FromFile("./TP/6x37.jpg"); pictureBox14.Height = 407; }
                else if (BoxX.SelectedIndex == 2)
                { pictureBox14.Visible = true; pictureBox14.Image = Image.FromFile("./TP/6x61.jpg"); pictureBox14.Height = 407; }
            }
        }

        private void BoxX_MouseLeave(object sender, EventArgs e)
        {
            pictureBox14.Visible = false;
        }

        private void BoxKH_MouseHover(object sender, EventArgs e)
        {
            if (BoxKH.Text != "")
            { pictureBox14.Visible = true; pictureBox14.Image = Image.FromFile("./TP/KH.jpg"); pictureBox14.Height = 227; }
        }

        private void BoxKH_MouseLeave(object sender, EventArgs e)
        {
            pictureBox14.Visible = false;
        }

        private void BoxXMQ1_MouseHover(object sender, EventArgs e)
        {
            if (BoxXMQ1.Text != "")
            {
                if (BoxXMQ1.SelectedIndex == 0)
                { pictureBox15.Visible = true; pictureBox15.Image = Image.FromFile("./TP/6x19.jpg"); pictureBox15.Height = 227; }
                else if (BoxXMQ1.SelectedIndex == 1)
                { pictureBox15.Visible = true; pictureBox15.Image = Image.FromFile("./TP/6x37.jpg"); pictureBox15.Height = 407; }
                else if (BoxXMQ1.SelectedIndex == 2)
                { pictureBox15.Visible = true; pictureBox15.Image = Image.FromFile("./TP/6x61.jpg"); pictureBox15.Height = 407; }
            }

        }

        private void BoxXMQ1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox15.Visible = false;
        }

        private void BoxKHMQ1_MouseHover(object sender, EventArgs e)
        {
            if (BoxKHMQ1.Text != "")
            {
                pictureBox15.Visible = true; pictureBox15.Image = Image.FromFile("./TP/KH.jpg"); pictureBox15.Height = 227;
            }
        }

        private void BoxKHMQ1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox15.Visible = false;
        }

        private void BoxQX1_MouseHover(object sender, EventArgs e)
        {
            ToolTip toollTip1 = new ToolTip();
            toollTip1.AutoPopDelay = 500;
            toolTip1.InitialDelay = 5;
            toolTip1.ReshowDelay = 5;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(BoxQX1, "根据《建筑施工起重吊装工程安全技术规范》：\n（1）当利用吊耳及卡环时，安全系数取6\n（2）当起吊重、大或精密重物时，安全系数取10");
        }

        private void BoxDD_MouseHover(object sender, EventArgs e)
        {
            ToolTip toollTip1 = new ToolTip();
            toollTip1.AutoPopDelay = 500;
            toolTip1.InitialDelay = 5;
            toolTip1.ReshowDelay = 5;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(BoxDD, "根据《建筑结构荷载规范》6.3.1条，动力系数取1.1\n根据《港口工程桩基规范》5.1.3条，动力系数取1.3\n根据《建筑施工起重吊装工程安全技术规范》B.0.2条，动力系数取1.5");

        }

        private void BoxQX1LP2_MouseHover(object sender, EventArgs e)
        {
            ToolTip toollTip1 = new ToolTip();
            toollTip1.AutoPopDelay = 500;
            toolTip1.InitialDelay = 5;
            toolTip1.ReshowDelay = 5;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(BoxQX1LP2, "根据《建筑施工起重吊装工程安全技术规范》：\n（1）当利用吊耳及卡环时，安全系数取6\n（2）当起吊重、大或精密重物时，安全系数取10");
        }

        private void BoxDDLP2_MouseHover(object sender, EventArgs e)
        {
            ToolTip toollTip1 = new ToolTip();

            toollTip1.AutoPopDelay = 500;
            toolTip1.InitialDelay = 5;
            toolTip1.ReshowDelay = 5;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(BoxDDLP2, "根据《建筑结构荷载规范》6.3.1条，动力系数取1.1\n根据《港口工程桩基规范》5.1.3条，动力系数取1.3\n根据《建筑施工起重吊装工程安全技术规范》B.0.2条，动力系数取1.5");
        }

        private void BoxQX1MQ1_MouseHover(object sender, EventArgs e)
        {
            ToolTip toollTip1 = new ToolTip();

            toollTip1.AutoPopDelay = 500;
            toolTip1.InitialDelay = 5;
            toolTip1.ReshowDelay = 5;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(BoxQX1MQ1, "根据《建筑施工起重吊装工程安全技术规范》：\n（1）当利用吊耳及卡环时，安全系数取6\n（2）当起吊重、大或精密重物时，安全系数取10");
        }

        private void BoxDDMQ1_MouseHover(object sender, EventArgs e)
        {
            ToolTip toollTip1 = new ToolTip();

            toollTip1.AutoPopDelay = 50;
            toolTip1.InitialDelay = 5;
            toolTip1.ReshowDelay = 5;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(BoxDDMQ1, "根据《建筑结构荷载规范》6.3.1条，动力系数取1.1\n根据《港口工程桩基规范》5.1.3条，动力系数取1.3\n根据《建筑施工起重吊装工程安全技术规范》B.0.2条，动力系数取1.5");
        }

        private void BoxXLP2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox13.Visible = false;

            BoxZJLP2.Items.Clear();
            if (BoxXLP2.SelectedIndex == 0)
            {
                BoxZJLP2.Items.Clear();
                BoxZJLP2.Items.AddRange(Z1LP2);
            }
            else if (BoxXLP2.SelectedIndex == 1)
            {
                BoxZJLP2.Items.Clear();
                BoxZJLP2.Items.AddRange(Z2LP2);
            }
            else if (BoxXLP2.SelectedIndex == 2)
            {
                BoxZJLP2.Items.Clear();
                BoxZJLP2.Items.AddRange(Z3LP2);
            }
        }

        private void BoxDD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '.'))
            { e.Handled = true; }
        }

        private void BoxDDLP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '.'))
            { e.Handled = true; }
        }

        private void BoxDDMQ1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '.'))
            { e.Handled = true; }
        }

        private void button4_Click(object sender, EventArgs e)
        {




            if (GSSLLLP2.Text == "暂时不知道" || GSSRXLLLP2.Text == "暂时不知道" || KHFHLP2.Text == "暂时不知道" || GSSLLsLP2.Text == "暂时不知道" || GZLP2.Text == "暂时不知道" || JKLLP2.Text == "暂时不知道" || PKLP2.Text == "暂时不知道" || KJLP2.Text == "暂时不知道" || FHLP2.Text == "暂时不知道" || DJLP2.Text == "暂时不知道" || CYLP2.Text == "暂时不知道")
            {
                MessageBox.Show("计算未完成！");


            }

            else
            {                                                                
                //这里加图片结束了
                button4.Enabled = false;//生成word的时候不能用，最后用

                Spire.Doc.Document doc = new Spire.Doc.Document();


                //添加一个section0，全文暂时添加一个

                Section section0 = doc.AddSection();

                section0.PageSetup.PageSize = PageSize.Letter;//设置第一页的格式


                //这是设置页边距，55f正好是中等的设置
                section0.PageSetup.Margins.Top = 55f;
                section0.PageSetup.Margins.Left = 55f;
                section0.PageSetup.Margins.Bottom = 55f;
                section0.PageSetup.Margins.Right = 55f;

                section0.AddParagraph();
                //添加一个段落 p0
                Paragraph p0 = section0.Paragraphs[0];//选择这个段落，我觉得有必要


                //插入公式，后面要用
                //OfficeMath officeMath = new OfficeMath(doc);
                //p0.Items.Add(officeMath);
                //officeMath.FromLatexMathCode("a^{2}+\\sqrt{|z|^{2}+1^2+\\frac{(x + y)^2}{x - y}}=2≤DS2dddd");



                //设置左对齐和缩进，第二段要用
                //p0.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                //p0.Format.FirstLineIndent = 24f;

                //设置中对齐
                p0.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                p0.Format.LineSpacing = 18f;
                TextRange rangeTop0 = p0.AppendText("第XX章 双吊耳吊装计算书");//选择这个段落，我觉得有必要
                rangeTop0.CharacterFormat.FontSize = 18;
                rangeTop0.CharacterFormat.Bold = true;
                rangeTop0.CharacterFormat.FontNameFarEast = "黑体";
                rangeTop0.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                //rangeTop0.StyleName.ToUpper(d) ;

                section0.AddParagraph();
                //添加一个段落 p1
                Paragraph p1 = section0.Paragraphs[1];//选择这个段落，我觉得有必要
                p1.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p1.Format.LineSpacing = 18f;
                TextRange rangeTop1 = p1.AppendText("XX.1 工况介绍");//选择这个段落，我觉得有必要
                rangeTop1.CharacterFormat.FontSize = 16;
                rangeTop1.CharacterFormat.Bold = true;
                rangeTop1.CharacterFormat.FontNameFarEast = "黑体";
                rangeTop1.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p2
                Paragraph p2 = section0.Paragraphs[2];//选择这个段落，我觉得有必要
                p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p2.Format.LineSpacing = 18f;
                p2.Format.FirstLineIndent = 24f;

                TextRange rangeTop2 = p2.AppendText("本构件采用双吊耳形式吊装" + "，" + "吊装示意见图XX.1-1" + "，" + "其中构件重量G2=" + tGLP2.Text + "t" + "，" + "右侧钢丝绳长度L1=" + tL1LP2.Text + "mm" + "，" + "左侧钢丝绳长度L2=" + tL2LP2.Text + "mm" + "，" + "右侧吊耳重心间距S1=" + tS1LP2.Text + "mm" + "，"
                    + "左侧吊耳重心间距S2=" + tS2LP2.Text + "mm" + "。");//添加文字段落1
                rangeTop2.CharacterFormat.FontSize = 12;
                rangeTop2.CharacterFormat.Bold = false;
                rangeTop2.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop2.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p3
                Paragraph p3 = section0.Paragraphs[3];//选择这个段落，我觉得有必要
                p3.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片放中间
                p3.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片放中间，不缩进
                string strp3 = System.Windows.Forms.Application.StartupPath;
                Image image3 = Image.FromFile(@strp3 + "\\TP\\双吊耳示意.jpg");
                DocPicture picture3 = section0.Paragraphs[3].AppendPicture(image3);
                picture3.Width = 280;
                picture3.Height = 230;

                section0.AddParagraph();
                //添加一个段落 p4
                Paragraph p4 = section0.Paragraphs[4];//选择这个段落，我觉得有必要
                p4.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p4.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop4 = p4.AppendText("图XX.1-1 吊装示意图");
                rangeTop4.CharacterFormat.FontSize = 11;
                rangeTop4.CharacterFormat.Bold = true;
                rangeTop4.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop4.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p5 这是第六个 写吊耳板详情的
                Paragraph p5 = section0.Paragraphs[5];//选择这个段落，我觉得有必要
                p5.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;//文字左对齐
                p5.Format.LineSpacing = 18f;
                p5.Format.FirstLineIndent = 24f;//普通文字段缩进
                TextRange rangeTop5 = p5.AppendText("工况采用吊耳尺寸见图XX.1-2" + "，" + "其中受力方向最小净距a=" + taLP2.Text + "mm" + "，" + "双侧边缘净距b=" + tbLP2.Text + "mm" + "，"
                      + "销轴孔径d0=" + td0LP2.Text + "mm" + "，" + "底部补长c=" + tcLP2.Text + "mm" + "，" + "加劲肋边距e=" + teLP2.Text + "mm" + "，" + "加劲肋中距f=" + tfLP2.Text + "mm" + "，" + "耳板厚度t=" + ttLP2.Text + "mm" + "，" + "耳板材质为" + BoxBLP2.Text + "。");//ttLP2
                rangeTop5.CharacterFormat.FontSize = 12;//小四是12，五号是11
                rangeTop5.CharacterFormat.Bold = false;
                rangeTop5.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop5.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p6 贴吊耳图片1的
                Paragraph p6 = section0.Paragraphs[6];//选择这个段落，我觉得有必要
                p6.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片放中间
                p6.Format.LineSpacing = 18f;
                p6.Format.FirstLineIndent = 28f;//图片放中间，不缩进，但是这个图片好像偏的有规律
                string strp6 = System.Windows.Forms.Application.StartupPath;//这个路径就是增加cad图片d1的位置
                Image image6 = Image.FromFile(@strp6 + "\\TP\\正面的.jpg");///////////////////////////改好了
                DocPicture picture6 = section0.Paragraphs[6].AppendPicture(image6);
                picture6.Width = 220;
                picture6.Height = 180;

                section0.AddParagraph();
                //添加一个段落 p7，写正立面图名的
                Paragraph p7 = section0.Paragraphs[7];//选择这个段落，我觉得有必要
                p7.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p7.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop7 = p7.AppendText("（a）正立面");
                rangeTop7.CharacterFormat.FontSize = 11;
                rangeTop7.CharacterFormat.Bold = true;
                rangeTop7.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop7.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p8贴吊耳侧面图的
                Paragraph p8 = section0.Paragraphs[8];//选择这个段落，我觉得有必要
                p8.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片放中间
                p8.Format.LineSpacing = 18f;
                p8.Format.FirstLineIndent = 28f;//图片放中间，不缩进，但是这个图片好像偏的有规律
                string strp8 = System.Windows.Forms.Application.StartupPath;//这个路径就是增加cad图片d2的位置
                Image image8 = Image.FromFile(@strp8 + "\\TP\\侧面的.jpg");
                DocPicture picture8 = section0.Paragraphs[8].AppendPicture(image8);
                picture8.Width = 200;
                picture8.Height = 160;


                section0.AddParagraph();
                //添加一个段落 p9，写侧立面图名的
                Paragraph p9 = section0.Paragraphs[9];//选择这个段落，我觉得有必要
                p9.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p9.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop9 = p9.AppendText("（b）侧面图");
                rangeTop9.CharacterFormat.FontSize = 11;
                rangeTop9.CharacterFormat.Bold = true;
                rangeTop9.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop9.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();
                //添加一个段落 p10，贴剖面图的
                Paragraph p10 = section0.Paragraphs[10];//选择这个段落，我觉得有必要
                p10.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片放中间
                p10.Format.LineSpacing = 18f;
                p10.Format.FirstLineIndent = 28f;//图片放中间，不缩进，但是这个图片好像偏的有规律
                string strp10 = System.Windows.Forms.Application.StartupPath;//这个路径就是增加cad图片d3的位置
                Image image10 = Image.FromFile(@strp10 + "\\TP\\剖面的.jpg");
                DocPicture picture10 = section0.Paragraphs[10].AppendPicture(image10);
                picture10.Width = 170;
                picture10.Height = 50;

                section0.AddParagraph();
                //添加一个段落 p11，写剖面图名的
                Paragraph p11 = section0.Paragraphs[11];//选择这个段落，我觉得有必要
                p11.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p11.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop11 = p11.AppendText("（c）底面图");
                rangeTop11.CharacterFormat.FontSize = 11;
                rangeTop11.CharacterFormat.Bold = true;
                rangeTop11.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop11.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p12，写尺寸图名的
                Paragraph p12 = section0.Paragraphs[12];//选择这个段落，我觉得有必要
                p12.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p12.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop12 = p12.AppendText("图XX.1-2 吊耳尺寸图");
                rangeTop12.CharacterFormat.FontSize = 11;//小四是12，五号是11
                rangeTop12.CharacterFormat.Bold = true;
                rangeTop12.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop12.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p13 写
                Paragraph p13 = section0.Paragraphs[13];//选择这个段落，我觉得有必要
                p13.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p13.Format.LineSpacing = 18f;
                TextRange rangeTop13 = p13.AppendText("XX.2 钢丝绳验算");//选择这个段落，我觉得有必要
                rangeTop13.CharacterFormat.FontSize = 16;
                rangeTop13.CharacterFormat.Bold = true;
                rangeTop13.CharacterFormat.FontNameFarEast = "黑体";
                rangeTop13.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();
                //添加一个段落 p14 
                Paragraph p14 = section0.Paragraphs[14];//选择这个段落，我觉得有必要
                p14.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p14.Format.LineSpacing = 18f;
                p14.Format.FirstLineIndent = 24f;

                TextRange rangeTop14 = p14.AppendText("考虑将吊点及吊耳中心所围成图形简化成三角形，吊点受到三向拉力" + "，" + "临时角度标注及边长示意见图XX.2-1" + "。");//选择这个段落，我觉得有必要
                rangeTop14.CharacterFormat.FontSize = 12;
                rangeTop14.CharacterFormat.Bold = false;
                rangeTop14.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop14.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();
                //添加一个段落 p15，贴算钢丝绳内力的
                Paragraph p15 = section0.Paragraphs[15];//选择这个段落，我觉得有必要
                p15.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片放中间
                p15.Format.LineSpacing = 18f;
                p15.Format.FirstLineIndent = 28f;//图片放中间，不缩进，但是这个图片好像偏的有规律
                string strp15 = System.Windows.Forms.Application.StartupPath;
                Image image15 = Image.FromFile(@strp15 + "\\TP\\算钢丝绳内力.jpg");
                DocPicture picture15 = section0.Paragraphs[15].AppendPicture(image15);
                picture15.Width = 272;
                picture15.Height = 240;


                section0.AddParagraph();
                //添加一个段落 p16，写钢丝绳内力图名
                Paragraph p16 = section0.Paragraphs[16];//选择这个段落，我觉得有必要
                p16.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p16.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop16 = p16.AppendText("图XX.2-1 钢丝绳内力计算示意图");
                rangeTop16.CharacterFormat.FontSize = 11;//小四是12，五号是11
                rangeTop16.CharacterFormat.Bold = true;
                rangeTop16.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop16.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();
                //添加一个段落 p17 写内力计算过程
                Paragraph p17 = section0.Paragraphs[17];//选择这个段落，我觉得有必要
                p17.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p17.Format.LineSpacing = 18f;
                p17.Format.FirstLineIndent = 24f;
                TextRange rangeTop17 = p17.AppendText("利用余弦定理求取aH1及aH2的余弦值" + "，" + "其公式如下" + "。");//选择这个段落，我觉得有必要
                rangeTop17.CharacterFormat.FontSize = 12;
                rangeTop17.CharacterFormat.Bold = false;
                rangeTop17.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop17.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                //插入公式，后面要用
                //OfficeMath officeMath = new OfficeMath(doc);
                //p0.Items.Add(officeMath);
                //officeMath.FromLatexMathCode("a^{2}+\\sqrt{|z|^{2}+1^2+\\frac{(x + y)^2}{x - y}}=2≤DS2dddd");

                section0.AddParagraph();
                //添加一个段落 p18 终于要写余弦公式了，两个角度的余弦的公式，这是第一个
                Paragraph p18 = section0.Paragraphs[18];//选择这个段落，我觉得有必要
                p18.Format.LineSpacing = 18f;
                OfficeMath officeMath18 = new OfficeMath(doc);
                p18.Items.Add(officeMath18);
                officeMath18.FromLatexMathCode("CosaH1=\\frac{L1^2+(S1+S2)^2-L2^2}{2L1(S1+S2)}");

                section0.AddParagraph();
                //添加一个段落 p19 终于要写余弦公式了，两个角度的余弦的公式，这是第二个
                Paragraph p19 = section0.Paragraphs[19];//选择这个段落，我觉得有必要
                p19.Format.LineSpacing = 18f;
                OfficeMath officeMath19 = new OfficeMath(doc);
                p19.Items.Add(officeMath19);
                officeMath19.FromLatexMathCode("CosaH2=\\frac{L2^2+(S1+S2)^2-L1^2}{2L2(S1+S2)}");

                section0.AddParagraph();
                //添加一个段落 p20 
                Paragraph p20 = section0.Paragraphs[20];//选择这个段落，我觉得有必要
                p20.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p20.Format.LineSpacing = 18f;
                p20.Format.FirstLineIndent = 24f;
                TextRange rangeTop20 = p20.AppendText("则余弦计算结果如下。");//选择这个段落，我觉得有必要
                rangeTop20.CharacterFormat.FontSize = 12;
                rangeTop20.CharacterFormat.Bold = false;
                rangeTop20.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop20.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                //原来的代码记不住
                //cosDH1 = Math.Abs(((YS1LP2 + YS2LP2) * (YS1LP2 + YS2LP2) + (YL1LP2 * YL1LP2) - YL2LP2 * YL2LP2) / 2 / (YS1LP2 + YS2LP2) / YL1LP2);
                //cosDH2 = Math.Abs(((YS1LP2 + YS2LP2) * (YS1LP2 + YS2LP2) + (YL2LP2 * YL2LP2) - YL1LP2 * YL1LP2) / 2 / (YS1LP2 + YS2LP2) / YL2LP2);
                //YH1LP2 = Math.Sqrt(YL1LP2 * YL1LP2 + YS1LP2 * YS1LP2 - 2 * YL1LP2 * YS1LP2 * cosDH1);
                //YH2LP2 = Math.Sqrt(YL2LP2 * YL2LP2 + YS2LP2 * YS2LP2 - 2 * YL2LP2 * YS2LP2 * cosDH2);


                section0.AddParagraph();
                //添加一个段落 p21//写计算余弦结果1
                Paragraph p21 = section0.Paragraphs[21];//选择这个段落，我觉得有必要
                p21.Format.LineSpacing = 18f;
                OfficeMath officeMath21 = new OfficeMath(doc);
                p21.Items.Add(officeMath21);
                officeMath21.FromLatexMathCode("CosaH1=\\frac{" + tL1LP2.Text + "^ 2+(" + tS1LP2.Text + "+" + tS2LP2.Text + ")^2-" + tL2LP2.Text + "^2}{2" + "*" + tL1LP2.Text + "*(" + tS1LP2.Text + "+" + tS2LP2.Text + ")}=" + cosDH1.ToString("0.###"));//你太美，尽管蒙着脸


                section0.AddParagraph();
                //添加一个段落 p22//写计算余弦结果2
                Paragraph p22 = section0.Paragraphs[22];//选择这个段落，我觉得有必要
                p22.Format.LineSpacing = 18f;
                OfficeMath officeMath22 = new OfficeMath(doc);
                p22.Items.Add(officeMath22);
                officeMath22.FromLatexMathCode("CosaH2=\\frac{" + tL2LP2.Text + "^ 2+(" + tS1LP2.Text + "+" + tS2LP2.Text + ")^2-" + tL1LP2.Text + "^2}{2" + "*" + tL2LP2.Text + "*(" + tS1LP2.Text + "+" + tS2LP2.Text + ")}=" + cosDH2.ToString("0.###"));//你太美，尽管蒙着脸

                section0.AddParagraph();
                //添加一个段落 p23 先求H
                Paragraph p23 = section0.Paragraphs[23];//选择这个段落，我觉得有必要
                p23.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p23.Format.LineSpacing = 18f;
                p23.Format.FirstLineIndent = 24f;
                TextRange rangeTop23 = p23.AppendText("利用双侧余弦计算吊耳构件间距H，其公式如下(第二个公式作为验证)。");//选择这个段落，我觉得有必要
                rangeTop23.CharacterFormat.FontSize = 12;
                rangeTop23.CharacterFormat.Bold = false;
                rangeTop23.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop23.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 求H，第二个公式
                Paragraph p24 = section0.Paragraphs[24];//选择这个段落，我觉得有必要
                p24.Format.LineSpacing = 18f;
                OfficeMath officeMath24 = new OfficeMath(doc);
                p24.Items.Add(officeMath24);
                officeMath24.FromLatexMathCode("H=\\sqrt{L1^2+S1^2-2L1S1CosaH1}");



                section0.AddParagraph();
                Paragraph p25 = section0.Paragraphs[25];//选择这个段落，我觉得有必要
                p25.Format.LineSpacing = 18f;
                OfficeMath officeMath25 = new OfficeMath(doc);
                p25.Items.Add(officeMath25);
                officeMath25.FromLatexMathCode("H=\\sqrt{L2^2+S2^2-2L2S2CosaH2}");

                section0.AddParagraph();
                //添加一个段落 p26 先求H
                Paragraph p26 = section0.Paragraphs[26];//选择这个段落，我觉得有必要
                p26.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p26.Format.LineSpacing = 18f;
                p26.Format.FirstLineIndent = 24f;
                TextRange rangeTop26 = p26.AppendText("H计算过程及结果如下。");//选择这个段落，我觉得有必要
                rangeTop26.CharacterFormat.FontSize = 12;
                rangeTop26.CharacterFormat.Bold = false;
                rangeTop26.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop26.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                //原来的代码记不住
                //cosDH1 = Math.Abs(((YS1LP2 + YS2LP2) * (YS1LP2 + YS2LP2) + (YL1LP2 * YL1LP2) - YL2LP2 * YL2LP2) / 2 / (YS1LP2 + YS2LP2) / YL1LP2);
                //cosDH2 = Math.Abs(((YS1LP2 + YS2LP2) * (YS1LP2 + YS2LP2) + (YL2LP2 * YL2LP2) - YL1LP2 * YL1LP2) / 2 / (YS1LP2 + YS2LP2) / YL2LP2);
                //YH1LP2 = Math.Sqrt(YL1LP2 * YL1LP2 + YS1LP2 * YS1LP2 - 2 * YL1LP2 * YS1LP2 * cosDH1);
                //YH2LP2 = Math.Sqrt(YL2LP2 * YL2LP2 + YS2LP2 * YS2LP2 - 2 * YL2LP2 * YS2LP2 * cosDH2);

                section0.AddParagraph();//27行写H的计算结果及过程1
                Paragraph p27 = section0.Paragraphs[27];//选择这个段落，我觉得有必要
                p27.Format.LineSpacing = 18f;
                OfficeMath officeMath27 = new OfficeMath(doc);
                p27.Items.Add(officeMath27);
                officeMath27.FromLatexMathCode("H=\\sqrt{" + tL1LP2.Text.ToString() + "^2+" + tS1LP2.Text.ToString() + "^2-2*" + tL1LP2.Text.ToString() + "*" + tS1LP2.Text.ToString() + "*" + cosDH1.ToString("0.###") + "}=" + YH1LP2.ToString("#.##"));

                section0.AddParagraph();//28行写H的计算结果及过程2
                Paragraph p28 = section0.Paragraphs[28];//选择这个段落，我觉得有必要
                p28.Format.LineSpacing = 18f;
                OfficeMath officeMath28 = new OfficeMath(doc);
                p28.Items.Add(officeMath28);
                officeMath28.FromLatexMathCode("H=\\sqrt{" + tL2LP2.Text.ToString() + "^2+" + tS2LP2.Text.ToString() + "^2-2*" + tL2LP2.Text.ToString() + "*" + tS2LP2.Text.ToString() + "*" + cosDH2.ToString("0.###") + "}=" + YH2LP2.ToString("#.##"));

                section0.AddParagraph();
                //添加一个段落 p29 先求H
                Paragraph p29 = section0.Paragraphs[29];//选择这个段落，我觉得有必要
                p29.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p29.Format.LineSpacing = 18f;
                p29.Format.FirstLineIndent = 24f;
                TextRange rangeTop29 = p29.AppendText("双侧余弦计算的H值相同，计算结果认为是正确，" + "则H=" + YH1LP2.ToString("#.##") + "mm");
                rangeTop29.CharacterFormat.FontSize = 12;
                rangeTop29.CharacterFormat.Bold = false;
                rangeTop29.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop29.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();
                //添加一个段落 p30
                Paragraph p30 = section0.Paragraphs[30];//选择这个段落，我觉得有必要
                p30.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p30.Format.LineSpacing = 18f;
                p30.Format.FirstLineIndent = 24f;
                TextRange rangeTop30 = p30.AppendText("按照相同的方式分别计算aS1及aS2的余弦值，计算过程如下。");//选择这个段落，我觉得有必要
                rangeTop30.CharacterFormat.FontSize = 12;
                rangeTop30.CharacterFormat.Bold = false;
                rangeTop30.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop30.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();
                //添加一个段落 p31//写计算aS1余弦结果
                Paragraph p31 = section0.Paragraphs[31];//选择这个段落，我觉得有必要
                p31.Format.LineSpacing = 18f;
                OfficeMath officeMath31 = new OfficeMath(doc);
                p31.Items.Add(officeMath31);
                officeMath31.FromLatexMathCode("CosaS1=\\frac{" + tL1LP2.Text + "^ 2+" + YH1LP2.ToString("#.##") + "^2-" + tS1LP2.Text +
                    "^2}{2" + "*" + tL1LP2.Text + "*" + YH1LP2.ToString("#.##") + "}=" + cosDS1.ToString("0.###"));//你太美，尽管蒙着脸

                section0.AddParagraph();
                //添加一个段落 p32//写计算aS2余弦结果
                Paragraph p32 = section0.Paragraphs[32];//选择这个段落，我觉得有必要
                p32.Format.LineSpacing = 18f;
                OfficeMath officeMath32 = new OfficeMath(doc);
                p32.Items.Add(officeMath32);
                officeMath32.FromLatexMathCode("CosaS2=\\frac{" + tL2LP2.Text + "^ 2+" + YH2LP2.ToString("#.##") + "^2-" + tS2LP2.Text +
                    "^2}{2" + "*" + tL2LP2.Text + "*" + YH2LP2.ToString("#.##") + "}=" + cosDS2.ToString("0.###"));//你太美，尽管蒙着脸

                section0.AddParagraph();
                //添加一个段落 p33
                Paragraph p33 = section0.Paragraphs[33];//选择这个段落，我觉得有必要
                p33.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p33.Format.LineSpacing = 18f;
                p33.Format.FirstLineIndent = 24f;
                TextRange rangeTop33 = p33.AppendText("在此基础上分别计算出四个角度的正弦值，计算结果如下。");//选择这个段落，我觉得有必要
                rangeTop33.CharacterFormat.FontSize = 12;
                rangeTop33.CharacterFormat.Bold = false;
                rangeTop33.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop33.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//34行写正弦值1
                Paragraph p34 = section0.Paragraphs[34];//选择这个段落，我觉得有必要
                p34.Format.LineSpacing = 18f;
                OfficeMath officeMath34 = new OfficeMath(doc);
                p34.Items.Add(officeMath34);
                officeMath34.FromLatexMathCode("SinaS1=\\sqrt{1-" + cosDS1.ToString("0.###") + "^2}=" + sinDS1.ToString("0.###"));

                section0.AddParagraph();//35行写正弦值2
                Paragraph p35 = section0.Paragraphs[35];//选择这个段落，我觉得有必要
                p35.Format.LineSpacing = 18f;
                OfficeMath officeMath35 = new OfficeMath(doc);
                p35.Items.Add(officeMath35);
                officeMath35.FromLatexMathCode("SinaS2=\\sqrt{1-" + cosDS2.ToString("0.###") + "^2}=" + sinDS2.ToString("0.###"));


                section0.AddParagraph();//36行写正弦值3
                Paragraph p36 = section0.Paragraphs[36];//选择这个段落，我觉得有必要
                p36.Format.LineSpacing = 18f;
                OfficeMath officeMath36 = new OfficeMath(doc);
                p36.Items.Add(officeMath36);
                officeMath36.FromLatexMathCode("SinaH1=\\sqrt{1-" + cosDH1.ToString("0.###") + "^2}=" + sinDH1.ToString("0.###"));

                section0.AddParagraph();//35行写正弦值2
                Paragraph p37 = section0.Paragraphs[37];//选择这个段落，我觉得有必要
                p37.Format.LineSpacing = 18f;
                OfficeMath officeMath37 = new OfficeMath(doc);
                p37.Items.Add(officeMath37);
                officeMath37.FromLatexMathCode("SinaH2=\\sqrt{1-" + cosDH2.ToString("0.###") + "^2}=" + sinDH2.ToString("0.###"));

                section0.AddParagraph();
                //添加一个段落 p38
                Paragraph p38 = section0.Paragraphs[38];//选择这个段落，我觉得有必要
                p38.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p38.Format.LineSpacing = 18f;
                p38.Format.FirstLineIndent = 24f;
                TextRange rangeTop38 = p38.AppendText("由图xx.2-1中三向拉力及分力的分布，由吊点平衡可得如下等式。");//选择这个段落，我觉得有必要
                rangeTop38.CharacterFormat.FontSize = 12;
                rangeTop38.CharacterFormat.Bold = false;
                rangeTop38.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop38.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p39 本段加等式一
                Paragraph p39 = section0.Paragraphs[39];//选择这个段落，我觉得有必要
                p39.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p39.Format.LineSpacing = 18f;
                p39.Format.FirstLineIndent = 24f;
                TextRange rangeTop39 = p39.AppendText("水平方向上：");//选择这个段落，我觉得有必要
                rangeTop39.CharacterFormat.FontSize = 12;
                rangeTop39.CharacterFormat.Bold = false;
                rangeTop39.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop39.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                //本段加公式
                OfficeMath officeMath39 = new OfficeMath(doc);
                p39.Items.Add(officeMath39);
                officeMath39.FromLatexMathCode("NaS2*SinaS2=NaS1*SinaS1");


                section0.AddParagraph();
                //添加一个段落 p40本段加等式二
                Paragraph p40 = section0.Paragraphs[40];//选择这个段落，我觉得有必要
                p40.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p40.Format.LineSpacing = 18f;
                p40.Format.FirstLineIndent = 24f;
                TextRange rangeTop40 = p40.AppendText("竖直方向上：");//选择这个段落，我觉得有必要
                rangeTop40.CharacterFormat.FontSize = 12;
                rangeTop40.CharacterFormat.Bold = false;
                rangeTop40.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop40.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                //本段加公式
                OfficeMath officeMath40 = new OfficeMath(doc);
                p40.Items.Add(officeMath40);
                officeMath40.FromLatexMathCode("NaS1*CosaS1+NaS2*CosaS2=N");

                section0.AddParagraph();
                //添加一个段落p41本段开始说明钢丝绳内力
                Paragraph p41 = section0.Paragraphs[41];//选择这个段落，我觉得有必要
                p41.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p41.Format.LineSpacing = 18f;
                p41.Format.FirstLineIndent = 24f;
                TextRange rangeTop41 = p41.AppendText("通过公式转换，双侧钢丝绳内力计算如下。");//选择这个段落，我觉得有必要
                rangeTop41.CharacterFormat.FontSize = 12;
                rangeTop41.CharacterFormat.Bold = false;
                rangeTop41.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop41.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//42行写NaS1
                Paragraph p42 = section0.Paragraphs[42];//选择这个段落，我觉得有必要
                p42.Format.LineSpacing = 18f;
                OfficeMath officeMath42 = new OfficeMath(doc);
                p42.Items.Add(officeMath42);
                officeMath42.FromLatexMathCode("NaS1=\\frac{N}{CosaS1+\\frac{SinaS1}{SinaS2}*CosaS2}");

                section0.AddParagraph();//43行写NaS2
                Paragraph p43 = section0.Paragraphs[43];//选择这个段落，我觉得有必要
                p43.Format.LineSpacing = 18f;
                OfficeMath officeMath43 = new OfficeMath(doc);
                p43.Items.Add(officeMath43);
                officeMath43.FromLatexMathCode("NaS2=NaS1*\\frac{SinaS1}{SinaS2}");

                section0.AddParagraph();
                //添加一个段落p44开始求解钢丝绳内力
                Paragraph p44 = section0.Paragraphs[44];//选择这个段落，我觉得有必要
                p44.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p44.Format.LineSpacing = 18f;
                p44.Format.FirstLineIndent = 24f;
                TextRange rangeTop44 = p44.AppendText("本工况动力系数为" + BoxDDLP2.Text + "，则钢丝绳内力标准值计算如下。");//选择这个段落，我觉得有必要
                rangeTop44.CharacterFormat.FontSize = 12;
                rangeTop44.CharacterFormat.Bold = false;
                rangeTop44.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop44.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//45行写NaS1的计算结果
                Paragraph p45 = section0.Paragraphs[45];//选择这个段落，我觉得有必要
                p45.Format.LineSpacing = 18f;
                OfficeMath officeMath45 = new OfficeMath(doc);
                p45.Items.Add(officeMath45);
                officeMath45.FromLatexMathCode("NaS1=\\frac{" + YDDLP2.ToString() + "*" + YGLP2.ToString() + "*10" + "}{" + cosDS1.ToString("0.##") + "+\\frac{" + sinDS1.ToString("0.##") + "}{" + sinDS2.ToString("0.##") + "}*" + cosDS2.ToString("0.##") + "}" + "=" + NS1.ToString("#.##") + "kN");

                section0.AddParagraph();//46行写NaS2的计算结果
                Paragraph p46 = section0.Paragraphs[46];//选择这个段落，我觉得有必要
                p46.Format.LineSpacing = 18f;
                OfficeMath officeMath46 = new OfficeMath(doc);
                p46.Items.Add(officeMath46);
                officeMath46.FromLatexMathCode("NaS2=" + NS1.ToString("#.##") + "*\\frac{" + sinDS1.ToString("0.##") + "}{" + sinDS2.ToString("0.##") + "}" + "=" + NS2.ToString("#.##") + "kN");

                section0.AddParagraph();
                //添加一个段落p47开始求解钢丝绳内力
                Paragraph p47 = section0.Paragraphs[47];//选择这个段落，我觉得有必要
                p47.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p47.Format.LineSpacing = 18f;
                p47.Format.FirstLineIndent = 24f;
                TextRange rangeTop47 = p47.AppendText("本工况所选择的钢丝绳材质：" + BOXSLP2.Text + "，型号为：" + BoxXLP2.Text + "，直径为：" + BoxZJLP2.Text + "mm，" +
                    "根据《建筑施工计算手册》查取，其破断拉力总和为" + FgLP2.ToString() + "kN。");//选择这个段落，我觉得有必要
                rangeTop47.CharacterFormat.FontSize = 12;
                rangeTop47.CharacterFormat.Bold = false;
                rangeTop47.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop47.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();
                //添加一个段落p48开始求解钢丝绳内力
                Paragraph p48 = section0.Paragraphs[48];//选择这个段落，我觉得有必要
                p48.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p48.Format.LineSpacing = 18f;
                p48.Format.FirstLineIndent = 24f;
                TextRange rangeTop48 = p48.AppendText("钢丝绳容许拉力根据《建筑施工计算手册》公式13-3计算，公式如下。");//选择这个段落，我觉得有必要
                rangeTop48.CharacterFormat.FontSize = 12;
                rangeTop48.CharacterFormat.Bold = false;
                rangeTop48.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop48.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//49行写NaS1的计算结果
                Paragraph p49 = section0.Paragraphs[49];//选择这个段落，我觉得有必要
                p49.Format.LineSpacing = 18f;
                OfficeMath officeMath49 = new OfficeMath(doc);
                p49.Items.Add(officeMath49);
                officeMath49.FromLatexMathCode("[Fg]=\\frac{αFg}{K}");

                section0.AddParagraph();
                //添加一个段落p50开始求解钢丝绳内力
                Paragraph p50 = section0.Paragraphs[50];//选择这个段落，我觉得有必要
                p50.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p50.Format.LineSpacing = 18f;
                p50.Format.FirstLineIndent = 24f;
                TextRange rangeTop50 = p50.AppendText("由于钢丝绳型号为：" + BoxXLP2.Text + "，不均匀系数α取值" + αLP2.ToString() + "安全系数K选择取值为" + KLP2.ToString() + "，则钢丝绳容许压力[Fg]计算如下。");//选择这个段落，我觉得有必要
                rangeTop50.CharacterFormat.FontSize = 12;
                rangeTop50.CharacterFormat.Bold = false;
                rangeTop50.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop50.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();//51行写[Fg]的计算结果
                Paragraph p51 = section0.Paragraphs[51];//选择这个段落，我觉得有必要
                p51.Format.LineSpacing = 18f;
                OfficeMath officeMath51 = new OfficeMath(doc);
                p51.Items.Add(officeMath51);
                officeMath51.FromLatexMathCode("[Fg]=\\frac{" + αLP2.ToString() + "*" + FgLP2.ToString() + "}{" + KLP2.ToString() + "}" + "=" + rFgLP2.ToString("#.##") + "kN");

                section0.AddParagraph();
                //添加一个段落p50开始求解钢丝绳内力
                Paragraph p52 = section0.Paragraphs[52];//选择这个段落，我觉得有必要
                p52.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p52.Format.LineSpacing = 18f;
                p52.Format.FirstLineIndent = 24f;

                if (rFgLP2 < YN3LP2)
                {
                    TextRange rangeTop52 = p52.AppendText("由于最大钢丝绳拉力" + YN3LP2.ToString("#.##") + ">" + rFgLP2.ToString("#.##") + "kN，钢丝绳满足受力不要求。");
                    rangeTop52.CharacterFormat.FontSize = 12;
                    rangeTop52.CharacterFormat.Bold = true;
                    rangeTop52.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop52.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop52.CharacterFormat.TextColor = Color.Red;
                }//选择这个段落，我觉得有必要
                else
                {
                    TextRange rangeTop52 = p52.AppendText("由于最大钢丝绳拉力" + YN3LP2.ToString("#.##") + "≤" + rFgLP2.ToString("#.##") + "kN，钢丝绳满足受力要求。");
                    rangeTop52.CharacterFormat.FontSize = 12;
                    rangeTop52.CharacterFormat.Bold = false;
                    rangeTop52.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop52.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }

                section0.AddParagraph();
                //添加一个段落p53开始写卡环
                Paragraph p53 = section0.Paragraphs[53];//选择这个段落，我觉得有必要
                p53.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p53.Format.LineSpacing = 18f;
                p53.Format.FirstLineIndent = 24f;
                if (rFhLP2 < YN3LP2)
                {
                    TextRange rangeTop53 = p53.AppendText("本工况选择的卡环型号为：" + BoxKHLP2.Text + "，根据《建筑施工计算手册》，卡环使用负荷[Fj]为" + rFhLP2.ToString("#.##") + "kN，由于" + YN3LP2.ToString("#.##") + ">" + rFhLP2.ToString() + "kN" + "，卡环不满足要求。");//选择这个段落，我觉得有必要
                    rangeTop53.CharacterFormat.FontSize = 12;
                    rangeTop53.CharacterFormat.Bold = true;
                    rangeTop53.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop53.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop53.CharacterFormat.TextColor = Color.Red;
                }
                else
                {
                    TextRange rangeTop53 = p53.AppendText("本工况选择的卡环型号为：" + BoxKHLP2.Text + "，根据《建筑施工计算手册》，卡环使用负荷[Fj]为" + rFhLP2.ToString("#.##") + "kN，由于" + YN3LP2.ToString("#.##") + "≤" + rFhLP2.ToString() + "kN" + "，卡环满足要求。");//选择这个段落，我觉得有必要
                    rangeTop53.CharacterFormat.FontSize = 12;
                    rangeTop53.CharacterFormat.Bold = false;
                    rangeTop53.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop53.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                }

                section0.AddParagraph();
                //添加一个段落 p54写第三个标题
                Paragraph p54 = section0.Paragraphs[54];//选择这个段落，我觉得有必要
                p54.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p54.Format.LineSpacing = 18f;
                TextRange rangeTop54 = p54.AppendText("XX.3 吊耳验算");//选择这个段落，我觉得有必要
                rangeTop54.CharacterFormat.FontSize = 16;
                rangeTop54.CharacterFormat.Bold = true;
                rangeTop54.CharacterFormat.FontNameFarEast = "黑体";
                rangeTop54.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落p55开始求解钢丝绳内力
                Paragraph p55 = section0.Paragraphs[55];//选择这个段落，我觉得有必要
                p55.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p55.Format.LineSpacing = 18f;
                p55.Format.FirstLineIndent = 24f;
                TextRange rangeTop55 = p55.AppendText("根据《钢结构设计标准》11.6.2条，吊耳板应满足如下要求。");//选择这个段落，我觉得有必要
                rangeTop55.CharacterFormat.FontSize = 12;
                rangeTop55.CharacterFormat.Bold = false;
                rangeTop55.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop55.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//56行写吊耳板构造
                Paragraph p56 = section0.Paragraphs[56];//选择这个段落，我觉得有必要
                p56.Format.LineSpacing = 18f;
                OfficeMath officeMath56 = new OfficeMath(doc);
                p56.Items.Add(officeMath56);
                officeMath56.FromLatexMathCode("a≥\\frac{4}{3}be");

                section0.AddParagraph();//57行写吊耳板构造2
                Paragraph p57 = section0.Paragraphs[57];//选择这个段落，我觉得有必要
                p57.Format.LineSpacing = 18f;
                OfficeMath officeMath57 = new OfficeMath(doc);
                p57.Items.Add(officeMath57);
                officeMath57.FromLatexMathCode("be=2t+16≤b");

                section0.AddParagraph();
                //添加一个段落p58写构造结果1
                Paragraph p58 = section0.Paragraphs[58];//选择这个段落，我觉得有必要
                p58.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p58.Format.LineSpacing = 18f;
                p58.Format.FirstLineIndent = 24f;
                TextRange rangeTop58 = p58.AppendText("则");//选择这个段落，我觉得有必要
                rangeTop58.CharacterFormat.FontSize = 12;
                rangeTop58.CharacterFormat.Bold = false;
                rangeTop58.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop58.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                OfficeMath officeMath58 = new OfficeMath(doc);
                p58.Items.Add(officeMath58);
                officeMath58.FromLatexMathCode("be=" + "2*" + YtLP2.ToString() + "+16=" + YbeLP2.ToString());
                TextRange rangeTop58X1 = p58.AppendText("。");//选择这个段落，我觉得有必要


                section0.AddParagraph();
                //添加一个段落p59写构造结果2
                Paragraph p59 = section0.Paragraphs[59];//选择这个段落，我觉得有必要
                p59.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p59.Format.LineSpacing = 18f;
                p59.Format.FirstLineIndent = 24f;
                TextRange rangeTop59 = p59.AppendText("由于a=" + YaLP2.ToString() + "，" + "b=" + YbLP2.ToString() + "。");//选择这个段落，我觉得有必要
                rangeTop59.CharacterFormat.FontSize = 12;
                rangeTop59.CharacterFormat.Bold = false;
                rangeTop59.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop59.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落p60写构造结果2
                Paragraph p60 = section0.Paragraphs[60];//选择这个段落，我觉得有必要
                p60.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p60.Format.LineSpacing = 18f;
                p60.Format.FirstLineIndent = 24f;
                if (YaLP2 < (YbeLP2 / 3 * 4))
                {
                    OfficeMath officeMath60 = new OfficeMath(doc);
                    p60.Items.Add(officeMath60);
                    officeMath60.FromLatexMathCode("a<\\frac{4}{3}be");
                }
                else
                {
                    OfficeMath officeMath60 = new OfficeMath(doc);
                    p60.Items.Add(officeMath60);
                    officeMath60.FromLatexMathCode("a≥\\frac{4}{3}be");
                }

                section0.AddParagraph();
                //添加一个段落p60写构造结果2
                Paragraph p61 = section0.Paragraphs[61];//选择这个段落，我觉得有必要
                p61.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p61.Format.LineSpacing = 18f;
                p61.Format.FirstLineIndent = 24f;
                if (YbLP2 < YbeLP2)
                {
                    OfficeMath officeMath61 = new OfficeMath(doc);
                    p61.Items.Add(officeMath61);
                    officeMath61.FromLatexMathCode("b<be");
                }
                else
                {
                    OfficeMath officeMath61 = new OfficeMath(doc);
                    p61.Items.Add(officeMath61);
                    officeMath61.FromLatexMathCode("b≥be");
                }


                section0.AddParagraph();
                //添加一个段落p59写构造结果2
                Paragraph p62 = section0.Paragraphs[62];//选择这个段落，我觉得有必要
                p62.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p62.Format.LineSpacing = 18f;
                p62.Format.FirstLineIndent = 24f;

                if (YaLP2 >= (YbeLP2 / 3 * 4) & YbLP2 >= YbeLP2)
                {
                    TextRange rangeTop62 = p62.AppendText("构造满足要求。");//选择这个段落，我觉得有必要
                    rangeTop62.CharacterFormat.FontSize = 12;
                    rangeTop62.CharacterFormat.Bold = false;
                    rangeTop62.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop62.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop62 = p62.AppendText("构造不满足要求。");//选择这个段落，我觉得有必要
                    rangeTop62.CharacterFormat.FontSize = 12;
                    rangeTop62.CharacterFormat.Bold = true;
                    rangeTop62.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop62.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop62.CharacterFormat.TextColor = Color.Red;
                }


                section0.AddParagraph();
                //添加一个段落p63写N设计值
                Paragraph p63 = section0.Paragraphs[63];//选择这个段落，我觉得有必要
                p63.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p63.Format.LineSpacing = 18f;
                p63.Format.FirstLineIndent = 24f;
                TextRange rangeTop63 = p63.AppendText("用于吊耳计算时采用的钢丝绳拉力设计值N=max(1.5*NaS1,1.5*NaS2)=" + YNLP2.ToString("#.##") + "kN。");//选择这个段落，我觉得有必要
                rangeTop63.CharacterFormat.FontSize = 12;
                rangeTop63.CharacterFormat.Bold = false;
                rangeTop63.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop63.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落p64写构造结果2
                Paragraph p64 = section0.Paragraphs[64];//选择这个段落，我觉得有必要
                p64.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p64.Format.LineSpacing = 18f;
                p64.Format.FirstLineIndent = 24f;
                TextRange rangeTop64 = p64.AppendText("根据《钢结构设计标准》公式11.6.3-1及11.6.3-2，耳板孔净截面处的抗拉应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop64.CharacterFormat.FontSize = 12;
                rangeTop64.CharacterFormat.Bold = false;
                rangeTop64.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop64.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();//64行耳板孔净截面处的抗拉强度公式2:
                Paragraph p65 = section0.Paragraphs[65];//选择这个段落，我觉得有必要
                p65.Format.LineSpacing = 18f;
                OfficeMath officeMath65 = new OfficeMath(doc);
                p65.Items.Add(officeMath65);
                officeMath65.FromLatexMathCode("b1=min(2t+16，b-\\frac{d0}{3})=min(2*" + YtLP2.ToString() + "+16，" + YbLP2.ToString() + "-\\frac{" + Yd0LP2.ToString()
                    + "}{3})=" + Yb1LP2.ToString() + "mm");

                section0.AddParagraph();//66行耳板孔净截面处的抗拉强度公式1:
                Paragraph p66 = section0.Paragraphs[66];//选择这个段落，我觉得有必要
                p66.Format.LineSpacing = 18f;
                OfficeMath officeMath66 = new OfficeMath(doc);
                p66.Items.Add(officeMath66);
                officeMath66.FromLatexMathCode("σ=\\frac{N}{2tb1}=\\frac{" + YNLP2.ToString("#.##") + "*1000}{2*" + YtLP2.ToString() + "*" + Yb1LP2.ToString() + "}=" + Yσ1LP2.ToString("#.##") + "MPa");




                section0.AddParagraph();
                //添加一个段落p67写耳板端部截面抗拉(劈开）强度
                Paragraph p67 = section0.Paragraphs[67];//选择这个段落，我觉得有必要
                p67.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p67.Format.LineSpacing = 18f;
                p67.Format.FirstLineIndent = 24f;
                TextRange rangeTop67 = p67.AppendText("根据《钢结构设计标准》公式11.6.3-3，耳板端部截面抗拉(劈开）应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop67.CharacterFormat.FontSize = 12;
                rangeTop67.CharacterFormat.Bold = false;
                rangeTop67.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop67.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();//添加一个段落p67写耳板端部截面抗拉(劈开）强度；
                Paragraph p68 = section0.Paragraphs[68];//选择这个段落，我觉得有必要
                p68.Format.LineSpacing = 18f;
                OfficeMath officeMath68 = new OfficeMath(doc);
                p68.Items.Add(officeMath68);
                officeMath68.FromLatexMathCode("σ=\\frac{N}{2t(a-\\frac{2d0}{3})}=\\frac{" + YNLP2.ToString("#.##") + "*1000}{2*" + YtLP2.ToString() + "*(" + YaLP2.ToString() + "-\\frac{2*" + Yd0LP2.ToString() + "}" + "{3})" + "}=" + Yσ2LP2.ToString("#.##") + "MPa");



                section0.AddParagraph();
                //添加一个段落p69写耳板抗剪强度
                Paragraph p69 = section0.Paragraphs[69];//选择这个段落，我觉得有必要
                p69.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p69.Format.LineSpacing = 18f;
                p69.Format.FirstLineIndent = 24f;
                TextRange rangeTop69 = p69.AppendText("根据《钢结构设计标准》公式11.6.3-4及11.6.3-5，耳板抗剪应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop69.CharacterFormat.FontSize = 12;
                rangeTop69.CharacterFormat.Bold = false;
                rangeTop69.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop69.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();//添加一个段落p70写耳板抗剪强度公式2；
                Paragraph p70 = section0.Paragraphs[70];//选择这个段落，我觉得有必要
                p70.Format.LineSpacing = 18f;
                OfficeMath officeMath70 = new OfficeMath(doc);
                p70.Items.Add(officeMath70);
                officeMath70.FromLatexMathCode("Z=\\sqrt{(a+d0/2)^2-(d0/2)^2}=\\sqrt{(" + YaLP2.ToString() + "+" + Yd0LP2.ToString() + "/2)^2-(" + Yd0LP2.ToString() + "/2)^2}=" + YZLP2.ToString("#.##") + "mm");

                section0.AddParagraph();//添加一个段落p71写耳板抗剪强度公式1；
                Paragraph p71 = section0.Paragraphs[71];//选择这个段落，我觉得有必要
                p71.Format.LineSpacing = 18f;
                OfficeMath officeMath71 = new OfficeMath(doc);
                p71.Items.Add(officeMath71);
                officeMath71.FromLatexMathCode("τ=\\frac{N}{2tZ}=\\frac{" + YNLP2.ToString("#.##") + "*1000}{2*" + YtLP2.ToString() + "*" + YZLP2.ToString("#.##") + "}=" + YτLP2.ToString("#.##") + "MPa");


                //这里增加局部抗压，由于是另外加入，所以需要在71后面加后缀//TMD,顺延把

                section0.AddParagraph();
                //添加一个段落p69写耳板局部受压强度
                Paragraph p72 = section0.Paragraphs[72];//选择这个段落，我觉得有必要
                p72.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p72.Format.LineSpacing = 18f;
                p72.Format.FirstLineIndent = 24f;
                TextRange rangeTop72 = p72.AppendText("根据《钢结构设计标准》公式11.6.6-1，耳板承压强度计算如下。");//选择这个段落，我觉得有必要
                rangeTop72.CharacterFormat.FontSize = 12;
                rangeTop72.CharacterFormat.Bold = false;
                rangeTop72.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop72.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();//添加一个段落p73写耳板承压强度公式2；
                Paragraph p73 = section0.Paragraphs[73];//选择这个段落，我觉得有必要
                p73.Format.LineSpacing = 18f;
                OfficeMath officeMath73 = new OfficeMath(doc);
                p73.Items.Add(officeMath73);
                officeMath73.FromLatexMathCode("σ_c=\\frac{N}{2dt}=\\frac{" + YNLP2.ToString("#.##") + "*1000}{(" + Yd0LP2.ToString() + "-1)" + "*" + YtLP2.ToString() + "}=" + YcLP2.ToString("#.##") + "MPa");



                section0.AddParagraph();
                //添加一个段落p74写耳板材料强度
                Paragraph p74 = section0.Paragraphs[74];//选择这个段落，我觉得有必要
                p74.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p74.Format.LineSpacing = 18f;
                p74.Format.FirstLineIndent = 24f;
                TextRange rangeTop74 = p74.AppendText("由于耳板厚度t=" + YtLP2.ToString() + "mm" + "，" + "耳板材质为" + BoxBLP2.Text + "，" + "则耳板抗拉及拉压强度f=" + YfLP2.ToString() + "MPa，耳板抗剪强度fv=" + YfvLP2.ToString() + "MPa，" + "耳板承压强度fc=" + YfcLP2.ToString() + "MPa。");//选择这个段落，我觉得有必要
                rangeTop74.CharacterFormat.FontSize = 12;
                rangeTop74.CharacterFormat.Bold = false;
                rangeTop74.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop74.CharacterFormat.FontNameNonFarEast = "Times New Roman";




                section0.AddParagraph();//添加一个段落p75判定耳板孔净截面处的抗拉强度；
                Paragraph p75 = section0.Paragraphs[75];//选择这个段落，我觉得有必要
                p75.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p75.Format.LineSpacing = 18f;
                p75.Format.FirstLineIndent = 24f;
                if (Yσ1LP2 <= YfLP2)
                {
                    TextRange rangeTop75 = p75.AppendText("则耳板孔净截面处的抗拉强度：σ=" + Yσ1LP2.ToString("#.##") + "≤" + YfLP2.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop75.CharacterFormat.FontSize = 12;
                    rangeTop75.CharacterFormat.Bold = false;
                    rangeTop75.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop75.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop75 = p75.AppendText("则耳板孔净截面处的抗拉强度：σ=" + Yσ1LP2.ToString("#.##") + ">" + YfLP2.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop75.CharacterFormat.FontSize = 12;
                    rangeTop75.CharacterFormat.Bold = true;
                    rangeTop75.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop75.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop75.CharacterFormat.TextColor = Color.Red;
                }


                section0.AddParagraph();//添加一个段落p76判定耳板端部截面抗拉(劈开）强度；
                Paragraph p76 = section0.Paragraphs[76];//选择这个段落，我觉得有必要
                p76.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p76.Format.LineSpacing = 18f;
                p76.Format.FirstLineIndent = 24f;
                if (Yσ2LP2 <= YfLP2)
                {
                    TextRange rangeTop76 = p76.AppendText("耳板端部截面抗拉(劈开）强度：σ=" + Yσ2LP2.ToString("#.##") + "≤" + YfLP2.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop76.CharacterFormat.FontSize = 12;
                    rangeTop76.CharacterFormat.Bold = false;
                    rangeTop76.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop76.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop76 = p76.AppendText("耳板端部截面抗拉(劈开）强度：σ=" + Yσ2LP2.ToString("#.##") + ">" + YfLP2.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop76.CharacterFormat.FontSize = 12;
                    rangeTop76.CharacterFormat.Bold = true;
                    rangeTop76.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop76.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop76.CharacterFormat.TextColor = Color.Red;
                }





                section0.AddParagraph();//添加一个段落p77判定耳板端部截面抗拉(劈开）强度；
                Paragraph p77 = section0.Paragraphs[77];//选择这个段落，我觉得有必要
                p77.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p77.Format.LineSpacing = 18f;
                p77.Format.FirstLineIndent = 24f;
                if (YτLP2 <= YfvLP2)
                {
                    TextRange rangeTop77 = p77.AppendText("耳板抗剪强度：τ=" + YτLP2.ToString("#.##") + "≤" + YfvLP2.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop77.CharacterFormat.FontSize = 12;
                    rangeTop77.CharacterFormat.Bold = false;
                    rangeTop77.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop77.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop77 = p77.AppendText("耳板抗剪强度：τ=" + YτLP2.ToString("#.##") + ">" + YfvLP2.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop77.CharacterFormat.FontSize = 12;
                    rangeTop77.CharacterFormat.Bold = true;
                    rangeTop77.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop77.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop77.CharacterFormat.TextColor = Color.Red;
                }


                //这里又加了一段，没办法，顺延吧
                section0.AddParagraph();//添加一个段落p78判定耳板承压强度；
                Paragraph p78 = section0.Paragraphs[78];//选择这个段落，我觉得有必要
                p78.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p78.Format.LineSpacing = 18f;
                p78.Format.FirstLineIndent = 24f;
                if (YσcLP2 <= YfcLP2)
                {
                    TextRange rangeTop78 = p78.AppendText("耳板承压强度：σc=" + YσcLP2.ToString("#.##") + "≤" + YfcLP2.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop78.CharacterFormat.FontSize = 12;
                    rangeTop78.CharacterFormat.Bold = false;
                    rangeTop78.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop78.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop78 = p78.AppendText("耳板承压强度：σc=" + YσcLP2.ToString("#.##") + ">" + YfcLP2.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop78.CharacterFormat.FontSize = 12;
                    rangeTop78.CharacterFormat.Bold = true;
                    rangeTop78.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop78.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop78.CharacterFormat.TextColor = Color.Red;
                }



                section0.AddParagraph();//添加一个段落p79总判定；
                Paragraph p79 = section0.Paragraphs[79];//选择这个段落，我觉得有必要
                p79.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p79.Format.LineSpacing = 18f;
                p79.Format.FirstLineIndent = 24f;
                if (YaLP2 >= (YbeLP2 / 3 * 4) & YbLP2 >= YbeLP2 & Yσ1LP2 <= YfLP2 & Yσ2LP2 <= YfLP2 & YτLP2 <= YfvLP2 & YσcLP2 <= YfcLP2)
                {
                    TextRange rangeTop79 = p79.AppendText("综上，耳板构造及强度满足要求。");//选择这个段落，我觉得有必要
                    rangeTop79.CharacterFormat.FontSize = 12;
                    rangeTop79.CharacterFormat.Bold = false;
                    rangeTop79.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop79.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop79 = p79.AppendText("综上，耳板构造及强度中至少有一项不满足要求。");//选择这个段落，我觉得有必要
                    rangeTop79.CharacterFormat.FontSize = 12;
                    rangeTop79.CharacterFormat.Bold = true;
                    rangeTop79.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop79.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop79.CharacterFormat.TextColor = Color.Red;
                }


                section0.AddParagraph();
                //添加一个段落 p80 写第4节标题
                Paragraph p80 = section0.Paragraphs[80];//选择这个段落，我觉得有必要
                p80.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p80.Format.LineSpacing = 18f;
                TextRange rangeTop80 = p80.AppendText("XX.4 吊耳底部焊缝验算");//选择这个段落，我觉得有必要
                rangeTop80.CharacterFormat.FontSize = 16;
                rangeTop80.CharacterFormat.Bold = true;
                rangeTop80.CharacterFormat.FontNameFarEast = "黑体";
                rangeTop80.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p81说明；
                Paragraph p81 = section0.Paragraphs[81];//选择这个段落，我觉得有必要
                p81.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p81.Format.LineSpacing = 18f;
                p81.Format.FirstLineIndent = 24f;
                TextRange rangeTop81 = p81.AppendText("吊耳底部采用全熔透焊缝，根据《钢结构设计标准》公式11.2.1-1及公式11.2.1-2，焊缝强度验算公式分列如下。");//选择这个段落，我觉得有必要
                rangeTop81.CharacterFormat.FontSize = 12;
                rangeTop81.CharacterFormat.Bold = false;
                rangeTop81.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop81.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p82写耳板底部焊缝强度公式1；
                Paragraph p82 = section0.Paragraphs[82];//选择这个段落，我觉得有必要
                p82.Format.LineSpacing = 18f;
                OfficeMath officeMath82 = new OfficeMath(doc);
                p82.Items.Add(officeMath82);
                officeMath82.FromLatexMathCode("σ=\\frac{N}{l_wh_e}≤f_t^w");

                section0.AddParagraph();//添加一个段落p83写耳板底部焊缝强度公式2；
                Paragraph p83 = section0.Paragraphs[83];//选择这个段落，我觉得有必要
                p83.Format.LineSpacing = 18f;
                OfficeMath officeMath83 = new OfficeMath(doc);
                p83.Items.Add(officeMath83);
                officeMath83.FromLatexMathCode("\\sqrt{σ^2+3τ^2}≤1.1f_t^w");


                section0.AddParagraph();//添加一个段落p84说明；
                Paragraph p84 = section0.Paragraphs[84];//选择这个段落，我觉得有必要
                p84.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p84.Format.LineSpacing = 18f;
                p84.Format.FirstLineIndent = 24f;
                TextRange rangeTop84 = p84.AppendText("经过查表，熔透焊缝强度与母材强度相等，即:");//选择这个段落，我觉得有必要
                rangeTop84.CharacterFormat.FontSize = 12;
                rangeTop84.CharacterFormat.Bold = false;
                rangeTop84.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop84.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                OfficeMath officeMath84 = new OfficeMath(doc);
                p84.Items.Add(officeMath84);
                officeMath84.FromLatexMathCode("f_t^w=f");
                TextRange rangeTop84x = p84.AppendText("。");//选择这个段落，我觉得有必要

                section0.AddParagraph();//添加一个段落p85说明；
                Paragraph p85 = section0.Paragraphs[85];//选择这个段落，我觉得有必要
                p85.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p85.Format.LineSpacing = 18f;
                p85.Format.FirstLineIndent = 24f;
                TextRange rangeTop85 = p85.AppendText("双侧吊耳受力分解示意见图XX.4-1。");//选择这个段落，我觉得有必要
                rangeTop85.CharacterFormat.FontSize = 12;
                rangeTop85.CharacterFormat.Bold = false;
                rangeTop85.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop85.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p86，贴算钢丝绳内力的
                Paragraph p86 = section0.Paragraphs[86];//选择这个段落，我觉得有必要
                p86.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片放中间
                p86.Format.LineSpacing = 18f;
                p86.Format.FirstLineIndent = 28f;//图片放中间，不缩进，但是这个图片好像偏的有规律
                string strp86 = System.Windows.Forms.Application.StartupPath;
                Image image86 = Image.FromFile(@strp86 + "\\TP\\算吊耳内力.jpg");
                DocPicture picture86 = section0.Paragraphs[86].AppendPicture(image86);
                picture86.Width = 300;
                picture86.Height = 240;


                section0.AddParagraph();
                //添加一个段落 p87，写钢丝绳内力图名
                Paragraph p87 = section0.Paragraphs[87];//选择这个段落，我觉得有必要
                p87.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p87.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop87 = p87.AppendText("图XX.4-1 吊耳内力计算示意图");
                rangeTop87.CharacterFormat.FontSize = 11;//小四是12，五号是11
                rangeTop87.CharacterFormat.Bold = true;
                rangeTop87.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop87.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p88说明；
                Paragraph p88 = section0.Paragraphs[88];//选择这个段落，我觉得有必要
                p88.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p88.Format.LineSpacing = 18f;
                p88.Format.FirstLineIndent = 24f;
                TextRange rangeTop88 = p88.AppendText("基本组合下，NaS1位置双向分力计算如下。");//选择这个段落，我觉得有必要
                rangeTop88.CharacterFormat.FontSize = 12;
                rangeTop88.CharacterFormat.Bold = false;
                rangeTop88.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop88.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p89说明第一个分力1；
                Paragraph p89 = section0.Paragraphs[89];//选择这个段落，我觉得有必要
                p89.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p89.Format.LineSpacing = 18f;
                p89.Format.FirstLineIndent = 24f;
                TextRange rangeTop89 = p89.AppendText("垂直构件方向：1.5*NaS1*SinaH1=" + NFLP2S1.ToString("#.##") + "kN");//选择这个段落，我觉得有必要
                rangeTop89.CharacterFormat.FontSize = 12;
                rangeTop89.CharacterFormat.Bold = false;
                rangeTop89.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop89.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p90说明第一个分力2；
                Paragraph p90 = section0.Paragraphs[90];//选择这个段落，我觉得有必要
                p90.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p90.Format.LineSpacing = 18f;
                p90.Format.FirstLineIndent = 24f;
                TextRange rangeTop90 = p90.AppendText("沿构件方向：1.5*NaS1*CosaH1=" + NVLP2S1.ToString("#.##") + "kN");//选择这个段落，我觉得有必要
                rangeTop90.CharacterFormat.FontSize = 12;
                rangeTop90.CharacterFormat.Bold = false;
                rangeTop90.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop90.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p91说明；
                Paragraph p91 = section0.Paragraphs[91];//选择这个段落，我觉得有必要
                p91.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p91.Format.LineSpacing = 18f;
                p91.Format.FirstLineIndent = 24f;
                TextRange rangeTop91 = p91.AppendText("基本组合下，NaS2位置双向分力计算如下。");//选择这个段落，我觉得有必要
                rangeTop91.CharacterFormat.FontSize = 12;
                rangeTop91.CharacterFormat.Bold = false;
                rangeTop91.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop91.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p92说明第二个分力1；
                Paragraph p92 = section0.Paragraphs[92];//选择这个段落，我觉得有必要
                p92.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p92.Format.LineSpacing = 18f;
                p92.Format.FirstLineIndent = 24f;
                TextRange rangeTop92 = p92.AppendText("垂直构件方向：1.5*NaS2*SinaH2=" + NFLP2S2.ToString("#.##") + "kN");//选择这个段落，我觉得有必要
                rangeTop92.CharacterFormat.FontSize = 12;
                rangeTop92.CharacterFormat.Bold = false;
                rangeTop92.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop92.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p93说明第二个分力2；
                Paragraph p93 = section0.Paragraphs[93];//选择这个段落，我觉得有必要
                p93.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p93.Format.LineSpacing = 18f;
                p93.Format.FirstLineIndent = 24f;
                TextRange rangeTop93 = p93.AppendText("沿构件方向：1.5*NaS2*CosaH2=" + NVLP2S2.ToString("#.##") + "kN");//选择这个段落，我觉得有必要
                rangeTop93.CharacterFormat.FontSize = 12;
                rangeTop93.CharacterFormat.Bold = false;
                rangeTop93.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop93.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();//添加一个段落p94说明；
                Paragraph p94 = section0.Paragraphs[94];//选择这个段落，我觉得有必要
                p94.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p94.Format.LineSpacing = 18f;
                p94.Format.FirstLineIndent = 24f;
                TextRange rangeTop94 = p94.AppendText("NaS1位置处吊耳位置因拉压产生的应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop94.CharacterFormat.FontSize = 12;
                rangeTop94.CharacterFormat.Bold = false;
                rangeTop94.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop94.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();//添加一个段落p95位置1的拉压；
                Paragraph p95 = section0.Paragraphs[95];//选择这个段落，我觉得有必要
                p95.Format.LineSpacing = 18f;
                OfficeMath officeMath95 = new OfficeMath(doc);
                p95.Items.Add(officeMath95);
                officeMath95.FromLatexMathCode("σ_1=\\frac{N}{2t(e+f)}=\\frac{" + NFLP2S1.ToString("#.##") + "*1000}{2*" + YtLP2.ToString() + "*(" + YeLP2.ToString() + "+" + YffLP2.ToString() + ")}=" + YσF1LP2S1.ToString("#.##") + "MPa"); ;




                section0.AddParagraph();//添加一个段落p96说明；
                Paragraph p96 = section0.Paragraphs[96];//选择这个段落，我觉得有必要
                p96.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p96.Format.LineSpacing = 18f;
                p96.Format.FirstLineIndent = 24f;
                TextRange rangeTop96 = p96.AppendText("NaS1位置处吊耳位置因弯矩产生的应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop96.CharacterFormat.FontSize = 12;
                rangeTop96.CharacterFormat.Bold = false;
                rangeTop96.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop96.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p97位置1的弯矩；
                Paragraph p97 = section0.Paragraphs[97];//选择这个段落，我觉得有必要
                p97.Format.LineSpacing = 18f;
                OfficeMath officeMath97 = new OfficeMath(doc);
                p97.Items.Add(officeMath97);
                officeMath97.FromLatexMathCode("σ_2=\\frac{M}{4t\\frac{(e+f)^2}{6}}=\\frac{" + NVLP2S1.ToString("#.##") + "*1000*(" + Yd0LP2.ToString() + "/2+" + YcLP2.ToString() + ")}{4*" + YtLP2.ToString() + "*\\frac{(" + YeLP2.ToString() + "+" + YffLP2.ToString() + ")^2}{6}}=" + YσF2LP2S1.ToString("#.##") + "MPa"); ;



                section0.AddParagraph();//添加一个段落p98说明；
                Paragraph p98 = section0.Paragraphs[98];//选择这个段落，我觉得有必要
                p98.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p98.Format.LineSpacing = 18f;
                p98.Format.FirstLineIndent = 24f;
                TextRange rangeTop98 = p98.AppendText("NaS1位置处吊耳位置因剪力产生的应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop98.CharacterFormat.FontSize = 12;
                rangeTop98.CharacterFormat.Bold = false;
                rangeTop98.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop98.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();//添加一个段落p99位置1的剪力；
                Paragraph p99 = section0.Paragraphs[99];//选择这个段落，我觉得有必要
                p99.Format.LineSpacing = 18f;
                OfficeMath officeMath99 = new OfficeMath(doc);
                p99.Items.Add(officeMath99);
                officeMath99.FromLatexMathCode("τ=\\frac{V}{2t(e+f)}=\\frac{" + NVLP2S1.ToString("#.##") + "*990)}{2*" + YtLP2.ToString() + "*(" + YeLP2.ToString() + "+" + YffLP2.ToString() + ")}=" + Yτ1LP2S1.ToString("#.##") + "MPa");


                section0.AddParagraph();//添加一个段落p97说明；
                Paragraph p100 = section0.Paragraphs[100];//选择这个段落，我觉得有必要
                p100.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p100.Format.LineSpacing = 18f;
                p100.Format.FirstLineIndent = 24f;
                TextRange rangeTop100 = p100.AppendText("则NaS1位置处吊耳位置三向应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop100.CharacterFormat.FontSize = 12;
                rangeTop100.CharacterFormat.Bold = false;
                rangeTop100.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop100.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p101位置三向应力；
                Paragraph p101 = section0.Paragraphs[101];//选择这个段落，我觉得有必要
                p101.Format.LineSpacing = 18f;
                OfficeMath officeMath101 = new OfficeMath(doc);
                p101.Items.Add(officeMath101);
                officeMath101.FromLatexMathCode("\\sqrt{(σ_1+σ_2)^2+3τ^2}=" + YσFZLP2S1.ToString("#.##") + "MPa");




                //接下来的都是第二段
                //接下来的都是第二段
                //接下来的都是第二段

                section0.AddParagraph();//添加一个段落p102说明；
                Paragraph p102 = section0.Paragraphs[102];//选择这个段落，我觉得有必要
                p102.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p102.Format.LineSpacing = 18f;
                p102.Format.FirstLineIndent = 24f;
                TextRange rangeTop102 = p102.AppendText("NaS2位置处吊耳位置因拉压产生的应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop102.CharacterFormat.FontSize = 12;
                rangeTop102.CharacterFormat.Bold = false;
                rangeTop102.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop102.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();//添加一个段落p103位置1的拉压；
                Paragraph p103 = section0.Paragraphs[103];//选择这个段落，我觉得有必要
                p103.Format.LineSpacing = 18f;
                OfficeMath officeMath103 = new OfficeMath(doc);
                p103.Items.Add(officeMath103);
                officeMath103.FromLatexMathCode("σ_1=\\frac{N}{2t(e+f)}=\\frac{" + NFLP2S2.ToString("#.##") + "*1030}{2*" + YtLP2.ToString() + "*(" + YeLP2.ToString() + "+" + YffLP2.ToString() + ")}=" + YσF1LP2S2.ToString("#.##") + "MPa"); ;




                section0.AddParagraph();//添加一个段落p104说明；
                Paragraph p104 = section0.Paragraphs[104];//选择这个段落，我觉得有必要
                p104.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p104.Format.LineSpacing = 18f;
                p104.Format.FirstLineIndent = 24f;
                TextRange rangeTop104 = p104.AppendText("NaS2位置处吊耳位置因弯矩产生的应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop104.CharacterFormat.FontSize = 12;
                rangeTop104.CharacterFormat.Bold = false;
                rangeTop104.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop104.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p105位置1的弯矩；
                Paragraph p105 = section0.Paragraphs[105];//选择这个段落，我觉得有必要
                p105.Format.LineSpacing = 18f;
                OfficeMath officeMath105 = new OfficeMath(doc);
                p105.Items.Add(officeMath105);
                officeMath105.FromLatexMathCode("σ_2=\\frac{M}{4t\\frac{(e+f)^2}{6}}=\\frac{" + NVLP2S2.ToString("#.##") + "*1000*(" + Yd0LP2.ToString() + "/2+" + YcLP2.ToString() + ")}{4*" + YtLP2.ToString() + "*\\frac{(" + YeLP2.ToString() + "+" + YffLP2.ToString() + ")^2}{6}}=" + YσF2LP2S2.ToString("#.##") + "MPa"); ;



                section0.AddParagraph();//添加一个段落p106说明；
                Paragraph p106 = section0.Paragraphs[106];//选择这个段落，我觉得有必要
                p106.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p106.Format.LineSpacing = 18f;
                p106.Format.FirstLineIndent = 24f;
                TextRange rangeTop106 = p106.AppendText("NaS2位置处吊耳位置因剪力产生的应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop106.CharacterFormat.FontSize = 12;
                rangeTop106.CharacterFormat.Bold = false;
                rangeTop106.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop106.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();//添加一个段落p107位置1的剪力；
                Paragraph p107 = section0.Paragraphs[107];//选择这个段落，我觉得有必要
                p107.Format.LineSpacing = 18f;
                OfficeMath officeMath107 = new OfficeMath(doc);
                p107.Items.Add(officeMath107);
                officeMath107.FromLatexMathCode("τ=\\frac{V}{2t(e+f)}=\\frac{" + NVLP2S2.ToString("#.##") + "*1000)}{2*" + YtLP2.ToString() + "*(" + YeLP2.ToString() + "+" + YffLP2.ToString() + ")}=" + Yτ1LP2S2.ToString("#.##") + "MPa");


                section0.AddParagraph();//添加一个段落p108说明；
                Paragraph p108 = section0.Paragraphs[108];//选择这个段落，我觉得有必要
                p108.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p108.Format.LineSpacing = 18f;
                p108.Format.FirstLineIndent = 24f;
                TextRange rangeTop108 = p108.AppendText("则NaS2位置处吊耳位置三向应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop108.CharacterFormat.FontSize = 12;
                rangeTop108.CharacterFormat.Bold = false;
                rangeTop108.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop108.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p109位置三向应力；
                Paragraph p109 = section0.Paragraphs[109];//选择这个段落，我觉得有必要
                p109.Format.LineSpacing = 18f;
                OfficeMath officeMath109 = new OfficeMath(doc);
                p109.Items.Add(officeMath109);
                officeMath109.FromLatexMathCode("\\sqrt{(σ_1+σ_2)^2+3τ^2}=" + YσFZLP2S2.ToString("#.##") + "MPa");



                section0.AddParagraph();//添加一个段落p110说明；
                Paragraph p110 = section0.Paragraphs[110];//选择这个段落，我觉得有必要
                p110.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p110.Format.LineSpacing = 18f;
                p110.Format.FirstLineIndent = 24f;
                if (Math.Max(YσF1LP2S2, YσF1LP2S2) <= YfLP2)
                {
                    TextRange rangeTop110 = p110.AppendText("最大拉压应力" + Math.Max(YσF1LP2S2, YσF1LP2S2).ToString("#.##") + "≤" + YfLP2.ToString() + "MPa。");//选择这个段落，我觉得有必要
                    rangeTop110.CharacterFormat.FontSize = 12;
                    rangeTop110.CharacterFormat.Bold = false;
                    rangeTop110.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop110.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop110 = p110.AppendText("最大拉压应力" + Math.Max(YσF1LP2S2, YσF1LP2S2).ToString("#.##") + ">" + YfLP2.ToString() + "MPa。");
                    rangeTop110.CharacterFormat.FontSize = 12;
                    rangeTop110.CharacterFormat.Bold = false;
                    rangeTop110.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop110.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }



                section0.AddParagraph();//添加一个段落p111说明；
                Paragraph p111 = section0.Paragraphs[111];//选择这个段落，我觉得有必要
                p111.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p111.Format.LineSpacing = 18f;
                p111.Format.FirstLineIndent = 24f;
                if (Math.Max(YσFZLP2S1, YσFZLP2S2) <= (1.1 * YfLP2))
                {
                    TextRange rangeTop111 = p111.AppendText("最大三向应力" + Math.Max(YσFZLP2S1, YσFZLP2S2).ToString("#.##") + "≤" + (1.1 * YfLP2).ToString() + "MPa。");//选择这个段落，我觉得有必要
                    rangeTop111.CharacterFormat.FontSize = 12;
                    rangeTop111.CharacterFormat.Bold = false;
                    rangeTop111.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop111.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop111 = p111.AppendText("最大三向应力" + Math.Max(YσFZLP2S1, YσFZLP2S2).ToString("#.##") + ">" + (1.1 * YfLP2).ToString() + "MPa。");
                    rangeTop111.CharacterFormat.FontSize = 12;
                    rangeTop111.CharacterFormat.Bold = false;
                    rangeTop111.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop111.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }

                section0.AddParagraph();//添加一个段落p112说明；
                Paragraph p112 = section0.Paragraphs[112];//选择这个段落，我觉得有必要
                p112.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p112.Format.LineSpacing = 18f;
                p112.Format.FirstLineIndent = 24f;
                if (Math.Max(YσFZLP2S1, YσFZLP2S2) <= (1.1 * YfLP2) & Math.Max(YσF1LP2S2, YσF1LP2S2) <= YfLP2)
                {
                    TextRange rangeTop112 = p112.AppendText("满足要求。");//选择这个段落，我觉得有必要
                    rangeTop112.CharacterFormat.FontSize = 12;
                    rangeTop112.CharacterFormat.Bold = false;
                    rangeTop112.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop112.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop112 = p112.AppendText("不满足要求。");
                    rangeTop112.CharacterFormat.FontSize = 12;
                    rangeTop112.CharacterFormat.Bold = true;
                    rangeTop112.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop112.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop112.CharacterFormat.TextColor = Color.Red;
                }

                section0.AddParagraph();
                //添加一个段落 p113写第5节标题
                Paragraph p113 = section0.Paragraphs[113];//选择这个段落，我觉得有必要
                p113.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p113.Format.LineSpacing = 18f;
                TextRange rangeTop113 = p113.AppendText("XX.5 小结");//选择这个段落，我觉得有必要
                rangeTop113.CharacterFormat.FontSize = 16;
                rangeTop113.CharacterFormat.Bold = true;
                rangeTop113.CharacterFormat.FontNameFarEast = "黑体";
                rangeTop113.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();//添加一个段落p114说明；
                Paragraph p114 = section0.Paragraphs[114];//选择这个段落，我觉得有必要
                p114.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p114.Format.LineSpacing = 18f;
                p114.Format.FirstLineIndent = 24f;
                TextRange rangeTop114 = p114.AppendText("根据2~4节计算，可得如下结论。");//选择这个段落，我觉得有必要
                rangeTop114.CharacterFormat.FontSize = 12;
                rangeTop114.CharacterFormat.Bold = false;
                rangeTop114.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop114.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();//添加一个段落p115说明；
                Paragraph p115 = section0.Paragraphs[115];//选择这个段落，我觉得有必要
                p115.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p115.Format.LineSpacing = 18f;
                p115.Format.FirstLineIndent = 24f;
                if (rFgLP2 >= YN2LP2 & rFhLP2 >= YN3LP2)
                {
                    TextRange rangeTop115 = p115.AppendText("（1）吊耳及卡环满足要求；");//选择这个段落，我觉得有必要
                    rangeTop115.CharacterFormat.FontSize = 12;
                    rangeTop115.CharacterFormat.Bold = false;
                    rangeTop115.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop115.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop115 = p115.AppendText("（1）吊耳及卡环中存在不满足项；");//选择这个段落，我觉得有必要
                    rangeTop115.CharacterFormat.FontSize = 12;
                    rangeTop115.CharacterFormat.Bold = true;
                    rangeTop115.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop115.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop115.CharacterFormat.TextColor = Color.Red;
                }



                section0.AddParagraph();//添加一个段落p116说明；
                Paragraph p116 = section0.Paragraphs[116];//选择这个段落，我觉得有必要
                p116.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p116.Format.LineSpacing = 18f;
                p116.Format.FirstLineIndent = 24f;
                if (YaLP2 >= (YbeLP2 / 3 * 4) & YbLP2 >= YbeLP2 & Yσ1LP2 <= YfLP2 & Yσ2LP2 <= YfLP2 & YτLP2 <= YfvLP2)
                {
                    TextRange rangeTop116 = p116.AppendText("（2）吊耳板自身构造及强度满足要求；");//选择这个段落，我觉得有必要
                    rangeTop116.CharacterFormat.FontSize = 12;
                    rangeTop116.CharacterFormat.Bold = false;
                    rangeTop116.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop116.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop116 = p116.AppendText("（2）吊耳板自身构造或强度不满足要求；");//选择这个段落，我觉得有必要
                    rangeTop116.CharacterFormat.FontSize = 12;
                    rangeTop116.CharacterFormat.Bold = true;
                    rangeTop116.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop116.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop116.CharacterFormat.TextColor = Color.Red;
                }





                section0.AddParagraph();//添加一个段落p113说明；
                Paragraph p117 = section0.Paragraphs[117];//选择这个段落，我觉得有必要
                p117.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p117.Format.LineSpacing = 18f;
                p117.Format.FirstLineIndent = 24f;
                if (Math.Max(YσFZLP2S1, YσFZLP2S2) <= (1.1 * YfLP2) & Math.Max(YσF1LP2S2, YσF1LP2S2) <= YfLP2)
                {
                    TextRange rangeTop117 = p117.AppendText("（3）吊耳板底部焊缝强度满足要求；");//选择这个段落，我觉得有必要
                    rangeTop117.CharacterFormat.FontSize = 12;
                    rangeTop117.CharacterFormat.Bold = false;
                    rangeTop117.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop117.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop117 = p117.AppendText("（3）吊耳板底部焊缝强度不满足要求；");//选择这个段落，我觉得有必要
                    rangeTop117.CharacterFormat.FontSize = 12;
                    rangeTop117.CharacterFormat.Bold = true;
                    rangeTop117.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop117.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop117.CharacterFormat.TextColor = Color.Red;
                }



                section0.AddParagraph();//添加一个段落p113说明；
                Paragraph p118 = section0.Paragraphs[118];//选择这个段落，我觉得有必要
                p118.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p118.Format.LineSpacing = 18f;
                p118.Format.FirstLineIndent = 24f;
                if (rFgLP2 >= YN2LP2 & rFhLP2 >= YN3LP2 & YaLP2 >= (YbeLP2 / 3 * 4) & YbLP2 >= YbeLP2 & Yσ1LP2 <= YfLP2 & Yσ2LP2 <= YfLP2 & YτLP2 <= YfvLP2 & YσcLP2 <= YfcLP2 & Math.Max(YσFZLP2S1, YσFZLP2S2) <= (1.1 * YfLP2) & Math.Max(YσF1LP2S2, YσF1LP2S2) <= YfLP2)
                {
                    TextRange rangeTop118 = p118.AppendText("满足要求。");//选择这个段落，我觉得有必要
                    rangeTop118.CharacterFormat.FontSize = 12;
                    rangeTop118.CharacterFormat.Bold = false;
                    rangeTop118.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop118.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop118 = p118.AppendText("不满足要求。");//选择这个段落，我觉得有必要
                    rangeTop118.CharacterFormat.FontSize = 12;
                    rangeTop118.CharacterFormat.Bold = true;
                    rangeTop118.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop118.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop118.CharacterFormat.TextColor = Color.Red;
                }


                //保存文档       
                string ttx = DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒");
                string JSSD1 = System.Windows.Forms.Application.StartupPath;
                //Image image86 = Image.FromFile(@strp86 + "\\算吊耳内力.jpg");
                string bt = System.Windows.Forms.Application.StartupPath+ "\\JSS\\双吊耳" + "（" + ttx + "）" + ".docx";
                doc.SaveToFile(bt, FileFormat.Docx);
                MessageBox.Show(bt.ToString() + "已创建");
                button4.Enabled = true;//

            }

        }

        private void button1X_Click(object sender, EventArgs e)
        {




            if (GSSLL.Text == "暂时不知道" || GSSRXLL.Text == "暂时不知道" || KHFH.Text == "暂时不知道" || GSSLLs.Text == "暂时不知道" || GZ.Text == "暂时不知道" || JKL.Text == "暂时不知道" || PK.Text == "暂时不知道" || KJ.Text == "暂时不知道" || FH.Text == "暂时不知道" || DJ.Text == "暂时不知道" || CY.Text == "暂时不知道")
            {
                MessageBox.Show("计算未完成！");


            }

            else
            {
             
               //这里加图片结束了
                button4.Enabled = false;//生成word的时候不能用，最后用

                Spire.Doc.Document doc = new Spire.Doc.Document();


                //添加一个section0，全文暂时添加一个

                Section section0 = doc.AddSection();

                section0.PageSetup.PageSize = PageSize.Letter;//设置第一页的格式


                //这是设置页边距，55f正好是中等的设置
                section0.PageSetup.Margins.Top = 55f;
                section0.PageSetup.Margins.Left = 55f;
                section0.PageSetup.Margins.Bottom = 55f;
                section0.PageSetup.Margins.Right = 55f;

                section0.AddParagraph();
                //添加一个段落 p0
                Paragraph p0 = section0.Paragraphs[0];//选择这个段落，我觉得有必要


                //插入公式，后面要用
                //OfficeMath officeMath = new OfficeMath(doc);
                //p0.Items.Add(officeMath);
                //officeMath.FromLatexMathCode("a^{2}+\\sqrt{|z|^{2}+1^2+\\frac{(x + y)^2}{x - y}}=2≤DS2dddd");



                //设置左对齐和缩进，第二段要用
                //p0.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                //p0.Format.FirstLineIndent = 24f;

                //设置中对齐
                p0.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                p0.Format.LineSpacing = 18f;
                TextRange rangeTop0 = p0.AppendText("第XX章 四吊耳吊装计算书");//选择这个段落，我觉得有必要
                rangeTop0.CharacterFormat.FontSize = 18;
                rangeTop0.CharacterFormat.Bold = true;
                rangeTop0.CharacterFormat.FontNameFarEast = "黑体";
                rangeTop0.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                //rangeTop0.StyleName.ToUpper(d) ;

                section0.AddParagraph();
                //添加一个段落 p1
                Paragraph p1 = section0.Paragraphs[1];//选择这个段落，我觉得有必要
                p1.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p1.Format.LineSpacing = 18f;
                TextRange rangeTop1 = p1.AppendText("XX.1 工况介绍");//选择这个段落，我觉得有必要
                rangeTop1.CharacterFormat.FontSize = 16;
                rangeTop1.CharacterFormat.Bold = true;
                rangeTop1.CharacterFormat.FontNameFarEast = "黑体";
                rangeTop1.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p2
                Paragraph p2 = section0.Paragraphs[2];//选择这个段落，我觉得有必要
                p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p2.Format.LineSpacing = 18f;
                p2.Format.FirstLineIndent = 24f;

                TextRange rangeTop2 = p2.AppendText("本构件采用四吊耳形式吊装" + "，" + "吊装示意见图XX.1-1" + "，" + "其中构件重量G4=" + tG.Text + "t" + "，" + "吊点竖向距离H=" + tH.Text + "mm" + "，" + "吊点纵向间距L1=" + tL1.Text + "mm" + "，" + "吊点横向间距L2=" + tL2.Text + "mm" + "。");//添加文字段落1
                rangeTop2.CharacterFormat.FontSize = 12;
                rangeTop2.CharacterFormat.Bold = false;
                rangeTop2.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop2.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p3
                Paragraph p3 = section0.Paragraphs[3];//选择这个段落，我觉得有必要
                p3.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片放中间
                p3.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片放中间，不缩进
                string strp3 = System.Windows.Forms.Application.StartupPath;
                Image image3 = Image.FromFile(@strp3 + "\\TP\\四吊耳示意.jpg");
                DocPicture picture3 = section0.Paragraphs[3].AppendPicture(image3);
                picture3.Width = 340;
                picture3.Height = 230;

                section0.AddParagraph();
                //添加一个段落 p4
                Paragraph p4 = section0.Paragraphs[4];//选择这个段落，我觉得有必要
                p4.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p4.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop4 = p4.AppendText("图XX.1-1 吊装示意图");
                rangeTop4.CharacterFormat.FontSize = 11;
                rangeTop4.CharacterFormat.Bold = true;
                rangeTop4.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop4.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p5 这是第六个 写吊耳板详情的
                Paragraph p5 = section0.Paragraphs[5];//选择这个段落，我觉得有必要
                p5.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;//文字左对齐
                p5.Format.LineSpacing = 18f;
                p5.Format.FirstLineIndent = 24f;//普通文字段缩进
                TextRange rangeTop5 = p5.AppendText("工况采用吊耳尺寸见图XX.1-2" + "，" + "其中受力方向最小净距a=" + ta.Text + "mm" + "，" + "双侧边缘净距b=" + tb.Text + "mm" + "，"
                      + "销轴孔径d0=" + td0.Text + "mm" + "，" + "底部补长c=" + tc.Text + "mm" + "，" + "加劲肋边距e=" + te.Text + "mm" + "，" + "加劲肋中距f=" + tf.Text + "mm" + "，" + "耳板厚度t=" + tt.Text + "mm" + "，" + "耳板材质为" + BoxB.Text + "。");//ttLP2
                rangeTop5.CharacterFormat.FontSize = 12;//小四是12，五号是11
                rangeTop5.CharacterFormat.Bold = false;
                rangeTop5.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop5.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p6 贴吊耳图片1的
                Paragraph p6 = section0.Paragraphs[6];//选择这个段落，我觉得有必要
                p6.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片放中间
                p6.Format.LineSpacing = 18f;
                p6.Format.FirstLineIndent = 28f;//图片放中间，不缩进，但是这个图片好像偏的有规律
                string strp6 = System.Windows.Forms.Application.StartupPath;//这个路径就是增加cad图片d1的位置
                Image image6 = Image.FromFile(@strp6 + "\\TP\\正面的.jpg");
                DocPicture picture6 = section0.Paragraphs[6].AppendPicture(image6);
                picture6.Width = 220;
                picture6.Height = 180;

                section0.AddParagraph();
                //添加一个段落 p7，写正立面图名的
                Paragraph p7 = section0.Paragraphs[7];//选择这个段落，我觉得有必要
                p7.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p7.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop7 = p7.AppendText("（a）正立面");
                rangeTop7.CharacterFormat.FontSize = 11;
                rangeTop7.CharacterFormat.Bold = true;
                rangeTop7.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop7.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p8贴吊耳侧面图的
                Paragraph p8 = section0.Paragraphs[8];//选择这个段落，我觉得有必要
                p8.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片放中间
                p8.Format.LineSpacing = 18f;
                p8.Format.FirstLineIndent = 28f;//图片放中间，不缩进，但是这个图片好像偏的有规律
                string strp8 = System.Windows.Forms.Application.StartupPath;//这个路径就是增加cad图片d2的位置
                Image image8 = Image.FromFile(@strp8 + "\\TP\\侧面的.jpg");
                DocPicture picture8 = section0.Paragraphs[8].AppendPicture(image8);
                picture8.Width = 200;
                picture8.Height = 160;


                section0.AddParagraph();
                //添加一个段落 p9，写侧立面图名的
                Paragraph p9 = section0.Paragraphs[9];//选择这个段落，我觉得有必要
                p9.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p9.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop9 = p9.AppendText("（b）侧立面");
                rangeTop9.CharacterFormat.FontSize = 11;
                rangeTop9.CharacterFormat.Bold = true;
                rangeTop9.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop9.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();
                //添加一个段落 p10，贴剖面图的
                Paragraph p10 = section0.Paragraphs[10];//选择这个段落，我觉得有必要
                p10.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片放中间
                p10.Format.LineSpacing = 18f;
                p10.Format.FirstLineIndent = 28f;//图片放中间，不缩进，但是这个图片好像偏的有规律
                string strp10 = System.Windows.Forms.Application.StartupPath;//这个路径就是增加cad图片d3的位置
                Image image10 = Image.FromFile(@strp10 + "\\TP\\剖面的.jpg");
                DocPicture picture10 = section0.Paragraphs[10].AppendPicture(image10);
                picture10.Width = 170;
                picture10.Height = 50;

                section0.AddParagraph();
                //添加一个段落 p11，写剖面图名的
                Paragraph p11 = section0.Paragraphs[11];//选择这个段落，我觉得有必要
                p11.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p11.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop11 = p11.AppendText("（c）底面图");
                rangeTop11.CharacterFormat.FontSize = 11;
                rangeTop11.CharacterFormat.Bold = true;
                rangeTop11.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop11.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p12，写尺寸图名的
                Paragraph p12 = section0.Paragraphs[12];//选择这个段落，我觉得有必要
                p12.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p12.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop12 = p12.AppendText("图XX.1-2 吊耳尺寸图");
                rangeTop12.CharacterFormat.FontSize = 11;//小四是12，五号是11
                rangeTop12.CharacterFormat.Bold = true;
                rangeTop12.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop12.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p13 写
                Paragraph p13 = section0.Paragraphs[13];//选择这个段落，我觉得有必要
                p13.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p13.Format.LineSpacing = 18f;
                TextRange rangeTop13 = p13.AppendText("XX.2 钢丝绳验算");//选择这个段落，我觉得有必要
                rangeTop13.CharacterFormat.FontSize = 16;
                rangeTop13.CharacterFormat.Bold = true;
                rangeTop13.CharacterFormat.FontNameFarEast = "黑体";
                rangeTop13.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();
                //添加一个段落 p14 
                Paragraph p14 = section0.Paragraphs[14];//选择这个段落，我觉得有必要
                p14.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p14.Format.LineSpacing = 18f;
                p14.Format.FirstLineIndent = 24f;

                TextRange rangeTop14 = p14.AppendText("取两根钢丝绳作为研究体，吊点受到三向拉力" + "，" + "钢丝绳受力示意见图XX.2-1" + "。");//选择这个段落，我觉得有必要
                rangeTop14.CharacterFormat.FontSize = 12;
                rangeTop14.CharacterFormat.Bold = false;
                rangeTop14.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop14.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();
                //添加一个段落 p15，贴算钢丝绳内力的
                Paragraph p15 = section0.Paragraphs[15];//选择这个段落，我觉得有必要
                p15.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片放中间
                p15.Format.LineSpacing = 18f;
                p15.Format.FirstLineIndent = 28f;//图片放中间，不缩进，但是这个图片好像偏的有规律
                string strp15 = System.Windows.Forms.Application.StartupPath;
                Image image15 = Image.FromFile(@strp15 + "\\TP\\四吊耳算钢丝绳内力.jpg");
                DocPicture picture15 = section0.Paragraphs[15].AppendPicture(image15);
                picture15.Width = 272;
                picture15.Height = 240;


                section0.AddParagraph();
                //添加一个段落 p16，写钢丝绳内力图名
                Paragraph p16 = section0.Paragraphs[16];//选择这个段落，我觉得有必要
                p16.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p16.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop16 = p16.AppendText("图XX.2-1 钢丝绳内力计算示意图");
                rangeTop16.CharacterFormat.FontSize = 11;//小四是12，五号是11
                rangeTop16.CharacterFormat.Bold = true;
                rangeTop16.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop16.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();
                //添加一个段落p17开始求解钢丝绳内力
                Paragraph p17 = section0.Paragraphs[17];//选择这个段落，我觉得有必要
                p17.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p17.Format.LineSpacing = 18f;
                p17.Format.FirstLineIndent = 24f;
                TextRange rangeTop17 = p17.AppendText("本工况动力系数为" + BoxDD.Text + "，而竖向平面角正弦值计算如下：");//选择这个段落，我觉得有必要
                rangeTop17.CharacterFormat.FontSize = 12;
                rangeTop17.CharacterFormat.Bold = false;
                rangeTop17.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop17.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                //本段加公式

                section0.AddParagraph();//18行写NaS2的计算结果
                Paragraph p18 = section0.Paragraphs[18];//选择这个段落，我觉得有必要
                p18.Format.LineSpacing = 18f;
                OfficeMath officeMath18 = new OfficeMath(doc);
                p18.Items.Add(officeMath18);
                double ZX = (Convert.ToDouble(tH.Text)) / (Math.Sqrt(Convert.ToDouble(tL1.Text) * Convert.ToDouble(tL1.Text) / 4 + Convert.ToDouble(tL2.Text) * Convert.ToDouble(tL2.Text) / 4 + Convert.ToDouble(tH.Text) * Convert.ToDouble(tH.Text) ));
                officeMath18.FromLatexMathCode("Sinθ=\\frac{H}{\\sqrt{" + "(L1/2)^2+" + "(L2/2)^2+" + "H^2" + "}}" + "=\\frac{" + tH.Text.ToString() + "}{\\sqrt{" + tL1.Text.ToString() + "^2/4+" + tL2.Text.ToString() + "^2/4+" + tH.Text.ToString() + "^2"+"}}"+"=" +ZX.ToString("0.00"));




                section0.AddParagraph();
                //添加一个段落p19开始求解钢丝绳内力
                Paragraph p19 = section0.Paragraphs[19];//选择这个段落，我觉得有必要
                p19.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p19.Format.LineSpacing = 18f;
                p19.Format.FirstLineIndent = 24f;
                TextRange rangeTop19 = p19.AppendText("钢丝绳内力标准值计算如下。");//选择这个段落，我觉得有必要
                rangeTop19.CharacterFormat.FontSize = 12;
                rangeTop19.CharacterFormat.Bold = false;
                rangeTop19.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop19.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//20行写NaS2的计算结果
                Paragraph p20 = section0.Paragraphs[20];//选择这个段落，我觉得有必要
                p20.Format.LineSpacing = 18f;
                OfficeMath officeMath20 = new OfficeMath(doc);
                p20.Items.Add(officeMath20);
                officeMath20.FromLatexMathCode("N=\\frac{"+ BoxDD .Text+"* "+tG.Text+ "* 10}{4*"+ZX.ToString("0.00")+"}="+ GSSLL.Text.ToString()+"kN");


              section0.AddParagraph();
             // 添加一个段落p21开始求解钢丝绳内力
              Paragraph p21 = section0.Paragraphs[21];//选择这个段落，我觉得有必要
              p21.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
              p21.Format.LineSpacing = 18f;
              p21.Format.FirstLineIndent = 24f;
            TextRange rangeTop21 = p21.AppendText("本工况所选择的钢丝绳材质：" + BOXS.Text + "，型号为：" + BoxX.Text + "，直径为：" + BoxZJ.Text + "mm，" +
            "根据《建筑施工计算手册》查取，其破断拉力总和为" + Fg.ToString() + "kN。");//选择这个段落，我觉得有必要
               rangeTop21.CharacterFormat.FontSize = 12;
               rangeTop21.CharacterFormat.Bold = false;
               rangeTop21.CharacterFormat.FontNameFarEast = "宋体";
               rangeTop21.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();
                //添加一个段落p22开始求解钢丝绳内力
                Paragraph p22 = section0.Paragraphs[22];//选择这个段落，我觉得有必要
                p22.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p22.Format.LineSpacing = 18f;
                p22.Format.FirstLineIndent = 24f;
                TextRange rangeTop22 = p22.AppendText("钢丝绳容许拉力根据《建筑施工计算手册》公式13-3计算，公式如下。");//选择这个段落，我觉得有必要
                rangeTop22.CharacterFormat.FontSize = 12;
                rangeTop22.CharacterFormat.Bold = false;
                rangeTop22.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop22.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//23行写NaS1的计算结果
                Paragraph p23 = section0.Paragraphs[23];//选择这个段落，我觉得有必要
                p23.Format.LineSpacing = 18f;
                OfficeMath officeMath23 = new OfficeMath(doc);
                p23.Items.Add(officeMath23);
                officeMath23.FromLatexMathCode("[Fg]=\\frac{αFg}{K}");

                section0.AddParagraph();
                //添加一个段落p24开始求解钢丝绳内力
                Paragraph p24 = section0.Paragraphs[24];//选择这个段落，我觉得有必要
                p24.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p24.Format.LineSpacing = 18f;
                p24.Format.FirstLineIndent = 24f;
                TextRange rangeTop24 = p24.AppendText("由于钢丝绳型号为：" + BoxX.Text + "，不均匀系数α取值" + α.ToString() + "，安全系数K选择取值为" + K.ToString() + "，则钢丝绳容许压力[Fg]计算如下。");//选择这个段落，我觉得有必要
                rangeTop24.CharacterFormat.FontSize = 12;
                rangeTop24.CharacterFormat.Bold = false;
                rangeTop24.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop24.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();//25行写[Fg]的计算结果
                Paragraph p25 = section0.Paragraphs[25];//选择这个段落，我觉得有必要
                p25.Format.LineSpacing = 18f;
                OfficeMath officeMath25 = new OfficeMath(doc);
                p25.Items.Add(officeMath25);
                officeMath25.FromLatexMathCode("[Fg]=\\frac{" + α.ToString() + "*" + Fg.ToString() + "}{" + K.ToString() + "}" + "=" + rFg.ToString("#.##") + "kN");

                section0.AddParagraph();
                Paragraph p26 = section0.Paragraphs[26];//选择这个段落，我觉得有必要
                p26.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p26.Format.LineSpacing = 18f;
                p26.Format.FirstLineIndent = 24f;

                if (rFg< YN2)
                {
                    TextRange rangeTop26 = p26.AppendText("由于钢丝绳拉力" + YN2.ToString("#.##") + ">" + rFg.ToString("#.##") + "kN，钢丝绳满足受力不要求。");
                    rangeTop26.CharacterFormat.FontSize = 12;
                    rangeTop26.CharacterFormat.Bold = true;
                    rangeTop26.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop26.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop26.CharacterFormat.TextColor = Color.Red;
                }//选择这个段落，我觉得有必要
                else
                {
                    TextRange rangeTop26 = p26.AppendText("由于最大钢丝绳拉力" + YN2.ToString("#.##") + "≤" + rFg.ToString("#.##") + "kN，钢丝绳满足受力要求。");
                    rangeTop26.CharacterFormat.FontSize = 12;
                    rangeTop26.CharacterFormat.Bold = false;
                    rangeTop26.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop26.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }



                section0.AddParagraph();
                //添加一个段落p27开始写卡环
                Paragraph p27 = section0.Paragraphs[27];//选择这个段落，我觉得有必要
                p27.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p27.Format.LineSpacing = 18f;
                p27.Format.FirstLineIndent = 24f;
                if (rFh < YN3)
                {
                    TextRange rangeTop27 = p27.AppendText("本工况选择的卡环型号为：" + BoxKH.Text + "，根据《建筑施工计算手册》，卡环使用负荷[Fj]为" + rFh.ToString("#.##") + "kN，由于" + YN3.ToString("#.##") + ">" + rFh.ToString() + "kN" + "，卡环不满足要求。");//选择这个段落，我觉得有必要
                    rangeTop27.CharacterFormat.FontSize = 12;
                    rangeTop27.CharacterFormat.Bold = true;
                    rangeTop27.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop27.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop27.CharacterFormat.TextColor = Color.Red;
                }
                else
                {
                    TextRange rangeTop27 = p27.AppendText("本工况选择的卡环型号为：" + BoxKH.Text + "，根据《建筑施工计算手册》，卡环使用负荷[Fj]为" + rFh.ToString("#.##") + "kN，由于" + YN3.ToString("#.##") + "≤" + rFh.ToString() + "kN" + "，卡环满足要求。");//选择这个段落，我觉得有必要
                    rangeTop27.CharacterFormat.FontSize = 12;
                    rangeTop27.CharacterFormat.Bold = false;
                    rangeTop27.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop27.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }

                section0.AddParagraph();
                //添加一个段落 p28写第三个标题
                Paragraph p28 = section0.Paragraphs[28];//选择这个段落，我觉得有必要
                p28.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p28.Format.LineSpacing = 18f;
                TextRange rangeTop28 = p28.AppendText("XX.3 吊耳验算");//选择这个段落，我觉得有必要
                rangeTop28.CharacterFormat.FontSize = 16;
                rangeTop28.CharacterFormat.Bold = true;
                rangeTop28.CharacterFormat.FontNameFarEast = "黑体";
                rangeTop28.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落p29开始求解钢丝绳内力
                Paragraph p29 = section0.Paragraphs[29];//选择这个段落，我觉得有必要
                p29.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p29.Format.LineSpacing = 18f;
                p29.Format.FirstLineIndent = 24f;
                TextRange rangeTop29 = p29.AppendText("根据《钢结构设计标准》11.6.2条，吊耳板应满足如下要求。");//选择这个段落，我觉得有必要
                rangeTop29.CharacterFormat.FontSize = 12;
                rangeTop29.CharacterFormat.Bold = false;
                rangeTop29.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop29.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//30行写吊耳板构造
                Paragraph p30 = section0.Paragraphs[30];//选择这个段落，我觉得有必要
                p30.Format.LineSpacing = 18f;
                OfficeMath officeMath30 = new OfficeMath(doc);
                p30.Items.Add(officeMath30);
                officeMath30.FromLatexMathCode("a≥\\frac{4}{3}be");

                section0.AddParagraph();//31行写吊耳板构造2
                Paragraph p31 = section0.Paragraphs[31];//选择这个段落，我觉得有必要
                p31.Format.LineSpacing = 18f;
                OfficeMath officeMath31 = new OfficeMath(doc);
                p31.Items.Add(officeMath31);
                officeMath31.FromLatexMathCode("be=2t+16≤b");

                section0.AddParagraph();
                //添加一个段落p32写构造结果1
                Paragraph p32 = section0.Paragraphs[32];//选择这个段落，我觉得有必要
                p32.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p32.Format.LineSpacing = 18f;
                p32.Format.FirstLineIndent = 24f;
                TextRange rangeTop32 = p32.AppendText("则");//选择这个段落，我觉得有必要
                rangeTop32.CharacterFormat.FontSize = 12;
                rangeTop32.CharacterFormat.Bold = false;
                rangeTop32.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop32.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                OfficeMath officeMath32 = new OfficeMath(doc);
                p32.Items.Add(officeMath32);
                officeMath32.FromLatexMathCode("be=" + "2*" + Yt.ToString() + "+16=" + Ybe.ToString());
                TextRange rangeTop32X1 = p32.AppendText("。");//选择这个段落，我觉得有必要


                section0.AddParagraph();
                //添加一个段落p33写构造结果2
                Paragraph p33 = section0.Paragraphs[33];//选择这个段落，我觉得有必要
                p33.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p33.Format.LineSpacing = 18f;
                p33.Format.FirstLineIndent = 24f;
                TextRange rangeTop33 = p33.AppendText("由于a=" + Ya.ToString() + "，" + "b=" + Yb.ToString() + "。");//选择这个段落，我觉得有必要
                rangeTop33.CharacterFormat.FontSize = 12;
                rangeTop33.CharacterFormat.Bold = false;
                rangeTop33.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop33.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落p34写构造结果2
                Paragraph p34 = section0.Paragraphs[34];//选择这个段落，我觉得有必要
                p34.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p34.Format.LineSpacing = 18f;
                p34.Format.FirstLineIndent = 24f;
                if (YaLP2 < (YbeLP2 / 3 * 4))
                {
                    OfficeMath officeMath34 = new OfficeMath(doc);
                    p34.Items.Add(officeMath34);
                    officeMath34.FromLatexMathCode("a<\\frac{4}{3}be");
                }
                else
                {
                    OfficeMath officeMath34 = new OfficeMath(doc);
                    p34.Items.Add(officeMath34);
                    officeMath34.FromLatexMathCode("a≥\\frac{4}{3}be");
                }

                section0.AddParagraph();
                //添加一个段落p35写构造结果2
                Paragraph p35 = section0.Paragraphs[35];//选择这个段落，我觉得有必要
                p35.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p35.Format.LineSpacing = 18f;
                p35.Format.FirstLineIndent = 24f;
                if (Yb < Ybe)
                {
                    OfficeMath officeMath35 = new OfficeMath(doc);
                    p35.Items.Add(officeMath35);
                    officeMath35.FromLatexMathCode("b<be");
                }
                else
                {
                    OfficeMath officeMath35 = new OfficeMath(doc);
                    p35.Items.Add(officeMath35);
                    officeMath35.FromLatexMathCode("b≥be");
                }


                section0.AddParagraph();
                //添加一个段落p36写构造结果2
                Paragraph p36 = section0.Paragraphs[36];//选择这个段落，我觉得有必要
                p36.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p36.Format.LineSpacing = 18f;
                p36.Format.FirstLineIndent = 24f;

                if (Ya >= (Ybe / 3 * 4) & Yb >= Ybe)
                {
                    TextRange rangeTop36 = p36.AppendText("构造满足要求。");//选择这个段落，我觉得有必要
                    rangeTop36.CharacterFormat.FontSize = 12;
                    rangeTop36.CharacterFormat.Bold = false;
                    rangeTop36.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop36.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop36 = p36.AppendText("构造不满足要求。");//选择这个段落，我觉得有必要
                    rangeTop36.CharacterFormat.FontSize = 12;
                    rangeTop36.CharacterFormat.Bold = true;
                    rangeTop36.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop36.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop36.CharacterFormat.TextColor = Color.Red;
                }

 

                ////这一段是吊耳板《钢结构设计标准》的计算内容

                //if (Yσ1 < Yf)
                //{ JKL.Text = "σ" + ":" + Yσ1.ToString("#.##") + "<" + "f" + ":" + Yf.ToString() + "," + "满足"; JKL.ForeColor = Color.Black; }
                //else
                //{ JKL.Text = "σ" + ":" + Yσ1.ToString("#.##") + ">" + "f" + ":" + Yf.ToString() + "," + "不满足"; JKL.ForeColor = Color.Red; }

                //if (Yσ2 < Yf)
                //{ PK.Text = "σ" + ":" + Yσ2.ToString("#.##") + "<" + "f" + ":" + Yf.ToString() + "," + "满足"; PK.ForeColor = Color.Black; }
                //else
                //{ PK.Text = "σ" + ":" + Yσ2.ToString("#.##") + ">" + "f" + ":" + Yf.ToString() + "," + "不满足"; PK.ForeColor = Color.Red; }

                //if (Yτ < Yfv)
                //{ KJ.Text = "τ" + ":" + Yτ.ToString("#.##") + "<" + "fv" + ":" + Yfv.ToString() + "," + "满足"; KJ.ForeColor = Color.Black; }
                //else
                //{ KJ.Text = "τ" + ":" + Yτ.ToString("#.##") + ">" + "fv" + ":" + Yfv.ToString() + "," + "不满足"; KJ.ForeColor = Color.Red; }

                //if (Yσc < Yfc)
                //{ CY.Text = "σ" + ":" + Yσc.ToString("#.##") + "<" + "f" + ":" + Yfc.ToString() + "," + "满足"; CY.ForeColor = Color.Black; }
                //else
                //{ CY.Text = "σ" + ":" + Yσc.ToString("#.##") + ">" + "f" + ":" + Yfc.ToString() + "," + "不满足"; CY.ForeColor = Color.Red; }

                ////这一段是吊耳板《钢结构设计标准》底部受力及加劲板是否干涉的计算
                //YBJ = Math.Sqrt((Yff - 6) * (Yff - 6) + (Math.Abs(Yc - Yg) + Yd0 / 2) * (Math.Abs(Yc - Yg) + Yd0 / 2)) - Yd0 / 2;
                //if ((2 * Ye + 2 * Yff) < (2 * Yb + Yd0)) { MessageBox.Show("底部一般不小于顶部宽度，可以增加e，f值"); }
                //else if (Yc < Yg || Yc < Yb) { MessageBox.Show("底部补长a一般比加劲肋边长g高，且不小于双侧边缘净距b"); }
                //else if ((YBJ) < 50) { MessageBox.Show("加劲肋距离孔边有点近，可以增加e，f值"); }
                //else
                //{
                //    YCθ = Math.Sqrt(1 - YSθ * YSθ);//表示角度的余弦
         

                section0.AddParagraph();
                //添加一个段落p37写N设计值
                Paragraph p37 = section0.Paragraphs[37];//选择这个段落，我觉得有必要
                p37.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p37.Format.LineSpacing = 18f;
                p37.Format.FirstLineIndent = 24f;
                TextRange rangeTop37 = p37.AppendText("用于吊耳计算时采用的钢丝绳拉力设计值N=1.5*" + YN2.ToString("#.##") + "="+YN.ToString("#.##") + "kN。");//选择这个段落，我觉得有必要
                rangeTop37.CharacterFormat.FontSize = 12;
                rangeTop37.CharacterFormat.Bold = false;
                rangeTop37.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop37.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落p38写构造结果2
                Paragraph p38 = section0.Paragraphs[38];//选择这个段落，我觉得有必要
                p38.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p38.Format.LineSpacing = 18f;
                p38.Format.FirstLineIndent = 24f;
                TextRange rangeTop38 = p38.AppendText("根据《钢结构设计标准》公式11.6.3-1及11.6.3-2，耳板孔净截面处的抗拉应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop38.CharacterFormat.FontSize = 12;
                rangeTop38.CharacterFormat.Bold = false;
                rangeTop38.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop38.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();//39行耳板孔净截面处的抗拉强度公式2:
                Paragraph p39 = section0.Paragraphs[39];//选择这个段落，我觉得有必要
                p39.Format.LineSpacing = 18f;
                OfficeMath officeMath39 = new OfficeMath(doc);
                p39.Items.Add(officeMath39);
                officeMath39.FromLatexMathCode("b1=min(2t+16，b-\\frac{d0}{3})=min(2*" + Yt.ToString() + "+16，" + Yb.ToString() + "-\\frac{" + Yd0.ToString()
                    + "}{3})=" + Yb1.ToString() + "mm");

                section0.AddParagraph();//40行耳板孔净截面处的抗拉强度公式1:
                Paragraph p40 = section0.Paragraphs[40];//选择这个段落，我觉得有必要
                p40.Format.LineSpacing = 18f;
                OfficeMath officeMath40 = new OfficeMath(doc);
                p40.Items.Add(officeMath40);
                officeMath40.FromLatexMathCode("σ=\\frac{N}{2tb1}=\\frac{" + YN.ToString("#.##") + "*1000}{2*" + Yt.ToString() + "*" + Yb1.ToString() + "}=" + Yσ1.ToString("#.##") + "MPa");




                section0.AddParagraph();
                //添加一个段落p41写耳板端部截面抗拉(劈开）强度
                Paragraph p41 = section0.Paragraphs[41];//选择这个段落，我觉得有必要
                p41.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p41.Format.LineSpacing = 18f;
                p41.Format.FirstLineIndent = 24f;
                TextRange rangeTop41 = p41.AppendText("根据《钢结构设计标准》公式11.6.3-3，耳板端部截面抗拉(劈开）应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop41.CharacterFormat.FontSize = 12;
                rangeTop41.CharacterFormat.Bold = false;
                rangeTop41.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop41.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();//添加一个段落p67写耳板端部截面抗拉(劈开）强度；
                Paragraph p42 = section0.Paragraphs[42];//选择这个段落，我觉得有必要
                p42.Format.LineSpacing = 18f;
                OfficeMath officeMath42 = new OfficeMath(doc);
                p42.Items.Add(officeMath42);
                officeMath42.FromLatexMathCode("σ=\\frac{N}{2t(a-\\frac{2d0}{3})}=\\frac{" + YN.ToString("#.##") + "*1000}{2*" + Yt.ToString() + "*(" + Ya.ToString() + "-\\frac{2*" + Yd0.ToString() + "}" + "{3})" + "}=" + Yσ2.ToString("#.##") + "MPa");



                section0.AddParagraph();
                //添加一个段落p43写耳板抗剪强度
                Paragraph p43 = section0.Paragraphs[43];//选择这个段落，我觉得有必要
                p43.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p43.Format.LineSpacing = 18f;
                p43.Format.FirstLineIndent = 24f;
                TextRange rangeTop43 = p43.AppendText("根据《钢结构设计标准》公式11.6.3-4及11.6.3-5，耳板抗剪应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop43.CharacterFormat.FontSize = 12;
                rangeTop43.CharacterFormat.Bold = false;
                rangeTop43.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop43.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();//添加一个段落p44写耳板抗剪强度公式2；
                Paragraph p44 = section0.Paragraphs[44];//选择这个段落，我觉得有必要
                p44.Format.LineSpacing = 18f;
                OfficeMath officeMath44 = new OfficeMath(doc);
                p44.Items.Add(officeMath44);
                officeMath44.FromLatexMathCode("Z=\\sqrt{(a+d0/2)^2-(d0/2)^2}=\\sqrt{(" + Ya.ToString() + "+" + Yd0.ToString() + "/2)^2-(" + Yd0.ToString() + "/2)^2}=" + YZ.ToString("#.##") + "mm");

                section0.AddParagraph();//添加一个段落p45写耳板抗剪强度公式1；
                Paragraph p45 = section0.Paragraphs[45];//选择这个段落，我觉得有必要
                p45.Format.LineSpacing = 18f;
                OfficeMath officeMath45 = new OfficeMath(doc);
                p45.Items.Add(officeMath45);
                officeMath45.FromLatexMathCode("τ=\\frac{N}{2tZ}=\\frac{" + YN.ToString("#.##") + "*1000}{2*" + Yt.ToString() + "*" + YZ.ToString("#.##") + "}=" + Yτ.ToString("#.##") + "MPa");


                //这里增加局部抗压，由于是另外加入，所以需要在71后面加后缀//TMD,顺延把

                section0.AddParagraph();
                //添加一个段落p69写耳板局部受压强度
                Paragraph p46 = section0.Paragraphs[46];//选择这个段落，我觉得有必要
                p46.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p46.Format.LineSpacing = 18f;
                p46.Format.FirstLineIndent = 24f;
                TextRange rangeTop46 = p46.AppendText("根据《钢结构设计标准》公式11.6.6-1，耳板承压强度计算如下。");//选择这个段落，我觉得有必要
                rangeTop46.CharacterFormat.FontSize = 12;
                rangeTop46.CharacterFormat.Bold = false;
                rangeTop46.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop46.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();//添加一个段落p47写耳板承压强度公式2；
                Paragraph p47 = section0.Paragraphs[47];//选择这个段落，我觉得有必要
                p47.Format.LineSpacing = 18f;
                OfficeMath officeMath47 = new OfficeMath(doc);
                p47.Items.Add(officeMath47);
                officeMath47.FromLatexMathCode("σ_c=\\frac{N}{2dt}=\\frac{" + YN.ToString("#.##") + "*1000}{(" + Yd0.ToString() + "-1)" + "*" + Yt.ToString() + "}=" + Yc.ToString("#.##") + "MPa");



                section0.AddParagraph();
                //添加一个段落p48写耳板材料强度
                Paragraph p48 = section0.Paragraphs[48];//选择这个段落，我觉得有必要
                p48.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p48.Format.LineSpacing = 18f;
                p48.Format.FirstLineIndent = 24f;
                TextRange rangeTop48 = p48.AppendText("由于耳板厚度t=" + Yt.ToString() + "mm" + "，" + "耳板材质为" + BoxB.Text + "，" + "则耳板抗拉及拉压强度f=" + Yf.ToString() + "MPa，耳板抗剪强度fv=" + Yfv.ToString() + "MPa，" + "耳板承压强度fc=" + Yfc.ToString() + "MPa。");//选择这个段落，我觉得有必要
                rangeTop48.CharacterFormat.FontSize = 12;
                rangeTop48.CharacterFormat.Bold = false;
                rangeTop48.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop48.CharacterFormat.FontNameNonFarEast = "Times New Roman";




                section0.AddParagraph();//添加一个段落p49判定耳板孔净截面处的抗拉强度；
                Paragraph p49 = section0.Paragraphs[49];//选择这个段落，我觉得有必要
                p49.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p49.Format.LineSpacing = 18f;
                p49.Format.FirstLineIndent = 24f;
                if (Yσ1<= Yf)
                {
                    TextRange rangeTop49 = p49.AppendText("则耳板孔净截面处的抗拉强度：σ=" + Yσ1.ToString("#.##") + "≤" + Yf.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop49.CharacterFormat.FontSize = 12;
                    rangeTop49.CharacterFormat.Bold = false;
                    rangeTop49.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop49.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop49 = p49.AppendText("则耳板孔净截面处的抗拉强度：σ=" + Yσ1.ToString("#.##") + ">" + Yf.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop49.CharacterFormat.FontSize = 12;
                    rangeTop49.CharacterFormat.Bold = true;
                    rangeTop49.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop49.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop49.CharacterFormat.TextColor = Color.Red;
                }


                section0.AddParagraph();//添加一个段落p50判定耳板端部截面抗拉(劈开）强度；
                Paragraph p50 = section0.Paragraphs[50];//选择这个段落，我觉得有必要
                p50.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p50.Format.LineSpacing = 18f;
                p50.Format.FirstLineIndent = 24f;
                if (Yσ2 <= Yf)
                {
                    TextRange rangeTop50 = p50.AppendText("耳板端部截面抗拉(劈开）强度：σ=" + Yσ2.ToString("#.##") + "≤" + Yf.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop50.CharacterFormat.FontSize = 12;
                    rangeTop50.CharacterFormat.Bold = false;
                    rangeTop50.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop50.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop50 = p50.AppendText("耳板端部截面抗拉(劈开）强度：σ=" + Yσ2.ToString("#.##") + ">" + Yf.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop50.CharacterFormat.FontSize = 12;
                    rangeTop50.CharacterFormat.Bold = true;
                    rangeTop50.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop50.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop50.CharacterFormat.TextColor = Color.Red;
                }





                section0.AddParagraph();//添加一个段落p51判定耳板端部截面抗拉(劈开）强度；
                Paragraph p51 = section0.Paragraphs[51];//选择这个段落，我觉得有必要
                p51.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p51.Format.LineSpacing = 18f;
                p51.Format.FirstLineIndent = 24f;
                if (Yτ <= Yfv)
                {
                    TextRange rangeTop51 = p51.AppendText("耳板抗剪强度：τ=" + Yτ.ToString("#.##") + "≤" + Yfv.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop51.CharacterFormat.FontSize = 12;
                    rangeTop51.CharacterFormat.Bold = false;
                    rangeTop51.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop51.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop51 = p51.AppendText("耳板抗剪强度：τ=" + Yτ.ToString("#.##") + ">" + Yfv.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop51.CharacterFormat.FontSize = 12;
                    rangeTop51.CharacterFormat.Bold = true;
                    rangeTop51.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop51.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop51.CharacterFormat.TextColor = Color.Red;
                }


                //这里又加了一段，没办法，顺延吧
                section0.AddParagraph();//添加一个段落p52判定耳板承压强度；
                Paragraph p52 = section0.Paragraphs[52];//选择这个段落，我觉得有必要
                p52.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p52.Format.LineSpacing = 18f;
                p52.Format.FirstLineIndent = 24f;
                if (Yσc <= Yfc)
                {
                    TextRange rangeTop52 = p52.AppendText("耳板承压强度：σc=" + Yσc.ToString("#.##") + "≤" + Yfc.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop52.CharacterFormat.FontSize = 12;
                    rangeTop52.CharacterFormat.Bold = false;
                    rangeTop52.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop52.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop52 = p52.AppendText("耳板承压强度：σc=" + Yσc.ToString("#.##") + ">" + Yfc.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop52.CharacterFormat.FontSize = 12;
                    rangeTop52.CharacterFormat.Bold = true;
                    rangeTop52.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop52.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop52.CharacterFormat.TextColor = Color.Red;
                }



                section0.AddParagraph();//添加一个段落p53总判定；
                Paragraph p53 = section0.Paragraphs[53];//选择这个段落，我觉得有必要
                p53.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p53.Format.LineSpacing = 18f;
                p53.Format.FirstLineIndent = 24f;
                if (Ya>= (Ybe / 3 * 4) & Yb >= Ybe & Yσ1 <= Yf & Yσ2 <= Yf & Yτ <= Yfv & Yσc<= Yfc)
                {
                    TextRange rangeTop53 = p53.AppendText("综上，耳板构造及强度满足要求。");//选择这个段落，我觉得有必要
                    rangeTop53.CharacterFormat.FontSize = 12;
                    rangeTop53.CharacterFormat.Bold = false;
                    rangeTop53.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop53.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop53 = p53.AppendText("综上，耳板构造及强度中至少有一项不满足要求。");//选择这个段落，我觉得有必要
                    rangeTop53.CharacterFormat.FontSize = 12;
                    rangeTop53.CharacterFormat.Bold = true;
                    rangeTop53.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop53.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop53.CharacterFormat.TextColor = Color.Red;
                }


                section0.AddParagraph();
                //添加一个段落 p54 写第4节标题
                Paragraph p54 = section0.Paragraphs[54];//选择这个段落，我觉得有必要
                p54.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p54.Format.LineSpacing = 18f;
                TextRange rangeTop54 = p54.AppendText("XX.4 吊耳底部焊缝验算");//选择这个段落，我觉得有必要
                rangeTop54.CharacterFormat.FontSize = 16;
                rangeTop54.CharacterFormat.Bold = true;
                rangeTop54.CharacterFormat.FontNameFarEast = "黑体";
                rangeTop54.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p55说明；
                Paragraph p55 = section0.Paragraphs[55];//选择这个段落，我觉得有必要
                p55.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p55.Format.LineSpacing = 18f;
                p55.Format.FirstLineIndent = 24f;
                TextRange rangeTop55 = p55.AppendText("吊耳底部采用全熔透焊缝，根据《钢结构设计标准》公式11.2.1-1及公式11.2.1-2，焊缝强度验算公式分列如下。");//选择这个段落，我觉得有必要
                rangeTop55.CharacterFormat.FontSize = 12;
                rangeTop55.CharacterFormat.Bold = false;
                rangeTop55.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop55.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p56写耳板底部焊缝强度公式1；
                Paragraph p56 = section0.Paragraphs[56];//选择这个段落，我觉得有必要
                p56.Format.LineSpacing = 18f;
                OfficeMath officeMath56 = new OfficeMath(doc);
                p56.Items.Add(officeMath56);
                officeMath56.FromLatexMathCode("σ=\\frac{N}{l_wh_e}≤f_t^w");

                section0.AddParagraph();//添加一个段落p57写耳板底部焊缝强度公式2；
                Paragraph p57 = section0.Paragraphs[57];//选择这个段落，我觉得有必要
                p57.Format.LineSpacing = 18f;
                OfficeMath officeMath57 = new OfficeMath(doc);
                p57.Items.Add(officeMath57);
                officeMath57.FromLatexMathCode("\\sqrt{σ^2+3τ^2}≤1.1f_t^w");


                section0.AddParagraph();//添加一个段落p58说明；
                Paragraph p58 = section0.Paragraphs[58];//选择这个段落，我觉得有必要
                p58.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p58.Format.LineSpacing = 18f;
                p58.Format.FirstLineIndent = 24f;
                TextRange rangeTop58 = p58.AppendText("经过查表，熔透焊缝强度与母材强度相等，即:");//选择这个段落，我觉得有必要
                rangeTop58.CharacterFormat.FontSize = 12;
                rangeTop58.CharacterFormat.Bold = false;
                rangeTop58.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop58.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                OfficeMath officeMath58 = new OfficeMath(doc);
                p58.Items.Add(officeMath58);
                officeMath58.FromLatexMathCode("f_t^w=f");
                TextRange rangeTop58x = p58.AppendText("。");//选择这个段落，我觉得有必要

                section0.AddParagraph();//添加一个段落p59说明；
                Paragraph p59 = section0.Paragraphs[59];//选择这个段落，我觉得有必要
                p59.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p59.Format.LineSpacing = 18f;
                p59.Format.FirstLineIndent = 24f;
                TextRange rangeTop59 = p59.AppendText("双侧吊耳受力分解示意见图XX.4-1。");//选择这个段落，我觉得有必要
                rangeTop59.CharacterFormat.FontSize = 12;
                rangeTop59.CharacterFormat.Bold = false;
                rangeTop59.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop59.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p60，贴算钢丝绳内力的
                Paragraph p60 = section0.Paragraphs[60];//选择这个段落，我觉得有必要
                p60.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片放中间
                p60.Format.LineSpacing = 18f;
                p60.Format.FirstLineIndent = 28f;//图片放中间，不缩进，但是这个图片好像偏的有规律
                string strp60 = System.Windows.Forms.Application.StartupPath;
                Image image60 = Image.FromFile(@strp60 + "\\TP\\四吊耳算吊耳内力.jpg");
                DocPicture picture60 = section0.Paragraphs[60].AppendPicture(image60);
                picture60.Width = 360;
                picture60.Height = 240;


                section0.AddParagraph();
                //添加一个段落 p61，写钢丝绳内力图名
                Paragraph p61 = section0.Paragraphs[61];//选择这个段落，我觉得有必要
                p61.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p61.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop61 = p61.AppendText("图XX.4-1 吊耳内力计算示意图");
                rangeTop61.CharacterFormat.FontSize = 11;//小四是12，五号是11
                rangeTop61.CharacterFormat.Bold = true;
                rangeTop61.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop61.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p62说明；
                Paragraph p62 = section0.Paragraphs[62];//选择这个段落，我觉得有必要
                p62.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p62.Format.LineSpacing = 18f;
                p62.Format.FirstLineIndent = 24f;
                TextRange rangeTop62 = p62.AppendText("基本组合下，吊耳位置双向分力计算如下。");//选择这个段落，我觉得有必要
                rangeTop62.CharacterFormat.FontSize = 12;
                rangeTop62.CharacterFormat.Bold = false;
                rangeTop62.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop62.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p63说明第一个分力1；
                Paragraph p63 = section0.Paragraphs[63];//选择这个段落，我觉得有必要
                p63.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p63.Format.LineSpacing = 18f;
                p63.Format.FirstLineIndent = 24f;
                TextRange rangeTop63 = p63.AppendText("垂直构件方向：N*Sinθ=" + YN.ToString("#.##")+"*"+ZX.ToString("0.00")+"="+(YN*ZX).ToString("0.00") + "kN");//选择这个段落，我觉得有必要
                rangeTop63.CharacterFormat.FontSize = 12;
                rangeTop63.CharacterFormat.Bold = false;
                rangeTop63.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop63.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                double YX = Math.Sqrt(1 - ZX * ZX);
                section0.AddParagraph();//添加一个段落p64说明第一个分力2；
                Paragraph p64 = section0.Paragraphs[64];//选择这个段落，我觉得有必要
                p64.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p64.Format.LineSpacing = 18f;
                p64.Format.FirstLineIndent = 24f;
                TextRange rangeTop64 = p64.AppendText("沿构件方向：N*Cosθ=" + YN.ToString("#.##") + "*" + YX.ToString("0.00") + "=" + (YN * YX).ToString("0.00") + "kN");//选择这个段落，我觉得有必要
                rangeTop64.CharacterFormat.FontSize = 12;
                rangeTop64.CharacterFormat.Bold = false;
                rangeTop64.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop64.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();//添加一个段落p65说明；
                Paragraph p65 = section0.Paragraphs[65];//选择这个段落，我觉得有必要
                p65.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p65.Format.LineSpacing = 18f;
                p65.Format.FirstLineIndent = 24f;
                TextRange rangeTop65 = p65.AppendText("吊耳位置因拉压产生的应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop65.CharacterFormat.FontSize = 12;
                rangeTop65.CharacterFormat.Bold = false;
                rangeTop65.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop65.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p66位置1的拉压；
                Paragraph p66 = section0.Paragraphs[66];//选择这个段落，我觉得有必要
                p66.Format.LineSpacing = 18f;
                OfficeMath officeMath66 = new OfficeMath(doc);
                p66.Items.Add(officeMath66);
                officeMath66.FromLatexMathCode("σ_1=\\frac{N}{2t(e+f)}=\\frac{" + (YN * ZX).ToString("#.##") + "*1000}{2*" + Yt.ToString() + "*(" + Ye.ToString() + "+" + Yff.ToString() + ")}=" + YσF1.ToString("#.##") + "MPa");

                //Yσ1 = YN * 1000 / (2 * Yt * Yb1);
                //Yσ2 = YN * 1000 / 2 / Yt / (Ya - 2 * Yd0 / 3);
                //Yσc = YN * 1000 / Yt / (Yd0 - 1);
                //YZ = Math.Sqrt((Ya + Yd0 / 2) * (Ya + Yd0 / 2) - (Yd0 / 2) * (Yd0 / 2));
                //// MessageBox.Show(YZ.ToString());
                //Yτ = YN * 1000 / 2 / Yt / YZ;//这个改过了


                section0.AddParagraph();//添加一个段落p67说明；
                Paragraph p67 = section0.Paragraphs[67];//选择这个段落，我觉得有必要
                p67.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p67.Format.LineSpacing = 18f;
                p67.Format.FirstLineIndent = 24f;
                TextRange rangeTop67 = p67.AppendText("吊耳位置因弯矩产生的应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop67.CharacterFormat.FontSize = 12;
                rangeTop67.CharacterFormat.Bold = false;
                rangeTop67.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop67.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                //    NF = YN * YSθ;
                //    NV = YN * YCθ;//表示水平力
                //    YσF1 = NF * 1000 / Yt / (2 * Ye + 2 * Yff);//表示竖向力引起的耳板底部正应力
                //                                               // MessageBox.Show(YσF1.ToString());
                //    YσF2 = NV * 1000 * (Yd0 / 2 + Yc) / Yt / (2 * Ye + 2 * Yff) / (2 * Ye + 2 * Yff) * 6;//表示弯矩引起的的耳板底部正应力
                //                                                                                         // MessageBox.Show(YσF2.ToString());
                //    Yτ1 = NV * 1000 / Yt / (2 * Ye + 2 * Yff);//表示水平力引起的耳板底部剪应力
                //                                              //MessageBox.Show(Yτ1.ToString());
                //    YσFZ = Math.Sqrt((YσF1 + YσF2) * (YσF1 + YσF2) + 3 * Yτ1 * Yτ1);
                //    // MessageBox.Show(YσFZ.ToString());
                //    if (YσFZ < (1.1 * Yf))
                //    { FH.Text = "σ" + ":" + YσFZ.ToString("#.##") + "<" + "1.1f" + ":" + (1.1 * Yf).ToString("#.#") + "," + "满足"; FH.ForeColor = Color.Black; }
                //    else
                //    { FH.Text = "σ" + ":" + YσFZ.ToString("#.##") + ">" + "1.1f" + ":" + (1.1 * Yf).ToString("#.#") + "," + "不满足"; FH.ForeColor = Color.Red; }

                //    if (YσF1 < Yf)
                //    { DJ.Text = "σ" + ":" + YσF1.ToString("#.##") + "<" + "f" + ":" + Yf.ToString() + "," + "满足"; DJ.ForeColor = Color.Black; }
                //    else
                //    { DJ.Text = "σ" + ":" + YσF1.ToString("#.##") + ">" + "f" + ":" + Yf.ToString() + "," + "不满足"; DJ.ForeColor = Color.Red; }

                //}

                section0.AddParagraph();//添加一个段落p68位置1的弯矩；
                Paragraph p68 = section0.Paragraphs[68];//选择这个段落，我觉得有必要
                p68.Format.LineSpacing = 18f;
                OfficeMath officeMath68 = new OfficeMath(doc);
                p68.Items.Add(officeMath68);
                officeMath68.FromLatexMathCode("σ_2=\\frac{M}{4t\\frac{(e+f)^2}{6}}=\\frac{" + NV.ToString("#.##") + "*1000*(" + Yd0.ToString() + "/2+" + Yc.ToString() + ")}{4*" + Yt.ToString() + "*\\frac{(" + Ye.ToString() + "+" + Yff.ToString() + ")^2}{6}}=" + YσF2.ToString("#.##") + "MPa");



                section0.AddParagraph();//添加一个段落p69说明；
                Paragraph p69 = section0.Paragraphs[69];//选择这个段落，我觉得有必要
                p69.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p69.Format.LineSpacing = 18f;
                p69.Format.FirstLineIndent = 24f;
                TextRange rangeTop69 = p69.AppendText("吊耳位置因剪力产生的应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop69.CharacterFormat.FontSize = 12;
                rangeTop69.CharacterFormat.Bold = false;
                rangeTop69.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop69.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();//添加一个段落p70位置1的剪力；
                Paragraph p70 = section0.Paragraphs[70];//选择这个段落，我觉得有必要
                p70.Format.LineSpacing = 18f;
                OfficeMath officeMath70 = new OfficeMath(doc);
                p70.Items.Add(officeMath70);
                officeMath70.FromLatexMathCode("τ=\\frac{V}{2t(e+f)}=\\frac{" + NV.ToString("#.##") + "*700)}{2*" + Yt.ToString() + "*(" + Ye.ToString() + "+" + Yff.ToString() + ")}=" + Yτ1.ToString("#.##") + "MPa");


                section0.AddParagraph();//添加一个段落p97说明；
                Paragraph p71 = section0.Paragraphs[71];//选择这个段落，我觉得有必要
                p71.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p71.Format.LineSpacing = 18f;
                p71.Format.FirstLineIndent = 24f;
                TextRange rangeTop71 = p71.AppendText("则吊耳位置三向应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop71.CharacterFormat.FontSize = 12;
                rangeTop71.CharacterFormat.Bold = false;
                rangeTop71.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop71.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p72位置三向应力；
                Paragraph p72 = section0.Paragraphs[72];//选择这个段落，我觉得有必要
                p72.Format.LineSpacing = 18f;
                OfficeMath officeMath72 = new OfficeMath(doc);
                p72.Items.Add(officeMath72);
                officeMath72.FromLatexMathCode("\\sqrt{(σ_1+σ_2)^2+3τ^2}=" + YσFZ.ToString("#.##") + "MPa");



                section0.AddParagraph();//添加一个段落p73说明；
                Paragraph p73 = section0.Paragraphs[73];//选择这个段落，我觉得有必要
                p73.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p73.Format.LineSpacing = 18f;
                p73.Format.FirstLineIndent = 24f;
                if (YσF1<= Yf)
                {
                    TextRange rangeTop73 = p73.AppendText("拉压应力" + YσF1.ToString("#.##") + "≤" + Yf.ToString() + "MPa。");//选择这个段落，我觉得有必要
                    rangeTop73.CharacterFormat.FontSize = 12;
                    rangeTop73.CharacterFormat.Bold = false;
                    rangeTop73.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop73.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop73 = p73.AppendText("拉压应力" + YσF1.ToString("#.##") + ">" + Yf.ToString() + "MPa。");
                    rangeTop73.CharacterFormat.FontSize = 12;
                    rangeTop73.CharacterFormat.Bold = false;
                    rangeTop73.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop73.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }



                section0.AddParagraph();//添加一个段落p74说明；
                Paragraph p74 = section0.Paragraphs[74];//选择这个段落，我觉得有必要
                p74.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p74.Format.LineSpacing = 18f;
                p74.Format.FirstLineIndent = 24f;
                if (YσFZ<= (1.1 * Yf))
                {
                    TextRange rangeTop74 = p74.AppendText("三向应力" + YσFZ.ToString("#.##") + "≤" + (1.1 * Yf).ToString() + "MPa。");//选择这个段落，我觉得有必要
                    rangeTop74.CharacterFormat.FontSize = 12;
                    rangeTop74.CharacterFormat.Bold = false;
                    rangeTop74.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop74.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop74 = p74.AppendText("三向应力" + YσFZ.ToString("#.##") + ">" + (1.1 * Yf).ToString() + "MPa。");
                    rangeTop74.CharacterFormat.FontSize = 12;
                    rangeTop74.CharacterFormat.Bold = false;
                    rangeTop74.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop74.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }

                section0.AddParagraph();//添加一个段落p75说明；
                Paragraph p75 = section0.Paragraphs[75];//选择这个段落，我觉得有必要
                p75.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p75.Format.LineSpacing = 18f;
                p75.Format.FirstLineIndent = 24f;
                if (YσFZ<= (1.1 * Yf) &YσF1<= Yf)
                {
                    TextRange rangeTop75 = p75.AppendText("满足要求。");//选择这个段落，我觉得有必要
                    rangeTop75.CharacterFormat.FontSize = 12;
                    rangeTop75.CharacterFormat.Bold = false;
                    rangeTop75.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop75.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop75 = p75.AppendText("不满足要求。");
                    rangeTop75.CharacterFormat.FontSize = 12;
                    rangeTop75.CharacterFormat.Bold = true;
                    rangeTop75.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop75.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop75.CharacterFormat.TextColor = Color.Red;
                }

                section0.AddParagraph();
                //添加一个段落 p76写第5节标题
                Paragraph p76 = section0.Paragraphs[76];//选择这个段落，我觉得有必要
                p76.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p76.Format.LineSpacing = 18f;
                TextRange rangeTop76 = p76.AppendText("XX.5 小结");//选择这个段落，我觉得有必要
                rangeTop76.CharacterFormat.FontSize = 16;
                rangeTop76.CharacterFormat.Bold = true;
                rangeTop76.CharacterFormat.FontNameFarEast = "黑体";
                rangeTop76.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();//添加一个段落p77说明；
                Paragraph p77 = section0.Paragraphs[77];//选择这个段落，我觉得有必要
                p77.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p77.Format.LineSpacing = 18f;
                p77.Format.FirstLineIndent = 24f;
                TextRange rangeTop77 = p77.AppendText("根据2~4节计算，可得如下结论。");//选择这个段落，我觉得有必要
                rangeTop77.CharacterFormat.FontSize = 12;
                rangeTop77.CharacterFormat.Bold = false;
                rangeTop77.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop77.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();//添加一个段落p78说明；
                Paragraph p78 = section0.Paragraphs[78];//选择这个段落，我觉得有必要
                p78.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p78.Format.LineSpacing = 18f;
                p78.Format.FirstLineIndent = 24f;
                if (rFg>= YN2 & rFh>= YN3)
                {
                    TextRange rangeTop78 = p78.AppendText("（1）吊耳及卡环满足要求；");//选择这个段落，我觉得有必要
                    rangeTop78.CharacterFormat.FontSize = 12;
                    rangeTop78.CharacterFormat.Bold = false;
                    rangeTop78.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop78.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop78 = p78.AppendText("（1）吊耳及卡环中存在不满足项；");//选择这个段落，我觉得有必要
                    rangeTop78.CharacterFormat.FontSize = 12;
                    rangeTop78.CharacterFormat.Bold = true;
                    rangeTop78.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop78.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop78.CharacterFormat.TextColor = Color.Red;
                }



                section0.AddParagraph();//添加一个段落p79说明；
                Paragraph p79 = section0.Paragraphs[79];//选择这个段落，我觉得有必要
                p79.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p79.Format.LineSpacing = 18f;
                p79.Format.FirstLineIndent = 24f;
                if (Ya >= (Ybe / 3 * 4) & Yb >= Ybe & Yσ1 <= Yf & Yσ2<= Yf & Yτ <= Yfv)
                {
                    TextRange rangeTop79 = p79.AppendText("（2）吊耳板自身构造及强度满足要求；");//选择这个段落，我觉得有必要
                    rangeTop79.CharacterFormat.FontSize = 12;
                    rangeTop79.CharacterFormat.Bold = false;
                    rangeTop79.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop79.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop79 = p79.AppendText("（2）吊耳板自身构造或强度不满足要求；");//选择这个段落，我觉得有必要
                    rangeTop79.CharacterFormat.FontSize = 12;
                    rangeTop79.CharacterFormat.Bold = true;
                    rangeTop79.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop79.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop79.CharacterFormat.TextColor = Color.Red;
                }





                section0.AddParagraph();//添加一个段落p113说明；
                Paragraph p80 = section0.Paragraphs[80];//选择这个段落，我觉得有必要
                p80.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p80.Format.LineSpacing = 18f;
                p80.Format.FirstLineIndent = 24f;
                if (YσFZ<= (1.1 * Yf) &YσF1<= Yf)
                {
                    TextRange rangeTop80 = p80.AppendText("（3）吊耳板底部焊缝强度满足要求；");//选择这个段落，我觉得有必要
                    rangeTop80.CharacterFormat.FontSize = 12;
                    rangeTop80.CharacterFormat.Bold = false;
                    rangeTop80.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop80.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop80 = p80.AppendText("（3）吊耳板底部焊缝强度不满足要求；");//选择这个段落，我觉得有必要
                    rangeTop80.CharacterFormat.FontSize = 12;
                    rangeTop80.CharacterFormat.Bold = true;
                    rangeTop80.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop80.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop80.CharacterFormat.TextColor = Color.Red;
                }



                section0.AddParagraph();//添加一个段落p113说明；
                Paragraph p81 = section0.Paragraphs[81];//选择这个段落，我觉得有必要
                p81.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p81.Format.LineSpacing = 18f;
                p81.Format.FirstLineIndent = 24f;
                if (rFg>= YN2 & rFh>= YN3 & Ya >= (Ybe / 3 * 4) & Yb >= Ybe & Yσ1 <= Yf & Yσ2 <= Yf & Yτ <= Yfv & Yσc <= Yfc & YσFZ <= (1.1 * Yf) &YσF1<= Yf)
                {
                    TextRange rangeTop81 = p81.AppendText("满足要求。");//选择这个段落，我觉得有必要
                    rangeTop81.CharacterFormat.FontSize = 12;
                    rangeTop81.CharacterFormat.Bold = false;
                    rangeTop81.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop81.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop81 = p81.AppendText("不满足要求。");//选择这个段落，我觉得有必要
                    rangeTop81.CharacterFormat.FontSize = 12;
                    rangeTop81.CharacterFormat.Bold = true;
                    rangeTop81.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop81.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop81.CharacterFormat.TextColor = Color.Red;
                }


                //保存文档       
                string ttx = DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒");
                string JSSD1 = System.Windows.Forms.Application.StartupPath;
                //Image image86 = Image.FromFile(@strp86 + "\\算吊耳内力.jpg");
                string bt = System.Windows.Forms.Application.StartupPath + "\\JSS\\四吊耳" + "（" + ttx + "）" + ".docx";
                doc.SaveToFile(bt, FileFormat.Docx);
                MessageBox.Show(bt.ToString() + "已创建");
                button4.Enabled = true;//

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (GSSLLMQ1.Text == "暂时不知道" || GSSRXLLMQ1.Text == "暂时不知道" || KHFHMQ1.Text == "暂时不知道" || GSSLLsMQ1.Text == "暂时不知道" || GZMQ1.Text == "暂时不知道" || JKLMQ1.Text == "暂时不知道" || PKMQ1.Text == "暂时不知道" || KJMQ1.Text == "暂时不知道" || FHMQ1.Text == "暂时不知道" || CYMQ1.Text == "暂时不知道")
            {
                MessageBox.Show("计算未完成！");


            }

            else
            {
                //这里加图片结束了
                button4.Enabled = false;//生成word的时候不能用，最后用

                Spire.Doc.Document doc = new Spire.Doc.Document();


                //添加一个section0，全文暂时添加一个

                Section section0 = doc.AddSection();

                section0.PageSetup.PageSize = PageSize.Letter;//设置第一页的格式


                //这是设置页边距，55f正好是中等的设置
                section0.PageSetup.Margins.Top = 55f;
                section0.PageSetup.Margins.Left = 55f;
                section0.PageSetup.Margins.Bottom = 55f;
                section0.PageSetup.Margins.Right = 55f;

                section0.AddParagraph();
                //添加一个段落 p0
                Paragraph p0 = section0.Paragraphs[0];//选择这个段落，我觉得有必要




                //设置中对齐
                p0.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                p0.Format.LineSpacing = 18f;
                TextRange rangeTop0 = p0.AppendText("第XX章 单吊耳吊装计算书");//选择这个段落，我觉得有必要
                rangeTop0.CharacterFormat.FontSize = 18;
                rangeTop0.CharacterFormat.Bold = true;
                rangeTop0.CharacterFormat.FontNameFarEast = "黑体";
                rangeTop0.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                //rangeTop0.StyleName.ToUpper(d) ;

                section0.AddParagraph();
                //添加一个段落 p1
                Paragraph p1 = section0.Paragraphs[1];//选择这个段落，我觉得有必要
                p1.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p1.Format.LineSpacing = 18f;
                TextRange rangeTop1 = p1.AppendText("XX.1 工况介绍");//选择这个段落，我觉得有必要
                rangeTop1.CharacterFormat.FontSize = 16;
                rangeTop1.CharacterFormat.Bold = true;
                rangeTop1.CharacterFormat.FontNameFarEast = "黑体";
                rangeTop1.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p2
                Paragraph p2 = section0.Paragraphs[2];//选择这个段落，我觉得有必要
                p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p2.Format.LineSpacing = 18f;
                p2.Format.FirstLineIndent = 24f;

                TextRange rangeTop2 = p2.AppendText("本构件采用双吊耳形式吊装" + "，" + "吊装示意见图XX.1-1" + "，" + "其中构件重量G1=" + tGMQ1.Text + "t"+ "。");//添加文字段落1
                rangeTop2.CharacterFormat.FontSize = 12;
                rangeTop2.CharacterFormat.Bold = false;
                rangeTop2.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop2.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p3
                Paragraph p3 = section0.Paragraphs[3];//选择这个段落，我觉得有必要
                p3.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片放中间
                p3.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片放中间，不缩进
                string strp3 = System.Windows.Forms.Application.StartupPath;
                Image image3 = Image.FromFile(@strp3 + "\\TP\\单吊耳示意.jpg");
                DocPicture picture3 = section0.Paragraphs[3].AppendPicture(image3);
                picture3.Width =150;
                picture3.Height = 230;

                section0.AddParagraph();
                //添加一个段落 p4
                Paragraph p4 = section0.Paragraphs[4];//选择这个段落，我觉得有必要
                p4.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p4.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop4 = p4.AppendText("图XX.1-1 吊装示意图");
                rangeTop4.CharacterFormat.FontSize = 11;
                rangeTop4.CharacterFormat.Bold = true;
                rangeTop4.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop4.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p5 这是第六个 写吊耳板详情的
                Paragraph p5 = section0.Paragraphs[5];//选择这个段落，我觉得有必要
                p5.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;//文字左对齐
                p5.Format.LineSpacing = 18f;
                p5.Format.FirstLineIndent = 24f;//普通文字段缩进
                TextRange rangeTop5 = p5.AppendText("工况采用吊耳尺寸见图XX.1-2" + "，" + "其中受力方向最小净距a=" + taMQ1.Text + "mm" + "，" + "双侧边缘净距b=" + tbMQ1.Text + "mm" + "，"
                      + "销轴孔径d0=" + td0MQ1.Text + "mm" + "，" + "底部补长c=" + tcMQ1.Text + "mm" + "，" + "加劲肋边距e=" + teMQ1.Text + "mm" + "，" + "加劲肋中距f=" + tfMQ1.Text + "mm" + "，" + "耳板厚度t=" + ttMQ1.Text + "mm" + "，" + "耳板材质为" + BoxBMQ1.Text + "。");//ttMQ1
                rangeTop5.CharacterFormat.FontSize = 12;//小四是12，五号是11
                rangeTop5.CharacterFormat.Bold = false;
                rangeTop5.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop5.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p6 贴吊耳图片1的
                Paragraph p6 = section0.Paragraphs[6];//选择这个段落，我觉得有必要
                p6.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片放中间
                p6.Format.LineSpacing = 18f;
                p6.Format.FirstLineIndent = 28f;//图片放中间，不缩进，但是这个图片好像偏的有规律
                string strp6 = System.Windows.Forms.Application.StartupPath;//这个路径就是增加cad图片d1的位置
                Image image6 = Image.FromFile(@strp6 + "\\TP\\正面的.jpg");
                DocPicture picture6 = section0.Paragraphs[6].AppendPicture(image6);
                picture6.Width = 220;
                picture6.Height = 180;

                section0.AddParagraph();
                //添加一个段落 p7，写正立面图名的
                Paragraph p7 = section0.Paragraphs[7];//选择这个段落，我觉得有必要
                p7.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p7.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop7 = p7.AppendText("（a）正立面");
                rangeTop7.CharacterFormat.FontSize = 11;
                rangeTop7.CharacterFormat.Bold = true;
                rangeTop7.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop7.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p8贴吊耳侧面图的
                Paragraph p8 = section0.Paragraphs[8];//选择这个段落，我觉得有必要
                p8.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片放中间
                p8.Format.LineSpacing = 18f;
                p8.Format.FirstLineIndent = 28f;//图片放中间，不缩进，但是这个图片好像偏的有规律
                string strp8 = System.Windows.Forms.Application.StartupPath;//这个路径就是增加cad图片d2的位置
                Image image8 = Image.FromFile(@strp8 + "\\TP\\侧面的.jpg");
                DocPicture picture8 = section0.Paragraphs[8].AppendPicture(image8);
                picture8.Width = 200;
                picture8.Height = 160;


                section0.AddParagraph();
                //添加一个段落 p9，写侧立面图名的
                Paragraph p9 = section0.Paragraphs[9];//选择这个段落，我觉得有必要
                p9.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p9.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop9 = p9.AppendText("（b）侧面图");
                rangeTop9.CharacterFormat.FontSize = 11;
                rangeTop9.CharacterFormat.Bold = true;
                rangeTop9.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop9.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();
                //添加一个段落 p10，贴剖面图的
                Paragraph p10 = section0.Paragraphs[10];//选择这个段落，我觉得有必要
                p10.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片放中间
                p10.Format.LineSpacing = 18f;
                p10.Format.FirstLineIndent = 28f;//图片放中间，不缩进，但是这个图片好像偏的有规律
                string strp10 = System.Windows.Forms.Application.StartupPath;//这个路径就是增加cad图片d3的位置
                Image image10 = Image.FromFile(@strp10 + "\\TP\\剖面的.jpg");
                DocPicture picture10 = section0.Paragraphs[10].AppendPicture(image10);
                picture10.Width = 170;
                picture10.Height = 50;

                section0.AddParagraph();
                //添加一个段落 p11，写剖面图名的
                Paragraph p11 = section0.Paragraphs[11];//选择这个段落，我觉得有必要
                p11.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p11.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop11 = p11.AppendText("（c）底面图");
                rangeTop11.CharacterFormat.FontSize = 11;
                rangeTop11.CharacterFormat.Bold = true;
                rangeTop11.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop11.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p12，写尺寸图名的
                Paragraph p12 = section0.Paragraphs[12];//选择这个段落，我觉得有必要
                p12.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;//图片名放中间
                p12.Format.LineSpacing = 18f;
                //p2.Format.FirstLineIndent = 24f;//图片名字放中间，不缩进
                TextRange rangeTop12 = p12.AppendText("图XX.1-2 吊耳尺寸图");
                rangeTop12.CharacterFormat.FontSize = 11;//小四是12，五号是11
                rangeTop12.CharacterFormat.Bold = true;
                rangeTop12.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop12.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落 p13 写
                Paragraph p13 = section0.Paragraphs[13];//选择这个段落，我觉得有必要
                p13.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p13.Format.LineSpacing = 18f;
                TextRange rangeTop13 = p13.AppendText("XX.2 钢丝绳验算");//选择这个段落，我觉得有必要
                rangeTop13.CharacterFormat.FontSize = 16;
                rangeTop13.CharacterFormat.Bold = true;
                rangeTop13.CharacterFormat.FontNameFarEast = "黑体";
                rangeTop13.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                                  


                section0.AddParagraph();
                //添加一个段落p14开始求解钢丝绳内力
                Paragraph p14 = section0.Paragraphs[14];//选择这个段落，我觉得有必要
                p14.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p14.Format.LineSpacing = 18f;
                p14.Format.FirstLineIndent = 24f;
                TextRange rangeTop14 = p14.AppendText("本工况动力系数为" + BoxDDMQ1.Text + "，则钢丝绳内力标准值计算如下。");//选择这个段落，我觉得有必要
                rangeTop14.CharacterFormat.FontSize = 12;
                rangeTop14.CharacterFormat.Bold = false;
                rangeTop14.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop14.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//15行写NaS1的计算结果
                Paragraph p15 = section0.Paragraphs[15];//选择这个段落，我觉得有必要
                p15.Format.LineSpacing = 15f;
                OfficeMath officeMath15 = new OfficeMath(doc);
                p15.Items.Add(officeMath15);
                officeMath15.FromLatexMathCode("N= "+ YDDMQ1.ToString() + "*" + YGMQ1.ToString() + "*10" +"=" + YN2MQ1.ToString("#.##") + "kN");



                section0.AddParagraph();
                //添加一个段落p16开始求解钢丝绳内力
                Paragraph p16 = section0.Paragraphs[16];//选择这个段落，我觉得有必要
                p16.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p16.Format.LineSpacing = 18f;
                p16.Format.FirstLineIndent = 24f;
                TextRange rangeTop16 = p16.AppendText("本工况所选择的钢丝绳材质：" + BOXSMQ1.Text + "，型号为：" + BoxXMQ1.Text + "，直径为：" + BoxZJMQ1.Text + "mm，" +
                    "根据《建筑施工计算手册》查取，其破断拉力总和为" + FgMQ1.ToString() + "kN。");//选择这个段落，我觉得有必要
                rangeTop16.CharacterFormat.FontSize = 12;
                rangeTop16.CharacterFormat.Bold = false;
                rangeTop16.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop16.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();
                //添加一个段落p17开始求解钢丝绳内力
                Paragraph p17 = section0.Paragraphs[17];//选择这个段落，我觉得有必要
                p17.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p17.Format.LineSpacing = 18f;
                p17.Format.FirstLineIndent = 24f;
                TextRange rangeTop17 = p17.AppendText("钢丝绳容许拉力根据《建筑施工计算手册》公式13-3计算，公式如下。");//选择这个段落，我觉得有必要
                rangeTop17.CharacterFormat.FontSize = 12;
                rangeTop17.CharacterFormat.Bold = false;
                rangeTop17.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop17.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//18行写NaS1的计算结果
                Paragraph p18 = section0.Paragraphs[18];//选择这个段落，我觉得有必要
                p18.Format.LineSpacing = 18f;
                OfficeMath officeMath18 = new OfficeMath(doc);
                p18.Items.Add(officeMath18);
                officeMath18.FromLatexMathCode("[Fg]=\\frac{αFg}{K}");

                section0.AddParagraph();
                //添加一个段落p19开始求解钢丝绳内力
                Paragraph p19 = section0.Paragraphs[19];//选择这个段落，我觉得有必要
                p19.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p19.Format.LineSpacing = 18f;
                p19.Format.FirstLineIndent = 24f;
                TextRange rangeTop19 = p19.AppendText("由于钢丝绳型号为：" + BoxXMQ1.Text + "，不均匀系数α取值" + αMQ1.ToString() + "安全系数K选择取值为" + KMQ1.ToString() + "，则钢丝绳容许压力[Fg]计算如下。");//选择这个段落，我觉得有必要
                rangeTop19.CharacterFormat.FontSize = 12;
                rangeTop19.CharacterFormat.Bold = false;
                rangeTop19.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop19.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//20行写[Fg]的计算结果
                Paragraph p20 = section0.Paragraphs[20];//选择这个段落，我觉得有必要
                p20.Format.LineSpacing = 18f;
                OfficeMath officeMath20 = new OfficeMath(doc);
                p20.Items.Add(officeMath20);
                officeMath20.FromLatexMathCode("[Fg]=\\frac{" + αMQ1.ToString() + "*" + FgMQ1.ToString() + "}{" + KMQ1.ToString() + "}" + "=" + rFgMQ1.ToString("#.##") + "kN");

                section0.AddParagraph();
                //添加一个段落p50开始求解钢丝绳内力
                Paragraph p21 = section0.Paragraphs[21];//选择这个段落，我觉得有必要
                p21.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p21.Format.LineSpacing = 18f;
                p21.Format.FirstLineIndent = 24f;

                if (rFgMQ1 < YN3MQ1)
                {
                    TextRange rangeTop21 = p21.AppendText("由于最大钢丝绳拉力" + YN3MQ1.ToString("#.##") + ">" + rFgMQ1.ToString("#.##") + "kN，钢丝绳满足受力不要求。");
                    rangeTop21.CharacterFormat.FontSize = 12;
                    rangeTop21.CharacterFormat.Bold = true;
                    rangeTop21.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop21.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop21.CharacterFormat.TextColor = Color.Red;
                }//选择这个段落，我觉得有必要
                else
                {
                    TextRange rangeTop21 = p21.AppendText("由于最大钢丝绳拉力" + YN3MQ1.ToString("#.##") + "≤" + rFgMQ1.ToString("#.##") + "kN，钢丝绳满足受力要求。");
                    rangeTop21.CharacterFormat.FontSize = 12;
                    rangeTop21.CharacterFormat.Bold = false;
                    rangeTop21.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop21.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }

                section0.AddParagraph();
                //添加一个段落p22开始写卡环
                Paragraph p22 = section0.Paragraphs[22];//选择这个段落，我觉得有必要
                p22.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p22.Format.LineSpacing = 18f;
                p22.Format.FirstLineIndent = 24f;
                if (rFhMQ1 < YN3MQ1)
                {
                    TextRange rangeTop22 = p22.AppendText("本工况选择的卡环型号为：" + BoxKHMQ1.Text + "，根据《建筑施工计算手册》，卡环使用负荷[Fj]为" + rFhMQ1.ToString("#.##") + "kN，由于" + YN3MQ1.ToString("#.##") + ">" + rFhMQ1.ToString() + "kN" + "，卡环不满足要求。");//选择这个段落，我觉得有必要
                    rangeTop22.CharacterFormat.FontSize = 12;
                    rangeTop22.CharacterFormat.Bold = true;
                    rangeTop22.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop22.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop22.CharacterFormat.TextColor = Color.Red;
                }
                else
                {
                    TextRange rangeTop22 = p22.AppendText("本工况选择的卡环型号为：" + BoxKHMQ1.Text + "，根据《建筑施工计算手册》，卡环使用负荷[Fj]为" + rFhMQ1.ToString("#.##") + "kN，由于" + YN3MQ1.ToString("#.##") + "≤" + rFhMQ1.ToString() + "kN" + "，卡环满足要求。");//选择这个段落，我觉得有必要
                    rangeTop22.CharacterFormat.FontSize = 12;
                    rangeTop22.CharacterFormat.Bold = false;
                    rangeTop22.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop22.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                }

                section0.AddParagraph();
                //添加一个段落 p23写第三个标题
                Paragraph p23 = section0.Paragraphs[23];//选择这个段落，我觉得有必要
                p23.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p23.Format.LineSpacing = 18f;
                TextRange rangeTop23 = p23.AppendText("XX.3 吊耳验算");//选择这个段落，我觉得有必要
                rangeTop23.CharacterFormat.FontSize = 16;
                rangeTop23.CharacterFormat.Bold = true;
                rangeTop23.CharacterFormat.FontNameFarEast = "黑体";
                rangeTop23.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落p24开始求解钢丝绳内力
                Paragraph p24 = section0.Paragraphs[24];//选择这个段落，我觉得有必要
                p24.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p24.Format.LineSpacing = 18f;
                p24.Format.FirstLineIndent = 24f;
                TextRange rangeTop24 = p24.AppendText("根据《钢结构设计标准》11.6.2条，吊耳板应满足如下要求。");//选择这个段落，我觉得有必要
                rangeTop24.CharacterFormat.FontSize = 12;
                rangeTop24.CharacterFormat.Bold = false;
                rangeTop24.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop24.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//25行写吊耳板构造
                Paragraph p25 = section0.Paragraphs[25];//选择这个段落，我觉得有必要
                p25.Format.LineSpacing = 18f;
                OfficeMath officeMath25 = new OfficeMath(doc);
                p25.Items.Add(officeMath25);
                officeMath25.FromLatexMathCode("a≥\\frac{4}{3}be");

                section0.AddParagraph();//26行写吊耳板构造2
                Paragraph p26 = section0.Paragraphs[26];//选择这个段落，我觉得有必要
                p26.Format.LineSpacing = 18f;
                OfficeMath officeMath26 = new OfficeMath(doc);
                p26.Items.Add(officeMath26);
                officeMath26.FromLatexMathCode("be=2t+16≤b");

                section0.AddParagraph();
                //添加一个段落p27写构造结果1
                Paragraph p27 = section0.Paragraphs[27];//选择这个段落，我觉得有必要
                p27.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p27.Format.LineSpacing = 18f;
                p27.Format.FirstLineIndent = 24f;
                TextRange rangeTop27 = p27.AppendText("则");//选择这个段落，我觉得有必要
                rangeTop27.CharacterFormat.FontSize = 12;
                rangeTop27.CharacterFormat.Bold = false;
                rangeTop27.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop27.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                OfficeMath officeMath27 = new OfficeMath(doc);
                p27.Items.Add(officeMath27);
                officeMath27.FromLatexMathCode("be=" + "2*" + YtMQ1.ToString() + "+16=" + YbeMQ1.ToString());
                TextRange rangeTop27X1 = p27.AppendText("。");//选择这个段落，我觉得有必要


                section0.AddParagraph();
                //添加一个段落p28写构造结果2
                Paragraph p28 = section0.Paragraphs[28];//选择这个段落，我觉得有必要
                p28.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p28.Format.LineSpacing = 18f;
                p28.Format.FirstLineIndent = 24f;
                TextRange rangeTop28 = p28.AppendText("由于a=" + YaMQ1.ToString() + "，" + "b=" + YbMQ1.ToString() + "。");//选择这个段落，我觉得有必要
                rangeTop28.CharacterFormat.FontSize = 12;
                rangeTop28.CharacterFormat.Bold = false;
                rangeTop28.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop28.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落p29写构造结果2
                Paragraph p29 = section0.Paragraphs[29];//选择这个段落，我觉得有必要
                p29.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p29.Format.LineSpacing = 18f;
                p29.Format.FirstLineIndent = 24f;
                if (YaMQ1 < (YbeMQ1 / 3 * 4))
                {
                    OfficeMath officeMath29 = new OfficeMath(doc);
                    p29.Items.Add(officeMath29);
                    officeMath29.FromLatexMathCode("a<\\frac{4}{3}be");
                }
                else
                {
                    OfficeMath officeMath29 = new OfficeMath(doc);
                    p29.Items.Add(officeMath29);
                    officeMath29.FromLatexMathCode("a≥\\frac{4}{3}be");
                }

                section0.AddParagraph();
                //添加一个段落p60写构造结果2
                Paragraph p30 = section0.Paragraphs[30];//选择这个段落，我觉得有必要
                p30.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p30.Format.LineSpacing = 18f;
                p30.Format.FirstLineIndent = 24f;
                if (YbMQ1 < YbeMQ1)
                {
                    OfficeMath officeMath30 = new OfficeMath(doc);
                    p30.Items.Add(officeMath30);
                    officeMath30.FromLatexMathCode("b<be");
                }
                else
                {
                    OfficeMath officeMath30 = new OfficeMath(doc);
                    p30.Items.Add(officeMath30);
                    officeMath30.FromLatexMathCode("b≥be");
                }


                section0.AddParagraph();
                //添加一个段落p59写构造结果2
                Paragraph p31 = section0.Paragraphs[31];//选择这个段落，我觉得有必要
                p31.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p31.Format.LineSpacing = 18f;
                p31.Format.FirstLineIndent = 24f;

                if (YaMQ1 >= (YbeMQ1 / 3 * 4) & YbMQ1 >= YbeMQ1)
                {
                    TextRange rangeTop31 = p31.AppendText("构造满足要求。");//选择这个段落，我觉得有必要
                    rangeTop31.CharacterFormat.FontSize = 12;
                    rangeTop31.CharacterFormat.Bold = false;
                    rangeTop31.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop31.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop31 = p31.AppendText("构造不满足要求。");//选择这个段落，我觉得有必要
                    rangeTop31.CharacterFormat.FontSize = 12;
                    rangeTop31.CharacterFormat.Bold = true;
                    rangeTop31.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop31.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop31.CharacterFormat.TextColor = Color.Red;
                }


                section0.AddParagraph();
                //添加一个段落p32写N设计值
                Paragraph p32 = section0.Paragraphs[32];//选择这个段落，我觉得有必要
                p32.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p32.Format.LineSpacing = 18f;
                p32.Format.FirstLineIndent = 24f;
                TextRange rangeTop32 = p32.AppendText("用于吊耳计算时采用的钢丝绳拉力设计值N= 1.5*"+ YDDMQ1.ToString() + " * " + YGMQ1.ToString() + " * 10" +" = "  + YNMQ1.ToString("#.##") + "kN。");//选择这个段落，我觉得有必要
                rangeTop32.CharacterFormat.FontSize = 12;
                rangeTop32.CharacterFormat.Bold = false;
                rangeTop32.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop32.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();
                //添加一个段落p33写构造结果2
                Paragraph p33 = section0.Paragraphs[33];//选择这个段落，我觉得有必要
                p33.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p33.Format.LineSpacing = 18f;
                p33.Format.FirstLineIndent = 24f;
                TextRange rangeTop33 = p33.AppendText("根据《钢结构设计标准》公式11.6.3-1及11.6.3-2，耳板孔净截面处的抗拉应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop33.CharacterFormat.FontSize = 12;
                rangeTop33.CharacterFormat.Bold = false;
                rangeTop33.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop33.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();//64行耳板孔净截面处的抗拉强度公式2:
                Paragraph p34 = section0.Paragraphs[34];//选择这个段落，我觉得有必要
                p34.Format.LineSpacing = 18f;
                OfficeMath officeMath34 = new OfficeMath(doc);
                p34.Items.Add(officeMath34);
                officeMath34.FromLatexMathCode("b1=min(2t+16，b-\\frac{d0}{3})=min(2*" + YtMQ1.ToString() + "+16，" + YbMQ1.ToString() + "-\\frac{" + Yd0MQ1.ToString()
                    + "}{3})=" + Yb1MQ1.ToString() + "mm");

                section0.AddParagraph();//35行耳板孔净截面处的抗拉强度公式1:
                Paragraph p35 = section0.Paragraphs[35];//选择这个段落，我觉得有必要
                p35.Format.LineSpacing = 18f;
                OfficeMath officeMath35 = new OfficeMath(doc);
                p35.Items.Add(officeMath35);
                officeMath35.FromLatexMathCode("σ=\\frac{N}{2tb1}=\\frac{" + YNMQ1.ToString("#.##") + "*1000}{2*" + YtMQ1.ToString() + "*" + Yb1MQ1.ToString() + "}=" + Yσ1MQ1.ToString("#.##") + "MPa");




                section0.AddParagraph();
                //添加一个段落p36写耳板端部截面抗拉(劈开）强度
                Paragraph p36 = section0.Paragraphs[36];//选择这个段落，我觉得有必要
                p36.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p36.Format.LineSpacing = 18f;
                p36.Format.FirstLineIndent = 24f;
                TextRange rangeTop36 = p36.AppendText("根据《钢结构设计标准》公式11.6.3-3，耳板端部截面抗拉(劈开）应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop36.CharacterFormat.FontSize = 12;
                rangeTop36.CharacterFormat.Bold = false;
                rangeTop36.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop36.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();//添加一个段落p67写耳板端部截面抗拉(劈开）强度；
                Paragraph p37 = section0.Paragraphs[37];//选择这个段落，我觉得有必要
                p37.Format.LineSpacing = 18f;
                OfficeMath officeMath37 = new OfficeMath(doc);
                p37.Items.Add(officeMath37);
                officeMath37.FromLatexMathCode("σ=\\frac{N}{2t(a-\\frac{2d0}{3})}=\\frac{" + YNMQ1.ToString("#.##") + "*1000}{2*" + YtMQ1.ToString() + "*(" + YaMQ1.ToString() + "-\\frac{2*" + Yd0MQ1.ToString() + "}" + "{3})" + "}=" + Yσ2MQ1.ToString("#.##") + "MPa");



                section0.AddParagraph();
                //添加一个段落p38写耳板抗剪强度
                Paragraph p38 = section0.Paragraphs[38];//选择这个段落，我觉得有必要
                p38.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p38.Format.LineSpacing = 18f;
                p38.Format.FirstLineIndent = 24f;
                TextRange rangeTop38 = p38.AppendText("根据《钢结构设计标准》公式11.6.3-4及11.6.3-5，耳板抗剪应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop38.CharacterFormat.FontSize = 12;
                rangeTop38.CharacterFormat.Bold = false;
                rangeTop38.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop38.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();//添加一个段落p39写耳板抗剪强度公式2；
                Paragraph p39 = section0.Paragraphs[39];//选择这个段落，我觉得有必要
                p39.Format.LineSpacing = 18f;
                OfficeMath officeMath39 = new OfficeMath(doc);
                p39.Items.Add(officeMath39);
                officeMath39.FromLatexMathCode("Z=\\sqrt{(a+d0/2)^2-(d0/2)^2}=\\sqrt{(" + YaMQ1.ToString() + "+" + Yd0MQ1.ToString() + "/2)^2-(" + Yd0MQ1.ToString() + "/2)^2}=" + YZMQ1.ToString("#.##") + "mm");

                section0.AddParagraph();//添加一个段落p40写耳板抗剪强度公式1；
                Paragraph p40 = section0.Paragraphs[40];//选择这个段落，我觉得有必要
                p40.Format.LineSpacing = 18f;
                OfficeMath officeMath40 = new OfficeMath(doc);
                p40.Items.Add(officeMath40);
                officeMath40.FromLatexMathCode("τ=\\frac{N}{2tZ}=\\frac{" + YNMQ1.ToString("#.##") + "*1000}{2*" + YtMQ1.ToString() + "*" + YZMQ1.ToString("#.##") + "}=" + YτMQ1.ToString("#.##") + "MPa");


                ////这里增加局部抗压，由于是另外加入，所以需要在71后面加后缀//TMD,顺延把

                section0.AddParagraph();
                //添加一个段落p69写耳板局部受压强度
                Paragraph p41 = section0.Paragraphs[41];//选择这个段落，我觉得有必要
                p41.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p41.Format.LineSpacing = 18f;
                p41.Format.FirstLineIndent = 24f;
                TextRange rangeTop41 = p41.AppendText("根据《钢结构设计标准》公式11.6.6-1，耳板承压强度计算如下。");//选择这个段落，我觉得有必要
                rangeTop41.CharacterFormat.FontSize = 12;
                rangeTop41.CharacterFormat.Bold = false;
                rangeTop41.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop41.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();//添加一个段落p42写耳板承压强度公式2；
                Paragraph p42 = section0.Paragraphs[42];//选择这个段落，我觉得有必要
                p42.Format.LineSpacing = 18f;
                OfficeMath officeMath42 = new OfficeMath(doc);
                p42.Items.Add(officeMath42);
                officeMath42.FromLatexMathCode("σ_c=\\frac{N}{2dt}=\\frac{" + YNMQ1.ToString("#.##") + "*1000}{(" + Yd0MQ1.ToString() + "-1)" + "*" + YtMQ1.ToString() + "}=" + YcMQ1.ToString("#.##") + "MPa");



                section0.AddParagraph();
                //添加一个段落p43写耳板材料强度
                Paragraph p43 = section0.Paragraphs[43];//选择这个段落，我觉得有必要
                p43.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p43.Format.LineSpacing = 18f;
                p43.Format.FirstLineIndent = 24f;
                TextRange rangeTop43 = p43.AppendText("由于耳板厚度t=" + YtMQ1.ToString() + "mm" + "，" + "耳板材质为" + BoxBMQ1.Text + "，" + "则耳板抗拉及拉压强度f=" + YfMQ1.ToString() + "MPa，耳板抗剪强度fv=" + YfvMQ1.ToString() + "MPa，" + "耳板承压强度fc=" + YfcMQ1.ToString() + "MPa。");//选择这个段落，我觉得有必要
                rangeTop43.CharacterFormat.FontSize = 12;
                rangeTop43.CharacterFormat.Bold = false;
                rangeTop43.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop43.CharacterFormat.FontNameNonFarEast = "Times New Roman";




                section0.AddParagraph();//添加一个段落p44判定耳板孔净截面处的抗拉强度；
                Paragraph p44 = section0.Paragraphs[44];//选择这个段落，我觉得有必要
                p44.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p44.Format.LineSpacing = 18f;
                p44.Format.FirstLineIndent = 24f;
                if (Yσ1MQ1 <= YfMQ1)
                {
                    TextRange rangeTop44 = p44.AppendText("则耳板孔净截面处的抗拉强度：σ=" + Yσ1MQ1.ToString("#.##") + "≤" + YfMQ1.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop44.CharacterFormat.FontSize = 12;
                    rangeTop44.CharacterFormat.Bold = false;
                    rangeTop44.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop44.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop44 = p44.AppendText("则耳板孔净截面处的抗拉强度：σ=" + Yσ1MQ1.ToString("#.##") + ">" + YfMQ1.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop44.CharacterFormat.FontSize = 12;
                    rangeTop44.CharacterFormat.Bold = true;
                    rangeTop44.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop44.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop44.CharacterFormat.TextColor = Color.Red;
                }


                section0.AddParagraph();//添加一个段落p45判定耳板端部截面抗拉(劈开）强度；
                Paragraph p45 = section0.Paragraphs[45];//选择这个段落，我觉得有必要
                p45.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p45.Format.LineSpacing = 18f;
                p45.Format.FirstLineIndent = 24f;
                if (Yσ2MQ1 <= YfMQ1)
                {
                    TextRange rangeTop45 = p45.AppendText("耳板端部截面抗拉(劈开）强度：σ=" + Yσ2MQ1.ToString("#.##") + "≤" + YfMQ1.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop45.CharacterFormat.FontSize = 12;
                    rangeTop45.CharacterFormat.Bold = false;
                    rangeTop45.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop45.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop45 = p45.AppendText("耳板端部截面抗拉(劈开）强度：σ=" + Yσ2MQ1.ToString("#.##") + ">" + YfMQ1.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop45.CharacterFormat.FontSize = 12;
                    rangeTop45.CharacterFormat.Bold = true;
                    rangeTop45.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop45.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop45.CharacterFormat.TextColor = Color.Red;
                }





                section0.AddParagraph();//添加一个段落p46判定耳板端部截面抗拉(劈开）强度；
                Paragraph p46 = section0.Paragraphs[46];//选择这个段落，我觉得有必要
                p46.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p46.Format.LineSpacing = 18f;
                p46.Format.FirstLineIndent = 24f;
                if (YτMQ1 <= YfvMQ1)
                {
                    TextRange rangeTop46 = p46.AppendText("耳板抗剪强度：τ=" + YτMQ1.ToString("#.##") + "≤" + YfvMQ1.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop46.CharacterFormat.FontSize = 12;
                    rangeTop46.CharacterFormat.Bold = false;
                    rangeTop46.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop46.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop46 = p46.AppendText("耳板抗剪强度：τ=" + YτMQ1.ToString("#.##") + ">" + YfvMQ1.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop46.CharacterFormat.FontSize = 12;
                    rangeTop46.CharacterFormat.Bold = true;
                    rangeTop46.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop46.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop46.CharacterFormat.TextColor = Color.Red;
                }


                //这里又加了一段，没办法，顺延吧
                section0.AddParagraph();//添加一个段落p47判定耳板承压强度；
                Paragraph p47 = section0.Paragraphs[47];//选择这个段落，我觉得有必要
                p47.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p47.Format.LineSpacing = 18f;
                p47.Format.FirstLineIndent = 24f;
                if (YσcMQ1 <= YfcMQ1)
                {
                    TextRange rangeTop47 = p47.AppendText("耳板承压强度：σc=" + YσcMQ1.ToString("#.##") + "≤" + YfcMQ1.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop47.CharacterFormat.FontSize = 12;
                    rangeTop47.CharacterFormat.Bold = false;
                    rangeTop47.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop47.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop47 = p47.AppendText("耳板承压强度：σc=" + YσcMQ1.ToString("#.##") + ">" + YfcMQ1.ToString() + "MPa；");//选择这个段落，我觉得有必要
                    rangeTop47.CharacterFormat.FontSize = 12;
                    rangeTop47.CharacterFormat.Bold = true;
                    rangeTop47.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop47.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop47.CharacterFormat.TextColor = Color.Red;
                }



                section0.AddParagraph();//添加一个段落p48总判定；
                Paragraph p48 = section0.Paragraphs[48];//选择这个段落，我觉得有必要
                p48.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p48.Format.LineSpacing = 18f;
                p48.Format.FirstLineIndent = 24f;
                if (YaMQ1 >= (YbeMQ1 / 3 * 4) & YbMQ1 >= YbeMQ1 & Yσ1MQ1 <= YfMQ1 & Yσ2MQ1 <= YfMQ1 & YτMQ1 <= YfvMQ1 & YσcMQ1 <= YfcMQ1)
                {
                    TextRange rangeTop48 = p48.AppendText("综上，耳板构造及强度满足要求。");//选择这个段落，我觉得有必要
                    rangeTop48.CharacterFormat.FontSize = 12;
                    rangeTop48.CharacterFormat.Bold = false;
                    rangeTop48.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop48.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop48 = p48.AppendText("综上，耳板构造及强度中至少有一项不满足要求。");//选择这个段落，我觉得有必要
                    rangeTop48.CharacterFormat.FontSize = 12;
                    rangeTop48.CharacterFormat.Bold = true;
                    rangeTop48.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop48.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop48.CharacterFormat.TextColor = Color.Red;
                }


                section0.AddParagraph();
                //添加一个段落 p49 写第4节标题
                Paragraph p49 = section0.Paragraphs[49];//选择这个段落，我觉得有必要
                p49.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p49.Format.LineSpacing = 18f;
                TextRange rangeTop49 = p49.AppendText("XX.4 吊耳底部焊缝验算");//选择这个段落，我觉得有必要
                rangeTop49.CharacterFormat.FontSize = 16;
                rangeTop49.CharacterFormat.Bold = true;
                rangeTop49.CharacterFormat.FontNameFarEast = "黑体";
                rangeTop49.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p50说明；
                Paragraph p50 = section0.Paragraphs[50];//选择这个段落，我觉得有必要
                p50.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p50.Format.LineSpacing = 18f;
                p50.Format.FirstLineIndent = 24f;
                TextRange rangeTop50 = p50.AppendText("吊耳底部采用全熔透焊缝，根据《钢结构设计标准》公式11.2.1-1及公式11.2.1-2，焊缝强度验算公式如下。");//选择这个段落，我觉得有必要
                rangeTop50.CharacterFormat.FontSize = 12;
                rangeTop50.CharacterFormat.Bold = false;
                rangeTop50.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop50.CharacterFormat.FontNameNonFarEast = "Times New Roman";

                section0.AddParagraph();//添加一个段落p51写耳板底部焊缝强度公式1；
                Paragraph p51 = section0.Paragraphs[51];//选择这个段落，我觉得有必要
                p51.Format.LineSpacing = 18f;
                OfficeMath officeMath51 = new OfficeMath(doc);
                p51.Items.Add(officeMath51);
                officeMath51.FromLatexMathCode("σ=\\frac{N}{l_wh_e}≤f_t^w");



                section0.AddParagraph();//添加一个段落p52说明；
                Paragraph p52 = section0.Paragraphs[52];//选择这个段落，我觉得有必要
                p52.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p52.Format.LineSpacing = 18f;
                p52.Format.FirstLineIndent = 24f;
                TextRange rangeTop52 = p52.AppendText("经过查表，熔透焊缝强度与母材强度相等，即:");//选择这个段落，我觉得有必要
                rangeTop52.CharacterFormat.FontSize = 12;
                rangeTop52.CharacterFormat.Bold = false;
                rangeTop52.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop52.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                OfficeMath officeMath52 = new OfficeMath(doc);
                p52.Items.Add(officeMath52);
                officeMath52.FromLatexMathCode("f_t^w=f");
                TextRange rangeTop52x = p52.AppendText("。");//选择这个段落，我觉得有必要



                section0.AddParagraph();//添加一个段落p53说明第二个分力2；
                Paragraph p53 = section0.Paragraphs[53];//选择这个段落，我觉得有必要
                p53.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p53.Format.LineSpacing = 18f;
                p53.Format.FirstLineIndent = 24f;
                TextRange rangeTop53 = p53.AppendText("基本组合下，用于焊缝计算的拉力设计值为：" + YNMQ1.ToString("0.00")+ "kN");//选择这个段落，我觉得有必要
                rangeTop53.CharacterFormat.FontSize = 12;
                rangeTop53.CharacterFormat.Bold = false;
                rangeTop53.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop53.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();//添加一个段落p54说明；
                Paragraph p54 = section0.Paragraphs[54];//选择这个段落，我觉得有必要
                p54.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p54.Format.LineSpacing = 18f;
                p54.Format.FirstLineIndent = 24f;
                TextRange rangeTop54 = p54.AppendText("吊耳位置因拉压产生的应力计算如下。");//选择这个段落，我觉得有必要
                rangeTop54.CharacterFormat.FontSize = 12;
                rangeTop54.CharacterFormat.Bold = false;
                rangeTop54.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop54.CharacterFormat.FontNameNonFarEast = "Times New Roman";


                section0.AddParagraph();//添加一个段落p55位置1的拉压；
                Paragraph p55 = section0.Paragraphs[55];//选择这个段落，我觉得有必要
                p55.Format.LineSpacing = 18f;
                OfficeMath officeMath55 = new OfficeMath(doc);
                p55.Items.Add(officeMath55);
                officeMath55.FromLatexMathCode("σ_1=\\frac{N}{2t(e+f)}=\\frac{" + YNMQ1.ToString("#.##") + "*1000}{2*" + YtMQ1.ToString() + "*(" + YeMQ1.ToString() + "+" + YffMQ1.ToString() + ")}=" + YσF1MQ1.ToString("#.##") + "MPa"); ;


                section0.AddParagraph();//添加一个段落p56说明；
                Paragraph p56 = section0.Paragraphs[56];//选择这个段落，我觉得有必要
                p56.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p56.Format.LineSpacing = 18f;
                p56.Format.FirstLineIndent = 24f;
                if ( YσF1MQ1 <= YfMQ1)
                {
                    TextRange rangeTop56 = p56.AppendText("最大拉压应力" + YσF1MQ1.ToString("#.##") + "≤" + YfMQ1.ToString() + "MPa。");//选择这个段落，我觉得有必要
                    rangeTop56.CharacterFormat.FontSize = 12;
                    rangeTop56.CharacterFormat.Bold = false;
                    rangeTop56.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop56.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop56 = p56.AppendText("最大拉压应力" + YσF1MQ1.ToString("#.##") + ">" + YfMQ1.ToString() + "MPa。");
                    rangeTop56.CharacterFormat.FontSize = 12;
                    rangeTop56.CharacterFormat.Bold = false;
                    rangeTop56.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop56.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }



                section0.AddParagraph();//添加一个段落p57说明；
                Paragraph p57 = section0.Paragraphs[57];//选择这个段落，我觉得有必要
                p57.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p57.Format.LineSpacing = 18f;
                p57.Format.FirstLineIndent = 24f;
                if (YσF1MQ1 <= YfMQ1)
                {
                    TextRange rangeTop57 = p57.AppendText("满足要求。");//选择这个段落，我觉得有必要
                    rangeTop57.CharacterFormat.FontSize = 12;
                    rangeTop57.CharacterFormat.Bold = false;
                    rangeTop57.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop57.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop57 = p57.AppendText("不满足要求。");
                    rangeTop57.CharacterFormat.FontSize = 12;
                    rangeTop57.CharacterFormat.Bold = true;
                    rangeTop57.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop57.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop57.CharacterFormat.TextColor = Color.Red;
                }




                section0.AddParagraph();
                //添加一个段落 p58写第5节标题
                Paragraph p58 = section0.Paragraphs[58];//选择这个段落，我觉得有必要
                p58.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p58.Format.LineSpacing = 18f;
                TextRange rangeTop58 = p58.AppendText("XX.5 小结");//选择这个段落，我觉得有必要
                rangeTop58.CharacterFormat.FontSize = 16;
                rangeTop58.CharacterFormat.Bold = true;
                rangeTop58.CharacterFormat.FontNameFarEast = "黑体";
                rangeTop58.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();//添加一个段落p59说明；
                Paragraph p59 = section0.Paragraphs[59];//选择这个段落，我觉得有必要
                p59.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p59.Format.LineSpacing = 18f;
                p59.Format.FirstLineIndent = 24f;
                TextRange rangeTop59 = p59.AppendText("根据2~4节计算，可得如下结论。");//选择这个段落，我觉得有必要
                rangeTop59.CharacterFormat.FontSize = 12;
                rangeTop59.CharacterFormat.Bold = false;
                rangeTop59.CharacterFormat.FontNameFarEast = "宋体";
                rangeTop59.CharacterFormat.FontNameNonFarEast = "Times New Roman";



                section0.AddParagraph();//添加一个段落p60说明；
                Paragraph p60 = section0.Paragraphs[60];//选择这个段落，我觉得有必要
                p60.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p60.Format.LineSpacing = 18f;
                p60.Format.FirstLineIndent = 24f;
                if (rFgMQ1 >= YN2MQ1 & rFhMQ1 >= YN3MQ1)
                {
                    TextRange rangeTop60 = p60.AppendText("（1）吊耳及卡环满足要求；");//选择这个段落，我觉得有必要
                    rangeTop60.CharacterFormat.FontSize = 12;
                    rangeTop60.CharacterFormat.Bold = false;
                    rangeTop60.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop60.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop60 = p60.AppendText("（1）吊耳及卡环中存在不满足项；");//选择这个段落，我觉得有必要
                    rangeTop60.CharacterFormat.FontSize = 12;
                    rangeTop60.CharacterFormat.Bold = true;
                    rangeTop60.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop60.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop60.CharacterFormat.TextColor = Color.Red;
                }



                section0.AddParagraph();//添加一个段落p61说明；
                Paragraph p61 = section0.Paragraphs[61];//选择这个段落，我觉得有必要
                p61.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p61.Format.LineSpacing = 18f;
                p61.Format.FirstLineIndent = 24f;
                if (YaMQ1 >= (YbeMQ1 / 3 * 4) & YbMQ1 >= YbeMQ1 & Yσ1MQ1 <= YfMQ1 & Yσ2MQ1 <= YfMQ1 & YτMQ1 <= YfvMQ1)
                {
                    TextRange rangeTop61 = p61.AppendText("（2）吊耳板自身构造及强度满足要求；");//选择这个段落，我觉得有必要
                    rangeTop61.CharacterFormat.FontSize = 12;
                    rangeTop61.CharacterFormat.Bold = false;
                    rangeTop61.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop61.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop61 = p61.AppendText("（2）吊耳板自身构造或强度不满足要求；");//选择这个段落，我觉得有必要
                    rangeTop61.CharacterFormat.FontSize = 12;
                    rangeTop61.CharacterFormat.Bold = true;
                    rangeTop61.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop61.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop61.CharacterFormat.TextColor = Color.Red;
                }





                section0.AddParagraph();//添加一个段落p113说明；
                Paragraph p62 = section0.Paragraphs[62];//选择这个段落，我觉得有必要
                p62.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p62.Format.LineSpacing = 18f;
                p62.Format.FirstLineIndent = 24f;
                if (YσF1MQ1 <= YfMQ1)
                {
                    TextRange rangeTop62 = p62.AppendText("（3）吊耳板底部焊缝强度满足要求；");//选择这个段落，我觉得有必要
                    rangeTop62.CharacterFormat.FontSize = 12;
                    rangeTop62.CharacterFormat.Bold = false;
                    rangeTop62.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop62.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop62 = p62.AppendText("（3）吊耳板底部焊缝强度不满足要求；");//选择这个段落，我觉得有必要
                    rangeTop62.CharacterFormat.FontSize = 12;
                    rangeTop62.CharacterFormat.Bold = true;
                    rangeTop62.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop62.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop62.CharacterFormat.TextColor = Color.Red;
                }



                section0.AddParagraph();//添加一个段落p113说明；
                Paragraph p63 = section0.Paragraphs[63];//选择这个段落，我觉得有必要
                p63.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                p63.Format.LineSpacing = 18f;
                p63.Format.FirstLineIndent = 24f;
                if (rFgMQ1 >= YN2MQ1 & rFhMQ1 >= YN3MQ1 & YaMQ1 >= (YbeMQ1 / 3 * 4) & YbMQ1 >= YbeMQ1 & Yσ1MQ1 <= YfMQ1 & Yσ2MQ1 <= YfMQ1 & YτMQ1 <= YfvMQ1 & YσcMQ1 <= YfcMQ1  & YσF1MQ1<= YfMQ1)
                {
                    TextRange rangeTop63 = p63.AppendText("满足要求。");//选择这个段落，我觉得有必要
                    rangeTop63.CharacterFormat.FontSize = 12;
                    rangeTop63.CharacterFormat.Bold = false;
                    rangeTop63.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop63.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                }
                else
                {
                    TextRange rangeTop63 = p63.AppendText("不满足要求。");//选择这个段落，我觉得有必要
                    rangeTop63.CharacterFormat.FontSize = 12;
                    rangeTop63.CharacterFormat.Bold = true;
                    rangeTop63.CharacterFormat.FontNameFarEast = "宋体";
                    rangeTop63.CharacterFormat.FontNameNonFarEast = "Times New Roman";
                    rangeTop63.CharacterFormat.TextColor = Color.Red;
                }


                //保存文档       
                string ttx = DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒");
                string JSSD1 = System.Windows.Forms.Application.StartupPath;
                //Image image86 = Image.FromFile(@strp86 + "\\算吊耳内力.jpg");
                string bt = System.Windows.Forms.Application.StartupPath + "\\JSS\\单吊耳" + "（" + ttx + "）" + ".docx";
                doc.SaveToFile(bt, FileFormat.Docx);
                MessageBox.Show(bt.ToString() + "已创建");
                button4.Enabled = true;//

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //这里是加图片开始
            AcadApplication AcadApp;
            AcadDocument AcadDoc;
            //try
            //{
            AcadApp = (AcadApplication)System.Runtime.InteropServices.Marshal.GetActiveObject("AUTOCAD.Application");

            AcadDoc = AcadApp.ActiveDocument;
            //}
            //catch
            //{

            //   // string filePath = "C:\\hehddehi.dwg";
            //   // if (filePath == "")
            //   // {
            //   //     MessageBox.Show("选择CAD文件无效！", "文件无效！");
            //   //     Application.Exit();

            //   // }

            //   AcadApp = new AcadApplication();
            //   AcadDoc = AcadApp.Documents.Open("C:\\d1.dwg", null, null);
            //}
            double[] S1TK = new double[3] { 0, 0, 0 };
            string pickPrompt1TK = "选取左下角点!";
            //MessageBox.Show("选左下角点");
            double[] S2TK = AcadDoc.Utility.GetPoint(S1TK, pickPrompt1TK);

            double S2TKX = S2TK[0];
            double S2TKY = S2TK[1];
            double S2TKZ = S2TK[2];

            AutoCAD.AcadMInsertBlock TK1 = AcadDoc.ModelSpace.AddMInsertBlock(S2TK, "DETK", 1, 1, 1, 0, 1, 1, 0, 0);

            AcadTextStyle acadTextStyle = AcadDoc.TextStyles.Add("dd");
            acadTextStyle.SetFont("Times New Roman", false, false, 1, 1);
            AcadApp.Visible = true;
            double ex = Convert.ToDouble(te.Text);
            double fx = Convert.ToDouble(tf.Text);
            double bx = Convert.ToDouble(tb.Text);
            double d0x = Convert.ToDouble(td0.Text);
            double ax = Convert.ToDouble(ta.Text);
            double cx = Convert.ToDouble(tc.Text);
            double gx = Convert.ToDouble(ttg.Text);
            double gdx = Convert.ToDouble(ttg.Text) * 0.3;
            double tx = Convert.ToDouble(tt.Text);
            string czx = BoxB.Text;

            int FDXS = 20;
            //这是画第一张图。

            //增加点

            //第一张图的移动点
            //double S2TKX = S2TK[0];
            //double S2TKY = S2TK[1];
            //double S2TKZ = S2TK[2];
            double[] D1YDS = new double[3] { 0, 0, 0 };
            double[] D1YDZ = new double[3] { 9600+S2TKX, 16800+ S2TKY, 0+S2TKZ};

            double[] StartPoint1 = new double[3] { 0, 0, 0 };
            double[] EndPoint1 = new double[3] { FDXS * (ex + fx + ex + fx), 0, 0 };

            AcadLine oAcadLine = AcadDoc.ModelSpace.AddLine(StartPoint1, EndPoint1);//这个是实物，要移动
           
            double[] YXPoint1 = new double[3] { FDXS * (ex + fx), FDXS * (cx + d0x / 2), 0 };
            // double[] YXPoint1 = new double[3] { FDXS * (ex + fx), FDXS * (cx + d0x / 2), 0 };
            double[] YXPoint2 = new double[3] { YXPoint1[0], FDXS * (cx + d0x / 2) + FDXS * ax - FDXS * bx, 0 };
            double radius1 = FDXS * d0x / 2;
            double radius2 = FDXS * (d0x / 2 + bx);   
            AcadCircle circle1= AcadDoc.ModelSpace.AddCircle(YXPoint1, radius1);
            
            double LR = Math.Sqrt((EndPoint1[0] - YXPoint2[0]) * (EndPoint1[0] - YXPoint2[0]) + (EndPoint1[1] - YXPoint2[1]) * (EndPoint1[1] - YXPoint2[1]) + (EndPoint1[2] - YXPoint2[2]) * (EndPoint1[2] - YXPoint2[2]));

            double cosLR = radius2 / LR;
            double cosDLR = YXPoint2[1] / LR;
            //MessageBox.Show(LR.ToString());
            //MessageBox.Show(YXPoint1[1].ToString());
            //MessageBox.Show(cosDLR.ToString());
            //MessageBox.Show(Math.Acos(cosLR).ToString());
            //MessageBox.Show(Math.Acos(cosDLR).ToString());
            //MessageBox.Show(((Math.PI) / 2).ToString());

            AcadArc DSD = AcadDoc.ModelSpace.AddArc(YXPoint2, radius2, (Math.Acos(cosLR) + Math.Acos(cosDLR) - ((Math.PI) / 2)), (Math.PI - (Math.Acos(cosLR) + Math.Acos(cosDLR) - (Math.PI / 2))));
            
            AcadLine oAcadLine2 = AcadDoc.ModelSpace.AddLine(EndPoint1, DSD.StartPoint);
            
            AcadLine oAcadLine3 = AcadDoc.ModelSpace.AddLine(StartPoint1, DSD.EndPoint);
            


            double[] zjj1d = new double[3] { FDXS * (ex - 6), 0, 0 };
            double[] zjj1s = new double[3] { FDXS * (ex - 6), 1000, 0 };

            double[] zjj2d = new double[3] { FDXS * (ex + 6), 0, 0 };
            double[] zjj2s = new double[3] { FDXS * (ex + 6), 1000, 0 };

            double[] zjj3z = new double[3] { FDXS * (ex - 6), 300, 0 };
            double[] zjj3y = new double[3] { FDXS * (ex + 6), 300, 0 };

            double[] zjj4z = new double[3] { FDXS * (ex - 6), 1000, 0 };
            double[] zjj4y = new double[3] { FDXS * (ex + 6), 1000, 0 };


            AcadLine jjx1 = AcadDoc.ModelSpace.AddLine(zjj1d, zjj1s);
            

            AcadLine jjx2 = AcadDoc.ModelSpace.AddLine(zjj2d, zjj2s);
            

            AcadLine jjx3 = AcadDoc.ModelSpace.AddLine(zjj3z, zjj3y);
            

            AcadLine jjx4 = AcadDoc.ModelSpace.AddLine(zjj4z, zjj4y);
            


            double[] yjj1d = new double[3] { FDXS * (ex - 6 + 2 * fx), 0, 0 };
            double[] yjj1s = new double[3] { FDXS * (ex - 6 + 2 * fx), 1000, 0 };

            double[] yjj2d = new double[3] { FDXS * (ex + 6 + 2 * fx), 0, 0 };
            double[] yjj2s = new double[3] { FDXS * (ex + 6 + 2 * fx), 1000, 0 };

            double[] yjj3z = new double[3] { FDXS * (ex - 6 + 2 * fx), 300, 0 };
            double[] yjj3y = new double[3] { FDXS * (ex + 6 + 2 * fx), 300, 0 };

            double[] yjj4z = new double[3] { FDXS * (ex - 6 + 2 * fx), 1000, 0 };
            double[] yjj4y = new double[3] { FDXS * (ex + 6 + 2 * fx), 1000, 0 };


            AcadLine yjjx1 = AcadDoc.ModelSpace.AddLine(yjj1d, yjj1s);
            

            AcadLine yjjx2 = AcadDoc.ModelSpace.AddLine(yjj2d, yjj2s);
            

            AcadLine yjjx3 = AcadDoc.ModelSpace.AddLine(yjj3z, yjj3y);
            

            AcadLine yjjx4 = AcadDoc.ModelSpace.AddLine(yjj4z, yjj4y);

            

            Boolean SFCZ = false;
            foreach (AcadLineType acadLineType in AcadDoc.Linetypes)
            {
                if (acadLineType.Name == "CENTER")
                {
                    SFCZ = true;
                }


            }


            if (SFCZ)
            { }
            else
            {
                AcadDoc.Linetypes.Load("center", "acadiso.lin");
                SFCZ = false;
            }


            // yjjx1.Linetype = "center";
            //yjjx1.LinetypeScale =5;
            double[] dwxz = new double[3] { YXPoint1[0] - FDXS * d0x / 2 - FDXS * bx - bx, YXPoint1[1], 0 };
            double[] dwxy = new double[3] { YXPoint1[0] + FDXS * d0x / 2 + bx + FDXS * bx, YXPoint1[1], 0 };

            double[] dwxs = new double[3] { YXPoint1[0], YXPoint1[1] + FDXS * d0x / 2 + FDXS * ax, 0 };
            double[] dwxx = new double[3] { YXPoint1[0], 0, 0 };

            AcadLine dwx1 = AcadDoc.ModelSpace.AddLine(dwxz, dwxy);
            

            AcadLine dwx2 = AcadDoc.ModelSpace.AddLine(dwxs, dwxx);
            

            dwx1.Linetype = "center";
            dwx1.LinetypeScale = 1;     

            dwx2.Linetype = "center";
            dwx2.LinetypeScale = 1;

            //double[] c1 = new double[3] {-100, -100,0};
            //double[] c2 = new double[3] { 10 * (ex + fx + ex + fx) + 100, 10 * (cx + d0x+ax) + 100, 0};



            double[] uTemporaryPoint = new double[3] { 2000, FDXS * cx + FDXS * d0x + FDXS * ax + 800, 0 };
            double[] dTemporaryPoint = new double[3] { 2000, -800, 0 };
            double[] rTemporaryPoint = new double[3] { FDXS * 2 * ex + FDXS * 2 * fx + 800, 3000, 0 };


            //这段是第一个图的底部横向标注点
            double[] bzpoint1 = new double[3] { StartPoint1[0], StartPoint1[1], StartPoint1[2] };
            double[] bzpoint2 = new double[3] { StartPoint1[0] + FDXS * ex, StartPoint1[1], StartPoint1[2] };
            double[] bzpoint3 = new double[3] { bzpoint2[0] + FDXS * fx, bzpoint2[1], bzpoint2[2] };
            double[] bzpoint4 = new double[3] { bzpoint3[0] + FDXS * fx, bzpoint3[1], bzpoint3[2] };
            double[] bzpoint5 = new double[3] { bzpoint4[0] + FDXS * ex, bzpoint4[1], bzpoint4[2] };

            //这段是加第一个图的底部横向标注的线

            AcadDimAligned oAcadDimAlignedX1 = AcadDoc.ModelSpace.AddDimAligned(bzpoint1, bzpoint2, dTemporaryPoint);
            
            AcadDimAligned oAcadDimAlignedX2 = AcadDoc.ModelSpace.AddDimAligned(bzpoint2, bzpoint3, dTemporaryPoint);
            
            AcadDimAligned oAcadDimAlignedX3 = AcadDoc.ModelSpace.AddDimAligned(bzpoint3, bzpoint4, dTemporaryPoint);
            
            AcadDimAligned oAcadDimAlignedX4 = AcadDoc.ModelSpace.AddDimAligned(bzpoint4, bzpoint5, dTemporaryPoint);
            
            //这段是第一个图的右侧竖向标注点
            //bzpoint5就是rbzpoint1
            double[] rbzpoint2 = new double[3] { bzpoint5[0], bzpoint5[1] + FDXS * cx, bzpoint5[2] };
            double[] rbzpoint3 = new double[3] { rbzpoint2[0], rbzpoint2[1] + FDXS * d0x, rbzpoint2[2] };
            double[] rbzpoint4 = new double[3] { rbzpoint3[0], rbzpoint3[1] + FDXS * ax, rbzpoint3[2] };

            //这段是第一个图加右侧竖向标注的线
            AcadDimAligned oAcadDimAlignedY1 = AcadDoc.ModelSpace.AddDimAligned(bzpoint5, rbzpoint2, rTemporaryPoint);
            

            AcadDimAligned oAcadDimAlignedY2 = AcadDoc.ModelSpace.AddDimAligned(rbzpoint2, rbzpoint3, rTemporaryPoint);
            

            AcadDimAligned oAcadDimAlignedY3 = AcadDoc.ModelSpace.AddDimAligned(rbzpoint3, rbzpoint4, rTemporaryPoint);
            

            //这段是第一个图的上侧横向标注点
            //bzpoint5就是rbzpoint1
            double[] ubzpoint1 = new double[3] { FDXS * ex + FDXS * fx - FDXS * d0x / 2 - FDXS * bx, bzpoint5[1] + FDXS * cx + FDXS * d0x + FDXS * ax, 0 };
            double[] ubzpoint2 = new double[3] { ubzpoint1[0] + FDXS * bx, ubzpoint1[1], ubzpoint1[2] };
            double[] ubzpoint3 = new double[3] { ubzpoint2[0] + FDXS * d0x, ubzpoint2[1], ubzpoint2[2] };
            double[] ubzpoint4 = new double[3] { ubzpoint3[0] + FDXS * bx, ubzpoint3[1], rbzpoint3[2] };

            //这段是第一个图加上侧竖向标注的线
            AcadDimAligned oAcadDimAlignedZ1 = AcadDoc.ModelSpace.AddDimAligned(ubzpoint1, ubzpoint2, uTemporaryPoint);
            
            AcadDimAligned oAcadDimAlignedZ2 = AcadDoc.ModelSpace.AddDimAligned(ubzpoint2, ubzpoint3, uTemporaryPoint);
            
            AcadDimAligned oAcadDimAlignedZ3 = AcadDoc.ModelSpace.AddDimAligned(ubzpoint3, ubzpoint4, uTemporaryPoint);

            //好吧，设置的不长，稳定点吧第一个图的底部横向标注的线1（4个）
            oAcadDimAlignedX1.StyleName = "TSSD_100_100";
            //oAcadDimAlignedX1.TextHeight = 300;
            //// oAcadDimAligned.Rotation = 0;
            ////oAcadDimAligned.ArrowheadSize = 200;
            ////oAcadDimAlignedX1.DimensionLineColor = ACAD_COLOR.acGreen;
            ////oAcadDimAlignedX1.TextOverride = "长度<>mm";
            //oAcadDimAlignedX1.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedX1.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedX1.ArrowheadSize = 100;
            //oAcadDimAlignedX1.TextStyle = "dd";
            //oAcadDimAlignedX1.DimensionLineExtend = 100;//吓唬谁啊
            //                                            //oAcadDimAlignedX1.ExtensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedX1.ExtensionLineExtend = 100;
            //oAcadDimAlignedX1.ExtensionLineOffset = 100;
            //oAcadDimAlignedX1.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //oAcadDimAlignedX1.TextInsideAlign = false;
            //oAcadDimAlignedX1.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //oAcadDimAlignedX1.TextGap = 50;
            double xx = oAcadDimAlignedX1.Measurement / FDXS;
            oAcadDimAlignedX1.TextOverride = xx.ToString("0");



            //第一个图的底部横向标注的线2（4个）
            oAcadDimAlignedX2.StyleName= "TSSD_100_100";
            //oAcadDimAlignedX2.TextHeight = 300;
            ////oAcadDimAlignedX2.DimensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedX2.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedX2.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedX2.ArrowheadSize = 100;
            //oAcadDimAlignedX2.TextStyle = "dd";
            //oAcadDimAlignedX2.DimensionLineExtend = 100;//吓唬谁啊
            //                                            //oAcadDimAlignedX2.ExtensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedX2.ExtensionLineExtend = 100;
            //oAcadDimAlignedX2.ExtensionLineOffset = 100;
            //oAcadDimAlignedX2.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //oAcadDimAlignedX2.TextInsideAlign = false;
            //oAcadDimAlignedX2.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //oAcadDimAlignedX2.TextGap = 50;
            xx = oAcadDimAlignedX2.Measurement / FDXS;
            oAcadDimAlignedX2.TextOverride = xx.ToString("0");


            oAcadDimAlignedX3.StyleName = "TSSD_100_100";
            //第一个图的底部横向标注的线3（4个）
            //oAcadDimAlignedX3.TextHeight = 300;
            ////oAcadDimAlignedX3.DimensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedX3.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedX3.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedX3.ArrowheadSize = 100;
            //oAcadDimAlignedX3.TextStyle = "dd";
            //oAcadDimAlignedX3.DimensionLineExtend = 100;//吓唬谁啊
            //                                            //oAcadDimAlignedX3.ExtensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedX3.ExtensionLineExtend = 100;
            //oAcadDimAlignedX3.ExtensionLineOffset = 100;
            //oAcadDimAlignedX3.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //oAcadDimAlignedX3.TextInsideAlign = false;
            //oAcadDimAlignedX3.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //oAcadDimAlignedX3.TextGap = 50;
            xx = oAcadDimAlignedX3.Measurement / FDXS;
            oAcadDimAlignedX3.TextOverride = xx.ToString("0");

            oAcadDimAlignedX4.StyleName = "TSSD_100_100";
            //第一个图的底部横向标注的线4（4个）
            //oAcadDimAlignedX4.TextHeight = 300;
            ////oAcadDimAlignedX4.DimensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedX4.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedX4.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedX4.ArrowheadSize = 100;
            //oAcadDimAlignedX4.TextStyle = "dd";
            //oAcadDimAlignedX4.DimensionLineExtend = 100;//吓唬谁啊
            //                                            // oAcadDimAlignedX4.ExtensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedX4.ExtensionLineExtend = 100;
            //oAcadDimAlignedX4.ExtensionLineOffset = 100;
            //oAcadDimAlignedX4.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //oAcadDimAlignedX4.TextInsideAlign = false;
            //oAcadDimAlignedX4.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //oAcadDimAlignedX4.TextGap = 50;
            xx = oAcadDimAlignedX4.Measurement / FDXS;
            oAcadDimAlignedX4.TextOverride = xx.ToString("0");

            oAcadDimAlignedY1.StyleName = "TSSD_100_100";
            //第一个图的右侧竖向标注的线1（3个）
            //oAcadDimAlignedY1.TextHeight = 300;
            //// oAcadDimAlignedY1.DimensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedY1.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedY1.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedY1.ArrowheadSize = 100;
            //oAcadDimAlignedY1.TextStyle = "dd";
            //oAcadDimAlignedY1.DimensionLineExtend = 100;//吓唬谁啊
            //                                            // oAcadDimAlignedY1.ExtensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedY1.ExtensionLineExtend = 100;
            //oAcadDimAlignedY1.ExtensionLineOffset = 100;
            //oAcadDimAlignedY1.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //oAcadDimAlignedY1.TextInsideAlign = false;
            //oAcadDimAlignedY1.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //oAcadDimAlignedY1.TextGap = 50;
            xx = oAcadDimAlignedY1.Measurement / FDXS;
            oAcadDimAlignedY1.TextOverride = xx.ToString("0");

            oAcadDimAlignedY2.StyleName = "TSSD_100_100";
            //第一个图的右侧竖向标注的线2（3个）
            //oAcadDimAlignedY2.TextHeight = 300;
            ////oAcadDimAlignedY2.DimensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedY2.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedY2.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedY2.ArrowheadSize = 100;
            //oAcadDimAlignedY2.TextStyle = "dd";
            //oAcadDimAlignedY2.DimensionLineExtend = 100;//吓唬谁啊
            //                                            //oAcadDimAlignedY2.ExtensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedY2.ExtensionLineExtend = 100;
            //oAcadDimAlignedY2.ExtensionLineOffset = 100;
            //oAcadDimAlignedY2.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //oAcadDimAlignedY2.TextInsideAlign = false;
            //oAcadDimAlignedY2.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //oAcadDimAlignedY2.TextGap = 50;
            xx = oAcadDimAlignedY2.Measurement / FDXS;
            oAcadDimAlignedY2.TextOverride = xx.ToString("0");


            oAcadDimAlignedY3.StyleName = "TSSD_100_100";
            //第一个图的右侧竖向标注的线3（3个）
            //oAcadDimAlignedY3.TextHeight = 300;
            //// oAcadDimAlignedY3.DimensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedY3.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedY3.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedY3.ArrowheadSize = 100;
            //oAcadDimAlignedY3.TextStyle = "dd";
            //oAcadDimAlignedY3.DimensionLineExtend = 100;//吓唬谁啊
            //                                            //oAcadDimAlignedY3.ExtensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedY3.ExtensionLineExtend = 100;
            //oAcadDimAlignedY3.ExtensionLineOffset = 100;
            //oAcadDimAlignedY3.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //oAcadDimAlignedY3.TextInsideAlign = false;
            //oAcadDimAlignedY3.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //oAcadDimAlignedY3.TextGap = 50;
            xx = oAcadDimAlignedY3.Measurement / FDXS;
            oAcadDimAlignedY3.TextOverride = xx.ToString("0");


            oAcadDimAlignedZ1.StyleName = "TSSD_100_100";
            //第一个图的顶部横向标注的线1（3个）
            //oAcadDimAlignedZ1.TextHeight = 300;
            //// oAcadDimAlignedZ1.DimensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedZ1.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedZ1.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedZ1.ArrowheadSize = 100;
            //oAcadDimAlignedZ1.TextStyle = "dd";
            //oAcadDimAlignedZ1.DimensionLineExtend = 100;//吓唬谁啊
            //                                            //oAcadDimAlignedZ1.ExtensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedZ1.ExtensionLineExtend = 100;
            //oAcadDimAlignedZ1.ExtensionLineOffset = 100;
            //oAcadDimAlignedZ1.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //oAcadDimAlignedZ1.TextInsideAlign = true;//到底能不能不动
            //                                         //oAcadDimAlignedZ1.TextPosition =uTemporaryPoint;
            //oAcadDimAlignedZ1.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //oAcadDimAlignedZ1.TextGap = 50;
            xx = oAcadDimAlignedZ1.Measurement / FDXS;
            oAcadDimAlignedZ1.TextOverride = xx.ToString("0");



            oAcadDimAlignedZ2.StyleName = "TSSD_100_100";
            //第一个图的顶部横向标注的线2（3个）
            //oAcadDimAlignedZ2.TextHeight = 300;
            //// oAcadDimAlignedZ2.DimensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedZ2.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedZ2.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedZ2.ArrowheadSize = 100;
            //oAcadDimAlignedZ2.TextStyle = "dd";
            //oAcadDimAlignedZ2.DimensionLineExtend = 100;//吓唬谁啊
            //                                            // oAcadDimAlignedZ2.ExtensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedZ2.ExtensionLineExtend = 100;
            //oAcadDimAlignedZ2.ExtensionLineOffset = 100;
            //oAcadDimAlignedZ2.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //oAcadDimAlignedZ2.TextInsideAlign = true;//到底能不能不动
            //                                         //oAcadDimAlignedZ2.TextPosition = uTemporaryPoint;
            //oAcadDimAlignedZ2.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //oAcadDimAlignedZ2.TextGap = 50;
            xx = oAcadDimAlignedZ2.Measurement / FDXS;
            oAcadDimAlignedZ2.TextOverride = xx.ToString("0");


            oAcadDimAlignedZ3.StyleName = "TSSD_100_100";
            //第一个图的顶部横向标注的线3（3个）
            //oAcadDimAlignedZ3.TextHeight = 300;
            //// oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedZ3.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedZ3.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //oAcadDimAlignedZ3.ArrowheadSize = 100;
            //oAcadDimAlignedZ3.TextStyle = "dd";
            //oAcadDimAlignedZ3.DimensionLineExtend = 100;//吓唬谁啊
            //                                            // oAcadDimAlignedZ3.TextPosition = uTemporaryPoint;
            //                                            //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedZ3.ExtensionLineExtend = 100;
            //oAcadDimAlignedZ3.ExtensionLineOffset = 100;
            //oAcadDimAlignedZ3.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //oAcadDimAlignedZ3.TextInsideAlign = true;//到底能不能不动
            //oAcadDimAlignedZ3.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //oAcadDimAlignedZ3.TextGap = 50;
            xx = oAcadDimAlignedZ3.Measurement / FDXS;
            oAcadDimAlignedZ3.TextOverride = xx.ToString("0");


            oAcadLine.Move(D1YDS, D1YDZ);
            circle1.Move(D1YDS, D1YDZ);
            DSD.Move(D1YDS, D1YDZ);
            oAcadLine2.Move(D1YDS, D1YDZ);
            oAcadLine3.Move(D1YDS, D1YDZ);
            jjx1.Move(D1YDS, D1YDZ);
            jjx2.Move(D1YDS, D1YDZ);
            jjx3.Move(D1YDS, D1YDZ);
            jjx4.Move(D1YDS, D1YDZ);
            yjjx1.Move(D1YDS, D1YDZ);
            yjjx2.Move(D1YDS, D1YDZ);
            yjjx3.Move(D1YDS, D1YDZ);
            yjjx4.Move(D1YDS, D1YDZ);
            dwx1.Move(D1YDS, D1YDZ);
            dwx2.Move(D1YDS, D1YDZ);
            oAcadDimAlignedX1.Move(D1YDS, D1YDZ);
            oAcadDimAlignedX2.Move(D1YDS, D1YDZ);
            oAcadDimAlignedX3.Move(D1YDS, D1YDZ);
            oAcadDimAlignedX4.Move(D1YDS, D1YDZ);
            oAcadDimAlignedY1.Move(D1YDS, D1YDZ);
            oAcadDimAlignedY2.Move(D1YDS, D1YDZ);
            oAcadDimAlignedY3.Move(D1YDS, D1YDZ);
            oAcadDimAlignedZ1.Move(D1YDS, D1YDZ);
            oAcadDimAlignedZ2.Move(D1YDS, D1YDZ);
            oAcadDimAlignedZ3.Move(D1YDS, D1YDZ);



            //第二张图的移动点，文字注释可千万不能错了
            //double S2TKX = S2TK[0];
            //double S2TKY = S2TK[1];
            //double S2TKZ = S2TK[2];
            double[] D2YDS = new double[3] { 0, 0, 0 };
            double[] D2YDZ = new double[3] { 29100 + S2TKX, 16800 + S2TKY-2 * FDXS * (cx + d0x + ax), 0 + S2TKZ };
            //这是画第二张侧面图。
            //底面的一条线
            double[] cdStartPoint1 = new double[3] { 0, 0+2 * FDXS * (cx + d0x + ax), 0 };
            double[] cdEndPoint1 = new double[3] { FDXS * gx, 0 + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] cdEndPoint2 = new double[3] { cdEndPoint1[0] + FDXS * tx, cdEndPoint1[1], cdEndPoint1[2] };
            double[] cdEndPoint3 = new double[3] { cdEndPoint2[0] + FDXS * gx, cdEndPoint2[1], cdEndPoint2[2] };
            AcadLine cdoAcadLine1 = AcadDoc.ModelSpace.AddLine(cdStartPoint1, cdEndPoint1);
            AcadLine cdoAcadLine2 = AcadDoc.ModelSpace.AddLine(cdEndPoint1, cdEndPoint2);
            AcadLine cdoAcadLine3 = AcadDoc.ModelSpace.AddLine(cdEndPoint2, cdEndPoint3);

            //第二张侧面图左边底部三条线

            double[] cd3lStartPoint1 = new double[3] { 0, FDXS * gdx + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] cd3lEndPoint1 = new double[3] { cd3lStartPoint1[0] + FDXS * gx - FDXS * gdx, FDXS * gx + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] cd3lEndPoint2 = new double[3] { FDXS * gx, FDXS * gx + 2 * FDXS * (cx + d0x + ax), 0 };

            AcadLine cd3loAcadLine1 = AcadDoc.ModelSpace.AddLine(cdStartPoint1, cd3lStartPoint1);
            AcadLine cd3loAcadLine2 = AcadDoc.ModelSpace.AddLine(cd3lStartPoint1, cd3lEndPoint1);
            AcadLine cd3loAcadLine3 = AcadDoc.ModelSpace.AddLine(cd3lEndPoint1, cd3lEndPoint2);

            //第二张侧面图右边底部三条线


            double[] cd3rEndPoint1 = new double[3] { FDXS * gx + FDXS * tx, FDXS * gx + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] cd3rEndPoint2 = new double[3] { cd3rEndPoint1[0] + FDXS * gdx, cd3rEndPoint1[1], 0 };
            double[] cd3rEndPoint3 = new double[3] { cd3rEndPoint1[0] + FDXS * gx, FDXS * gdx + 2 * FDXS * (cx + d0x + ax), 0 };
            AcadLine cd3roAcadLine1 = AcadDoc.ModelSpace.AddLine(cd3rEndPoint1, cd3rEndPoint2);
            AcadLine cd3roAcadLine2 = AcadDoc.ModelSpace.AddLine(cd3rEndPoint2, cd3rEndPoint3);
            AcadLine cd3roAcadLine3 = AcadDoc.ModelSpace.AddLine(cd3rEndPoint3, cdEndPoint3);

            //第二张侧面图中间五条线
            double[] zsl5EndPoint1 = new double[3] { FDXS * gx, FDXS * (cx + d0x + ax) + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] zsl5EndPoint2 = new double[3] { FDXS * gx + FDXS * tx, FDXS * (cx + d0x + ax) + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] zsl5EndPoint3 = new double[3] { FDXS * gx, FDXS * cx + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] zsl5EndPoint4 = new double[3] { FDXS * gx + FDXS * tx, FDXS * cx + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] zsl5EndPoint5 = new double[3] { FDXS * gx, FDXS * cx + FDXS * d0x + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] zsl5EndPoint6 = new double[3] { FDXS * gx + FDXS * tx, FDXS * cx + FDXS * d0x + 2 * FDXS * (cx + d0x + ax), 0 };
            //竖向两条线
            AcadLine zsl5AcadLine1 = AcadDoc.ModelSpace.AddLine(cdEndPoint1, zsl5EndPoint1);
            AcadLine zsl5AcadLine2 = AcadDoc.ModelSpace.AddLine(cdEndPoint2, zsl5EndPoint2);
            //横向三条线
            AcadLine zsl5AcadLine3 = AcadDoc.ModelSpace.AddLine(zsl5EndPoint1, zsl5EndPoint2);
            AcadLine zsl5AcadLine4 = AcadDoc.ModelSpace.AddLine(zsl5EndPoint3, zsl5EndPoint4);
            AcadLine zsl5AcadLine5 = AcadDoc.ModelSpace.AddLine(zsl5EndPoint5, zsl5EndPoint6);

            //侧面图的中心线
            double[] cdwxz = new double[3] { FDXS * gx + FDXS * tx / 2, 0 + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] cdwxy = new double[3] { FDXS * gx + FDXS * tx / 2, FDXS * cx + FDXS * d0x + FDXS * ax + 2 * FDXS * (cx + d0x + ax), 0 };

            AcadLine cdwx1 = AcadDoc.ModelSpace.AddLine(cdwxz, cdwxy);
            cdwx1.Linetype = "center";
            cdwx1.LinetypeScale = 1;



            double[] d2TemporaryPoint = new double[3] { 2000, -800 + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] r2TemporaryPoint = new double[3] { FDXS * 2 * gx + FDXS * tx + 800, 3000 + 2 * FDXS * (cx + d0x + ax), 0 };


            //这段是加第二个图的底部横向标注的线

            AcadDimAligned cAcadDimAlignedX2 = AcadDoc.ModelSpace.AddDimAligned(cdEndPoint1, cdEndPoint2, d2TemporaryPoint);


            //这段是加第二个图的右侧竖向标注的线
            //cdEndPoint3
            double[] bzcrEndPoint1 = new double[3] { cdEndPoint3[0], cdEndPoint3[1] + FDXS * cx, 0 };
            double[] bzcrEndPoint2 = new double[3] { bzcrEndPoint1[0], bzcrEndPoint1[1] + FDXS * d0x, 0 };
            double[] bzcrEndPoint3 = new double[3] { bzcrEndPoint2[0], bzcrEndPoint2[1] + FDXS * ax, 0 };

            AcadDimAligned cAcadDimAlignedY1 = AcadDoc.ModelSpace.AddDimAligned(cdEndPoint3, bzcrEndPoint1, r2TemporaryPoint);
            AcadDimAligned cAcadDimAlignedY2 = AcadDoc.ModelSpace.AddDimAligned(bzcrEndPoint1, bzcrEndPoint2, r2TemporaryPoint);
            AcadDimAligned cAcadDimAlignedY3 = AcadDoc.ModelSpace.AddDimAligned(bzcrEndPoint2, bzcrEndPoint3, r2TemporaryPoint);


            cAcadDimAlignedX2.StyleName = "TSSD_100_100";
            //第二个图的底部横向标注的线1（1个）
            //cAcadDimAlignedX2.TextHeight = 300;
            //// oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            //cAcadDimAlignedX2.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //cAcadDimAlignedX2.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //cAcadDimAlignedX2.ArrowheadSize = 100;
            //cAcadDimAlignedX2.TextStyle = "dd";
            //cAcadDimAlignedX2.DimensionLineExtend = 100;//吓唬谁啊
            //                                            //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            //cAcadDimAlignedX2.ExtensionLineExtend = 100;
            //cAcadDimAlignedX2.ExtensionLineOffset = 100;
            //cAcadDimAlignedX2.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //cAcadDimAlignedX2.TextInsideAlign = false;
            //cAcadDimAlignedX2.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //cAcadDimAlignedX2.TextGap = 50;
            xx = cAcadDimAlignedX2.Measurement / FDXS;
            cAcadDimAlignedX2.TextOverride = xx.ToString("0");

            //第二个图的右侧竖向标注的线1（3个）


            cAcadDimAlignedY1.StyleName = "TSSD_100_100";
            //cAcadDimAlignedY1.TextHeight = 300;
            //// oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            //cAcadDimAlignedY1.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //cAcadDimAlignedY1.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //cAcadDimAlignedY1.ArrowheadSize = 100;
            //cAcadDimAlignedY1.TextStyle = "dd";
            //cAcadDimAlignedY1.DimensionLineExtend = 100;//吓唬谁啊
            //                                            //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            //cAcadDimAlignedY1.ExtensionLineExtend = 100;
            //cAcadDimAlignedY1.ExtensionLineOffset = 100;
            //cAcadDimAlignedY1.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //cAcadDimAlignedY1.TextInsideAlign = false;
            //cAcadDimAlignedY1.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //cAcadDimAlignedY1.TextGap = 50;
            xx = cAcadDimAlignedY1.Measurement / FDXS;
            cAcadDimAlignedY1.TextOverride = xx.ToString("0");

            //第二个图的右侧竖向标注的线2（3个）
            cAcadDimAlignedY2.StyleName = "TSSD_100_100";
            //cAcadDimAlignedY2.TextHeight = 300;
            //// oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            //cAcadDimAlignedY2.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //cAcadDimAlignedY2.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //cAcadDimAlignedY2.ArrowheadSize = 100;
            //cAcadDimAlignedY2.TextStyle = "dd";
            //cAcadDimAlignedY2.DimensionLineExtend = 100;//吓唬谁啊
            //                                            //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            //cAcadDimAlignedY2.ExtensionLineExtend = 100;
            //cAcadDimAlignedY2.ExtensionLineOffset = 100;
            //cAcadDimAlignedY2.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //cAcadDimAlignedY2.TextInsideAlign = false;
            //cAcadDimAlignedY2.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //cAcadDimAlignedY2.TextGap = 50;
            xx = cAcadDimAlignedY2.Measurement / FDXS;
            cAcadDimAlignedY2.TextOverride = xx.ToString("0");

            //第二个图的右侧竖向标注的线3（3个）
            cAcadDimAlignedY3.StyleName = "TSSD_100_100";
            //cAcadDimAlignedY3.TextHeight = 300;
            //// oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            //cAcadDimAlignedY3.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //cAcadDimAlignedY3.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //cAcadDimAlignedY3.ArrowheadSize = 100;
            //cAcadDimAlignedY3.TextStyle = "dd";
            //cAcadDimAlignedY3.DimensionLineExtend = 100;//吓唬谁啊
            //                                            //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            //cAcadDimAlignedY3.ExtensionLineExtend = 100;
            //cAcadDimAlignedY3.ExtensionLineOffset = 100;
            //cAcadDimAlignedY3.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //cAcadDimAlignedY3.TextInsideAlign = false;
            //cAcadDimAlignedY3.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //cAcadDimAlignedY3.TextGap = 50;
            xx = cAcadDimAlignedY3.Measurement / FDXS;
            cAcadDimAlignedY3.TextOverride = xx.ToString("0");

            cdoAcadLine1.Move(D2YDS, D2YDZ);
            cdoAcadLine2.Move(D2YDS, D2YDZ);
            cdoAcadLine3.Move(D2YDS, D2YDZ);

            cd3loAcadLine1.Move(D2YDS, D2YDZ);
            cd3loAcadLine2.Move(D2YDS, D2YDZ);
            cd3loAcadLine3.Move(D2YDS, D2YDZ);

            cd3roAcadLine1.Move(D2YDS, D2YDZ);
            cd3roAcadLine2.Move(D2YDS, D2YDZ);
            cd3roAcadLine3.Move(D2YDS, D2YDZ);


            zsl5AcadLine1.Move(D2YDS, D2YDZ);
            zsl5AcadLine2.Move(D2YDS, D2YDZ);
            zsl5AcadLine3.Move(D2YDS, D2YDZ);
            zsl5AcadLine4.Move(D2YDS, D2YDZ);
            zsl5AcadLine5.Move(D2YDS, D2YDZ);

            cdwx1.Move(D2YDS, D2YDZ);

            cAcadDimAlignedX2.Move(D2YDS, D2YDZ);

            cAcadDimAlignedY1.Move(D2YDS, D2YDZ);
            cAcadDimAlignedY2.Move(D2YDS, D2YDZ);
            cAcadDimAlignedY3.Move(D2YDS, D2YDZ);





            //第二张图的移动点，文字注释可千万不能错了
            //double S2TKX = S2TK[0];
            //double S2TKY = S2TK[1];
            //double S2TKZ = S2TK[2];
            double[] D3YDS = new double[3] { 0, 0, 0 };
            double[] D3YDZ = new double[3] { 9600 + S2TKX, 8000+ S2TKY - 4 * FDXS * (cx + d0x + ax), 0 + S2TKZ };
            //这是画第二张侧面图。




            //这是画第三张底面图。
            //底面的一条线的点和标注点
            double[] dmdStartPoint1 = new double[3] { 0, 0 + 4 * FDXS * (cx + d0x + ax), 0 };
            double[] dmdEndPoint1 = new double[3] { FDXS * ex, 0 + 4 * FDXS * (cx + d0x + ax), 0 };
            double[] dmdEndPoint2 = new double[3] { dmdEndPoint1[0] + FDXS * fx, 0 + 4 * FDXS * (cx + d0x + ax), 0 };
            double[] dmdEndPoint3 = new double[3] { dmdEndPoint2[0] + FDXS * fx, 0 + 4 * FDXS * (cx + d0x + ax), 0 };
            double[] dmdEndPoint4 = new double[3] { dmdEndPoint3[0] + FDXS * ex, 0 + 4 * FDXS * (cx + d0x + ax), 0 };

            //上面的一条线的点和标注点
            double[] dmsStartPoint1 = new double[3] { 0, FDXS * tx + 4 * FDXS * (cx + d0x + ax), 0 };
            double[] dmsEndPoint1 = new double[3] { dmdEndPoint1[0], dmdEndPoint1[1] + FDXS * tx, 0 };
            double[] dmsEndPoint2 = new double[3] { dmdEndPoint2[0], dmdEndPoint2[1] + FDXS * tx, 0 };
            double[] dmsEndPoint3 = new double[3] { dmdEndPoint3[0], dmdEndPoint3[1] + FDXS * tx, 0 };
            double[] dmsEndPoint4 = new double[3] { dmdEndPoint4[0], dmdEndPoint4[1] + FDXS * tx, 0 };

            //底下一条线
            AcadLine dmdAcadLine1 = AcadDoc.ModelSpace.AddLine(dmdStartPoint1, dmdEndPoint4);
            //上面一条线
            AcadLine dmdAcadLine2 = AcadDoc.ModelSpace.AddLine(dmsStartPoint1, dmsEndPoint4);

            //竖向线1-5
            AcadLine dmdsxAcadLine1 = AcadDoc.ModelSpace.AddLine(dmdStartPoint1, dmsStartPoint1);
            AcadLine dmdsxAcadLine2 = AcadDoc.ModelSpace.AddLine(dmdEndPoint1, dmsEndPoint1);
            AcadLine dmdsxAcadLine3 = AcadDoc.ModelSpace.AddLine(dmdEndPoint2, dmsEndPoint2);
            AcadLine dmdsxAcadLine4 = AcadDoc.ModelSpace.AddLine(dmdEndPoint3, dmsEndPoint3);
            AcadLine dmdsxAcadLine5 = AcadDoc.ModelSpace.AddLine(dmdEndPoint4, dmsEndPoint4);

            //这段是加第三个图的底部横向标注的线
            double[] dmdd2TemporaryPoint = new double[3] { 2000, -800 + 4 * FDXS * (cx + d0x + ax), 0 };
            double[] dmdr2TemporaryPoint = new double[3] { FDXS * 2 * ex + FDXS * 2 * fx + 800, 3000 + 4 * FDXS * (cx + d0x + ax), 0 };

            //这段是加第三个图的底部横向标注的线
            AcadDimAligned dmdAcadDimAlignedX1 = AcadDoc.ModelSpace.AddDimAligned(dmdStartPoint1, dmdEndPoint1, dmdd2TemporaryPoint);
            AcadDimAligned dmdAcadDimAlignedX2 = AcadDoc.ModelSpace.AddDimAligned(dmdEndPoint1, dmdEndPoint2, dmdd2TemporaryPoint);
            AcadDimAligned dmdAcadDimAlignedX3 = AcadDoc.ModelSpace.AddDimAligned(dmdEndPoint2, dmdEndPoint3, dmdd2TemporaryPoint);
            AcadDimAligned dmdAcadDimAlignedX4 = AcadDoc.ModelSpace.AddDimAligned(dmdEndPoint3, dmdEndPoint4, dmdd2TemporaryPoint);


            //这段是加第三个图的右侧横向标注的线
            AcadDimAligned dmdAcadDimAlignedY1 = AcadDoc.ModelSpace.AddDimAligned(dmdEndPoint4, dmsEndPoint4, dmdr2TemporaryPoint);

            //第三个图的标注的线1（5个）

            dmdAcadDimAlignedX1.StyleName = "TSSD_100_100";
            //dmdAcadDimAlignedX1.TextHeight = 300;
            //// oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            //dmdAcadDimAlignedX1.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //dmdAcadDimAlignedX1.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //dmdAcadDimAlignedX1.ArrowheadSize = 100;
            //dmdAcadDimAlignedX1.TextStyle = "dd";
            //dmdAcadDimAlignedX1.DimensionLineExtend = 100;//吓唬谁啊
            //                                              //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            //dmdAcadDimAlignedX1.ExtensionLineExtend = 100;
            //dmdAcadDimAlignedX1.ExtensionLineOffset = 100;
            //dmdAcadDimAlignedX1.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //dmdAcadDimAlignedX1.TextInsideAlign = false;
            //dmdAcadDimAlignedX1.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //dmdAcadDimAlignedX1.TextGap = 50;
            xx = dmdAcadDimAlignedX1.Measurement / FDXS;
            dmdAcadDimAlignedX1.TextOverride = xx.ToString("0");

            //第三个图的标注的线2（5个）
            dmdAcadDimAlignedX2.StyleName = "TSSD_100_100";
            //dmdAcadDimAlignedX2.TextHeight = 300;
            //// oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            //dmdAcadDimAlignedX2.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //dmdAcadDimAlignedX2.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //dmdAcadDimAlignedX2.ArrowheadSize = 100;
            //dmdAcadDimAlignedX2.TextStyle = "dd";
            //dmdAcadDimAlignedX2.DimensionLineExtend = 100;//吓唬谁啊
            //                                              //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            //dmdAcadDimAlignedX2.ExtensionLineExtend = 100;
            //dmdAcadDimAlignedX2.ExtensionLineOffset = 100;
            //dmdAcadDimAlignedX2.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //dmdAcadDimAlignedX2.TextInsideAlign = false;
            //dmdAcadDimAlignedX2.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //dmdAcadDimAlignedX2.TextGap = 50;
            xx = dmdAcadDimAlignedX2.Measurement / FDXS;
            dmdAcadDimAlignedX2.TextOverride = xx.ToString("0");

            //第三个图的标注的线3（5个）
            dmdAcadDimAlignedX3.StyleName = "TSSD_100_100";
            //dmdAcadDimAlignedX3.TextHeight = 300;
            //// oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            //dmdAcadDimAlignedX3.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //dmdAcadDimAlignedX3.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //dmdAcadDimAlignedX3.ArrowheadSize = 100;
            //dmdAcadDimAlignedX3.TextStyle = "dd";
            //dmdAcadDimAlignedX3.DimensionLineExtend = 100;//吓唬谁啊
            //                                              //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            //dmdAcadDimAlignedX3.ExtensionLineExtend = 100;
            //dmdAcadDimAlignedX3.ExtensionLineOffset = 100;
            //dmdAcadDimAlignedX3.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //dmdAcadDimAlignedX3.TextInsideAlign = false;
            //dmdAcadDimAlignedX3.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //dmdAcadDimAlignedX3.TextGap = 50;
            xx = dmdAcadDimAlignedX3.Measurement / FDXS;
            dmdAcadDimAlignedX3.TextOverride = xx.ToString("0");

            //第三个图的标注的线4（5个）
            dmdAcadDimAlignedX4.StyleName = "TSSD_100_100";
            //dmdAcadDimAlignedX4.TextHeight = 300;
            //// oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            //dmdAcadDimAlignedX4.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //dmdAcadDimAlignedX4.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //dmdAcadDimAlignedX4.ArrowheadSize = 100;
            //dmdAcadDimAlignedX4.TextStyle = "dd";
            //dmdAcadDimAlignedX4.DimensionLineExtend = 100;//吓唬谁啊
            //                                              //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            //dmdAcadDimAlignedX4.ExtensionLineExtend = 100;
            //dmdAcadDimAlignedX4.ExtensionLineOffset = 100;
            //dmdAcadDimAlignedX4.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //dmdAcadDimAlignedX4.TextInsideAlign = false;
            //dmdAcadDimAlignedX4.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //dmdAcadDimAlignedX4.TextGap = 50;
            xx = dmdAcadDimAlignedX4.Measurement / FDXS;
            dmdAcadDimAlignedX4.TextOverride = xx.ToString("0");

            //第三个图的标注的线5（5个）
            dmdAcadDimAlignedY1.StyleName = "TSSD_100_100";
            //dmdAcadDimAlignedY1.TextHeight = 300;
            //// oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            //dmdAcadDimAlignedY1.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //dmdAcadDimAlignedY1.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            //dmdAcadDimAlignedY1.ArrowheadSize = 100;
            //dmdAcadDimAlignedY1.TextStyle = "dd";
            //dmdAcadDimAlignedY1.DimensionLineExtend = 100;//吓唬谁啊
            //                                              //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            //dmdAcadDimAlignedY1.ExtensionLineExtend = 100;
            //dmdAcadDimAlignedY1.ExtensionLineOffset = 100;
            //dmdAcadDimAlignedY1.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            //dmdAcadDimAlignedY1.TextInsideAlign = false;
            //dmdAcadDimAlignedY1.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            //dmdAcadDimAlignedY1.TextGap = 50;
            xx = dmdAcadDimAlignedY1.Measurement / FDXS;
            dmdAcadDimAlignedY1.TextOverride = xx.ToString("0");

            dmdAcadLine1.Move(D3YDS, D3YDZ);
            dmdAcadLine2.Move(D3YDS, D3YDZ);
            dmdsxAcadLine1.Move(D3YDS, D3YDZ);
            dmdsxAcadLine2.Move(D3YDS, D3YDZ);
            dmdsxAcadLine3.Move(D3YDS, D3YDZ);
            dmdsxAcadLine4.Move(D3YDS, D3YDZ);
            dmdsxAcadLine5.Move(D3YDS, D3YDZ);

            dmdAcadDimAlignedX1.Move(D3YDS, D3YDZ);
            dmdAcadDimAlignedX2.Move(D3YDS, D3YDZ);
            dmdAcadDimAlignedX3.Move(D3YDS, D3YDZ);
            dmdAcadDimAlignedX4.Move(D3YDS, D3YDZ);

            dmdAcadDimAlignedY1.Move(D3YDS, D3YDZ);

            //List<AcadPoint> h1PointsXX = new List<AcadPoint>();//第一多段线的点集
           double[] DPL11 = new double[6] { 11800 + S2TKX, 14500 + S2TKY, 0 + S2TKZ , 13800 + S2TKX, 14500 + S2TKY, 0 + S2TKZ };
           AcadPolyline D1PL= AcadDoc.ModelSpace.AddPolyline(DPL11);
           D1PL.SetWidth(0, 70, 70);
           D1PL.color = ACAD_COLOR.acGreen;


           double[] DPL22 = new double[6] { 12075 + S2TKX, 5511 + S2TKY, 0 + S2TKZ, 13525 + S2TKX, 5511 + S2TKY, 0 + S2TKZ };
           AcadPolyline D2PL = AcadDoc.ModelSpace.AddPolyline(DPL22);
           D2PL.SetWidth(0, 70, 70);
           D2PL.color = ACAD_COLOR.acGreen;

            double[] DPL33 = new double[6] { 29410 + S2TKX,  14500+ S2TKY, 0 + S2TKZ, 31390 + S2TKX, 14500 + S2TKY, 0 + S2TKZ };
            AcadPolyline D3PL = AcadDoc.ModelSpace.AddPolyline(DPL33);
            D3PL.SetWidth(0, 70, 70);
            D3PL.color = ACAD_COLOR.acGreen;

            double textHeight = 700;
            double[] ST1 = new double[3] {11818 + S2TKX, 14560 + S2TKY, 0 + S2TKZ };
            string ZLM = "正立面图";
            AcadText ST1T = AcadDoc.ModelSpace.AddText(ZLM, ST1, textHeight);
            ST1T.StyleName = "TSSD_NORM";
            ST1T.ScaleFactor = 0.7;
            double[] ST2 = new double[3] { 12065 + S2TKX, 5571 + S2TKY, 0 + S2TKZ };
            string PMT = "平面图";
            AcadText ST2T= AcadDoc.ModelSpace.AddText(PMT, ST2, textHeight);
            ST2T.StyleName = "TSSD_NORM";
            ST2T.ScaleFactor = 0.7;

            double[] ST3= new double[3] { 29419 + S2TKX, 14560 + S2TKY, 0 + S2TKZ };
            string CLMT = "侧立面图";
            AcadText ST3T = AcadDoc.ModelSpace.AddText(CLMT, ST3, textHeight);
            ST3T.StyleName = "TSSD_NORM";
            ST3T.ScaleFactor = 0.7;



            double textHeight2 = 450;
            string SM1 = "说明：";
            double[] SM1D = new double[3] { 25709 + S2TKX, 10121 + S2TKY, 0 + S2TKZ };
            AcadText SM1T = AcadDoc.ModelSpace.AddText(SM1, SM1D, textHeight2);
            SM1T.StyleName = "TSSD_NORM";
            SM1T.ScaleFactor = 0.7;

            string SM2 = "1、图中尺寸均以mm计；";
            double[] SM2D = new double[3] { 26453 + S2TKX, 9371 + S2TKY, 0 + S2TKZ };
            AcadText SM2T = AcadDoc.ModelSpace.AddText(SM2, SM2D, textHeight2);
            SM2T.StyleName = "TSSD_NORM";
            SM2T.ScaleFactor = 0.7;

            string SM3 = "2、吊耳板材质均为"+ czx+ "；";
            double[] SM3D = new double[3] { 26453 + S2TKX, 8621 + S2TKY, 0 + S2TKZ };
            AcadText SM3T = AcadDoc.ModelSpace.AddText(SM3, SM3D, textHeight2);
            SM3T.StyleName = "TSSD_NORM";
            SM3T.ScaleFactor = 0.7;


            string SM4 = "3、吊耳板底部通过熔透焊缝与构件连接，加劲肋通过角焊缝分别与";
            double[] SM4D = new double[3] { 26453 + S2TKX, 7871 + S2TKY, 0 + S2TKZ };
            AcadText SM4T = AcadDoc.ModelSpace.AddText(SM4, SM4D, textHeight2);
            SM4T.StyleName = "TSSD_NORM";
            SM4T.ScaleFactor = 0.7;

            string SM5 = "吊耳板、构件连接，焊脚尺寸不小于8mm。";
            double[] SM5D = new double[3] { 27053 + S2TKX, 7142 + S2TKY, 0 + S2TKZ };
            AcadText SM5T = AcadDoc.ModelSpace.AddText(SM5, SM5D, textHeight2);
            SM5T.StyleName = "TSSD_NORM";
            SM5T.ScaleFactor = 0.7;

            string ctx2s34D2 = DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒");
            string ctx2TT4D2 = "\\CAD\\四吊耳";
            string ctx2JW4D2 = ".dwg";
            string jpgNamePathsDW34D2 = System.Windows.Forms.Application.StartupPath + ctx2TT4D2 + ctx2s34D2 + ctx2JW4D2;
            //string jpgNamePath2s3 = "C:\\d3.dwg";//保存文件
            AcadDoc.SaveAs(jpgNamePathsDW34D2);
            //AcadDoc.Save();//到底关不关呢？

            
        }












        private void button7_Click(object sender, EventArgs e)
        {
            AcadApplication AcadApp;
            AcadDocument AcadDoc;
            //try
            //{
            AcadApp = (AcadApplication)System.Runtime.InteropServices.Marshal.GetActiveObject("AUTOCAD.Application");

            AcadDoc = AcadApp.ActiveDocument;
            //}
            //catch
            //{

            //   // string filePath = "C:\\hehddehi.dwg";
            //   // if (filePath == "")
            //   // {
            //   //     MessageBox.Show("选择CAD文件无效！", "文件无效！");
            //   //     Application.Exit();

            //   // }

            //   AcadApp = new AcadApplication();
            //   AcadDoc = AcadApp.Documents.Open("C:\\d1.dwg", null, null);
            //}


            AcadTextStyle acadTextStyle = AcadDoc.TextStyles.Add("dd");
            acadTextStyle.SetFont("Times New Roman", false, false, 1, 1);
            AcadApp.Visible = true;
            double ex = Convert.ToDouble(teLP2.Text);
            double fx = Convert.ToDouble(tfLP2.Text);
            double bx = Convert.ToDouble(tbLP2.Text);
            double d0x = Convert.ToDouble(td0LP2.Text);
            double ax = Convert.ToDouble(taLP2.Text);
            double cx = Convert.ToDouble(tcLP2.Text);
            double gx = Convert.ToDouble(ttgLP2.Text);
            double gdx = Convert.ToDouble(ttgLP2.Text) * 0.3;
            double tx = Convert.ToDouble(ttLP2.Text);


            int FDXS = 20;
            //这是画第一张图。
            double[] StartPoint1 = new double[3] { 0, 0, 0 };
            double[] EndPoint1 = new double[3] { FDXS * (ex + fx + ex + fx), 0, 0 };

            AcadLine oAcadLine = AcadDoc.ModelSpace.AddLine(StartPoint1, EndPoint1);
            double[] YXPoint1 = new double[3] { FDXS * (ex + fx), FDXS * (cx + d0x / 2), 0 };
            // double[] YXPoint1 = new double[3] { FDXS * (ex + fx), FDXS * (cx + d0x / 2), 0 };
            double[] YXPoint2 = new double[3] { YXPoint1[0], FDXS * (cx + d0x / 2) + FDXS * ax - FDXS * bx, 0 };
            double radius1 = FDXS * d0x / 2;
            double radius2 = FDXS * (d0x / 2 + bx);
            AcadDoc.ModelSpace.AddCircle(YXPoint1, radius1);
            double LR = Math.Sqrt((EndPoint1[0] - YXPoint2[0]) * (EndPoint1[0] - YXPoint2[0]) + (EndPoint1[1] - YXPoint2[1]) * (EndPoint1[1] - YXPoint2[1]) + (EndPoint1[2] - YXPoint2[2]) * (EndPoint1[2] - YXPoint2[2]));

            double cosLR = radius2 / LR;
            double cosDLR = YXPoint2[1] / LR;
            //MessageBox.Show(LR.ToString());
            //MessageBox.Show(YXPoint1[1].ToString());
            //MessageBox.Show(cosDLR.ToString());
            //MessageBox.Show(Math.Acos(cosLR).ToString());
            //MessageBox.Show(Math.Acos(cosDLR).ToString());
            //MessageBox.Show(((Math.PI) / 2).ToString());

            AcadArc DSD = AcadDoc.ModelSpace.AddArc(YXPoint2, radius2, (Math.Acos(cosLR) + Math.Acos(cosDLR) - ((Math.PI) / 2)), (Math.PI - (Math.Acos(cosLR) + Math.Acos(cosDLR) - (Math.PI / 2))));

            AcadLine oAcadLine2 = AcadDoc.ModelSpace.AddLine(EndPoint1, DSD.StartPoint);

            AcadLine oAcadLine3 = AcadDoc.ModelSpace.AddLine(StartPoint1, DSD.EndPoint);



            double[] zjj1d = new double[3] { FDXS * (ex - 6), 0, 0 };
            double[] zjj1s = new double[3] { FDXS * (ex - 6), 1000, 0 };

            double[] zjj2d = new double[3] { FDXS * (ex + 6), 0, 0 };
            double[] zjj2s = new double[3] { FDXS * (ex + 6), 1000, 0 };

            double[] zjj3z = new double[3] { FDXS * (ex - 6), 300, 0 };
            double[] zjj3y = new double[3] { FDXS * (ex + 6), 300, 0 };

            double[] zjj4z = new double[3] { FDXS * (ex - 6), 1000, 0 };
            double[] zjj4y = new double[3] { FDXS * (ex + 6), 1000, 0 };


            AcadLine jjx1 = AcadDoc.ModelSpace.AddLine(zjj1d, zjj1s);
            AcadLine jjx2 = AcadDoc.ModelSpace.AddLine(zjj2d, zjj2s);
            AcadLine jjx3 = AcadDoc.ModelSpace.AddLine(zjj3z, zjj3y);
            AcadLine jjx4 = AcadDoc.ModelSpace.AddLine(zjj4z, zjj4y);


            double[] yjj1d = new double[3] { FDXS * (ex - 6 + 2 * fx), 0, 0 };
            double[] yjj1s = new double[3] { FDXS * (ex - 6 + 2 * fx), 1000, 0 };

            double[] yjj2d = new double[3] { FDXS * (ex + 6 + 2 * fx), 0, 0 };
            double[] yjj2s = new double[3] { FDXS * (ex + 6 + 2 * fx), 1000, 0 };

            double[] yjj3z = new double[3] { FDXS * (ex - 6 + 2 * fx), 300, 0 };
            double[] yjj3y = new double[3] { FDXS * (ex + 6 + 2 * fx), 300, 0 };

            double[] yjj4z = new double[3] { FDXS * (ex - 6 + 2 * fx), 1000, 0 };
            double[] yjj4y = new double[3] { FDXS * (ex + 6 + 2 * fx), 1000, 0 };


            AcadLine yjjx1 = AcadDoc.ModelSpace.AddLine(yjj1d, yjj1s);
            AcadLine yjjx2 = AcadDoc.ModelSpace.AddLine(yjj2d, yjj2s);
            AcadLine yjjx3 = AcadDoc.ModelSpace.AddLine(yjj3z, yjj3y);
            AcadLine yjjx4 = AcadDoc.ModelSpace.AddLine(yjj4z, yjj4y);


            Boolean SFCZ = false;
            foreach (AcadLineType acadLineType in AcadDoc.Linetypes)
            {
                if (acadLineType.Name == "CENTER")
                {
                    SFCZ = true;
                }


            }
          //  MessageBox.Show(SFCZ.ToString());

            if (SFCZ)
            { }
            else
            {
                AcadDoc.Linetypes.Load("center", "acadiso.lin");
                SFCZ = false;
            }


            // yjjx1.Linetype = "center";
            //yjjx1.LinetypeScale =5;
            double[] dwxz = new double[3] { YXPoint1[0] - FDXS * d0x / 2 - FDXS * bx - bx, YXPoint1[1], 0 };
            double[] dwxy = new double[3] { YXPoint1[0] + FDXS * d0x / 2 + bx + FDXS * bx, YXPoint1[1], 0 };

            double[] dwxs = new double[3] { YXPoint1[0], YXPoint1[1] + FDXS * d0x / 2 + FDXS * ax, 0 };
            double[] dwxx = new double[3] { YXPoint1[0], 0, 0 };

            AcadLine dwx1 = AcadDoc.ModelSpace.AddLine(dwxz, dwxy);
            AcadLine dwx2 = AcadDoc.ModelSpace.AddLine(dwxs, dwxx);

            dwx1.Linetype = "center";
            dwx1.LinetypeScale = 5;

            dwx2.Linetype = "center";
            dwx2.LinetypeScale = 5;

            //double[] c1 = new double[3] {-100, -100,0};
            //double[] c2 = new double[3] { 10 * (ex + fx + ex + fx) + 100, 10 * (cx + d0x+ax) + 100, 0};



            double[] uTemporaryPoint = new double[3] { 2000, FDXS * cx + FDXS * d0x + FDXS * ax + 800, 0 };
            double[] dTemporaryPoint = new double[3] { 2000, -800, 0 };
            double[] rTemporaryPoint = new double[3] { FDXS * 2 * ex + FDXS * 2 * fx + 800, 3000, 0 };


            //这段是第一个图的底部横向标注点
            double[] bzpoint1 = new double[3] { StartPoint1[0], StartPoint1[1], StartPoint1[2] };
            double[] bzpoint2 = new double[3] { StartPoint1[0] + FDXS * ex, StartPoint1[1], StartPoint1[2] };
            double[] bzpoint3 = new double[3] { bzpoint2[0] + FDXS * fx, bzpoint2[1], bzpoint2[2] };
            double[] bzpoint4 = new double[3] { bzpoint3[0] + FDXS * fx, bzpoint3[1], bzpoint3[2] };
            double[] bzpoint5 = new double[3] { bzpoint4[0] + FDXS * ex, bzpoint4[1], bzpoint4[2] };

            //这段是加第一个图的底部横向标注的线

            AcadDimAligned oAcadDimAlignedX1 = AcadDoc.ModelSpace.AddDimAligned(bzpoint1, bzpoint2, dTemporaryPoint);
            AcadDimAligned oAcadDimAlignedX2 = AcadDoc.ModelSpace.AddDimAligned(bzpoint2, bzpoint3, dTemporaryPoint);
            AcadDimAligned oAcadDimAlignedX3 = AcadDoc.ModelSpace.AddDimAligned(bzpoint3, bzpoint4, dTemporaryPoint);
            AcadDimAligned oAcadDimAlignedX4 = AcadDoc.ModelSpace.AddDimAligned(bzpoint4, bzpoint5, dTemporaryPoint);

            //这段是第一个图的右侧竖向标注点
            //bzpoint5就是rbzpoint1
            double[] rbzpoint2 = new double[3] { bzpoint5[0], bzpoint5[1] + FDXS * cx, bzpoint5[2] };
            double[] rbzpoint3 = new double[3] { rbzpoint2[0], rbzpoint2[1] + FDXS * d0x, rbzpoint2[2] };
            double[] rbzpoint4 = new double[3] { rbzpoint3[0], rbzpoint3[1] + FDXS * ax, rbzpoint3[2] };

            //这段是第一个图加右侧竖向标注的线
            AcadDimAligned oAcadDimAlignedY1 = AcadDoc.ModelSpace.AddDimAligned(bzpoint5, rbzpoint2, rTemporaryPoint);
            AcadDimAligned oAcadDimAlignedY2 = AcadDoc.ModelSpace.AddDimAligned(rbzpoint2, rbzpoint3, rTemporaryPoint);
            AcadDimAligned oAcadDimAlignedY3 = AcadDoc.ModelSpace.AddDimAligned(rbzpoint3, rbzpoint4, rTemporaryPoint);

            //这段是第一个图的上侧横向标注点
            //bzpoint5就是rbzpoint1
            double[] ubzpoint1 = new double[3] { FDXS * ex + FDXS * fx - FDXS * d0x / 2 - FDXS * bx, bzpoint5[1] + FDXS * cx + FDXS * d0x + FDXS * ax, 0 };
            double[] ubzpoint2 = new double[3] { ubzpoint1[0] + FDXS * bx, ubzpoint1[1], ubzpoint1[2] };
            double[] ubzpoint3 = new double[3] { ubzpoint2[0] + FDXS * d0x, ubzpoint2[1], ubzpoint2[2] };
            double[] ubzpoint4 = new double[3] { ubzpoint3[0] + FDXS * bx, ubzpoint3[1], rbzpoint3[2] };

            //这段是第一个图加上侧竖向标注的线
            AcadDimAligned oAcadDimAlignedZ1 = AcadDoc.ModelSpace.AddDimAligned(ubzpoint1, ubzpoint2, uTemporaryPoint);
            AcadDimAligned oAcadDimAlignedZ2 = AcadDoc.ModelSpace.AddDimAligned(ubzpoint2, ubzpoint3, uTemporaryPoint);
            AcadDimAligned oAcadDimAlignedZ3 = AcadDoc.ModelSpace.AddDimAligned(ubzpoint3, ubzpoint4, uTemporaryPoint);

            //好吧，设置的不长，稳定点吧第一个图的底部横向标注的线1（4个）
            oAcadDimAlignedX1.TextHeight = 300;
            // oAcadDimAligned.Rotation = 0;
            //oAcadDimAligned.ArrowheadSize = 200;
            //oAcadDimAlignedX1.DimensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedX1.TextOverride = "长度<>mm";
            oAcadDimAlignedX1.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedX1.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedX1.ArrowheadSize = 100;
            oAcadDimAlignedX1.TextStyle = "dd";
            oAcadDimAlignedX1.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedX1.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedX1.ExtensionLineExtend = 100;
            oAcadDimAlignedX1.ExtensionLineOffset = 100;
            oAcadDimAlignedX1.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedX1.TextInsideAlign = false;
            oAcadDimAlignedX1.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedX1.TextGap = 50;
            double xx = oAcadDimAlignedX1.Measurement / FDXS;
            oAcadDimAlignedX1.TextOverride = xx.ToString("0");

            //第一个图的底部横向标注的线2（4个）
            oAcadDimAlignedX2.TextHeight = 300;
            //oAcadDimAlignedX2.DimensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedX2.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedX2.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedX2.ArrowheadSize = 100;
            oAcadDimAlignedX2.TextStyle = "dd";
            oAcadDimAlignedX2.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedX2.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedX2.ExtensionLineExtend = 100;
            oAcadDimAlignedX2.ExtensionLineOffset = 100;
            oAcadDimAlignedX2.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedX2.TextInsideAlign = false;
            oAcadDimAlignedX2.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedX2.TextGap = 50;
            xx = oAcadDimAlignedX2.Measurement / FDXS;
            oAcadDimAlignedX2.TextOverride = xx.ToString("0");

            //第一个图的底部横向标注的线3（4个）
            oAcadDimAlignedX3.TextHeight = 300;
            //oAcadDimAlignedX3.DimensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedX3.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedX3.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedX3.ArrowheadSize = 100;
            oAcadDimAlignedX3.TextStyle = "dd";
            oAcadDimAlignedX3.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedX3.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedX3.ExtensionLineExtend = 100;
            oAcadDimAlignedX3.ExtensionLineOffset = 100;
            oAcadDimAlignedX3.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedX3.TextInsideAlign = false;
            oAcadDimAlignedX3.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedX3.TextGap = 50;
            xx = oAcadDimAlignedX3.Measurement / FDXS;
            oAcadDimAlignedX3.TextOverride = xx.ToString("0");

            //第一个图的底部横向标注的线4（4个）
            oAcadDimAlignedX4.TextHeight = 300;
            //oAcadDimAlignedX4.DimensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedX4.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedX4.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedX4.ArrowheadSize = 100;
            oAcadDimAlignedX4.TextStyle = "dd";
            oAcadDimAlignedX4.DimensionLineExtend = 100;//吓唬谁啊
                                                        // oAcadDimAlignedX4.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedX4.ExtensionLineExtend = 100;
            oAcadDimAlignedX4.ExtensionLineOffset = 100;
            oAcadDimAlignedX4.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedX4.TextInsideAlign = false;
            oAcadDimAlignedX4.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedX4.TextGap = 50;
            xx = oAcadDimAlignedX4.Measurement / FDXS;
            oAcadDimAlignedX4.TextOverride = xx.ToString("0");

            //第一个图的右侧竖向标注的线1（3个）
            oAcadDimAlignedY1.TextHeight = 300;
            // oAcadDimAlignedY1.DimensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedY1.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedY1.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedY1.ArrowheadSize = 100;
            oAcadDimAlignedY1.TextStyle = "dd";
            oAcadDimAlignedY1.DimensionLineExtend = 100;//吓唬谁啊
                                                        // oAcadDimAlignedY1.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedY1.ExtensionLineExtend = 100;
            oAcadDimAlignedY1.ExtensionLineOffset = 100;
            oAcadDimAlignedY1.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedY1.TextInsideAlign = false;
            oAcadDimAlignedY1.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedY1.TextGap = 50;
            xx = oAcadDimAlignedY1.Measurement / FDXS;
            oAcadDimAlignedY1.TextOverride = xx.ToString("0");

            //第一个图的右侧竖向标注的线2（3个）
            oAcadDimAlignedY2.TextHeight = 300;
            //oAcadDimAlignedY2.DimensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedY2.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedY2.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedY2.ArrowheadSize = 100;
            oAcadDimAlignedY2.TextStyle = "dd";
            oAcadDimAlignedY2.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedY2.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedY2.ExtensionLineExtend = 100;
            oAcadDimAlignedY2.ExtensionLineOffset = 100;
            oAcadDimAlignedY2.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedY2.TextInsideAlign = false;
            oAcadDimAlignedY2.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedY2.TextGap = 50;
            xx = oAcadDimAlignedY2.Measurement / FDXS;
            oAcadDimAlignedY2.TextOverride = xx.ToString("0");

            //第一个图的右侧竖向标注的线3（3个）
            oAcadDimAlignedY3.TextHeight = 300;
            // oAcadDimAlignedY3.DimensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedY3.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedY3.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedY3.ArrowheadSize = 100;
            oAcadDimAlignedY3.TextStyle = "dd";
            oAcadDimAlignedY3.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedY3.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedY3.ExtensionLineExtend = 100;
            oAcadDimAlignedY3.ExtensionLineOffset = 100;
            oAcadDimAlignedY3.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedY3.TextInsideAlign = false;
            oAcadDimAlignedY3.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedY3.TextGap = 50;
            xx = oAcadDimAlignedY3.Measurement / FDXS;
            oAcadDimAlignedY3.TextOverride = xx.ToString("0");


            //第一个图的顶部横向标注的线1（3个）
            oAcadDimAlignedZ1.TextHeight = 300;
            // oAcadDimAlignedZ1.DimensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedZ1.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedZ1.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedZ1.ArrowheadSize = 100;
            oAcadDimAlignedZ1.TextStyle = "dd";
            oAcadDimAlignedZ1.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedZ1.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedZ1.ExtensionLineExtend = 100;
            oAcadDimAlignedZ1.ExtensionLineOffset = 100;
            oAcadDimAlignedZ1.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedZ1.TextInsideAlign = true;//到底能不能不动
                                                     //oAcadDimAlignedZ1.TextPosition =uTemporaryPoint;
            oAcadDimAlignedZ1.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedZ1.TextGap = 50;
            xx = oAcadDimAlignedZ1.Measurement / FDXS;
            oAcadDimAlignedZ1.TextOverride = xx.ToString("0");

            //第一个图的顶部横向标注的线2（3个）
            oAcadDimAlignedZ2.TextHeight = 300;
            // oAcadDimAlignedZ2.DimensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedZ2.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedZ2.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedZ2.ArrowheadSize = 100;
            oAcadDimAlignedZ2.TextStyle = "dd";
            oAcadDimAlignedZ2.DimensionLineExtend = 100;//吓唬谁啊
                                                        // oAcadDimAlignedZ2.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedZ2.ExtensionLineExtend = 100;
            oAcadDimAlignedZ2.ExtensionLineOffset = 100;
            oAcadDimAlignedZ2.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedZ2.TextInsideAlign = true;//到底能不能不动
                                                     //oAcadDimAlignedZ2.TextPosition = uTemporaryPoint;
            oAcadDimAlignedZ2.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedZ2.TextGap = 50;
            xx = oAcadDimAlignedZ2.Measurement / FDXS;
            oAcadDimAlignedZ2.TextOverride = xx.ToString("0");


            //第一个图的顶部横向标注的线3（3个）
            oAcadDimAlignedZ3.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedZ3.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedZ3.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedZ3.ArrowheadSize = 100;
            oAcadDimAlignedZ3.TextStyle = "dd";
            oAcadDimAlignedZ3.DimensionLineExtend = 100;//吓唬谁啊
                                                        // oAcadDimAlignedZ3.TextPosition = uTemporaryPoint;
                                                        //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedZ3.ExtensionLineExtend = 100;
            oAcadDimAlignedZ3.ExtensionLineOffset = 100;
            oAcadDimAlignedZ3.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedZ3.TextInsideAlign = true;//到底能不能不动
            oAcadDimAlignedZ3.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedZ3.TextGap = 50;
            xx = oAcadDimAlignedZ3.Measurement / FDXS;
            oAcadDimAlignedZ3.TextOverride = xx.ToString("0");







          //  string jpgNamePathDW = System.Windows.Forms.Application.StartupPath + "\\CAD\\双吊耳正面的.dwg";
                 //   AcadDoc.SaveAs(jpgNamePathDW);

            //暂时不用




 

            //这是画第二张侧面图。
            //底面的一条线
            double[] cdStartPoint1 = new double[3] { 0, 0+2*FDXS*(cx+d0x+ax), 0 };
            double[] cdEndPoint1 = new double[3] { FDXS * gx, 0 + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] cdEndPoint2 = new double[3] { cdEndPoint1[0] + FDXS * tx, cdEndPoint1[1] , cdEndPoint1[2] };
            double[] cdEndPoint3 = new double[3] { cdEndPoint2[0] + FDXS * gx, cdEndPoint2[1] , cdEndPoint2[2] };
            AcadLine cdoAcadLine1 = AcadDoc.ModelSpace.AddLine(cdStartPoint1, cdEndPoint1);
            AcadLine cdoAcadLine2 = AcadDoc.ModelSpace.AddLine(cdEndPoint1, cdEndPoint2);
            AcadLine cdoAcadLine3 = AcadDoc.ModelSpace.AddLine(cdEndPoint2, cdEndPoint3);

            //第二张侧面图左边底部三条线

            double[] cd3lStartPoint1 = new double[3] { 0, FDXS * gdx + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] cd3lEndPoint1 = new double[3] { cd3lStartPoint1[0] + FDXS * gx - FDXS * gdx , FDXS * gx + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] cd3lEndPoint2 = new double[3] { FDXS * gx, FDXS * gx + 2 * FDXS * (cx + d0x + ax), 0 };

            AcadLine cd3loAcadLine1 = AcadDoc.ModelSpace.AddLine(cdStartPoint1, cd3lStartPoint1);
            AcadLine cd3loAcadLine2 = AcadDoc.ModelSpace.AddLine(cd3lStartPoint1, cd3lEndPoint1);
            AcadLine cd3loAcadLine3 = AcadDoc.ModelSpace.AddLine(cd3lEndPoint1, cd3lEndPoint2);

            //第二张侧面图右边底部三条线


            double[] cd3rEndPoint1 = new double[3] { FDXS * gx + FDXS * tx, FDXS * gx + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] cd3rEndPoint2 = new double[3] { cd3rEndPoint1[0] + FDXS * gdx, cd3rEndPoint1[1], 0 };
            double[] cd3rEndPoint3 = new double[3] { cd3rEndPoint1[0] + FDXS * gx, FDXS * gdx + 2 * FDXS * (cx + d0x + ax), 0 };
            AcadLine cd3roAcadLine1 = AcadDoc.ModelSpace.AddLine(cd3rEndPoint1, cd3rEndPoint2);
            AcadLine cd3roAcadLine2 = AcadDoc.ModelSpace.AddLine(cd3rEndPoint2, cd3rEndPoint3);
            AcadLine cd3roAcadLine3 = AcadDoc.ModelSpace.AddLine(cd3rEndPoint3, cdEndPoint3);

            //第二张侧面图中间五条线
            double[] zsl5EndPoint1 = new double[3] { FDXS * gx, FDXS * (cx + d0x + ax) + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] zsl5EndPoint2 = new double[3] { FDXS * gx + FDXS * tx, FDXS * (cx + d0x + ax) + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] zsl5EndPoint3 = new double[3] { FDXS * gx, FDXS * cx + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] zsl5EndPoint4 = new double[3] { FDXS * gx + FDXS * tx, FDXS * cx + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] zsl5EndPoint5 = new double[3] { FDXS * gx, FDXS * cx + FDXS * d0x + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] zsl5EndPoint6 = new double[3] { FDXS * gx + FDXS * tx, FDXS * cx + FDXS * d0x + 2 * FDXS * (cx + d0x + ax), 0 };
            //竖向两条线
            AcadLine zsl5AcadLine1 = AcadDoc.ModelSpace.AddLine(cdEndPoint1, zsl5EndPoint1);
            AcadLine zsl5AcadLine2 = AcadDoc.ModelSpace.AddLine(cdEndPoint2, zsl5EndPoint2);
            //横向三条线
            AcadLine zsl5AcadLine3 = AcadDoc.ModelSpace.AddLine(zsl5EndPoint1, zsl5EndPoint2);
            AcadLine zsl5AcadLine4 = AcadDoc.ModelSpace.AddLine(zsl5EndPoint3, zsl5EndPoint4);
            AcadLine zsl5AcadLine5 = AcadDoc.ModelSpace.AddLine(zsl5EndPoint5, zsl5EndPoint6);

            //侧面图的中心线
            double[] cdwxz = new double[3] { FDXS * gx + FDXS * tx / 2, 0 + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] cdwxy = new double[3] { FDXS * gx + FDXS * tx / 2, FDXS * cx + FDXS * d0x + FDXS * ax + 2 * FDXS * (cx + d0x + ax), 0 };

            AcadLine cdwx1 = AcadDoc.ModelSpace.AddLine(cdwxz, cdwxy);
            cdwx1.Linetype = "center";
            cdwx1.LinetypeScale = 5;



            double[] d2TemporaryPoint = new double[3] { 2000, -800 + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] r2TemporaryPoint = new double[3] { FDXS * 2 * gx + FDXS * tx + 800, 3000 + 2 * FDXS * (cx + d0x + ax), 0 };


            //这段是加第二个图的底部横向标注的线

            AcadDimAligned cAcadDimAlignedX2 = AcadDoc.ModelSpace.AddDimAligned(cdEndPoint1, cdEndPoint2, d2TemporaryPoint);


            //这段是加第二个图的右侧竖向标注的线
            //cdEndPoint3
            double[] bzcrEndPoint1 = new double[3] { cdEndPoint3[0], cdEndPoint3[1] + FDXS * cx , 0 };
            double[] bzcrEndPoint2 = new double[3] { bzcrEndPoint1[0], bzcrEndPoint1[1] + FDXS * d0x, 0 };
            double[] bzcrEndPoint3 = new double[3] { bzcrEndPoint2[0], bzcrEndPoint2[1] + FDXS * ax, 0 };

            AcadDimAligned cAcadDimAlignedY1 = AcadDoc.ModelSpace.AddDimAligned(cdEndPoint3, bzcrEndPoint1, r2TemporaryPoint);
            AcadDimAligned cAcadDimAlignedY2 = AcadDoc.ModelSpace.AddDimAligned(bzcrEndPoint1, bzcrEndPoint2, r2TemporaryPoint);
            AcadDimAligned cAcadDimAlignedY3 = AcadDoc.ModelSpace.AddDimAligned(bzcrEndPoint2, bzcrEndPoint3, r2TemporaryPoint);

            //第二个图的底部横向标注的线1（1个）
            cAcadDimAlignedX2.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            cAcadDimAlignedX2.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            cAcadDimAlignedX2.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            cAcadDimAlignedX2.ArrowheadSize = 100;
            cAcadDimAlignedX2.TextStyle = "dd";
            cAcadDimAlignedX2.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            cAcadDimAlignedX2.ExtensionLineExtend = 100;
            cAcadDimAlignedX2.ExtensionLineOffset = 100;
            cAcadDimAlignedX2.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            cAcadDimAlignedX2.TextInsideAlign = false;
            cAcadDimAlignedX2.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            cAcadDimAlignedX2.TextGap = 50;
            xx = cAcadDimAlignedX2.Measurement / FDXS;
            cAcadDimAlignedX2.TextOverride = xx.ToString("0");

            //第二个图的右侧竖向标注的线1（3个）

            cAcadDimAlignedY1.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            cAcadDimAlignedY1.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            cAcadDimAlignedY1.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            cAcadDimAlignedY1.ArrowheadSize = 100;
            cAcadDimAlignedY1.TextStyle = "dd";
            cAcadDimAlignedY1.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            cAcadDimAlignedY1.ExtensionLineExtend = 100;
            cAcadDimAlignedY1.ExtensionLineOffset = 100;
            cAcadDimAlignedY1.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            cAcadDimAlignedY1.TextInsideAlign = false;
            cAcadDimAlignedY1.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            cAcadDimAlignedY1.TextGap = 50;
            xx = cAcadDimAlignedY1.Measurement / FDXS;
            cAcadDimAlignedY1.TextOverride = xx.ToString("0");

            //第二个图的右侧竖向标注的线2（3个）

            cAcadDimAlignedY2.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            cAcadDimAlignedY2.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            cAcadDimAlignedY2.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            cAcadDimAlignedY2.ArrowheadSize = 100;
            cAcadDimAlignedY2.TextStyle = "dd";
            cAcadDimAlignedY2.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            cAcadDimAlignedY2.ExtensionLineExtend = 100;
            cAcadDimAlignedY2.ExtensionLineOffset = 100;
            cAcadDimAlignedY2.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            cAcadDimAlignedY2.TextInsideAlign = false;
            cAcadDimAlignedY2.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            cAcadDimAlignedY2.TextGap = 50;
            xx = cAcadDimAlignedY2.Measurement / FDXS;
            cAcadDimAlignedY2.TextOverride = xx.ToString("0");

            //第二个图的右侧竖向标注的线3（3个）

            cAcadDimAlignedY3.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            cAcadDimAlignedY3.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            cAcadDimAlignedY3.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            cAcadDimAlignedY3.ArrowheadSize = 100;
            cAcadDimAlignedY3.TextStyle = "dd";
            cAcadDimAlignedY3.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            cAcadDimAlignedY3.ExtensionLineExtend = 100;
            cAcadDimAlignedY3.ExtensionLineOffset = 100;
            cAcadDimAlignedY3.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            cAcadDimAlignedY3.TextInsideAlign = false;
            cAcadDimAlignedY3.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            cAcadDimAlignedY3.TextGap = 50;
            xx = cAcadDimAlignedY3.Measurement / FDXS;
            cAcadDimAlignedY3.TextOverride = xx.ToString("0");

            //oAcadDimAligned.Rotation = 0;
            // string jpgNamePaths2 = "C:\\d2.jpg";//打印文件




            //这是画第三张底面图。
            //底面的一条线的点和标注点
            double[] dmdStartPoint1 = new double[3] { 0, 0 + 4 * FDXS * (cx + d0x + ax), 0 };
            double[] dmdEndPoint1 = new double[3] { FDXS * ex, 0 + 4 * FDXS * (cx + d0x + ax), 0 };
            double[] dmdEndPoint2 = new double[3] { dmdEndPoint1[0] + FDXS * fx, 0 + 4 * FDXS * (cx + d0x + ax), 0 };
            double[] dmdEndPoint3 = new double[3] { dmdEndPoint2[0] + FDXS * fx, 0 + 4 * FDXS * (cx + d0x + ax), 0 };
            double[] dmdEndPoint4 = new double[3] { dmdEndPoint3[0] + FDXS * ex, 0 + 4 * FDXS * (cx + d0x + ax), 0 };

            //上面的一条线的点和标注点
            double[] dmsStartPoint1 = new double[3] { 0, FDXS * tx + 4 * FDXS * (cx + d0x + ax), 0 };
            double[] dmsEndPoint1 = new double[3] { dmdEndPoint1[0], dmdEndPoint1[1] + FDXS * tx, 0 };
            double[] dmsEndPoint2 = new double[3] { dmdEndPoint2[0], dmdEndPoint2[1] + FDXS * tx , 0 };
            double[] dmsEndPoint3 = new double[3] { dmdEndPoint3[0], dmdEndPoint3[1] + FDXS * tx , 0 };
            double[] dmsEndPoint4 = new double[3] { dmdEndPoint4[0], dmdEndPoint4[1] + FDXS * tx , 0 };

            //底下一条线
            AcadLine dmdAcadLine1 = AcadDoc.ModelSpace.AddLine(dmdStartPoint1, dmdEndPoint4);
            //上面一条线
            AcadLine dmdAcadLine2 = AcadDoc.ModelSpace.AddLine(dmsStartPoint1, dmsEndPoint4);

            //竖向线1-5
            AcadLine dmdsxAcadLine1 = AcadDoc.ModelSpace.AddLine(dmdStartPoint1, dmsStartPoint1);
            AcadLine dmdsxAcadLine2 = AcadDoc.ModelSpace.AddLine(dmdEndPoint1, dmsEndPoint1);
            AcadLine dmdsxAcadLine3 = AcadDoc.ModelSpace.AddLine(dmdEndPoint2, dmsEndPoint2);
            AcadLine dmdsxAcadLine4 = AcadDoc.ModelSpace.AddLine(dmdEndPoint3, dmsEndPoint3);
            AcadLine dmdsxAcadLine5 = AcadDoc.ModelSpace.AddLine(dmdEndPoint4, dmsEndPoint4);

            //这段是加第三个图的底部横向标注的线
            double[] dmdd2TemporaryPoint = new double[3] { 2000, -800 + 4 * FDXS * (cx + d0x + ax), 0 };
            double[] dmdr2TemporaryPoint = new double[3] { FDXS * 2 * ex + FDXS * 2 * fx + 800, 3000 + 4 * FDXS * (cx + d0x + ax), 0 };

            //这段是加第三个图的底部横向标注的线
            AcadDimAligned dmdAcadDimAlignedX1 = AcadDoc.ModelSpace.AddDimAligned(dmdStartPoint1, dmdEndPoint1, dmdd2TemporaryPoint);
            AcadDimAligned dmdAcadDimAlignedX2 = AcadDoc.ModelSpace.AddDimAligned(dmdEndPoint1, dmdEndPoint2, dmdd2TemporaryPoint);
            AcadDimAligned dmdAcadDimAlignedX3 = AcadDoc.ModelSpace.AddDimAligned(dmdEndPoint2, dmdEndPoint3, dmdd2TemporaryPoint);
            AcadDimAligned dmdAcadDimAlignedX4 = AcadDoc.ModelSpace.AddDimAligned(dmdEndPoint3, dmdEndPoint4, dmdd2TemporaryPoint);


            //这段是加第三个图的右侧横向标注的线
            AcadDimAligned dmdAcadDimAlignedY1 = AcadDoc.ModelSpace.AddDimAligned(dmdEndPoint4, dmsEndPoint4, dmdr2TemporaryPoint);

            //第三个图的标注的线1（5个）
            dmdAcadDimAlignedX1.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedX1.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedX1.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedX1.ArrowheadSize = 100;
            dmdAcadDimAlignedX1.TextStyle = "dd";
            dmdAcadDimAlignedX1.DimensionLineExtend = 100;//吓唬谁啊
                                                          //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedX1.ExtensionLineExtend = 100;
            dmdAcadDimAlignedX1.ExtensionLineOffset = 100;
            dmdAcadDimAlignedX1.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            dmdAcadDimAlignedX1.TextInsideAlign = false;
            dmdAcadDimAlignedX1.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            dmdAcadDimAlignedX1.TextGap = 50;
            xx = dmdAcadDimAlignedX1.Measurement / FDXS;
            dmdAcadDimAlignedX1.TextOverride = xx.ToString("0");

            //第三个图的标注的线2（5个）
            dmdAcadDimAlignedX2.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedX2.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedX2.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedX2.ArrowheadSize = 100;
            dmdAcadDimAlignedX2.TextStyle = "dd";
            dmdAcadDimAlignedX2.DimensionLineExtend = 100;//吓唬谁啊
                                                          //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedX2.ExtensionLineExtend = 100;
            dmdAcadDimAlignedX2.ExtensionLineOffset = 100;
            dmdAcadDimAlignedX2.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            dmdAcadDimAlignedX2.TextInsideAlign = false;
            dmdAcadDimAlignedX2.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            dmdAcadDimAlignedX2.TextGap = 50;
            xx = dmdAcadDimAlignedX2.Measurement / FDXS;
            dmdAcadDimAlignedX2.TextOverride = xx.ToString("0");

            //第三个图的标注的线3（5个）
            dmdAcadDimAlignedX3.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedX3.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedX3.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedX3.ArrowheadSize = 100;
            dmdAcadDimAlignedX3.TextStyle = "dd";
            dmdAcadDimAlignedX3.DimensionLineExtend = 100;//吓唬谁啊
                                                          //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedX3.ExtensionLineExtend = 100;
            dmdAcadDimAlignedX3.ExtensionLineOffset = 100;
            dmdAcadDimAlignedX3.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            dmdAcadDimAlignedX3.TextInsideAlign = false;
            dmdAcadDimAlignedX3.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            dmdAcadDimAlignedX3.TextGap = 50;
            xx = dmdAcadDimAlignedX3.Measurement / FDXS;
            dmdAcadDimAlignedX3.TextOverride = xx.ToString("0");

            //第三个图的标注的线4（5个）
            dmdAcadDimAlignedX4.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedX4.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedX4.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedX4.ArrowheadSize = 100;
            dmdAcadDimAlignedX4.TextStyle = "dd";
            dmdAcadDimAlignedX4.DimensionLineExtend = 100;//吓唬谁啊
                                                          //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedX4.ExtensionLineExtend = 100;
            dmdAcadDimAlignedX4.ExtensionLineOffset = 100;
            dmdAcadDimAlignedX4.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            dmdAcadDimAlignedX4.TextInsideAlign = false;
            dmdAcadDimAlignedX4.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            dmdAcadDimAlignedX4.TextGap = 50;
            xx = dmdAcadDimAlignedX4.Measurement / FDXS;
            dmdAcadDimAlignedX4.TextOverride = xx.ToString("0");

            //第三个图的标注的线5（5个）
            dmdAcadDimAlignedY1.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedY1.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedY1.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedY1.ArrowheadSize = 100;
            dmdAcadDimAlignedY1.TextStyle = "dd";
            dmdAcadDimAlignedY1.DimensionLineExtend = 100;//吓唬谁啊
                                                          //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedY1.ExtensionLineExtend = 100;
            dmdAcadDimAlignedY1.ExtensionLineOffset = 100;
            dmdAcadDimAlignedY1.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            dmdAcadDimAlignedY1.TextInsideAlign = false;
            dmdAcadDimAlignedY1.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            dmdAcadDimAlignedY1.TextGap = 50;
            xx = dmdAcadDimAlignedY1.Measurement / FDXS;
            dmdAcadDimAlignedY1.TextOverride = xx.ToString("0");


            string ctx2s3 = DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒");
            string ctx2TT = "\\CAD\\双吊耳";
            string ctx2JW = ".dwg";
            string jpgNamePathsDW3 = System.Windows.Forms.Application.StartupPath + ctx2TT + ctx2s3 + ctx2JW;
            //string jpgNamePath2s3 = "C:\\d3.dwg";//保存文件
            AcadDoc.SaveAs(jpgNamePathsDW3);
            AcadDoc.Save();//到底关不关呢？
                         
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AcadApplication AcadApp;
            AcadDocument AcadDoc;
            //try
            //{
            AcadApp = (AcadApplication)System.Runtime.InteropServices.Marshal.GetActiveObject("AUTOCAD.Application");

            AcadDoc = AcadApp.ActiveDocument;
            //}
            //catch
            //{

            //   // string filePath = "C:\\hehddehi.dwg";
            //   // if (filePath == "")
            //   // {
            //   //     MessageBox.Show("选择CAD文件无效！", "文件无效！");
            //   //     Application.Exit();

            //   // }

            //   AcadApp = new AcadApplication();
            //   AcadDoc = AcadApp.Documents.Open("C:\\d1.dwg", null, null);
            //}


            AcadTextStyle acadTextStyle = AcadDoc.TextStyles.Add("dd");
            acadTextStyle.SetFont("Times New Roman", false, false, 1, 1);
            AcadApp.Visible = true;
            double ex = Convert.ToDouble(teMQ1.Text);
            double fx = Convert.ToDouble(tfMQ1.Text);
            double bx = Convert.ToDouble(tbMQ1.Text);
            double d0x = Convert.ToDouble(td0MQ1.Text);
            double ax = Convert.ToDouble(taMQ1.Text);
            double cx = Convert.ToDouble(tcMQ1.Text);
            double gx = Convert.ToDouble(ttgMQ1.Text);
            double gdx = Convert.ToDouble(ttgMQ1.Text) * 0.3;
            double tx = Convert.ToDouble(ttMQ1.Text);


            int FDXS = 20;
            //这是画第一张图。
            double[] StartPoint1 = new double[3] { 0, 0, 0 };
            double[] EndPoint1 = new double[3] { FDXS * (ex + fx + ex + fx), 0, 0 };

            AcadLine oAcadLine = AcadDoc.ModelSpace.AddLine(StartPoint1, EndPoint1);
            double[] YXPoint1 = new double[3] { FDXS * (ex + fx), FDXS * (cx + d0x / 2), 0 };
            // double[] YXPoint1 = new double[3] { FDXS * (ex + fx), FDXS * (cx + d0x / 2), 0 };
            double[] YXPoint2 = new double[3] { YXPoint1[0], FDXS * (cx + d0x / 2) + FDXS * ax - FDXS * bx, 0 };
            double radius1 = FDXS * d0x / 2;
            double radius2 = FDXS * (d0x / 2 + bx);
            AcadDoc.ModelSpace.AddCircle(YXPoint1, radius1);
            double LR = Math.Sqrt((EndPoint1[0] - YXPoint2[0]) * (EndPoint1[0] - YXPoint2[0]) + (EndPoint1[1] - YXPoint2[1]) * (EndPoint1[1] - YXPoint2[1]) + (EndPoint1[2] - YXPoint2[2]) * (EndPoint1[2] - YXPoint2[2]));

            double cosLR = radius2 / LR;
            double cosDLR = YXPoint2[1] / LR;
            //MessageBox.Show(LR.ToString());
            //MessageBox.Show(YXPoint1[1].ToString());
            //MessageBox.Show(cosDLR.ToString());
            //MessageBox.Show(Math.Acos(cosLR).ToString());
            //MessageBox.Show(Math.Acos(cosDLR).ToString());
            //MessageBox.Show(((Math.PI) / 2).ToString());

            AcadArc DSD = AcadDoc.ModelSpace.AddArc(YXPoint2, radius2, (Math.Acos(cosLR) + Math.Acos(cosDLR) - ((Math.PI) / 2)), (Math.PI - (Math.Acos(cosLR) + Math.Acos(cosDLR) - (Math.PI / 2))));

            AcadLine oAcadLine2 = AcadDoc.ModelSpace.AddLine(EndPoint1, DSD.StartPoint);

            AcadLine oAcadLine3 = AcadDoc.ModelSpace.AddLine(StartPoint1, DSD.EndPoint);



            double[] zjj1d = new double[3] { FDXS * (ex - 6), 0, 0 };
            double[] zjj1s = new double[3] { FDXS * (ex - 6), 1000, 0 };

            double[] zjj2d = new double[3] { FDXS * (ex + 6), 0, 0 };
            double[] zjj2s = new double[3] { FDXS * (ex + 6), 1000, 0 };

            double[] zjj3z = new double[3] { FDXS * (ex - 6), 300, 0 };
            double[] zjj3y = new double[3] { FDXS * (ex + 6), 300, 0 };

            double[] zjj4z = new double[3] { FDXS * (ex - 6), 1000, 0 };
            double[] zjj4y = new double[3] { FDXS * (ex + 6), 1000, 0 };


            AcadLine jjx1 = AcadDoc.ModelSpace.AddLine(zjj1d, zjj1s);
            AcadLine jjx2 = AcadDoc.ModelSpace.AddLine(zjj2d, zjj2s);
            AcadLine jjx3 = AcadDoc.ModelSpace.AddLine(zjj3z, zjj3y);
            AcadLine jjx4 = AcadDoc.ModelSpace.AddLine(zjj4z, zjj4y);


            double[] yjj1d = new double[3] { FDXS * (ex - 6 + 2 * fx), 0, 0 };
            double[] yjj1s = new double[3] { FDXS * (ex - 6 + 2 * fx), 1000, 0 };

            double[] yjj2d = new double[3] { FDXS * (ex + 6 + 2 * fx), 0, 0 };
            double[] yjj2s = new double[3] { FDXS * (ex + 6 + 2 * fx), 1000, 0 };

            double[] yjj3z = new double[3] { FDXS * (ex - 6 + 2 * fx), 300, 0 };
            double[] yjj3y = new double[3] { FDXS * (ex + 6 + 2 * fx), 300, 0 };

            double[] yjj4z = new double[3] { FDXS * (ex - 6 + 2 * fx), 1000, 0 };
            double[] yjj4y = new double[3] { FDXS * (ex + 6 + 2 * fx), 1000, 0 };


            AcadLine yjjx1 = AcadDoc.ModelSpace.AddLine(yjj1d, yjj1s);
            AcadLine yjjx2 = AcadDoc.ModelSpace.AddLine(yjj2d, yjj2s);
            AcadLine yjjx3 = AcadDoc.ModelSpace.AddLine(yjj3z, yjj3y);
            AcadLine yjjx4 = AcadDoc.ModelSpace.AddLine(yjj4z, yjj4y);


            Boolean SFCZ = false;
            foreach (AcadLineType acadLineType in AcadDoc.Linetypes)
            {
                if (acadLineType.Name == "CENTER")
                {
                    SFCZ = true;
                }


            }
            //  MessageBox.Show(SFCZ.ToString());

            if (SFCZ)
            { }
            else
            {
                AcadDoc.Linetypes.Load("center", "acadiso.lin");
                SFCZ = false;
            }


            // yjjx1.Linetype = "center";
            //yjjx1.LinetypeScale =5;
            double[] dwxz = new double[3] { YXPoint1[0] - FDXS * d0x / 2 - FDXS * bx - bx, YXPoint1[1], 0 };
            double[] dwxy = new double[3] { YXPoint1[0] + FDXS * d0x / 2 + bx + FDXS * bx, YXPoint1[1], 0 };

            double[] dwxs = new double[3] { YXPoint1[0], YXPoint1[1] + FDXS * d0x / 2 + FDXS * ax, 0 };
            double[] dwxx = new double[3] { YXPoint1[0], 0, 0 };

            AcadLine dwx1 = AcadDoc.ModelSpace.AddLine(dwxz, dwxy);
            AcadLine dwx2 = AcadDoc.ModelSpace.AddLine(dwxs, dwxx);

            dwx1.Linetype = "center";
            dwx1.LinetypeScale = 5;

            dwx2.Linetype = "center";
            dwx2.LinetypeScale = 5;

            //double[] c1 = new double[3] {-100, -100,0};
            //double[] c2 = new double[3] { 10 * (ex + fx + ex + fx) + 100, 10 * (cx + d0x+ax) + 100, 0};



            double[] uTemporaryPoint = new double[3] { 2000, FDXS * cx + FDXS * d0x + FDXS * ax + 800, 0 };
            double[] dTemporaryPoint = new double[3] { 2000, -800, 0 };
            double[] rTemporaryPoint = new double[3] { FDXS * 2 * ex + FDXS * 2 * fx + 800, 3000, 0 };


            //这段是第一个图的底部横向标注点
            double[] bzpoint1 = new double[3] { StartPoint1[0], StartPoint1[1], StartPoint1[2] };
            double[] bzpoint2 = new double[3] { StartPoint1[0] + FDXS * ex, StartPoint1[1], StartPoint1[2] };
            double[] bzpoint3 = new double[3] { bzpoint2[0] + FDXS * fx, bzpoint2[1], bzpoint2[2] };
            double[] bzpoint4 = new double[3] { bzpoint3[0] + FDXS * fx, bzpoint3[1], bzpoint3[2] };
            double[] bzpoint5 = new double[3] { bzpoint4[0] + FDXS * ex, bzpoint4[1], bzpoint4[2] };

            //这段是加第一个图的底部横向标注的线

            AcadDimAligned oAcadDimAlignedX1 = AcadDoc.ModelSpace.AddDimAligned(bzpoint1, bzpoint2, dTemporaryPoint);
            AcadDimAligned oAcadDimAlignedX2 = AcadDoc.ModelSpace.AddDimAligned(bzpoint2, bzpoint3, dTemporaryPoint);
            AcadDimAligned oAcadDimAlignedX3 = AcadDoc.ModelSpace.AddDimAligned(bzpoint3, bzpoint4, dTemporaryPoint);
            AcadDimAligned oAcadDimAlignedX4 = AcadDoc.ModelSpace.AddDimAligned(bzpoint4, bzpoint5, dTemporaryPoint);

            //这段是第一个图的右侧竖向标注点
            //bzpoint5就是rbzpoint1
            double[] rbzpoint2 = new double[3] { bzpoint5[0], bzpoint5[1] + FDXS * cx, bzpoint5[2] };
            double[] rbzpoint3 = new double[3] { rbzpoint2[0], rbzpoint2[1] + FDXS * d0x, rbzpoint2[2] };
            double[] rbzpoint4 = new double[3] { rbzpoint3[0], rbzpoint3[1] + FDXS * ax, rbzpoint3[2] };

            //这段是第一个图加右侧竖向标注的线
            AcadDimAligned oAcadDimAlignedY1 = AcadDoc.ModelSpace.AddDimAligned(bzpoint5, rbzpoint2, rTemporaryPoint);
            AcadDimAligned oAcadDimAlignedY2 = AcadDoc.ModelSpace.AddDimAligned(rbzpoint2, rbzpoint3, rTemporaryPoint);
            AcadDimAligned oAcadDimAlignedY3 = AcadDoc.ModelSpace.AddDimAligned(rbzpoint3, rbzpoint4, rTemporaryPoint);

            //这段是第一个图的上侧横向标注点
            //bzpoint5就是rbzpoint1
            double[] ubzpoint1 = new double[3] { FDXS * ex + FDXS * fx - FDXS * d0x / 2 - FDXS * bx, bzpoint5[1] + FDXS * cx + FDXS * d0x + FDXS * ax, 0 };
            double[] ubzpoint2 = new double[3] { ubzpoint1[0] + FDXS * bx, ubzpoint1[1], ubzpoint1[2] };
            double[] ubzpoint3 = new double[3] { ubzpoint2[0] + FDXS * d0x, ubzpoint2[1], ubzpoint2[2] };
            double[] ubzpoint4 = new double[3] { ubzpoint3[0] + FDXS * bx, ubzpoint3[1], rbzpoint3[2] };

            //这段是第一个图加上侧竖向标注的线
            AcadDimAligned oAcadDimAlignedZ1 = AcadDoc.ModelSpace.AddDimAligned(ubzpoint1, ubzpoint2, uTemporaryPoint);
            AcadDimAligned oAcadDimAlignedZ2 = AcadDoc.ModelSpace.AddDimAligned(ubzpoint2, ubzpoint3, uTemporaryPoint);
            AcadDimAligned oAcadDimAlignedZ3 = AcadDoc.ModelSpace.AddDimAligned(ubzpoint3, ubzpoint4, uTemporaryPoint);

            //好吧，设置的不长，稳定点吧第一个图的底部横向标注的线1（4个）
            oAcadDimAlignedX1.TextHeight = 300;
            // oAcadDimAligned.Rotation = 0;
            //oAcadDimAligned.ArrowheadSize = 200;
            //oAcadDimAlignedX1.DimensionLineColor = ACAD_COLOR.acGreen;
            //oAcadDimAlignedX1.TextOverride = "长度<>mm";
            oAcadDimAlignedX1.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedX1.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedX1.ArrowheadSize = 100;
            oAcadDimAlignedX1.TextStyle = "dd";
            oAcadDimAlignedX1.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedX1.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedX1.ExtensionLineExtend = 100;
            oAcadDimAlignedX1.ExtensionLineOffset = 100;
            oAcadDimAlignedX1.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedX1.TextInsideAlign = false;
            oAcadDimAlignedX1.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedX1.TextGap = 50;
            double xx = oAcadDimAlignedX1.Measurement / FDXS;
            oAcadDimAlignedX1.TextOverride = xx.ToString("0");

            //第一个图的底部横向标注的线2（4个）
            oAcadDimAlignedX2.TextHeight = 300;
            //oAcadDimAlignedX2.DimensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedX2.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedX2.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedX2.ArrowheadSize = 100;
            oAcadDimAlignedX2.TextStyle = "dd";
            oAcadDimAlignedX2.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedX2.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedX2.ExtensionLineExtend = 100;
            oAcadDimAlignedX2.ExtensionLineOffset = 100;
            oAcadDimAlignedX2.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedX2.TextInsideAlign = false;
            oAcadDimAlignedX2.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedX2.TextGap = 50;
            xx = oAcadDimAlignedX2.Measurement / FDXS;
            oAcadDimAlignedX2.TextOverride = xx.ToString("0");

            //第一个图的底部横向标注的线3（4个）
            oAcadDimAlignedX3.TextHeight = 300;
            //oAcadDimAlignedX3.DimensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedX3.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedX3.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedX3.ArrowheadSize = 100;
            oAcadDimAlignedX3.TextStyle = "dd";
            oAcadDimAlignedX3.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedX3.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedX3.ExtensionLineExtend = 100;
            oAcadDimAlignedX3.ExtensionLineOffset = 100;
            oAcadDimAlignedX3.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedX3.TextInsideAlign = false;
            oAcadDimAlignedX3.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedX3.TextGap = 50;
            xx = oAcadDimAlignedX3.Measurement / FDXS;
            oAcadDimAlignedX3.TextOverride = xx.ToString("0");

            //第一个图的底部横向标注的线4（4个）
            oAcadDimAlignedX4.TextHeight = 300;
            //oAcadDimAlignedX4.DimensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedX4.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedX4.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedX4.ArrowheadSize = 100;
            oAcadDimAlignedX4.TextStyle = "dd";
            oAcadDimAlignedX4.DimensionLineExtend = 100;//吓唬谁啊
                                                        // oAcadDimAlignedX4.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedX4.ExtensionLineExtend = 100;
            oAcadDimAlignedX4.ExtensionLineOffset = 100;
            oAcadDimAlignedX4.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedX4.TextInsideAlign = false;
            oAcadDimAlignedX4.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedX4.TextGap = 50;
            xx = oAcadDimAlignedX4.Measurement / FDXS;
            oAcadDimAlignedX4.TextOverride = xx.ToString("0");

            //第一个图的右侧竖向标注的线1（3个）
            oAcadDimAlignedY1.TextHeight = 300;
            // oAcadDimAlignedY1.DimensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedY1.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedY1.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedY1.ArrowheadSize = 100;
            oAcadDimAlignedY1.TextStyle = "dd";
            oAcadDimAlignedY1.DimensionLineExtend = 100;//吓唬谁啊
                                                        // oAcadDimAlignedY1.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedY1.ExtensionLineExtend = 100;
            oAcadDimAlignedY1.ExtensionLineOffset = 100;
            oAcadDimAlignedY1.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedY1.TextInsideAlign = false;
            oAcadDimAlignedY1.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedY1.TextGap = 50;
            xx = oAcadDimAlignedY1.Measurement / FDXS;
            oAcadDimAlignedY1.TextOverride = xx.ToString("0");

            //第一个图的右侧竖向标注的线2（3个）
            oAcadDimAlignedY2.TextHeight = 300;
            //oAcadDimAlignedY2.DimensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedY2.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedY2.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedY2.ArrowheadSize = 100;
            oAcadDimAlignedY2.TextStyle = "dd";
            oAcadDimAlignedY2.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedY2.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedY2.ExtensionLineExtend = 100;
            oAcadDimAlignedY2.ExtensionLineOffset = 100;
            oAcadDimAlignedY2.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedY2.TextInsideAlign = false;
            oAcadDimAlignedY2.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedY2.TextGap = 50;
            xx = oAcadDimAlignedY2.Measurement / FDXS;
            oAcadDimAlignedY2.TextOverride = xx.ToString("0");

            //第一个图的右侧竖向标注的线3（3个）
            oAcadDimAlignedY3.TextHeight = 300;
            // oAcadDimAlignedY3.DimensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedY3.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedY3.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedY3.ArrowheadSize = 100;
            oAcadDimAlignedY3.TextStyle = "dd";
            oAcadDimAlignedY3.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedY3.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedY3.ExtensionLineExtend = 100;
            oAcadDimAlignedY3.ExtensionLineOffset = 100;
            oAcadDimAlignedY3.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedY3.TextInsideAlign = false;
            oAcadDimAlignedY3.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedY3.TextGap = 50;
            xx = oAcadDimAlignedY3.Measurement / FDXS;
            oAcadDimAlignedY3.TextOverride = xx.ToString("0");


            //第一个图的顶部横向标注的线1（3个）
            oAcadDimAlignedZ1.TextHeight = 300;
            // oAcadDimAlignedZ1.DimensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedZ1.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedZ1.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedZ1.ArrowheadSize = 100;
            oAcadDimAlignedZ1.TextStyle = "dd";
            oAcadDimAlignedZ1.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedZ1.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedZ1.ExtensionLineExtend = 100;
            oAcadDimAlignedZ1.ExtensionLineOffset = 100;
            oAcadDimAlignedZ1.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedZ1.TextInsideAlign = true;//到底能不能不动
                                                     //oAcadDimAlignedZ1.TextPosition =uTemporaryPoint;
            oAcadDimAlignedZ1.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedZ1.TextGap = 50;
            xx = oAcadDimAlignedZ1.Measurement / FDXS;
            oAcadDimAlignedZ1.TextOverride = xx.ToString("0");

            //第一个图的顶部横向标注的线2（3个）
            oAcadDimAlignedZ2.TextHeight = 300;
            // oAcadDimAlignedZ2.DimensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedZ2.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedZ2.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedZ2.ArrowheadSize = 100;
            oAcadDimAlignedZ2.TextStyle = "dd";
            oAcadDimAlignedZ2.DimensionLineExtend = 100;//吓唬谁啊
                                                        // oAcadDimAlignedZ2.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedZ2.ExtensionLineExtend = 100;
            oAcadDimAlignedZ2.ExtensionLineOffset = 100;
            oAcadDimAlignedZ2.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedZ2.TextInsideAlign = true;//到底能不能不动
                                                     //oAcadDimAlignedZ2.TextPosition = uTemporaryPoint;
            oAcadDimAlignedZ2.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedZ2.TextGap = 50;
            xx = oAcadDimAlignedZ2.Measurement / FDXS;
            oAcadDimAlignedZ2.TextOverride = xx.ToString("0");


            //第一个图的顶部横向标注的线3（3个）
            oAcadDimAlignedZ3.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedZ3.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedZ3.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            oAcadDimAlignedZ3.ArrowheadSize = 100;
            oAcadDimAlignedZ3.TextStyle = "dd";
            oAcadDimAlignedZ3.DimensionLineExtend = 100;//吓唬谁啊
                                                        // oAcadDimAlignedZ3.TextPosition = uTemporaryPoint;
                                                        //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            oAcadDimAlignedZ3.ExtensionLineExtend = 100;
            oAcadDimAlignedZ3.ExtensionLineOffset = 100;
            oAcadDimAlignedZ3.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            oAcadDimAlignedZ3.TextInsideAlign = true;//到底能不能不动
            oAcadDimAlignedZ3.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            oAcadDimAlignedZ3.TextGap = 50;
            xx = oAcadDimAlignedZ3.Measurement / FDXS;
            oAcadDimAlignedZ3.TextOverride = xx.ToString("0");







            //  string jpgNamePathDW = System.Windows.Forms.Application.StartupPath + "\\CAD\\双吊耳正面的.dwg";
            //   AcadDoc.SaveAs(jpgNamePathDW);

            //暂时不用






            //这是画第二张侧面图。
            //底面的一条线
            double[] cdStartPoint1 = new double[3] { 0, 0 + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] cdEndPoint1 = new double[3] { FDXS * gx, 0 + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] cdEndPoint2 = new double[3] { cdEndPoint1[0] + FDXS * tx, cdEndPoint1[1], cdEndPoint1[2] };
            double[] cdEndPoint3 = new double[3] { cdEndPoint2[0] + FDXS * gx, cdEndPoint2[1], cdEndPoint2[2] };
            AcadLine cdoAcadLine1 = AcadDoc.ModelSpace.AddLine(cdStartPoint1, cdEndPoint1);
            AcadLine cdoAcadLine2 = AcadDoc.ModelSpace.AddLine(cdEndPoint1, cdEndPoint2);
            AcadLine cdoAcadLine3 = AcadDoc.ModelSpace.AddLine(cdEndPoint2, cdEndPoint3);

            //第二张侧面图左边底部三条线

            double[] cd3lStartPoint1 = new double[3] { 0, FDXS * gdx + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] cd3lEndPoint1 = new double[3] { cd3lStartPoint1[0] + FDXS * gx - FDXS * gdx, FDXS * gx + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] cd3lEndPoint2 = new double[3] { FDXS * gx, FDXS * gx + 2 * FDXS * (cx + d0x + ax), 0 };

            AcadLine cd3loAcadLine1 = AcadDoc.ModelSpace.AddLine(cdStartPoint1, cd3lStartPoint1);
            AcadLine cd3loAcadLine2 = AcadDoc.ModelSpace.AddLine(cd3lStartPoint1, cd3lEndPoint1);
            AcadLine cd3loAcadLine3 = AcadDoc.ModelSpace.AddLine(cd3lEndPoint1, cd3lEndPoint2);

            //第二张侧面图右边底部三条线


            double[] cd3rEndPoint1 = new double[3] { FDXS * gx + FDXS * tx, FDXS * gx + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] cd3rEndPoint2 = new double[3] { cd3rEndPoint1[0] + FDXS * gdx, cd3rEndPoint1[1], 0 };
            double[] cd3rEndPoint3 = new double[3] { cd3rEndPoint1[0] + FDXS * gx, FDXS * gdx + 2 * FDXS * (cx + d0x + ax), 0 };
            AcadLine cd3roAcadLine1 = AcadDoc.ModelSpace.AddLine(cd3rEndPoint1, cd3rEndPoint2);
            AcadLine cd3roAcadLine2 = AcadDoc.ModelSpace.AddLine(cd3rEndPoint2, cd3rEndPoint3);
            AcadLine cd3roAcadLine3 = AcadDoc.ModelSpace.AddLine(cd3rEndPoint3, cdEndPoint3);

            //第二张侧面图中间五条线
            double[] zsl5EndPoint1 = new double[3] { FDXS * gx, FDXS * (cx + d0x + ax) + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] zsl5EndPoint2 = new double[3] { FDXS * gx + FDXS * tx, FDXS * (cx + d0x + ax) + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] zsl5EndPoint3 = new double[3] { FDXS * gx, FDXS * cx + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] zsl5EndPoint4 = new double[3] { FDXS * gx + FDXS * tx, FDXS * cx + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] zsl5EndPoint5 = new double[3] { FDXS * gx, FDXS * cx + FDXS * d0x + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] zsl5EndPoint6 = new double[3] { FDXS * gx + FDXS * tx, FDXS * cx + FDXS * d0x + 2 * FDXS * (cx + d0x + ax), 0 };
            //竖向两条线
            AcadLine zsl5AcadLine1 = AcadDoc.ModelSpace.AddLine(cdEndPoint1, zsl5EndPoint1);
            AcadLine zsl5AcadLine2 = AcadDoc.ModelSpace.AddLine(cdEndPoint2, zsl5EndPoint2);
            //横向三条线
            AcadLine zsl5AcadLine3 = AcadDoc.ModelSpace.AddLine(zsl5EndPoint1, zsl5EndPoint2);
            AcadLine zsl5AcadLine4 = AcadDoc.ModelSpace.AddLine(zsl5EndPoint3, zsl5EndPoint4);
            AcadLine zsl5AcadLine5 = AcadDoc.ModelSpace.AddLine(zsl5EndPoint5, zsl5EndPoint6);

            //侧面图的中心线
            double[] cdwxz = new double[3] { FDXS * gx + FDXS * tx / 2, 0 + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] cdwxy = new double[3] { FDXS * gx + FDXS * tx / 2, FDXS * cx + FDXS * d0x + FDXS * ax + 2 * FDXS * (cx + d0x + ax), 0 };

            AcadLine cdwx1 = AcadDoc.ModelSpace.AddLine(cdwxz, cdwxy);
            cdwx1.Linetype = "center";
            cdwx1.LinetypeScale = 5;



            double[] d2TemporaryPoint = new double[3] { 2000, -800 + 2 * FDXS * (cx + d0x + ax), 0 };
            double[] r2TemporaryPoint = new double[3] { FDXS * 2 * gx + FDXS * tx + 800, 3000 + 2 * FDXS * (cx + d0x + ax), 0 };


            //这段是加第二个图的底部横向标注的线

            AcadDimAligned cAcadDimAlignedX2 = AcadDoc.ModelSpace.AddDimAligned(cdEndPoint1, cdEndPoint2, d2TemporaryPoint);


            //这段是加第二个图的右侧竖向标注的线
            //cdEndPoint3
            double[] bzcrEndPoint1 = new double[3] { cdEndPoint3[0], cdEndPoint3[1] + FDXS * cx, 0 };
            double[] bzcrEndPoint2 = new double[3] { bzcrEndPoint1[0], bzcrEndPoint1[1] + FDXS * d0x, 0 };
            double[] bzcrEndPoint3 = new double[3] { bzcrEndPoint2[0], bzcrEndPoint2[1] + FDXS * ax, 0 };

            AcadDimAligned cAcadDimAlignedY1 = AcadDoc.ModelSpace.AddDimAligned(cdEndPoint3, bzcrEndPoint1, r2TemporaryPoint);
            AcadDimAligned cAcadDimAlignedY2 = AcadDoc.ModelSpace.AddDimAligned(bzcrEndPoint1, bzcrEndPoint2, r2TemporaryPoint);
            AcadDimAligned cAcadDimAlignedY3 = AcadDoc.ModelSpace.AddDimAligned(bzcrEndPoint2, bzcrEndPoint3, r2TemporaryPoint);

            //第二个图的底部横向标注的线1（1个）
            cAcadDimAlignedX2.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            cAcadDimAlignedX2.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            cAcadDimAlignedX2.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            cAcadDimAlignedX2.ArrowheadSize = 100;
            cAcadDimAlignedX2.TextStyle = "dd";
            cAcadDimAlignedX2.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            cAcadDimAlignedX2.ExtensionLineExtend = 100;
            cAcadDimAlignedX2.ExtensionLineOffset = 100;
            cAcadDimAlignedX2.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            cAcadDimAlignedX2.TextInsideAlign = false;
            cAcadDimAlignedX2.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            cAcadDimAlignedX2.TextGap = 50;
            xx = cAcadDimAlignedX2.Measurement / FDXS;
            cAcadDimAlignedX2.TextOverride = xx.ToString("0");

            //第二个图的右侧竖向标注的线1（3个）

            cAcadDimAlignedY1.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            cAcadDimAlignedY1.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            cAcadDimAlignedY1.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            cAcadDimAlignedY1.ArrowheadSize = 100;
            cAcadDimAlignedY1.TextStyle = "dd";
            cAcadDimAlignedY1.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            cAcadDimAlignedY1.ExtensionLineExtend = 100;
            cAcadDimAlignedY1.ExtensionLineOffset = 100;
            cAcadDimAlignedY1.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            cAcadDimAlignedY1.TextInsideAlign = false;
            cAcadDimAlignedY1.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            cAcadDimAlignedY1.TextGap = 50;
            xx = cAcadDimAlignedY1.Measurement / FDXS;
            cAcadDimAlignedY1.TextOverride = xx.ToString("0");

            //第二个图的右侧竖向标注的线2（3个）

            cAcadDimAlignedY2.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            cAcadDimAlignedY2.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            cAcadDimAlignedY2.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            cAcadDimAlignedY2.ArrowheadSize = 100;
            cAcadDimAlignedY2.TextStyle = "dd";
            cAcadDimAlignedY2.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            cAcadDimAlignedY2.ExtensionLineExtend = 100;
            cAcadDimAlignedY2.ExtensionLineOffset = 100;
            cAcadDimAlignedY2.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            cAcadDimAlignedY2.TextInsideAlign = false;
            cAcadDimAlignedY2.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            cAcadDimAlignedY2.TextGap = 50;
            xx = cAcadDimAlignedY2.Measurement / FDXS;
            cAcadDimAlignedY2.TextOverride = xx.ToString("0");

            //第二个图的右侧竖向标注的线3（3个）

            cAcadDimAlignedY3.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            cAcadDimAlignedY3.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            cAcadDimAlignedY3.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            cAcadDimAlignedY3.ArrowheadSize = 100;
            cAcadDimAlignedY3.TextStyle = "dd";
            cAcadDimAlignedY3.DimensionLineExtend = 100;//吓唬谁啊
                                                        //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            cAcadDimAlignedY3.ExtensionLineExtend = 100;
            cAcadDimAlignedY3.ExtensionLineOffset = 100;
            cAcadDimAlignedY3.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            cAcadDimAlignedY3.TextInsideAlign = false;
            cAcadDimAlignedY3.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            cAcadDimAlignedY3.TextGap = 50;
            xx = cAcadDimAlignedY3.Measurement / FDXS;
            cAcadDimAlignedY3.TextOverride = xx.ToString("0");

            //oAcadDimAligned.Rotation = 0;
            // string jpgNamePaths2 = "C:\\d2.jpg";//打印文件




            //这是画第三张底面图。
            //底面的一条线的点和标注点
            double[] dmdStartPoint1 = new double[3] { 0, 0 + 4 * FDXS * (cx + d0x + ax), 0 };
            double[] dmdEndPoint1 = new double[3] { FDXS * ex, 0 + 4 * FDXS * (cx + d0x + ax), 0 };
            double[] dmdEndPoint2 = new double[3] { dmdEndPoint1[0] + FDXS * fx, 0 + 4 * FDXS * (cx + d0x + ax), 0 };
            double[] dmdEndPoint3 = new double[3] { dmdEndPoint2[0] + FDXS * fx, 0 + 4 * FDXS * (cx + d0x + ax), 0 };
            double[] dmdEndPoint4 = new double[3] { dmdEndPoint3[0] + FDXS * ex, 0 + 4 * FDXS * (cx + d0x + ax), 0 };

            //上面的一条线的点和标注点
            double[] dmsStartPoint1 = new double[3] { 0, FDXS * tx + 4 * FDXS * (cx + d0x + ax), 0 };
            double[] dmsEndPoint1 = new double[3] { dmdEndPoint1[0], dmdEndPoint1[1] + FDXS * tx, 0 };
            double[] dmsEndPoint2 = new double[3] { dmdEndPoint2[0], dmdEndPoint2[1] + FDXS * tx, 0 };
            double[] dmsEndPoint3 = new double[3] { dmdEndPoint3[0], dmdEndPoint3[1] + FDXS * tx, 0 };
            double[] dmsEndPoint4 = new double[3] { dmdEndPoint4[0], dmdEndPoint4[1] + FDXS * tx, 0 };

            //底下一条线
            AcadLine dmdAcadLine1 = AcadDoc.ModelSpace.AddLine(dmdStartPoint1, dmdEndPoint4);
            //上面一条线
            AcadLine dmdAcadLine2 = AcadDoc.ModelSpace.AddLine(dmsStartPoint1, dmsEndPoint4);

            //竖向线1-5
            AcadLine dmdsxAcadLine1 = AcadDoc.ModelSpace.AddLine(dmdStartPoint1, dmsStartPoint1);
            AcadLine dmdsxAcadLine2 = AcadDoc.ModelSpace.AddLine(dmdEndPoint1, dmsEndPoint1);
            AcadLine dmdsxAcadLine3 = AcadDoc.ModelSpace.AddLine(dmdEndPoint2, dmsEndPoint2);
            AcadLine dmdsxAcadLine4 = AcadDoc.ModelSpace.AddLine(dmdEndPoint3, dmsEndPoint3);
            AcadLine dmdsxAcadLine5 = AcadDoc.ModelSpace.AddLine(dmdEndPoint4, dmsEndPoint4);

            //这段是加第三个图的底部横向标注的线
            double[] dmdd2TemporaryPoint = new double[3] { 2000, -800 + 4 * FDXS * (cx + d0x + ax), 0 };
            double[] dmdr2TemporaryPoint = new double[3] { FDXS * 2 * ex + FDXS * 2 * fx + 800, 3000 + 4 * FDXS * (cx + d0x + ax), 0 };

            //这段是加第三个图的底部横向标注的线
            AcadDimAligned dmdAcadDimAlignedX1 = AcadDoc.ModelSpace.AddDimAligned(dmdStartPoint1, dmdEndPoint1, dmdd2TemporaryPoint);
            AcadDimAligned dmdAcadDimAlignedX2 = AcadDoc.ModelSpace.AddDimAligned(dmdEndPoint1, dmdEndPoint2, dmdd2TemporaryPoint);
            AcadDimAligned dmdAcadDimAlignedX3 = AcadDoc.ModelSpace.AddDimAligned(dmdEndPoint2, dmdEndPoint3, dmdd2TemporaryPoint);
            AcadDimAligned dmdAcadDimAlignedX4 = AcadDoc.ModelSpace.AddDimAligned(dmdEndPoint3, dmdEndPoint4, dmdd2TemporaryPoint);


            //这段是加第三个图的右侧横向标注的线
            AcadDimAligned dmdAcadDimAlignedY1 = AcadDoc.ModelSpace.AddDimAligned(dmdEndPoint4, dmsEndPoint4, dmdr2TemporaryPoint);

            //第三个图的标注的线1（5个）
            dmdAcadDimAlignedX1.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedX1.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedX1.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedX1.ArrowheadSize = 100;
            dmdAcadDimAlignedX1.TextStyle = "dd";
            dmdAcadDimAlignedX1.DimensionLineExtend = 100;//吓唬谁啊
                                                          //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedX1.ExtensionLineExtend = 100;
            dmdAcadDimAlignedX1.ExtensionLineOffset = 100;
            dmdAcadDimAlignedX1.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            dmdAcadDimAlignedX1.TextInsideAlign = false;
            dmdAcadDimAlignedX1.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            dmdAcadDimAlignedX1.TextGap = 50;
            xx = dmdAcadDimAlignedX1.Measurement / FDXS;
            dmdAcadDimAlignedX1.TextOverride = xx.ToString("0");

            //第三个图的标注的线2（5个）
            dmdAcadDimAlignedX2.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedX2.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedX2.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedX2.ArrowheadSize = 100;
            dmdAcadDimAlignedX2.TextStyle = "dd";
            dmdAcadDimAlignedX2.DimensionLineExtend = 100;//吓唬谁啊
                                                          //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedX2.ExtensionLineExtend = 100;
            dmdAcadDimAlignedX2.ExtensionLineOffset = 100;
            dmdAcadDimAlignedX2.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            dmdAcadDimAlignedX2.TextInsideAlign = false;
            dmdAcadDimAlignedX2.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            dmdAcadDimAlignedX2.TextGap = 50;
            xx = dmdAcadDimAlignedX2.Measurement / FDXS;
            dmdAcadDimAlignedX2.TextOverride = xx.ToString("0");

            //第三个图的标注的线3（5个）
            dmdAcadDimAlignedX3.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedX3.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedX3.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedX3.ArrowheadSize = 100;
            dmdAcadDimAlignedX3.TextStyle = "dd";
            dmdAcadDimAlignedX3.DimensionLineExtend = 100;//吓唬谁啊
                                                          //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedX3.ExtensionLineExtend = 100;
            dmdAcadDimAlignedX3.ExtensionLineOffset = 100;
            dmdAcadDimAlignedX3.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            dmdAcadDimAlignedX3.TextInsideAlign = false;
            dmdAcadDimAlignedX3.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            dmdAcadDimAlignedX3.TextGap = 50;
            xx = dmdAcadDimAlignedX3.Measurement / FDXS;
            dmdAcadDimAlignedX3.TextOverride = xx.ToString("0");

            //第三个图的标注的线4（5个）
            dmdAcadDimAlignedX4.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedX4.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedX4.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedX4.ArrowheadSize = 100;
            dmdAcadDimAlignedX4.TextStyle = "dd";
            dmdAcadDimAlignedX4.DimensionLineExtend = 100;//吓唬谁啊
                                                          //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedX4.ExtensionLineExtend = 100;
            dmdAcadDimAlignedX4.ExtensionLineOffset = 100;
            dmdAcadDimAlignedX4.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            dmdAcadDimAlignedX4.TextInsideAlign = false;
            dmdAcadDimAlignedX4.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            dmdAcadDimAlignedX4.TextGap = 50;
            xx = dmdAcadDimAlignedX4.Measurement / FDXS;
            dmdAcadDimAlignedX4.TextOverride = xx.ToString("0");

            //第三个图的标注的线5（5个）
            dmdAcadDimAlignedY1.TextHeight = 300;
            // oAcadDimAlignedZ3.DimensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedY1.Arrowhead1Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedY1.Arrowhead2Type = AutoCAD.AcDimArrowheadType.acArrowArchTick;
            dmdAcadDimAlignedY1.ArrowheadSize = 100;
            dmdAcadDimAlignedY1.TextStyle = "dd";
            dmdAcadDimAlignedY1.DimensionLineExtend = 100;//吓唬谁啊
                                                          //oAcadDimAlignedZ3.ExtensionLineColor = ACAD_COLOR.acGreen;
            dmdAcadDimAlignedY1.ExtensionLineExtend = 100;
            dmdAcadDimAlignedY1.ExtensionLineOffset = 100;
            dmdAcadDimAlignedY1.HorizontalTextPosition = AcDimHorizontalJustification.acHorzCentered;
            dmdAcadDimAlignedY1.TextInsideAlign = false;
            dmdAcadDimAlignedY1.VerticalTextPosition = AcDimVerticalJustification.acAbove;
            dmdAcadDimAlignedY1.TextGap = 50;
            xx = dmdAcadDimAlignedY1.Measurement / FDXS;
            dmdAcadDimAlignedY1.TextOverride = xx.ToString("0");


            string ctx2s31DD = DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒");
            string ctx2TT1DD = "\\CAD\\单吊耳";
            string ctx2JW1DD = ".dwg";
            string jpgNamePathsDW31DD = System.Windows.Forms.Application.StartupPath + ctx2TT1DD + ctx2s31DD + ctx2JW1DD;
        
            AcadDoc.SaveAs(jpgNamePathsDW31DD);
            AcadDoc.Save();//到底关不关呢？
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }
    }
}
