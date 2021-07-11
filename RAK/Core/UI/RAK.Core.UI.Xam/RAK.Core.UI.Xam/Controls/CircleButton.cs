using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Controls
{
    public class CircleButton : Button
    {
        private int size;
        public int Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
                this.WidthRequest = this.Size;
                this.HeightRequest = this.Size;
                this.BorderRadius = this.Size / 2;
                this.FontSize = this.Size / 2;
            }
        }

        //public new int WidthRequest
        //{
        //    get
        //    {
        //        return this.Size;
        //    }
        //}

        //public new int HeightRequest
        //{
        //    get
        //    {
        //        return this.Size;
        //    }
        //}

        //public new int FontSize
        //{
        //    get
        //    {
        //        return this.Size / 2;
        //    }
        //}

        //public new int BorderRadius
        //{
        //    get
        //    {
        //        return this.Size / 2;
        //    }
        //}



        public CircleButton()
        {
            //this.WidthRequest = this.Size;
            //this.HeightRequest = this.Size;
            //this.BorderRadius = this.Size / 2;
            //this.FontSize = this.Size / 2;
        }


    }
}
