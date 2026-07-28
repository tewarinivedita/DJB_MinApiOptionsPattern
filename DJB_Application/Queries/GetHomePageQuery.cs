using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DJB_Core.Entities;
using DJB_Core.Interfaces;
using DJB_Core.Models;
using MediatR;

namespace DJB_Application.Queries
{
    public record GetHomePageQuery() : IRequest<HomeViewModel>;
    public class GetHomePageQueryHandler(IProductRepository productRepository)
        : IRequestHandler<GetHomePageQuery, HomeViewModel>
    {
        public async Task<HomeViewModel> Handle(GetHomePageQuery request, CancellationToken cancellationToken)
        {
           // var ds = productRepository.GetProducts();

            return await new Task<HomeViewModel>(() =>
            {
                return new HomeViewModel
                {
                    Title = "Home Page",
                    MetaData = "Welcome to our home page!",
                    StickyHeaders = new List<string> { "Header1", "Header2", "Header3" },
                    LoginBtnImg = new ImagesViewModel { Url = "/images/login.png", AltText = "Login" },
                    ToggleMenuBtnImg = new ImagesViewModel { Url = "/images/toggle-menu.png", AltText = "Toggle Menu" },
                    LogoHeaderImg = new ImagesViewModel { Url = "/images/logo.png", AltText = "Logo" },
                    //Banners = ds.Select(p => new BannerViewModel
                    //{
                    //    Title = p.Title,
                    //    BannerButtonText = p.BannerButtonText,
                    //    BannerHeaderText = p.BannerHeaderText,
                    //    BannerSubHeaderText = p.BannerSubHeaderText,
                    //    Images = new ImagesViewModel
                    //    {
                    //        Url = p.ImageUrl,
                    //        AltText = p.Title,
                    //        Title = p.Title
                    //    }

                    //}).ToList()
                };
            });
        }
    }
}
