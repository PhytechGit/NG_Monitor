
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using QRCoder;

public class Print 
 {
    private Font fontSmall;
    private Font fontMedium;
    private Font fontLarge;
    private Font fontxLarge;
//    private Font fontBarcode;
    private Font Myfont;
    String sTitle, sType;//, sName;
    byte nTypeID;
    Image imgRcm;
    bool bFirstSticker;

    //public Print() 
    //{
    //    Printing();
    //}

    public Print(string sID, string type, byte typeID ,Image aus/*,  Image bc, Image QR, Image lg*/)
     {
         sTitle = sID;
         sType = type;
         nTypeID = typeID;
         imgRcm = aus;
         //s_imgBarcode = bc;
//         s_imgQRcode = QR;
//         s_imgLogo = lg;
         Printing();
     }
     // The PrintPage event is raised for each page to be printed.

     private void pd_PrintPage(object sender, PrintPageEventArgs ev) 
     {
         string s0 = "TYPE: "+sType;
        string s5 = "ID: "+sTitle;
        string st = sType;
         string s1 = "FCCID: 2ALN6102";
         string s2 = "MANUFACTURER: PHYTECH LTD\n";
         string s3 = /*"S/N:\n " + */sTitle;
         string s4 = "*" + sTitle + "*";
        float x = 3f, y = 12f ;
        /*
         * SENSOR TYPE:

SD/DEND/WATER/RAIN/PIVOT

FCCID: 2ALN6102

MANUFACTURER:

PHYTECH LTD

S/N: XXXXXXXX
         */
        if (bFirstSticker == true)
        {
            ev.Graphics.DrawString(s5, fontxLarge, Brushes.Black, x, y, new StringFormat());
            ev.Graphics.DrawString(sType, fontLarge, Brushes.Black, x, y + 15, new StringFormat());
            //ev.Graphics.DrawString(st, fontLarge, Brushes.Black, x, y + 5, new StringFormat());
            ev.Graphics.DrawString(s1, fontMedium, Brushes.Black, x, y + 30, new StringFormat());
            ev.Graphics.DrawString(s2, fontSmall, Brushes.Black, x, y + 40, new StringFormat());
            //ev.Graphics.DrawString(s4, fontBarcode, Brushes.Black, x, 70, new StringFormat());
            ev.Graphics.DrawImage(imgRcm, x + 24, y + 50, 15, 15);
        }
        else
        {
            // print on next sticker:
            x = 3f;
            st += "  ";
            //type
            ev.Graphics.DrawString(st, Myfont, Brushes.Black, x, y, new StringFormat());
            //id
            ev.Graphics.DrawString(s3, Myfont, Brushes.Black, x+30, y, new StringFormat());

            //print QR code  
            QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)0;// (level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                //string sQR = sTitle + "  " + sName;//{"id": 123456, "type_id": 32}
                string sQR = string.Format("\"id\": {0}, \"type_id\": {1}, \"type\": \"sensor\"", sTitle, nTypeID);
                sQR = '{' + sQR + '}';
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(sQR, eccLevel))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        Bitmap bitQr = qrCode.GetGraphic(2, Color.Black, Color.White, null, 0, 0);
                        ev.Graphics.DrawImage(bitQr, 6f, y + 12, 60, 60);// bitQr.Width,bitQr.Height);
                    }
                }
            }
        }
    }

    //private void pd_PrintPage2(object sender, PrintPageEventArgs ev)
    //{
    //    string st = sType;
    //    ev.Graphics.DrawString(st, fontxLarge, Brushes.Black, 47, 165, new StringFormat());

    //}
    // Print the file.

    string GetDefaultPrinter()
    {
        PrinterSettings settings = new PrinterSettings();
        foreach (string printer in PrinterSettings.InstalledPrinters)
        {
            settings.PrinterName = printer;
            if (settings.IsDefaultPrinter)
                return printer;
        }
        return string.Empty;
    }

    public void Printing()
     {
         try 
         {
            PrintDialog printDialog1 = new PrintDialog();
//            printDialog1.PrinterSettings.PrinterName = "‏‏Godex G530";
            printDialog1.PrinterSettings.PrinterName = GetDefaultPrinter();// "Godex G530";/*printDialog1.PrinterSettings.PrinterName;*///printDialog1.PrinterSettings.P "Citizen CL-S321";//

            //DialogResult result = printDialog1.ShowDialog();
            //if (result != DialogResult.OK)
            //    return;


            //try 
            //{
            float fSize = 2.8f;
                fontSmall = new Font("Antique Olive", fSize, FontStyle.Bold);
                fontMedium = new Font("Antique Olive", 4, FontStyle.Bold);
                fSize = 6f;
                fontLarge = new Font("Antique Olive", fSize, FontStyle.Bold);
                fontxLarge = new Font("Antique Olive", 8, FontStyle.Bold);
            fSize = 7f;
            Myfont = new Font("Antique Olive", fSize, FontStyle.Bold);
            PrintDocument pd = new PrintDocument(); 
               pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
               // Set the page orientation to landscape.
               pd.DefaultPageSettings.Landscape = true;
            pd.PrinterSettings.PrinterName = printDialog1.PrinterSettings.PrinterName;//"Godex G530";//printDialog1.PrinterSettings.P "Citizen CL-S321";//
            pd.DefaultPageSettings.PaperSize = new PaperSize("Small_Sticker", 90, 90);
            // Specify the printer to use.

            // Print the document.
            bFirstSticker = true;
            pd.Print();
            bFirstSticker = false;
            pd.Print();
            //finally 
            //{
            //   //streamToPrint.Close() ;
            //}
        }
        catch (Exception ex) 
        { 
            MessageBox.Show(ex.Message);
        }

     }

 }