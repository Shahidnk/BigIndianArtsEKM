using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigIndianArts.Interface
{
    public interface IImageResizer
    {
        Task<byte[]> ResizeImage(byte[] imageData);
    }
}
