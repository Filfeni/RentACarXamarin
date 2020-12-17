using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using RentACar.Models;
using RentACar;

namespace RentACar.Services
{
    public interface IApiService
    {
        [Get(Config.BrandsApiUrl)]
        Task<ObservableCollection<Brand>> GetBrandAsync();
    }
}
