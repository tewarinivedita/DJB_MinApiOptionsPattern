using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJB_Core.Models
{
    public class HomeViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string MetaData { get; set; } = string.Empty;
        public List<string> StickyHeaders { get; set; } = new List<string>();
        public ImagesViewModel LoginBtnImg { get; set; }

        public ImagesViewModel ToggleMenuBtnImg { get; set; }

        public ImagesViewModel LogoHeaderImg { get; set; }
        public List<BannerViewModel> Banners { get; set; } = new List<BannerViewModel>();

    }
}
