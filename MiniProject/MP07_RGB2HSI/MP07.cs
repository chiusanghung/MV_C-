﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project07
{
    public partial class MP07 : Form
    {
        public MP07()
        {
            InitializeComponent();
            Bitmap HinhGoc = new Bitmap(@"D:\Tai Lieu\Machine Vision\lena_color.gif");
            
            picBoxHinhGoc.Image= HinhGoc;

            List<Bitmap> HSI = ChuyenDoiRBGsangHSI(HinhGoc);

            picBoxH.Image = HSI[0];
            picBoxS.Image = HSI[1];
            picBoxI.Image = HSI[2];
            picBoxHSI.Image = HSI[3];

        }

        public List<Bitmap> ChuyenDoiRBGsangHSI(Bitmap hinhgoc)
        {
            List<Bitmap> HSI = new List<Bitmap>();

            Bitmap Hue = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap Saturation = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap Intensity = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap HSIImg = new Bitmap(hinhgoc.Width, hinhgoc.Height);

            for (int x =0; x <hinhgoc.Width; x++)
            
                for (int y =0; y< hinhgoc.Height; y++)
                {
                    //lấy thông tin điểm ảnh
                    Color pixel = hinhgoc.GetPixel(x, y);

                    //do quá trình tính toán các giá trị HSI kết quả trả về của các công thức đều là kiểu double
                    double R = pixel.R;
                    double G = pixel.G;
                    double B = pixel.B;


                    // dựa theo công thức ta có
                    //t1 phần tử số 
                    //t2 là phần mẫu số của công thức 
                    double t1 = ((R - G) + (R - B) / 2);
                    double t2 = ((R - G) * (R - G) + Math.Sqrt((R - B)*(G - B)));
                    //kết quả trả về Acos là radian
                    double theta = Math.Acos(t1/t2);

                    double H = 0;

                    // nếu mà blue <= Green thì Hue =  theta
                    if (B <= G)
                        H = theta;
                    else  //  con ngược lại thì Hue tính theo công thức này 
                        H = 2*Math.PI - theta;

                    H = H+180/Math.PI; // chuyển đổi từ radian sang độ

                    // đây là công thức tính giá trị kênh Saturation
                    double S = 1 - 3 * Math.Min(R, Math.Min(G, B)) / (R + G + B);

                    // do giá trị tính ra của S sẽ nằm trong khoảng [0;1]
                    // để mà bitmap có thể hiện được thì mình phải convert S sang khoảng giá trị 0, 255 công thức dưới đây chuyển đổi từ rank 0 , 1 sang rank 0,255
                    // 
                    double I = (R + G + B) / 3;


                    // Hiển thị các kênh giá trị HSI tại các picBox
                    // chú ý là phải ép kểu các giá trị HSI về kiểu byte để hình bitmap
                    Hue.SetPixel(x, y, Color.FromArgb((byte)H, (byte)H, (byte)H));
                    // riêng giá trị s, hoặc mình có thể normalized như trên hoặc minh có thể normalized chỉ lức hiển thị này thôi
                    Saturation.SetPixel(x, y, Color.FromArgb((byte)(S*255), (byte)(S*255), (byte)(S*255)));
                    Intensity.SetPixel(x, y, Color.FromArgb((byte)I, (byte)I, (byte)I));
                    // kết hợp kênh H,S,I tạo thành hình HSI
                    HSIImg.SetPixel(x,y,Color.FromArgb((byte)H, (byte)S,(byte)I));  

                }

                HSI.Add(Hue);
                HSI.Add(Saturation);
                HSI.Add(Intensity);
                HSI.Add(HSIImg);

                return HSI;

            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
