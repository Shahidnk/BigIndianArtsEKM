﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BigIndianArts.Models
{
    public class Boarding
    {
        #region Properties

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        public View RotatorItem { get; set; }

        #endregion
    }
}
