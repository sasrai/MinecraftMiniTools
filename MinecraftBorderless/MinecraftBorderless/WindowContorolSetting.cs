using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MinecraftBorderless
{
    public class WindowContorolSetting
    {
        Size MinecraftDefaultSize = new Size(854, 480);
        const int ConfigVersion = 1;

        public class WindowOptionSetting
        {
            public bool IsBorderVisible { get; set; }
            public bool IsTopMostWindow { get; set; }

            /// <summary>
            /// 初期化ー
            /// </summary>
            public WindowOptionSetting()
            {
                IsBorderVisible = true;
                IsTopMostWindow = false;
            }
        }
        public WindowOptionSetting WindowOption = new WindowOptionSetting();

        public Rectangle WindowPosition { get; set; }

        /// <summary>
        /// 初期化ー
        /// </summary>
        public WindowContorolSetting()
        {
            // 初期位置はデスクトップの中央
            this.WindowPosition = new Rectangle(
                SystemInformation.WorkingArea.Width / 2 - (SystemInformation.FrameBorderSize.Width + MinecraftDefaultSize.Width / 2),
                SystemInformation.WorkingArea.Height / 2 - (SystemInformation.CaptionHeight + SystemInformation.FrameBorderSize.Height + MinecraftDefaultSize.Height / 2),
                MinecraftDefaultSize.Width + SystemInformation.FrameBorderSize.Width * 2,
                MinecraftDefaultSize.Height + SystemInformation.CaptionHeight + SystemInformation.FrameBorderSize.Height * 2);
        }

        public bool Save(string filename = "wcontrol.conf")
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(this.GetType());

            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Create);

            serializer.Serialize(fs, this);

            fs.Close();
            fs.Dispose();

            return true;
        }

        public bool Load(string filename = "wcontrol.conf")
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(this.GetType());

            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open);

            WindowContorolSetting wcs = (WindowContorolSetting) serializer.Deserialize(fs);

            fs.Close();
            fs.Dispose();

            this.WindowPosition = wcs.WindowPosition;
            this.WindowOption = wcs.WindowOption;

            return true;
        }
    }
}
